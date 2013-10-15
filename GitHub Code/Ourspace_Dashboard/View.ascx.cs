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
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common;
using System.Drawing;
using System.IO;
using DotNetNuke.Common.Lists;
using System.Web.Profile;
using System.Web.UI;
using System.Globalization;


namespace DotNetNuke.Modules.Ourspace_Dashboard
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_Dashboard class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_DashboardModuleBase, IActionable
    {
        int SQUARE_THUMB_SIDE = 50;
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


                if (Request.QueryString["facebook"] != null)
                {
                    isFacebook = true;
                }

                ScriptManager objScriptManager = ScriptManager.GetCurrent(this.Page);
                ScriptReference objScriptReference;

                objScriptReference = new ScriptReference(@"~/DesktopModules/Ourspace_Dashboard/js/textboxLimiter.js");
                objScriptManager.Scripts.Add(objScriptReference);

                objScriptReference = new ScriptReference(@"~/DesktopModules/Ourspace_Dashboard/js/jquery-ui-1.8.16.custom.min.js");
                objScriptManager.Scripts.Add(objScriptReference);
                // objScriptReference = new ScriptReference(@"~/DesktopModules/Ourspace_Dashboard/js/dashboard.js");
                //objScriptManager.Scripts.Add(objScriptReference);

                Page.ClientScript.RegisterClientScriptInclude("dashboard.js", this.TemplateSourceDirectory + "/js/dashboard.js");







                Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                if (!IsPostBack)
                {
                    hdnfldCurrentUser.Value = UserId.ToString();
                    if (Request.QueryString["user"] != null)
                    {
                        hdnfldCurrentUser.Value = Request.QueryString["user"];
                    }

                    LoadNotifications();
                    int points = util.GetUserPoints(int.Parse(hdnfldCurrentUser.Value));


                    //lblSideLevel.Text = util.GetLevel(points).ToString();

                    int userLevel = util.GetLevel(Convert.ToInt32(points));
                   lblSideLevel.Text = userLevel.ToString() + " (<span><i>" + util.GetLevelName(userLevel, LocalResourceFile.Replace("Ourspace_Dashboard", "Ourspace_Utilities")) + "</i></span>)";
        
                    lblSidePoints.Text = points.ToString();
                    lblPoints.Text = points.ToString();
                    lblSidePoints.Text = lblPoints.Text;
                    int pointsToNextLevel = util.GetPointsToNextLevel(points);
                    if (pointsToNextLevel == -1)
                        lblPointsToNextLevel.Text = "-";
                    else
                        lblPointsToNextLevel.Text = pointsToNextLevel.ToString();


                    lblReferrals.Text = util.GetUserReferralCount(UserId) + "";
                   
                    
                }
                if (Request.QueryString["user"] != null && int.Parse(Request.QueryString["user"]) != UserId)
                {
                    lnkBtnEditProfile.Visible = false;
                }

                imgProfilePic.ImageUrl = util.GetHighResUserImgUrl(Server, int.Parse(hdnfldCurrentUser.Value));

                UserInfo userInfo = UserController.GetUserById(0, int.Parse(hdnfldCurrentUser.Value));
                lblUserFullName.Text = userInfo.FirstName + " " + userInfo.LastName;

                lblMemberSinceDate.Text = String.Format("{0:MMMM d, yyyy}", userInfo.Membership.CreatedDate);
                //UserInfo userInfo = UserInfo;
                // Loading user info
                lbl_FirstNameValue.Text = userInfo.Profile.FirstName;
                lbl_LastNameValue.Text = userInfo.Profile.LastName;
                string dob = userInfo.Profile.GetPropertyValue("DOB");
                if (userInfo.Profile.ProfileProperties.GetByName("DOB") != null)
                {

                    // lbl_AgeValue.Text  = String.Format("{0:MMMM d, yyyy}", DateTime.Parse(userInfo.Profile.ProfileProperties.GetByName("DOB").PropertyValue));
                    txt_DOB.Text = DateTime.Parse(userInfo.Profile.ProfileProperties.GetByName("DOB").PropertyValue).ToString("dd-MM-yyyy");
                    hdnfld_DOB.Value = DateTime.Parse(userInfo.Profile.ProfileProperties.GetByName("DOB").PropertyValue).ToString("dd-MM-yyyy");

                }
                lbl_CityValue.Text = userInfo.Profile.GetPropertyValue("City"); ;
                lbl_CountryValue.Text = userInfo.Profile.Country;

                if (Session["FacebookUserId"] != null && Session["FacebookUserId"].ToString() == "0" /*&& Request.QueryString["user"] != null*/)
                {
                    lnkbtn_ChangePhoto.Visible = true;
                    lnkbtn_ChangePassword.Visible = true;
                }

                // txt_DOB.Visible = true;

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

        protected void lstvw_Replies_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            Label PostIDLabel = (Label)e.Item.FindControl("PostIDLabel");

            Label SubjectLabel = (Label)e.Item.FindControl("SubjectLabel");
            Label lblDate = (Label)e.Item.FindControl("lblDate");
            //DateTime postDate = DateTime.Parse( FormatDate(postDate));
            // lblDate.Text = util.GetDaysAgo(postDate);
            SubjectLabel.Text = util.GetThreadName(int.Parse(PostIDLabel.Text));
        }

        protected string FormatDate(object date)
        {
            //{
            //    if (this.Session["Locale"] != null)
            //    {
            //        if (this.Session["Locale"].ToString().Equals("USA"))
            //        {
            //            return Convert.ToDateTime(date).ToString("MM/dd/yyyy");
            //        }
            //        else
            //        {
            //            return Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            //        }
            //    }
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            string timeAgo = util.GetTimeAgo(Convert.ToDateTime(date));
            if (timeAgo.Length > 1)
            {
                return  Localization.GetString(timeAgo.Split('#')[1], LocalResourceFile).Replace("{0}",timeAgo.Split('#')[0]);
            }
            else
            {
               return timeAgo;
            }
        }

        protected void lstvw_Solutions_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblBody = (Label)e.Item.FindControl("lblBody");
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            lblBody.Text = util.GetTrimmedBody(Server, 60, lblBody.Text);
        }

        protected void lnkBtnMySolutions_Click(object sender, EventArgs e)
        {
            resetTabs();
            lstvw_Solutions.Visible = true;
            lnkBtnMySolutions.CssClass = "tab-active";
        }

        protected void resetTabs()
        {
            lnkbtnMyDebates.CssClass = "tab-inactive";
            lnkbtnMyReplies.CssClass = "tab-inactive";
            lnkBtnMySolutions.CssClass = "tab-inactive";

            lstvw_DebateProposals.Visible = false;
            lstvw_Replies.Visible = false;
            lstvw_Solutions.Visible = false;
        }

        protected void lnkbtnMyDebates_Click(object sender, EventArgs e)
        {
            resetTabs();
            lstvw_DebateProposals.Visible = true;
            lnkbtnMyDebates.CssClass = "tab-active";
        }

        protected void lnkbtnMyReplies_Click(object sender, EventArgs e)
        {
            resetTabs();
            lstvw_Replies.Visible = true;
            lnkbtnMyReplies.CssClass = "tab-active";
        }


        class Notification
        {
            public String Creator { get; set; }
            public int CreatorId { get; set; }
            public String ActionType { get; set; }
            public String PostSubject { get; set; }
            public String PostUrl { get; set; }

        }

        private const int NOTIFICATIONS_MAX_SIZE = 2;

        private string sql =
