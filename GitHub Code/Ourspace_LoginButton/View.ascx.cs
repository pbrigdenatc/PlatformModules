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
using System.Globalization;


namespace DotNetNuke.Modules.Ourspace_LoginButton
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_LoginButton class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_LoginButtonModuleBase, IActionable
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
            {//LocalResourceFile
                string resource = LocalResourceFile.Replace("login_module", "View.ascx.resx");
                hprlnkLogin.Text = DotNetNuke.Services.Localization.Localization.GetString("Login.Text", resource);
                hprlnkLogout.Text = DotNetNuke.Services.Localization.Localization.GetString("Logout.Text", resource);
            
                if (Request.IsAuthenticated)
                {
                    hprlnkLogin.Visible = false;
                    hprlnkLogout.Visible = true;
                }
                else
                {
                    hprlnkLogin.Visible = true;
                    hprlnkLogout.Visible = false;
                }

                string returnurl = "";
                if (CultureInfo.CurrentCulture.Name == "en-GB")
                {

                    string[] parameters = new string[1];
                    parameters = new string[1] { "returnurl="+returnurl };
                    hprlnkLogin.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(208, "", parameters);

                }
                else if (CultureInfo.CurrentCulture.Name == "el-GR")
                {
                    string[] parameters = new string[1];
                    parameters = new string[1] { "returnurl=" + returnurl };
                    hprlnkLogin.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(209, "", parameters);

                }
                else if (CultureInfo.CurrentCulture.Name == "cs-CZ")
                {

                    string[] parameters = new string[1];
                    parameters = new string[1] { "returnurl=" + returnurl };
                    hprlnkLogin.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(210, "", parameters);
                }
                else if (CultureInfo.CurrentCulture.Name == "de-AT")
                {
                    string[] parameters = new string[1];
                    parameters = new string[1] { "returnurl=" + returnurl };
                    hprlnkLogin.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(211, "", parameters);
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

    }

}
