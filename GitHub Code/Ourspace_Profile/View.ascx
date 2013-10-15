<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_Profile.View" AutoEventWireup="false"
    CodeBehind="View.ascx.cs" %>
<div class="profile-picture-container">
    <asp:Image ID="img_ProfileImage" runat="server" BorderWidth="1" />
</div>
<asp:Panel ID="pnl_privateInfo3" runat="server" CssClass="profile-details-table-container">


    <table class="profile-details-table">
        <tr>
            <td>
                <b>
                    <asp:Label ID="lbl_FirstName" runat="server" Text="First Name"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lbl_FirstNameValue" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txt_FirstName" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>
                <b>
                    <asp:Label ID="lbl_Country" runat="server" Text="Country"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lbl_CountryValue" runat="server" Text=""></asp:Label><asp:DropDownList
                    ID="ddlCountries" Visible="false" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="lbl_MiddleName" runat="server" Text="Middle Name"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lbl_MiddleNameValue" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txt_MiddleName" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>
                <b>
                    <asp:Label ID="lbl_City" runat="server" Text="City"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lbl_CityValue" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txt_City" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="lbl_LastName" runat="server" Text="Last Name"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lbl_LastNameValue" runat="server" Text=""></asp:Label><asp:TextBox
                    ID="txt_LastName" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>
                <b>
                    <asp:Label ID="lbl_Age" runat="server" Text="Date of Birth"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lbl_AgeValue" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txt_DOB" class="DatePicker" Visible="false" runat="server"></asp:TextBox>
                <script type="text/javascript">
        $(function() {
            $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy',changeMonth: true,
            changeYear: true, yearRange: "-100:+0",
        });
        });

        $('.datepicker[maxLength]').limitMaxlength();
                </script>
            </td>
        </tr>
    </table>
</asp:Panel>


<asp:LinkButton CssClass="Ourspace_ToolbarLink" ID="lnkbtn_AddFriend" runat="server" OnClick="lnkbtn_AddFriend_Click"
    Visible="false">Add Friend</asp:LinkButton>
    <div class="clear"></div>
<asp:TextBox ID="txt_Message" Visible="false" runat="server" TextMode="MultiLine"></asp:TextBox>
<div class="clear"></div>
<asp:LinkButton CssClass="Ourspace_ToolbarLink" ID="lnkbtn_SendUserAMessage" runat="server" 
    onclick="lnkbtn_SendUserAMessage_Click" Visible="false">Send a message</asp:LinkButton>
    <div class="clear"></div>


<br />


<asp:LinkButton ID="lnkbtn_AcceptFriendship" CssClass="Ourspace_ToolbarLink" Visible="false" runat="server" OnClick="lnkbtn_AcceptFriendship_Click">Accept friendship request</asp:LinkButton>
<asp:Label ID="lbl_whosProfile" runat="server" Text="Label"></asp:Label>
<asp:Label ID="lbl_AlreadyFriends" Visible="false" runat="server" Text="You are friends with this user."></asp:Label>
<asp:Label ID="lbl_CurrentUserAlreadyRequestedFriendship" Visible="false" runat="server"
    Text="This user has not replied to your friendship request yet."></asp:Label>
<asp:Label ID="lbl_CurrentUserHasNotAcceptedFriendship" Visible="false" runat="server"
    Text="This user has already sent you a friendship request. Accept now?"></asp:Label>
<asp:Label ID="lbl_FriendshipRequestSent" Visible="false" runat="server" Text="Friendship request sent!"></asp:Label>
<asp:HiddenField ID="hdnfld_profileUserID" Visible="false" runat="server" />
<asp:HiddenField ID="hdnfld_viewingUserID" runat="server" />

<asp:Label ID="lbl_ProfileNotVisible" runat="server" Text="You do not have permission to see this user's details. They are only visible to friends." Visible="False"></asp:Label>

