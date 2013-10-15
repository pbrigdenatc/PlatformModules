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
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;


namespace DotNetNuke.Modules.Ourspace_FB_Test
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_FB_Test class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_FB_TestModuleBase, IActionable
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
                //FacebookApp app = new FacebookApp();
                //Authorizer auth = new Authorizer(app);
                //CanvasAuthorizer auth = new CanvasAuthorizer(app);
                //FacebookWebAuthorizer auth2 = new FacebookWebAuthorizer(
                //var client = new FacebookClient();
                //var me = (IDictionary<string, object>)client.Get("me");
                //string firstName = (string)me["first_name"];
                //string lastName = (string)me["last_name"];
                //string email = (string)me["email"];
                //Label1.Text = email;
               // fbApp = new FacebookApp();
               // authorizer = new CanvasAuthorizer(fbApp);
               // authorizer.Perms = requiredAppPermissions;
               // if (authorizer.Authorize())
               // {
                    //ShowFacebookContent();
               // }
                //if (Request.Params["signed_request"] != null)
                //{
                    
                //}


                //foreach (var thing in Request.Params)
                //{
                //    Label1.Text += thing.ToString();
                //}

               // Label1.Text = Request.Form["signed_request"].ToString();
                //foreach (var item in DecodePayload("4"))
                //{

                //}

                
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public Dictionary<string, string> DecodePayload(string payload)
        {
            var encoding = new UTF8Encoding();
            var decodedJson = payload.Replace("=", string.Empty).Replace('-', '+').Replace('_', '/');
            var base64JsonArray = Convert.FromBase64String(decodedJson.PadRight(decodedJson.Length + (4 - decodedJson.Length % 4) % 4, '='));
            var json = encoding.GetString(base64JsonArray);
            var jObject = JObject.Parse(json);

            var parameters = new Dictionary<string, string>();
            parameters.Add("user_id", (string)jObject["user_id"] ?? "");
            parameters.Add("oauth_token", (string)jObject["oauth_token"] ?? "");
            var expires = ((long?)jObject["expires"] ?? 0);
            parameters.Add("expires", expires > 0 ? expires.ToString() : "");
            parameters.Add("profile_id", (string)jObject["profile_id"] ?? "");

            return parameters;
        }


        /// Example signed_request variable from PHPSDK Unit Testing
        private string VALID_SIGNED_REQUEST = "ZcZocIFknCpcTLhwsRwwH5nL6oq7OmKWJx41xRTi59E.eyJhbGdvcml0aG0iOiJITUFDLVNIQTI1NiIsImV4cGlyZXMiOiIxMjczMzU5NjAwIiwib2F1dGhfdG9rZW4iOiIyNTQ3NTIwNzMxNTJ8Mi5JX2VURmtjVEtTelg1bm8zakk0cjFRX18uMzYwMC4xMjczMzU5NjAwLTE2Nzc4NDYzODV8dUk3R3dybUJVZWQ4c2VaWjA1SmJkekdGVXBrLiIsInNlc3Npb25fa2V5IjoiMi5JX2VURmtjVEtTelg1bm8zakk0cjFRX18uMzYwMC4xMjczMzU5NjAwLTE2Nzc4NDYzODUiLCJ1c2VyX2lkIjoiMTY3Nzg0NjM4NSJ9";

        public bool ValidateSignedRequest()
        {
            string applicationSecret = "904270b68a2cc3d54485323652da4d14";
            string[] signedRequest = VALID_SIGNED_REQUEST.Split('.');
            string expectedSignature = signedRequest[0];
            string payload = signedRequest[1];

            // Attempt to get same hash
            var Hmac = SignWithHmac(UTF8Encoding.UTF8.GetBytes(payload), UTF8Encoding.UTF8.GetBytes(applicationSecret));
            var HmacBase64 = ToUrlBase64String(Hmac);

            return (HmacBase64 == expectedSignature);
        }


        private string ToUrlBase64String(byte[] Input)
        {
            return Convert.ToBase64String(Input).Replace("=", String.Empty)
                                                .Replace('+', '-')
                                                .Replace('/', '_');
        }

        private byte[] SignWithHmac(byte[] dataToSign, byte[] keyBody)
        {
            using (var hmacAlgorithm = new HMACSHA256(keyBody))
            {
                hmacAlgorithm.ComputeHash(dataToSign);
                return hmacAlgorithm.Hash;
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
