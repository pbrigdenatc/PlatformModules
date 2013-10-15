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
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Globalization;
using Telerik.Web.UI;
using System.Data;
using System.Web.UI;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;
using DotNetNuke.Entities.Profile;
using System.Data.SqlClient;


namespace DotNetNuke.Modules.Ourspace_Overview
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_Overview class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_OverviewModuleBase, IActionable
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
                if (UserInfo.IsInRole("Administrators"))
                {
                    lnkbtn_TestLocation.Visible = true;
                    lnkbtn_SetNationalityFromIP.Visible = true;
                    lblCountry.Visible = true;
                    txtUserIdToGreek.Visible = true;
                    lnkbtn_switchToGreek.Visible = true;
                }
                

                if (!IsPostBack)
                {
                    Session["currentLanguage"] = CultureInfo.CurrentCulture.Name; 
                    ddlToDay.SelectedValue = DateTime.Now.Day.ToString();
                    ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();
                    ddlToYear.SelectedValue = DateTime.Now.Year.ToString();
                    lblStatRange.Text = (new DateTime(2010, 01, 01)).ToLongDateString() + " - " + DateTime.Now.ToLongDateString();
                }

                adminSuccessPanel.Visible = false;
                if (UserInfo.IsInRole("Collaborator"))
                {
                    pnlAdminControls.Visible = true;
                }

                Page.ClientScript.RegisterClientScriptInclude("overview.js", this.TemplateSourceDirectory + "/js/overview.js?v=1");
                if (Session["Ourspace_Overview_CurrentTab"] == null)
                {
                    Session["Ourspace_Overview_CurrentTab"] = "National";
                }
                if (Request.QueryString["facebook"] != null)
                {
                    isFacebook = true;
                }


                // lblOurspace_Overview_CurrentTab.Text = Session["Ourspace_Overview_CurrentTab"].ToString();
                if (Session["Ourspace_Overview_CurrentTab"] == "Eu")
                {
                    pnlEu.Visible = true;
                    pnlNational.Visible = false;

                    lnkbtnNationalDebates.CssClass = "tab-inactive";
                    lnkbtnEuDebates.CssClass = "tab-active";

                }
                else
                {
                    pnlEu.Visible = false;
                    pnlNational.Visible = true;

                    lnkbtnNationalDebates.CssClass = "tab-active";
                    lnkbtnEuDebates.CssClass = "tab-inactive";
                }

                if (Session["Ourspace_Overview_LanguageFilter"] != null)
                {
                    // lblOurspace_Overview_LanguageFilter.Text = Session["Ourspace_Overview_LanguageFilter"].ToString();
                    if (Session["Ourspace_Overview_LanguageFilter"] == "en-EU")
                    {
                        hdnfldLanguage.Value = Session["Ourspace_Overview_LanguageFilter"].ToString();
                        lstvw_OverviewItems.DataSourceID = sqldtsrc_ActiveDiscussionsPerLanguage.ID;
                        sqldtsrc_ActiveDiscussionsPerLanguage.DataBind();
                        lstvw_OverviewItems.DataBind();
                    }
                    else if (Session["Ourspace_Overview_LanguageFilter"] == "all")
                    {
                        lstvw_OverviewItems.DataSourceID = sqldtsrc_ActiveDiscussions.ID;
                        sqldtsrc_ActiveDiscussions.DataBind();
                        lstvw_OverviewItems.DataBind();

                        pnlViewingAll.Visible = true;
                        pnlViewingNational.Visible = false;
                    }
                    else
                    {
                        hdnfldLanguage.Value = Session["Ourspace_Overview_LanguageFilter"].ToString();
                        lstvw_OverviewItems.DataSourceID = sqldtsrc_ActiveDiscussionsPerLanguage.ID;
                        sqldtsrc_ActiveDiscussionsPerLanguage.DataBind();
                        lstvw_OverviewItems.DataBind();
                        pnlViewingAll.Visible = false;
                        pnlViewingNational.Visible = true;
                    }

                }
                else
                {
                    // hdnfldLanguage.Value = CultureInfo.CurrentCulture.Name;
                    lstvw_OverviewItems.DataSourceID = sqldtsrc_ActiveDiscussions.ID;
                    sqldtsrc_ActiveDiscussions.DataBind();
                    lstvw_OverviewItems.DataBind();

                    pnlViewingAll.Visible = true;
                    pnlViewingNational.Visible = false;

                }

        

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void lstvw_OverviewItems_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            // Localize previous/next buttons

            try{
            Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
            Label lbl_Body = (Label)e.Item.FindControl("lbl_Body");
            Label CreatedDateLabel = (Label)e.Item.FindControl("CreatedDateLabel");

            Label lblRejectReasonId = (Label)e.Item.FindControl("lblRejectReasonId");
            Label lblRejected = (Label)e.Item.FindControl("lblRejected");
            Label lblRejectedDash = (Label)e.Item.FindControl("lblRejectedDash"); 
                

            Label lblUserId = (Label)e.Item.FindControl("lblUserId");

            Literal ltrlImage = (Literal)e.Item.FindControl("ltrlImage");
            HyperLink hprlnk_subject = (HyperLink)e.Item.FindControl("hprlnk_subject");


            HyperLink hprlnk_userProfile = (HyperLink)e.Item.FindControl("hprlnk_userProfile");


            if (lblRejectReasonId.Text != "-1")
            {
                lblRejected.Visible = true;
                lblRejectedDash.Visible = true;
            }
            string[] dateArr = CreatedDateLabel.Text.Split(' ');
            if (dateArr.Length > 1)
            {
                CreatedDateLabel.Text = dateArr[0] + ", " + dateArr[1];
            }
            else
            {
                CreatedDateLabel.Text = dateArr[0];
            }


            string[] parameters2 = new string[2];
            parameters2 = new string[2] { "threadid=" + ThreadIDLabel.Text, "mode=featured" };
            string url = DotNetNuke.Common.Globals.NavigateURL(200, "", parameters2);
            hprlnk_subject.NavigateUrl = url;

            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            string lang = CultureInfo.CurrentCulture.ToString();
            hprlnk_userProfile.NavigateUrl = util.GetUserProfileLink(int.Parse(lblUserId.Text), lang, isFacebook);

            HtmlGenericControl phase1 = (HtmlGenericControl)e.Item.FindControl("phase1");
            HtmlGenericControl phase2 = (HtmlGenericControl)e.Item.FindControl("phase2");
            HtmlGenericControl phase3 = (HtmlGenericControl)e.Item.FindControl("phase3");
            HtmlGenericControl phase4 = (HtmlGenericControl)e.Item.FindControl("phase4");
            HtmlGenericControl[] phases = { phase1, phase2, phase3, phase4 };
            Label lblPhaseId = (Label)e.Item.FindControl("lblPhaseId");
            int currentPhase = int.Parse(lblPhaseId.Text);
            int i = 0;
            int j = 0;
            for (i = 0; i < currentPhase - 1; i++)
            {
                phases[i].Attributes["class"] = "phase-progress-icon phase-progress-complete phase-progress-" + (i + 1);

            }


            phases[i].Attributes["class"] = "phase-progress-icon phase-progress-active phase-progress-" + (i + 1);

            if (i == 3)
                phases[i].Attributes["class"] = "phase-progress-icon phase-progress-complete phase-progress-" + (i + 1);


            i++;
            for (j = i; j < 4; j++)
            {
                phases[j].Attributes["class"] = "phase-progress-icon phase-progress-inactive phase-progress-" + (i + 1);
            }


            // Make phase label links redirect user to phase according to phase.
            HyperLink hprlnk_Phase1 = (HyperLink)e.Item.FindControl("hprlnk_Phase1");
            HyperLink hprlnk_Phase2 = (HyperLink)e.Item.FindControl("hprlnk_Phase2");
            HyperLink hprlnk_Phase3 = (HyperLink)e.Item.FindControl("hprlnk_Phase3");
            HyperLink hprlnk_Phase4 = (HyperLink)e.Item.FindControl("hprlnk_Phase4");

            // Associating the Facebook tabs to each language

            // Suggest
            Dictionary<string, int> suggestTabs = new Dictionary<string, int>();
            suggestTabs.Add("en-GB", 271);
            suggestTabs.Add("el-GR", 272);
            suggestTabs.Add("cs-CZ", 273);
            suggestTabs.Add("de-AT", 274);

            // Join
            Dictionary<string, int> joinTabs = new Dictionary<string, int>();
            joinTabs.Add("en-GB", 259);
            joinTabs.Add("el-GR", 259);
            joinTabs.Add("cs-CZ", 259);
            joinTabs.Add("de-AT", 259);

            // Vote
            Dictionary<string, int> voteTabs = new Dictionary<string, int>();
            voteTabs.Add("en-GB", 279);
            voteTabs.Add("el-GR", 280);
            voteTabs.Add("cs-CZ", 281);
            voteTabs.Add("de-AT", 282);

            // Results


            // Subject link redirects user according to topic phase
            string language = CultureInfo.CurrentCulture.Name;
            if (currentPhase == 1)
            {
                if (isFacebook)
                {
                    int suggestTab = suggestTabs[language];
                    string[] parameters = new string[3];
                    //parameters1 = new string[2] { "user=" + userId, "facebook=1" };
                    parameters = new string[3] { "threadid=" + ThreadIDLabel.Text, "scope=posts", "facebook=1" };
                    hprlnk_subject.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(suggestTab, "", parameters);



                    // Phase links
                    hprlnk_Phase1.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(suggestTab, "", parameters);
                    hprlnk_Phase2.CssClass = "phase-unavailable";
                    hprlnk_Phase3.CssClass = "phase-unavailable";
                    hprlnk_Phase4.CssClass = "phase-unavailable";
                }
                else
                {
                    string[] parameters = new string[2];
                    parameters = new string[2] { "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                    hprlnk_subject.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(73, "", parameters);
                    hprlnk_subject.NavigateUrl = hprlnk_subject.NavigateUrl.Replace("en-GB", language);

                    // Phase links
                    hprlnk_Phase1.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(73, "", parameters);
                    hprlnk_Phase1.NavigateUrl = hprlnk_Phase1.NavigateUrl.Replace("en-GB", language);
                    hprlnk_Phase2.CssClass = "phase-unavailable";
                    hprlnk_Phase3.CssClass = "phase-unavailable";
                    hprlnk_Phase4.CssClass = "phase-unavailable";
                }

            }
            else if (currentPhase == 2)
            {
                if (isFacebook)
                {
                    
                    int joinTab = joinTabs[language];
                    string[] parameters = new string[3];
                    parameters = new string[3] { "threadid=" + ThreadIDLabel.Text, "scope=posts", "facebook=1" };
                    hprlnk_subject.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(joinTab, "", parameters);

                    // Phase links                    
                    // Phase 2
                    hprlnk_Phase2.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(joinTab, "", parameters);
                    
                    // Phase 1
                    hprlnk_Phase1.CssClass = "phase-unavailable";
                    //int suggestTab = suggestTabs[language];
                    //parameters = new string[3] { "threadid=" + ThreadIDLabel.Text, "scope=posts", "facebook=1" };
                   // hprlnk_Phase1.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(suggestTab, "", parameters);
                    // Phase 3
                    hprlnk_Phase3.CssClass = "phase-unavailable";
                    // Phase 4
                    hprlnk_Phase4.CssClass = "phase-unavailable";

                }
                else
                {
                    string[] parameters = new string[2];
                    parameters = new string[2] { "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                    hprlnk_subject.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters);
                    hprlnk_subject.NavigateUrl = hprlnk_subject.NavigateUrl.Replace("en-GB", language);
                    
                    // Phase links

                    // Phase 2
                    hprlnk_Phase2.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters);
                    hprlnk_Phase2.NavigateUrl = hprlnk_Phase2.NavigateUrl.Replace("en-GB", language);
                    // Phase 1
                   // int suggestTab = suggestTabs[language];
                   // string[] phase1Parameters = new string[2];
                   // phase1Parameters = new string[2] { "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                    //hprlnk_Phase1.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(73, "", phase1Parameters);
                    hprlnk_Phase1.CssClass = "phase-unavailable";
                    
                    // Phase 3
                    hprlnk_Phase3.CssClass = "phase-unavailable";
                    // Phase 4
                    hprlnk_Phase4.CssClass = "phase-unavailable";

                }

            }
            else if (currentPhase == 3)
            {
                if (isFacebook)
                {
                    int voteTab = voteTabs[language];
                    string[] parameters = new string[3];
                    parameters = new string[3] { "threadid=" + ThreadIDLabel.Text, "mode=featured", "facebook=1" };
                    hprlnk_subject.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(voteTab, "", parameters);

                    // Phase links

                    // Phase 3
                    hprlnk_Phase3.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(voteTab, "", parameters);
                    // Phase 1
                    //int suggestTab = suggestTabs[language];
                    //parameters = new string[3] { "threadid=" + ThreadIDLabel.Text, "scope=posts", "facebook=1" };
                    //hprlnk_Phase1.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(suggestTab, "", parameters);
                    hprlnk_Phase1.CssClass = "phase-unavailable";
                    // Phase 2
                    int joinTab = joinTabs[language];
                    parameters = new string[2] { "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                    hprlnk_Phase2.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(joinTab, "", parameters);
                    // Phase 4
                    hprlnk_Phase4.CssClass = "phase-unavailable";

                }
                else
                {
                    string[] parameters = new string[2];
                    parameters = new string[2] { "threadid=" + ThreadIDLabel.Text, "mode=featured" };
                    hprlnk_subject.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(200, "", parameters);
                    hprlnk_subject.NavigateUrl = hprlnk_subject.NavigateUrl.Replace("en-GB", language);


                    // Phase links

                    // Phase 3
                    hprlnk_Phase3.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(200, "", parameters);
                    hprlnk_Phase3.NavigateUrl = hprlnk_Phase3.NavigateUrl.Replace("en-GB", language);
                    // Phase 1
                   // string[] phase1Parameters = new string[2];
                    //phase1Parameters = new string[2] { "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                    //hprlnk_Phase1.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(73, "", phase1Parameters);
                    hprlnk_Phase1.CssClass = "phase-unavailable";
                    
                    // Phase 2
                    string[] phase2Parameters = new string[2];
                    phase2Parameters = new string[2] { "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                    hprlnk_Phase2.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(62, "", phase2Parameters);
                    hprlnk_Phase2.NavigateUrl = hprlnk_Phase2.NavigateUrl.Replace("en-GB", language);
                    // Phase 4
                    hprlnk_Phase4.CssClass = "phase-unavailable";


                }
            }
            else if (currentPhase == 4)
            {
                if (isFacebook)
                {
                    int voteTab = voteTabs[language];
                    string[] parameters = new string[2];
                    parameters = new string[2] { "result=" + ThreadIDLabel.Text, "facebook=1" };
                    hprlnk_subject.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(voteTab, "", parameters);

                    // Phase links

                    // Phase 4
                    hprlnk_Phase4.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(voteTab, "", parameters);

                    // Phase 1
                    //int suggestTab = suggestTabs[language];
                    //parameters = new string[3] { "threadid=" + ThreadIDLabel.Text, "scope=posts", "facebook=1" };
                    //hprlnk_Phase1.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(suggestTab, "", parameters);
                    hprlnk_Phase1.CssClass = "phase-unavailable";

                    // Phase 2
                    int joinTab = joinTabs[language];
                    parameters = new string[2] { "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                    hprlnk_Phase2.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(joinTab, "", parameters);

                    // Phase 3
                    //string[] voteParameters = new string[3];
                    //voteParameters = new string[3] { "threadid=" + ThreadIDLabel.Text, "mode=featured", "facebook=1" };
                    //hprlnk_Phase3.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(voteTab, "", voteParameters);
                    hprlnk_Phase3.CssClass = "phase-unavailable";
                }
                else
                {
                    string[] parameters = new string[1];
                    parameters = new string[1] { "result=" + ThreadIDLabel.Text };
                    hprlnk_subject.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(196, "", parameters);
                    hprlnk_subject.NavigateUrl = hprlnk_subject.NavigateUrl.Replace("en-GB", language);


                    hprlnk_Phase1.CssClass = "phase-unavailable";
                    // Phase links

                    // Phase 4
                    hprlnk_Phase4.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(196, "", parameters);
                    hprlnk_Phase4.NavigateUrl = hprlnk_Phase4.NavigateUrl.Replace("en-GB", language);
                    // Phase 2
                    string[] phase2Parameters = new string[2];
                    phase2Parameters = new string[2] { "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                    hprlnk_Phase2.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(62, "", phase2Parameters);
                    hprlnk_Phase2.NavigateUrl = hprlnk_Phase2.NavigateUrl.Replace("en-GB", language);

                    // Phase 3
                    //string[] phase3Parameters = new string[2];
                    //phase3Parameters = new string[2] { "threadid=" + ThreadIDLabel.Text, "mode=featured" };
                    //hprlnk_Phase3.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(200, "", phase3Parameters);
                    hprlnk_Phase3.CssClass = "phase-unavailable";
                }

            }
                
            }catch(Exception ex)
            {
                string message = ex.Message;
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

        protected void lnkbtnNationalDebates_Click(object sender, EventArgs e)
        {
            lnkbtnFeaturedDebates.CssClass = "tab-inactive";
            lnkbtnNationalDebates.CssClass = "tab-active";
            lnkbtnEuDebates.CssClass = "tab-inactive";
            if (Session["Ourspace_Overview_NationalStatusAll"] != null)
            {
                if ((bool)Session["Ourspace_Overview_NationalStatusAll"])
                {
                    Session["Ourspace_Overview_LanguageFilter"] = "all";
                    hdnfldLanguage.Value = CultureInfo.CurrentCulture.Name;
                    lstvw_OverviewItems.DataSourceID = sqldtsrc_ActiveDiscussions.ID;
                    pnlViewingAll.Visible = true;
                    pnlViewingNational.Visible = false;
                }
                else
                {
                    Session["Ourspace_Overview_LanguageFilter"] = CultureInfo.CurrentCulture.Name;
                    hdnfldLanguage.Value = CultureInfo.CurrentCulture.Name;
                    lstvw_OverviewItems.DataSourceID = sqldtsrc_ActiveDiscussionsPerLanguage.ID;
                    pnlViewingAll.Visible = false;
                    pnlViewingNational.Visible = true;
                }
            }
            else
            {
                Session["Ourspace_Overview_LanguageFilter"] = CultureInfo.CurrentCulture.Name;
                hdnfldLanguage.Value = CultureInfo.CurrentCulture.Name;
                lstvw_OverviewItems.DataSourceID = sqldtsrc_ActiveDiscussionsPerLanguage.ID;
                pnlViewingAll.Visible = false;
                pnlViewingNational.Visible = true;
            }


            sqldtsrc_ActiveDiscussionsPerLanguage.DataBind();
            lstvw_OverviewItems.DataBind();


            pnlFeatured.Visible = false;
            pnlEu.Visible = false;
            pnlNational.Visible = true;


            Session["Ourspace_Overview_CurrentTab"] = "National";

        }

        protected void lnkbtnEuDebates_Click(object sender, EventArgs e)
        {
            lnkbtnFeaturedDebates.CssClass = "tab-inactive";
            lnkbtnNationalDebates.CssClass = "tab-inactive";
            lnkbtnEuDebates.CssClass = "tab-active";

            Session["Ourspace_Overview_LanguageFilter"] = "en-EU";
            hdnfldLanguage.Value = "en-EU";
            lstvw_OverviewItems.DataSourceID = sqldtsrc_ActiveDiscussionsPerLanguage.ID;
            sqldtsrc_ActiveDiscussionsPerLanguage.DataBind();
            lstvw_OverviewItems.DataBind();
            pnlEu.Visible = true;
            pnlNational.Visible = false;
            pnlFeatured.Visible = false;


            Session["Ourspace_Overview_CurrentTab"] = "Eu";

        }

        protected void lnkbtnFeaturedDebates_Click(object sender, EventArgs e)
        {
            lnkbtnEuDebates.CssClass = "tab-inactive";
            lnkbtnNationalDebates.CssClass = "tab-inactive";
            lnkbtnFeaturedDebates.CssClass = "tab-active";

            //Session["Ourspace_Overview_LanguageFilter"] = "en-EU";
            //hdnfldLanguage.Value = "en-EU";
            lstvw_OverviewItems.DataSourceID = sqldtsrc_FeaturedDiscussions.ID;
            sqldtsrc_FeaturedDiscussions.DataBind();
            lstvw_OverviewItems.DataBind();
            pnlEu.Visible = false;
            pnlNational.Visible = false;
            pnlFeatured.Visible = true;

            Session["Ourspace_Overview_CurrentTab"] = "Eu";

        }

        protected void lnkbtnViewAllLanguages_Click(object sender, EventArgs e)
        {
            Session["Ourspace_Overview_LanguageFilter"] = "all";
            Session["Ourspace_Overview_NationalStatusAll"] = true;
            lstvw_OverviewItems.DataSourceID = sqldtsrc_ActiveDiscussions.ID;
            sqldtsrc_ActiveDiscussions.DataBind();
            lstvw_OverviewItems.DataBind();
            pnlViewingAll.Visible = true;
            pnlViewingNational.Visible = false;
        }

        protected void lnkbtnViewOwnLanguage_Click(object sender, EventArgs e)
        {
            Session["Ourspace_Overview_LanguageFilter"] = CultureInfo.CurrentCulture.Name;
            Session["Ourspace_Overview_NationalStatusAll"] = false;
            hdnfldLanguage.Value = CultureInfo.CurrentCulture.Name;
            lstvw_OverviewItems.DataSourceID = sqldtsrc_ActiveDiscussionsPerLanguage.ID;
            sqldtsrc_ActiveDiscussionsPerLanguage.DataBind();
            lstvw_OverviewItems.DataBind();
            pnlViewingAll.Visible = false;
            pnlViewingNational.Visible = true;

        }

        public string GetString()
        {
            return Localization.GetString("Next");
        }

        public string GetLocalizedCategory(string category)
        {
            string resfile = LocalResourceFile;
            string forumresfile = LocalResourceFile.Replace("View", "") + "../../Forum/App_LocalResources/SharedResources";
            category = Localization.GetString(category.Replace(" ", ""), forumresfile);

            return category;
        }

        
        

        protected void lstvw_reportedPosts_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int forumId = Convert.ToInt32( ((Label) e.Item.FindControl("ForumIDLabel")).Text);
            int postId = Convert.ToInt32(((Label)e.Item.FindControl("PostIDLabel")).Text);
             int userId = Convert.ToInt32(((Label)e.Item.FindControl("UserIDLabel")).Text);


            Label BodyLabel = (Label)e.Item.FindControl("BodyLabel");
              Label ReasonLabel = (Label)e.Item.FindControl("ReasonLabel");
           

            Ourspace_Utilities.View util = new Ourspace_Utilities.View();
            ((HyperLink)e.Item.FindControl("hprlnk_GoToPost")).NavigateUrl = util.GetPostUrl(forumId, postId);
            ((HyperLink)e.Item.FindControl("hprlnk_GoToProfile")).NavigateUrl = util.GetUserProfileLink(userId,CultureInfo.CurrentCulture.ToString(),false);
            
            BodyLabel.Text = util.GetTrimmedBody(Server, 300, BodyLabel.Text);

            ReasonLabel.Text = util.GetTrimmedBody(Server, 300, ReasonLabel.Text);
            
            
        }

        protected void lstvw_reportedPosts_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                adminSuccessPanel.Visible = false;
                if (e.CommandName == "RemoveFromList")
                {
                    DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();
                    dp.ExecuteSQL("UPDATE Forum_Post_Reported SET Addressed = 'True' WHERE postId =" + e.CommandArgument);
                    lstvw_reportedPosts.DataBind();
                    adminSuccessPanel.Visible = true;
                    lblOkMessage.Text = "Reported post removed from report list.";
                }
                else if (e.CommandName == "RequestRemoval")
                {
                DotNetNuke.Entities.Users.UserInfo admin =   DotNetNuke.Entities.Users.UserController.GetUserById(0, 1);

                 DotNetNuke.Services.Mail.Mail.SendEmail("info@ep-ourspace.eu", admin.Email, "Please remove post", UserInfo.DisplayName + " (" + UserInfo.UserID + ")" + " has requested you to delete post with postId " + e.CommandArgument);
                 adminSuccessPanel.Visible = true;
                    lblOkMessage.Text = "Your request has been sent to the Ourspace Platform administrator. You will be notified as soon as your request has been fulfilled.";
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        protected void lnkbtn_ViewReportedPosts_Click(object sender, EventArgs e)
        {
            updtpnl_AllThreads.Visible = false;
            pnlAdmin.Visible = true;
            pnlStatistics.Visible = false;

            lnkbtn_ViewReportedPosts.CssClass = "tab-active";

            lnkbtn_ViewTopicsOverview.CssClass = "tab-inactive";
            lnkbtn_ViewPlatformStatistics.CssClass = "tab-inactive";
            lstvw_reportedPosts.DataBind();
        }

        protected void RefreshStatsValues()
        {
           // Session["statsFrom"] = txtFrom.ToString();
           // Session["statsTo"] = txtTo.ToString();
            DataView dvSql = (DataView)sqldtsrc_UsersEnGb.Select(DataSourceSelectArguments.Empty);

            //sqldtsrc_UsersEnGb.SelectParameters.Add("@id", id);



            foreach (DataRowView drvSql in dvSql)
            {
                lblUsersEnGb.Text = drvSql["count"].ToString();
            }
            dvSql = (DataView)sqldtsrc_UsersElGr.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblUsersElGr.Text = drvSql["count"].ToString();
            }
            dvSql = (DataView)sqldtsrc_UsersDeAt.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblUsersDeAt.Text = drvSql["count"].ToString();
            }
            dvSql = (DataView)sqldtsrc_UsersCsCz.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblUsersCsCz.Text = drvSql["count"].ToString();
            }





            // Threads
            dvSql = (DataView)sqldtsrc_ThreadsEnEu.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblThreadsEnEu.Text = drvSql["count"].ToString();
            }


            dvSql = (DataView)sqldtsrc_ThreadsEnGb.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblThreadsEnGb.Text = drvSql["count"].ToString();
            }

            dvSql = (DataView)sqldtsrc_ThreadsElGr.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblThreadsElGr.Text = drvSql["count"].ToString();
            }

            dvSql = (DataView)sqldtsrc_ThreadsDeAt.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblThreadsDeAt.Text = drvSql["count"].ToString();
            }

            dvSql = (DataView)sqldtsrc_ThreadsCsCz.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblThreadsCsCz.Text = drvSql["count"].ToString();
            }

            // Posts
            dvSql = (DataView)sqldtsrc_PostsEnEu.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblPostsEnEu.Text = drvSql["count"].ToString();
            }

            dvSql = (DataView)sqldtsrc_PostsEnGb.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblPostsEnGb.Text = drvSql["count"].ToString();
            }

            dvSql = (DataView)sqldtsrc_PostsElGr.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblPostsElGr.Text = drvSql["count"].ToString();
            }

            dvSql = (DataView)sqldtsrc_PostsDeAt.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblPostsDeAt.Text = drvSql["count"].ToString();
            }

            dvSql = (DataView)sqldtsrc_PostsCsCz.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblPostsCsCz.Text = drvSql["count"].ToString();
            }

            // Likes
            dvSql = (DataView)sqldtsrc_LikesEnEu.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblLikesEnEu.Text = drvSql["count"].ToString();
            }

            dvSql = (DataView)sqldtsrc_LikesEnGb.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblLikesEnGb.Text = drvSql["count"].ToString();
            }

            dvSql = (DataView)sqldtsrc_LikesElGr.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblLikesElGr.Text = drvSql["count"].ToString();
            }

            dvSql = (DataView)sqldtsrc_LikesDeAt.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblLikesDeAt.Text = drvSql["count"].ToString();
            }

            dvSql = (DataView)sqldtsrc_LikesCsCz.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView drvSql in dvSql)
            {
                lblLikesCsCz.Text = drvSql["count"].ToString();
            }
        }

        protected void lnkbtn_ViewPlatformStatistics_Click(object sender, EventArgs e)
        {
            //updtpnl_AllThreads.Visible = false;
            //pnlAdmin.Visible = true;
            //lnkbtn_ViewReportedPosts.CssClass = "tab-active";

            //lnkbtn_ViewTopicsOverview.CssClass = "tab-inactive";
            //lstvw_reportedPosts.DataBind();

            updtpnl_AllThreads.Visible = false;
            pnlAdmin.Visible = false;
            pnlUserDetails.Visible = false;
            pnlStatistics.Visible = true;

            lnkbtn_ViewReportedPosts.CssClass = "tab-inactive";
            lnkbtn_ViewTopicsOverview.CssClass = "tab-inactive";
            lnkbtn_ViewUserDetails.CssClass = "tab-inactive";
            lnkbtn_ViewPlatformStatistics.CssClass = "tab-active";


            RefreshStatsValues();







            //DataView dvSql = (DataView)sqldtsrc_UsersAll.Select(DataSourceSelectArguments.Empty);
            //foreach (DataRowView drvSql in dvSql)
            //{
            //    //lblUsersAll.Text = drvSql["count"].ToString();
            //}

            // Users

            

        }

        protected void lnkbtn_ViewUserDetails_Click(object sender, EventArgs e)
        {

            updtpnl_AllThreads.Visible = false;
            pnlAdmin.Visible = false;
            pnlStatistics.Visible = false;
            pnlUserDetails.Visible = true;

            lnkbtn_ViewReportedPosts.CssClass = "tab-inactive";
            lnkbtn_ViewTopicsOverview.CssClass = "tab-inactive";
            lnkbtn_ViewUserDetails.CssClass = "tab-active";

            lnkbtn_ViewPlatformStatistics.CssClass = "tab-inactive";


            RefreshStatsValues();
}


        

        protected void lnkbtn_ViewTopicsOverview_Click(object sender, EventArgs e)
        {
            updtpnl_AllThreads.Visible = true;
            pnlAdmin.Visible = false;
            lnkbtn_ViewReportedPosts.CssClass = "tab-inactive";

            lnkbtn_ViewTopicsOverview.CssClass = "tab-active";
        }

      

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DateTime fromDateValue;
   DateTime toDateValue;
            bool fromValid = false;
            bool toValid = false;
            string from = ddlFromYear.SelectedValue + "-" + ddlFromMonth.SelectedValue + "-" + ddlFromDay.SelectedValue;
            if (DateTime.TryParse(from, out fromDateValue))
            {

               
                fromValid = true;
            }
            else
            {
                lblInvalidFrom.Visible = true;
            }

            string to = ddlToYear.SelectedValue + "-" + ddlToMonth.SelectedValue + "-" + ddlToDay.SelectedValue;
            if (DateTime.TryParse(to, out toDateValue))
            {

                
                toValid = true;
            }
            else
            {
                lblInvalidTo.Visible = true;
            }

            if (fromValid && toValid)
            {
                if (fromDateValue < toDateValue)
                {
                    Session["statsFrom"] = from;
                    Session["statsTo"] = to;
                    lblStatRange.Text = fromDateValue.ToLongDateString() + " - " + toDateValue.ToLongDateString();
                    RefreshStatsValues();
                    lblInvalidTo.Visible = false;
                    lblInvalidFrom.Visible = false;
                }
                else
                {
                    lblInvalidTo.Visible = true;
                    lblInvalidFrom.Visible = true;
                }
            }


        }

        protected void lnkbtn_Execute_Click(object sender, EventArgs e)
        {
            string CONNECTION_STRING = DotNetNuke.Common.Utilities.Config.GetConnectionString();
            SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Forum_Thread_Move", 1320, 52, 0, "Requested by Dominika");
        SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Forum_Thread_Move", 1319, 52, 0, "Requested by Dominika");
        SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Forum_Thread_Move", 1314, 52, 0, "Requested by Dominika");
        
            SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Forum_Thread_Move", 1275, 52, 0, "Requested by Dominika");
        
            SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Forum_Thread_Move", 184, 52, 0, "Requested by Dominika");
        
            SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Forum_Thread_Move", 211, 52, 0, "Requested by Dominika");
        
            SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Forum_Thread_Move", 1079, 52, 0, "Requested by Dominika");
        
            SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Forum_Thread_Move", 822, 52, 0, "Requested by Dominika");
        SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Forum_Thread_Move", 1085, 52, 0, "Requested by Dominika");
        SqlHelper.ExecuteNonQuery(CONNECTION_STRING, "Forum_Thread_Move", 1063, 52, 0, "Requested by Dominika");
        
        
        }

        protected void lnkbtn_CopyLanguageToNationality_Click(object sender, EventArgs e)
        {


          // CopyLanguageToNationality();
            CopyLanguageToNationalityOfNewUsers();
        }

        protected void CopyLanguageToNationalityOfNewUsers()
        {
            string roleName = "Registered Users";
            Security.Roles.RoleController roleCtlr = new Security.Roles.RoleController();
            ArrayList objUserRoles = roleCtlr.GetUsersByRoleName(PortalId, roleName);
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

        protected void SetNationalityFromIP()
        {
            string roleName = "Registered Users";
            Security.Roles.RoleController roleCtlr = new Security.Roles.RoleController();
            ArrayList objUserRoles = roleCtlr.GetUsersByRoleName(PortalId, roleName);
            foreach (Entities.Users.UserInfo user in objUserRoles)
            {
                
                    string CONNECTION_STRING = DotNetNuke.Common.Utilities.Config.GetConnectionString();
                    SqlConnection conn = new SqlConnection(CONNECTION_STRING);
                    // Checking if nationality has been set from IP before
                    conn.Open();  
                    SqlDataReader readerUserAlreadyDone = SqlHelper.ExecuteReader(conn, CommandType.Text, "SELECT * FROM OURSPACE_LOG WHERE userId = " + user.UserID);
                     
                if (!readerUserAlreadyDone.HasRows)
                    {
                        SqlConnection conn2 = new SqlConnection(CONNECTION_STRING);
                        conn2.Open();
                        SqlDataReader reader = SqlHelper.ExecuteReader(conn2, CommandType.Text, "SELECT * FROM Users WHERE Userid = " + user.UserID);
                       
                        if (reader.Read())
                        {
                            string ip = reader["LastIPAddress"].ToString();
                            Ourspace_Utilities.View.LocationInfo location = Ourspace_Utilities.View.GeoLocationService.GetLocationInfo(ip);
                            if (location != null)
                            {
                                string countryCode = GetCountryFromCountryName(location.CountryName);
                                if (countryCode != "UNKNOWN")
                                {
                                    user.Profile.SetProfileProperty("OurSpaceNationality", countryCode);
                                    ProfileController.UpdateUserProfile(user);
                                }
                                    lblCountry.Text += user.UserID + " " + ip + " " + location.CountryName + " " + countryCode + "<br/>";
                                    SqlConnection conn3 = new SqlConnection(CONNECTION_STRING);
                                    conn3.Open();
                                    SqlHelper.ExecuteNonQuery(conn3, CommandType.Text, "INSERT INTO OURSPACE_LOG  VALUES ('" + user.UserID + " " + ip + " " + location.CountryName + " " + countryCode + "', " + user.UserID + ")");
                                    conn3.Close();
                                
                            }

                        }
                        conn2.Close();
                    }
                conn.Close();
                
                
            }
        }

        protected string GetCountryFromCountryName(string countryName)
        {
            if (countryName.Contains("CYPRUS") || countryName.Contains("GREECE"))
                return "el-GR";
            else if (countryName.Contains("CZECH REPUBLIC"))
                return "cs-CZ";
            else if (countryName.Contains("GERMANY") || countryName.Contains("AUSTRIA"))
                return "de-AT";
            else if (countryName.Contains("Unknown"))
                return "UNKNOWN";
            else
                return "en-GB";
        }

        protected void lnkbtn_SetNationalityFromIP_Click(object sender, EventArgs e)
        {
            SetNationalityFromIP();
        }

        protected void switchUserToGreek(string _userId)
        {
          Entities.Users.UserInfo user =   DotNetNuke.Entities.Users.UserController.GetUserById(PortalId, Convert.ToInt32(_userId));
          user.Profile.SetProfileProperty("OurSpaceNationality", "el-GR");
          ProfileController.UpdateUserProfile(user);
        }

        protected void lnkbtn_switchToGreek_Click(object sender, EventArgs e)
        {

          string[] ids =  txtUserIdToGreek.Text.Split(' ');
          foreach (string id in ids)
          {
              string newid = id.Trim();
              if(newid.Length > 0)
                switchUserToGreek(id.Trim());
          }
        }
        

        




    }

}