<asp:Panel ID="pnl_privateInfo1" CssClass="profile-details-table-2-container" runat="server">



    <table class="profile-details-table-2">
        <tr>
            <td>
                <b>
                    <asp:Label ID="lbl_AboutMe" runat="server" Text="About Me"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lbl_AboutMeValue" runat="server"></asp:Label><asp:TextBox ID="txt_AboutMe"
                    Visible="false" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="lbl_Website" runat="server" Text="Website"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lbl_WebsiteValue" runat="server" Text="Label"></asp:Label><asp:TextBox
                    ID="txt_Website" Visible="false" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="lbl_Ranking" runat="server" Text="Ranking"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lbl_RankingValue" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="lbl_Points" runat="server" Text="Points"></asp:Label></b>
            </td>
            <td>
                <asp:Label ID="lbl_PointsValue" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>
<div class="clear">
</div>
<div style="text-align: center; font-weight: bold;">
    <asp:FileUpload ID="flupld_ProfileImage" Visible="false" runat="server" /><asp:Label
        ID="lblOnlyJpeg" Visible="false" runat="server" Text="*JPG, *JPEG only"></asp:Label>
    <asp:LinkButton ID="lnkbtn_uploadPhoto" CssClass="Ourspace_ToolbarLink" runat="server" OnClick="lnkbtn_uploadPhoto_Click"
        Visible="false">Upload photo</asp:LinkButton>
    <asp:LinkButton ID="lnkbtn_ChangePhoto" CssClass="Ourspace_ToolbarLink" runat="server" OnClick="lnkbtn_ChangePhoto_Click">Change Photo</asp:LinkButton>
    <asp:LinkButton ID="lnkbtn_EditProfile" CssClass="Ourspace_ToolbarLink" runat="server" OnClick="lnkbtn_EditProfile_Click">Edit profile</asp:LinkButton>
    <asp:LinkButton ID="lnkbtn_UpdateProfile" CssClass="Ourspace_ToolbarLink" Visible="false" runat="server" OnClick="lnkbtn_UpdateProfile_Click">Update Profile</asp:LinkButton>
    <asp:LinkButton ID="lnkbtn_cancel" CssClass="Ourspace_ToolbarLink" runat="server" OnClick="lnkbtn_cancel_Click" Visible="False">Cancel</asp:LinkButton>
    
    
    
    <asp:LinkButton ID="lnkbtn_editProfileSettings" CssClass="Ourspace_ToolbarLink" runat="server" 
    onclick="lnkbtn_editProfileSettings_Click" Visible="False">Change my profile settings</asp:LinkButton>


<br />

<br />

    
    
    
    
    
    
    
    
    
    
    </div>
    <asp:Label ID="lbl_EditSettingsVisibility" runat="server" 
    Text="Who can see my profile " Visible="False"></asp:Label>
<asp:DropDownList ID="ddl_profileVisibility" runat="server" Visible="False">
    <asp:ListItem Value="0">Friends</asp:ListItem>
    <asp:ListItem Value="1">Everyone</asp:ListItem>
</asp:DropDownList>
<div class="clear">
</div>
  <b> <asp:LinkButton ID="lnkbtn_saveProfileSettings" CssClass="Ourspace_ToolbarLink" runat="server" 
    onclick="lnkbtn_saveProfileSettings_Click" Visible="False">Save</asp:LinkButton>
&nbsp;<asp:LinkButton ID="lnkbtn_CancelEditSettings" CssClass="Ourspace_ToolbarLink" runat="server" 
    onclick="lnkbtn_CancelEditSettings_Click" Visible="False">Cancel</asp:LinkButton></b> 
    
