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
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Users;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Globalization;


namespace DotNetNuke.Modules.Ourspace_News
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_News class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_NewsModuleBase, IActionable
    {

        #region Event Handlers
        bool isFacebook = false;
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
                //lblLog.Text += "In page load<br/>";
                //if (Session["userIdWhenPageUrlStored"] != null && UserId > 0 && Session["pageVisitedBeforeLogin"] != null && Session["tabIdVisitedBeforeLogin"] != null)
                //{
                //    lblLog.Text += "userIdWhenPageUrlStored is not null: " + Session["userIdWhenPageUrlStored"].ToString();
                //    lblLog.Text += "userId > 0: " + UserId + "<br/>";
                   
                    
                //    int oldUserId = Convert.ToInt32(Session["userIdWhenPageUrlStored"].ToString());
                //    lblLog.Text += "oldUserId:" + oldUserId + "<br/>";
                //    lblLog.Text += "currentUserId:" + UserId + "<br/>";
                //    lblLog.Text += "Url: " + Session["pageVisitedBeforeLogin"].ToString() +"<br/>";
                //    lblLog.Text += "TabId:" + Session["tabIdVisitedBeforeLogin"];
                    
                //    if (oldUserId != UserId)
                //    {
                //        lblLog.Text += "oldUserId <> UserId<br/>";
                //        Session["userIdWhenPageUrlStored"] = UserId;
                //        Response.Redirect(Session["pageVisitedBeforeLogin"].ToString(), true);
                //    }
                //}
                if (Session["atc_pageVisited"] != null)
                {
                    lblTest.Text += "url: ";
                    lblTest.Text += Session["atc_pageVisited"].ToString();
                    lblTest.Text += "<br/>";
                    

                }
                if(Session["atc_pageVisitedSet"] != null)
                {
                    lblTest.Text += "is set: ";
                    lblTest.Text += Session["atc_pageVisitedSet"].ToString();
                    lblTest.Text += "<br/>";
                }
                if (Session["userRedirected"] != null)
                {
                    lblTest.Text += "has been redirected?: ";
                    lblTest.Text += Session["userRedirected"].ToString();
                    lblTest.Text += "<br/>";
                }

                lblLog.Text += "Checking..";
                
                //lblLog.Text += Session["pageVisitedBeforeLogin"].ToString();
                if (Session["atc_pageVisited"] != null && UserId > 0 && Session["userRedirected"] == null)
                {
                   
                        Session["atc_pageVisitedSet"] = null;
                        lblLog.Text += Session["atc_pageVisited"].ToString();
                        string url = Session["atc_pageVisited"].ToString();
                        Session["userRedirected"] = "done";
                        Session["userRedirected"] = "yes to " + "http://www.joinourspace.eu/" + Server.UrlDecode(url);
                        if (url.StartsWith("/def"))
                        {
                           url = url.Replace("/defaul", "defaul");
                        }

                        //lblAbsoluteUri.Text = Request.Url.AbsoluteUri;
                        if (UserId > 2)
                        {
                            string preferredLocale = UserInfo.Profile.GetPropertyValue("PreferredLocale");
                            if (url == "default.aspx")
                            {
                                if (preferredLocale == "el-GR")
                                {
                                    Response.Redirect("http://www.joinourspace.eu/%CE%91%CF%81%CF%87%CE%B9%CE%BA%CE%AE/tabid/88/language/el-GR/Default.aspx");
                                }
                                else if (preferredLocale == "de-AT")
                                {
                                    Response.Redirect("http://www.joinourspace.eu/%C3%9Cbersicht/tabid/170/language/de-AT/Default.aspx");
                                }
                                else if (preferredLocale == "cs-CZ")
                                {
                                    Response.Redirect("http://www.joinourspace.eu/Dom%C5%AF/tabid/101/language/cs-CZ/Default.aspx");
                                }

                            }


                        }
                        string finalUrl = "www.joinourspace.eu/" + url;
                        finalUrl = finalUrl.Replace("//", "/");
                    //http added later because // / replace affected the http://
                        finalUrl = "http://" + finalUrl;
                    Response.Redirect(finalUrl);
                      
                        lblTest.Text += "response redirect yet were here.";
                        //Response.Redirect("http://www.google.com/",false);
                        
                    
                }







                if (Session["newsRecentActivitiesLang"] == null ||  Session["newsRecentActivitiesLang"] != null && Session["newsRecentActivitiesLang"] != CultureInfo.CurrentCulture.Name)
                    Session["newsRecentActivitiesLang"] = CultureInfo.CurrentCulture.Name.ToString();

                if (Session["newsTopTopicsLang"] == null || Session["newsTopTopicsLang"] != null && Session["newsTopTopicsLang"] != CultureInfo.CurrentCulture.Name)
                    Session["newsTopTopicsLang"] = CultureInfo.CurrentCulture.Name.ToString();
                
                if (Request.QueryString["facebook"] != null)
                {
                    isFacebook = true;
                }
                if (!IsPostBack)
                {
                    string url = "";
                    string[] parameters = new string[3];

                    parameters = new string[0] { };
                    if (CultureInfo.CurrentCulture.Name == "el-GR")
                    {
                        hprlnk_forum.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(152);
                    }
                    else if (CultureInfo.CurrentCulture.Name == "cs-CZ")
                    {
                        hprlnk_forum.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(153);
                    }
                    else if (CultureInfo.CurrentCulture.Name == "de-AT")
                    {
                        hprlnk_forum.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(180);
                    }
                    else
                    {
                        hprlnk_forum.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(150);
                    }

                    Ourspace_Utilities.View util = new Ourspace_Utilities.View();

                    
                        //DotNetNuke.Modules.Ourspace_Utilities.View.AdmAccessToken admToken = (DotNetNuke.Modules.Ourspace_Utilities.View.AdmAccessToken)  Application["BingTranslateAccessToken"];
                       // var test = admToken.expires_in;
                    // var test2 =   util.TranslateText(Application, "en", "el", "Once upon a time");
                    }

                   

                
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
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

        protected void lstvw_TopTopics_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
Label lbl_Body = (Label)e.Item.FindControl("lbl_Body");
            Literal ltrlImage = (Literal)e.Item.FindControl("ltrlImage");

          

            string htmlContent = Server.HtmlDecode(lbl_Body.Text);
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            List<string> images = util.GetImagesInHTMLString(htmlContent);//.GetImagesInHTMLString(html);
            lbl_Body.Text = util.GetTrimmedBody(Server, 200, htmlContent);
             
            if (images.Count > 0)
            {

                ltrlImage.Text = images[0].Replace("style=", "ourspace=");
            }
            else
            {
                HtmlTableCell imageTd = (HtmlTableCell)e.Item.FindControl("imageTd");
               // HtmlTableCell textTd = (HtmlTableCell)e.Item.FindControl("textTd");
                imageTd.Visible = false;
               // textTd.ColSpan = 2;
                HtmlTableCell title_td = (HtmlTableCell)e.Item.FindControl("title_td");
                title_td.ColSpan = 2;
            }
            string resfile = LocalResourceFile;
            string forumresfile = LocalResourceFile.Replace("View","") + "../../Forum/App_LocalResources/SharedResources";
            string category = Localization.GetString("Environment",forumresfile);
           

            Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
            Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
            Label PhaseIDLabel = (Label)e.Item.FindControl("PhaseIDLabel");
            HyperLink hprlnk_post = (HyperLink)e.Item.FindControl("hprlnk_post");
            if (ThreadIDLabel != null)
            {
                
               // parameters = new string[3] { "forumid=" + ForumIDLabel.Text, "threadid=" + ThreadIDLabel.Text, "scope=posts" };
              
               // url = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters);
                //hprlnk_post.NavigateUrl = url;

                int currentPhase = Int32.Parse(PhaseIDLabel.Text);
                

                if (currentPhase == 1)
                {
                    string[] parameters2 = new string[2];
                    parameters2 = new string[2] { "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                    hprlnk_post.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(73, "", parameters2);
                }
                else if (currentPhase == 2)
                {
                    string[] parameters3 = new string[2];
                    parameters3 = new string[2] { "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                    hprlnk_post.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters3);
                }
                else if (currentPhase == 3)
                {
                    string[] parameters4 = new string[2];
                    parameters4 = new string[2] { "threadid=" + ThreadIDLabel.Text, "mode=featured" };
                    hprlnk_post.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(200, "", parameters4);

                }
                else if (currentPhase == 4)
                {
                    string[] parameters5 = new string[1];
                    parameters5 = new string[1] { "result=" + ThreadIDLabel.Text };
                    hprlnk_post.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(196, "", parameters5);

                }
                if (UserId == -1 && Response.Cookies["language"] != null)
                {
                    string cookieLang = Response.Cookies["language"].Value;
                    hprlnk_post.NavigateUrl = hprlnk_post.NavigateUrl.Replace("en-GB", cookieLang);
                }
            }






        }

        public string GetLocalizedCategory(string category)
        {
            string resfile = LocalResourceFile;
            string forumresfile = LocalResourceFile.Replace("View","") + "../../Forum/App_LocalResources/SharedResources";
            category = Localization.GetString(category.Replace(" ",""), forumresfile);

            return category;
        }

        protected void lstvw_RecentActivities_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            // HyperLink hprlnk_userLink = (HyperLink)e.Item.FindControl("hprlnk_userLink");
            //Label friendshipRequesterLabel = (Label)e.Item.FindControl("friendshipRequesterLabel");
            Label UserIDLabel = (Label)e.Item.FindControl("UserIDLabel");
            Label SubjectLabel = (Label)e.Item.FindControl("SubjectLabel");
            //Label lbl_FriendDisplayName = (Label)e.Item.FindControl("lbl_FriendDisplayName");
            Label lbl_Name = (Label)e.Item.FindControl("lbl_Name");
            //Label lbl_Location = (Label)e.Item.FindControl("lbl_Location");
           // Label lbl_LocationText = (Label)e.Item.FindControl("lbl_LocationText");
            Label BodyLabel = (Label)e.Item.FindControl("BodyLabel");
            if (SubjectLabel.Text.Length > 4 && SubjectLabel.Text.Substring(0, 4) == "Re: ")
            {
                SubjectLabel.Text = SubjectLabel.Text.Substring(4,SubjectLabel.Text.Length -4);
            }

            
           
            //SubjectLabel.Text = SubjectLabel.Text.Substring(0,4)
            UserInfo userInfo = new UserInfo();
            System.Web.UI.WebControls.Image img_profileMini = (System.Web.UI.WebControls.Image)e.Item.FindControl("img_profileMini");

            userInfo = UserController.GetUserById(PortalId, Convert.ToInt32(UserIDLabel.Text));
            if (userInfo.FirstName != "")
            {

                lbl_Name.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userInfo.Profile.FirstName) + " " + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userInfo.Profile.LastName); //.Substring(0, 1).ToUpper()
            }
            else
            {
                lbl_Name.Text = userInfo.Username;
            }



            //if (userInfo.Profile.Country == "" || userInfo.Profile.Country == "N/A" || userInfo.Profile.Country == null)
            //{
            //    lbl_LocationText.Visible = false;
            //    lbl_Location.Visible = false;
            //}
            //else
            //{
            //    lbl_Location.Text = userInfo.Profile.Country;
            //}
            
            //lbl_FriendDisplayName.Text = userInfo.FirstName + " " + userInfo.LastName;
            //hprlnk_userLink.NavigateUrl = userInfo.UserID.ToString();
            //img_profileMini.ToolTip = lbl_FriendDisplayName.Text;


            /* Link to user profile */
            HyperLink hprlnk_UserProfile = (HyperLink)e.Item.FindControl("hprlnk_UserProfile");
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            string lang = CultureInfo.CurrentCulture.ToString();
            hprlnk_UserProfile.NavigateUrl = util.GetUserProfileLink(int.Parse(UserIDLabel.Text), lang, isFacebook);
            

            /* Link to thread*/
            HyperLink hprlnk_Topic = (HyperLink)e.Item.FindControl("hprlnk_Topic");

            Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
            Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
            string topicUrl = "";
            string[] topicParameters = new string[3];

            topicParameters = new string[3] { "forumid=" + ForumIDLabel.Text, "threadid=" + ThreadIDLabel.Text, "scope=posts" };
            topicUrl = DotNetNuke.Common.Globals.NavigateURL(62, "", topicParameters);

            if (UserId == -1 &&  Response.Cookies["language"] != null)
            {
               string cookieLang = Response.Cookies["language"].Value;
             topicUrl =  topicUrl.Replace("en-GB", cookieLang);
            }

            hprlnk_Topic.NavigateUrl = topicUrl;

            img_profileMini.ImageUrl = util.GetOurSpaceUserImgUrl(Server, userInfo.UserID);
                        
            //Label CreatedDateLabel = (Label)e.Item.FindControl("CreatedDateLabel");

            //string[] dateArr = CreatedDateLabel.Text.Split(' ');
            //CreatedDateLabel.Text = dateArr[0] + " @ " + dateArr[1];
            BodyLabel.Text = Regex.Replace(HttpUtility.HtmlDecode(BodyLabel.Text), @"<(.|\n)*?>", string.Empty);
            if (BodyLabel.Text.Length > 200)
            {
                BodyLabel.Text = Regex.Replace(HttpUtility.HtmlDecode(BodyLabel.Text), @"<(.|\n)*?>", string.Empty);
                if (BodyLabel.Text.Length > 200)
                {
                    BodyLabel.Text = BodyLabel.Text.Substring(0, 199) + "..";
                }
            }
            //BodyLabel.Text = BodyLabel.Text.Replace("&ndash;", "-");
            
        }

        protected void lnkbtn_ViewRecentActivitiesLangSwitchAll_Click(object sender, EventArgs e)
        {
            Session["recentActivitiesAllLang"] = 1;
            lnkbtn_ViewRecentActivitiesLangSwitchOwn.Visible = true;
            lnkbtn_ViewRecentActivitiesLangSwitchAll.Visible = false;
            lstvw_RecentActivities.DataBind();
        }

        protected void lnkbtn_ViewRecentActivitiesLangSwitchOwn_Click(object sender, EventArgs e)
        {
            Session["recentActivitiesAllLang"] = 2;
            lnkbtn_ViewRecentActivitiesLangSwitchOwn.Visible = false;
            lnkbtn_ViewRecentActivitiesLangSwitchAll.Visible = true;

            Session["newsRecentActivitiesLang"] = CultureInfo.CurrentCulture.Name;
            lstvw_RecentActivities.DataBind();
        }

        protected void lnkbtn_ViewRecentActivitiesLangSwitchEu_Click(object sender, EventArgs e)
        {
            Session["recentActivitiesAllLang"] = 2;
            lnkbtn_ViewRecentActivitiesLangSwitchOwn.Visible = false;
            lnkbtn_ViewRecentActivitiesLangSwitchAll.Visible = true;

            Session["newsRecentActivitiesLang"] = "en-EU";
            lstvw_RecentActivities.DataBind();
        }


    }

}
