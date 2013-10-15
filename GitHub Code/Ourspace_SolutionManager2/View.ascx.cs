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
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;


namespace DotNetNuke.Modules.Ourspace_SolutionManager2
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_SolutionManager2 class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_SolutionManager2ModuleBase, IActionable
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
                if (Session["activeDiscussionsOwnLang"] == null)
                {
                    Session["activeDiscussionsOwnLang"] = CultureInfo.CurrentCulture.Name;
                }
                
                if (Request.QueryString["facebook"] != null)
                {
                    isFacebook = true;
                }
              if( UserInfo.IsInRole("Administrator"))
            {
             
            }
            //  GetTotalVotingDiscussions(LanguageType.Own);

              if (Request.QueryString["discussion"] != null)
              {
                  hdnfld_DiscussionByID.Value = Convert.ToInt32(Request.QueryString["discussion"].ToString()).ToString();
                  lstvw_ActiveDiscussions.DataSourceID = sqldtsrc_ActiveDiscussionsByID.ID;
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
                int userHasAlreadyRated = Convert.ToInt32(SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Proposal_Solutions_User_Check", e.CommandArgument, UserId));
                if (userHasAlreadyRated == 0 && UserId > -1)
                {
                    SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Ourspace_Proposal_Solutions_Thumbs_Update", e.CommandArgument, UserId, 0);

                }
                lstvw_ActiveDiscussions.DataBind();
            }
            else if (e.CommandName == "ManageSolutions")
            {
               // TextBox ThreadIDTextBox = (TextBox)lstvw_Solutions.InsertItem.FindControl("ThreadIDTextBox");

            

                hdnfld_CurrentProposal.Value = e.CommandArgument.ToString();
              
               // lstvw_Solutions.Visible = true;

            }

        }



        protected void lstvw_Solutions_DataBound(object sender, EventArgs e)
        {
            
         
        }

        protected void sqldtsrc_Solutions_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
         
        }

        protected void lstvw_DebateProposals_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {

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
            if (!UserInfo.IsInRole("Administrator") && lnkbtn_ApproveThread != null)
            {
                lnkbtn_ApproveThread.Visible = false;
            }


            Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
            Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
            Label BodyLabel = (Label)e.Item.FindControl("BodyLabel");
            Label CreatedDateLabel = (Label)e.Item.FindControl("CreatedDateLabel");

            string[] dateArr = CreatedDateLabel.Text.Split(' ');
            CreatedDateLabel.Text = dateArr[0] + " @ " + dateArr[1];
            BodyLabel.Text = Server.HtmlDecode(BodyLabel.Text);

            HyperLink hprlnk_post = (HyperLink)e.Item.FindControl("hprlnk_post");
            if (ThreadIDLabel != null)
            {
                string url = "";
                string[] parameters = new string[3];

                parameters = new string[3] { "forumid=" + ForumIDLabel.Text, "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                url = DotNetNuke.Common.Globals.NavigateURL(73, "", parameters);
                hprlnk_post.NavigateUrl = url;
            }


        }


        protected void lstvw_ResultsSnippets_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
            Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
            
            Label lbl_Body = (Label)e.Item.FindControl("lbl_Body");
            Label CreatedDateLabel = (Label)e.Item.FindControl("CreatedDateLabel");
            Label lbl_UserId = (Label)e.Item.FindControl("lbl_UserId");
            Literal ltrlImage = (Literal)e.Item.FindControl("ltrlImage");
            HyperLink hprlnk_subject = (HyperLink)e.Item.FindControl("hprlnk_subject");

            HyperLink hprlnk_post = (HyperLink)e.Item.FindControl("hprlnk_post");
            

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
            lbl_Body.Text = util.GetTrimmedBody(Server,350,htmlContent);

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

            string url = "";
            int resultsPageId = 200;

            if (Request.QueryString["facebook"] != null)
            {
                // if accessed via Facebook App Canvas
                resultsPageId = 279;
                string[] parameters2 = new string[3];
                parameters2 = new string[3] { "threadid=" + ThreadIDLabel.Text, "mode=featured","facebook=1" };
                url = DotNetNuke.Common.Globals.NavigateURL(resultsPageId, "", parameters2);
            }
            else
            {

                string[] parameters2 = new string[2];
                parameters2 = new string[2] { "threadid=" + ThreadIDLabel.Text, "mode=featured" };
                url = DotNetNuke.Common.Globals.NavigateURL(resultsPageId, "", parameters2);
            }


            string language = CultureInfo.CurrentCulture.Name;
            hprlnk_subject.NavigateUrl = url.Replace("language/en-GB", "language/"+language);


             HyperLink hprlnk_userProfile = (HyperLink)e.Item.FindControl("hprlnk_userProfile");

             string lang = CultureInfo.CurrentCulture.ToString();
             hprlnk_userProfile.NavigateUrl = util.GetUserProfileLink(int.Parse(lbl_UserId.Text), lang, isFacebook);


             
             if (isFacebook)
             {
                 Dictionary<string, int> tabs = new Dictionary<string, int>();
                 
                 tabs.Add("en-GB", 259);
                 tabs.Add("el-GR", 260);
                 tabs.Add("cs-CZ", 261);
                 tabs.Add("de-AT", 262);
                 int suggestTab = tabs[language];
                 string[] parameters = new string[3];
                 parameters = new string[3] { "threadid=" + ThreadIDLabel.Text, "scope=posts", "facebook=1" };
                 hprlnk_post.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(suggestTab, "", parameters);

             }
             else
             {
                 Dictionary<string, int> tabs = new Dictionary<string, int>();
                 tabs.Add("en-GB", 62);
                 tabs.Add("el-GR", 93);
                 tabs.Add("cs-CZ", 106);
                 tabs.Add("de-AT", 171);
                 int suggestTab = tabs[language];
                 string[] parameters = new string[2];
                 parameters = new string[2] { "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                 hprlnk_post.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(suggestTab, "", parameters);
             }




             


        }


      
        protected void lstvw_ActiveDiscussions_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {

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
            if (!UserInfo.IsInRole("Administrator") && lnkbtn_ApproveThread != null)
            {
                lnkbtn_ApproveThread.Visible = false;
            }


            //Label PostIDLabel = (Label)e.Item.FindControl("PostIDLabel");
            Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
            Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
            Label BodyLabel = (Label)e.Item.FindControl("BodyLabel");
            Label lbl_UserId = (Label)e.Item.FindControl("lbl_UserId");
            Label CreatedDateLabel = (Label)e.Item.FindControl("CreatedDateLabel");
            
            string[] dateArr = CreatedDateLabel.Text.Split(' ');
            CreatedDateLabel.Text = dateArr[0] + " @ " + dateArr[1];
            BodyLabel.Text = Server.HtmlDecode(BodyLabel.Text);


            // .Replace("&amp;amp;lt;br /&amp;amp;gt;", "<br/>");
            // if (BodyLabel.Text.Length > 100)
            // {
            //     BodyLabel.Text = BodyLabel.Text.Substring(0, 99) + "..";
            // }

            HyperLink hprlnk_post = (HyperLink)e.Item.FindControl("hprlnk_post");
            HyperLink hprlnk_userProfile = (HyperLink)e.Item.FindControl("hprlnk_userProfile");

                        Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                        string lang = CultureInfo.CurrentCulture.ToString();
                        hprlnk_userProfile.NavigateUrl = util.GetUserProfileLink(int.Parse(lbl_UserId.Text), lang, isFacebook);

                 

            
            if (ThreadIDLabel != null)
            {
                string url = "";
                string[] parameters = new string[3];

               

                parameters = new string[3] { "forumid=" + ForumIDLabel.Text, "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                //url = NavigateURL(TabId, "", parameters);
                url = DotNetNuke.Common.Globals.NavigateURL(73, "", parameters);
                hprlnk_post.NavigateUrl = url;

               
            }
            if (!UserInfo.IsInRole("Administrator"))
            {
                ListView lstvw_SolutionsInner = (ListView)e.Item.FindControl("lstvw_Solutions");
                Panel pnl_insertTemplate = (Panel)lstvw_SolutionsInner.InsertItem.FindControl("pnl_insertTemplate");
                pnl_insertTemplate.Visible = false;
            }
        }


        protected void lstvw_Solutions_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //if (e.Item.ItemType == ListViewItemType.DataItem)
            //{
            //    ListView lstvw_Solutions = (ListView)sender;
            //    ListView lstvw_SolutionsInner = (ListView)lstvw_Solutions.FindControl("lstvw_Solutions");
            //    Panel pnl_insertTemplate = (Panel)lstvw_SolutionsInner.InsertItem.FindControl("pnl_insertTemplate");
            //    //Panel pnl_insertTemplate = (Panel)e.Item.FindControl("pnl_insertTemplate");
            //    if (!UserInfo.IsInRole("Administrator"))
            //    {
            //        pnl_insertTemplate.Visible = false;
            //    }
            //}
            Button DeleteButton = (Button)e.Item.FindControl("DeleteButton");
            Button EditButton = (Button)e.Item.FindControl("EditButton");
            if (!UserInfo.IsInRole("Administrator"))
            {
                DeleteButton.Visible = false;
                EditButton.Visible = false;
            }

        }

        protected void lstvw_Solutions_ItemCommand(object sender, ListViewCommandEventArgs e)
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
                int userHasAlreadyRated = Convert.ToInt32(SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Proposal_Solutions_User_Check", e.CommandArgument, UserId));
                if (userHasAlreadyRated == 0 && UserId > -1)
                {
                    SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Ourspace_Proposal_Solutions_Thumbs_Update", e.CommandArgument, UserId, 0);

                }
                lstvw_ActiveDiscussions.DataBind();
            }
        }

        public enum LanguageType { Own, All }

       

    

        protected void lstvw_ResultsSnippets_DataBound(object sender, EventArgs e)
        {

            DataPager DataPager1 = (DataPager)lstvw_ResultsSnippets.FindControl("DataPager1");
            if (DataPager1 != null)
            {
                lblTopicsCount.Text = DataPager1.TotalRowCount.ToString();
            }
        }

        protected void lnkbtnViewOwnLanguage_Click(object sender, EventArgs e)
        {
            Session["activeDiscussionsOwnLang"] = CultureInfo.CurrentCulture.Name;




            //lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussionsOwnLang.ID;



            Session["solutionManager_currentLanguageMode"] = LanguageType.All;

            if (lstvw_ResultsSnippets.DataSourceID == sqldtsrc_ActiveDiscussionsByDate.ID)
                lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussionsOwnLangByDate.ID;
            else if (lstvw_ResultsSnippets.DataSourceID == sqldtsrc_ActiveDiscussionsByTitle.ID)
                lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussionsOwnLangByTitle.ID;
            else if (lstvw_ResultsSnippets.DataSourceID == sqldtsrc_ActiveDiscussions.ID)
                lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussionsOwnLang.ID;



            lstvw_ResultsSnippets.DataBind();
            pnlViewingAll.Visible = false;
            pnlViewingNational.Visible = true;
        }

        protected void lnkbtnViewAllLanguages_Click(object sender, EventArgs e)
        {

            Session["solutionManager_currentLanguageMode"] = LanguageType.All;

            if (lstvw_ResultsSnippets.DataSourceID == sqldtsrc_ActiveDiscussionsOwnLangByDate.ID)
                lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussionsByDate.ID;
            else if (lstvw_ResultsSnippets.DataSourceID == sqldtsrc_ActiveDiscussionsOwnLangByTitle.ID)
                lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussionsByTitle.ID;
            else if (lstvw_ResultsSnippets.DataSourceID == sqldtsrc_ActiveDiscussionsOwnLang.ID)
                lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussions.ID;

            
            
         //   lstvw_ResultsSnippets.DataBind();
          //  pnlViewingAll.Visible = true;
         //   pnlViewingNational.Visible = false;
         //   GetTotalVotingDiscussions(LanguageType.All);








            lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussions.ID;
            lstvw_ResultsSnippets.DataBind();
            pnlViewingAll.Visible = true;
            pnlViewingNational.Visible = false;
        }

        protected void ddlSortDebates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["solutionManager_currentLanguageMode"] == null)
                Session["solutionManager_currentLanguageMode"] = LanguageType.Own;
            if ((LanguageType)Session["solutionManager_currentLanguageMode"] == LanguageType.All)
            {
                
                if (ddlSortDebates.SelectedValue == "Date")
                    lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussionsByDate.ID;
                else if (ddlSortDebates.SelectedValue == "Title")
                    lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussionsByTitle.ID;
               else if (ddlSortDebates.SelectedValue == "Popularity")
                    lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussions.ID;
            }
            else if ((LanguageType)Session["solutionManager_currentLanguageMode"] == LanguageType.Own)
            {
                if (ddlSortDebates.SelectedValue == "Date")
                    lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussionsOwnLangByDate.ID;
                else if (ddlSortDebates.SelectedValue == "Title")
                    lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussionsOwnLangByTitle.ID;
               else if (ddlSortDebates.SelectedValue == "Popularity")
                   lstvw_ResultsSnippets.DataSourceID = sqldtsrc_ActiveDiscussionsOwnLang.ID;
            }
            lstvw_ResultsSnippets.DataBind();

        }
    }

}
