<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_Microprofile.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<script language="javascript">
    jQuery('.referralDescriptionDesc').live('click', function () {
        var proposalDialog = jQuery('#referralDescriptionDesc').dialog({ modal: true, minHeight: 20, minWidth: 630, title: jQuery('.referralDescriptionTitle').html() });
        if (!jQuery(this).hasClass('dashboardRef')) {
            //jQuery(this).hide();
        }


    });

    jQuery(document).ready(function () {
        if (document.location.href.indexOf('#Work') > -1) {

            jQuery("#referPanel").removeClass('hidden');
            jQuery('.referralDescriptionDesc').hide();
            jQuery('.referralDescriptionDesc').slideDown('slow');
        }
        // jQuery('.referralDescriptionDesc').fadeIn();
    });
</script>
<div id="fb-root">
</div>
<div class="hidden">
    <asp:Label ID="Label3" CssClass="referralDescriptionTitle" runat="server" resourcekey="ReferralProgramTitle"></asp:Label>
    <asp:Literal ID="ltrl_UserId" runat="server"></asp:Literal></div>
<div id="micro-profile">
    <asp:Panel class="hidden" runat="server" ID="referPanel" Visible="false" Style="padding: 5px;
        cursor: pointer; margin: 0; position: fixed; right: 0; bottom: 0; z-index: 10000;
        bottom: 10px;">
        <asp:Image Width="300" Height="106" ImageUrl="/images/promote-button.png" ID="Image1"
            runat="server" />
    </asp:Panel>
    <div class="microprofile-flag small-flag-<%= CultureInfo.CurrentUICulture.Name %> fright">
    </div>
    <b>
        <asp:Label ID="Label1" runat="server" Style="margin-right: 0; border: none;" resourcekey="Language"
            CssClass="micro-name" Text="">:</asp:Label></b>
    <asp:HyperLink ID="hprlnk_toProfile" runat="server">
        <asp:Label ID="lblName" runat="server" CssClass="bold-link micro-name" Text=""></asp:Label></asp:HyperLink>
    <!-- currentCultureLanguage also used by JS to detect language
    so handle with care -->
    <asp:Label ID="lblLanguage" CssClass="currentCultureLanguage" runat="server" Text=""></asp:Label>
    <asp:Image ID="img_Profile" CssClass="profilePicture" runat="server" />
    <asp:Label ID="lbl_FacebookId" CssClass="userFacebookId" runat="server" Text="0"></asp:Label>
    <asp:Label ID="lbl_notLoggedOnFacebook" CssClass="notLoggedOnFacebook hidden micro-name"
        runat="server" Text="You are no longer connected to Facebook"></asp:Label>
