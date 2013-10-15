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
using System.Web.UI.WebControls;


namespace DotNetNuke.Modules.Ourspace_YourToolkit
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_YourToolkit class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_YourToolkitModuleBase, IActionable
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
                System.Web.UI.Control ctl = DotNetNuke.Common.Globals.FindControlRecursiveDown(this.ContainerControl, "lblTitle");
                if ((ctl != null))
                {
                    ((Label)ctl).Text = Localization.GetString("YourToolkit.Text",LocalResourceFile);
                }
               // hprlnk_vote.NavigateUrl = "";

               // parameters = new string[3] {" };
                //url = NavigateURL(TabId, "", parameters);

                // Suggest
                if (CultureInfo.CurrentCulture.Name == "el-GR")
                {

                    hprlnk_suggest.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(94);
                }
                else if (CultureInfo.CurrentCulture.Name == "cs-CZ")
                {
                    hprlnk_suggest.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(107);
                }
                else if (CultureInfo.CurrentCulture.Name == "de-AT")
                {
                    hprlnk_suggest.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(177);
                }


                // Join
                if (CultureInfo.CurrentCulture.Name == "el-GR")
                {
                    hprlnk_join.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(93);
                }
                else if (CultureInfo.CurrentCulture.Name == "cs-CZ")
                {
                    hprlnk_join.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(106);
                }
                else if (CultureInfo.CurrentCulture.Name == "de-AT")
                {
                    hprlnk_join.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(171);
                }

                // Vote
                if (CultureInfo.CurrentCulture.Name == "el-GR")
                {
                    hprlnk_vote.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(124);
                }
                else if (CultureInfo.CurrentCulture.Name == "cs-CZ")
                {
                    hprlnk_vote.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(125);
                }
                else if (CultureInfo.CurrentCulture.Name == "de-AT")
                {
                    hprlnk_vote.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(173);
                }

                // Results
                if (CultureInfo.CurrentCulture.Name == "el-GR")
                {
                    hprlnk_view.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(160);
                }
                else if (CultureInfo.CurrentCulture.Name == "cs-CZ")
                {
                    hprlnk_view.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(161);
                }
                else if (CultureInfo.CurrentCulture.Name == "de-AT")
                {
                    hprlnk_view.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(172);
                }
                

               
                
                
               
                
                
                

                // Suggest Topic
                if (TabId == 74 || TabId == 73 || TabId == 94 || TabId == 107 || TabId == 177)
                {
                    toolkit_active.Attributes["class"] = toolkit_active.Attributes["class"] + " " + toolkit_active.Attributes["class"] + "-1";
                }
                // Join Open Debates
                else if (TabId == 62 || TabId == 171 || TabId == 106 || TabId == 93 || (TabId == 200 && Request.QueryString["mode"] == null))
                {
                    toolkit_active.Attributes["class"] = toolkit_active.Attributes["class"] + " " + toolkit_active.Attributes["class"] + "-2";
                }
                //  Vote Proposals
                else if (TabId == 124 || TabId == 122 || TabId == 173 || TabId == 125 || (TabId == 200 && Request.QueryString["mode"] != null))
                {
                    toolkit_active.Attributes["class"] = toolkit_active.Attributes["class"] + " " + toolkit_active.Attributes["class"] + "-3";
                }
                // View Results
                else if (TabId == 158 || TabId == 196 || TabId == 172 || TabId == 161 || TabId == 160)
                {
                    toolkit_active.Attributes["class"] = toolkit_active.Attributes["class"] + " " + toolkit_active.Attributes["class"] + "-4";
                }
                else
                {
                    toolkit_active.Attributes["class"] = toolkit_active.Attributes["class"] + " " + toolkit_active.Attributes["class"] + "-0";
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