@"SELECT [OurSpace].[dbo].[Users].[DisplayName] Sender,

	[OurSpace].[dbo].[Ourspace_Notifications].[Type],
	[OurSpace].[dbo].[Forum_Posts].[Subject],
	[OurSpace].[dbo].[Forum_Posts].[PostID],
	[OurSpace].[dbo].[Forum_Threads].[ForumID],
	[OurSpace].[dbo].[Ourspace_Notifications].[Date],
[OurSpace].[dbo].[Users].[UserID]
  FROM [OurSpace].[dbo].[Ourspace_Notifications]
  INNER JOIN [OurSpace].[dbo].[Users] ON [OurSpace].[dbo].[Users].[UserID] = [OurSpace].[dbo].[Ourspace_Notifications].[Creator]
  INNER JOIN [OurSpace].[dbo].[Forum_Posts] ON [OurSpace].[dbo].[Forum_Posts].[PostID] = [OurSpace].[dbo].[Ourspace_Notifications].[PostId]
  INNER JOIN [OurSpace].[dbo].[Forum_Threads] ON [OurSpace].[dbo].[Forum_Threads].[ThreadID] = [OurSpace].[dbo].[Forum_Posts].[ThreadID]
  WHERE [Recipient] = @Recipient
  ORDER BY [OurSpace].[dbo].[Ourspace_Notifications].[Date] DESC";

        protected void NotificationsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (NotificationsRepeater.Items.Count < 1)
                {
                    if (e.Item.ItemType == ListItemType.Footer)
                    {
                        Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                        lblFooter.Visible = true;
                    }
                }
                HyperLink hprlnkUserProfile = (HyperLink)e.Item.FindControl("hprlnkUserProfile");

                string[] parameters3 = new string[1];

                Label lblUserId = (Label)e.Item.FindControl("lblUserId");

                Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                string lang = CultureInfo.CurrentCulture.ToString();
                hprlnkUserProfile.NavigateUrl = util.GetUserProfileLink(int.Parse(lblUserId.Text), lang, isFacebook);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }



        }

        protected void LoadNotifications()
        {
            try
            {
                UserInfo currentUserInfo = UserController.GetUserById(0, int.Parse(hdnfldCurrentUser.Value));
                if (currentUserInfo != null)
                {
                    String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

                    using (var sqlConn = new SqlConnection(connectionString))
                    {
                        sqlConn.Open();

                        using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;
                            SqlParameter recipientParam = new SqlParameter("@Recipient", SqlDbType.Int);
                            recipientParam.Value = currentUserInfo.UserID;
                            cmd.Parameters.Add(recipientParam);
                            cmd.Prepare();
                            SqlDataReader reader = cmd.ExecuteReader();
                            var notifications = new List<Notification>();
                            int i = 0;
                            while (reader.Read())
                            {
                                var not = new Notification();
                                not.Creator = reader.GetString(0);
                                not.CreatorId = reader.GetInt32(6);
                                switch (reader.GetString(1))
                                {
                                    case "Thumbs up":
                                        not.ActionType = Localization.GetString("thumbup.Text", LocalResourceFile);
                                        break;
                                    case "Thumbs down":
                                        not.ActionType = Localization.GetString("thumbdown.Text", LocalResourceFile);
                                        break;
                                    case "Reply":
                                        not.ActionType = Localization.GetString("replied2.Text", LocalResourceFile);
                                        break;
                                }
                                not.PostUrl = GetForumPostUrl(reader.GetInt32(4), reader.GetInt32(3));
                                
                                not.PostSubject = reader.GetString(2);
                                notifications.Add(not);
                                i++;
                                if (i >= NOTIFICATIONS_MAX_SIZE)
                                {
                                    break;
                                }
                            }
                            reader.Close();

                            NotificationsRepeater.DataSource = notifications;
                            NotificationsRepeater.DataBind();

                            if (notifications.Count > 0)
                            {
                                //SetTitle(notifications.Count);
                            }
                        }

                        sqlConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }
        public String GetForumPostUrl(int forumId, int postId)
        {
            String url = "";
            if (Request.QueryString["facebook"] != null)
            {
                String[] urlParams = {"forumid=" + forumId.ToString(), 
                                   "postid=" + postId.ToString(), 
                                   "scope=posts","facebook=1"};
                url = Globals.NavigateURL(259, "", urlParams);
                url = url + "#" + postId.ToString();
            }
            else
            {
                String[] urlParams = {"forumid=" + forumId.ToString(), 
                                   "postid=" + postId.ToString(), 
                                   "scope=posts"};
                url = Globals.NavigateURL(62, "", urlParams);
                url = url + "#" + postId.ToString();
            }


            return url;
        }

        protected void lstvwRecentActitity_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label TypeLabel = (Label)e.Item.FindControl("TypeLabel");
            Label PostIdLabel = (Label)e.Item.FindControl("PostIdLabel");
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            if (TypeLabel.Text == "Reply")
            {
                TypeLabel.Text = Localization.GetString("Replied.Text", LocalResourceFile);
               // TypeLabel.Text = "replying to";
                Label ThreadNameLabel = (Label)e.Item.FindControl("ThreadNameLabel");
                ThreadNameLabel.Text = util.GetThreadName(int.Parse(PostIdLabel.Text));
                ThreadNameLabel.Visible = true;
            }
            else if (TypeLabel.Text == "Thumbs up" || TypeLabel.Text == "Thumbs down")
            {
                if(TypeLabel.Text == "Thumbs up")
                    TypeLabel.Text = Localization.GetString("Thumbsup.Text", LocalResourceFile);
                else
                    TypeLabel.Text = Localization.GetString("Thumbsdown.Text", LocalResourceFile);
               // TypeLabel.Text = "rating";
                Label ThreadNameLabel = (Label)e.Item.FindControl("ThreadNameLabel");
                ThreadNameLabel.Text = util.GetThreadName(int.Parse(PostIdLabel.Text));
                ThreadNameLabel.Visible = true;
            }

            //TypeLabel.Text = util.GetThreadName(int.Parse(PostIdLabel.Text));
        }

        protected void lnkbtnAllMyReplies_Click(object sender, EventArgs e)
        {
            ToggleAllDashboard(false);
            lstvw_Replies.Visible = true;
            lnkbtnMyReplies.Visible = true;
            lnkbtnMyReplies.CssClass = "tab-active";
            lstvw_Replies.DataSourceID = sqldtsrc_RepliesAll.ID;

            LinkButton lnkbtnFooterAllReplies = (LinkButton)lstvw_Replies.FindControl("lnkbtnFooterAllReplies");
            if (lnkbtnFooterAllReplies != null)
            {
                lnkbtnFooterAllReplies.Visible = false;
            }
        }
        protected void lnkbtnFooterAllReplies_Click(object sender, EventArgs e)
        {
            lstvw_Replies.DataSourceID = sqldtsrc_RepliesAll.ID;
            LinkButton lnkbtnFooterAllReplies = (LinkButton)sender;
            lnkbtnFooterAllReplies.Visible = false;
        }

        protected void lnkbtnFooterAllSolutions_Click(object sender, EventArgs e)
        {

            lstvw_Solutions.DataSourceID = sqldtsrc_SolutionsAll.ID;
            LinkButton lnkbtnFooterAllSolutions = (LinkButton)sender;
            lnkbtnFooterAllSolutions.Visible = false;
        }

        protected void lnkbtnFooterAllDebateProposals_Click(object sender, EventArgs e)
        {

            lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsAll.ID;
            LinkButton lnkbtnFooterAllDebateProposals = (LinkButton)sender;
            lnkbtnFooterAllDebateProposals.Visible = false;

        }

        protected void lnkbtnAllMyDebatesProposals_Click(object sender, EventArgs e)
        {
            ToggleAllDashboard(false);
            lstvw_DebateProposals.Visible = true;
            lnkbtnMyDebates.Visible = true;
            lnkbtnMyDebates.CssClass = "tab-active";
            lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsAll.ID;

            // Next line throws error
            LinkButton lnkbtnFooterAllDebateProposals = (LinkButton)lstvw_DebateProposals.FindControl("lnkbtnFooterAllDebateProposals");
            if (lnkbtnFooterAllDebateProposals != null)
            {
                lnkbtnFooterAllDebateProposals.Visible = false;
            }
        }

        protected void lnkbtnAllMySolutions_Click(object sender, EventArgs e)
        {
            ToggleAllDashboard(false);
            lstvw_Solutions.Visible = true;
            lnkBtnMySolutions.Visible = true;
            lnkBtnMySolutions.CssClass = "tab-active";
            lstvw_Solutions.DataSourceID = sqldtsrc_SolutionsAll.ID;
            LinkButton lnkbtnFooterAllSolutions = (LinkButton)lstvw_Solutions.FindControl("lnkbtnFooterAllSolutions");
            if (lnkbtnFooterAllSolutions != null)
            {
                lnkbtnFooterAllSolutions.Visible = false;
            }
        }


        protected void ToggleAllDashboard(bool show)
        {
            lstvw_DebateProposals.Visible = show;
            lstvw_Solutions.Visible = show;
            lstvw_Replies.Visible = show;

            lnkbtnMyDebates.Visible = show;
            lnkBtnMySolutions.Visible = show;
            lnkbtnMyReplies.Visible = show;

            pnlExtraDashboardInfo.Visible = show;

            lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposals.ID;
            lstvw_Replies.DataSourceID = sqldtsrc_Replies.ID;
            lstvw_Solutions.DataSourceID = sqldtsrc_Solutions.ID;

        }
        protected void lnkbtnGoToDashboard_Click(object sender, EventArgs e)
        {
            ToggleAllDashboard(true);
            lstvw_DebateProposals.Visible = false;
            lstvw_Solutions.Visible = false;
            lnkbtnMyReplies.CssClass = "tab-active";
            lnkbtnMyDebates.CssClass = "tab-inactive";
            lnkBtnMySolutions.CssClass = "tab-inactive";

            lstvw_Replies.DataSourceID = sqldtsrc_Replies.ID;
            lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposals.ID;
            lstvw_Solutions.DataSourceID = sqldtsrc_Solutions.ID;
            // Next line throws error
            LinkButton lnkbtnFooterAllReplies = (LinkButton)lstvw_Replies.FindControl("lnkbtnFooterAllReplies");
            if (lnkbtnFooterAllReplies != null)
            {
                lnkbtnFooterAllReplies.Visible = true;
            }
            LinkButton lnkbtnFooterAllDebateProposals = (LinkButton)lstvw_DebateProposals.FindControl("lnkbtnFooterAllDebateProposals");
            if (lnkbtnFooterAllDebateProposals != null)
            {
                lnkbtnFooterAllDebateProposals.Visible = false;
            }

            LinkButton lnkbtnFooterAllSolutions = (LinkButton)lstvw_Solutions.FindControl("lnkbtnFooterAllSolutions");
            if (lnkbtnFooterAllSolutions != null)
            {
                lnkbtnFooterAllSolutions.Visible = true;
            }



        }


        protected void lnkbtn_uploadPhoto_Click(object sender, EventArgs e)
        {
            //flupld_ProfileImage.PostedFile.SaveAs(Server.MapPath(".\\") + flupld_ProfileImage.PostedFile.FileName);
            if (flupld_ProfileImage.HasFile)
            {
                lblFileNotSelected.Visible = false;
                string extension = Path.GetExtension(flupld_ProfileImage.FileName);

                if (extension.Contains("jpeg"))
                {
                    extension = extension.Replace(".jpeg", ".jpg");
                }

                if (extension.Contains("jpg"))
                {


                    UserInfo.Profile.SetProfileProperty("Photo", extension);
                    string strPath = Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\");
                    if (!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\"));
                        if (UserId <= 9)
                        {
                            Directory.CreateDirectory(Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\" + UserId.ToString("00")));
                            strPath = (Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\" + UserId.ToString("00")));
                        }
                        else
                        {
                            Directory.CreateDirectory(Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\" + UserId));
                            strPath = Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\" + UserId);
                        }
                        Directory.CreateDirectory(strPath + "\\" + UserId);
                        strPath += "\\" + UserId;
                    }
                    else
                    {
                        if (UserId <= 9)
                        {
                            strPath = (Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\" + UserId.ToString("00")));
                        }
                        else
                        {
                            strPath = Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\" + UserId);
                        }
                        strPath += "\\" + UserId;
                    }



                    string strPathThumbToCropSmall = strPath + "\\" + UserId + "_thumbToCropSmall" + extension;
                    //string strPathThumbToCropTiny = strPath + "\\" + UserId + "_thumbToCropTiny" + extension;
                    string strPathThumb = strPath + "\\" + UserId + "_180" + extension;
                    //string strPathThumbFinal = strPath + "\\" + UserId + "thumbfinal" + extension;
                    string strPathCropped = strPath + "\\" + UserId + "_50" + extension;
                    string strPathCropped32 = strPath + "\\" + UserId + "_32" + extension;
                    strPath += "\\" + UserId + extension;



                    flupld_ProfileImage.PostedFile.SaveAs(strPath);

                    // First we resize the image, making the smaller side 50px and the other side
                    // based on the picture ratio.

                    System.Drawing.Image img = System.Drawing.Image.FromFile(strPath);
                    int resizeHeight = 0;
                    int resizeWidth = 0;

                    double ratio = Convert.ToDouble(img.Height) / Convert.ToDouble(img.Width);
                    if (img.Height < img.Width)
                    {
                        resizeHeight = SQUARE_THUMB_SIDE;
                        resizeWidth = Convert.ToInt32(Convert.ToDouble(SQUARE_THUMB_SIDE) / ratio);
                    }
                    else
                    {
                        resizeWidth = SQUARE_THUMB_SIDE;
                        resizeHeight = Convert.ToInt32(ratio * Convert.ToDouble(SQUARE_THUMB_SIDE));
                    }
                    img = null;

                    ResizePicture(strPath, strPathThumbToCropSmall, new Size(resizeWidth, resizeHeight));

                    ResizePicture(strPath, strPathThumb, new Size(180, Convert.ToInt32(180 * ratio)));
                    //int thumbHeight = Convert.ToInt32(181 * ratio);
                    //cropImage(strPathThumb, new Rectangle(0, 0, 180, thumbHeight-1)).Save(strPathThumbFinal);

                    cropImage(strPathThumbToCropSmall, new Rectangle(0, 0, SQUARE_THUMB_SIDE, SQUARE_THUMB_SIDE)).Save(strPathCropped);
                    ResizePicture(strPathCropped, strPathCropped32, new Size(32, 32));

                    //squareImg.Save(path.Replace(".jpg","square.jpg"))
                    Response.Redirect(Request.Url.ToString());
                }
                else
                {
                    lblUploadPhotoInstructions.CssClass = "red";
                    lnkbtn_ChangePassword.Visible = false;
                }

            }
            else
            {
                lnkbtn_ChangePhoto.Visible = false;
                lblFileNotSelected.Visible = true;
            }
        }

        public void ResizePicture(string originalpath, string newpath, Size newsize)
        {
            using (Bitmap newbmp = new Bitmap(newsize.Width, newsize.Height), oldbmp = Bitmap.FromFile(originalpath) as Bitmap)
            {
                using (Graphics newgraphics = Graphics.FromImage(newbmp))
                {
                    newgraphics.Clear(Color.FromArgb(-1));
                    if ((float)oldbmp.Width / (float)newsize.Width == (float)oldbmp.Height / (float)newsize.Height) //Target size has a 1:1 aspect ratio
                    {
                        newgraphics.DrawImage(oldbmp, 0, 0, newsize.Width, newsize.Height);
                    }

                    else if ((float)oldbmp.Width / (float)newsize.Width > (float)oldbmp.Height / (float)newsize.Height) //There will be white space on the top and bottom
                    {
                        newgraphics.DrawImage(oldbmp, 0f, (float)newbmp.Height / 2f - (oldbmp.Height * ((float)newbmp.Width / (float)oldbmp.Width)) / 2f, (float)newbmp.Width, oldbmp.Height * ((float)newbmp.Width / (float)oldbmp.Width));
                    }

                    else if ((float)oldbmp.Width / (float)newsize.Width < (float)oldbmp.Height / (float)newsize.Height) //There will be white space on the sides
                    {
                        newgraphics.DrawImage(oldbmp, (float)newbmp.Width / 2f - (oldbmp.Width * ((float)newbmp.Height / (float)oldbmp.Height)) / 2f, 0f, oldbmp.Width * ((float)newbmp.Height / (float)oldbmp.Height), (float)newbmp.Height);
                    }

                    newgraphics.Save();
                    newbmp.Save(newpath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }

        private static System.Drawing.Image cropImage(string path, Rectangle cropArea)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);

            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea,
            bmpImage.PixelFormat);
            //System.Drawing.Image squareImg = (System.Drawing.Image)(bmpCrop);
            return (System.Drawing.Image)(bmpCrop);
        }

        protected void lnkbtn_ChangePhoto_Click(object sender, EventArgs e)
        {


            lnkbtn_editProfileSettings.Visible = false;
            lnkbtn_ChangePassword.Visible = false;
            lnkbtn_cancelPhotoUpload.Visible = true;
            lnkbtn_EditProfile.Visible = false;
            flupld_ProfileImage.Visible = true;
            lblUploadPhotoInstructions.Visible = true;
            lnkbtn_ChangePhoto.Visible = false;
            lnkbtn_uploadPhoto.Visible = true;
            lnkbtn_UpdateProfile.Visible = false;
        }

        protected void lnkbtn_ChangePassword_Click(object sender, EventArgs e)
        {
            pnl_feedback.Visible = false;
            lbl_IncorrectOldPassword.Visible = false;
            lbl_PasswordChanged.Visible = false;
            lbl_PasswordTooShort.Visible = false;

            lnkbtn_ConfirmNewPassword.Visible = true;
            lnkbtn_KeepPassword.Visible = true;
            lnkbtn_ChangePassword.Visible = false;
            lbl_OldPassword.Visible = true;
            lbl_NewPassword.Visible = true;
            txt_OldPassword.Visible = true;
            txt_NewPassword.Visible = true;
        }

        protected void lnkbtn_KeepPassword_Click(object sender, EventArgs e)
        {
            pnl_feedback.Visible = false;
            lbl_IncorrectOldPassword.Visible = false;
            lbl_PasswordChanged.Visible = false;
            lbl_PasswordTooShort.Visible = false;

            lnkbtn_ConfirmNewPassword.Visible = false;
            lnkbtn_KeepPassword.Visible = false;
            lnkbtn_ChangePassword.Visible = true;
            lbl_OldPassword.Visible = false;
            lbl_NewPassword.Visible = false;
            txt_OldPassword.Visible = false;
            txt_NewPassword.Visible = false;
        }



        protected void lnkbtn_ChangePasswordConfirm_Click(object sender, EventArgs e)
        {
            pnl_feedback.Visible = false;
            lbl_IncorrectOldPassword.Visible = false;
            lbl_PasswordChanged.Visible = false;
            lbl_PasswordTooShort.Visible = false;
            pnl_feedback.Visible = true;
            if (txt_NewPassword.Text.Trim().Length < 8)
            {
                lnkbtn_ChangePassword.Visible = false;
                lbl_PasswordChanged.Visible = false;

                lbl_PasswordTooShort.Visible = true;
            }
            else
            {

                if (DotNetNuke.Entities.Users.UserController.ChangePassword(UserInfo, txt_OldPassword.Text, txt_NewPassword.Text))
                {
                    pnl_feedback.Visible = true;
                    lbl_PasswordChanged.Visible = true;

                    lnkbtn_KeepPassword.Visible = false;
                    lnkbtn_ConfirmNewPassword.Visible = false;
                    lnkbtn_ChangePassword.Visible = true;

                    lbl_OldPassword.Visible = false;
                    lbl_NewPassword.Visible = false;
                    txt_OldPassword.Visible = false;
                    txt_NewPassword.Visible = false;

                    txt_NewPassword.Text = "";
                    txt_OldPassword.Text = "";

                }
                else
                {
                    lnkbtn_ChangePassword.Visible = false;
                    lbl_IncorrectOldPassword.Visible = true;
                }
            }
        }



        protected void lnkbtn_EditProfile_Click(object sender, EventArgs e)
        {
            // string navurl = DotNetNuke.Common.Globals.NavigateURL(tabid, "", query);
            //HttpContext.Current.Response.Redirect(navurl);
            lnkbtn_UpdateProfile.Visible = true;
            lnkbtn_EditProfile.Visible = false;
            lnkbtn_editProfileSettings.Visible = false;

            lnkbtn_ChangePassword.Visible = false;

            Ourspace_Utilities.View util = new Ourspace_Utilities.View();


            if (!util.IsFacebookUser(UserId))
            {
                txt_FirstName.Visible = true;
                txt_LastName.Visible = true;

                txt_FirstName.Text = lbl_FirstNameValue.Text;
                txt_LastName.Text = lbl_LastNameValue.Text;

                lbl_FirstNameValue.Visible = false;
                lbl_LastNameValue.Visible = false;

                lnkbtn_ChangePhoto.Visible = true;
            }


            //pnlEditDate.Visible = true;

            txt_City.Visible = true;
            txt_City.Text = lbl_CityValue.Text;
            lbl_CityValue.Visible = false;
            ddlCountries.Visible = true;
            lbl_CountryValue.Visible = false;


            txt_DOB.Visible = true;

            // txt_DOB.Text = lbl_AgeValue.Text;

            ListController lc = new ListController();

            ListEntryInfoCollection leic = lc.GetListEntryInfoCollection("Country", "", "");
            ddlCountries.DataTextField = "Text";
            ddlCountries.DataValueField = "Value";
            ddlCountries.DataSource = leic;
            ddlCountries.DataBind();

            int selectedIndex = 0;
            int i = 0;
            foreach (ListItem item in ddlCountries.Items)
            {

                if (item.Text == lbl_CountryValue.Text)
                {
                    selectedIndex = i;
                }
                i++;
            }
            ddlCountries.SelectedIndex = selectedIndex;
            lnkbtn_cancel.Visible = true;
            // lnkbtn_ChangePhoto.Visible = false;
            //ddlCountries.Items.Insert(0, new ListItem("Select Country", "-1"));


        }




        protected void lnkbtn_UpdateProfile_Click(object sender, EventArgs e)
        {
            // UserInfo.Profile.InitialiseProfile(PortalSettings.PortalId);

            //DateTime newDob = DateTime.Parse(hdnfld_DOB.Value);
            //string result = newDob.ToString();
            // UserInfo.Profile.SetProfileProperty("DOB", newDob.ToString());

            UserInfo.Profile.SetProfileProperty("City", txt_City.Text);
            UserInfo.Profile.SetProfileProperty("Country", ddlCountries.SelectedItem.Text);
            //UserInfo.Profile.SetProfileProperty("Website", txt_Website.Text);
            // UserInfo.Profile.SetProfileProperty("Biography", txt_AboutMe.Text);
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            bool testing = util.IsFacebookUser(UserId);

            if (!util.IsFacebookUser(UserId))
            {
                UserInfo.Profile.SetProfileProperty("FirstName", txt_FirstName.Text);
                UserInfo.Profile.SetProfileProperty("LastName", txt_LastName.Text);
            }
            //  DotNetNuke.Security.Profile;
            DotNetNuke.Entities.Users.UserController.UpdateUser(0, UserInfo);









            // DotNetNuke.Entities.Profile.ProfileController.

            DotNetNuke.Entities.Profile.ProfileController.UpdateUserProfile(UserInfo);
            var test = UserInfo.Profile.ProfileProperties;


            lbl_FirstNameValue.Text = UserInfo.Profile.FirstName;
            lbl_LastNameValue.Text = UserInfo.Profile.LastName;
            string dob = UserInfo.Profile.GetPropertyValue("DOB");
            if (UserInfo.Profile.ProfileProperties.GetByName("DOB") != null)
            {
                // lbl_AgeValue.Text = String.Format("{0:MMMM d, yyyy}", DateTime.Parse(UserInfo.Profile.ProfileProperties.GetByName("DOB").PropertyValue));
            }
            lbl_CityValue.Text = UserInfo.Profile.GetPropertyValue("City");
            lbl_CountryValue.Text = UserInfo.Profile.Country;

            lbl_CityValue.Visible = true;
            txt_City.Visible = false;


            lbl_CountryValue.Visible = true;
            ddlCountries.Visible = false;

            lbl_FirstNameValue.Visible = true;
            txt_FirstName.Visible = false;

            lbl_LastNameValue.Visible = true;
            txt_LastName.Visible = false;

            // lbl_AgeValue.Visible = true;


            //pnlEditDate.Visible = false;

            lnkbtn_cancel.Visible = false;
            lnkbtn_UpdateProfile.Visible = false;
            lnkbtn_EditProfile.Visible = true;



            Response.Redirect(Request.Url.ToString());
        }

        protected void lnkbtn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }

        protected void lnkbtn_cancelPhotoUpload_Click(object sender, EventArgs e)
        {
            lnkbtn_uploadPhoto.Visible = false;
            lnkbtn_cancelPhotoUpload.Visible = false;
            flupld_ProfileImage.Visible = false;
            lblUploadPhotoInstructions.Visible = false;
            lblFileNotSelected.Visible = false;


            lnkbtn_EditProfile.Visible = true;
            lnkbtn_ChangePhoto.Visible = true;
            lnkbtn_ChangePassword.Visible = true;
          

            //Response.Redirect(Request.Url.ToString());
        }

        protected void lnkbtn_editProfileSettings_Click(object sender, EventArgs e)
        {
            lnkbtn_ChangePhoto.Visible = false;
            lnkbtn_EditProfile.Visible = false;
            lnkbtn_editProfileSettings.Visible = false;
            ddl_profileVisibility.Visible = true;
            lbl_EditSettingsVisibility.Visible = true;
            lnkbtn_saveProfileSettings.Visible = true;
            lnkbtn_CancelEditSettings.Visible = true;

            DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();

            System.Data.IDataReader dr = dp.ExecuteSQL("SELECT * FROM Ourspace_Forum_User_Info WHERE userID =" + UserId);
            string visibility = "0";
            if (dr.Read())
            {
                visibility = dr["profileVisibility"].ToString();
            }


            int selectedIndex = 0;
            int i = 0;
            foreach (ListItem item in ddl_profileVisibility.Items)
            {

                if (item.Value == visibility)
                {
                    selectedIndex = i;
                }
                i++;
            }
            ddl_profileVisibility.SelectedIndex = selectedIndex;
        }

        protected void lnkbtn_saveProfileSettings_Click(object sender, EventArgs e)
        {
            int visibility = Convert.ToInt32(ddl_profileVisibility.SelectedValue);

            // Check if user has an entry in the user settings table
            DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();
            System.Data.IDataReader dr = dp.ExecuteSQL("SELECT * FROM Ourspace_Forum_User_Info WHERE userId = " + UserId + " AND portalID=" + PortalId);
            if (!dr.Read())
            {
                DotNetNuke.Data.DataProvider dp2 = DotNetNuke.Data.DataProvider.Instance();
                System.Data.IDataReader dr2 = dp.ExecuteSQL("INSERT INTO Ourspace_Forum_User_Info (userId,portalID,threadViews,profileVisibility) VALUES (" + hdnfldCurrentUser.Value + "," + PortalId + ",0,0)");

            }

            DotNetNuke.Data.DataProvider dp3 = DotNetNuke.Data.DataProvider.Instance();
            dp3.ExecuteSQL("UPDATE Ourspace_Forum_User_Info SET profileVisibility = " + visibility + " WHERE userId =" + UserId);



            Response.Redirect(Request.Url.ToString());
        }

        protected void lnkbtn_CancelEditSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());

        }

        protected void lnkBtnEditProfile_Click(object sender, EventArgs e)
        {
            pnlEditMyProfile.Visible = true;
        }

        public string GetDebateProposalUrl(int forumId, int threadId, int phaseId)
        {
String[] urlParams = {"forumid=" + forumId.ToString(), 
                                   "threadid=" + threadId.ToString(), 
                                   "scope=posts"};
                String[] urlParamsFb = {"forumid=" + forumId.ToString(), "facebook=1",
                                   "threadid=" + threadId.ToString(), 
                                   "scope=posts"};
                String[] urlParamsToUse = urlParams;
                int targetTabId = 0;
            
            // Redirect to suggest Forum
            if (phaseId == 1)
            {
                
               
                
                if (CultureInfo.CurrentCulture.Name == "el-GR")
                {
                    targetTabId = 73;
                }
                else if (CultureInfo.CurrentCulture.Name == "en-GB")
                {
                    targetTabId = 73;
                }
                else if (CultureInfo.CurrentCulture.Name == "cs-CZ")
                {
                    targetTabId = 73;
                }
                else if (CultureInfo.CurrentCulture.Name == "de-AT")
                {
                    targetTabId = 73;
                }
                if (Request.QueryString["facebook"] != null)
                {
                    urlParamsToUse = urlParamsFb;
                    targetTabId = 271;
                }
                
            }
            // Redirect to discuss Forum
            else
            {
                targetTabId = 62;
                if (Request.QueryString["facebook"] != null)
                {
                    urlParamsToUse = urlParamsFb;
                    targetTabId = 259;
                }
                
            }
           
            if (targetTabId != 0)
            {
                return DotNetNuke.Common.Globals.NavigateURL(targetTabId, "", urlParamsToUse);
            }
            return "#";
        }
    }


}
