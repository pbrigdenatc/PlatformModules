using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Resources;
using DotNetNuke.Modules.Ourspace_Utilities;


namespace DotNetNuke.Modules.HallOfFame
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewHallOfFame class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : HallOfFameModuleBase
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
                // On the Join Discussion page the module is not always visible
                if ((TabId == 62 && Request.QueryString["scope"] == null) || (TabId == 62 && Request.QueryString["scope"].ToString() == "threads") || ((TabId == 62 || TabId == 93 || TabId == 106 || TabId == 171) && Request.QueryString["scope"].ToString() == "threadsearch"))
                {
                    ContainerControl.Visible = false;
                }

                SetTitle(Localization.GetString("HallOfFame.Text", LocalResourceFile));

                weeklyLink.Click += weeklyClicked;
                monthlyLink.Click += monthlyClicked;
                allLink.Click += allClicked;

                getImages("All");

                string language = CultureInfo.CurrentUICulture.Name;
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void SetTitle(string title)
        {

            System.Web.UI.Control ctl = DotNetNuke.Common.Globals.FindControlRecursiveDown(this.ContainerControl, "lblTitle");
            if ((ctl != null))
            {
                ((Label)ctl).Text = title;
            }
        }

        private static String selectAllSql =
            @"SELECT TOP 5 [Ourspace_Forum_User_Info].[UserID], [Ourspace_Forum_User_Info].[points], [Users].[DisplayName] 
              FROM [Ourspace_Forum_User_Info]
              INNER JOIN [Users] ON [Ourspace_Forum_User_Info].[UserID] = [Users].[UserID]
              WHERE  [Ourspace_Forum_User_Info].[points] IS NOT NULL
              ORDER BY [Ourspace_Forum_User_Info].[points] DESC";
        private static String selectWeeklySql =
            @"SELECT TOP 5 [Ourspace_Forum_User_Info].[UserID], [Ourspace_Forum_User_Info].[pointsWeekly], [Users].[DisplayName]
             FROM [Ourspace_Forum_User_Info]
             INNER JOIN [Users] ON [Ourspace_Forum_User_Info].[UserID] = [Users].[UserID]
             WHERE  [Ourspace_Forum_User_Info].[pointsWeekly] IS NOT NULL
             ORDER BY [Ourspace_Forum_User_Info].[pointsWeekly] DESC";
        private static String selectMonthlySql =
            @"SELECT TOP 5 [Ourspace_Forum_User_Info].[UserID], [Ourspace_Forum_User_Info].[pointsMonthly], [Users].[DisplayName]
              FROM [Ourspace_Forum_User_Info]
              INNER JOIN [Users] ON [Ourspace_Forum_User_Info].[UserID] = [Users].[UserID]
              WHERE  [Ourspace_Forum_User_Info].[pointsMonthly] IS NOT NULL
              ORDER BY [Ourspace_Forum_User_Info].[pointsMonthly] DESC";

        private Dictionary<String, String> sqlMap = new Dictionary<string, string>() { 
            { "All", selectAllSql }, 
            { "Weekly", selectWeeklySql }, 
            { "Monthly", selectMonthlySql } };


        private void getImages(String type)
        {
            Image[] topUserImages = { ImageTopUser1, ImageTopUser2, ImageTopUser3,
                                      ImageTopUser4, ImageTopUser5};

            Label[] topUserLabels = { LabelTopUser1, LabelTopUser2, LabelTopUser3,
                                      LabelTopUser4, LabelTopUser5};

            HyperLink[] topUserHyperlinks = { hprlnk_TopUser1, hprlnk_TopUser2, hprlnk_TopUser3,
                                                hprlnk_TopUser4, hprlnk_TopUser5};
            String sql = sqlMap[type];

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
                        int userId = reader.GetInt32(0);

                        int points = reader.GetInt32(1);

                        String userName = reader.GetString(2);
                        topUserHyperlinks[i].Text = userName;

                        Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                        string lang = CultureInfo.CurrentCulture.ToString();
                        topUserHyperlinks[i].NavigateUrl = util.GetUserProfileLink(userId, lang, isFacebook);



                        //Ourspace_Utilities.View util = new Ourspace_Utilities.View();

                        //topUserImages[i].ImageUrl = getImageUrl(userId);
                        topUserImages[i].ImageUrl = util.GetOurSpaceUserImgUrl(Server, userId);
                        topUserImages[i].ToolTip = userName;
                        string pointsLbl = Localization.GetString("points", LocalResourceFile);
                        

                        // Retrieves String and Image resources.
                        //string myString = myManager.GetString("StringResource");

                        //ResourceManager rm = new ResourceManager(Type.DefaultBinder);
                        //string pointsLbl = ResourceManager.GetString("points");

                        topUserLabels[i].Text = points.ToString() +
                                        ((points == 1) ? " point" : " "+pointsLbl);
                        i++;

                    }
                    reader.Close();
                }
                sqlConn.Close();
            }
        }

        void weeklyClicked(object sender, EventArgs e)
        {
            weeklyLink.CssClass = "selected";
            monthlyLink.CssClass = "";
            allLink.CssClass = "";

            getImages("Weekly");
        }

        void monthlyClicked(object sender, EventArgs e)
        {
            weeklyLink.CssClass = "";
            monthlyLink.CssClass = "selected";
            allLink.CssClass = "";

            getImages("Monthly");
        }

        void allClicked(object sender, EventArgs e)
        {
            weeklyLink.CssClass = "";
            monthlyLink.CssClass = "";
            allLink.CssClass = "selected";

            getImages("All");
        }

        private String getImageUrl(int userId)
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

    }

}
