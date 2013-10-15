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
using Telerik.Web.UI;
using DotNetNuke.Modules.Ourspace_Utilities;
using DotNetNuke.Common;
using System.Globalization;
using System.Web.UI;
using DotNetNuke.Common.Utilities;
using System.Data.SqlClient;
using DotNetNuke.Entities.Users;

namespace DotNetNuke.Modules.Ourspace_PeopleSearch
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_PeopleSearch class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_PeopleSearchModuleBase, IActionable
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
                string test = LocalResourceFile;
                Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Ourspace_PeopleSearch", (this.TemplateSourceDirectory + "/js/Ourspace_PeopleSearch.js"));


                if (UserInfo.IsInRole("Administrators"))
                {
                    lnkbtn_testUser.Visible = true;
                }








            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }



        public string GetUserImgUrl(string userId)
        {
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
           
            return util.GetOurSpaceUserImgUrl(Server, Convert.ToInt32( userId));

           
          
        }

        public string GetUserProfileUrl(string userId)
        {
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            return util.GetUserProfileLink(Convert.ToInt32( userId), CultureInfo.CurrentCulture.Name,false);
        }

        public string GetPointsNoun(string points)
        {
           int _points = Convert.ToInt32(points);
           if (_points > 4)
               return DotNetNuke.Services.Localization.Localization.GetString("pointsFivePlus.Text", LocalResourceFile);
           else if(_points > 1)
               return DotNetNuke.Services.Localization.Localization.GetString("points.Text", LocalResourceFile);
           else
               return DotNetNuke.Services.Localization.Localization.GetString("point.Text", LocalResourceFile);
        }

        public string GetUserLevelAndName(string points)
        {
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            int userLevel = util.GetLevel(Convert.ToInt32(points));
            return userLevel.ToString() + " <span><i>" + util.GetLevelName(userLevel, LocalResourceFile.Replace("Ourspace_PeopleSearch", "Ourspace_Utilities")) + "</i></span>";
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

        protected void lnkbtn_ContestProgress_Click(object sender, EventArgs e)
        {
            lstvw_ContestantHallOfFame.DataSource = sqldtsrc_ContestantHallOfFame;
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            Session["PeopleSearch_UserNationality"] = util.GetUserNationality(UserId);
            lstvw_ContestantHallOfFame.DataBind();
            grid_allPeople.Visible = false;
            lstvw_ContestantHallOfFame.Visible = true;
            lnkbtn_ContestProgress.CssClass = "tab-active";
            lnkbtn_HallOfFame.CssClass = "tab-inactive";
        }

        protected void lnkbtn_HallOfFame_Click(object sender, EventArgs e)
        {
            grid_allPeople.Visible = true;
            lnkbtn_ContestProgress.CssClass = "tab-inactive";
            lnkbtn_HallOfFame.CssClass = "tab-active";
            lstvw_ContestantHallOfFame.Visible = false;
        }

       

        protected void lnkbtn_testUser_Click(object sender, EventArgs e)
        {
           // AddTestUsers(1);
        }

    }

}
