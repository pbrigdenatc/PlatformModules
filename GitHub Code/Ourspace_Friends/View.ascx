<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_Friends.View" AutoEventWireup="false"
    CodeBehind="View.ascx.cs" %>
   
    <div class="friends-entire-wrapper" >

<div id="hidden-title">
    <asp:Label ID="Label1" runat="server" resourcekey="MyFriends" Text="My Friends"></asp:Label></div>
<asp:HiddenField ID="hdnfld_UserID" runat="server" />
<h2>
    <asp:Label ID="Label2" runat="server" resourcekey="InviteYourFriends" Text="Invite your friends"></asp:Label></h2>
 
        <asp:Panel ID="pnlSearchFacebookFriends" runat="server">
     
 <div class="ui-widget">
	<label for="tags"><asp:Label ID="Label13" runat="server" resourcekey="FindAFriend" Text="Find a friend:"></asp:Label></label>

	<input id="tags">
</div>
   </asp:Panel>
 <div class="info-div hidden not-logged-in-fb">
        <div class="info-icon">
            <div>
                <div>
                    <asp:Label ID="Label12" resourcekey="NotOnFb" runat="server"></asp:Label>
          </div>
            </div>
            <div class="cleared">
                &nbsp;</div>
        </div>
    </div>
<asp:Panel ID="pnlFbFriends" runat="server" Visible="false">
    <div class="friend-table-loading">
        <table id="friend-table">
            <tr>
                <td class="title-bar topic-padded">
                    <asp:Label ID="lblFriend" runat="server" resourcekey="Friend"></asp:Label>
                </td>
                <td class="title-bar">
                    <div class="header-separator">
                        <asp:Label ID="lblInfo" runat="server" resourcekey="Information"></asp:Label></div>
                </td>
            </tr>
        </table>
        <div id="ourspaceFriends-pager-wrap">
        </div>
    </div>
    <div id="more-fb-friends">
        <a>
            <asp:Label ID="Label3" CssClass="Help" runat="server" resourcekey="LoadFriends"></asp:Label></a></div>
</asp:Panel>
<asp:Panel ID="pnlNonFbInfo" runat="server" Visible="false">
    <div class="info-div">
        <div class="info-icon">
            <div>
                <div>
                    <asp:Label ID="lblPleaseFacebook" resourcekey="NonFb" runat="server"></asp:Label>
                    <div class="fb-login-wrap">
                        <asp:HyperLink ID="hprlnkLogin" class="fb-friends-logout action-button" runat="server"
                           resourcekey="Logout"></asp:HyperLink>
                    </div>
                </div>
            </div>
            <div class="cleared">
                &nbsp;</div>
        </div>
    </div>
</asp:Panel>
<div class="entry">
</div>
<div class="hidden">
<!-- Used to store strings used by javascript. Done this way to enable localization -->
    <asp:Label ID="Label5" runat="server" CssClass="js_Gender" resourcekey="Gender" Text="Gender"></asp:Label>
    <asp:Label ID="Label6" runat="server" CssClass="js_Invite" resourcekey="Invite" Text="Invite"></asp:Label>
    <asp:Label ID="Label8" runat="server" CssClass="js_IsOnOurspace" resourcekey="IsOnOurspace" Text="is on OurSpace"></asp:Label>
    <asp:Label ID="Label9" runat="server" CssClass="js_IsNotOnOurspace" resourcekey="IsNotOnOurspace" Text="is not on OurSpace yet"></asp:Label>
    <asp:Label ID="Label10" runat="server" CssClass="js_InvitationNotSent" resourcekey="InvitationNotSent" Text="Invitation not sent!"></asp:Label>
     <asp:Label ID="Label11" runat="server" CssClass="js_Success" resourcekey="Success" Text="Success"></asp:Label>
</div>
 <div class="hidden post-success-wrap">
    <asp:Label ID="Label4" runat="server" resourcekey="YourInvitationSent"></asp:Label>
    </div>
    <div class="hidden post-failure-wrap">
    <asp:Label ID="Label7" runat="server" resourcekey="YourInvitationNotSent"></asp:Label>
    </div>

   
</div>
