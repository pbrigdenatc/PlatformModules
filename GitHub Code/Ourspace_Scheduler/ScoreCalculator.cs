using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DotNetNuke.Modules.Ourspace_Scheduler
{
    public class ScoreCalculator
    {
       private static string dateTimeLastWeek = DateTime.Now.AddDays(-7).ToString("MM/dd/yyyy");
       private static string dateTimeLastMonth = DateTime.Now.AddDays(-30).ToString("MM/dd/yyyy");
        // Rule 1: User receives 1 point for each post he/she rates
        private String rule1Sql =
            @"SELECT [UserID], COUNT([UserID])
                FROM [Ourspace_Forum_Post_Thumbs_Log]
                GROUP BY ([UserID])";
      private  String rule1SqlWeek = "SELECT [UserID], COUNT([UserID])   FROM [Ourspace_Forum_Post_Thumbs_Log] where [Ourspace_Forum_Post_Thumbs_Log].Date > '"+ dateTimeLastWeek +"' GROUP BY ([UserID])";
      private String rule1SqlMonth = "SELECT [UserID], COUNT([UserID])   FROM [Ourspace_Forum_Post_Thumbs_Log] where [Ourspace_Forum_Post_Thumbs_Log].Date > '" + dateTimeLastMonth + "' GROUP BY ([UserID])";
      
        // Rule 3: User receives 2 points for each thumbs up in his/her post
        // Rule 4: User loses 1 point for each thumbs down in his/her post
        private String rule3rule4Sql =
            @"SELECT [Forum_Posts].[UserID], Sum((-1 + 3*[Type]))
                FROM [Ourspace_Forum_Post_Thumbs_Log] 
                INNER JOIN [Forum_Posts] ON [Forum_Posts].[PostID] = [Ourspace_Forum_Post_Thumbs_Log].[PostID]
                GROUP BY [Forum_Posts].[UserID]";
        private String rule3rule4SqlWeek =
            @"SELECT [Forum_Posts].[UserID], Sum((-1 + 3*[Type]))   FROM [Ourspace_Forum_Post_Thumbs_Log]    INNER JOIN [Forum_Posts] ON [Forum_Posts].[PostID] = [Ourspace_Forum_Post_Thumbs_Log].[PostID]   where [Ourspace_Forum_Post_Thumbs_Log].Date > '" + dateTimeLastWeek + "'   GROUP BY [Forum_Posts].[UserID]";
        private String rule3rule4SqlMonth =
            @"SELECT [Forum_Posts].[UserID], Sum((-1 + 3*[Type]))   FROM [Ourspace_Forum_Post_Thumbs_Log]    INNER JOIN [Forum_Posts] ON [Forum_Posts].[PostID] = [Ourspace_Forum_Post_Thumbs_Log].[PostID]   where [Ourspace_Forum_Post_Thumbs_Log].Date > '" + dateTimeLastMonth + "'   GROUP BY [Forum_Posts].[UserID]";

        //where [Ourspace_Forum_Post_Thumbs_Log].Date > '8/31/2012'
        // Rule 2: User receives 20 points for every post he/she creates
        private String rule2Sql =
            @"SELECT [UserID], COUNT([PostID])*20
                FROM [OurSpace].[dbo].[Forum_Posts]
                GROUP BY [UserID]";
        private String rule2SqlWeek =
            @"SELECT [UserID], COUNT([PostID])*20   FROM [OurSpace].[dbo].[Forum_Posts] where [Forum_Posts].CreatedDate > '" + dateTimeLastWeek + "' GROUP BY [UserID]";
        private String rule2SqlMonth =
            @"SELECT [UserID], COUNT([PostID])*20   FROM [OurSpace].[dbo].[Forum_Posts] where [Forum_Posts].CreatedDate > '" + dateTimeLastMonth + "' GROUP BY [UserID]";


        // Bonus points: For each post with 20+ thumbs up user receives 200 points
        private String bonusSql =
            @"SELECT Forum_Posts.UserID Creator
                     ,(CASE WHEN Sum(1*[Type]) >= 20 THEN 200 ELSE 0 END) Score
              FROM [Ourspace_Forum_Post_Thumbs_Log] 
              INNER JOIN [Forum_Posts] ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs_Log.PostID
              GROUP BY Forum_Posts.UserID
              HAVING Sum(1*[Type]) >= 20";

        private String resetUserPointsSql =
            @"UPDATE [Ourspace_Forum_User_Info]
               SET [points] = 0, [pointsWeekly] = 0, pointsMonthly = 0";

        private String updateUserInfoSql =
            @"UPDATE [Ourspace_Forum_User_Info]
               SET [points] = @Points
               WHERE [userId] = @UserId";

        private String updateUserInfoSqlWeek =
           @"UPDATE [Ourspace_Forum_User_Info]
               SET [pointsWeekly] = @Points
               WHERE [userId] = @UserId";

        private String updateUserInfoSqlMonth =
           @"UPDATE [Ourspace_Forum_User_Info]
               SET [pointsMonthly] = @Points
               WHERE [userId] = @UserId";

        private Dictionary<int, int> userPoints;

        private void AddPoints(int user, int points)
        {
            if (!userPoints.ContainsKey(user))
            {
                userPoints.Add(user, 0);
            }
            userPoints[user] += points;
        }

        public bool Calculate()
        {
            try
            {
                ResetAllUserPoints();
                CalculateAll();
                userPoints.Clear();
                CalculateMonth();
                userPoints.Clear();
                CalculateWeek();

                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }


        public bool ResetAllUserPoints()
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();
                using (var sqlConn = new SqlConnection(connectionString))
                {
                    sqlConn.Open();
                    using (SqlCommand updateCommand = new SqlCommand(null, sqlConn))
                    {
                        updateCommand.CommandText = resetUserPointsSql;
                        updateCommand.Prepare();
                        updateCommand.ExecuteNonQuery();
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

        public bool CalculateAll()
        {
            String[] sqls = { rule1Sql, rule3rule4Sql, rule2Sql, bonusSql };
            try
            {
                userPoints = new Dictionary<int, int>();

                String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

                using (var sqlConn = new SqlConnection(connectionString))
                {
                    sqlConn.Open();
                    foreach (String sql in sqls)
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                AddPoints(reader.GetInt32(0), reader.GetInt32(1));
                            }
                            reader.Close();
                        }
                    }

                    foreach (var user in userPoints.Keys)
                    {
                        using (SqlCommand updateCommand = new SqlCommand(null, sqlConn))
                        {
                            SqlParameter userParam = new SqlParameter("@UserId", SqlDbType.Int);
                            userParam.Value = user;
                            SqlParameter pointsParam = new SqlParameter("@Points", SqlDbType.Int);
                            pointsParam.Value = userPoints[user];
                            updateCommand.CommandText = updateUserInfoSql;
                            updateCommand.Parameters.Add(userParam);
                            updateCommand.Parameters.Add(pointsParam);

                            updateCommand.Prepare();
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool CalculateWeek()
        {
            String[] sqls = { rule1SqlWeek, rule3rule4SqlWeek, rule2SqlWeek };
            try
            {
                userPoints = new Dictionary<int, int>();

                String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

                using (var sqlConn = new SqlConnection(connectionString))
                {
                    sqlConn.Open();
                    foreach (String sql in sqls)
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                AddPoints(reader.GetInt32(0), reader.GetInt32(1));
                            }
                            reader.Close();
                        }
                    }

                    foreach (var user in userPoints.Keys)
                    {
                        using (SqlCommand updateCommand = new SqlCommand(null, sqlConn))
                        {
                            SqlParameter userParam = new SqlParameter("@UserId", SqlDbType.Int);
                            userParam.Value = user;
                            SqlParameter pointsParam = new SqlParameter("@Points", SqlDbType.Int);
                            pointsParam.Value = userPoints[user];
                            updateCommand.CommandText = updateUserInfoSqlWeek;
                            updateCommand.Parameters.Add(userParam);
                            updateCommand.Parameters.Add(pointsParam);

                            updateCommand.Prepare();
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool CalculateMonth()
        {
            String[] sqls = { rule1SqlMonth, rule3rule4SqlMonth, rule2SqlMonth, bonusSql };
            try
            {
                userPoints = new Dictionary<int, int>();

                String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

                using (var sqlConn = new SqlConnection(connectionString))
                {
                    sqlConn.Open();
                    foreach (String sql in sqls)
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                AddPoints(reader.GetInt32(0), reader.GetInt32(1));
                            }
                            reader.Close();
                        }
                    }

                    foreach (var user in userPoints.Keys)
                    {
                        using (SqlCommand updateCommand = new SqlCommand(null, sqlConn))
                        {
                            SqlParameter userParam = new SqlParameter("@UserId", SqlDbType.Int);
                            userParam.Value = user;
                            SqlParameter pointsParam = new SqlParameter("@Points", SqlDbType.Int);
                            pointsParam.Value = userPoints[user];
                            updateCommand.CommandText = updateUserInfoSqlMonth;
                            updateCommand.Parameters.Add(userParam);
                            updateCommand.Parameters.Add(pointsParam);

                            updateCommand.Prepare();
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }
    }
}
