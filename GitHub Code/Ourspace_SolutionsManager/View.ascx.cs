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
using Microsoft.ApplicationBlocks.Data;


namespace DotNetNuke.Modules.Ourspace_SolutionsManager
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_SolutionsManager class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_SolutionsManagerModuleBase, IActionable
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


            //Label PostIDLabel = (Label)e.Item.FindControl("PostIDLabel");
            Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
            Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
            Label BodyLabel = (Label)e.Item.FindControl("BodyLabel");
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
            if (ThreadIDLabel != null)
            {
                string url = "";
                string[] parameters = new string[3];

                parameters = new string[3] { "forumid=" + ForumIDLabel.Text, "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                //url = NavigateURL(TabId, "", parameters);
                url = DotNetNuke.Common.Globals.NavigateURL(73, "", parameters);
                hprlnk_post.NavigateUrl = url;
            }


        }

    }

}
