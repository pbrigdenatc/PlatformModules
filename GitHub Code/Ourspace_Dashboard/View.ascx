<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_Dashboard.View" AutoEventWireup="false"
    CodeBehind="View.ascx.cs" %>
<div class="hidden-title">
    <asp:Label ID="lblDashboardTitle" resourcekey="lblDashboardTitle" runat="server"
        Text=""></asp:Label></div>
<asp:TextBox ID="txt_DOB" runat="server" CssClass="datepicker hidden"></asp:TextBox>
<asp:HiddenField ID="hdnfld_DOB" runat="server" />
<asp:Label ID="Label2" runat="server" CssClass="dateselected" Text=""></asp:Label>
<div id="dashboard-wrap">
    <asp:HiddenField ID="hdnfldCurrentUser" runat="server" Value="-1" />
    <h2>
        <asp:Label ID="Label3" runat="server" resourcekey="UserProfile" Text=""></asp:Label>
    </h2>
    <div id="profile-pic-wrap">
        <asp:Image ID="imgProfilePic" runat="server" Height="117px" /></div>
    <div id="profile-info-wrap">
        <div id="profile-info-1">
            <h3>
                <asp:Label ID="lblUserFullName" runat="server" Text="Label"></asp:Label></h3>
            <asp:Label ID="lblMemberSinceTitle" runat="server" resourcekey="MemberSince" Text="Member since"></asp:Label>:
            <asp:Label ID="lblMemberSinceDate" runat="server" Text=""></asp:Label>
        </div>
        <div id="profile-info-2">
            <asp:LinkButton ID="lnkBtnEditProfile" runat="server" Text="Edit my profile & preferences"
                resourcekey="EditProfile" OnClick="lnkBtnEditProfile_Click"></asp:LinkButton>
        </div>
        <div class="cleared">
        </div>
        <div id="best-proposals">
            <span id="best-prop-percent">
                <asp:Label ID="lblPoints" runat="server" Text=""></asp:Label></span>&nbsp;
            <asp:Label ID="Label4" runat="server" Text="Label" resourcekey="points"></asp:Label>
        </div>
    </div>
    <div class="cleared">
    </div>
    <asp:Panel ID="pnlEditMyProfile" runat="server" Visible="false">
        <div class="tabs-wrapper">
            <div class="cleared">
            </div>
            <asp:LinkButton ID="LinkButton3" runat="server" Text="My Profile" CssClass="tab-active"
                OnClick="lnkbtnMyReplies_Click" resourcekey="MyProfile"></asp:LinkButton>
            <div class="tab-line">
            </div>
        </div>
        <div class="clear">
        </div>
        <asp:Panel ID="pnl_privateInfo3" runat="server" CssClass="profile-details-table-container">
            <table class="profile-details-table">
                <tr>
                    <td>
                        <b>
                            <asp:Label ID="lbl_FirstName" resourcekey="firstName" runat="server" Text="First Name"></asp:Label>:</b>
                    </td>
                    <td>
                        <asp:Label ID="lbl_FirstNameValue" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="txt_FirstName" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td class="detail-right">
                        <b>
                            <asp:Label ID="lbl_Country" resourcekey="country" runat="server" Text="Country"></asp:Label>:</b>
                    </td>
                    <td>
                        <asp:Label ID="lbl_CountryValue" runat="server" Text=""></asp:Label><asp:DropDownList
                            ID="ddlCountries" Visible="false" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="alt-item">
                        <b>
                            <asp:Label ID="lbl_LastName" resourcekey="lastName" runat="server" Text="Last Name"></asp:Label>:</b>
                    </td>
                    <td class="alt-item">
                        <asp:Label ID="lbl_LastNameValue" runat="server" Text=""></asp:Label><asp:TextBox
                            ID="txt_LastName" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td class="detail-right alt-item">
                        <b>
                            <asp:Label ID="lbl_City" resourcekey="city" runat="server" Text="City"></asp:Label>:</b>
                    </td>
                    <td class="alt-item">
                        <asp:Label ID="lbl_CityValue" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="txt_City" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <asp:Label ID="lbl_OldPassword" runat="server" Visible="false" Text="Old password:" resourcekey="OldPassword"></asp:Label></b>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_OldPassword" runat="server" TextMode="Password" Visible="false"></asp:TextBox>
                    </td>
                    <td class="detail-right">
                        <b>
                            <asp:Label ID="lbl_NewPassword" runat="server"  Text="New password:" Visible="false" resourcekey="NewPassword"></asp:Label></b>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_NewPassword" runat="server" TextMode="Password" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="no-border">
                    <div class="password-controls">
                        <asp:LinkButton ID="lnkbtn_ChangePassword" CssClass="action-button fleft" Visible="false"
                            runat="server" OnClick="lnkbtn_ChangePassword_Click" Text="Change password" resourcekey="changePassword"></asp:LinkButton>
                        <asp:LinkButton ID="lnkbtn_KeepPassword" CssClass="action-button fleft" Visible="false" runat="server"
                            OnClick="lnkbtn_KeepPassword_Click" Text="Keep old password" resourcekey="keepOldPassword"></asp:LinkButton>
                        <asp:LinkButton ID="lnkbtn_ConfirmNewPassword" Text="Confirm new password" CssClass="action-button fleft"
                            Visible="false" runat="server" OnClick="lnkbtn_ChangePasswordConfirm_Click" resourcekey="Confirm"></asp:LinkButton>
                      <div class="cleared">
                        </div>
                        <asp:Panel ID="pnl_feedback" runat="server" Visible="false">
                            <div class="info-div">
                                <div class="info-icon">
                                    <asp:Label ID="lbl_PasswordChanged" Visible="false" runat="server" Text="Password successfully changed."></asp:Label>
                                    <asp:Label ID="lbl_PasswordTooShort" Visible="false" runat="server" Text="Password must be at least 8 characters long."></asp:Label>
                                    <asp:Label ID="lbl_IncorrectOldPassword" Visible="false" runat="server" Text="Incorrect old password, please try again."></asp:Label>
                                    <div class="cleared">
                                        &nbsp;</div>
                                </div>
                            </div>
                        </asp:Panel>
                        </div>
                    </td>
                </tr>
               
            </table>
        </asp:Panel>
        <asp:Label ID="lbl_EditSettingsVisibility" runat="server" Text="Who can see my profile "
            Visible="False"></asp:Label>
        <asp:DropDownList ID="ddl_profileVisibility" runat="server" Visible="False">
            <asp:ListItem Value="0">Friends</asp:ListItem>
            <asp:ListItem Value="1">Everyone</asp:ListItem>
        </asp:DropDownList>
        <div class="clear">
        </div>
        <b>
            <asp:LinkButton ID="lnkbtn_saveProfileSettings" CssClass="Ourspace_ToolbarLink" runat="server"
                OnClick="lnkbtn_saveProfileSettings_Click" Visible="False">Save</asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lnkbtn_CancelEditSettings" CssClass="Ourspace_ToolbarLink"
                runat="server" OnClick="lnkbtn_CancelEditSettings_Click" Visible="False">Cancel</asp:LinkButton></b>
        <div class="clear">
        </div>
        <div class="edit-profile-button-wrap">
            <div class="fleft" style="margin-right:10px;"><asp:FileUpload ID="flupld_ProfileImage" Visible="false" runat="server" /> <asp:Label ID="lblFileNotSelected" runat="server" style="color:Red; font-size:18px;" Visible="false" Text="*"></asp:Label>
                <asp:Label ID="lblUploadPhotoInstructions" Visible="false" runat="server" Text="*.jpg, *.jpeg"></asp:Label>
            </div>
           
            <asp:LinkButton ID="lnkbtn_uploadPhoto" CssClass="action-button fleft" runat="server"
                OnClick="lnkbtn_uploadPhoto_Click" resourcekey="uploadPhoto" Visible="false"></asp:LinkButton>
            <asp:LinkButton ID="lnkbtn_ChangePhoto" CssClass="action-button fleft" Visible="false"
                runat="server" OnClick="lnkbtn_ChangePhoto_Click" resourcekey="changePhoto"></asp:LinkButton>
            <asp:LinkButton ID="lnkbtn_EditProfile" CssClass="action-button fleft" runat="server"
                OnClick="lnkbtn_EditProfile_Click" resourcekey="EditProfile"></asp:LinkButton>
            <asp:LinkButton ID="lnkbtn_UpdateProfile" CssClass="action-button fleft" Visible="false"
                runat="server" OnClick="lnkbtn_UpdateProfile_Click" resourcekey="updateProfile"></asp:LinkButton>
            <asp:LinkButton ID="lnkbtn_cancel" CssClass="action-button fleft" runat="server"
                OnClick="lnkbtn_cancel_Click" Visible="False" resourcekey="cancel"></asp:LinkButton>
                 <asp:LinkButton ID="lnkbtn_cancelPhotoUpload" CssClass="action-button fleft" runat="server"
                OnClick="lnkbtn_cancelPhotoUpload_Click" Visible="False" resourcekey="cancel"></asp:LinkButton>
            <asp:LinkButton ID="lnkbtn_editProfileSettings" CssClass="action-button fleft" runat="server"
                OnClick="lnkbtn_editProfileSettings_Click" Visible="False">Change my profile settings</asp:LinkButton>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnGoToDashboard" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnAllMyReplies" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnAllMyDebatesProposals" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnAllMySolutions" />
        </Triggers>
        <ContentTemplate>
            <div class="updatePanel">
                <div class="cleared">
                </div>
                <div class="tabs-wrapper">
                    <div class="cleared">
                    </div>
                    <asp:LinkButton ID="lnkbtnMyReplies" runat="server" resourcekey="MyReplies" Text="My Replies"
                        CssClass="tab-active" OnClick="lnkbtnMyReplies_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lnkbtnMyDebates" runat="server" Text="My Debates-Proposals" resourcekey="MyDebatesProposals"
                        CssClass="tab-inactive" OnClick="lnkbtnMyDebates_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnMySolutions" runat="server" Text="My Solutions" resourcekey="MySolutions"
                        CssClass="tab-inactive" OnClick="lnkBtnMySolutions_Click"></asp:LinkButton>
                    <div class="tab-line">
                    </div>
                </div>
                <asp:SqlDataSource ID="sqldtsrc_Replies" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
                    SelectCommand="SELECT TOP (3) Forum_Forums.Name, Forum_Threads.Replies, Forum_Threads.ThreadID, Forum_Threads.ForumID, Forum_Posts.Subject, Forum_Posts.PostID, Ourspace_Forum_Thread_Info.ThreadLanguage, Forum_Posts.Body, Forum_Posts.CreatedDate FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Posts.UserID = @userId) AND (Forum_Posts.PostID <> Forum_Posts.ThreadID) ORDER BY Forum_Posts.CreatedDate DESC">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hdnfldCurrentUser" DefaultValue="-1" Name="userId"
                            PropertyName="Value" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sqldtsrc_RepliesAll" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
                    SelectCommand="SELECT Forum_Forums.Name, Forum_Threads.Replies, Forum_Threads.ThreadID, Forum_Threads.ForumID, Forum_Posts.Subject, Forum_Posts.PostID, Ourspace_Forum_Thread_Info.ThreadLanguage, Forum_Posts.Body, Forum_Posts.CreatedDate FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Posts.UserID = @userId)  AND (Forum_Posts.PostID <> Forum_Posts.ThreadID) ORDER BY Forum_Posts.CreatedDate DESC">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hdnfldCurrentUser" DefaultValue="-1" Name="userId"
                            PropertyName="Value" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:ListView ID="lstvw_Replies" runat="server" DataKeyNames="ThreadID" DataSourceID="sqldtsrc_Replies"
                    EnableModelValidation="True" OnItemDataBound="lstvw_Replies_ItemDataBound">
                    <EmptyDataTemplate>
                        <div class="reply-item">
                            <asp:Label ID="Label10" runat="server" resourcekey="NoReplies" Text="Label"></asp:Label>
                        </div>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <div class="reply-item">
                            <div class="debate-title bold-link">
                                <a href='<%# GetForumPostUrl(Convert.ToInt32(Eval("ForumID")), Convert.ToInt32(Eval("PostID")) ) %>'><asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></a></div>
                            <asp:Label ID="lblDebateProposal" resourcekey="debateProposal" runat="server"></asp:Label>:
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />&nbsp;-&nbsp;
                            <asp:Label ID="RepliesLabel" runat="server" Text='<%# Eval("Replies") %>' />&nbsp;<asp:Label
                                ID="lblReplies" runat="server" resourcekey="replies"></asp:Label>&nbsp;-&nbsp; <span class="light-text">
                                    <asp:Label ID="lblDate" runat="server" Text='<%# FormatDate(Eval("CreatedDate")) %>' />
                                </span>
                            <asp:Label ID="ThreadIDLabel" Visible="false" runat="server" Text='<%# Eval("ThreadID") %>' />
                            <asp:Label ID="PostIDLabel" Visible="false" runat="server" Text='<%# Eval("PostID") %>' />
                            <asp:Label ID="ForumIDLabel" Visible="false" runat="server" Text='<%# Eval("ForumID") %>' />
                            <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fright">
                            </div>
                        </div>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <div class="reply-item alt-item">
                            <div class="debate-title bold-link">
                                  <a href='<%# GetForumPostUrl(Convert.ToInt32(Eval("ForumID")), Convert.ToInt32(Eval("PostID")) ) %>'><asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></a></div>
                            <asp:Label ID="lblDebateProposal" runat="server" Text="Debate-Proposal"></asp:Label>:
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />&nbsp;-&nbsp;
                            <asp:Label ID="RepliesLabel" runat="server" Text='<%# Eval("Replies") %>' />&nbsp;<asp:Label
                                ID="lblReplies" runat="server" Text="Replies"></asp:Label>&nbsp;-&nbsp; <span class="light-text">
                                    <asp:Label ID="lblDate" runat="server" Text='<%# FormatDate(Eval("CreatedDate")) %>' />
                                </span>
                            <asp:Label ID="ThreadIDLabel" Visible="false" runat="server" Text='<%# Eval("ThreadID") %>' />
                            <asp:Label ID="PostIDLabel" Visible="false" runat="server" Text='<%# Eval("PostID") %>' />
                            <asp:Label ID="ForumIDLabel" Visible="false" runat="server" Text='<%# Eval("ForumID") %>' />
                            <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fright">
                            </div>
                        </div>
                    </AlternatingItemTemplate>
                    <LayoutTemplate>
                        <div id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <div id="itemPlaceholder" runat="server">
                            </div>
                        </div>
                        <asp:LinkButton ID="lnkbtnFooterAllReplies" OnClick="lnkbtnFooterAllReplies_Click"
                            CssClass="go-link fright" runat="server">
                            <asp:Label ID="Label1" resourcekey="ViewAllReplies" runat="server" Text="View all replies..."></asp:Label></asp:LinkButton>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lstvw_Solutions" runat="server" Visible="false" DataSourceID="sqldtsrc_Solutions"
                    EnableModelValidation="True" OnItemDataBound="lstvw_Solutions_ItemDataBound">
                    <EmptyDataTemplate>
                        <div class="reply-item">
                            <asp:Label ID="Label12" runat="server" resourcekey="NoSolutions" Text="Label"></asp:Label>
                        </div>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <div class="reply-item">
                            <div class="debate-title bold-link">
                                <a href='<%# GetForumPostUrl(Convert.ToInt32(Eval("ForumID")), Convert.ToInt32(Eval("PostID")) ) %>'><asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></a></div>
                            <asp:Label ID="lblBody" runat="server" Text='<%# Eval("Body") %>'></asp:Label>
                            &nbsp;-&nbsp; <span class="light-text">
                                <asp:Label ID="lblDate" runat="server" Text='<%# FormatDate(Eval("CreatedDate")) %>' />
                            </span>
                            <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fright">
                            </div>
                        </div>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <div class="reply-item alt-item">
                            <div class="debate-title bold-link">
                                <a href='<%# GetForumPostUrl(Convert.ToInt32(Eval("ForumID")), Convert.ToInt32(Eval("PostID")) ) %>'><asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></a></div>
                            <asp:Label ID="lblBody" runat="server" Text='<%# Eval("Body") %>'></asp:Label>
                            &nbsp;-&nbsp; <span class="light-text">
                                <asp:Label ID="lblDate" runat="server" Text='<%# FormatDate(Eval("CreatedDate")) %>' />
                            </span>
                            <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fright">
                            </div>
                        </div>
                    </AlternatingItemTemplate>
                    <LayoutTemplate>
                        <div id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <div id="itemPlaceholder" runat="server">
                            </div>
                        </div>
                        <asp:LinkButton ID="lnkbtnFooterAllSolutions" OnClick="lnkbtnFooterAllSolutions_Click"
                            CssClass="go-link fright" runat="server">
                            <asp:Label ID="Label1" runat="server" resourcekey="ViewAllReplies" Text="View all solutions..."></asp:Label></asp:LinkButton>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lstvw_DebateProposals" runat="server" Visible="false" DataSourceID="sqldtsrc_DebateProposals"
                    EnableModelValidation="True" OnItemDataBound="lstvw_Solutions_ItemDataBound">
                    <EmptyDataTemplate>
                        <div class="reply-item">
                            <asp:Label ID="Label13" runat="server" resourcekey="NoDebateProposals" Text="Label"></asp:Label>
                        </div>
                    </EmptyDataTemplate>
                    <AlternatingItemTemplate>
                        <div class="reply-item alt-item">
                            <div class="debate-title bold-link">
                                <a href='<%# GetDebateProposalUrl(Convert.ToInt32(Eval("ForumID")), Convert.ToInt32(Eval("PostID")), Convert.ToInt32(Eval("phaseId")))  %>'><asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></a></div>
                            <asp:Label ID="lblBody" runat="server" Text='<%# Eval("Body") %>'></asp:Label>
                            &nbsp;-&nbsp; <span class="light-text">
                                <asp:Label ID="lblDate" runat="server" Text='<%# FormatDate(Eval("CreatedDate")) %>' />
                            </span>
                            <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fright">
                            </div>
                        </div>
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <div class="reply-item">
                            <div class="debate-title bold-link">
                                          <a href='<%# GetDebateProposalUrl(Convert.ToInt32(Eval("ForumID")), Convert.ToInt32(Eval("PostID")), Convert.ToInt32(Eval("phaseId")))  %>'><asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></a></div>
                            <asp:Label ID="lblBody" runat="server" Text='<%# Eval("Body") %>'></asp:Label>
                            &nbsp;-&nbsp; <span class="light-text">
                                <asp:Label ID="lblDate" runat="server" Text='<%# FormatDate(Eval("CreatedDate")) %>' />
                            </span>
                            <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fright">
                            </div>
                        </div>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <div id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <div id="itemPlaceholder" runat="server">
                            </div>
                        </div>
                        <asp:LinkButton ID="lnkbtnFooterAllDebateProposals" OnClick="lnkbtnFooterAllDebateProposals_Click"
                            CssClass="go-link fright" runat="server">
                            <asp:Label ID="Label1" runat="server" resourcekey="ViewAllReplies" Text="View all solutions..."></asp:Label></asp:LinkButton>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:Panel ID="pnlExtraDashboardInfo" runat="server">
                    <div class="tabs-wrapper">
                        <div class="cleared">
                        </div>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Notifications" resourcekey="Notifications"
                            CssClass="tab-active" OnClick="lnkbtnMyReplies_Click"></asp:LinkButton>
                        <div class="tab-line">
                        </div>
                    </div>
                    <asp:Repeater ID="NotificationsRepeater" runat="server" OnItemDataBound="NotificationsRepeater_ItemDataBound">
                        <HeaderTemplate>
                            <ul class="notifications-list">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <asp:Label ID="lblUserId" runat="server" Visible="false" Text='<%#Eval("CreatorId")%>'></asp:Label>
                                <asp:HyperLink ID="hprlnkUserProfile" runat="server"><%#Eval("Creator")%></asp:HyperLink>
                                <%#Eval("ActionType")%>
                                <asp:Label ID="lblYourPost" runat="server" resourcekey="yourPost" Text="your post about"></asp:Label>
                                <a href="<%#Eval("PostUrl")%>">
                                    <%#Eval("PostSubject")%></a>.</li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                            <asp:Label ID="lblEmptyData" Text="No new notifications" resourcekey="NonewNotifications"
                                runat="server" Visible="false">
                            </asp:Label>
                        </FooterTemplate>
                    </asp:Repeater>
                    <div class="tabs-wrapper">
                        <div class="cleared">
                        </div>
                        <asp:LinkButton ID="LinkButton2" runat="server" Text="Recent Activity" resourcekey="RecentActivity"
                            CssClass="tab-active" OnClick="lnkbtnMyReplies_Click"></asp:LinkButton>
                        <div class="tab-line">
                        </div>
                    </div>
                    <asp:ListView ID="lstvwRecentActitity" runat="server" DataKeyNames="Id" DataSourceID="sqldtrc_RecentActivity"
                        EnableModelValidation="True" OnItemDataBound="lstvwRecentActitity_ItemDataBound">
                        <EmptyDataTemplate>
                            <div class="reply-item">
                                <asp:Label ID="Label11" runat="server" resourcekey="NoRecentActivity"></asp:Label>
                            </div>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <li>
                                <%--<asp:Label ID="Label14" runat="server" resourcekey="ThanksFor" Text="Thanks for"></asp:Label>--%>
                               
                                    <asp:Label ID="TypeLabel" runat="server" Text='<%# Eval("Type") %>' resourcekey="" />
                                <%--<asp:Label ID="LabelOn" resourcekey="On" runat="server" Text="on"></asp:Label>--%>

                                <a href="<%# GetForumPostUrl( Convert.ToInt32(Eval("ForumId")),  Convert.ToInt32(Eval("PostId")) ) %>">
                                <asp:Label ID="ThreadNameLabel" Visible="false" runat="server" Text='' /></a>
                                <%--  <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' /> <asp:Label ID="RecipientLabel" runat="server" Text='<%# Eval("Recipient") %>' />
                       
                             <asp:Label ID="CreatorLabel" runat="server" Text='<%# Eval("Creator") %>' />--%>
                                &nbsp;-&nbsp;<span class="light-text">
                                    <asp:Label ID="lblDate" runat="server" Text='<%# FormatDate(Eval("Date")) %>' /></span>
                                <asp:Label ID="PostIdLabel" Visible="false" runat="server" Text='<%# Eval("PostId") %>' />
                            </li>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <ul id="itemPlaceholderContainer" runat="server" border="0" style="">
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                    </asp:ListView>
                </asp:Panel>
               
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div id="dashboard-right">
    <div class="art-blockheader-sidebar2">
        <div class="t">
            <span class="Head">
                <asp:Label ID="Label9" runat="server" resourcekey="BasicStatistics" Text="Label"></asp:Label></span></div>
    </div>
    <ul>
        <li>
            <asp:Label ID="Label6" runat="server" resourcekey="Level" Text="Label"></asp:Label>:<b>
                <asp:Label ID="lblSideLevel" runat="server" Text=""></asp:Label></b> </li>
        <li>
            <asp:Label ID="Label7" runat="server" resourcekey="PPoints" Text="Label"></asp:Label>:
            <b>
                <asp:Label ID="lblSidePoints" runat="server" Text=""></asp:Label></b></li>
        <li>
            <asp:Label ID="Label5" runat="server" resourcekey="NextlevelPoints" Text="Label"></asp:Label>:
            <b>
                <asp:Label ID="lblPointsToNextLevel" runat="server" Text="Label"></asp:Label></b></li>
              <%--   <li>
            <asp:Label ID="Label14" runat="server" resourcekey="NextlevelPoints" Text="Label"></asp:Label>:
            <b>
                <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label></b></li>--%>
