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
using System.Globalization;


namespace DotNetNuke.Modules.Ourspace_Statistics
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_Statistics class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_StatisticsModuleBase, IActionable
    {

        #region Event Handlers
        bool isFacebook = false;
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
                if (Request.QueryString["facebook"] != null)
                {
                    isFacebook = true;
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

        protected void lstvw_NewestMember_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            HyperLink hprlnkUserProfile = (HyperLink)e.Item.FindControl("hprlnkUserProfile");

            string[] parameters3 = new string[1];

            Label lblUserId = (Label)e.Item.FindControl("lblUserId");

            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            string lang = CultureInfo.CurrentCulture.ToString();
            hprlnkUserProfile.NavigateUrl = util.GetUserProfileLink(int.Parse(lblUserId.Text), lang, isFacebook);
            
        }

    }

}
