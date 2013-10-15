/*
' Copyright (c) 2010  DotNetNuke Corporation
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DotNetNuke.Modules.Ourspace_Utilities;
using System.Globalization;


namespace DotNetNuke.Modules.Ourspace_Microprofile
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_Microprofile class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_MicroprofileModuleBase, IActionable
    {

        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                
                // Replace with Paul or Annas userId so Paul can show Anna without affecting other users.
                if (UserId == 45 || UserId == 75)
                {
                    if(Request.QueryString["googleConversion"] != null)
                    {
                        pnlGoogleConversion.Visible = true;
                    }
                       
                    Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "pageGuide", (this.TemplateSourceDirectory + "/js/pageguide.min.js?v=1"));
                    
                }
                if (UserId > -1)
                {   Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "microprofile", (this.TemplateSourceDirectory + "/js/microprofile.js?v=2"));
                 
               
                }
                if (!IsPostBack)
                {
                    ltrl_UserId.Text = UserId.ToString();

                    if (UserId > -1)
                    {

                        SetLikeButtonCode();
                        referPanel.Visible = true;
                        int profilePageTabId = 71;
                        HideReferralPanels();
                        string promoPageUrl = "www.joinourspace.eu/pr/tabid/307/language/en-GB/refx/" + UserId + "/Default.aspx";
                        if (CultureInfo.CurrentCulture.Name == "el-GR")
                        {
                            promoPageUrl = "www.joinourspace.eu/pr/tabid/308/language/el-GR/refx/" + UserId + "/Default.aspx";

                            pnlReferralElGr.Visible = true;
                            profilePageTabId = 91;
                        }
                        else if (CultureInfo.CurrentCulture.Name == "cs-CZ")
                        {
                            profilePageTabId = 104;
                            pnlReferralCsCz.Visible = true;
                            promoPageUrl = "www.joinourspace.eu/pr/tabid/309/language/cs-CZ/refx/" + UserId + "/Default.aspx";

                        }
                        else if (CultureInfo.CurrentCulture.Name == "de-AT")
                        {
                            profilePageTabId = 174;
                            pnlReferralDeAt.Visible = true;
                            promoPageUrl = "www.joinourspace.eu/pr/tabid/310/language/de-AT/refx/" + UserId + "/Default.aspx";

                        }
                        else
                        {
                            pnlReferralEnGb.Visible = true;
                        }

                        hprlnk_toProfile.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(profilePageTabId);

                        lbl_personalSharingUrl.Text = promoPageUrl;
                        lbl_personalSharingUrlElGr.Text = promoPageUrl;
                        lbl_personalSharingUrlCsCz.Text = promoPageUrl;
                        lbl_personalSharingUrlDeAt.Text = promoPageUrl;
                        lblName.Text = UserInfo.FirstName;
                        lbl_FacebookId.Text = AuthenticateUser().ToString();



                        // This is the only place that FacebookuserId can be defined
                        Session["FacebookUserId"] = lbl_FacebookId.Text;

                        Ourspace_Utilities.View util = new Ourspace_Utilities.View();

                        img_Profile.ImageUrl = util.GetOurSpaceUserImgUrl(Server, UserId);





                    }
                    else
                    {
                        hprlnk_toProfile.Visible = false;
                        img_Profile.Visible = false;

                        //ContainerControl.Visible = false;

                    }

                    lblLanguage.Text = CultureInfo.CurrentUICulture.Name;

                }
                else
                {
                    pnlGoogleConversion.Visible = false;
                }

                try
                {
                    string currentCookieCulture = "no cookie found";
                    string currentCulture = CultureInfo.CurrentCulture.Name;
                    if (Request.Cookies["language"] != null)
                    {
                        currentCookieCulture = Request.Cookies["language"].Value;
                    }

                    if (Request.QueryString["facebook"] != null)
                    {
                        hprlnk_toProfile.Visible = false;
                        img_Profile.Visible = false;
                    }


                    string test = UserInfo.Profile.GetPropertyValue("PreferredLocale");

                    lblCulture.Text = currentCulture;
                    lblCookie.Text = currentCookieCulture;
                    lblLangSetting.Text = test;
                }
                catch (Exception ex) { string exception = ex.Message; }

               
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void HideReferralPanels()
        {
            pnlReferralEnGb.Visible = false;
            pnlReferralElGr.Visible = false;
            pnlReferralCsCz.Visible = false;
            pnlReferralDeAt.Visible = false;

        }

        #endregion

        #region Optional Interfaces

        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(), Localization.GetString("EditModule", this.LocalResourceFile), "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true, false);
                return Actions;
            }
        }

        #endregion

        string referralUserId = "-1";
        int pageId = 0;
        string button = string.Empty;
        public void SetLikeButtonCode()
        {
            //string promoPageUrl = "www.joinourspace.eu/pr/tabid/307/language/en-GB/refx/" + UserId + "/Default.aspx";
            pageId = 307;
            if (CultureInfo.CurrentCulture.Name == "el-GR")
            {
                //promoPageUrl = "www.joinourspace.eu/pr/tabid/308/language/el-GR/refx/" + UserId + "/Default.aspx";
                 pageId = 308;
                //button = "<iframe src='//www.facebook.com/plugins/like.php?href=http%3A%2F%2Fwww.joinourspace.eu%2FAdmin%2FCompetitiondeAT%2Frefx%2F"+ UserId +"%2Ftabid%2F" + pageId + "%2Flanguage%2F" + CultureInfo.CurrentCulture.Name + "%2FDefault.aspx&amp;send=false&amp;layout=standard&amp;width=450&amp;show_faces=true&amp;font&amp;colorscheme=light&amp;action=like&amp;height=80&amp;appId=220955987953254' scrolling='no' frameborder='0' style='border:none; overflow:hidden; width:450px; height:80px;' allowTransparency='true'></iframe>";

               
            
            }
            else if (CultureInfo.CurrentCulture.Name == "cs-CZ")
            {
                //promoPageUrl = "www.joinourspace.eu/pr/tabid/309/language/cs-CZ/refx/" + UserId + "/Default.aspx";
                pageId = 309;
            }
            else if (CultureInfo.CurrentCulture.Name == "de-AT")
            {
               // promoPageUrl = "www.joinourspace.eu/pr/tabid/310/language/de-AT/refx/" + UserId + "/Default.aspx";
                pageId = 310;
            }
           // return promoPageUrl;

           //string button = "<div class=\"fb-like\" data-href=\"" +promoPageUrl + "\" data-send=\"false\" data-width=\"450\" data-show-faces=\"false\"></div>";


           //button = "<iframe src='//www.facebook.com/plugins/like.php?href=http%3A%2F%2Fwww.joinourspace.eu%2FAdmin%2FCompetitiondeAT%2Ftabid%2F323%2Flanguage%2Fde-AT%2FDefault.aspx&amp;send=false&amp;layout=standard&amp;width=450&amp;show_faces=true&amp;font&amp;colorscheme=light&amp;action=like&amp;height=80&amp;appId=220955987953254' scrolling='no' frameborder='0' style='border:none; overflow:hidden; width:450px; height:80px;' allowTransparency='true'></iframe>";
            button = "<iframe src='//www.facebook.com/plugins/like.php?href=http%3A%2F%2Fwww.joinourspace.eu%2FAdmin%2FCompetitiondeAT%2Frefx%2F" + UserId + "%2Ftabid%2F" + pageId + "%2Flanguage%2F" + CultureInfo.CurrentCulture.Name + "%2FDefault.aspx&amp;send=false&amp;layout=standard&amp;width=450&amp;show_faces=true&amp;font&amp;colorscheme=light&amp;action=like&amp;height=80&amp;appId=220955987953254' scrolling='no' frameborder='0' style='border:none; overflow:hidden; width:450px; height:80px;' allowTransparency='true'></iframe>";

           ltrl_likeButton.Text = button;
        }

        public long AuthenticateUser()
        {
            if(Session["referralUserId"] != null)
            {
                referralUserId = Session["referralUserId"].ToString();
            }
            string username = UserInfo.Username;
            string[] parts = username.Split('_');
            int length = parts.Length;
            string possibleFacebookId = parts[length - 1];
            long facebookId = 0;
            bool isInt = long.TryParse(possibleFacebookId, out facebookId);


            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();


            // edw elegxeis an to facebookId tairiazei me to facebookId pou
            // pires kalwntas to graph.api ktl

            // an tairiazoun simainei oti vrikes ton xristi stin vasi opote prostheteis to facebookId ston pinaka
            // forum_user_info, vazeis kai to access_token sto sessionToken ston idio pinaka.
            //  ;// userId


            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = "";
                string sqlCheck = string.Format(@"SELECT * FROM Ourspace_Forum_User_Info WHERE UserId ={0}", UserId);
                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, sqlConn))
                {
                    cmdCheck.CommandType = CommandType.Text;
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (reader["referralUserId"] != null)
                        {
                            string currentReferralUserId = reader["referralUserId"].ToString();
                            if (currentReferralUserId == "")
                            {
                                sql = string.Format(@"UPDATE Ourspace_Forum_User_Info SET facebookId = {0}, referralUserId = '{2}' WHERE userId = {1}", facebookId, UserId, referralUserId);
                            }
                            else
                            {
                                sql = string.Format(@"UPDATE Ourspace_Forum_User_Info SET facebookId = {0} WHERE userId = {1}", facebookId, UserId);
                            }
                        }
                        

                    }
                    else
                    {
                  
                        // New user
                        sql = string.Format(@"INSERT INTO Ourspace_Forum_User_Info VALUES ({0},{1},{2},{3},{4},{5},{6},{7},'{8}',{9},{10})", UserId, PortalId, 0, 0, 0, 0, 0, "''", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"), facebookId,referralUserId);
                       // Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "pageGuide", (this.TemplateSourceDirectory + "/js/pageguide.min.js?v=1"));
                        pnlGoogleConversion.Visible = true;
                    }

                    sqlConn.Close();
                }
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    sqlConn.Close();
                }
                // reader.Close();
            }
            //}
            return facebookId;
        }
    }

}
