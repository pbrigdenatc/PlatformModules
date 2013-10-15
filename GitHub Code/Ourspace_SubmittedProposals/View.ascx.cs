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
using System.Web.Security;
using DotNetNuke.Modules.Ourspace_Utilities;
using System.Globalization;

namespace DotNetNuke.Modules.Ourspace_SubmittedProposals
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_SubmittedProposals class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_SubmittedProposalsModuleBase, IActionable    {

        
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
                   
                    //el-GR 93
                    // cs- 106
                    // de-AT 171
                    ContainerControl.Visible = false;
                }
                if (Request.QueryString["threadId"] != null)
                {
                    Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                    hdnfld_ThreadId.Value = Convert.ToInt32(Request.QueryString["threadId"].ToString()).ToString();
                    sqldtsrc_submittedProposals.DataBind();
                    Repeater1.DataBind();
                    //if (util.GetPhaseId(int.Parse(hdnfld_ThreadId.Value)) == 2)
                    //{
                      //  Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "submittedProposals", (this.TemplateSourceDirectory + "/js/submittedProposals.js"));
                    //}
                    string[] parameters = new string[1] { "threadId=" + hdnfld_ThreadId.Value };
                    string url = DotNetNuke.Common.Globals.NavigateURL(200, "", parameters);
                    url = url.Replace("en-GB", CultureInfo.CurrentCulture.ToString());
                    hprlnk_ViewAllSubmittedProposals.NavigateUrl = url;
                }
                else if(Request.QueryString["postId"] != null)
                {
                    Ourspace_Utilities.View util = new Ourspace_Utilities.View();

                    hdnfld_ThreadId.Value = util.GetThreadId(int.Parse(Request.QueryString["postId"].ToString())).ToString();
                    sqldtsrc_submittedProposals.DataBind();
                    Repeater1.DataBind();
                   // if (util.GetPhaseId(int.Parse(hdnfld_ThreadId.Value)) == 2)
                    //{
                     //   Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "submittedProposals", (this.TemplateSourceDirectory + "/js/submittedProposals.js"));
                   // }
                    string[] parameters = new string[1] { "threadId=" + hdnfld_ThreadId.Value };
                    string url = DotNetNuke.Common.Globals.NavigateURL(200, "", parameters);
                    url = url.Replace("en-GB", CultureInfo.CurrentCulture.ToString());
                    hprlnk_ViewAllSubmittedProposals.NavigateUrl = url;
                }
                SetTitle(DotNetNuke.Services.Localization.Localization.GetString("Title.Text",LocalResourceFile));
                
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        
        #endregion

         private void SetTitle(string title)
         {

             System.Web.UI.Control ctl = DotNetNuke.Common.Globals.FindControlRecursiveDown(this.ContainerControl, "lblTitle");
             if ((ctl != null))
             {
                 ((Label)ctl).Text = title;
             }
         }

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

        protected void Repeater1_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            Image imgUser = (Image) e.Item.FindControl("imgUser");
         Label lblUsername = (Label)   e.Item.FindControl("lblUserName");
         string username = lblUsername.Text;
            DotNetNuke.Entities.Users.UserInfo user =   DotNetNuke.Entities.Users.UserController.GetUserByName(0,username);
            Ourspace_Utilities.View view = new Ourspace_Utilities.View();

            imgUser.ImageUrl = view.GetOurSpaceUserImgUrl(Server, user.UserID);


            Label lblDate = (Label)e.Item.FindControl("lblDate");
          DateTime date =  Convert.ToDateTime(lblDate.Text);
          lblDate.Text = date.ToString("dd MMM. yyyy");

         Label lblDisplayName = (Label)e.Item.FindControl("lblUserDisplayName");
         lblDisplayName.Text = user.DisplayName;
        }

        

    }

}
