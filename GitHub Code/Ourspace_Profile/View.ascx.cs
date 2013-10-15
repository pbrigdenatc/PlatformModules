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
using DotNetNuke.Common;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;
using System.Web.UI;
using DotNetNuke.Common.Lists;
using DotNetNuke.Entities.Users;
using System.Drawing;


namespace DotNetNuke.Modules.Ourspace_Profile
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewOurspace_Profile class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Ourspace_ProfileModuleBase, IActionable
    {

        int SQUARE_THUMB_SIDE = 50;
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
                // Adding js to page
                ScriptManager objScriptManager = ScriptManager.GetCurrent(this.Page);
                ScriptReference objScriptReference;

                objScriptReference = new ScriptReference(@"~/DesktopModules/Ourspace_Profile/js/jquery-ui-1.8.14.custom.min.js");
                objScriptManager.Scripts.Add(objScriptReference);
                objScriptReference = new ScriptReference(@"~/DesktopModules/Ourspace_Profile/js/textboxLimiter.js");
                objScriptManager.Scripts.Add(objScriptReference);


                
                
                hdnfld_viewingUserID.Value = UserId.ToString();

                // Checking if we are viewing someone elses profile or not
                if (Request.QueryString["user"] == null)
                {
                    hdnfld_profileUserID.Value = UserId.ToString();
                }
                else
                {
                    hdnfld_profileUserID.Value = Request.QueryString["user"];
                }
                // Getting the info of the user of the profile we are viewing
                UserInfo userInfo = UserController.GetUserById(PortalId, Convert.ToInt32(hdnfld_profileUserID.Value));
                
                // Viewing own profile
                if (hdnfld_profileUserID.Value == hdnfld_viewingUserID.Value)
                {
                    lbl_whosProfile.Text = "Your own profile.";
                    lnkbtn_editProfileSettings.Visible = true;
                }
                // Viewing other user's profile
                else
                {
                    lbl_whosProfile.Text = "Someone elses profile.";
                    
                    lnkbtn_ChangePhoto.Visible = false;
                    lnkbtn_EditProfile.Visible = false;
                    // Checking friendship status
                    DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();
                    System.Data.IDataReader dr = dp.ExecuteSQL("SELECT * FROM Ourspace_Relationships WHERE (friendshipRequester = " + UserId + " AND friendshipRequestee = " + userInfo.UserID + " AND isRequest = 1) OR ((friendshipRequester = " + userInfo.UserID + " AND friendshipRequestee = " + UserId + " AND isRequest = 1))");
                    //System.Data.IDataReader dr = dp.ExecuteSQL("SELECT * FROM Ourspace_Relationships WHERE (friendshipRequester = " + userInfo.UserID + " AND friendshipRequestee = " + UserId + " AND isRequest = 1)");
                    bool friendsProfile = false;
                    if (dr.Read())
                    {
                      // Are they friends?
                      if (dr["relationshipStatusID"].ToString() == "1")
                      {
                          // Friends!
                          lbl_AlreadyFriends.Visible = true;
                          lnkbtn_SendUserAMessage.Visible = true;
                          txt_Message.Visible = true;
                          friendsProfile = true;

                      }
                      else if(dr["friendshipRequester"].ToString() == hdnfld_viewingUserID.Value)
                      {
                          // Current user has already sent a friendship request
                          lbl_CurrentUserAlreadyRequestedFriendship.Visible = true;
                         
                      }
                      else if (dr["friendshipRequester"].ToString() == hdnfld_profileUserID.Value)
                      {
                          // Current user has not accepted the user's friendship request
                          lbl_CurrentUserHasNotAcceptedFriendship.Visible = true;
                          lnkbtn_AcceptFriendship.Visible = true;
                      }
                      
                  }
                    else
                  {
                      lnkbtn_AddFriend.Visible = true;
                  }
                    // If the user isn't friends with the viewing user then we check if
                    // the profile user allows non-friends to view his/her profile.
                    if (!friendsProfile)
                    {
                        string visibility = "0";
                        DotNetNuke.Data.DataProvider dp2 = DotNetNuke.Data.DataProvider.Instance();
                        System.Data.IDataReader dr2 = dp.ExecuteSQL("SELECT * FROM Ourspace_Forum_User_Info WHERE userId=" + hdnfld_profileUserID.Value);
                        if (dr2.Read())
                        {
                            visibility = dr2["profileVisibility"].ToString();
                            // Visibility = 0 means profile only visible to friends
                            // Visibility = 1 means profile visible to everyone
                            
                        }
                        if (visibility == "0")
                        {
                            pnl_privateInfo1.Visible = false;
                            pnl_privateInfo2.Visible = false;
                            pnl_privateInfo3.Visible = false;
                            lbl_ProfileNotVisible.Visible = true;
                        }
                       
                            
                       
                    }
                }

                System.Web.UI.Control ctl = Globals.FindControlRecursiveDown(this.ContainerControl, "lblTitle");
                
                if ((ctl != null))
                {
                    ((Label)ctl).Text = userInfo.Username;
                }
                if (!IsPostBack)
                {
                    

                    //DotNetNuke.Entities.Profile.ProfileController.ClearProfileDefinitionCache(PortalId);
                    
                    //string biography = UserInfo.Profile.GetPropertyValue("Biography");
                   lbl_FirstNameValue.Text = userInfo.Profile.FirstName;
                    lbl_RankingValue.Text = "4th";
                    lbl_PointsValue.Text = "33294";
                    lbl_LastNameValue.Text = userInfo.Profile.LastName;
                    lbl_WebsiteValue.Text = userInfo.Profile.GetPropertyValue("Website");
                    lbl_AboutMeValue.Text = userInfo.Profile.GetPropertyValue("Biography");

                    string dob = userInfo.Profile.GetPropertyValue("DOB");
                    if (userInfo.Profile.ProfileProperties.GetByName("DOB") != null)
                    {
                        lbl_AgeValue.Text = userInfo.Profile.ProfileProperties.GetByName("DOB").PropertyValue;
                    }
                    lbl_CityValue.Text = userInfo.Profile.City;
                    lbl_CountryValue.Text = userInfo.Profile.Country;
                    // lbl_DisplayName.Text = UserInfo.DisplayName;

                    //img_ProfileImage.ImageUrl = DotNetNuke.Common.Globals.LinkClick("FileID=" + UserInfo.Profile.Photo, TabId, ModuleId, false);
                    string strPath = Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + userInfo.UserID.ToString("000") + "\\"); 
                    if (Directory.Exists(strPath))
                    {
                        if (UserId <= 9)
                        {
                            strPath = (ResolveUrl("~/Portals/" + PortalId + "/Users/" + userInfo.UserID.ToString("000") + "/" + userInfo.UserID.ToString("00")));
                        }
                        else
                        {
                            strPath = ResolveUrl("~/Portals/" + PortalId + "/Users/" + userInfo.UserID.ToString("000") + "/" + userInfo.UserID);
                        }
                        strPath += "/" + userInfo.UserID + "/" + userInfo.UserID + "_180.jpg?" + DateTime.Now.Ticks;
                        //ResolveUrl("~/images/help.gif");
                        img_ProfileImage.ImageUrl = strPath;
                    }
                    else
                    {
                        img_ProfileImage.ImageUrl = userInfo.Profile.PhotoURL;
                    }
                    lbl_friends.Text += " ("+GetFriendsCount()+")";



                }

                //img_ProfileImage.ImageUrl = Server.MapPath(".\\Portals\\" + PortalId + "\\OurspaceUsers\\" + UserId + UserInfo.Profile.Photo);


                //lbl_CountryValue.Text += DotNetNuke.Common.Globals.LinkClick("FileID=" + UserInfo.Profile.Photo, TabId, ModuleId, false);

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

        protected void lnkbtn_EditProfile_Click(object sender, EventArgs e)
        {
            // string navurl = DotNetNuke.Common.Globals.NavigateURL(tabid, "", query);
            //HttpContext.Current.Response.Redirect(navurl);
            lnkbtn_UpdateProfile.Visible = true;
            lnkbtn_EditProfile.Visible = false;
            lnkbtn_editProfileSettings.Visible = false;

            txt_City.Visible = true;
            txt_City.Text = lbl_CityValue.Text;
            lbl_CityValue.Visible = false;

            ddlCountries.Visible = true;
            lbl_CountryValue.Visible = false;
            //txt_Country.Text = lbl_CountryValue.Text;


            // txt_FirstName.Visible = true;
            // lbl_FirstNameValue.Visible = false;
            // txt_FirstName.Text = lbl_FirstNameValue.Text;

            // txt_MiddleName.Visible = true;
            // lbl_MiddleNameValue.Visible = false;
            // txt_MiddleName.Text = lbl_MiddleNameValue.Text;

            txt_AboutMe.Visible = true;
            lbl_AboutMeValue.Visible = false;
            txt_AboutMe.Text = lbl_AboutMeValue.Text;

            // txt_LastName.Visible = true;
            // lbl_LastNameValue.Visible = false;
            // txt_LastName.Text = lbl_LastNameValue.Text;


            txt_Website.Visible = true;
            lbl_WebsiteValue.Visible = false;
            txt_Website.Text = lbl_WebsiteValue.Text;

            txt_DOB.Visible = true;
            lbl_AgeValue.Visible = false;
            txt_DOB.Text = lbl_AgeValue.Text;

            ListController lc = new ListController();

            ListEntryInfoCollection leic = lc.GetListEntryInfoCollection("Country", "", "");
            ddlCountries.DataTextField = "Text";
            ddlCountries.DataValueField = "Value";
            ddlCountries.DataSource = leic;
            ddlCountries.DataBind();

            int selectedIndex = 0;
            int i = 0;
            foreach (ListItem item in ddlCountries.Items)
            {

                if (item.Text == lbl_CountryValue.Text)
                {
                    selectedIndex = i;
                }
                i++;
            }
            ddlCountries.SelectedIndex = selectedIndex;
            lnkbtn_cancel.Visible = true;
            lnkbtn_ChangePhoto.Visible = false;
            //ddlCountries.Items.Insert(0, new ListItem("Select Country", "-1"));

        }

        protected void lnkbtn_uploadPhoto_Click(object sender, EventArgs e)
        {
            //flupld_ProfileImage.PostedFile.SaveAs(Server.MapPath(".\\") + flupld_ProfileImage.PostedFile.FileName);

            string extension = Path.GetExtension(flupld_ProfileImage.FileName);
            UserInfo.Profile.SetProfileProperty("Photo", extension);
            string strPath = Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\");
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\"));
                if (UserId <= 9)
                {
                    Directory.CreateDirectory(Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\" + UserId.ToString("00")));
                    strPath = (Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\" + UserId.ToString("00")));
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\" + UserId));
                    strPath = Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\" + UserId);
                }
                Directory.CreateDirectory(strPath + "\\" + UserId);
                strPath += "\\" + UserId;
            }
            else
            {
                if (UserId <= 9)
                {
                    strPath = (Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\" + UserId.ToString("00")));
                }
                else
                {
                    strPath = Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + UserId.ToString("000") + "\\" + UserId);
                }
                strPath += "\\" + UserId;
            }
            string  strPathThumbToCropSmall = strPath + "\\"+UserId + "_thumbToCropSmall"+extension;
            //string strPathThumbToCropTiny = strPath + "\\" + UserId + "_thumbToCropTiny" + extension;
            string strPathThumb = strPath + "\\" + UserId + "_180" + extension;
            //string strPathThumbFinal = strPath + "\\" + UserId + "thumbfinal" + extension;
            string  strPathCropped = strPath + "\\"+UserId + "_50"+extension;
            string strPathCropped32 = strPath + "\\" + UserId + "_32" + extension;
            strPath += "\\" + UserId + extension;
            
            flupld_ProfileImage.PostedFile.SaveAs(strPath);

            // First we resize the image, making the smaller side 50px and the other side
            // based on the picture ratio.

            System.Drawing.Image img = System.Drawing.Image.FromFile(strPath);
            int resizeHeight = 0;
            int resizeWidth = 0;

            double ratio = Convert.ToDouble( img.Height) / Convert.ToDouble(img.Width);
            if (img.Height < img.Width)
            {
                resizeHeight = SQUARE_THUMB_SIDE;
                resizeWidth = Convert.ToInt32(Convert.ToDouble(SQUARE_THUMB_SIDE) / ratio);
            }
            else
            {
                resizeWidth = SQUARE_THUMB_SIDE;
                resizeHeight = Convert.ToInt32(ratio * Convert.ToDouble(SQUARE_THUMB_SIDE));
            }
            img = null;

            ResizePicture(strPath, strPathThumbToCropSmall, new Size(resizeWidth, resizeHeight));

            ResizePicture(strPath, strPathThumb, new Size(180, Convert.ToInt32( 180*ratio)));
            //int thumbHeight = Convert.ToInt32(181 * ratio);
            //cropImage(strPathThumb, new Rectangle(0, 0, 180, thumbHeight-1)).Save(strPathThumbFinal);

            cropImage(strPathThumbToCropSmall, new Rectangle(0, 0, SQUARE_THUMB_SIDE, SQUARE_THUMB_SIDE)).Save(strPathCropped);
            ResizePicture(strPathCropped, strPathCropped32, new Size(32, 32));
            
            //squareImg.Save(path.Replace(".jpg","square.jpg"))
            Response.Redirect(Request.Url.ToString());
        }

        protected void lnkbtn_ChangePhoto_Click(object sender, EventArgs e)
        {
            lnkbtn_editProfileSettings.Visible = false;
            lnkbtn_cancel.Visible = true;
            lnkbtn_EditProfile.Visible = false;
            flupld_ProfileImage.Visible = true;
            lnkbtn_ChangePhoto.Visible = false;
            lnkbtn_uploadPhoto.Visible = true;
            lblOnlyJpeg.Visible = true;
        }

        protected void lnkbtn_UpdateProfile_Click(object sender, EventArgs e)
        {
            UserInfo.Profile.InitialiseProfile(PortalSettings.PortalId);


            UserInfo.Profile.SetProfileProperty("DOB", txt_DOB.Text);
            UserInfo.Profile.SetProfileProperty("OurSpaceName", txt_FirstName.Text);
            UserInfo.Profile.SetProfileProperty("OurSpaceMiddleName", txt_MiddleName.Text);
            UserInfo.Profile.SetProfileProperty("OurSpaceLastName", txt_LastName.Text);
            UserInfo.Profile.SetProfileProperty("City", txt_City.Text);
            UserInfo.Profile.SetProfileProperty("Country", ddlCountries.SelectedItem.Text);
            UserInfo.Profile.SetProfileProperty("Website", txt_Website.Text);
            UserInfo.Profile.SetProfileProperty("Biography", txt_AboutMe.Text);











            // DotNetNuke.Entities.Profile.ProfileController.

            DotNetNuke.Entities.Profile.ProfileController.UpdateUserProfile(UserInfo);
            var test = UserInfo.Profile.ProfileProperties;

            Response.Redirect(Request.Url.ToString());
        }

        protected void lnkbtn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }

        protected void lstvw_recentPosts_DataBound(object sender, EventArgs e)
        {

            //Return url


        }

        protected void lstvw_recentPosts_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label PostIDLabel = (Label)e.Item.FindControl("PostIDLabel");
            Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
            Label ForumIDLabel = (Label)e.Item.FindControl("ForumIDLabel");
            Label BodyLabel = (Label)e.Item.FindControl("BodyLabel");
            Label CreatedDateLabel = (Label)e.Item.FindControl("CreatedDateLabel");

            string[] dateArr = CreatedDateLabel.Text.Split(' ');
            CreatedDateLabel.Text = dateArr[0] + " @ " + dateArr[1];

            if (BodyLabel.Text.Length > 100)
          {
              BodyLabel.Text = BodyLabel.Text.Substring(0, 99) + "..";
            }
            
            HyperLink hprlnk_post = (HyperLink)e.Item.FindControl("hprlnk_post");
            if (PostIDLabel != null)
            {
                string url = "";
                string[] parameters = new string[3];

                parameters = new string[3] { "forumid=" + ForumIDLabel.Text, "threadid=" + ThreadIDLabel.Text, "scope=posts" };
                //url = NavigateURL(TabId, "", parameters);
                url = DotNetNuke.Common.Globals.NavigateURL(62, "", parameters);
                hprlnk_post.NavigateUrl = url + "#" + PostIDLabel.Text;
            }
        }

        protected void lnkbtn_AddFriend_Click(object sender, EventArgs e)
        {
            try
            {

                DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();
                dp.ExecuteSQL("INSERT INTO Ourspace_Relationships (FriendshipRequester, FriendshipRequestee, relationshipStatusID,isRequest) VALUES ("+UserId+","+hdnfld_profileUserID.Value+",0,1)");
                dp.ExecuteSQL("INSERT INTO Ourspace_Relationships (FriendshipRequester, FriendshipRequestee, relationshipStatusID,isRequest) VALUES (" + hdnfld_profileUserID.Value + "," + UserId + ",0,0)");
                lbl_FriendshipRequestSent.Visible = true;
                lnkbtn_AddFriend.Visible = false;
            }
            catch (Exception ex)
            {
              string mess =  ex.Message;
            }
        }

        protected void lnkbtn_AcceptFriendship_Click(object sender, EventArgs e)
        {
            DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();
            dp.ExecuteSQL("UPDATE Ourspace_Relationships SET relationshipStatusID = 1 WHERE (friendshipRequester="+hdnfld_profileUserID.Value +" AND friendshipRequestee="+hdnfld_viewingUserID.Value+")");
            dp.ExecuteSQL("UPDATE Ourspace_Relationships SET relationshipStatusID = 1 WHERE (friendshipRequester=" + hdnfld_viewingUserID.Value + " AND friendshipRequestee=" + hdnfld_profileUserID.Value + ")");
            Response.Redirect(Request.Url.ToString());
        }

        //protected void lstvw_friends_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    HyperLink hprlnk_userLink = (HyperLink)e.Item.FindControl("hprlnk_userLink");
        //    Label friendshipRequesterLabel = (Label) e.Item.FindControl("friendshipRequesterLabel");
        //   Label friendshipRequesteeLabel = (Label) e.Item.FindControl("friendshipRequesteeLabel");
        //   Label lbl_FriendDisplayName = (Label)e.Item.FindControl("lbl_FriendDisplayName");
        //   if (hdnfld_profileUserID.Value == friendshipRequesterLabel.Text)
        //   {
        //       UserInfo userInfo = UserController.GetUserById(PortalId, Convert.ToInt32(friendshipRequesteeLabel.Text));
        //       lbl_FriendDisplayName.Text = userInfo.FirstName + " " + userInfo.LastName;
        //       hprlnk_userLink.NavigateUrl = userInfo.UserID.ToString();

        //   }
        //   else
        //   {
        //       UserInfo userInfo = UserController.GetUserById(PortalId, Convert.ToInt32(friendshipRequesterLabel.Text));
        //       lbl_FriendDisplayName.Text = userInfo.FirstName + " " + userInfo.LastName;
        //       hprlnk_userLink.NavigateUrl = userInfo.UserID.ToString();
        //   }
        //}

        protected void lstvw_messages_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
          // //Label PostIDLabel = (Label)e.Item.FindControl("PostIDLabel");
          // Label ThreadIDLabel = (Label)e.Item.FindControl("ThreadIDLabel");
           Label ConversationIDLabel = (Label)e.Item.FindControl("ConversationIDLabel");
            Label BodyLabel = (Label)e.Item.FindControl("BodyLabel");
           Label DateLabel = (Label)e.Item.FindControl("DateLabel");

           string[] dateArr = DateLabel.Text.Split(' ');
           DateLabel.Text = dateArr[0] + " @ " + dateArr[1];

            if (BodyLabel.Text.Length > 100)
            {
                BodyLabel.Text = BodyLabel.Text.Substring(0, 99) + "..";
            }

            HyperLink hprlnk_conversation = (HyperLink)e.Item.FindControl("hprlnk_conversation");
            if (ConversationIDLabel != null)
            {
                string url = "";
                string[] parameters = new string[1];

                parameters = new string[1] { "conversation=" + ConversationIDLabel.Text};
                //url = NavigateURL(TabId, "", parameters);
                url = DotNetNuke.Common.Globals.NavigateURL(69, "", parameters);
                hprlnk_conversation.NavigateUrl = url;// +"#" + PostIDLabel.Text;
            }
        }

        protected void lnkbtn_SendUserAMessage_Click(object sender, EventArgs e)
        {
            DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();
            System.Data.IDataReader dr = dp.ExecuteSQL("INSERT INTO Ourspace_Messages (FromUserID, ToUserID, Date, IsRead, Body, ConversationID) VALUES (" + UserId + "," + hdnfld_profileUserID.Value + ",'" + DateTime.Now.ToString(new System.Globalization.CultureInfo("EN-us")) + "',0,'" + txt_Message.Text + "'," + "-1) SELECT SCOPE_IDENTITY()");
            string conversationID = "";
            if (dr.Read())
            {
                conversationID = dr[0].ToString();
            }
            dp.ExecuteSQL("UPDATE Ourspace_Messages SET conversationID = " + conversationID + " WHERE MessageID ="+conversationID);
            //lstvw_conversation.DataBind();
        }


        public void ResizePicture(string originalpath, string newpath, Size newsize)
        {
            using (Bitmap newbmp = new Bitmap(newsize.Width, newsize.Height), oldbmp = Bitmap.FromFile(originalpath) as Bitmap)
            {
                using (Graphics newgraphics = Graphics.FromImage(newbmp))
                {
                    newgraphics.Clear(Color.FromArgb(-1));
                    if ((float)oldbmp.Width / (float)newsize.Width == (float)oldbmp.Height / (float)newsize.Height) //Target size has a 1:1 aspect ratio
                    {
                        newgraphics.DrawImage(oldbmp, 0, 0, newsize.Width, newsize.Height);
                    }

                    else if ((float)oldbmp.Width / (float)newsize.Width > (float)oldbmp.Height / (float)newsize.Height) //There will be white space on the top and bottom
                    {
                        newgraphics.DrawImage(oldbmp, 0f, (float)newbmp.Height / 2f - (oldbmp.Height * ((float)newbmp.Width / (float)oldbmp.Width)) / 2f, (float)newbmp.Width, oldbmp.Height * ((float)newbmp.Width / (float)oldbmp.Width));
                    }

                    else if ((float)oldbmp.Width / (float)newsize.Width < (float)oldbmp.Height / (float)newsize.Height) //There will be white space on the sides
                    {
                        newgraphics.DrawImage(oldbmp, (float)newbmp.Width / 2f - (oldbmp.Width * ((float)newbmp.Height / (float)oldbmp.Height)) / 2f, 0f, oldbmp.Width * ((float)newbmp.Height / (float)oldbmp.Height), (float)newbmp.Height);
                    }

                    newgraphics.Save();
                    newbmp.Save(newpath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }

        private static System.Drawing.Image cropImage(string path, Rectangle cropArea)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(path); 

            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea,
            bmpImage.PixelFormat);
            //System.Drawing.Image squareImg = (System.Drawing.Image)(bmpCrop);
            return (System.Drawing.Image)(bmpCrop);
        }

        protected void lnkbtn_editProfileSettings_Click(object sender, EventArgs e)
        {
            lnkbtn_ChangePhoto.Visible = false;
            lnkbtn_EditProfile.Visible = false;
            lnkbtn_editProfileSettings.Visible = false;
            ddl_profileVisibility.Visible = true;
            lbl_EditSettingsVisibility.Visible = true;
            lnkbtn_saveProfileSettings.Visible = true;
            lnkbtn_CancelEditSettings.Visible = true;

            DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();

            System.Data.IDataReader dr = dp.ExecuteSQL("SELECT * FROM Ourspace_Forum_User_Info WHERE userID =" + UserId);
            string visibility = "0";
            if (dr.Read())
            {
                visibility = dr["profileVisibility"].ToString();
            }
            

            int selectedIndex = 0;
            int i = 0;
            foreach (ListItem item in ddl_profileVisibility.Items)
            {

                if (item.Value == visibility)
                {
                    selectedIndex = i;
                }
                i++;
            }
            ddl_profileVisibility.SelectedIndex = selectedIndex;
        }

        protected void lnkbtn_saveProfileSettings_Click(object sender, EventArgs e)
        {
            int visibility = Convert.ToInt32(ddl_profileVisibility.SelectedValue);

            // Check if user has an entry in the user settings table
            DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();
           System.Data.IDataReader dr = dp.ExecuteSQL("SELECT * FROM Ourspace_Forum_User_Info WHERE userId = " + UserId + " AND portalID="+PortalId);
           if (!dr.Read())
           {
               DotNetNuke.Data.DataProvider dp2 = DotNetNuke.Data.DataProvider.Instance();
               System.Data.IDataReader dr2 = dp.ExecuteSQL("INSERT INTO Ourspace_Forum_User_Info (userId,portalID,threadViews,profileVisibility) VALUES (" + hdnfld_profileUserID.Value + "," + PortalId + ",0,0)");
            
           }

            DotNetNuke.Data.DataProvider dp3 = DotNetNuke.Data.DataProvider.Instance();
            dp3.ExecuteSQL("UPDATE Ourspace_Forum_User_Info SET profileVisibility = " + visibility + " WHERE userId =" + UserId);
            
                        
            
            Response.Redirect(Request.Url.ToString());
        }

        protected void lnkbtn_CancelEditSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
           
        }

        protected void lstvw_friends_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            HyperLink hprlnk_userLink = (HyperLink)e.Item.FindControl("hprlnk_userLink");
            Label friendshipRequesterLabel = (Label)e.Item.FindControl("friendshipRequesterLabel");
            Label friendshipRequesteeLabel = (Label)e.Item.FindControl("friendshipRequesteeLabel");
            Label lbl_FriendDisplayName = (Label)e.Item.FindControl("lbl_FriendDisplayName");
            UserInfo userInfo = new UserInfo();
            System.Web.UI.WebControls.Image img_profileMini = (System.Web.UI.WebControls.Image)e.Item.FindControl("img_profileMini");
           
                userInfo = UserController.GetUserById(PortalId, Convert.ToInt32(friendshipRequesteeLabel.Text));
                lbl_FriendDisplayName.Text = userInfo.FirstName + " " + userInfo.LastName;
                hprlnk_userLink.NavigateUrl = userInfo.UserID.ToString();
                img_profileMini.ToolTip = lbl_FriendDisplayName.Text;
            string strPath = Server.MapPath(".\\Portals\\" + PortalId + "\\Users\\" + Convert.ToInt32(userInfo.UserID.ToString()).ToString("000") + "\\"); ;

            if (Directory.Exists(strPath))
            {
                if (UserId <= 9)
                {
                    strPath = (ResolveUrl("~/Portals/" + PortalId + "/Users/" + Convert.ToInt32(userInfo.UserID.ToString()).ToString("000") + "/" + Convert.ToInt32(userInfo.UserID.ToString()).ToString("00")));
                }
                else
                {
                    strPath = ResolveUrl("~/Portals/" + PortalId + "/Users/" + Convert.ToInt32(userInfo.UserID.ToString()).ToString("000") + "/" + userInfo.UserID.ToString());
                }
                strPath += "/" + userInfo.UserID.ToString() + "/" + userInfo.UserID.ToString() + "_50.jpg?" + DateTime.Now.Ticks;
                img_profileMini.ImageUrl = strPath;
            }
            else
            {
                img_profileMini.ImageUrl = ResolveUrl("~/images/no-avatar.png");
            }


        }

        protected int GetFriendsCount()
        {
            DotNetNuke.Data.DataProvider dp = DotNetNuke.Data.DataProvider.Instance();
            System.Data.IDataReader dr = dp.ExecuteSQL("SELECT COUNT(*) FROM Ourspace_Relationships WHERE (friendshipRequester = "+ hdnfld_profileUserID.Value+" ) AND (relationshipStatusID = 1)");
            int count = 0;
            if (dr.Read())
            {
              count =   Convert.ToInt32(dr[0].ToString());
                //DotNetNuke.Data.DataProvider dp2 = DotNetNuke.Data.DataProvider.Instance();
                //System.Data.IDataReader dr2 = dp.ExecuteSQL("INSERT INTO Ourspace_Forum_User_Info (userId,portalID,threadViews,profileVisibility) VALUES (" + hdnfld_profileUserID.Value + "," + PortalId + ",0,0)");

            }
            return count;
        }

        


    }

}
