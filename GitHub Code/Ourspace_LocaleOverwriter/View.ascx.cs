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
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI;


namespace DotNetNuke.Modules.Ourspace_LocaleOverwriter
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_LocaleOverwriter class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_LocaleOverwriterModuleBase, IActionable
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


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                //if( CultureInfo.CurrentCulture.Name == "en-GB")
                //{
                //    hprlnkSurvey.NavigateUrl = "http://www.joinourspace.eu/Survey/tabid/303/language/en-GB/Default.aspx";
                //}
                //else if (CultureInfo.CurrentCulture.Name == "el-GR")
                //{
                //    hprlnkSurvey.NavigateUrl = "http://www.joinourspace.eu/Survey/tabid/304/language/el-GR/Default.aspx";
                //}
                //else if (CultureInfo.CurrentCulture.Name == "cs-CZ")
                //{
                //    hprlnkSurvey.NavigateUrl = "http://www.joinourspace.eu/Survey/tabid/305/language/cs-CZ/Default.aspx";
                //}
                //else if (CultureInfo.CurrentCulture.Name == "de-AT")
                //{
                //    hprlnkSurvey.NavigateUrl = "http://www.joinourspace.eu/Survey/tabid/306/language/de-AT/Default.aspx";
                //}

                if (Session["213Test"] != null)
                    lblTest.Text = Session["213Test"].ToString();

                //en gb 208
                // el gr 209
                // cs-cz 210
                // 211

                //if (TabId != 208 && TabId != 209 && TabId != 210 && TabId != 211 && TabId != 88)
                //{
                //    Session["pageVisitedBeforeLogin"] = Request.Url.ToString();
                //    lblRedirectToAfterLogin.Text = Request.Url.ToString();
                //    Session["userIdWhenPageUrlStored"] = UserId;
                //    Session["tabIdVisitedBeforeLogin"] = TabId;
                //}
                //if (Session["pageVisitedBeforeLogin"] != null)
                //    lblRedirectToAfterLogin.Text = Session["pageVisitedBeforeLogin"].ToString();

                //Session["pageVisitedBeforeLogin"] = "test";
                if (Request.QueryString["returnurl"] != null && UserId < 0 && !(Request.QueryString["returnurl"].ToString().Contains("208") || Request.QueryString["returnurl"].ToString().Contains("209") || Request.QueryString["returnurl"].ToString().Contains("210") || Request.QueryString["returnurl"].ToString().Contains("211")))
                {
                    Session["atc_pageVisited"] = Request.QueryString["returnurl"].ToString();
                    Session["atc_pageVisitedSet"] = true;
                    //Session["atc_pageVisited"] = "aurl";// Request.QueryString["returnurl"].ToString();
                    lblRedirectToAfterLogin.Text = Request.QueryString["returnurl"].ToString();
                    Session["userRedirected"] = null;
                }
                HtmlHead head = Page.Header;

                LiteralControl lctl = new LiteralControl("<link rel='image_src' href='http://joinourspace.eu/images/logo.png' />");

                head.Controls.Add(lctl);
                //  bool forceRedirect = false;
                if (Request.QueryString["facebook"] != null)
                {
                    SetLanguageSwitcherUrls(true);
                }
                else
                {
                    SetLanguageSwitcherUrls(false);
                }
                Page.ClientScript.RegisterClientScriptInclude("LocaleOverwriter.js", this.TemplateSourceDirectory + "/js/LocaleOverwriter.js");
                string currentCookieCulture = "no cookie found";
                if (UserId > -1)
                {

                    if (Request.QueryString["newLang"] != null)
                    {
                        SetUserLanguage(Request.QueryString["newLang"]);
                        if (Request.QueryString["facebook"] != null)
                        {
                            string fbUrl = DotNetNuke.Common.Globals.NavigateURL(251) + "?facebook=1";
                            Response.Redirect(fbUrl, true);

                        }


















                        //if (TabId != 88 && TabId != 170 && TabId != 41 && TabId != 101)
                        //{
                        // If not the home page
                        // forceRedirect = true;
                        //}

                        //SetUserLanguage(Request.QueryString["newLang"]);
                        string overrideLang = Request.QueryString["newLang"].ToString();

                        if (overrideLang == "en-GB")
                        {
                            Response.Redirect("http://www.joinourspace.eu/Home/tabid/41/language/en-GB/default.aspx");
                        }
                        else if (overrideLang == "el-GR")
                        {
                            Response.Redirect("http://www.joinourspace.eu/%CE%91%CF%81%CF%87%CE%B9%CE%BA%CE%AE/tabid/88/language/el-GR/Default.aspx");
                        }
                        else if (overrideLang == "de-AT")
                        {
                            Response.Redirect("http://www.joinourspace.eu/%C3%9Cbersicht/tabid/170/language/de-AT/Default.aspx");
                        }
                        else if (overrideLang == "cs-CZ")
                        {
                            Response.Redirect("http://www.joinourspace.eu/Dom%C5%AF/tabid/101/language/cs-CZ/Default.aspx");
                        }
                    }


                    string currentCulture = CultureInfo.CurrentCulture.Name;
                    if (Request.Cookies["language"] != null)
                    {
                        currentCookieCulture = Request.Cookies["language"].Value;
                    }
                    string preferredLocale = UserInfo.Profile.GetPropertyValue("PreferredLocale");

                    lblCulture.Text = currentCulture;
                    lblCookie.Text = currentCookieCulture;
                    lblLangSetting.Text = preferredLocale;

                    if (preferredLocale != "")
                    {
                        if (currentCulture != preferredLocale || currentCookieCulture != preferredLocale)
                        {
                            if (Response.Cookies["language"] != null)
                            {
                                Response.Cookies["language"].Value = preferredLocale;
                            }
                            if (Request.QueryString["language"] != null)
                            {
                                Response.Redirect(Request.Url.AbsoluteUri.Replace(currentCulture, preferredLocale));
                            }
                        }
                    }
                    if (Request.QueryString["language"] != null)
                    {
                        string queryLanguage = Request.QueryString["language"];
                        if (queryLanguage != preferredLocale)
                        {
                            Response.Redirect(Request.Url.AbsoluteUri.Replace(currentCulture, preferredLocale));
                        }
                    }
                    //if (forceRedirect)
                    //{
                    //    string url = Request.Url.AbsoluteUri;
                    //    url = ReplaceQueryString(url, "newLang", "en-GB");
                    //    url = url.Replace("&newLang=en-GB", "");
                    //    url = url.Replace("?newLang=en-GB", "");
                    //    Response.Redirect(url);
                    //}
                }
                //else
                //{
                //    string cookieLocale = "";
                //    if (Request.Url.AbsoluteUri.Contains("language="))
                //    {

                //        //Response.Redirect(Request.Url.AbsoluteUri.Replace("language=en-GB", "ign=en-GB"));
                //        //Response.Redirect(Request.Url.AbsoluteUri.Replace("language=el-GR", "ign=el-GR"));
                //        //Response.Redirect(Request.Url.AbsoluteUri.Replace("language=de-AT", "ign=de-AT"));
                //        //Response.Redirect(Request.Url.AbsoluteUri.Replace("language=cs-CZ", "ign=cs-CZ"));

                //        if (Response.Cookies["language"] != null)
                //        {
                //          cookieLocale =   Response.Cookies["language"].Value;
                //        }
                //        Response.Redirect(Request.Url.AbsoluteUri.Replace("language", "ign"));
                //        //Response.Redirect(Request.Url.AbsoluteUri.Replace("language", "ign"));
                //    }
                //    else if (Request.Url.AbsoluteUri.Contains("language/"))
                //    {

                //        //Response.Redirect(Request.Url.AbsoluteUri.Replace("language/en-GB", "ign/en-GB"));
                //        //Response.Redirect(Request.Url.AbsoluteUri.Replace("language/el-GR", "ign/el-GR"));
                //        //Response.Redirect(Request.Url.AbsoluteUri.Replace("language/de-AT", "ign/de-AT"));
                //        //Response.Redirect(Request.Url.AbsoluteUri.Replace("language/cs-CZ", "ign/cs-CZ"));
                //        if (Response.Cookies["language"] != null)
                //        {
                //            cookieLocale = Response.Cookies["language"].Value;
                //        }
                //        Response.Redirect(Request.Url.AbsoluteUri.Replace("language", "ign"));
                //    }


                //}
                //else
                //{
                //    if (Request.Cookies["language"] != null)
                //    {
                //        currentCookieCulture = Request.Cookies["language"].Value;


                //        if (Request.QueryString["language"] != null)
                //        {
                //            string queryLanguage = Request.QueryString["language"];
                //            if (queryLanguage != currentCookieCulture)
                //            {
                //                string currentCulture = CultureInfo.CurrentCulture.Name;
                //                if (Session["redirectCount"] != null)
                //                {
                //                    Session["redirectCount"] = 1;
                //                    Response.Redirect(Request.Url.AbsoluteUri.Replace(queryLanguage, currentCookieCulture));
                //                }
                //            }
                //        }
                //    }

                //}

                //Session["redirectCount"] = null;

                // Fix for Home Page language problem
                lblAbsoluteUri.Text = Request.Url.AbsoluteUri;
                if (UserId > 2 && Session["userRedirected"] == null)
                {
                    string preferredLocale = UserInfo.Profile.GetPropertyValue("PreferredLocale");
                    if (Request.Url.AbsoluteUri == "http://www.joinourspace.eu/Home/tabid/41/language/en-GB/default.aspx")
                    {
                        if (preferredLocale == "el-GR")
                        {
                            //Response.Redirect("http://www.joinourspace.eu/%CE%91%CF%81%CF%87%CE%B9%CE%BA%CE%AE/tabid/88/language/el-GR/Default.aspx");
                        }
                        else if (preferredLocale == "de-AT")
                        {
                            //Response.Redirect("http://www.joinourspace.eu/%C3%9Cbersicht/tabid/170/language/de-AT/Default.aspx");
                        }
                        else if (preferredLocale == "cs-CZ")
                        {
                            //Response.Redirect("http://www.joinourspace.eu/Dom%C5%AF/tabid/101/language/cs-CZ/Default.aspx");
                        }

                    }


                }




                //http://www.joinourspace.eu/%CE%91%CF%81%CF%87%CE%B9%CE%BA%CE%AE/tabid/88/language/el-GR/Default.aspx
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        protected void SetUserLanguage(string newLocale)
        {
            //UserInfo.Profile.InitialiseProfile(PortalSettings.PortalId); 
            UserInfo.Profile.SetProfileProperty("PreferredLocale", newLocale);
            DotNetNuke.Entities.Users.UserController.UpdateUser(0, UserInfo);
        }

        protected void SetLanguageSwitcherUrls(bool isFacebook)
        {
            //string url = Request.Url.AbsoluteUri;

            string urlEnGb = "";
            string urlElGr = "";
            string urlDeAt = "";
            string urlCsCz = "";

            if (isFacebook)
            {
                string dashboardUrl = DotNetNuke.Common.Globals.NavigateURL(251);
                urlEnGb = dashboardUrl;
                urlElGr = dashboardUrl;
                urlDeAt = dashboardUrl;
                urlCsCz = dashboardUrl;
            }
            else
            {
                //First Home Page
                //urlEnGb = DotNetNuke.Common.Globals.NavigateURL(41);
                //urlElGr = DotNetNuke.Common.Globals.NavigateURL(88);
                //urlDeAt = DotNetNuke.Common.Globals.NavigateURL(170);
                //urlCsCz = DotNetNuke.Common.Globals.NavigateURL(101);

                //Promo Register Page
                urlEnGb = DotNetNuke.Common.Globals.NavigateURL(307);
                urlElGr = DotNetNuke.Common.Globals.NavigateURL(308);
                urlDeAt = DotNetNuke.Common.Globals.NavigateURL(310);
                urlCsCz = DotNetNuke.Common.Globals.NavigateURL(309);
                // 307
                // 308
                // 309

            }

            //string url = Request.Url.AbsoluteUri;//
            //ReplaceQueryString(url, "newLang", "en-GB");
            if (Request.QueryString["newLang"] != null)
            {
                hprlnk_ChangeElGr.NavigateUrl = ReplaceQueryString(urlElGr, "newLang", "el-GR");
                hprlnk_ChangeEnGb.NavigateUrl = ReplaceQueryString(urlEnGb, "newLang", "en-GB");
                hprlnk_ChangeDeAt.NavigateUrl = ReplaceQueryString(urlDeAt, "newLang", "de-AT");
                hprlnk_ChangeCsCz.NavigateUrl = ReplaceQueryString(urlCsCz, "newLang", "cs-CZ");
                if (isFacebook)
                {
                    hprlnk_ChangeElGr.NavigateUrl += "?facebook=1";
                    hprlnk_ChangeEnGb.NavigateUrl += "?facebook=1";
                    hprlnk_ChangeDeAt.NavigateUrl += "?facebook=1";
                    hprlnk_ChangeCsCz.NavigateUrl += "?facebook=1";
                }
            }
            else
            {
                //if (url.Contains("?"))
                //{
                //    hprlnk_ChangeElGr.NavigateUrl = urlElGr + "&newLang=el-GR";
                //    hprlnk_ChangeEnGb.NavigateUrl = urlEnGb + "&newLang=en-GB";
                //    hprlnk_ChangeDeAt.NavigateUrl = urlDeAt + "&newLang=de-AT";
                //    hprlnk_ChangeCsCz.NavigateUrl = urlCsCz + "&newLang=cs-CZ";
                //}
                //else
                //{

                hprlnk_ChangeElGr.NavigateUrl = urlElGr + "?newLang=el-GR";
                hprlnk_ChangeEnGb.NavigateUrl = urlEnGb + "?newLang=en-GB";
                hprlnk_ChangeDeAt.NavigateUrl = urlDeAt + "?newLang=de-AT";
                hprlnk_ChangeCsCz.NavigateUrl = urlCsCz + "?newLang=cs-CZ";

                //hprlnk_ChangeElGr.NavigateUrl = urlElGr;
                //hprlnk_ChangeEnGb.NavigateUrl = urlEnGb;
                //hprlnk_ChangeDeAt.NavigateUrl = urlDeAt;
                //hprlnk_ChangeCsCz.NavigateUrl = urlCsCz;
                if (isFacebook)
                {
                    hprlnk_ChangeElGr.NavigateUrl += "&facebook=1";
                    hprlnk_ChangeEnGb.NavigateUrl += "&facebook=1";
                    hprlnk_ChangeDeAt.NavigateUrl += "&facebook=1";
                    hprlnk_ChangeCsCz.NavigateUrl += "&facebook=1";
                }
                //}

            }
            hprlnk_ChangeElGr.NavigateUrl = ReplaceQueryString(hprlnk_ChangeElGr.NavigateUrl, "language", "el-GR");// 88
            hprlnk_ChangeEnGb.NavigateUrl = ReplaceQueryString(hprlnk_ChangeEnGb.NavigateUrl, "language", "en-GB"); // 41
            hprlnk_ChangeDeAt.NavigateUrl = ReplaceQueryString(hprlnk_ChangeDeAt.NavigateUrl, "language", "de-AT"); // 170
            hprlnk_ChangeCsCz.NavigateUrl = ReplaceQueryString(hprlnk_ChangeCsCz.NavigateUrl, "language", "cs-CZ"); //101


        }

        static string ReplaceQueryString(string url, string key, string value)
        {
            return Regex.Replace(
                url,
                @"([?&]" + key + ")=[^?&]+",
                "$1=" + value);
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
