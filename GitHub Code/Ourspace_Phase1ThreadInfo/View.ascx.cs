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
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Common;
using System.Globalization;


namespace DotNetNuke.Modules.Ourspace_Phase1ThreadInfo
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_Phase1ThreadInfo class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_Phase1ThreadInfoModuleBase, IActionable
    {
        string CONNECTION_STRING = DotNetNuke.Common.Utilities.Config.GetConnectionString();
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
                if (CultureInfo.CurrentCulture.Name != "en-GB")
                    rdbtnlst_RejectionReasons.DataTextField = "description-" + CultureInfo.CurrentCulture.Name;
                if (!UserInfo.IsInRole("Administrator") && !UserInfo.IsInRole("Collaborator"))
                {
                   
                        lnkbtn_ApproveThread.Visible = false;
                        lnkbtn_RejectThread.Visible = false;
                   
                    
                }
                if (Request.QueryString["threadid"] != null)
                {
                   int threadId = Convert.ToInt32(Request.QueryString["threadid"]);
                    Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                    if (util.GetPhaseId(threadId) > 1)
                    {
                        lblPhase1Instructions.Visible = false;
                        lblPhase2Instructions.Visible = true;

                        hprlnk_GoToCurrentThreadPhase.NavigateUrl = util.GetTopicCurrentPhaseUrl(Request.QueryString["facebook"] != null, threadId, CultureInfo.CurrentCulture.Name);
                        hprlnk_GoToCurrentThreadPhase.Visible = true;

                        // We also hide the Collaborator buttons for moving topic to phase 2
                        lnkbtn_ApproveThread.Visible = false;
                        lnkbtn_RejectThread.Visible = false;

                    }
                    else
                    {
                        lblPhase1Instructions.Visible = true;
                        lblPhase2Instructions.Visible = false;
                        hprlnk_GoToCurrentThreadPhase.Visible = false;
                    }
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

        protected void lnkbtn_ApproveThread_Click(object sender, EventArgs e)
        {
            int threadId = -1;// GET QUERYSTRING
            if (Request.QueryString["threadid"] != null)
            {
                threadId = Convert.ToInt32(Request.QueryString["threadid"]);

                // Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
                // int currentForumID = Convert.ToInt32(ForumIDLabel.Text);

                Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                int currentForumID = util.GetForumId(threadId);
                Dictionary<int, int> fr = new Dictionary<int, int>(); // Forum correspondence
                fr.Add(18, 32); // Crime

                fr.Add(19, 33); // Drugs and alcohol

                fr.Add(20, 34); // Economy

                fr.Add(13, 3); // Education

                fr.Add(21, 35); // Employment

                fr.Add(6, 1); // Environment

                fr.Add(22, 36); // Health

                fr.Add(23, 37); // Human rights 

                fr.Add(24, 38); // Innovation

                fr.Add(53, 52); // Politics

                fr.Add(49, 48); // Other
                int newForumID = fr[currentForumID];
                DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();
                //int threadID = Convert.ToInt32(e.CommandArgument);

                int moderatorID = 0;
                string notes = "some notes";
                SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Forum_Thread_Move", threadId, newForumID, moderatorID, notes);
                //SqlHelper.execut
                util.UpdateThreadPhase(threadId, 2);

                String[] urlParams = {"forumid=" + newForumID, 
                                   "postid=" + threadId, 
                                   "scope=posts"};
               string url = Globals.NavigateURL(62, "", urlParams);
               util.SendEmailToThreadTrackersAboutMovingToPhase2(newForumID, threadId);
               util.TransferThreadSubscriptionsToPhase2(threadId, newForumID);
                //url = url + "#" + postId.ToString();
                Response.Redirect(url);
                //http://localhost/ourspace/OpenDebates/tabid/62/forumid/37/threadid/99/scope/posts/language/en-GB/Default.aspx

            }
        }

        protected void lnkbtn_RejectThread_Click(object sender, EventArgs e)
        {

            //SubjectLabel.Text = ((Label)e.Item.FindControl("SubjectLabel")).Text;
            ////hprlnk_userProfile.Text = ((HyperLink)e.Item.FindControl("hprlnk_userProfile")).Text;
            //CreatedDateLabel.Text = ((Label)e.Item.FindControl("CreatedDateLabel")).Text;
            //lbl_LastName.Text = ((Label)e.Item.FindControl("lbl_LastName")).Text;
            //lbl_Name.Text = ((Label)e.Item.FindControl("lbl_Name")).Text;
            //lbl_FullBody.Text = ((Label)e.Item.FindControl("lbl_FullBody")).Text;
            //ThreadIDLabel.Text = ((Label)e.Item.FindControl("ThreadIDLabel")).Text;
            //hprlnk_subject.NavigateUrl = ((HyperLink)e.Item.FindControl("hprlnk_subject")).NavigateUrl;

            lblPhase1Instructions.Visible = false;
            pnlRejectThread.Visible = true;
            lnkbtn_ApproveThread.Visible = false;
            lnkbtn_RejectThread.Visible = false;
                  
            //lstvw_DebateProposals.Visible = false;
        }

        protected void lnkBtn_SubmitRejection_Click(object sender, EventArgs e)
        {
            int threadId = Convert.ToInt32(Request.QueryString["threadid"].ToString());
            //rdbtnlst_RejectionReasons.DataTextField =
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            util.RejectThread(Convert.ToInt32(rdbtnlst_RejectionReasons.SelectedValue),
                txtRejectionComment.Text,
                threadId);
            util.SendEmailToThreadTrackersAboutTopicRejection(threadId);
            Response.Redirect(Request.Url.ToString());
        }

        protected void lnkBtnCancelRejection_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }

        protected void lstvwRejectionReason_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            lnkbtn_ApproveThread.Visible = false;
            lnkbtn_RejectThread.Visible = false;
            lblPhase1Instructions.Visible = false;
        }

    }

}
