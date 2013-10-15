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
using Microsoft.ApplicationBlocks.Data;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using DotNetNuke.Modules.Ourspace_Utilities;
using System.Globalization;
using System.Web.UI;


namespace DotNetNuke.Modules.Ourspace_DeliberationManager
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_DeliberationManager class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_DeliberationManagerModuleBase, IActionable
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
                Session["debateProposalsOwnLang"] = CultureInfo.CurrentCulture.Name;
               // Page.ClientScript.RegisterClientScriptInclude("module.js", this.TemplateSourceDirectory + "/js/module.js");

                if (Session["debateProposalsOwnLang"] == null)
                {
                    Session["debateProposalsOwnLang"] = CultureInfo.CurrentCulture.Name;
                }
                if (Request.QueryString["facebook"] != null)
                {
                    isFacebook = true;
                }
                if (!IsPostBack)
                {
                    int allProposalsTabId = 73;
                    if (isFacebook)
                    {

                        if (CultureInfo.CurrentCulture.Name == "en-GB")
                        {
                            allProposalsTabId = 271;
                        }
                        else if (CultureInfo.CurrentCulture.Name == "el-GR")
                        {
                            allProposalsTabId = 272;
                        }
                        else if (CultureInfo.CurrentCulture.Name == "cs-CZ")
                        {
                            allProposalsTabId = 273;
                        }
                        else if (CultureInfo.CurrentCulture.Name == "de-AT")
                        {
                            allProposalsTabId = 274;
                        }
                        string[] parameters = new string[5] { "forumid=" + 6, "ctl=" + "PostEdit", "action=new", "mid=415", "facebook=1" };
                        string url = DotNetNuke.Common.Globals.NavigateURL(allProposalsTabId, "", parameters);
                        hprlnk_ProposeTopic.NavigateUrl = url;
                    }
                    else
                    {
                        string[] parameters = new string[4] { "forumid=" + 6, "ctl=" + "PostEdit", "action=new", "mid=415"};
                        string url = DotNetNuke.Common.Globals.NavigateURL(allProposalsTabId, "", parameters);

                        Ourspace_Utilities.View util = new Ourspace_Utilities.View();

                        hprlnk_ProposeTopic.NavigateUrl = url.Replace("language/en-GB", "language/" + CultureInfo.CurrentCulture.Name);// 88
                        hprlnk_ProposeTopic.NavigateUrl = util.ReplaceQueryString(hprlnk_ProposeTopic.NavigateUrl, "language", CultureInfo.CurrentCulture.Name); 
                    }





                    //GetTotalProposals();
                }
                //Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "script2", ("/OurSpace/js/ui.selectmenu.js"));
                if (Request.QueryString["mode"] != null)
                {
                    pnlThankYou.Visible = true;
                   
                        Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                        int threadId =  util.GetUserLastPostId(UserId);
                        string[] parameters = new string[2] { "threadid=" + threadId, "scope=posts" };

                        string url = DotNetNuke.Common.Globals.NavigateURL(73, "", parameters);
                        if (hprlnkThankyou != null)
                        {
                            hprlnkThankyou.Visible = true;
                            hprlnkThankyou.NavigateUrl = url;
                        }
                   
                }
                if (UserInfo.IsInRole("Administrator"))
                {
                    pnl_admin.Visible = true;
                }

                if (!IsPostBack)
                {
                    GetTotalProposals(LanguageType.Own);
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

        string CONNECTION_STRING = DotNetNuke.Common.Utilities.Config.GetConnectionString();

        protected void lstvw_DebateProposals_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {

            if (e.CommandName == "RateThreadUp")
            {
                int userHasAlreadyRated = Convert.ToInt32(SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Proposals_User_Check", e.CommandArgument, UserId));
                if (userHasAlreadyRated == 0 && UserId > -1)
                {
                    SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Ourspace_Proposals_Thumbs_Update", e.CommandArgument, UserId, 1);

                }
            }
            else if (e.CommandName == "RateThreadDown")
            {
                int userHasAlreadyRated = Convert.ToInt32(SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Proposals_User_Check", e.CommandArgument, UserId));
                if (userHasAlreadyRated == 0 && UserId > -1)
                {
                    SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Ourspace_Proposals_Thumbs_Update", e.CommandArgument, UserId, 0);

                }
            }
            
            lstvw_DebateProposals.DataBind();

        }


        protected void UpdateThreadPhase(int threadId, int phaseId)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = string.Format(@"UPDATE Ourspace_Forum_Thread_Info SET phaseId = {0} WHERE threadId = {1}", phaseId, threadId);
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }
        }

        


        protected void lstvw_DebateProposals_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            try
            {
                Label lbl_ThumbsDown = (Label)e.Item.FindControl("lbl_ThumbsDown");
                Label lbl_ThumbsUp = (Label)e.Item.FindControl("lbl_ThumbsUp");
                LinkButton lnkbtn_ApproveThread = (LinkButton)e.Item.FindControl("lnkbtn_ApproveThread");
                LinkButton lnkbtn_RejectThread = (LinkButton)e.Item.FindControl("lnkbtn_RejectThread");
                
                if (lbl_ThumbsDown != null)
                {
                    if (lbl_ThumbsDown.Text == "")
                    {
                        lbl_ThumbsDown.Text = "0";
                        lbl_ThumbsUp.Text = "0";
                    }
                }
                if (!UserInfo.IsInRole("Administrator") && !UserInfo.IsInRole("Collaborator") && lnkbtn_ApproveThread != null)
                {
                    lnkbtn_ApproveThread.Visible = false;
                    lnkbtn_RejectThread.Visible = false;
                }


                //Label PostIDLabel = (Label)e.Item.FindControl("PostIDLabel");
                Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
                Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
                Label lbl_Body = (Label)e.Item.FindControl("lbl_Body");
                Label lbl_FullBody = (Label)e.Item.FindControl("lbl_FullBody");
                Label UserIDLabel = (Label)e.Item.FindControl("UserIDLabel");
                Label lblRejectReasonId = (Label)e.Item.FindControl("lblRejectReasonId");
                  Label lblRejected = (Label)e.Item.FindControl("lblRejected");
                

                Label CreatedDateLabel = (Label)e.Item.FindControl("CreatedDateLabel");
                Literal ltrlImage = (Literal)e.Item.FindControl("ltrlImage");

                string[] dateArr = CreatedDateLabel.Text.Split(' ');
                if (dateArr.Length > 1)
                {
                    CreatedDateLabel.Text = dateArr[0] + ", " + dateArr[1];
                }
                else
                {
                    CreatedDateLabel.Text = dateArr[0];
                }

                if (lblRejectReasonId.Text != "-1")
                {
                    lblRejected.Visible = true;
                }

                Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                if (lbl_Body != null)
                {
                    string htmlContent = Server.HtmlDecode(lbl_Body.Text);

                    List<string> images = util.GetImagesInHTMLString(htmlContent);//.GetImagesInHTMLString(html);
                    lbl_Body.Text = util.GetTrimmedBody(Server, 350, htmlContent);
                    lbl_FullBody.Text = htmlContent;
                    if (images.Count > 0)
                    {

                        ltrlImage.Text = images[0].Replace("style=", "ourspace=");
                    }
                    else
                    {
                        HtmlTableCell imageTd = (HtmlTableCell)e.Item.FindControl("imageTd");
                        HtmlTableCell textTd = (HtmlTableCell)e.Item.FindControl("textTd");
                        imageTd.Visible = false;
                        textTd.ColSpan = 2;
                    }
                }




                // .Replace("&amp;amp;lt;br /&amp;amp;gt;", "<br/>");
                // if (BodyLabel.Text.Length > 100)
                // {
                //     BodyLabel.Text = BodyLabel.Text.Substring(0, 99) + "..";
                // }

                HyperLink hprlnk_post = (HyperLink)e.Item.FindControl("hprlnk_post");
                HyperLink hprlnk_subject = (HyperLink)e.Item.FindControl("hprlnk_subject");




                if (ThreadIDLabel != null)
                {
                    string url = "";
                    int resultsPageId = 73;

                    if (Request.QueryString["facebook"] != null)
                    {
                        // if accessed via Facebook App Canvas
                        resultsPageId = 271;
                        string[] parameters = new string[4];
                        parameters = new string[4] { "forumid=" + ForumIDLabel.Text, "threadid=" + ThreadIDLabel.Text, "scope=posts", "facebook=1" };
                        url = DotNetNuke.Common.Globals.NavigateURL(resultsPageId, "", parameters);
                    }
                    else
                    {
                        string[] parameters = new string[3];
                        parameters = new string[3] { "forumid=" + ForumIDLabel.Text, "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                        url = DotNetNuke.Common.Globals.NavigateURL(resultsPageId, "", parameters);
                    }
                    hprlnk_post.NavigateUrl = url;
                    if (hprlnk_subject != null)
                        hprlnk_subject.NavigateUrl = url;
                }

                HyperLink hprlnk_userProfile = (HyperLink)e.Item.FindControl("hprlnk_userProfile");

                string lang = CultureInfo.CurrentCulture.ToString();
                if (hprlnk_userProfile != null)
                    hprlnk_userProfile.NavigateUrl = util.GetUserProfileLink(int.Parse(UserIDLabel.Text), lang, isFacebook);

            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }


        }

        protected void lstvw_ActiveDiscussions_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            if (e.CommandName == "RateSolutionUp")
            {
                int userHasAlreadyRated = Convert.ToInt32(SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Proposal_Solutions_User_Check", e.CommandArgument, UserId));
                if (userHasAlreadyRated == 0 && UserId > -1)
                {
                    SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Ourspace_Proposal_Solutions_Thumbs_Update", e.CommandArgument, UserId, 1);

                }
                lstvw_ActiveDiscussions.DataBind();
            }
            else if (e.CommandName == "RateSolutionDown")
            {
                int userHasAlreadyRated = Convert.ToInt32(SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Proposals_User_Check", e.CommandArgument, UserId));
                if (userHasAlreadyRated == 0 && UserId > -1)
                {
                    SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Ourspace_Proposal_Solutions_Thumbs_Update", e.CommandArgument, UserId, 0);

                }
                lstvw_ActiveDiscussions.DataBind();
            }
            else if (e.CommandName == "ManageSolutions")
            {
                TextBox ThreadIDTextBox = (TextBox)lstvw_Solutions.InsertItem.FindControl("ThreadIDTextBox");

                //ThreadIDTextBox.Text = e.CommandArgument.ToString();

                hdnfld_CurrentProposal.Value = e.CommandArgument.ToString();
                //lstvw_ActiveDiscussions.DataBind();
                //lstvw_Solutions.DataBind();
                lstvw_Solutions.Visible = true;

            }

        }

        protected void lnkbtn_ManageDiscussionSolutions_Click(object sender, EventArgs e)
        {
            lstvw_ActiveDiscussions.Visible = true;
            lstvw_DebateProposals.Visible = false;
        }

        protected void lnkbtn_ManageDeliberations_Click(object sender, EventArgs e)
        {
            lstvw_DebateProposals.Visible = true;
            lstvw_ActiveDiscussions.Visible = false;
            lstvw_Solutions.Visible = false;
        }

        protected void lstvw_Solutions_DataBound(object sender, EventArgs e)
        {
            TextBox ThumbsUpTextBox = (TextBox)lstvw_Solutions.InsertItem.FindControl("ThumbsUpTextBox");
            TextBox ThumbsDownTextBox = (TextBox)lstvw_Solutions.InsertItem.FindControl("ThumbsDownTextBox");

            ThumbsUpTextBox.Text = "0";
            ThumbsDownTextBox.Text = "0";




        }

        protected void sqldtsrc_Solutions_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            Label lbl_NoSolutions = (Label)lstvw_Solutions.FindControl("lbl_NoSolutions");
            lbl_NoSolutions.Visible = false;
            if (e.AffectedRows == 0)
            {
                lbl_NoSolutions.Visible = true;
            }
            //lbl_NoSolutions.Text = e.AffectedRows.ToString();
        }

        public enum LanguageType { Own, All }


        protected int GetTotalProposals(LanguageType type)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();
            string sql = "";
            if (type == LanguageType.Own)
            {
                sql = "SELECT COUNT(*) AS count FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID WHERE (Forum_Groups.ModuleID = 415) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = '" + Session["debateProposalsOwnLang"] + "')";
            
            }
            else if (type == LanguageType.All)
            {
                sql = "SELECT COUNT(*) AS count FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID LEFT OUTER JOIN Ourspace_Proposals_Thumbs ON Forum_Threads.ThreadID = Ourspace_Proposals_Thumbs.ThreadID WHERE (Forum_Groups.ModuleID = 415)";
            }
            int count = 0;
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    //int i = 0;
                    while (reader.Read())
                    {
                        count = reader.GetInt32(0);
                        lblCount.Text = count.ToString();
                    }
                    reader.Close();
                }
                sqlConn.Close();
            }
            return count;
        }

        protected void lstvw_DebateProposals_DataBound(object sender, EventArgs e)
        {

        }

        protected void lnkbtnViewOwnLanguage_Click(object sender, EventArgs e)
        {
            Session["deliberationManager_currentLanguageMode"] = LanguageType.Own;
            Session["debateProposalsOwnLang"] = CultureInfo.CurrentCulture.Name;

            if (lstvw_DebateProposals.DataSourceID == sqldtsrc_DebateProposalsByDate.ID)
                lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsOwnLangByDate.ID;
            else if (lstvw_DebateProposals.DataSourceID == sqldtsrc_DebateProposalsByTitle.ID)
                lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsOwnLangByTitle.ID;
            else if (lstvw_DebateProposals.DataSourceID == sqldtsrc_DebateProposalsByPopularity.ID)
                lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsOwnLangByPopularity.ID;

            lstvw_DebateProposals.DataBind();
            pnlViewingAll.Visible = false;
            pnlViewingNational.Visible = true;
            GetTotalProposals(LanguageType.Own);
        }

        protected void lnkbtnViewAllLanguages_Click(object sender, EventArgs e)
        {
            Session["deliberationManager_currentLanguageMode"] = LanguageType.All;

            if (lstvw_DebateProposals.DataSourceID == sqldtsrc_DebateProposalsOwnLangByDate.ID)
                lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsByDate.ID;
            else if (lstvw_DebateProposals.DataSourceID == sqldtsrc_DebateProposalsOwnLangByTitle.ID)
                lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsByTitle.ID;
            else if (lstvw_DebateProposals.DataSourceID == sqldtsrc_DebateProposalsOwnLangByPopularity.ID)
                lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsByPopularity.ID;

            lstvw_DebateProposals.DataBind();
            pnlViewingAll.Visible = true;
            pnlViewingNational.Visible = false;
            GetTotalProposals(LanguageType.All);
        }

        protected void ddlSortDebates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Session["deliberationManager_currentLanguageMode"] == null)
             Session["deliberationManager_currentLanguageMode"] = LanguageType.Own;
            if ((LanguageType) Session["deliberationManager_currentLanguageMode"] == LanguageType.All)
            {
                if (ddlSortDebates.SelectedValue == "Date")
                    lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsByDate.ID;
                else if (ddlSortDebates.SelectedValue == "Title")
                    lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsByTitle.ID;
                else if (ddlSortDebates.SelectedValue == "Popularity")
                    lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsByPopularity.ID;
            }
            else if ((LanguageType)Session["deliberationManager_currentLanguageMode"] == LanguageType.Own)
            {
                if (ddlSortDebates.SelectedValue == "Date")
                    lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsOwnLangByDate.ID;
                else if (ddlSortDebates.SelectedValue == "Title")
                    lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsOwnLangByTitle.ID;
                else if (ddlSortDebates.SelectedValue == "Popularity")
                    lstvw_DebateProposals.DataSourceID = sqldtsrc_DebateProposalsOwnLangByPopularity.ID;
            }
            lstvw_DebateProposals.DataBind();
            
        }

      

    }
}
