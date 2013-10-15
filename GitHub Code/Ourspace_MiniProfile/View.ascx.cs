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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using DotNetNuke.Modules.Ourspace_Utilities;


namespace DotNetNuke.Modules.Ourspace_MiniProfile
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_MiniProfile class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_MiniProfileModuleBase, IActionable
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
                if (UserId > -1)
                {
                        
                    if (!IsPostBack)
                    {
                       // Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "miniprofile", (this.TemplateSourceDirectory + "/js/miniprofile.js?v=5"));
                        Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                        lblName.Text = UserInfo.FirstName;
                        int points = util.GetUserPoints(UserInfo.UserID);
                        lblPointsNo.Text = points.ToString();
                        int level1 = util.GetLevel(0);
                        int level2 = util.GetLevel(50);
                        int level3= util.GetLevel(100);
                        int level4 = util.GetLevel(101);
                        int level5 = util.GetLevel(222);
                        int level6 = util.GetLevel(580);

                        
                        util.GetLevel(points).ToString();
                        util.GetLevel(points).ToString();

                        util.GetLevel(points).ToString();

                        img_Profile.ImageUrl = GetImageUrl(UserId);
                        img_Profile.ImageUrl = util.GetOurSpaceUserImgUrl(Server, UserId);
                        lblLevelNo.Text = util.GetLevel(points).ToString();
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        

        private string GetImageUrl(int userId)
        {
            string strPath = Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + userId.ToString("000") + "\\");
            if (Directory.Exists(strPath))
            {
                if (userId <= 9)
                {
                    strPath = (ResolveUrl("~/Portals/" + PortalId + "/Users/" + userId.ToString("000") + "/" + userId.ToString("00")));
                }
                else
                {
                    strPath = ResolveUrl("~/Portals/" + PortalId + "/Users/" + userId.ToString("000") + "/" + userId);
                }
                strPath += "/" + userId + "/" + userId + "_50.jpg?" + DateTime.Now.Ticks;
                return strPath;
            }
            return ResolveUrl("~/images/no-avatar.png");

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