<li>
<div class="info-icon referralDescriptionDesc  " id="dashboardRef" style="min-height:15px; cursor:pointer;"><asp:Label ID="Label14" runat="server"  Text="Referrals"></asp:Label>: <b><asp:Label ID="lblReferrals" runat="server" Text="Label"></asp:Label></b></div>
</li>
    </ul>
    
    <div id="dashboard-menu">
        <div class="title">
            <asp:Label ID="Label8" runat="server" resourcekey="ProfileAndActivities" Text="Label"></asp:Label></div>
        <asp:LinkButton ID="lnkbtnGoToDashboard" resourcekey="Dashboard" runat="server" Text="Dashboard"
            OnClick="lnkbtnGoToDashboard_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkbtnAllMyReplies" runat="server" resourcekey="MyReplies" Text="My Replies"
            OnClick="lnkbtnAllMyReplies_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkbtnAllMyDebatesProposals" runat="server" resourcekey="MyDebatesProposals"
            Text="My Debates-Proposals" OnClick="lnkbtnAllMyDebatesProposals_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkbtnAllMySolutions" runat="server" Text="My Solutions" resourcekey="MySolutions"
            OnClick="lnkbtnAllMySolutions_Click"></asp:LinkButton>
    </div>
</div>
<asp:SqlDataSource ID="sqldtsrc_Solutions" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT TOP (3) Forum_Posts.UserID, Forum_Posts.PostId, Forum_Threads.ForumID, Forum_Posts.CreatedDate, Forum_Posts.Body, Users.Username, Users.DisplayName, Forum_Posts.Subject, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Posts INNER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID                                    INNER JOIN Forum_Threads ON Forum_Threads.ThreadID = Forum_Posts.ThreadID                                    INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Posts.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Ourspace_Forum_Post_Thumbs.IsSolution = 1) AND (Forum_Posts.UserID = @userId)">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfldCurrentUser" DefaultValue="-1" Name="userId"
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_SolutionsAll" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Posts.UserID, Forum_Posts.PostId, Forum_Threads.ForumID, Forum_Posts.CreatedDate, Forum_Posts.Body, Users.Username, Users.DisplayName, Forum_Posts.Subject, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Posts INNER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID                                    INNER JOIN Forum_Threads ON Forum_Threads.ThreadID = Forum_Posts.ThreadID                                    INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Posts.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Ourspace_Forum_Post_Thumbs.IsSolution = 1) AND (Forum_Posts.UserID = @userId)">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfldCurrentUser" DefaultValue="-1" Name="userId"
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_DebateProposals" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT  TOP (3)  Forum_Posts.UserID, Ourspace_Forum_Thread_Info.phaseId, Forum_Threads.ForumID, Forum_Posts.PostId, Forum_Posts.CreatedDate, Forum_Posts.Body, Users.Username, Users.DisplayName, Forum_Posts.Subject, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Posts  INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Posts.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Threads ON Forum_Posts.ThreadID = Forum_Threads.ThreadID WHERE (Forum_Posts.UserID = @userId) AND (Forum_Posts.ParentPostID = 0)">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfldCurrentUser" DefaultValue="-1" Name="userId"
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_DebateProposalsAll" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Posts.UserID, Ourspace_Forum_Thread_Info.phaseId, Forum_Threads.ForumID, Forum_Posts.PostId, Forum_Posts.CreatedDate, Forum_Posts.Body, Users.Username, Users.DisplayName, Forum_Posts.Subject, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Posts  INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Posts.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Threads ON Forum_Posts.ThreadID = Forum_Threads.ThreadID WHERE (Forum_Posts.UserID = @userId) AND (Forum_Posts.ParentPostID = 0)">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfldCurrentUser" DefaultValue="-1" Name="userId"
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
 <asp:SqlDataSource ID="sqldtrc_RecentActivity" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
                    
    SelectCommand="SELECT TOP (6) Ourspace_Notifications.Id, Ourspace_Notifications.Recipient, Ourspace_Notifications.Creator, Ourspace_Notifications.Type, Ourspace_Notifications.Date, Ourspace_Notifications.PostId, Forum_Threads.ForumID FROM Ourspace_Notifications INNER JOIN Forum_Posts ON Ourspace_Notifications.PostId = Forum_Posts.PostID INNER JOIN Forum_Threads ON Forum_Posts.ThreadID = Forum_Threads.ThreadID WHERE (Ourspace_Notifications.Creator = @userid) ORDER BY Ourspace_Notifications.Date DESC">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hdnfldCurrentUser" Name="userid" PropertyName="Value" />
                    </SelectParameters>
                </asp:SqlDataSource>