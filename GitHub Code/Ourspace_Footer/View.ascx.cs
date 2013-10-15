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


namespace DotNetNuke.Modules.Ourspace_Footer
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_Footer class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_FooterModuleBase, IActionable
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
                string resource = LocalResourceFile.Replace("footer_module", "View.ascx.resx");
                string locale =  DotNetNuke.Services.Localization.Localization.GetString("Locale.Text", resource);
                if (locale == "en-GB")
                {
                     
                    hprlnkPrivacy.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(228, "");
                    hprlnkTermsAndCons.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(216, "");
                    hprlnkLegal.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(224, "");
                    hprlnkFaq.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(220, "");
                    hprlnkGuidelines.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(295, "");                   
                }
                else if (locale == "el-GR")
                {
                   
                    hprlnkPrivacy.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(229, "");
                    hprlnkTermsAndCons.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(217, "");
                    hprlnkLegal.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(225, "");
                    hprlnkFaq.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(221, "");
                    hprlnkGuidelines.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(296, "");

                }
                else if (locale == "cs-CZ")
                {
                    
                    hprlnkPrivacy.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(230, "");
                    hprlnkTermsAndCons.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(218, "");
                    hprlnkLegal.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(226, "");
                    hprlnkFaq.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(222, "");
                    hprlnkGuidelines.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(297, "");
                }
                else if (locale == "de-AT")
                {
                    hprlnkPrivacy.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(231, "");
                    hprlnkTermsAndCons.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(219, "");
                    hprlnkLegal.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(227, "");
                    hprlnkFaq.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(223, "");
                    hprlnkGuidelines.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(298, "");
                }
                //LocalResourceFile
                
            
            hprlnkLegal.Text = DotNetNuke.Services.Localization.Localization.GetString("LegalDisclaimer.Text", resource);
            hprlnkPrivacy.Text = DotNetNuke.Services.Localization.Localization.GetString("PrivacyPolicy.Text", resource);
            hprlnkTermsAndCons.Text = DotNetNuke.Services.Localization.Localization.GetString("TermsAndCons.Text", resource);
            hprlnkFaq.Text = DotNetNuke.Services.Localization.Localization.GetString("Faq.Text", resource);
            hprlnkGuidelines.Text = DotNetNuke.Services.Localization.Localization.GetString("Guidelines.Text", resource);

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
