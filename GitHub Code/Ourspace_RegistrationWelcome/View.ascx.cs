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
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace DotNetNuke.Modules.Ourspace_RegistrationWelcome
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_RegistrationWelcome class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_RegistrationWelcomeModuleBase, IActionable
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
                if (UserId > 0)
                {
                    lblName.Text = UserInfo.FirstName;
                    
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public void AuthenticateUser()
        {
            //int userId = -1;
            //if (type == Sns.DNN)
            //{

            //}
            //else if (type == Sns.Facebook)
            //{
            // access_token
            // prota koita an einai idi ston pinaka ourspace_forum_user_info
            // graph.api.facebook pairneis to facebookid tou
            // koita an ston idio pinaka iparxei facebookId tou xristi
            // select * users, kane


            //string username = "johni_blabla_123456";
            string username = UserInfo.Username;
            string[] parts = username.Split('_');
            int length = parts.Length;
            string possibleFacebookId = parts[length - 1];
            long facebookId = 0;
            bool isInt = long.TryParse(possibleFacebookId, out facebookId);

            if (isInt && facebookId > 99999)
            {
                String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();


                // edw elegxeis an to facebookId tairiazei me to facebookId pou
                // pires kalwntas to graph.api ktl

                // an tairiazoun simainei oti vrikes ton xristi stin vasi opote prostheteis to facebookId ston pinaka
                // forum_user_info, vazeis kai to access_token sto sessionToken ston idio pinaka.
                //  ;// userId


                using (var sqlConn = new SqlConnection(connectionString))
                {
                    sqlConn.Open();
                    string sql = "";
                    string sqlCheck = string.Format(@"SELECT * FROM Ourspace_Forum_User_Info WHERE UserId ={0}", UserId);
                    using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, sqlConn))
                    {
                        cmdCheck.CommandType = CommandType.Text;
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            sql = string.Format(@"UPDATE Ourspace_Forum_User_Info SET facebookId = {0} WHERE userId = {1}",facebookId, UserId);
                    
                        }
                        else
                        {
                            sql = string.Format(@"INSERT INTO Ourspace_Forum_User_Info VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", UserId, PortalId, 0, 0, 0, 0, 0, "", DateTime.Now, facebookId);
                    
                        }

                        sqlConn.Close();
                    }
                    sqlConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                        sqlConn.Close();
                    }
                    // reader.Close();
                }
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