</div>
<div class="hidden">
    Culture
    <asp:Label ID="lblCulture" runat="server" Text=""></asp:Label><br />
    Cookie
    <asp:Label ID="lblCookie" runat="server" Text=""></asp:Label><br />
    User setting
    <asp:Label ID="lblLangSetting" runat="server" Text=""></asp:Label><br />
    <img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/fb_share.png"
        alt="ipad mini" />
    <div id="referralDescriptionDesc">
        <div style="margin: 10px 0 0 0; padding-left: 150px;">
            <asp:Literal ID="ltrl_likeButton" runat="server"></asp:Literal></div>
        <div style="text-align: center; font-size: 13px;">
            <img src="http://www.joinourspace.eu/images/ipadmini.png" alt="ipad Mini" /></div>
        <asp:Panel ID="pnlReferralEnGb" runat="server">
            Bring as many users as you can to <span style="color: #4297d7; font-weight: bold;"><b>OurSpace</b></span>
            and win an iPad Mini
            <br />
            <br />
            Your <b>personal</b> sharing link:
            <div style="text-align: center; font-size: 14px; font-weight: bold; color: #4C98D7;
                margin: 5px 0;">
                <asp:Label ID="lbl_personalSharingUrl" CssClass="personalSharingUrl" runat="server"></asp:Label>
            </div>
            <span>Everytime someone registers using your link and has at least 100 <b><span class=" info-icon pointsAndLevelsInfo"
                style="display: inline-block; line-height: 15px; padding-left: 15px; margin-left: 3px;">
                points</span></b> on the platform, you get one step closer to winning the prize.<br />
                <br />
                <b>Post to Facebook</b> with the "Like" button so <b>you</b> bring more friends
                to <span style="color: #4297d7; font-weight: bold;">OurSpace</span>!</span>
        </asp:Panel>
        <asp:Panel ID="pnlReferralElGr" runat="server" Visible="false">
            Φέρτε όσους πιο πολλούς χρήστες μπορείτε στο <span style="color: #4297d7; font-weight: bold;"><b>OurSpace</b></span> και κερδίστε ένα iPad Mini
            <br />
            <br />
            Ο <b>προσωπικός</b> σας σύνδεσμος για την ανταλλαγή:
            <div style="text-align: center; font-size: 14px; font-weight: bold; color: #4C98D7;
                margin: 5px 0;">
                <asp:Label ID="Label2" CssClass="personalSharingUrl" runat="server"></asp:Label>
            </div>


            <span>Κάθε φορά που κάποιος νέος χρήστης εγγράφεται χρησιμοποιώντας το δικό σας σύνδεσμο και έχει τουλάχιστον 100 <b><span class=" info-icon pointsAndLevelsInfo"
                style="display: inline-block; line-height: 15px; padding-left: 15px; margin-left: 3px;">
                βαθμούς</span></b> στην πλατφόρμα, είστε όλο και πιο  κοντά στο να κερδίσετε το ipad mini.<br /><br />
                <b>Δημοσιεύστε στο Facebook</b> κάνοντας 'Like', έτσι ώστε να φέρετε περισσότερους φίλους σας στο <span style="color: #4297d7; font-weight: bold;">OurSpace</span>!

</span>
        </asp:Panel>
         <asp:Panel ID="pnlReferralCsCz" runat="server" Visible="false">
            Bring 300 users to <span style="color: #4297d7; font-weight: bold;"><b>OurSpace</b></span>
            and win an iPad Mini
            <br />
            <br />
            Your <b>personal</b> sharing link:
            <div style="text-align: center; font-size: 14px; font-weight: bold; color: #4C98D7;
                margin: 5px 0;">
                <asp:Label ID="Label4" CssClass="personalSharingUrl" runat="server"></asp:Label>
            </div>
            <span>Everytime someone registers using your link and has at least 100 <b><span class=" info-icon pointsAndLevelsInfo"
                style="display: inline-block; line-height: 15px; padding-left: 15px; margin-left: 3px;">
                points</span></b> on the platform, you get one step closer to winning the prize.<br />
                <br />
                <b>Post to Facebook</b> with the "Like" button so <b>you</b> bring more friends
                to <span style="color: #4297d7; font-weight: bold;">OurSpace</span>!</span>
        </asp:Panel>
        <asp:Panel ID="pnlReferralDeAt" runat="server" Visible="false">
            Bring 300 users to <span style="color: #4297d7; font-weight: bold;"><b>OurSpace</b></span>
            and win an iPad Mini
            <br />
            <br />
            Your <b>personal</b> sharing link:
            <div style="text-align: center; font-size: 14px; font-weight: bold; color: #4C98D7;
                margin: 5px 0;">
                <asp:Label ID="Label5" CssClass="personalSharingUrl" runat="server"></asp:Label>
            </div>
            <span>Everytime someone registers using your link and has at least 100 <b><span class=" info-icon pointsAndLevelsInfo"
                style="display: inline-block; line-height: 15px; padding-left: 15px; margin-left: 3px;">
                points</span></b> on the platform, you get one step closer to winning the prize.<br />
                <br />
                <b>Post to Facebook</b> with the "Like" button so <b>you</b> bring more friends
                to <span style="color: #4297d7; font-weight: bold;">OurSpace</span>!</span>
        </asp:Panel>

    </div>
</div>
<div class="cleared">
</div>
