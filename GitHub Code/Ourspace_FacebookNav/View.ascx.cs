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


namespace DotNetNuke.Modules.Ourspace_FacebookNav
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_FacebookNav class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_FacebookNavModuleBase, IActionable
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
                if (TabId == 251 || TabId == 252 || TabId == 253 || TabId == 254)
                {
                    overviewDiv.Attributes["class"] = "tab-active";
                }
                else if(TabId == 255 || TabId == 256 || TabId == 257 || TabId == 258)
                {
                    suggestDiv.Attributes["class"] = "tab-active";
                }
                else if (TabId == 271 || TabId == 272 || TabId == 273 || TabId == 274)
                {
                    suggestDiv.Attributes["class"] = "tab-active";
                }
                else if (TabId == 259 || TabId == 260 || TabId == 261 || TabId == 262)
                {
                    joinDiv.Attributes["class"] = "tab-active";
                }
                else if (TabId == 263 || TabId == 264 || TabId == 265 || TabId == 266)
                {
                    voteDiv.Attributes["class"] = "tab-active";
                }
                else if (TabId == 267 || TabId == 268 || TabId == 269 || TabId == 270)
                {
                    resultsDiv.Attributes["class"] = "tab-active";
                }
                else if (TabId == 275 || TabId == 276 || TabId == 277 || TabId == 278)
                {
                    resultsDiv.Attributes["class"] = "tab-active";
                }
                else if (TabId == 279 || TabId == 280 || TabId == 281 || TabId == 282)
                {
                    voteDiv.Attributes["class"] = "tab-active";
                }
                else if (TabId == 287)
                {

                    dashboardDiv.Attributes["class"] = "tab-active";
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
