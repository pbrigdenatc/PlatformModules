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
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Globalization;


namespace DotNetNuke.Modules.Ourspace_ResultDetails
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_ResultDetails class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_ResultDetailsModuleBase, IActionable
    {
        int currentRow = 1;
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
            if (UserInfo.IsInRole("Administrators"))
            {
                pnlDisqus.Visible = true;
            }


            try
            {if (Request.QueryString["facebook"] != null)
                {
                    isFacebook = true;
                }
                if (Request.QueryString["result"] != null)
                {
                    int threadId = -1;
                   bool isInt = int.TryParse( Request.QueryString["result"].ToString(),out threadId);
                   hdnfld_ThreadId.Value = threadId.ToString();
                   sqldtsrc_ResultDetails.DataBind();
                   sqldtsrc_ResultSummary.DataBind();
                   lstvw_ResultDetails.DataBind();
                   lstvw_ResultSummary.DataBind();

                   string url = "";
                   string lang = CultureInfo.CurrentCulture.ToString();
                   Dictionary<string, int> tabs = new Dictionary<string, int>();
                   if (isFacebook)
                   {
                       tabs.Add("en-GB", 279);
                       tabs.Add("el-GR", 280);
                       tabs.Add("cs-CZ", 281);
                       tabs.Add("de-AT", 282);
                       int allProposalsTabId = tabs[lang];              
                       string[] parameters = new string[2] { "threadid=" + hdnfld_ThreadId.Value, "facebook=1" };
                       url = DotNetNuke.Common.Globals.NavigateURL(allProposalsTabId, "", parameters);
                   }
                   else
                   {
                       tabs.Add("en-GB", 200);
                       tabs.Add("el-GR", 201);
                       tabs.Add("cs-CZ", 202);
                       tabs.Add("de-AT", 203);
                       int allProposalsTabId = tabs[lang];
                       string[] parameters = new string[1] { "threadid=" + hdnfld_ThreadId.Value };
                       url = DotNetNuke.Common.Globals.NavigateURL(allProposalsTabId, "", parameters);
                   }
                   

                    hprlnk_ViewAllProposals.NavigateUrl = url;
                }
                
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void lstvw_Solutions_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lbl_currentRow = (Label)e.Item.FindControl("lbl_currentRow");
            lbl_currentRow.Text = currentRow + ".";
            currentRow++;

            Label lbl_Body = (Label)e.Item.FindControl("lbl_Body");
            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            lbl_Body.Text = util.GetTrimmedBody(Server, 500, lbl_Body.Text);

            Label UserIDLabel = (Label)e.Item.FindControl("UserIDLabel");
            HyperLink hprlnk_userProfile = (HyperLink)e.Item.FindControl("hprlnk_userProfile");

            string lang = CultureInfo.CurrentCulture.ToString();
            hprlnk_userProfile.NavigateUrl = util.GetUserProfileLink(int.Parse(UserIDLabel.Text), lang, Request.QueryString["facebook"] != null);


          
            string test = "";
        }

        protected void lstvw_DebateProposals_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            try
            {
                Label lbl_ThumbsDown = (Label)e.Item.FindControl("lbl_ThumbsDown");
                Label lbl_ThumbsUp = (Label)e.Item.FindControl("lbl_ThumbsUp");
                Label UserIDLabel = (Label)e.Item.FindControl("UserIDLabel");
                LinkButton lnkbtn_ApproveThread = (LinkButton)e.Item.FindControl("lnkbtn_ApproveThread");
                if (lbl_ThumbsDown != null)
                {
                    if (lbl_ThumbsDown.Text == "")
                    {
                        lbl_ThumbsDown.Text = "0";
                        lbl_ThumbsUp.Text = "0";
                    }
                }
                if (!UserInfo.IsInRole("Administrator") && lnkbtn_ApproveThread != null)
                {
                    lnkbtn_ApproveThread.Visible = false;
                }


                //Label PostIDLabel = (Label)e.Item.FindControl("PostIDLabel");
                Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
                Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
                Label lbl_Body = (Label)e.Item.FindControl("lbl_Body");
                Label CreatedDateLabel = (Label)e.Item.FindControl("CreatedDateLabel");
                Literal ltrlImage = (Literal)e.Item.FindControl("ltrlImage");

                string[] dateArr = CreatedDateLabel.Text.Split(' ');
                if (dateArr.Length > 1)
                {
                    CreatedDateLabel.Text = dateArr[0] + ", " + dateArr[1];
                }
                else
                {
                    CreatedDateLabel.Text = dateArr[0];
                }

                string htmlContent = Server.HtmlDecode(lbl_Body.Text);
                Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                List<string> images = util.GetImagesInHTMLString(htmlContent);//.GetImagesInHTMLString(html);
                lbl_Body.Text = util.GetTrimmedBody(Server, 350, htmlContent);

                if (images.Count > 0)
                {

                    ltrlImage.Text = images[0].Replace("style=", "ourspace=");
                }
                else
                {
                    HtmlTableCell imageTd = (HtmlTableCell)e.Item.FindControl("imageTd");
                    HtmlTableCell textTd = (HtmlTableCell)e.Item.FindControl("textTd");
                    imageTd.Visible = false;
                    textTd.ColSpan = 2;
                }

                // .Replace("&amp;amp;lt;br /&amp;amp;gt;", "<br/>");
                // if (BodyLabel.Text.Length > 100)
                // {
                //     BodyLabel.Text = BodyLabel.Text.Substring(0, 99) + "..";
                // }

                HyperLink hprlnk_post = (HyperLink)e.Item.FindControl("hprlnk_post");
                HyperLink hprlnk_subject = (HyperLink)e.Item.FindControl("hprlnk_subject");
                if (ThreadIDLabel != null)
                {


                    if (isFacebook)
                    {
                        string url = "";
                        string[] parameters = new string[3];

                        parameters = new string[4] { "forumid=" + ForumIDLabel.Text, "threadid=" + ThreadIDLabel.Text, "scope=posts", "facebook=1" };
                        //url = NavigateURL(TabId, "", parameters);
                        url = DotNetNuke.Common.Globals.NavigateURL(259, "", parameters);
                        hprlnk_post.NavigateUrl = url;
                        hprlnk_subject.NavigateUrl = url;
                    }
                    else
                    {
 string url = "";
                    string[] parameters = new string[4];

                    parameters = new string[4] { "forumid=" + ForumIDLabel.Text, "threadid=" + ThreadIDLabel.Text, "scope=posts","language="+  CultureInfo.CurrentCulture.ToString()};
                    //url = NavigateURL(TabId, "", parameters);
                    url = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters);
                    hprlnk_post.NavigateUrl = url;
                    hprlnk_subject.NavigateUrl = url;
                    }






                   
                }

                HyperLink hprlnk_userProfile = (HyperLink)e.Item.FindControl("hprlnk_userProfile");

               
                string lang = CultureInfo.CurrentCulture.ToString();
                hprlnk_userProfile.NavigateUrl = util.GetUserProfileLink(int.Parse(UserIDLabel.Text), lang, isFacebook);



            }
            catch (Exception ex)
            {
                string exception = ex.Message;
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

        protected void lstvw_Solutions_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
             if (e.CommandName == "translateProposal")
            {
                Label lbl_Body = (Label)e.Item.FindControl("lbl_Body");
                 Label lblServiceDown = (Label)e.Item.FindControl("lblServiceDown");
                 LinkButton lnkbtnTranslateProposal = (LinkButton)e.Item.FindControl("lnkbtnTranslateProposal");
                 Panel pnlTranslationBtnWrap = (Panel)e.Item.FindControl("pnlTranslationBtnWrap");
                
                lnkbtnTranslateProposal.Visible = false;
                pnlTranslationBtnWrap.Visible = false;

                Ourspace_Utilities.View util = new Ourspace_Utilities.View();
                string translatedBody = util.TranslateText(Application, "", CultureInfo.CurrentCulture.Name.Substring(0, 2), e.CommandArgument.ToString());

                
                if (!translatedBody.Contains("#NLA#"))
                {
                    lbl_Body.Text = translatedBody;
                    
                }
                else
                {
                    lbl_Body.Attributes.Add("error", translatedBody);
                    pnlTranslationBtnWrap.Visible = true;
                    lblServiceDown.Visible = true;
                }
            }
        }

        

    }

}
