using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;
using DotNetNuke.Entities.Users;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using DotNetNuke.Common;

namespace DotNetNuke.Modules.OurspaceNotifications
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspaceNotifications class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : OurspaceNotificationsModuleBase, DotNetNuke.Entities.Modules.IActionable
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


        class Notification
        {
            public String Creator { get; set; }
            public String ActionType { get; set; }
            public String PostSubject { get; set; }
            public String PostUrl { get; set; }
        }

        private const int NOTIFICATIONS_MAX_SIZE = 2;

        private string sql = 
@"SELECT [OurSpace].[dbo].[Users].[DisplayName] Sender,
	[OurSpace].[dbo].[Ourspace_Notifications].[Type],
	[OurSpace].[dbo].[Forum_Posts].[Subject],
	[OurSpace].[dbo].[Forum_Posts].[PostID],
	[OurSpace].[dbo].[Forum_Threads].[ForumID],
	[OurSpace].[dbo].[Ourspace_Notifications].[Date]
  FROM [OurSpace].[dbo].[Ourspace_Notifications]
  INNER JOIN [OurSpace].[dbo].[Users] ON [OurSpace].[dbo].[Users].[UserID] = [OurSpace].[dbo].[Ourspace_Notifications].[Creator]
  INNER JOIN [OurSpace].[dbo].[Forum_Posts] ON [OurSpace].[dbo].[Forum_Posts].[PostID] = [OurSpace].[dbo].[Ourspace_Notifications].[PostId]
  INNER JOIN [OurSpace].[dbo].[Forum_Threads] ON [OurSpace].[dbo].[Forum_Threads].[ThreadID] = [OurSpace].[dbo].[Forum_Posts].[ThreadID]
  WHERE [Recipient] = @Recipient
  ORDER BY [OurSpace].[dbo].[Ourspace_Notifications].[Date] DESC";

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                UserInfo currentUserInfo = UserController.GetCurrentUserInfo();
                if (currentUserInfo != null)
                {
                    String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

                    using (var sqlConn = new SqlConnection(connectionString))
                    {
                        sqlConn.Open();

                        using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;
                            SqlParameter recipientParam = new SqlParameter("@Recipient", SqlDbType.Int);
                            recipientParam.Value = currentUserInfo.UserID;
                            cmd.Parameters.Add(recipientParam);
                            cmd.Prepare();
                            SqlDataReader reader = cmd.ExecuteReader();
                            var notifications = new List<Notification>();
                            int i = 0;
                            while (reader.Read())
                            {
                                var not = new Notification();
                                not.Creator = reader.GetString(0);
                                switch (reader.GetString(1))
                                {
                                    case "Thumbs up":
                                        not.ActionType = "\"thumbed up\"";
                                        break;
                                    case "Thumbs down":
                                        not.ActionType = "\"thumbed down\"";
                                        break;
                                    case "Reply":
                                        not.ActionType = "replied";
                                        break;
                                }
                                not.PostUrl = GetForumPostUrl(reader.GetInt32(4), reader.GetInt32(3));
                                not.PostSubject = reader.GetString(2);
                                notifications.Add(not);
                                i++;
                                if (i >= NOTIFICATIONS_MAX_SIZE)
                                {
                                    break;
                                }
                            }
                            reader.Close();

                            NotificationsRepeater.DataSource = notifications;
                            NotificationsRepeater.DataBind();

                            if (notifications.Count > 0)
                            {
                                SetTitle(notifications.Count);
                            }
                        }

                        sqlConn.Close();
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void SetTitle(int notifications)
        {
            Control ctl = DotNetNuke.Common.Globals.FindControlRecursiveDown(this.ContainerControl, "lblTitle");
            if ((ctl != null))
            {
                ((Label)ctl).Text += String.Format(" ({0})", notifications);
            }
        }

        private String GetForumPostUrl(int forumId, int postId)
        {
            String url = "";
            String [] urlParams = {"forumid=" + forumId.ToString(), 
                                   "postid=" + postId.ToString(), 
                                   "scope=posts"};
            url = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", urlParams);
            url = url + "#" + postId.ToString();
            return url;
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

        protected void NotificationsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (NotificationsRepeater.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                    lblFooter.Visible = true;
                }
            }
        }

    }

}
