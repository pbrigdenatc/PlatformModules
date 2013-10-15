using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Services.Scheduling;
using DotNetNuke.Services.Exceptions;
using System.Collections;
using DotNetNuke.Entities.Profile;

namespace DotNetNuke.Modules.Ourspace_Scheduler
{
    public class UserPointUpdater : SchedulerClient
    {
        public UserPointUpdater(ScheduleHistoryItem scheduleHistoryItem)
            : base()
        {
            this.ScheduleHistoryItem = scheduleHistoryItem;
        }

        protected void CopyLanguageToNationalityOfNewUsers()
        {
            string roleName = "Registered Users";
            Security.Roles.RoleController roleCtlr = new Security.Roles.RoleController();
            ArrayList objUserRoles = roleCtlr.GetUsersByRoleName(0, roleName);
            foreach (Entities.Users.UserInfo user in objUserRoles)
            {
                string nat = user.Profile.GetPropertyValue("OurSpaceNationality");
                if (user.Profile.GetPropertyValue("OurSpaceNationality") == null)
                {
                    string lang = user.Profile.GetPropertyValue("PreferredLocale");
                    user.Profile.SetProfileProperty("OurSpaceNationality", lang);
                    ProfileController.UpdateUserProfile(user);
                }

            }
        }

        public override void DoWork()
        {
            try
            {
                var scoreCalculator = new ScoreCalculator();
               bool status = scoreCalculator.Calculate();
                var emailQueueTask = new EmailQueueTask();
                bool emailStatus = emailQueueTask.SendPendingEmails();

                CopyLanguageToNationalityOfNewUsers();


                Ourspace_Utilities.View util = new Ourspace_Utilities.View();
               //string addTestUsersMessage = util.AddTestUsers(1);
                // Adding Test Users


                // Do some work, like download today's currency...
                //int count = 9999;
                //for (int i = 0; i < count; i++)
                //{
                //    string repeat = "ss";
                //}
                // Notify that scheduled succeeded+









               if (status)
               {
                   ScheduleHistoryItem.AddLogNote("User points updated successfully!");
                   ScheduleHistoryItem.Succeeded = true;
               }
               else
               {
                   ScheduleHistoryItem.AddLogNote("Failed to update user points");
                   ScheduleHistoryItem.Succeeded = false;
               }
               
                
            }
            catch (Exception ex)
            {
                // report a failure
                ScheduleHistoryItem.Succeeded = false;

                // log the exception into
                // the scheduler framework
                ScheduleHistoryItem.AddLogNote("EXCEPTION: " + ex.ToString());

                // call the Errored method
                Errored(ref ex);

                // log the exception into the DNN core
                Exceptions.LogException(ex);

            }
        }
    }
}