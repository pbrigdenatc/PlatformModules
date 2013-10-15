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


namespace DotNetNuke.Modules.Ourspace_LoginPageInfo
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_LoginPageInfo class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_LoginPageInfoModuleBase, IActionable
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
                if(Request.QueryString["refx"] != null)
                {
                    Session["referralUserId"] = Request.QueryString["refx"].ToString();
                }
                    Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "script1", (this.TemplateSourceDirectory + "/js/loginPageInfo.js?v=1"));
                imgExpress.ImageUrl = ResolveUrl("~/DesktopModules/Ourspace_LoginPageInfo/Images/express.png");
                imgDebate.ImageUrl = ResolveUrl("~/DesktopModules/Ourspace_LoginPageInfo/Images/debate.png");
                imgMeet.ImageUrl = ResolveUrl("~/DesktopModules/Ourspace_LoginPageInfo/Images/meet.png");


                // To prevent users from going to the registration page when logged in
                if (UserId > 3)
                {
                    string preferredLocale = CultureInfo.CurrentCulture.Name;
                  //  Session["userRedirected"] = null;
                   // Session["atc_pageVisited"] = null;


                    if (Request.QueryString["newLang"] != null)
                    {
                       // SetUserLanguage(Request.QueryString["newLang"]);
                        string overrideLang = Request.QueryString["newLang"].ToString();
                        if (overrideLang == "en-GB")
                        {
                            Response.Redirect("http://www.joinourspace.eu/Home/tabid/41/language/en-GB/default.aspx?newLang=en-GB");
                        }
                        else if (overrideLang == "el-GR")
                        {
                            Response.Redirect("http://www.joinourspace.eu/%CE%91%CF%81%CF%87%CE%B9%CE%BA%CE%AE/tabid/88/language/el-GR/Default.aspx?newLang=el-GR");
                        }
                        else if (overrideLang == "de-AT")
                        {
                            Response.Redirect("http://www.joinourspace.eu/%C3%9Cbersicht/tabid/170/language/de-AT/Default.aspx?newLang=de-AT");
                        }
                        else if (overrideLang == "cs-CZ")
                        {
                            Response.Redirect("http://www.joinourspace.eu/Dom%C5%AF/tabid/101/language/cs-CZ/Default.aspx?newLang=cs-CZ");
                        }
                    }
                    else
                    {
                        if (preferredLocale == "en-GB")
                        {
                            Response.Redirect("http://www.joinourspace.eu/Home/tabid/41/language/en-GB/default.aspx");
                        }
                        else if (preferredLocale == "el-GR")
                        {
                            Response.Redirect("http://www.joinourspace.eu/%CE%91%CF%81%CF%87%CE%B9%CE%BA%CE%AE/tabid/88/language/el-GR/Default.aspx");
                        }
                        else if (preferredLocale == "de-AT")
                        {
                            Response.Redirect("http://www.joinourspace.eu/%C3%9Cbersicht/tabid/170/language/de-AT/Default.aspx");
                        }
                        else if (preferredLocale == "cs-CZ")
                        {
                            Response.Redirect("http://www.joinourspace.eu/Dom%C5%AF/tabid/101/language/cs-CZ/Default.aspx");
                        }
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

    }

}
