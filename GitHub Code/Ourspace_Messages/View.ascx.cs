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
using System.Web.UI.WebControls;
using System.IO;


namespace DotNetNuke.Modules.Ourspace_Messages
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_Messages class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_MessagesModuleBase, IActionable
    {

        #region Event Handlers
        int MESSAGES_TAB_ID = 71;
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
                hdnfld_profileUserID.Value = UserId.ToString();

                if (Request.QueryString["conversation"] == null)
                {
                    lstvw_messages.Visible = true;
                    hdnfld_ConversationWithUserID.Value = "";

                }
                else
                {
                    txt_reply.Visible = true;
                    lnkbtn_reply.Visible = true;
                    hdnfld_conversationID.Value = (Convert.ToInt32(Request.QueryString["conversation"].ToString())).ToString();
                    lnkbtn_BackToInbox.Visible = true;
                    lstvw_conversation.Visible = true;
                    MarkConversationMessagesAsRead(hdnfld_conversationID.Value);
                }

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void lstvw_messages_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            
            Label ConversationIDLabel = (Label)e.Item.FindControl("ConversationIDLabel");
            Label BodyLabel = (Label)e.Item.FindControl("BodyLabel");
            Label DateLabel = (Label)e.Item.FindControl("DateLabel");

            string[] dateArr = DateLabel.Text.Split(' ');
            DateLabel.Text = dateArr[0] + "<br/>@ " + dateArr[1];

            if (BodyLabel.Text.Length > 100)
            {
                BodyLabel.Text = BodyLabel.Text.Substring(0, 99) + "..";
            }

            HyperLink hprlnk_conversation = (HyperLink)e.Item.FindControl("hprlnk_conversation");
            if (ConversationIDLabel != null)
            {
                string url = "";
                string[] parameters = new string[1];

                parameters = new string[1] { "conversation=" + ConversationIDLabel.Text };
                url = DotNetNuke.Common.Globals.NavigateURL(MESSAGES_TAB_ID, "", parameters);
                hprlnk_conversation.NavigateUrl = url;// +"#" + PostIDLabel.Text;
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

        protected void lnkbtn_BackToInbox_Click(object sender, EventArgs e)
        {
            string url = "";
            string[] parameters = new string[0];

            parameters = new string[0];
            //url = NavigateURL(TabId, "", parameters);
            url = DotNetNuke.Common.Globals.NavigateURL(MESSAGES_TAB_ID, "", parameters);
            Response.Redirect(url);
        }

        protected void  MarkConversationMessagesAsRead(string conversationID)
        {
            DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();
            dp.ExecuteSQL("UPDATE Ourspace_Messages SET isRead = 1 WHERE conversationID =" + conversationID + " AND ToUserID=" + UserId);
        }

        protected void lnkbtn_reply_Click(object sender, EventArgs e)
        {
            DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();
            dp.ExecuteSQL("INSERT INTO Ourspace_Messages (FromUserID, ToUserID, Date, IsRead, Body, ConversationID) VALUES (" + UserId + "," + hdnfld_ConversationWithUserID.Value+ ",'" + DateTime.Now.ToString(new System.Globalization.CultureInfo("EN-us")) +"',0,'" + txt_reply.Text + "'," + hdnfld_conversationID.Value+ ")");
            
            lstvw_conversation.DataBind();
        }

        protected void lstvw_conversation_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            // Finding who the current user is having the conversation with
            // There are 2 users returned from the message data. The one that sent
            // the message and the one that received it. Here we compare each one with
            // the userID of the current user and set the ConversationWithUserID with the 
            // userID that is different to it.
            HiddenField hdnfld_FromUserID = (HiddenField)e.Item.FindControl("hdnfld_FromUserID");
            HiddenField hdnfld_ToUserID = (HiddenField)e.Item.FindControl("hdnfld_ToUserID");

            Label  DateLabel= (Label)e.Item.FindControl("DateLabel");

            DateTime date = Convert.ToDateTime(DateLabel.Text);

            DateLabel.Text = date.ToString("d MMMM @ HH:mm");
            

            Image img_profileMini = (Image)e.Item.FindControl("img_profileMini");
            if (hdnfld_ConversationWithUserID.Value == "")
            {
                
                
                if (hdnfld_FromUserID.Value == UserId.ToString())
                {
                    hdnfld_ConversationWithUserID.Value = hdnfld_ToUserID.Value;
                }
                else
                {
                    hdnfld_ConversationWithUserID.Value = hdnfld_FromUserID.Value;
                }
               
                

            }
            string strPath = Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + Convert.ToInt32(hdnfld_FromUserID.Value).ToString("000") + "\\"); ;
            if (Directory.Exists(strPath))
            {
                if (UserId <= 9)
                {
                    strPath = (ResolveUrl("~/Portals/" + PortalId + "/Users/" + Convert.ToInt32(hdnfld_FromUserID.Value).ToString("000") + "/" + Convert.ToInt32(hdnfld_FromUserID.Value).ToString("00")));
                }
                else
                {
                    strPath = ResolveUrl("~/Portals/" + PortalId + "/Users/" + Convert.ToInt32(hdnfld_FromUserID.Value).ToString("000") + "/" + hdnfld_FromUserID.Value);
                }
                strPath += "/" + hdnfld_FromUserID.Value + "/" + hdnfld_FromUserID.Value + "_50.jpg?" + DateTime.Now.Ticks;
                img_profileMini.ImageUrl = strPath;
            }
            else
            {
                img_profileMini.ImageUrl = ResolveUrl("~/images/no-avatar.png");
            }
        }
    }

}
