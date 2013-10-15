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
using System.Globalization;
using System.Collections.Generic;
using DotNetNuke.Modules.Forum;


namespace DotNetNuke.Modules.Ourspace_TextEditor
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_TextEditor class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_TextEditorModuleBase, IActionable
    {
        //String CONNECTION_STRING = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();
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
            string currentPost = lbl_CurrentPostReply.Text;
            try
            {
               // lbl_CurrentPostReply.Text = "134";
                if (!IsPostBack)
                {

                   



                    lblUserId.Text = UserId.ToString();
                    ctlAttachment.ModuleId = ModuleId;
                    //ctlAttachment.
                    ctlAttachment.LoadInitialView();

                    Random random = new Random();
                    int attachmentsSession = random.Next(800000000, 900000000);
                    Session["ATTACHMENTS_SESSION"] = attachmentsSession;
                    ctlAttachment.PostId = attachmentsSession;
                    
                }
                var ids = ctlAttachment.UserId;
                var ids2 = ctlAttachment.UserInfo;
                var test2 = ctlAttachment.lstAttachmentIDs;
                Page.ClientScript.RegisterClientScriptInclude("textEditor.js", this.TemplateSourceDirectory + "/js/textEditor.js?v=3");
                if (Session["showPostSuccess"] != null)
                {
                    if ((bool)Session["showPostSuccess"] == true)
                    {
                        //Page.ClientScript.RegisterClientScriptInclude("postSuccess.js", this.TemplateSourceDirectory + "/js/postSuccess.js?v=1");
                        Session["showPostSuccess"] = false;
                    }
                }
                if (Session["showProposeSuccess"] != null)
                {
                    if ((bool)Session["showProposeSuccess"] == true)
                    {
                        Page.ClientScript.RegisterClientScriptInclude("proposeSuccess.js", this.TemplateSourceDirectory + "/js/proposeSuccess.js?v=1");
                        Session["showProposeSuccess"] = false;
                    }
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

        String CONNECTION_STRING = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();

        protected void lnkbtnSubmitReply_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserId > -1)
                {
                    string test = ctlAttachment.lstAttachmentIDs;
                    
                    string text = txtEditor.Text;

                    string title = "";
                    Random random = new Random();
                    //Session["ATTACHMENTS_SESSION"] = 
                                
                   
                    int postId = -1;
                    GetThreadTitle(out title, out postId);
                    






                    int newPostId = (int)SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Forum_Post_Add", postId, -1, UserId, "::1", title, txtEditor.Text, false, DateTime.Now, false, PortalId, -1, false, 0, false);
                    // http://localhost/ourspace/OpenDebates/tabid/62/forumid/1/postid/25/scope/posts/language/en-GB/Default.aspx#25
                    Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                    int threadId = util.GetThreadId(newPostId);
                    int forumId = (int)SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Forum_Id_From_Thread_Id", threadId);

                    var added = SqlHelper.ExecuteScalar(CONNECTION_STRING, "Forum_Forum_PostAdded", forumId, threadId, newPostId, UserId, "approve");


                    int attachmentsSession = (int)Session["ATTACHMENTS_SESSION"];
                    AssignAttachmentsToPost(attachmentsSession, newPostId);

                    util.SubscribeUserToThread(threadId, forumId, UserId);

                   

                    //Forum.PostConnector postConnector = new Forum.PostConnector();


                   //List<Entities.Content.Taxonomy.Term> terms = new List<Entities.Content.Taxonomy.Term>();
                    //postConnector.SubmitExternalPost(TabId, 381, 0, UserId, title, txtEditor.Text, forumId, threadId, ctlAttachment.lstAttachmentIDs,  "Ourspace_Texteditor:reply",threadId,terms);
                  // ctlAttachment.

                    //Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                    util.SendEmailToThreadTrackers(threadId, newPostId, CultureInfo.CurrentCulture.ToString());
                    Session["showPostSuccess"] = true;

                    if (Request.QueryString["threadId"] != null)
                    {
                        threadId = int.Parse(Request.QueryString["threadId"]);


                        string[] parameters = new string[3] { "postId=" + newPostId, "forumId=" + forumId, "scope=posts" };
                        string url = DotNetNuke.Common.Globals.NavigateURL(TabId, "", parameters);



                        //Response.Redirect(Request.Url.ToString().Replace("threadid=" + threadId, "postId=" + newPostId) + "#" + newPostId,true);
                        Response.Redirect(url);
                    }
                    else if (Request.QueryString["postId"] != null)
                    {
                        //Response.Redirect(Request.Url.ToString().Replace("postid=" + postId, "postId=" + newPostId) + "#" + newPostId,true);
                        string[] parameters = new string[3] { "postId=" + newPostId, "forumId=" + forumId, "scope=posts" };
                        string url = DotNetNuke.Common.Globals.NavigateURL(TabId, "", parameters);



                        //Response.Redirect(Request.Url.ToString().Replace("threadid=" + threadId, "postId=" + newPostId) + "#" + newPostId,true);
                        Response.Redirect(url);
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        protected void AssignAttachmentsToPost(int attachmentsSession, int postId)
        {
            String CONNECTION_STRING = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString();
            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            string sql = "UPDATE Forum_Attachments SET postId = @newPostId WHERE postId = @attachmentsSession";
            SqlCommand chpass = new SqlCommand(sql, conn);
            //chpass.CommandType = CommandType.Text;
            chpass.Parameters.AddWithValue("@attachmentsSession", attachmentsSession);
            chpass.Parameters.AddWithValue("@newPostId", postId);
            try
            {
                conn.Open();
                chpass.ExecuteNonQuery();
                //errLabel.Text = "Password Sucessfully Changed";
            }
            catch (Exception exp) { //errLabel.Text = exp.message;
            }
            finally { conn.Close(); }
        }

        protected void lnkbtnSubmitProposal_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserId > -1)
                {
                    int postId = -1;
                    string title = "";
                    GetThreadTitle(out title, out postId);
                    title = txtProposalTitle.Text;
                    string body = txtProposalDescription.Text;
                    
                    Ourspace_Utilities.View util = new Ourspace_Utilities.View();

                    int newPostId = (int)SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Forum_Post_Add", postId, -1, UserId, "::1", title, body, false, DateTime.Now, false, PortalId, -1, false, 0, true);
                    int threadId = util.GetThreadId(newPostId);
                    int forumId = (int)SqlHelper.ExecuteScalar(CONNECTION_STRING, "Ourspace_Forum_Id_From_Thread_Id", threadId);

                    var added = SqlHelper.ExecuteScalar(CONNECTION_STRING, "Forum_Forum_PostAdded", forumId, threadId, newPostId, UserId, "approve");
                    
                    //RegisterSolutionProposal(thre;
                    

                    // Sending emails to Thread Trackers (Subscribers)


                    util.SendEmailToThreadTrackers(threadId, newPostId, CultureInfo.CurrentCulture.ToString());
                    //SendEmailToThreadTrackers(threadId, newPostId);
                    //lblShowProposalSubmitted.Text = "yes";
                    Session["showProposeSuccess"] = true;
                    if (Request.QueryString["threadId"] != null)
                    {
                        threadId = int.Parse(Request.QueryString["threadId"]);
                        RegisterSolutionProposal(threadId, newPostId);

                        Response.Redirect(Request.Url.ToString().Replace("threadid=" + threadId, "postId=" + newPostId) + "#" + newPostId);
                    }
                    else if (Request.QueryString["postId"] != null)
                    {
                        RegisterSolutionProposal(threadId, newPostId);
                        Response.Redirect(Request.Url.ToString().Replace("postid=" + postId, "postId=" + newPostId) + "#" + newPostId);
                    }
                }

                if (CultureInfo.CurrentCulture.Name == "en-GB")
                {

             
                    hprlnkTermsAndCons.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(216, "");
        
                }
                else if (CultureInfo.CurrentCulture.Name == "el-GR")
                {

             
                    hprlnkTermsAndCons.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(217, "");
      

                }
                else if (CultureInfo.CurrentCulture.Name == "cs-CZ")
                {


                    hprlnkTermsAndCons.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(218, "");
                }
                else if (CultureInfo.CurrentCulture.Name == "de-AT")
                {

                    hprlnkTermsAndCons.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(219, "");

                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

      
        private void RegisterSolutionProposal(int ThreadId, int postId)
        {
            using (var sqlConn = new SqlConnection(CONNECTION_STRING))
            {
                sqlConn.Open();
                string sql = "INSERT INTO Ourspace_Proposal_Solutions VALUES (@threadId,'',0,0,0,0,0,@postId,0)";
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@threadId", ThreadId));
                    cmd.Parameters.Add(new SqlParameter("@postId", postId));
                    int rows = cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }
        }

        private void GetThreadTitle(out string title, out int postId)
        {
            string sql = "";
            title = "";
            postId = -1;
            if (Request.QueryString["threadId"] != null)
            {
                sql = string.Format(@"SELECT Subject, PostId FROM  Forum_Posts  WHERE        (ThreadID = {0}) AND (ParentPostID = 0)", Request.QueryString["threadId"]);

            }
            else if (Request.QueryString["postId"] != null)
            {
                sql = string.Format(@"SELECT Subject FROM  Forum_Posts  WHERE  PostID = {0}", Request.QueryString["postId"]);
                postId = int.Parse(Request.QueryString["postId"].ToString());
            }
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

                        title = reader["Subject"].ToString();
                        if (Request.QueryString["threadId"] != null)
                        {
                            postId = int.Parse(reader["PostId"].ToString());
                        }
                    }
                }
            }

        }

    }

}
