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
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DotNetNuke.Modules.Ourspace_WhoIsOnOurspace
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_WhoIsOnOurspace class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_WhoIsOnOurspaceModuleBase, IActionable
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
                
//                HtmlHead head = Page.Header;

//                LiteralControl lctl = new LiteralControl("<link rel='image_src' href='http://joinourspace.eu/images/logo.png' />");

//head.Controls.Add(lctl);
                if (Session["FacebookUserId"] != null && Session["FacebookUserId"].ToString() == "0" && !UserInfo.IsInRole("Administrator"))
                {
                    ContainerControl.Visible = false;
                }

                // On the Join Discussion page the module is not always visible
                if ((((TabId == 62 || TabId == 93 || TabId == 106 || TabId == 171) && Request.QueryString["scope"] == null) || ((TabId == 62 || TabId == 93 || TabId == 106 || TabId == 171) && Request.QueryString["scope"].ToString() == "threads") || ((TabId == 62 || TabId == 93 || TabId == 106 || TabId == 171) && Request.QueryString["scope"].ToString() == "threadsearch")) && !UserInfo.IsInRole("Administrator"))
                {
                    ContainerControl.Visible = false;
                }

                if (UserId > -1)
                {
                    Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "whoisonourspace", (this.TemplateSourceDirectory + "/js/whoisonourspace.js"));
                    
                }
                SetTitle(DotNetNuke.Services.Localization.Localization.GetString("Who.Text", LocalResourceFile));
            
                        
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void SetTitle(string title)
        {

            System.Web.UI.Control ctl = DotNetNuke.Common.Globals.FindControlRecursiveDown(this.ContainerControl, "lblTitle");
            if ((ctl != null))
            {
                ((Label)ctl).Text = title;
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

    }

}
