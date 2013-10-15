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
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DotNetNuke.Entities.Modules.Communications;
using System.Xml;
using System.Globalization;


namespace DotNetNuke.Modules.Ourspace_ProposedSolutions
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_ProposedSolutions class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_ProposedSolutionsModuleBase, IActionable, IModuleCommunicator
    {
        public DotNetNuke.Framework.CDefault BasePage
        {
            get { return (DotNetNuke.Framework.CDefault)this.Page; }
        }

        public event ModuleCommunicationEventHandler ModuleCommunication;
        String CONNECTION_STRING = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();
        int currentPhaseId = -1;
        int currentThreadId = -1;
        int currentForumId = -1;
        bool isFacebook = false;
        int proposalPosition = 1;

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
                Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "script1", (this.TemplateSourceDirectory + "/js/proposals.js?v=1"));

                string resource = LocalResourceFile.Replace("footer_module", "View.ascx.resx");

                this.BasePage.Title = DotNetNuke.Services.Localization.Localization.GetString("pageTitle.Text", LocalResourceFile);
                   // this.BasePage.Title = lblPageTitle.Text;
                  //  this.BasePage.Title = "Test";
                
                if (Request.QueryString["facebook"] != null)
                {
                    isFacebook = true;
                }
                if (Request.QueryString["threadId"] != null)
                {
                    int threadId = Convert.ToInt32(Request.QueryString["threadId"].ToString());
                    currentThreadId = threadId;
                    Ourspace_Utilities.View util = new Ourspace_Utilities.View();

                    currentPhaseId = util.GetPhaseId(threadId);
                    if (UserInfo.IsInRole("Collaborator"))
                    {

                        pnlAdminInstructions.Visible = true;



                        int featuredSolutions = GetFeaturedSolutionsCount(threadId);
                        lblFeaturedCount.Text = featuredSolutions.ToString();
                        if (featuredSolutions > 2 && currentPhaseId == 2)
                        {
                            lnkbtnPromoteToPhase3.Visible = true;
                        }
                        else
                        {
                            lnkbtnPromoteToPhase3.Visible = false;
                        }
                        if ((currentPhaseId == 3) && Request.QueryString["mode"] != null)
                        {
                            //pnlNotInPhase2.Visible = true;

                            pnlAdminInPhase3.Visible = true;
                        }
                        if ((currentPhaseId == 3) && Request.QueryString["mode"] == null)
                        {
                            pnlInWrongPhase.Visible = true;
                            pnlInPhase3.Visible = true;

                        }

                        else if ((currentPhaseId == 4) && Request.QueryString["mode"] != null)
                        {
                            //pnlNotInPhase2.Visible = true;
                            pnlAdminInPhase3.Visible = true;
                        }

                        else
                        {
                            pnlInPhase2.Visible = true;
                        }


                    }
                    string lang = CultureInfo.CurrentCulture.ToString();
                    if (currentPhaseId == 3)
                    {
                        if (Request.QueryString["mode"] == null)
                        {
                            pnlInWrongPhase.Visible = true;
                            pnlNotInPhase2.Visible = true;
                            pnlInPhase3.Visible = true;

                            string[] parameters1 = new string[2];
                            parameters1 = new string[2] { "threadid=" + threadId, "mode=featured" };
                            hprlnk_GoToPhase3.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(200, "", parameters1).Replace("language/en-GB","language/"+ CultureInfo.CurrentCulture.ToString());

                            if (isFacebook)
                            {
                                Dictionary<string, int> tabs = new Dictionary<string, int>();
                                tabs.Add("en-GB", 279);
                                tabs.Add("el-GR", 280);
                                tabs.Add("cs-CZ", 281);
                                tabs.Add("de-AT", 282);
                                int voteTab = tabs[lang];
                                string[] parameters = new string[3];
                                parameters = new string[3] { "threadid=" + threadId, "mode=featured", "facebook=1" };
                                hprlnk_GoToPhase3.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(voteTab, "", parameters);
                            }
                            else
                            {
                                string[] parameters = new string[2];
                                parameters = new string[2] { "threadid=" + threadId, "mode=featured" };
                                hprlnk_GoToPhase3.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(200, "", parameters);
                            }
                        
                        
                        }

                    }
                    else if (currentPhaseId == 4)
                    {

                        pnlInWrongPhase.Visible = true;
                        pnlInPhase4.Visible = true;
                        pnlAdminInPhase3.Visible = false;
                        Label2.Visible = false;
                        if (isFacebook)
                        {
                            Dictionary<string, int> tabs = new Dictionary<string, int>();
                            tabs.Add("en-GB", 275);
                            tabs.Add("el-GR", 276);
                            tabs.Add("cs-CZ", 277);
                            tabs.Add("de-AT", 278);
                            int voteTab = tabs[lang];
                            string[] parameters = new string[2];
                            parameters = new string[2] { "result=" + threadId, "facebook=1" };
                            hprlnk_GoToPhase4.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(voteTab, "", parameters);
                        }
                        else
                        {
                            string[] parameters = new string[1];
                            parameters = new string[1] { "result=" + threadId };
                            hprlnk_GoToPhase4.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(196, "", parameters);
                        }

                                               }

                }

                lstvw_AllProposals.DataSource = sqldtsrc_AllProposals;
                if (Request.QueryString["mode"] != null)
                {
                    if (Request.QueryString["mode"] == "featured")
                    {
                        lstvw_AllProposals.DataSource = sqldtsrc_AllProposalsFeatured;

                    }
                }
                if (Request.QueryString["threadId"] != null)
                {
                    hdnfld_ThreadId.Value = Convert.ToInt32(Request.QueryString["threadId"].ToString()).ToString();
                    sqldtsrc_submittedProposals.DataBind();
                }
                lstvw_AllProposals.DataBind();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void lstvw_DebateProposals_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            try
            {
                Label lblProposalPosition = (Label)e.Item.FindControl("lblProposalPosition");

                if (lblProposalPosition != null)
                {
                    int position = proposalPosition++;
                    //lblProposalPosition.Text = position.ToString();
                    lblProposalPosition.CssClass = "proposalPosition" + position;
                }
                LinkButton lnkbtn_disagree = (LinkButton)e.Item.FindControl("lnkbtn_disagree");
                LinkButton lnkbtn_agree = (LinkButton)e.Item.FindControl("lnkbtn_agree");
                if (lnkbtn_agree != null && lnkbtn_disagree != null)
                {
                    if (UserId < 0)
                    {
                        lnkbtn_disagree.CssClass += " please-log-in";
                        lnkbtn_disagree.Attributes.Add("onclick", "return false;");
                        lnkbtn_agree.CssClass += " please-log-in";
                        lnkbtn_agree.Attributes.Add("onclick", "return false;");

                    }
                    if (UserHasVotedProposal(int.Parse(lnkbtn_disagree.CommandArgument), UserId))
                    {
                        lnkbtn_disagree.CssClass += " already-voted";
                        lnkbtn_disagree.Attributes.Add("onclick", "return false;");
                        lnkbtn_agree.CssClass += " already-voted";
                        lnkbtn_agree.Attributes.Add("onclick", "return false;");
                    }
                }





                Label lbl_ThumbsDown = (Label)e.Item.FindControl("lbl_ThumbsDown");
                Label lbl_ThumbsUp = (Label)e.Item.FindControl("lbl_ThumbsUp");
                LinkButton lnkbtn_ApproveThread = (LinkButton)e.Item.FindControl("lnkbtn_ApproveThread");
                if (lbl_ThumbsDown != null)
                {
                    if (lbl_ThumbsDown.Text == "")
                    {
                        lbl_ThumbsDown.Text = "0";
                        lbl_ThumbsUp.Text = "0";
                    }
                }
                if (!UserInfo.IsInRole("Administrator") && lnkbtn_ApproveThread != null && !UserInfo.IsInRole("Collaborator"))
                {
                    lnkbtn_ApproveThread.Visible = false;
                }

                Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                //Label PostIDLabel = (Label)e.Item.FindControl("PostIDLabel");
                Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
                Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
                Label lbl_Body = (Label)e.Item.FindControl("lbl_Body");
                Label lbl_BodyWhole = (Label)e.Item.FindControl("lbl_BodyWhole");
                Label CreatedDateLabel = (Label)e.Item.FindControl("CreatedDateLabel");
                // Literal ltrlImage = (Literal)e.Item.FindControl("ltrlImage");
                Label UserIDLabel = (Label)e.Item.FindControl("UserIDLabel");
                Image userImage = (Image)e.Item.FindControl("userImage");

                string[] dateArr = CreatedDateLabel.Text.Split(' ');
                if (dateArr.Length > 1)
                {
                    CreatedDateLabel.Text = dateArr[0] + ", " + dateArr[1];
                }
                else
                {
                    CreatedDateLabel.Text = dateArr[0];
                }
                if (lbl_Body != null)
                {
                    string htmlContent = Server.HtmlDecode(lbl_Body.Text);

                    List<string> images = util.GetImagesInHTMLString(htmlContent);//.GetImagesInHTMLString(html);
                    lbl_Body.Text = util.GetTrimmedBody(Server, 350, htmlContent);
                }
                else if (lbl_BodyWhole != null)
                {
                    //lbl_BodyWhole.Text = Server.HtmlDecode(lbl_Body.Text);
                    lbl_BodyWhole.Text = util.GetTrimmedBody(Server, 5000, Server.HtmlDecode(lbl_BodyWhole.Text));
                }

                HyperLink hprlnk_userProfile = (HyperLink)e.Item.FindControl("hprlnk_userProfile");



                string lang = CultureInfo.CurrentCulture.ToString();
                hprlnk_userProfile.NavigateUrl = util.GetUserProfileLink(int.Parse(UserIDLabel.Text), lang, isFacebook);

                Panel pnlAdminControls = (Panel)e.Item.FindControl("pnlAdminControls");
                if (pnlAdminControls != null)
                {

                    if (!(UserInfo.IsInRole("Collaborator") && currentPhaseId == 2))
                    {

                        pnlAdminControls.Visible = false;

                    }
                }
               
                HyperLink hprlnk_post = (HyperLink)e.Item.FindControl("hprlnk_post");
                HyperLink hprlnk_subject = (HyperLink)e.Item.FindControl("hprlnk_subject");
                if (hprlnk_subject != null && hprlnk_post != null)
                {
                    if (ThreadIDLabel != null)
                    {
                        string url = "";
                        string[] parameters = new string[3];

                        parameters = new string[3] { "forumid=" + ForumIDLabel.Text, "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                        //url = NavigateURL(TabId, "", parameters);
                        url = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters);
                        url = url.Replace("language/en-GB", "language/"+ CultureInfo.CurrentCulture.ToString());
                        hprlnk_post.NavigateUrl = url;
                        hprlnk_subject.NavigateUrl = url;
                        //currentForumId = Int32.Parse(ForumIDLabel.Text);
                       // Session["currentForumId"] = currentForumId;
                    }
                }

                if (ForumIDLabel != null)
                {
                    currentForumId = Int32.Parse(ForumIDLabel.Text);
                    Session["currentForumId"] = currentForumId;
                }
               // Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
                

                userImage.ImageUrl = util.GetOurSpaceUserImgUrl(Server, int.Parse(UserIDLabel.Text));

                if (currentPhaseId == 3 && Request.QueryString["mode"] != null)
                {
                    Panel pnlPhase2Voting = (Panel)e.Item.FindControl("pnlPhase2Voting");
                    pnlPhase2Voting.Visible = false;
                    Panel pnlPhase3Voting = (Panel)e.Item.FindControl("pnlPhase3Voting");
                    pnlPhase3Voting.Visible = true;
                }
                else
                {
                    Panel pnlPhase2Voting = (Panel)e.Item.FindControl("pnlPhase2Voting");
                    pnlPhase2Voting.Visible = true;
                    Panel pnlPhase3Voting = (Panel)e.Item.FindControl("pnlPhase3Voting");
                    pnlPhase3Voting.Visible = false;
                }



            }
            catch (Exception ex)
            {
                string exception = ex.Message;
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

        protected void lstvw_AllProposals_ItemCommand(object sender, ListViewCommandEventArgs e)
        {


            
            if (e.CommandName == "thumbsUp")
            {
                if (UserId > 0)
                {
                    int userHasVoted = int.Parse(SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Forum_Post_Thumbs_User_Check", e.CommandArgument, UserId).ToString());
                    if (userHasVoted == 0)
                    {
                        SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Forum_Post_Thumbs_Update", e.CommandArgument, UserId, 1);
                    }
                    lstvw_AllProposals.DataBind();
                }
            }
            else if (e.CommandName == "thumbsDown")
            {
                if (UserId > 0)
                {
                    int userHasVoted = int.Parse(SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Forum_Post_Thumbs_User_Check", e.CommandArgument, UserId).ToString());
                    if (userHasVoted == 0)
                    {
                        SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Forum_Post_Thumbs_Update", e.CommandArgument, UserId, 0);
                    }
                    lstvw_AllProposals.DataBind();
                }
            }
            else if (e.CommandName == "FeatureSolution")
            {

                UpdateIsFeatured(int.Parse(e.CommandArgument.ToString()), true);
                lstvw_AllProposals.DataBind();

            }
            else if (e.CommandName == "UnfeatureSolution")
            {

                UpdateIsFeatured(int.Parse(e.CommandArgument.ToString()), false);
                lstvw_AllProposals.DataBind();

            }
            else if (e.CommandName == "thumbsUpAgree")
            {
                if (UserId > -1)
                {
                    int userHasVoted = int.Parse(SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Proposal_Solutions_User_Check", e.CommandArgument, UserId).ToString());
                    if (userHasVoted == 0)
                    {
                        SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Proposal_Solutions_Thumbs_Update", UserId, 1, e.CommandArgument);
                    }
                }
                lstvw_AllProposals.DataBind();

            }
            else if (e.CommandName == "thumbsDownDisagree")
            {
                if (UserId > -1)
                {
                    int userHasVoted = int.Parse(SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Proposal_Solutions_User_Check", e.CommandArgument, UserId).ToString());
                    if (userHasVoted == 0)
                    {
                        SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Proposal_Solutions_Thumbs_Update", UserId, 0, e.CommandArgument);
                    }
                }
                lstvw_AllProposals.DataBind();
            }

            else if (e.CommandName == "translateProposal")
            {
                Label lbl_BodyWhole = (Label)e.Item.FindControl("lbl_BodyWhole");
                 Label lblServiceDown = (Label)e.Item.FindControl("lblServiceDown");
                 LinkButton lnkbtnTranslateProposal = (LinkButton)e.Item.FindControl("lnkbtnTranslateProposal");
                 Panel pnlTranslationBtnWrap = (Panel)e.Item.FindControl("pnlTranslationBtnWrap");
                
                lnkbtnTranslateProposal.Visible = false;
                pnlTranslationBtnWrap.Visible = false;

                Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                string translatedBody = util.TranslateText(Application, "", CultureInfo.CurrentCulture.Name.Substring(0, 2), e.CommandArgument.ToString());

                
                if (!translatedBody.Contains("#NLA#"))
                {
                    lbl_BodyWhole.Text = translatedBody;
                    
                }
                else
                {
                    lbl_BodyWhole.Attributes.Add("error", translatedBody);
                    pnlTranslationBtnWrap.Visible = true;
                    lblServiceDown.Visible = true;
                }
            }

            
        }


        private bool UserHasVotedProposal(int postId, int userId)
        {

            int userHasVoted = int.Parse(SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Proposal_Solutions_User_Check", postId, UserId).ToString());
            if (userHasVoted == 0)
            {
                return false;
            }
            return true;
        }


        private void UpdateIsFeatured(int postId, bool isFeatured)
        {
            using (var sqlConn = new SqlConnection(CONNECTION_STRING))
            {
                sqlConn.Open();
                string sql = "UPDATE Ourspace_Proposal_Solutions SET IsFeatured = @IsFeatured WHERE PostId = @PostId";
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@IsFeatured", isFeatured));
                    cmd.Parameters.Add(new SqlParameter("@PostId", postId));
                    int rows = cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }
            int featuredSolutions = GetFeaturedSolutionsCount(Convert.ToInt32(Request.QueryString["threadId"].ToString()));
            lblFeaturedCount.Text = featuredSolutions.ToString();
            if (featuredSolutions > 2 && currentPhaseId == 2)
            {
                lnkbtnPromoteToPhase3.Visible = true;
            }
            else
            {
                lnkbtnPromoteToPhase3.Visible = false;
            }
        }

        public bool getFeaturedStatus(string isFeatured)
        {
            if (isFeatured != "")
            {
                if (isFeatured == "True")
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        private int GetFeaturedSolutionsCount(int threadId)
        {

            try
            {
                string sql = "SELECT COUNT(*) FROM Ourspace_Proposal_Solutions WHERE IsFeatured = 1 AND ThreadId = @threadId";

                using (var sqlConn = new SqlConnection(CONNECTION_STRING))
                {
                    sqlConn.Open();
                    int featuredCount = 0;
                    using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@threadId", threadId));
                        SqlDataReader reader = cmd.ExecuteReader();
                        int i = 0;
                        if (reader.Read())
                        {
                            featuredCount = reader.GetInt32(0);
                        }
                        reader.Close();
                    }
                    sqlConn.Close();
                    return featuredCount;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }

        }

        protected void lnkbtnPromoteToPhase3_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["threadId"] != null)
            {
                int threadId = Convert.ToInt32(Request.QueryString["threadId"].ToString());


                using (var sqlConn = new SqlConnection(CONNECTION_STRING))
                {
                    sqlConn.Open();
                    string sql = "UPDATE Ourspace_Forum_Thread_Info SET phaseId = 3 WHERE ThreadId = @ThreadId";
                    using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@ThreadId", threadId));
                        int rows = cmd.ExecuteNonQuery();
                    }
                    sqlConn.Close();

                }


                // Sending email notification
                Ourspace_Utilities.View util = new Ourspace_Utilities.View(); 
                util.SendEmailToThreadTrackersAboutMovingToPhase3(threadId, threadId, CultureInfo.CurrentCulture.ToString());

                // the IMC message data gets stored inside
                // a ModuleCommunicationEventArgs object
                ModuleCommunicationEventArgs mcArgs =
                    new ModuleCommunicationEventArgs();
                mcArgs.Sender = "Ourspace_ProposedSolutions";
                mcArgs.Target = "Ourspace_ThreadDetails";
                mcArgs.Text = "updatePhaseDisplay";
                mcArgs.Type = "Your custom type";
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.Load("path/to/xml/doc.xml");
                mcArgs.Value = "notUsed";

                // if ModuleCommunication is null,
                // the cache settings for your module
                // might need to be set to 0 (turned off)
                if (ModuleCommunication != null)
                {
                    // calling your ModuleCommunication delegate event
                    // will cause the event to be raised
                    ModuleCommunication(this, mcArgs);
                }
                lnkbtnPromoteToPhase3.Visible = false;

                string[] parameters1 = new string[3];
                parameters1 = new string[3] { "threadid=" + threadId,"mode=featured", "language=" + CultureInfo.CurrentCulture.ToString()};
                string url = DotNetNuke.Common.Globals.NavigateURL(200, "", parameters1);

                //string language = CultureInfo.CurrentCulture.ToString();
              //string  url = "http://www.joinourspace.eu/tabid/196/result/" + threadId + "/language/" + language + "/Default.aspx";
                Response.Redirect(url);
            }
        }

        protected void lnkbtnPromoteToPhase4_Click(object sender, EventArgs e)
        {

            //http://localhost/ourspace/ProposeYourTopic/tabid/73/forumid/6/ctl/PostEdit/action/new/mid/415/language/en-GB/Default.aspx
            //62
            //ctl/PostEdit/forumid/108/postid/442969/action/reply/mid/2108
            string[] parameters1 = new string[6];
            parameters1 = new string[6] { "forumid=" + Session["currentForumId"].ToString(), "postid=" + currentThreadId, "action=reply", "mid=381", "ctl=PostEdit", "toFinalPhase=1" };
            string url = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters1);
            Response.Redirect(url);
        }

        







        //private void SendEmailToThreadTrackersAboutMovingToPhase4(int threadId, int postId)
        //{
        //    Ourspace_Utilities.View util = new Ourspace_Utilities.View();
        //    string threadName = util.GetThreadName(postId);

        //    // The HTML template of the email that notifies the user that a post has been posted in a 
        //    // thread he has subscribed in
        //    string htmlTemplate = "Hi [FIRSTNAME],<br><br>The discussion '[THREAD_NAME]' has moved to the Results Phase.<br><br>You can find the discussion in its current phase here:<br><br>[URL]";
        //    string htmlTemplateDeAt = "Hi [FIRSTNAME],<br><br>The discussion '[THREAD_NAME]' has moved to the Results Phase.<br><br>You can find the discussion in its current phase here:<br><br>[URL]";

        //    String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

        //    string subject = "OurSpace - Discussion moved to Results Phase";
        //    string subjectDeAt = "OurSpace - Discussion moved to Results Phase";
        //    string url = "";
        //    using (var sqlConn = new SqlConnection(connectionString))
        //    {
        //        sqlConn.Open();
        //        string sql = "SELECT     Forum_TrackedThreads.ForumID, Forum_TrackedThreads.ThreadID, Forum_TrackedThreads.UserID, Forum_TrackedThreads.CreatedDate, Forum_TrackedThreads.ModuleID, Users.Email, Users.FirstName, UserProfile.PropertyValue as Language, UserProfile.PropertyDefinitionID FROM   Forum_TrackedThreads INNER JOIN  Users ON Forum_TrackedThreads.UserID = Users.UserID INNER JOIN  UserProfile ON Users.UserID = UserProfile.UserID WHERE     (UserProfile.PropertyDefinitionID = 40) AND (Forum_TrackedThreads.ThreadID = " + threadId + ")";
        //        using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
        //        {
        //            cmd.CommandType = CommandType.Text;

        //            SqlDataReader reader = cmd.ExecuteReader();
        //            int i = 0;
        //            List<string> emails = new List<string>();
        //            // Adding the current users email to list prevents him from receiving a notification email
        //            // when he posts his message
        //            //emails.Add(UserInfo.Email);
        //            while (reader.Read())
        //            {
        //                string email = reader["Email"].ToString();
        //                if (!emails.Contains(email))
        //                {
        //                    int forumId = Convert.ToInt32(reader["ForumId"]);
        //                    string name = reader["FirstName"].ToString();
        //                    string language = reader["Language"].ToString();

        //                    //Ourspace_Utilities.View util = new Ourspace_Utilities.View();
        //                    //if (url == "")
        //                    //  url = util.GetPostUrl(forumId, postId);
        //                    string lang = CultureInfo.CurrentCulture.ToString();

        //                    string emailMessage = "";
        //                    string finalSubject = "";
        //                    if (language == "en-GB")
        //                    {
        //                        url = "http://www.joinourspace.eu/tabid/196/result/" + threadId + "/language/" + language + "/Default.aspx";
        //                        emailMessage = htmlTemplate.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
        //                        finalSubject = subject;
        //                    }
        //                    else if (language == "de-AT")
        //                    {
        //                        url = "http://www.joinourspace.eu/tabid/196/result/" + threadId + "/language/" + language + "/Default.aspx";
        //                        emailMessage = htmlTemplateDeAt.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
        //                        finalSubject = subjectDeAt;
        //                    }
        //                    else
        //                    {
        //                        url = "http://www.joinourspace.eu/tabid/196/result/" + threadId + "/language/" + language + "/Default.aspx";
        //                        emailMessage = htmlTemplate.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
        //                        finalSubject = subject;
        //                    }

        //                    try
        //                    {


        //                        //emailTask.EmailQueueTaskAdd("info@ep-ourspace.eu", "OurSpace", 0, emailMessage, emailMessage, finalSubject, PortalID, forumMail.QueuePriority, objConfig.ModuleID, forumMail.EnableFriendlyToName, forumMail.DistroCall, forumMail.DistroIsSproc, forumMail.DistroParams, Date.Now(), False, "")
        //                        //Ourspace_Utilities.View util = new Ourspace_Utilities.View();
        //                        util.AddEmailToQueue("info@ourspace.eu", email, finalSubject, emailMessage);

        //                        //DotNetNuke.Services.Mail.Mail.SendEmail("info@ep-ourspace.eu", email, finalSubject, emailMessage);
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                }
        //            }
        //            reader.Close();
        //        }
        //        sqlConn.Close();
        //    }
        //}
    }
}