<div class="clear">
</div>
<asp:Panel ID="pnl_privateInfo2" runat="server">
<div class="recent-posts-table">
<h2>
    Recent posts</h2>


    <asp:ListView ID="lstvw_recentPosts" runat="server" DataSourceID="sqldtsrc_recentPosts"
    EnableModelValidation="True" DataKeyNames="PostID" OnDataBound="lstvw_recentPosts_DataBound"
    OnItemDataBound="lstvw_recentPosts_ItemDataBound">
    <EmptyDataTemplate>
        <table id="Table1" runat="server" style="">
            <tr>
                <td>
                    No recent posts.
                </td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <ItemTemplate>
        <tr style="">
            <td>
                <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' />
            </td>
            <td>
                <asp:HyperLink  ID="hprlnk_post" runat="server">
                    <asp:Label ID="BodyLabel" runat="server" Text='<%# Eval("Body") %>' /></asp:HyperLink>
            </td>
            <td>
                <asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' />
                <asp:Label ID="ThreadIDLabel" runat="server" Text='<%# Eval("ThreadID") %>' Visible="false" />
                <asp:Label ID="ForumIDLabel" runat="server" Text='<%# Eval("ForumID") %>' Visible="false" />
                <asp:Label ID="PostIDLabel" runat="server" Text='<%# Eval("PostID") %>' Visible="false" />
            </td>
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
      
                    <table id="itemPlaceholderContainer" runat="server"  border="0" style="">
                        <tr id="Tr2" runat="server" style="">
                            <th id="Th1" runat="server">
                                <asp:Label ID="lbl_Subject" runat="server" Text="Subject"></asp:Label>
                            </th>
                            <th id="Th2" runat="server">
                                <asp:Label ID="lbl_Post" runat="server" Text="Post"></asp:Label>
                            </th>
                            <th id="Th3" runat="server">
                                <asp:Label ID="lbl_Date" runat="server" Text="Date"></asp:Label>
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                
    </LayoutTemplate>
</asp:ListView>
</div>
<div class="friends-thumbs">
<h2>
    <asp:Label ID="lbl_friends" runat="server" Text="Friends"></asp:Label></h2>
<asp:ListView ID="lstvw_friends" runat="server" DataKeyNames="friendshipRequester,friendshipRequestee"
    DataSourceID="sqldtsrc_friends" EnableModelValidation="True" OnItemDataBound="lstvw_friends_ItemDataBound">
    <EmptyDataTemplate>
        <table id="Table1" runat="server" style="">
            <tr>
                <td>
                    No data was returned.
                </td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <ItemTemplate>
        <div class="friendMini">
                <asp:Image ID="img_profileMini" runat="server" />
                <asp:Label Visible="false" ID="friendshipRequesterLabel" runat="server" Text='<%# Eval("friendshipRequester") %>' />
            
                <asp:Label Visible="false" ID="friendshipRequesteeLabel" runat="server" Text='<%# Eval("friendshipRequestee") %>' />
                <asp:Label Visible="false" ID="relationshipStatusIDLabel" runat="server" Text='<%# Eval("relationshipStatusID") %>' />
                <asp:HyperLink Visible="false" ID="hprlnk_userLink" runat="server">
                    <asp:Label ID="lbl_FriendDisplayName" runat="server" Text="Label"></asp:Label>
                </asp:HyperLink>
           </div>
      
    </ItemTemplate>
    <LayoutTemplate>
   
           
              
                    <div id="itemPlaceholderContainer" runat="server">
                       
                        <div id="itemPlaceholder" runat="server">
                        </div>
                    </div>
          
      
    </LayoutTemplate>
</asp:ListView>
</div>
</asp:Panel>

<asp:SqlDataSource ID="sqldtsrc_recentPosts" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT TOP (5) Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Posts.PostID, Forum_Threads.ForumID FROM Forum_Posts INNER JOIN Forum_Threads ON Forum_Posts.ThreadID = Forum_Threads.ThreadID WHERE (Forum_Posts.UserID = @userID)">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_profileUserID" Name="userID" PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_friends" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT TOP (10) friendshipRequester, friendshipRequestee, relationshipStatusID FROM Ourspace_Relationships WHERE (friendshipRequester = @user ) AND (relationshipStatusID = 1)">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_profileUserID" Name="user" PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>


