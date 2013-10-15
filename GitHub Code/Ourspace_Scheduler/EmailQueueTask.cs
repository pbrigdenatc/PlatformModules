using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DotNetNuke.Modules.Ourspace_Scheduler
{
    public class EmailQueueTask
    {
        private String getEmailsToSendSql = "SELECT * FROM Ourspace_ForumEmailQueue WHERE Sent = 0";
        
       

       

        public bool SendPendingEmails()
        {
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

                using (var sqlConn = new SqlConnection(connectionString))
                {
                    sqlConn.Open();
                    string sql = getEmailsToSendSql;
                    using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                    {
                        cmd.CommandType = CommandType.Text;

                        SqlDataReader reader = cmd.ExecuteReader();
                        string emailId = "-1"; 
                        while (reader.Read())
                        {
                            emailId = reader["EmailId"].ToString();
                            DotNetNuke.Services.Mail.Mail.SendEmail("info@ep-ourspace.eu", reader["To"].ToString(), reader["Subject"].ToString(), reader["EmailHtml"].ToString());
                            String setEmailAsSent = "UPDATE Ourspace_ForumEmailQueue SET Sent = 1 WHERE EmailId = " + emailId;

                            SqlConnection sqlConn2 = new SqlConnection(connectionString);
                            sqlConn2.Open();
                            using (SqlCommand cmdUpdateSentEmails = new SqlCommand(setEmailAsSent, sqlConn2))
                            {
                                cmdUpdateSentEmails.CommandType = CommandType.Text;
                                cmdUpdateSentEmails.ExecuteNonQuery();
                            }
                            sqlConn2.Close();
                        }
                        reader.Close();
                        

                    }
                    sqlConn.Close();
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
                return false;
            }
                  
        }
    }
}
