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


namespace DotNetNuke.Modules.Ourspace_SearchBox
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_SearchBox class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_SearchBoxModuleBase, IActionable
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
                txtSearchTerms.Attributes.Add("onKeyPress", "javascript:if (event.keyCode == 13) __doPostBack('" + lnkbtn_Search.UniqueID + "','')");

                Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "jquery.watermark", (this.TemplateSourceDirectory + "/js/jquery.watermark.js"));
                Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Ourspace_SearchBox", (this.TemplateSourceDirectory + "/js/Ourspace_SearchBox.js"));

       
                

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

        protected void lnkbtn_Search_Click(object sender, EventArgs e)
        {
            string searchTerms = txtSearchTerms.Text;
            if (searchTerms != string.Empty)
            {
                string[] parameters = new string[3] { "scope=threadsearch", "subject=" + searchTerms, "body=" + searchTerms };
                string url = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters);
                //hprlnk_ProposeTopic.NavigateUrl = url;
                Response.Redirect(url);
            }
        }

    }

}
