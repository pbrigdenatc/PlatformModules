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
using DotNetNuke.Modules.Ourspace_Utilities;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;


namespace DotNetNuke.Modules.Ourspace_ResultsManager
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_ResultsManager class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_ResultsManagerModuleBase, IActionable
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
                if(Session["resultsManagerOwnLang"] == null)
                {
                    Session["resultsManagerOwnLang"] = CultureInfo.CurrentCulture.Name;
                }

                DataPager DataPager1 = (DataPager)lstvw_DebateResults.FindControl("DataPager1");
                if (Request.QueryString["facebook"] != null)
                {
                    isFacebook = true;
                }
                if (!IsPostBack)
                    GetTotalResults( LanguageType.Own);

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
                Label lbl_ThumbsDown = (Label)e.Item.FindControl("lbl_ThumbsDown");
                Label lbl_ThumbsUp = (Label)e.Item.FindControl("lbl_ThumbsUp");
                Label UserIDLabel = (Label)e.Item.FindControl("UserIDLabel");
                LinkButton lnkbtn_ApproveThread = (LinkButton)e.Item.FindControl("lnkbtn_ApproveThread");
                if (lbl_ThumbsDown != null)
                {
                    if (lbl_ThumbsDown.Text == "")
                    {
                        lbl_ThumbsDown.Text = "0";
                        lbl_ThumbsUp.Text = "0";
                    }
                }
                if (!UserInfo.IsInRole("Administrator") && lnkbtn_ApproveThread != null)
                {
                    lnkbtn_ApproveThread.Visible = false;
                }


                //Label PostIDLabel = (Label)e.Item.FindControl("PostIDLabel");
                Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
                Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
                Label lbl_Body = (Label)e.Item.FindControl("lbl_Body");
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

                string htmlContent = Server.HtmlDecode(lbl_Body.Text);
                Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                List<string> images = util.GetImagesInHTMLString(htmlContent);//.GetImagesInHTMLString(html);
                lbl_Body.Text = util.GetTrimmedBody(Server, 350, htmlContent);

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

                // .Replace("&amp;amp;lt;br /&amp;amp;gt;", "<br/>");
                // if (BodyLabel.Text.Length > 100)
                // {
                //     BodyLabel.Text = BodyLabel.Text.Substring(0, 99) + "..";
                // }

                HyperLink hprlnk_post = (HyperLink)e.Item.FindControl("hprlnk_post");
                HyperLink hprlnk_subject = (HyperLink)e.Item.FindControl("hprlnk_subject");
                string language = CultureInfo.CurrentCulture.Name;
                if (ThreadIDLabel != null)
                {

                    if (Request.QueryString["facebook"] != null)
                    {
                        Dictionary<string, int> tabs = new Dictionary<string, int>();
                        tabs.Add("en-GB", 259);
                        tabs.Add("el-GR", 260);
                        tabs.Add("cs-CZ", 261);
                        tabs.Add("de-AT", 262);
                        int joinTab = tabs[language];
                        string[] parameters = new string[3];
                        parameters = new string[3] { "threadid=" + ThreadIDLabel.Text, "scope=posts", "facebook=1" };
                        hprlnk_post.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(joinTab, "", parameters);
                    }
                    else
                    {
                        string[] parameters = new string[2];
                        parameters = new string[2] { "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                        hprlnk_post.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters);
                    }








                    // string url = "";
                    //string[] parameters = new string[3];

                    //parameters = new string[3] { "forumid=" + ForumIDLabel.Text, "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                    //url = NavigateURL(TabId, "", parameters);
                    // url = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters);
                    //hprlnk_post.NavigateUrl = url;
                    // Add FB support
                    int resultsPageId = 196;
                    string url2 = "";
                    // if accessed via Facebook App Canvas
                    if (Request.QueryString["facebook"] != null)
                    {
                        resultsPageId = 275;
                        string[] parameters2 = new string[2] { "result=" + ThreadIDLabel.Text, "facebook=1" };
                        url2 = DotNetNuke.Common.Globals.NavigateURL(resultsPageId, "", parameters2);
                    }
                    else
                    {
                        string[] parameters2 = new string[1] { "result=" + ThreadIDLabel.Text };
                        url2 = DotNetNuke.Common.Globals.NavigateURL(resultsPageId, "", parameters2);
                    }
                    hprlnk_subject.NavigateUrl = url2;
                    hprlnk_subject.NavigateUrl = url2.Replace("language/en-GB", "language/" + language);
                }


                Label lbl_FavoriteSolution = (Label)e.Item.FindControl("lbl_FavoriteSolution");

                IDataReader reader = DotNetNuke.Data.DataProvider.Instance().ExecuteSQL(@" SELECT        Ourspace_Proposal_Solutions.ThreadID, Forum_Posts.UserID, Forum_Posts.Body, Ourspace_Proposal_Solutions.IsFeatured
FROM            Ourspace_Proposal_Solutions INNER JOIN
                         Forum_Posts ON Ourspace_Proposal_Solutions.PostId = Forum_Posts.PostID
WHERE        (Ourspace_Proposal_Solutions.ThreadID = " + ThreadIDLabel.Text + @") AND (Ourspace_Proposal_Solutions.IsFeatured = 'true')
ORDER BY Ourspace_Proposal_Solutions.ThumbsUp DESC");
                if (reader.Read())
                {
                    //util = new Ourspace_Utilities.View();

                    lbl_FavoriteSolution.Text += " " + util.GetTrimmedBody(Server, 95, reader["body"].ToString());
                }
                // Displaying favorite- solution

                HyperLink hprlnk_userProfile = (HyperLink)e.Item.FindControl("hprlnk_userProfile");

                string lang = CultureInfo.CurrentCulture.ToString();
                hprlnk_userProfile.NavigateUrl = util.GetUserProfileLink(int.Parse(UserIDLabel.Text), lang, Request.QueryString["facebook"] != null);

            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }


        }

         public enum LanguageType { Own, All }


       

        protected int GetTotalResults(LanguageType type)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            string sql = "";
            if (type == LanguageType.Own)
            {
                sql = "SELECT COUNT(*) AS count FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Proposals_Thumbs ON Forum_Threads.ThreadID = Ourspace_Proposals_Thumbs.ThreadID WHERE (Forum_Groups.ModuleID = 381) AND (Ourspace_Forum_Thread_Info.phaseId = 4) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = '" + Session["resultsManagerOwnLang"] + "')";

            }
            else if (type == LanguageType.All)
            {
                sql = "SELECT        COUNT(*) AS count FROM            Forum_Threads INNER JOIN                         Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN                         Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID LEFT OUTER JOIN                         Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE        (Ourspace_Forum_Thread_Info.phaseId = 4) AND (Forum_Groups.ModuleID = 381)";
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
                        //lblTitle.Text = count + " " + lblTitle.Text;
                       

                    }
                    reader.Close();
                }
                sqlConn.Close();
            }
            return count;
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

        protected void sqldtsrc_DebateResults_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            DataPager DataPager1 = (DataPager)lstvw_DebateResults.FindControl("DataPager1");
            if (e.AffectedRows <= DataPager1.PageSize)
            {
                DataPager1.Visible = false;
            }
        }

        protected void lnkbtnViewOwnLanguage_Click(object sender, EventArgs e)
        {
            Session["resultsManagerOwnLang"] = CultureInfo.CurrentCulture.Name;









           // lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResultsOwnLang.ID;


            Session["resultsManager_currentLanguageMode"] = LanguageType.All;

            if (lstvw_DebateResults.DataSourceID == sqldtsrc_DebateResultsByDate.ID)
                lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResultsOwnLangByDate.ID;
            else if (lstvw_DebateResults.DataSourceID == sqldtsrc_DebateResultsByTitle.ID)
                lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResultsOwnLangByTitle.ID;
            else if (lstvw_DebateResults.DataSourceID == sqldtsrc_DebateResults.ID)
                lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResultsOwnLang.ID;





            lstvw_DebateResults.DataBind();
            pnlViewingAll.Visible = false;
            pnlViewingNational.Visible = true;
            GetTotalResults(LanguageType.Own);
        }

        protected void lnkbtnViewAllLanguages_Click(object sender, EventArgs e)
        {




            //lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResults.ID;





            Session["resultsManager_currentLanguageMode"] = LanguageType.All;

            if (lstvw_DebateResults.DataSourceID == sqldtsrc_DebateResultsOwnLangByDate.ID)
                lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResultsByDate.ID;
            else if (lstvw_DebateResults.DataSourceID == sqldtsrc_DebateResultsOwnLangByTitle.ID)
                lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResultsByTitle.ID;
            else if (lstvw_DebateResults.DataSourceID == sqldtsrc_DebateResultsOwnLang.ID)
                lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResults.ID;








            lstvw_DebateResults.DataBind();
            pnlViewingAll.Visible = true;
            pnlViewingNational.Visible = false;
            GetTotalResults(LanguageType.All);
        }


        protected void ddlSortDebates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["resultsManager_currentLanguageMode"] == null)
                Session["resultsManager_currentLanguageMode"] = LanguageType.Own;
            if ((LanguageType)Session["resultsManager_currentLanguageMode"] == LanguageType.All)
            {

                if (ddlSortDebates.SelectedValue == "Date")
                    lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResultsByDate.ID;
                else if (ddlSortDebates.SelectedValue == "Title")
                    lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResultsByTitle.ID;
                else if (ddlSortDebates.SelectedValue == "Popularity")
                    lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResults.ID;
            }
            else if ((LanguageType)Session["resultsManager_currentLanguageMode"] == LanguageType.Own)
            {
                if (ddlSortDebates.SelectedValue == "Date")
                    lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResultsOwnLangByDate.ID;
                else if (ddlSortDebates.SelectedValue == "Title")
                    lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResultsOwnLangByTitle.ID;
                else if (ddlSortDebates.SelectedValue == "Popularity")
                    lstvw_DebateResults.DataSourceID = sqldtsrc_DebateResultsOwnLang.ID;
            }
            lstvw_DebateResults.DataBind();

        }


    }

}
