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
using DotNetNuke.Entities.Modules.Communications;


namespace DotNetNuke.Modules.Ourspace_ThreadDetails
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_ThreadDetails class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_ThreadDetailsModuleBase, IActionable, IModuleListener
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
                // On the Join Discussion page the module is not always visible
                if ((((TabId == 62 || TabId == 93 || TabId == 106 || TabId == 171) && Request.QueryString["scope"] == null) || ((TabId == 62 || TabId == 93 || TabId == 106 || TabId == 171) && Request.QueryString["scope"].ToString() == "threads") || ((TabId == 62 || TabId == 93 || TabId == 106 || TabId == 171) && Request.QueryString["scope"].ToString() == "threadsearch")) && !UserInfo.IsInRole("Administrator"))
                {
                    ContainerControl.Visible = false;
                }
                if (Request.QueryString["threadId"] != null)
                {
                    hdnfld_ThreadId.Value = Convert.ToInt32(Request.QueryString["threadId"].ToString()).ToString();
                    sqldtsrc_ThreadInfo.DataBind();
                    rptr_ThreadInfo.DataBind();
                }
                else
                {
                    //hdnfld_ThreadId.Value = Convert.ToInt32(Request.QueryString["threadId"].ToString()).ToString();

                    int postId = Convert.ToInt32(Request.QueryString["postid"]);
                    Ourspace_Utilities.View util = new Ourspace_Utilities.View();

                    hdnfld_ThreadId.Value = util.GetThreadId(postId).ToString();
                                        
                    sqldtsrc_ThreadInfo.DataBind();
                    rptr_ThreadInfo.DataBind();

                }
                SetTitle(DotNetNuke.Services.Localization.Localization.GetString("TopicDebateStatus.Text", LocalResourceFile));
                
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

        protected void rptr_ThreadInfo_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            Label lblReplies = (Label)e.Item.FindControl("lblReplies");
            HyperLink hprlnk_Category = (HyperLink)e.Item.FindControl("hprlnk_Category");
            int forumId = 0;

            switch (hprlnk_Category.Text)
            {
                case "Crime":
                    forumId = 32;
                    break;
                case "Drugs and Alcohol":
                    forumId = 33;
                    break;
                case "Economy":
                    forumId = 34;
                    break;
                case "Education":
                    forumId = 3;
                    break;
                case "Employment":
                    forumId = 35;
                    break;
                case "Environment":
                    forumId = 1;
                    break;
                case "Health":
                    forumId = 36;
                    break;
                case "Human Rights":
                    forumId = 37;
                    break;
                case "Innovation":
                    forumId = 38;
                    break;
                case "Politics":
                    forumId = 52;
                    break;
                case "Other":
                    forumId = 48;
                    break;
            }
            string category = hprlnk_Category.Text.Replace(" ", "");

            hprlnk_Category.Text = Localization.GetString(category + ".Text", LocalResourceFile);

            string[] parameters = new string[2];
            parameters = new string[2] { "forumId=" + forumId, "scope=threads" };
            string url = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters);
            hprlnk_Category.NavigateUrl = url;

            if (lblReplies.Text != "")
            {
                lblReplies.Text = (Convert.ToInt32(lblReplies.Text) + 1).ToString();
            }
            Label lblPhaseName = (Label)e.Item.FindControl("lblPhaseName");
            lblPhaseName.Text = GetPhaseName(int.Parse(lblPhaseName.Text));



        }

        private void SetTitle(string title)
        {

            System.Web.UI.Control ctl = DotNetNuke.Common.Globals.FindControlRecursiveDown(this.ContainerControl, "lblTitle");
            if ((ctl != null))
            {
                ((Label)ctl).Text = title;
            }
        }

        public string GetThreadId()
        {
            if (Request.QueryString["threadid"] != null)
            {
                pnlLikeButton.Visible = true;
                return Request.QueryString["threadid"].ToString();
            }
            else if (Request.QueryString["postid"] != null)
            {
                //int postId = Convert.ToInt32(Request.QueryString["postid"]);
                //Ourspace_Utilities.View util = new Ourspace_Utilities.View();
               // return util.GetThreadId(postId).ToString();
                return hdnfld_ThreadId.Value;
            }
            else
            {

                pnlLikeButton.Visible = false;
            }
            return "";
        }

        public string GetPhaseName(int phaseId)
        {
            string test = LocalResourceFile;
            string phaseName = "undefined";
            if (phaseId == 1)
            {
                phaseName = Localization.GetString("phase1name", LocalResourceFile);
            }
            else if (phaseId == 2)
            {
                phaseName = Localization.GetString("phase2name", LocalResourceFile);
            }
            else if (phaseId == 3)
            {
                phaseName = Localization.GetString("phase3name", LocalResourceFile);
            }
            else if (phaseId == 4)
            {
                phaseName = Localization.GetString("phase4name", LocalResourceFile);
            }
            return phaseName;
        }

        public void OnModuleCommunication(object s,
        ModuleCommunicationEventArgs e)
        {
            if (e.Target == "Ourspace_ThreadDetails")
            {
                if (e.Text == "updatePhaseDisplay")
                {
                    rptr_ThreadInfo.DataBind();
                }
            }
            // throw new NotImplementedException();
        }


    }

}
