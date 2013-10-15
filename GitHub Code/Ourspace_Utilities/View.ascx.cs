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
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Reflection;
using DotNetNuke.Common;
using System.Runtime.Serialization;
using System.Net;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using System.Linq;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Users;


namespace DotNetNuke.Modules.Ourspace_Utilities
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_Utilities class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_UtilitiesModuleBase, IActionable
    {
        String CONNECTION_STRING = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();


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

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public string GetOurSpaceUserImgUrl(HttpServerUtility currentWebRequest, int userId)
        {
            // Checking if user is Facebook user
            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = "";
                string sqlCheck = string.Format(@"SELECT * FROM Ourspace_Forum_User_Info WHERE UserId ={0}", userId);
                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, sqlConn))
                {
                    string strPath = currentWebRequest.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + userId.ToString("000") + "\\");

                    cmdCheck.CommandType = CommandType.Text;
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.Read())
                    {

                        // Is facebook user
                        if (reader["facebookId"] != null && reader["facebookId"].ToString() != "" && reader["facebookId"].ToString() != "0")
                        {
                            strPath = "http://graph.facebook.com/" + reader["facebookId"].ToString() + "/picture";
                        }
                        else
                        {
                            if (Directory.Exists(strPath))
                            {
                                strPath = ConfigurationManager.AppSettings["OurspaceDomain"];
                                if (userId <= 9)
                                {
                                    strPath = ConfigurationManager.AppSettings["OurspaceDomain"];
                                    strPath += "/Portals/" + PortalId + "/Users/" + userId.ToString("000") + "/" + userId.ToString("00");
                                }
                                else
                                {
                                    strPath += "/Portals/" + PortalId + "/Users/" + userId.ToString("000") + "/" + userId;
                                }
                                strPath += "/" + userId + "/" + userId + "_50.jpg?" + DateTime.Now.Ticks;
                                return strPath;
                            }
                            else
                            {
                                strPath = ConfigurationManager.AppSettings["OurspaceDomain"];
                                strPath += "/images/no-avatar.png";
                            }
                        }


                        sqlConn.Close();
                        //strPath = Control.ResolveClientUrl("~/images/no-avatar.png");// ResolveUrl("~/images/no-avatar.png");
                        return strPath;


                    }
                    sqlConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                        sqlConn.Close();
                    }
                }
                //



            }
            return "";
        }

        public string GetHighResUserImgUrl(HttpServerUtility currentWebRequest, int userId)
        {
            // Checking if user is Facebook user
            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = "";
                string sqlCheck = string.Format(@"SELECT * FROM Ourspace_Forum_User_Info WHERE UserId ={0}", userId);
                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, sqlConn))
                {
                    string strPath = currentWebRequest.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + userId.ToString("000") + "\\");

                    cmdCheck.CommandType = CommandType.Text;
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.Read())
                    {

                        // Is facebook user
                        if (IsFacebookUser(userId))
                        {
                            strPath = "http://graph.facebook.com/" + reader["facebookId"].ToString() + "/picture?type=large";
                        }
                        else
                        {
                            if (Directory.Exists(strPath))
                            {
                                strPath = ConfigurationManager.AppSettings["OurspaceDomain"];
                                if (userId <= 9)
                                {
                                    strPath = ConfigurationManager.AppSettings["OurspaceDomain"];
                                    strPath += "/Portals/" + PortalId + "/Users/" + userId.ToString("000") + "/" + userId.ToString("00");
                                }
                                else
                                {
                                    strPath += "/Portals/" + PortalId + "/Users/" + userId.ToString("000") + "/" + userId;
                                }
                                strPath += "/" + userId + "/" + userId + "_180.jpg?" + DateTime.Now.Ticks;
                                return strPath;
                            }
                            else
                            {
                                strPath = ConfigurationManager.AppSettings["OurspaceDomain"];
                                strPath += "/images/no-avatar-high.png";
                            }
                        }


                        sqlConn.Close();
                        //strPath = Control.ResolveClientUrl("~/images/no-avatar.png");// ResolveUrl("~/images/no-avatar.png");
                        return strPath;


                    }
                    sqlConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                        sqlConn.Close();
                    }
                }
                //



            }
            return "";
        }

        public bool IsFacebookUser(int userId)
        {

            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = "";
                string sqlCheck = string.Format(@"SELECT * FROM Ourspace_Forum_User_Info WHERE UserId ={0}", userId);
                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, sqlConn))
                {
                    cmdCheck.CommandType = CommandType.Text;
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.Read())
                    {

                        // Is facebook user
                        if (reader["facebookId"] != null && reader["facebookId"].ToString() != "" && reader["facebookId"].ToString() != "0")
                        {
                            sqlConn.Close();
                            return true;
                        }
                        else
                        {
                            sqlConn.Close();
                            return false;
                        }
                    }
                }
            }
            return false;
        }


        //"SELECT * FROM [OurSpace].[dbo].[Ourspace_Forum_User_Info] where referralUserId = "
        public int GetUserReferralCount(int userId)
        {
            int count = 0;
            string sql = string.Format(@"SELECT COUNT(*) AS UserCount FROM Ourspace_Forum_User_Info WHERE referralUserId = {0} and points > 100", userId);

            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                        count = int.Parse(reader["UserCount"].ToString());
                    }
                }
            }
            return count;


        }

        public int GetThreadId(int postId)
        {
            string sql = "";
            int threadId = -1;

            sql = string.Format(@"SELECT ThreadId FROM  Forum_Posts  WHERE  PostID = {0}", postId);

            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {

                        threadId = int.Parse(reader["ThreadId"].ToString());

                    }
                }
            }
            return threadId;

        }

        public int GetForumId(int threadId)
        {
            string sql = "";
            int forumId = -1;

            sql = string.Format(@"SELECT ForumId FROM  Forum_Threads  WHERE  ThreadId = {0}", threadId);

            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {

                        forumId = int.Parse(reader["ForumId"].ToString());

                    }
                }
            }
            return forumId;

        }

        public int GetPhaseId(int threadId)
        {
            string sql = "";
            int phaseId = -1;

            sql = string.Format(@"SELECT phaseId FROM  Ourspace_Forum_Thread_Info  WHERE  threadId = {0}", threadId);

            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {

                        phaseId = int.Parse(reader["phaseId"].ToString());

                    }
                }
            }
            return phaseId;

        }

        public string GetThreadName(int postId)
        {
            string sql = "";
            string threadName = "";
            int threadId = -1;

            sql = string.Format(@"SELECT ThreadId FROM  Forum_Posts  WHERE  PostID = {0}", postId);

            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {

                        threadId = int.Parse(reader["ThreadId"].ToString());

                    }
                }
            }
            sql = string.Format(@"SELECT Subject FROM  Forum_Posts  WHERE  ThreadId = {0} AND ParentPostId = 0", threadId);
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {

                        threadName = reader["Subject"].ToString();
                        break;
                    }
                }
            }

            return threadName;

        }

        public string GetTimeAgo(DateTime date)
        {
            if (DateTime.Compare(DateTime.Now, date) >= 0)
            {
                TimeSpan ts = DateTime.Now.Subtract(date);
                //return string.Format("{0} days, {1} hours, {2} minutes, {3} seconds",
                //  , ts.Hours, ts.Minutes, ts.Seconds);
                string pointsLbl = Localization.GetString("hours", LocalResourceFile);
                string test = Localization.GetString("hoursAgo", LocalResourceFile);
                string test2 = Localization.GetString("daysAgo", LocalResourceFile);
                string resource = ModulePath + LocalResourceFile + "View.ascx.resx";

                if (ts.Days < 1)
                {
                    return ts.Hours + "#hoursAgo.Text";
                }
                else
                {
                    string testing = ts.Days + "#daysAgo.Text";
                    return ts.Days + "#daysAgo.Text";
                }
                return ts.Days.ToString();
            }

            return "0";

        }

        public string GetUserProfileLink(int userId, string language, bool isFacebook)
        {
            int profileTab = 71;
            if (language == "en-GB")
            {
                profileTab = 71;
            }
            string profileLink = "";
            if (isFacebook)
            {
                Dictionary<string, int> tabs = new Dictionary<string, int>();
                tabs.Add("en-GB", 287);
                tabs.Add("el-GR", 288);
                tabs.Add("cs-CZ", 289);
                tabs.Add("de-AT", 290);
                profileTab = tabs[language];
                string[] parameters1 = new string[2];
                parameters1 = new string[2] { "user=" + userId, "facebook=1" };
                profileLink = DotNetNuke.Common.Globals.NavigateURL(profileTab, "", parameters1);//.Replace("language/en-GB", "language/" + language);
          
            }
            else
            {
                string[] parameters2 = new string[1];
                parameters2 = new string[1] { "user=" + userId };
                profileLink = DotNetNuke.Common.Globals.NavigateURL(profileTab, "", parameters2).Replace("language/en-GB", "language/" + language);
            }
            return profileLink;
        }

        public int GetUserPoints(int userId)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            using (var sqlConn = new SqlConnection(connectionString))
            {
                String sql =
        @"SELECT [Ourspace_Forum_User_Info].[points] FROM [Ourspace_Forum_User_Info]
              INNER JOIN [Users] ON [Ourspace_Forum_User_Info].[UserID] = [Users].[UserID]
              WHERE  [Ourspace_Forum_User_Info].[points] IS NOT NULL AND [Users].[UserID] = " + userId;
                sqlConn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                        int points = reader.GetInt32(0);
                        return points;
                    }
                    reader.Close();
                }
                sqlConn.Close();
            }
            return 0;

        }


        public string GetUserNationality(int userId)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            using (var sqlConn = new SqlConnection(connectionString))
            {
                String sql =
        @"SELECT [PropertyValue] FROM [OurSpace].[dbo].[UserProfile] WHERE userId = " + userId + " AND PropertyDefinitionId = 50";
                sqlConn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string points = reader.GetString(0);
                            return points;
                        }
                    }
                    reader.Close();
                }
                sqlConn.Close();
            }
            return "-1";
        }

        /*
        public Object[] GetTempUserDetails()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            using (var sqlConn = new SqlConnection(connectionString))
            {
                String sql =
        @"SELECT TOP 1 * FROM [OurSpace].[dbo].[Ourspace_TempUserCache] WHERE added = 'false'";
                sqlConn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Object[] values = new Object[10];
                        reader.GetValues(values);
                        return values;
                    }
                    reader.Close();
                }
                sqlConn.Close();
            }
            return null;
        }*/

        public void SetTempUserAsAdded(string userId)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = string.Format(@"UPDATE Ourspace_TempUserCache SET added = 'true' WHERE id = {0}", userId);
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }
        }


       /* public string AddTestUsers(int numberOfUsers)
        {
            string toReturn = "";
            try
            {
                Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                Object[] user = util.GetTempUserDetails();
                toReturn += "Adding Test Users..";
                if (user != null)
                {
                    Random rnd = new Random();
                    //user.Read();
                    toReturn += "User not null";
                    DotNetNuke.Entities.Users.UserInfo objUser = new DotNetNuke.Entities.Users.UserInfo();
                    objUser.AffiliateID = Null.NullInteger;
                    objUser.Username = user.GetValue(1).ToString();
                    objUser.FirstName = user.GetValue(2).ToString();
                    //objUser.Profile.fir
                    objUser.LastName = user.GetValue(3).ToString();
                    objUser.DisplayName = objUser.FirstName + " " + objUser.LastName;
                    objUser.Email = user.GetValue(4).ToString();
                    objUser.Membership.Password = user.GetValue(8).ToString() + rnd.Next(1000, 9999);

                    //string ageRange = user["agerange"].ToString();
                    //string language = user["language"].ToString();
                    string id = user.GetValue(0).ToString();
                    // objUser.Username = user["foundFrom"].ToString();
                    objUser.PortalID = 0;
                    objUser.IsSuperUser = false;

                    toReturn += "User first name = " + user.GetValue(2).ToString();

                    // = txtPassword.Text;
                    //please check in web.config requirements for your password (length, letters, etc)
                    objUser.Membership.Approved = true;
                    objUser.Membership.UpdatePassword = false;
                    //this one needs if you want user to update password on first login, else set to false
                    objUser.Profile.InitialiseProfile(0);
                    objUser.Profile.Country = "";
                    objUser.Profile.Street = "";
                    objUser.Profile.City = "";
                    objUser.Profile.Region = "";


                    int lang = rnd.Next(1, 4);
                    int ageRangeInt = rnd.Next(1, 4);
                    string language = "";
                    if (lang == 1)
                    {
                        language = "en-GB";
                    }
                    else if (lang == 2)
                    {
                        language = "el-GR";
                    }
                    else if (lang == 3)
                    {
                        language = "de-AT";
                    }
                    else if (lang == 4)
                    {
                        language = "cs-CZ";
                    }



                    objUser.Profile.PreferredLocale = language;
                    objUser.Profile.PostalCode = "";
                    objUser.Profile.Unit = "";
                    objUser.Profile.Telephone = "";
                    objUser.Profile.FirstName = user.GetValue(2).ToString();
                    objUser.Profile.LastName = user.GetValue(3).ToString();

                    DotNetNuke.Security.Membership.UserCreateStatus objCreateStatus =
                    DotNetNuke.Entities.Users.UserController.CreateUser(ref objUser);

                    if (objCreateStatus == DotNetNuke.Security.Membership.UserCreateStatus.Success)
                    {

                        toReturn += "User create statud = success";

                        UserInfo newUser = UserController.GetUserById(0, objUser.UserID);
                        // 48
                        newUser.Profile.SetProfileProperty("AgeRange", ageRangeInt.ToString());
                        //newUser.Profile.SetProfileProperty("First Name", user.GetValue(2).ToString());
                        //newUser.Profile.SetProfileProperty("Last Name", user.GetValue(3).ToString());
                        // 49
                        newUser.Profile.SetProfileProperty("HeardFrom", "Other");

                        UserController.UpdateUser(0, newUser);



                        // Authenticating user
                        toReturn += "Authenticating user with username = " + (user.GetValue(1).ToString() + " userid = " +objUser.UserID) ;
                        AuthenticateUser(user.GetValue(1).ToString(), objUser.UserID);

                        util.SetTempUserAsAdded(id);
                        return toReturn;
                    }
                    else if (objCreateStatus == DotNetNuke.Security.Membership.UserCreateStatus.UsernameAlreadyExists)
                    {
                        util.SetTempUserAsAdded(id);
                        return toReturn;
                    }
                    else
                    {
                        toReturn += "User create status = " + objCreateStatus.ToString();
                        return toReturn;
                    }

                }
                else
                {
                    return toReturn;
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
           
        }

        */


        public long AuthenticateUser(string usernamePar, int userId)
        {
            string username = usernamePar;
            string[] parts = username.Split('_');
            int length = parts.Length;
            string possibleFacebookId = parts[length - 1];
            long facebookId = 0;
            bool isInt = long.TryParse(possibleFacebookId, out facebookId);


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
                string sqlCheck = string.Format(@"SELECT * FROM Ourspace_Forum_User_Info WHERE UserId ={0}", userId);
                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, sqlConn))
                {
                    cmdCheck.CommandType = CommandType.Text;
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {



                    }
                    else
                    {
                        //if (reader["referralUserId"] != null)
                        // {
                        //    string test = reader["referralUserId"].ToString();
                        // }
                        sql = string.Format(@"INSERT INTO Ourspace_Forum_User_Info VALUES ({0},{1},{2},{3},{4},{5},{6},{7},'{8}',{9},{10})", userId, 0, 0, 0, 0, 0, 0, "''", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"), facebookId, -1);

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
            //}
            return facebookId;
        }

        public int GetLevel(int points)
        {
            // Level 1 > 10 points
            // Level 2 > 80 points
            // Level 3 > 300 points
            // Level 4 > 600 points
            // Level 5 > 1000 points
            if (points > 1000)
                return 5;
            else if (points > 500)
                return 4;
            else if (points > 200)
                return 3;
            else if (points > 10)
                return 2;
            else
                return 1;
        }

        public string GetLevelName(int level, string localResourceFile)
        {
            string resource = this.LocalResourceFile;
            string level1 = Localization.GetString("level1Name", localResourceFile);
            string level12 = Localization.GetString("level1Name.Text", localResourceFile);
            return Localization.GetString("level" + level + "Name", localResourceFile);
        }

        public int GetPointsToNextLevel(int points)
        {
            int nextLevelPoints = 0;
            int currentLevel = GetLevel(points);

            switch (currentLevel)
            {
                case 1:
                    nextLevelPoints = 10;
                    break;
                case 2:
                    nextLevelPoints = 200;
                    break;
                case 3:
                    nextLevelPoints = 500;
                    break;
                case 4:
                    nextLevelPoints = 1000;
                    break;
                default:
                    return -1;
                    break;
            }

            return nextLevelPoints - points;
        }


        public List<string> GetImagesInHTMLString(string htmlString)
        {
            List<string> images = new List<string>();
            string pattern = @"<(img)\b[^>]*>";

            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(htmlString);

            for (int i = 0, l = matches.Count; i < l; i++)
            {
                images.Add(matches[i].Value);
            }

            return images;
        }

        public string GetTrimmedBody(HttpServerUtility currentWebRequest, int length, string body)
        {
            //body = Server.UrlDecode(body);
            body = currentWebRequest.HtmlDecode(body);

            body = Regex.Replace(body, @"<[^>]*>", String.Empty);

            if (body.Length >= length)
            {
                return body.Substring(0, length - 10) + "...";
            }
            else
            {
                return body;
            }


        }

        public string GetPostUrl(int forumId, int postId)
        {
            string url;
            String[] urlParams = {"forumid=" + forumId.ToString(), 
                                   "postid=" + postId.ToString(), 
                                   "scope=posts"};
            url = Globals.NavigateURL(62, "", urlParams);
            url = url + "#" + postId.ToString();
            return url;
        }

        public string ReplaceQueryString(string url, string key, string value)
        {
            return Regex.Replace(
                url,
                @"([?&]" + key + ")=[^?&]+",
                "$1=" + value);
        }

        public void SendEmailToThreadTrackers(int threadId, int postId, string lang)
        {
            // The HTML template of the email that notifies the user that a post has been posted in a 
            // thread he has subscribed in
            string htmlTemplate = "Hi [FIRSTNAME],<br><br>Someone has posted a new message in a discussion you are following on Ourspace. Follow the link below to see the new post: <br><br>[URL]";
            string htmlTemplateDeAt = "Hi [FIRSTNAME],<br><br>jemand hat eine neue Nachricht in einer Diskussion auf OurSpace geschrieben, der du folgst. Klick auf den folgenden Link, um diese Nachricht zu sehen: <br><br>[URL]";
            string htmlTemplatElGr = "Γεια σου [FIRSTNAME],<br><br>κάποιος έχει αναρτήσει ένα νέο μήνυμα σε μία συζήτηση του Ourspace που παρακολουθείς. Πάτησε πάνω στον παρακάτω σύνδεσμο για να δεις την καινούργια ανάρτηση: <br><br>[URL]";
            string htmlTemplateCsCz = "Ahoj [FIRSTNAME],<br><br>někdo nově reagoval na diskusi, které jste se účastnil na OurSpace. Klikněte sem, pokud chcete na diskusi přejít:<br><br>[URL]";






            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            string subject = "OurSpace - New post";
            string subjectDeAt = "OurSpace - Neuer Beitrag";
            string url = "";
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = "SELECT     Forum_TrackedThreads.ForumID, Forum_TrackedThreads.ThreadID, Forum_TrackedThreads.UserID, Forum_TrackedThreads.CreatedDate, Forum_TrackedThreads.ModuleID, Users.Email, Users.FirstName, UserProfile.PropertyValue as Language, UserProfile.PropertyDefinitionID FROM   Forum_TrackedThreads INNER JOIN  Users ON Forum_TrackedThreads.UserID = Users.UserID INNER JOIN  UserProfile ON Users.UserID = UserProfile.UserID WHERE     (UserProfile.PropertyDefinitionID = 40) AND (Forum_TrackedThreads.ThreadID = " + threadId + ")";
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    List<string> emails = new List<string>();
                    // Adding the current users email to list prevents him from receiving a notification email
                    // when he posts his message
                    emails.Add(UserInfo.Email);
                    while (reader.Read())
                    {
                        string email = reader["Email"].ToString();
                        if (!emails.Contains(email))
                        {
                            int forumId = Convert.ToInt32(reader["ForumId"]);
                            string name = reader["FirstName"].ToString();
                            string language = reader["Language"].ToString();

                            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                            if (url == "")
                                url = util.GetPostUrl(forumId, postId);
                            //string lang = CultureInfo.CurrentCulture.ToString();

                            string emailMessage = "";
                            string finalSubject = "";
                            if (language == "en-GB")
                            {
                                emailMessage = htmlTemplate.Replace("[FIRSTNAME]", name).Replace("[URL]", url);
                                finalSubject = subject;
                            }
                            else if (language == "de-AT")
                            {
                                emailMessage = htmlTemplateDeAt.Replace("[FIRSTNAME]", name).Replace("[URL]", url);
                                finalSubject = subjectDeAt;
                            }
                            else
                            {
                                emailMessage = htmlTemplate.Replace("[FIRSTNAME]", name).Replace("[URL]", url);
                                finalSubject = subject;
                            }

                            try
                            {


                                //emailTask.EmailQueueTaskAdd("info@ep-ourspace.eu", "OurSpace", 0, emailMessage, emailMessage, finalSubject, PortalID, forumMail.QueuePriority, objConfig.ModuleID, forumMail.EnableFriendlyToName, forumMail.DistroCall, forumMail.DistroIsSproc, forumMail.DistroParams, Date.Now(), False, "")
                                //Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                                util.AddEmailToQueue("info@ourspace.eu", email, finalSubject, emailMessage);

                                //DotNetNuke.Services.Mail.Mail.SendEmail("info@ep-ourspace.eu", email, finalSubject, emailMessage);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    reader.Close();
                }
                sqlConn.Close();
            }
        }

        public bool AddEmailToQueue(string from, string to, string subject, string emailHtml)
        {
            try
            {

                //string sql = "INSERT INTO Ourspace_ForumEmailQueue (From, To, Subject, EmailHtml, DateCreated) VALUES (@from ,@to, @subject, @emailHtml)";
                using (var sqlConn = new SqlConnection(CONNECTION_STRING))
                {
                    sqlConn.Open();
                    //string sql = "INSERT INTO Ourspace_Proposal_Solutions VALUES (@threadId,'',0,0,0,0,0,@postId,0)";
                    string sql = "INSERT INTO Ourspace_ForumEmailQueue ([From], [To], [Subject], [EmailHtml]) VALUES (@from ,@to, @subject, @emailHtml)";

                    using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@from", from));
                        cmd.Parameters.Add(new SqlParameter("@to", to));
                        cmd.Parameters.Add(new SqlParameter("@subject", subject));
                        cmd.Parameters.Add(new SqlParameter("@emailHtml", emailHtml));
                        int rows = cmd.ExecuteNonQuery();
                    }
                    sqlConn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddTranslationErrorLogEntry(string accessToken, string uri, string tokenExpiresIn, string message)
        {
            try
            {

                //string sql = "INSERT INTO Ourspace_ForumEmailQueue (From, To, Subject, EmailHtml, DateCreated) VALUES (@from ,@to, @subject, @emailHtml)";
                using (var sqlConn = new SqlConnection(CONNECTION_STRING))
                {
                    sqlConn.Open();
                    //string sql = "INSERT INTO Ourspace_Proposal_Solutions VALUES (@threadId,'',0,0,0,0,0,@postId,0)";
                    string sql = "INSERT INTO Ourspace_TranslationErrorLog ([accessToken], [uri], [tokenExpiresIn], [message]) VALUES (@accessToken ,@uri, @tokenExpiresIn, @message)";
                    using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@accessToken", accessToken));
                        cmd.Parameters.Add(new SqlParameter("@uri", uri));
                        cmd.Parameters.Add(new SqlParameter("@tokenExpiresIn", tokenExpiresIn));
                        cmd.Parameters.Add(new SqlParameter("@message", message));
                        int rows = cmd.ExecuteNonQuery();
                    }
                    sqlConn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // 

        public void UpdateThreadPhase(int threadId, int phaseId)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = string.Format(@"UPDATE Ourspace_Forum_Thread_Info SET phaseId = {0} WHERE threadId = {1}", phaseId, threadId);
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }
        }

        public void RejectThread(int rejectReasonId, string comment, int threadId)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = string.Format(@"UPDATE Ourspace_Forum_Thread_Info SET rejectReasonId = @rejectReasonId, rejectComment = @rejectComment WHERE threadId = @threadId");
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.Parameters.Add(new SqlParameter("@rejectReasonId", rejectReasonId));
                    cmd.Parameters.Add(new SqlParameter("@rejectComment", comment));
                    cmd.Parameters.Add(new SqlParameter("@threadId", threadId));
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }
        }

        /// <summary>
        /// Localised
        /// </summary>
        /// <param name="threadId"></param>
        public void SendEmailToThreadTrackersAboutTopicRejection(int threadId)
        {
            string threadName = GetThreadName(threadId);

            // The HTML template of the email that notifies the user that a post has been posted in a 
            // thread he has subscribed in
            string htmlTemplate = "Hi [FIRSTNAME],<br><br>The topic '[THREAD_NAME]' has been rejected.<br><br>You can find more information about why it has been rejected here:<br><br>[URL]";
            string htmlTemplateDeAt = "Hi [FIRSTNAME],<br><br>das Thema '[THREAD_NAME]' wurde abgelehnt. Mehr Informationen darüber, warum es abgelehnt wurde, findest du hier:<br><br>[URL]";
            string htmlTemplateElGr = "Γεια σου [FIRSTNAME],<br><br>Το θέμα '[THREAD_NAME]' απορρίφθηκε. Μπορείς να δεις περισσότερες πληροφορίες για την απόρρηψη εδώ:<br><br>[URL]";
            string htmlTemplateCsCz = "Ahoj [FIRSTNAME],<br><br>Téma '[THREAD_NAME]' bylo zrušeno. Zde naleznete více informací, proč se tak stalo:<br><br>[URL]";






            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            string subject = "OurSpace - Topic Rejected";
            string subjectDeAt = "OurSpace - Das Thema wurde abgelehnt";
            string subjectCsCz = "OurSpace - Téma byla zamítnuta";
            string subjectElGr = "OurSpace - Το θέμα απορρίφθηκε";
            string url = "";
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = "SELECT     Forum_TrackedThreads.ForumID, Forum_TrackedThreads.ThreadID, Forum_TrackedThreads.UserID, Forum_TrackedThreads.CreatedDate, Forum_TrackedThreads.ModuleID, Users.Email, Users.FirstName, UserProfile.PropertyValue as Language, UserProfile.PropertyDefinitionID FROM   Forum_TrackedThreads INNER JOIN  Users ON Forum_TrackedThreads.UserID = Users.UserID INNER JOIN  UserProfile ON Users.UserID = UserProfile.UserID WHERE     (UserProfile.PropertyDefinitionID = 40) AND (Forum_TrackedThreads.ThreadID = " + threadId + ")";
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    List<string> emails = new List<string>();

                    while (reader.Read())
                    {
                        string email = reader["Email"].ToString();
                        if (!emails.Contains(email))
                        {
                            int forumId = Convert.ToInt32(reader["ForumId"]);
                            string name = reader["FirstName"].ToString();
                            string language = reader["Language"].ToString();

                            string emailMessage = "";
                            string finalSubject = "";
                            if (language == "en-GB")
                            {
                                url = "http://www.joinourspace.eu/tabid/73/threadid/" + threadId + "/language/" + language + "/scope/posts/Default.aspx";
                                emailMessage = htmlTemplate.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subject;
                            }
                            else if (language == "de-AT")
                            {
                                url = "http://www.joinourspace.eu/tabid/73/threadid/" + threadId + "/language/" + language + "/scope/posts/Default.aspx";
                                emailMessage = htmlTemplateDeAt.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subjectDeAt;
                            }
                            else if (language == "el-GR")
                            {
                                url = "http://www.joinourspace.eu/tabid/73/threadid/" + threadId + "/language/" + language + "/scope/posts/Default.aspx";
                                emailMessage = htmlTemplateElGr.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subjectElGr;
                            }
                            else if (language == "cs-CZ")
                            {
                                url = "http://www.joinourspace.eu/tabid/73/threadid/" + threadId + "/language/" + language + "/scope/posts/Default.aspx";
                                emailMessage = htmlTemplateCsCz.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subjectCsCz;
                            }
                            else
                            {
                                url = "http://www.joinourspace.eu/tabid/73/threadid/" + threadId + "/language/" + language + "/scope/posts/Default.aspx";
                                emailMessage = htmlTemplate.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subject;
                            }

                            try
                            {
                                AddEmailToQueue("info@ourspace.eu", email, finalSubject, emailMessage);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    reader.Close();
                }
                sqlConn.Close();
            }
        }


        public void TransferThreadSubscriptionsToPhase2(int threadId, int newForumId)
        {

            using (var sqlConn = new SqlConnection(CONNECTION_STRING))
            {
                sqlConn.Open();
                string sql = "UPDATE     Forum_TrackedThreads SET ForumId = " + newForumId + ", ModuleId = 381 WHERE threadId = " + threadId + "";
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }
        }

        public void SubscribeUserToThread(int threadId, int forumId, int userId)
        {
            bool hasRows = false;
            using (var sqlConn = new SqlConnection(CONNECTION_STRING))
            {
                sqlConn.Open();
                string sql = "SELECT * FROM Forum_TrackedThreads WHERE ForumId = " + forumId + " AND ModuleId = 381 AND threadId = " + threadId + " AND userId = " + userId;
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    // We update the entry
                    hasRows = reader.HasRows;
                }
                sqlConn.Close();
                if (!hasRows)
                {
                    sqlConn.Open();
                    string sql2 = "INSERT INTO Forum_TrackedThreads (ForumId, ThreadId, UserId, CreatedDate, ModuleId) VALUES (" + forumId + "," + threadId + "," + userId + ", GETDATE(), 381)";

                    using (SqlCommand cmd2 = new SqlCommand(sql2, sqlConn))
                    {
                        cmd2.CommandType = CommandType.Text;
                        cmd2.ExecuteNonQuery();
                    }
                }


            }

        }



        public int GetUserLastPostId(int userId)
        {

            int postId = -1;
            using (var sqlConn = new SqlConnection(CONNECTION_STRING))
            {
                sqlConn.Open();
                string sql = "SELECT     PostID, UserID FROM         Forum_Posts WHERE UserID = " + userId + " ORDER BY postId desc";
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    List<string> emails = new List<string>();

                    if (reader.Read())
                    {
                        postId = Convert.ToInt32(reader["PostId"].ToString());

                    }
                    reader.Close();
                }
                sqlConn.Close();
                return postId;
            }
        }

        public void SendEmailToThreadTrackersAboutMovingToPhase2(int forumId, int threadId)
        {
            string threadName = GetThreadName(threadId);

            // The HTML template of the email that notifies the user that a post has been posted in a 
            // thread he has subscribed in
            string htmlTemplate = "Hi [FIRSTNAME],<br><br>The discussion '[THREAD_NAME]' has moved to the Discussion Phase.<br><br>You can find the discussion in its new phase and start posting yourself here:<br><br>[URL]";
            string htmlTemplateDeAt = "Hi [FIRSTNAME],<br><br>das Thema '[THREAD_NAME]' befindet sich nun in der Diskussionsphase. Diskutiere hier mit:<br><br>[URL]";
            string htmlTemplateElGr = "Γεια σου [FIRSTNAME]<br><br>Η συζήτηση '[THREAD_NAME]' έχει προχωρήσει στο στάδιο των Συζητήσεων. Μπορείς να δεις τη συζήτηση σε αυτή τη φάση αλλά και να αναρτήσεις ο ίδιος εδώ:<br><br>[URL]";
            string htmlTemplateCsCz = "Ahoj [FIRSTNAME],<br><br>téma '[THREAD_NAME]' bylo právě posunuto do fáze 'Diskuse'. Téma nyní najdete zde a můžete začít vkládat své příspěvky:<br><br>[URL]";






            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            string subject = "OurSpace - Topic moved to Discussion Phase";
            string subjectDeAt = "OurSpace - Das Thema bewegt Diskussion Phase";
            string subjectCsCz = "OurSpace - Téma se stěhoval do diskuse fáze";
            string subjectElGr = "OurSpace - Το θέμα μεταφέρθηκε στη φάση Συζήτηση";
            string url = "";
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = "SELECT     Forum_TrackedThreads.ForumID, Forum_TrackedThreads.ThreadID, Forum_TrackedThreads.UserID, Forum_TrackedThreads.CreatedDate, Forum_TrackedThreads.ModuleID, Users.Email, Users.FirstName, UserProfile.PropertyValue as Language, UserProfile.PropertyDefinitionID FROM   Forum_TrackedThreads INNER JOIN  Users ON Forum_TrackedThreads.UserID = Users.UserID INNER JOIN  UserProfile ON Users.UserID = UserProfile.UserID WHERE     (UserProfile.PropertyDefinitionID = 40) AND (Forum_TrackedThreads.ThreadID = " + threadId + ")";
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    List<string> emails = new List<string>();

                    while (reader.Read())
                    {
                        string email = reader["Email"].ToString();
                        if (!emails.Contains(email))
                        {
                            string name = reader["FirstName"].ToString();
                            string language = reader["Language"].ToString();

                            string emailMessage = "";
                            string finalSubject = "";
                            if (language == "en-GB")
                            {
                                url = "http://www.joinourspace.eu/tabid/62/threadid/" + threadId + "/language/" + language + "/forumId/" + forumId + "/scope/posts/Default.aspx";
                                emailMessage = htmlTemplate.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subject;
                            }
                            else if (language == "de-AT")
                            {
                                url = "http://www.joinourspace.eu/tabid/62/threadid/" + threadId + "/language/" + language + "/forumId/" + forumId + "/scope/posts/Default.aspx";
                                emailMessage = htmlTemplateDeAt.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subjectDeAt;
                            }
                            else if (language == "el-GR")
                            {
                                url = "http://www.joinourspace.eu/tabid/62/threadid/" + threadId + "/language/" + language + "/forumId/" + forumId + "/scope/posts/Default.aspx";
                                emailMessage = htmlTemplateElGr.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subjectElGr;
                            }
                            else if (language == "cs-CZ")
                            {
                                url = "http://www.joinourspace.eu/tabid/62/threadid/" + threadId + "/language/" + language + "/forumId/" + forumId + "/scope/posts/Default.aspx";
                                emailMessage = htmlTemplateCsCz.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subjectCsCz;
                            }
                            else
                            {
                                url = "http://www.joinourspace.eu/tabid/62/threadid/" + threadId + "/language/" + language + "/forumId/" + forumId + "/scope/posts/Default.aspx";
                                emailMessage = htmlTemplate.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subject;
                            }

                            try
                            {
                                AddEmailToQueue("info@ourspace.eu", email, finalSubject, emailMessage);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    reader.Close();
                }
                sqlConn.Close();
            }
        }

        public void SendEmailToThreadTrackersAboutMovingToPhase3(int threadId, int postId, string lang)
        {
            string threadName = GetThreadName(postId);

            // The HTML template of the email that notifies the user that a post has been posted in a 
            // thread he has subscribed in
            string htmlTemplate = "Hi [FIRSTNAME],<br><br>The discussion '[THREAD_NAME]' has moved to the Voting Phase.<br><br>You can find the discussion in its current phase here:<br><br>[URL]";
            string htmlTemplateDeAt = "Hi [FIRSTNAME],<br><br>die Diskussion [THREAD_NAME] befindet sich nun in der Abstimmungsphase. Du kannst hier: [URL] über die Lösungsvorschläge der Diskussion abstimmen.<br><br>[URL]";
            string htmlTemplateCsCz = "Ahoj [FIRSTNAME],<br><br>diskuse [THREAD_NAME] byla posunuta do fáze 'Hlasování'. Diskusi nyní najdete zde:<br><br>[URL]";
            string htmlTemplateElGr = "Γεια σου [FIRSTNAME],<br><br>Η συζήτηση '[THREAD_NAME]' έχει προχωρήσει στο στάδιο των Ψηφοφοριών. Μπορείς να δεις τη συζήτηση σε αυτή τη φάση εδώ:<br><br>[URL]";



            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            string subject = "OurSpace - Discussion moved to Voting Phase";
            string subjectDeAt = "OurSpace - Discussion moved to Voting Phase";
            string subjectCsCz = "OurSpace - Téma se stěhoval do fáze Hlasovací";
            string subjectElGr = "OurSpace - Θέμα μετακινήθηκε στη φάση ψηφοφορίας";
            string url = "";
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = "SELECT     Forum_TrackedThreads.ForumID, Forum_TrackedThreads.ThreadID, Forum_TrackedThreads.UserID, Forum_TrackedThreads.CreatedDate, Forum_TrackedThreads.ModuleID, Users.Email, Users.FirstName, UserProfile.PropertyValue as Language, UserProfile.PropertyDefinitionID FROM   Forum_TrackedThreads INNER JOIN  Users ON Forum_TrackedThreads.UserID = Users.UserID INNER JOIN  UserProfile ON Users.UserID = UserProfile.UserID WHERE     (UserProfile.PropertyDefinitionID = 40) AND (Forum_TrackedThreads.ThreadID = " + threadId + ")";
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    List<string> emails = new List<string>();
                    // Adding the current users email to list prevents him from receiving a notification email
                    // when he posts his message
                    //emails.Add(UserInfo.Email);
                    while (reader.Read())
                    {
                        string email = reader["Email"].ToString();
                        if (!emails.Contains(email))
                        {
                            int forumId = Convert.ToInt32(reader["ForumId"]);
                            string name = reader["FirstName"].ToString();
                            string language = reader["Language"].ToString();

                            string emailMessage = "";
                            string finalSubject = "";
                            if (language == "en-GB")
                            {
                                url = "http://www.joinourspace.eu/Solutionproposals/tabid/200/threadid/" + threadId + "/mode/featured/language/" + language + "/Default.aspx";
                                emailMessage = htmlTemplate.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subject;
                            }
                            else if (language == "de-AT")
                            {
                                url = "http://www.joinourspace.eu/Solutionproposals/tabid/200/threadid/" + threadId + "/mode/featured/language/" + language + "/Default.aspx";
                                emailMessage = htmlTemplateDeAt.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subjectDeAt;
                            }
                            else if (language == "cs-CZ")
                            {
                                url = "http://www.joinourspace.eu/Solutionproposals/tabid/200/threadid/" + threadId + "/mode/featured/language/" + language + "/Default.aspx";
                                emailMessage = htmlTemplateCsCz.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subjectCsCz;
                            }
                            else if (language == "el-GR")
                            {
                                url = "http://www.joinourspace.eu/Solutionproposals/tabid/200/threadid/" + threadId + "/mode/featured/language/" + language + "/Default.aspx";
                                emailMessage = htmlTemplateElGr.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subjectElGr;
                            }
                            else
                            {
                                url = "http://www.joinourspace.eu/Solutionproposals/tabid/200/threadid/" + threadId + "/mode/featured/language/" + language + "/Default.aspx";
                                emailMessage = htmlTemplate.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subject;
                            }

                            try
                            {
                                AddEmailToQueue("info@ourspace.eu", email, finalSubject, emailMessage);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    reader.Close();
                }
                sqlConn.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="threadId"></param>
        /// <param name="postId"></param>
        /// <returns>The url of the thread in its new phase</returns>
        public void SendEmailToThreadTrackersAboutMovingToPhase4(int threadId, int postId)
        {
            string threadName = GetThreadName(postId);

            // The HTML template of the email that notifies the user that a post has been posted in a 
            // thread he has subscribed in
            string htmlTemplate = "Hi [FIRSTNAME],<br><br>The discussion '[THREAD_NAME]' has moved to the Results Phase.<br><br>You can find the discussion in its current phase here:<br><br>[URL]";
            string htmlTemplateDeAt = "Hi [FIRSTNAME],<br><br>das Thema '[THREAD_NAME]' befindet sich nun in der Ergebnisphase. Du findest das Thema in seiner neuen Phase hier:<br><br>[URL]";
            string htmlTemplateElGr = "Γεια σου [FIRSTNAME],<br><br>Το Θέμα '[THREAD_NAME]' έχει προχωρήσει στο στάδιο των Αποτελεσμάτων. Μπορείς να βρεις το το θέμα στην καινούρια φάση εδώ:<br><br>[url]";
            string htmlTemplateCsCz = "Ahoj [FIRSTNAME],<br><br>téma '[THREAD_NAME]' bylo právě posunuto do fáze 'Výsledky'. Téma nyní najdete zde:<br><br>[URL]";





            String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

            string subject = "OurSpace - Discussion moved to Results Phase";
            string subjectDeAt = "OurSpace - Discussion moved to Results Phase";
            string subjectElGr = "OurSpace - Το θέμα μεταφέρθηκε στη φάση των αποτελεσμάτων";
            string subjectCsCz = "OurSpace - Téma bylo přesunuto k výsledkům fáze";
            string url = "";
            using (var sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                string sql = "SELECT     Forum_TrackedThreads.ForumID, Forum_TrackedThreads.ThreadID, Forum_TrackedThreads.UserID, Forum_TrackedThreads.CreatedDate, Forum_TrackedThreads.ModuleID, Users.Email, Users.FirstName, UserProfile.PropertyValue as Language, UserProfile.PropertyDefinitionID FROM   Forum_TrackedThreads INNER JOIN  Users ON Forum_TrackedThreads.UserID = Users.UserID INNER JOIN  UserProfile ON Users.UserID = UserProfile.UserID WHERE     (UserProfile.PropertyDefinitionID = 40) AND (Forum_TrackedThreads.ThreadID = " + threadId + ")";
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    List<string> emails = new List<string>();

                    while (reader.Read())
                    {
                        string email = reader["Email"].ToString();
                        if (!emails.Contains(email))
                        {
                            int forumId = Convert.ToInt32(reader["ForumId"]);
                            string name = reader["FirstName"].ToString();
                            string language = reader["Language"].ToString();

                            string emailMessage = "";
                            string finalSubject = "";
                            if (language == "en-GB")
                            {
                                url = "http://www.joinourspace.eu/tabid/196/result/" + threadId + "/language/" + language + "/Default.aspx";
                                emailMessage = htmlTemplate.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subject;
                            }
                            else if (language == "de-AT")
                            {
                                url = "http://www.joinourspace.eu/tabid/196/result/" + threadId + "/language/" + language + "/Default.aspx";
                                emailMessage = htmlTemplateDeAt.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subjectDeAt;
                            }
                            else if (language == "el-GR")
                            {
                                url = "http://www.joinourspace.eu/tabid/196/result/" + threadId + "/language/" + language + "/Default.aspx";
                                emailMessage = htmlTemplateElGr.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subjectElGr;
                            }
                            else if (language == "cs-CZ")
                            {
                                url = "http://www.joinourspace.eu/tabid/196/result/" + threadId + "/language/" + language + "/Default.aspx";
                                emailMessage = htmlTemplateCsCz.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subjectCsCz;
                            }
                            else
                            {
                                url = "http://www.joinourspace.eu/tabid/196/result/" + threadId + "/language/" + language + "/Default.aspx";
                                emailMessage = htmlTemplate.Replace("[FIRSTNAME]", name).Replace("[URL]", url).Replace("[THREAD_NAME]", threadName);
                                finalSubject = subject;
                            }

                            try
                            {
                                AddEmailToQueue("info@ourspace.eu", email, finalSubject, emailMessage);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    reader.Close();
                }
                sqlConn.Close();
            }
        }

        #endregion


        public void RefreshAccessToken(HttpApplicationState app)
        {

            try
            {
                AdmAuthentication admAuth = new AdmAuthentication("joinourspace_eu", "Y/usBFdwOtdOG8md/Ar7XpLW82PmWepVDeWtvHHyUj8=");
                AdmAccessToken admToken = admAuth.GetAccessToken();
                app["BingTranslateAccessToken"] = admToken;
                app["BingTranslateAccessTokenCreatedDateTime"] = DateTime.Now;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from">en</param>
        /// <param name="to">el</param>
        /// <param name="accessToken"></param>
        /// <returns>#NLA# if there was a problem with the service OR the translated text</returns>
        public string TranslateText(HttpApplicationState app, string from, string to, string text)
        {
            string uri = "";
            try
            {
                AdmAccessToken admToken = new AdmAccessToken();
                if (app["BingTranslateAccessToken"] != null)
                {
                    admToken = (AdmAccessToken)app["BingTranslateAccessToken"];
                    int timeLeft = 0;
                    DateTime now = DateTime.Now;
                    DateTime tokenCreatedOn = (DateTime)app["BingTranslateAccessTokenCreatedDateTime"];
                    double difference = (now - tokenCreatedOn).TotalSeconds;

                    if ((now - tokenCreatedOn).TotalSeconds > 580)
                    {
                        RefreshAccessToken(app);
                    }
                }
                else
                {
                    RefreshAccessToken(app);
                }
                admToken = (AdmAccessToken)app["BingTranslateAccessToken"];

                string headerValue;
                //Get Client Id and Client Secret from https://datamarket.azure.com/developer/applications/
                //Refer obtaining AccessToken (http://msdn.microsoft.com/en-us/library/hh454950.aspx) 

                // Create a header with the access_token property of the returned token
                //headerValue = "Bearer " + admToken.access_token;






                uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + System.Web.HttpUtility.UrlEncode(text) + "&from=" + from + "&to=" + to;
                string authToken = "Bearer" + " " + admToken.access_token;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.Headers.Add("Authorization", authToken);

                WebResponse response = null;


                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string translation = (string)dcs.ReadObject(stream);
                    return translation;
                    //Console.WriteLine("Translation for source text '{0}' from {1} to {2} is", text, "en", "de");
                    //Console.WriteLine(translation);
                }
            }
            catch (Exception ex)
            {
                if (app["BingTranslateAccessToken"] != null)
                {
                    AdmAccessToken admToken = (AdmAccessToken)app["BingTranslateAccessToken"];
                    string errorInfo = "Token: " + admToken.access_token + " | Expires in: " + admToken.expires_in + " | Scope: " + admToken.scope + " | Token type: " + admToken.token_type + " | Uri: " + uri;

                    Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                    util.AddTranslationErrorLogEntry(admToken.access_token, uri, admToken.expires_in, ex.Message);
                }
                else
                {
                    Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                    util.AddTranslationErrorLogEntry("no token", uri, "no expires in", ex.Message);
                }
                string error = ex.Message;
                return "#NLA#" + error;
            }

        }

        #region Bing translate
        [DataContract]
        public class AdmAccessToken
        {
            [DataMember]
            public string access_token { get; set; }
            [DataMember]
            public string token_type { get; set; }
            [DataMember]
            public string expires_in { get; set; }
            [DataMember]
            public string scope { get; set; }
        }

        public class AdmAuthentication
        {
            public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
            private string clientId;
            private string cientSecret;
            private string request;

            public AdmAuthentication(string clientId, string clientSecret)
            {
                this.clientId = clientId;
                this.cientSecret = clientSecret;
                //If clientid or client secret has special characters, encode before sending request
                this.request = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
            }

            public AdmAccessToken GetAccessToken()
            {
                return HttpPost(DatamarketAccessUri, this.request);
            }

            private AdmAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
            {
                //Prepare OAuth request 
                WebRequest webRequest = WebRequest.Create(DatamarketAccessUri);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";
                byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
                webRequest.ContentLength = bytes.Length;


                //WebProxy proxyObject = new WebProxy("http://10.1.1.51:8080/");
                // GlobalProxySelection.Select = proxyObject;

                using (Stream outputStream = webRequest.GetRequestStream())
                {
                    outputStream.Write(bytes, 0, bytes.Length);
                }
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
                    //Get deserialized object from JSON stream
                    AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
                    return token;
                }
            }
        }

        public bool userPostedRecently(int userId, int secondsAgo)
        {
            using (var sqlConn = new SqlConnection(CONNECTION_STRING))
            {
                sqlConn.Open();
                var sqlFormattedDate = DateTime.Now.AddSeconds(secondsAgo).ToString("yyyy-MM-dd HH:mm:ss");


                string sql = "SELECT * FROM Forum_Posts WHERE UserId = " + userId + " WHERE CreateDate < " + sqlFormattedDate;
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    return reader.HasRows;
                }
            }
        }

        public string GetTopicCurrentPhaseUrl(bool isFacebook, int threadId, string language)
        {
            string url = "";
            int currentPhase = GetPhaseId(threadId);
            // Suggest
            Dictionary<string, int> suggestTabs = new Dictionary<string, int>();
            suggestTabs.Add("en-GB", 271);
            suggestTabs.Add("el-GR", 272);
            suggestTabs.Add("cs-CZ", 273);
            suggestTabs.Add("de-AT", 274);

            // Join
            Dictionary<string, int> joinTabs = new Dictionary<string, int>();
            joinTabs.Add("en-GB", 259);
            joinTabs.Add("el-GR", 260);
            joinTabs.Add("cs-CZ", 261);
            joinTabs.Add("de-AT", 262);

            // Vote
            Dictionary<string, int> voteTabs = new Dictionary<string, int>();
            voteTabs.Add("en-GB", 279);
            voteTabs.Add("el-GR", 280);
            voteTabs.Add("cs-CZ", 281);
            voteTabs.Add("de-AT", 282);

            // Results


            // Subject link redirects user according to topic phase
            if (currentPhase == 1)
            {
                if (isFacebook)
                {
                    int suggestTab = suggestTabs[language];
                    string[] parameters = new string[3];
                    parameters = new string[3] { "threadid=" + threadId, "scope=posts", "facebook=1" };
                    url = DotNetNuke.Common.Globals.NavigateURL(suggestTab, "", parameters);
                }
                else
                {
                    string[] parameters = new string[2];
                    parameters = new string[2] { "threadid=" + threadId, "scope=posts" };
                    url = DotNetNuke.Common.Globals.NavigateURL(73, "", parameters);
                    url = url.Replace("en-GB", language);
                }

            }
            else if (currentPhase == 2)
            {
                if (isFacebook)
                {

                    int joinTab = joinTabs[language];
                    string[] parameters = new string[3];
                    parameters = new string[3] { "threadid=" + threadId, "scope=posts", "facebook=1" };
                    url = DotNetNuke.Common.Globals.NavigateURL(joinTab, "", parameters);
                }
                else
                {
                    string[] parameters = new string[2];
                    parameters = new string[2] { "threadid=" + threadId, "scope=posts" };
                    url = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters);
                    url = url.Replace("en-GB", language);
                }

            }
            else if (currentPhase == 3)
            {
                if (isFacebook)
                {
                    int voteTab = voteTabs[language];
                    string[] parameters = new string[3];
                    parameters = new string[3] { "threadid=" + threadId, "mode=featured", "facebook=1" };
                    url = DotNetNuke.Common.Globals.NavigateURL(voteTab, "", parameters);
                }
                else
                {
                    string[] parameters = new string[2];
                    parameters = new string[2] { "threadid=" + threadId, "mode=featured" };
                    url = DotNetNuke.Common.Globals.NavigateURL(200, "", parameters);
                    url = url.Replace("en-GB", language);
                }
            }
            else if (currentPhase == 4)
            {
                if (isFacebook)
                {
                    int voteTab = voteTabs[language];
                    string[] parameters = new string[2];
                    parameters = new string[2] { "result=" + threadId, "facebook=1" };
                    url = DotNetNuke.Common.Globals.NavigateURL(voteTab, "", parameters);
                }
                else
                {
                    string[] parameters = new string[1];
                    parameters = new string[1] { "result=" + threadId };

                    url = DotNetNuke.Common.Globals.NavigateURL(196, "", parameters);
                    url = url.Replace("en-GB", language);
                }

            }
            return url;
        }

        #endregion

        #region
        public class LocationInfo
        {
            public float Latitude { get; set; }
            public float Longitude { get; set; }
            public string CountryName { get; set; }
            public string CountryCode { get; set; }
            public string Name { get; set; }
        }

        public class GeoLocationService
        {
            public static LocationInfo GetLocationInfo()
            {
                //TODO: How/where do we refactor this and tidy up the use of Context? This isn't testable.
                string ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                LocationInfo v = new LocationInfo();

                if (ipaddress != "127.0.0.1")
                    v = GeoLocationService.GetLocationInfo(ipaddress);
                else //debug locally
                    v = new LocationInfo()
                    {
                        Name = "Sugar Grove, IL",
                        CountryCode = "US",
                        CountryName = "UNITED STATES",
                        Latitude = 41.7696F,
                        Longitude = -88.4588F
                    };
                return v;
            }

            private static Dictionary<string, LocationInfo> cachedIps = new Dictionary<string, LocationInfo>();

            public static LocationInfo GetLocationInfo(string ipParam)
            {
                try
                {
                    LocationInfo result = null;
                    IPAddress i = System.Net.IPAddress.Parse(ipParam);
                    string ip = i.ToString();
                    if (!cachedIps.ContainsKey(ip))
                    {

                        string r;
                        using (var w = new WebClient())
                        {
                            //var p = new WebProxy("10.1.1.51:8080", false);
                            //WebRequest.DefaultWebProxy = p;
                            //w.Proxy = p;
                            r = w.DownloadString(String.Format("http://api.hostip.info/?ip={0}&position=true", ip));
                        }

                        /*
                     string r =
                        @"<?xml version=""1.0"" encoding=""ISO-8859-1"" ?>
        <HostipLookupResultSet version=""1.0.0"" xmlns=""http://www.hostip.info/api"" xmlns:gml=""http://www.opengis.net/gml"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.hostip.info/api/hostip-1.0.0.xsd"">
         <gml:description>This is the Hostip Lookup Service</gml:description>
         <gml:name>hostip</gml:name>
         <gml:boundedBy>
            <gml:Null>inapplicable</gml:Null>
         </gml:boundedBy>
         <gml:featureMember>
            <Hostip>
             <gml:name>Sugar Grove, IL</gml:name>
             <countryName>UNITED STATES</countryName>
             <countryAbbrev>US</countryAbbrev>
             <!-- Co-ordinates are available as lng,lat -->
             <ipLocation>
                <gml:PointProperty>
                 <gml:Point srsName=""http://www.opengis.net/gml/srs/epsg.xml#4326"">
                    <gml:coordinates>-88.4588,41.7696</gml:coordinates>
                 </gml:Point>
                </gml:PointProperty>
             </ipLocation>
            </Hostip>
         </gml:featureMember>
        </HostipLookupResultSet>";
                     */

                        var xmlResponse = XDocument.Parse(r);
                        var gml = (XNamespace)"http://www.opengis.net/gml";
                        var ns = (XNamespace)"http://www.hostip.info/api";




                        var names = ns + "Hostip";
                        result = (from x in xmlResponse.Descendants("Hostip")
                                  select new LocationInfo
                                  {
                                      CountryCode = x.Element(/*ns + */"countryAbbrev").Value,
                                      CountryName = x.Element(/*ns + */"countryName").Value,
                                      //Latitude = float.Parse(x.Descendants(gml + "coordinates").Single().Value.Split(',')[0]),
                                      //Longitude = float.Parse(x.Descendants(gml + "coordinates").Single().Value.Split(',')[1]),
                                      Name = x.Element(gml + "name").Value
                                  }).SingleOrDefault();

                        if (result != null)
                        {
                            cachedIps.Add(ip, result);
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        result = cachedIps[ip];
                        return result;
                    }
                }
                catch (NullReferenceException)
                {
                    //Looks like we didn't get what we expected.
                    return null;
                }
                catch (FormatException ex)
                {
                    // Invalid format
                    return null;
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
