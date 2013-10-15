<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_Microprofile.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<script language="javascript">

jQuery('#dnn_img_promote, #dnn_img_promote_mini, #dashboardRef').live('click', function (e) {

        if ($(e.target).is('span')) { return; }
       
       var proposalDialog = jQuery('#referralDescriptionDesc').dialog({ modal: true, resizable: false, minHeight: 20, minWidth: 630, title: jQuery('.referralDescriptionTitle').html() });
        if (!jQuery(this).hasClass('dashboardRef')) {
            //jQuery(this).hide();
        
        }
       


    });


    jQuery('#dnn_img_promote_right').live('click', function () {
        //event.stopPropagation();

        jQuery(this).hide();
        jQuery("#referPanel").fadeOut(200, function () {

            jQuery('#dnn_img_promote').hide();
            jQuery('#dnn_img_promote_mini').show();
            jQuery("#referPanel").fadeIn(200);
        });
    });


    jQuery(document).ready(function () {
        
            jQuery("#referPanel").removeClass('hidden');
            jQuery('#referPanel').hide();
            jQuery('#referPanel').slideDown('slow');
       
        // jQuery('.referralDescriptionDesc').fadeIn();
    });
</script>
<div id="fb-root">
</div>
<div class="hidden">
<div class="hidden">
    <asp:Label ID="lblPointsLevelTitle" CssClass="pointsAndLevelsTitle" runat="server" resourcekey="PointsAndLevels"></asp:Label>


<div id="pointsAndLevelsDesc">
    <asp:Label ID="Label2"  runat="server" resourcekey="WhatArePoints"></asp:Label>

</div>

</div>
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

        
        <%--<div class="data-block first_element_to_target">
            <h3>FIRST</h3>
            <img src="img/img1.jpg" alt="Brisbane"/>
            <p><a href="http://www.flickr.com/photos/ipyo/4948872706/in/photostream/" target="_blank">Credit</a></p>
        </div>
        <div class="data-block" id="second_element_to_target">
            <h3>SECOND</h3>
            <img src="img/img2.jpg" alt="Glen Canyon"/>
            <p><a href="http://www.flickr.com/photos/alanenglish/7523729938/" target="_blank">Credit</a></p>
        </div>
        <div class="data-block third_element_to_target">
            <div class="is_here">
                <h3>THIRD</h3>
                <img src="img/img3.jpg" alt="London"/>
                <p><a href="http://www.flickr.com/photos/fishyfish/200610788/" target="_blank">Credit</a></p>
            </div>
        </div>
        <div class="data-block" id="fourth_element_to_target">
            <h3>FOURTH</h3>
            <img src="img/img4.jpg" alt="Paros"/>
            <p><a href="http://www.flickr.com/photos/97373666@N00/3123603304/in/photostream/" target="_blank">Credit</a></p>
        </div>--%>
<asp:Panel ID="pnlGoogleConversion" Visible="false" runat="server">
<!-- Google Code for AdRegistrations Conversion Page -->
<script type="text/javascript">
/* <![CDATA[ */
var google_conversion_id = 979736372;
var google_conversion_language = "en";
var google_conversion_format = "3";
var google_conversion_color = "ffffff";
var google_conversion_label = "VwzgCOSSowcQtK6W0wM";
var google_conversion_value = 0;
var google_remarketing_only = false;
/* ]]> */
</script>
<script type="text/javascript" src="//www.googleadservices.com/pagead/conversion.js">
</script>
<noscript>
<div style="display:inline;">
<img height="1" width="1" style="border-style:none;" alt="" src="//www.googleadservices.com/pagead/conversion/979736372/?value=0&amp;label=VwzgCOSSowcQtK6W0wM&amp;guid=ON&amp;script=0"/>
</div>
</noscript>

</asp:Panel>

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
            <span>Everytime someone registers using your link and has at least 20 <b><span class=" info-icon pointsAndLevelsInfo"
                style="display: inline-block; line-height: 15px; padding-left: 15px; margin-left: 3px;">
                points</span></b> on the platform, you get one step closer to winning the prize.<br />
                <br />
                <b>Post to Facebook</b> with the "Like" button so <b>you</b> bring more friends
                to <span style="color: #4297d7; font-weight: bold;">OurSpace</span>!</span>
       <p style="text-align:center; margin-top: 15px;"><a target="_blank" href="http://www.joinourspace.eu/ContestTermsandConditionsel/tabid/315/language/en-GB/Default.aspx">Terms and conditions</a></p>
        </asp:Panel>
        <asp:Panel ID="pnlReferralElGr" runat="server" Visible="false">
            Φέρτε όσους πιο πολλούς χρήστες μπορείτε στο <span style="color: #4297d7; font-weight: bold;"><b>OurSpace</b></span> και κερδίστε ένα iPad Mini
            <br />
            <br />
            Ο <b>προσωπικός</b> σας σύνδεσμος για την ανταλλαγή:
            <div style="text-align: center; font-size: 14px; font-weight: bold; color: #4C98D7;
                margin: 5px 0;">
                <asp:Label ID="lbl_personalSharingUrlElGr" CssClass="personalSharingUrl" runat="server"></asp:Label>
            </div>


            <span>Κάθε φορά που κάποιος νέος χρήστης εγγράφεται χρησιμοποιώντας το δικό σας σύνδεσμο και έχει τουλάχιστον 20 <b><span class=" info-icon pointsAndLevelsInfo"
                style="display: inline-block; line-height: 15px; padding-left: 15px; margin-left: 3px;">
                βαθμούς</span></b> στην πλατφόρμα, είστε όλο και πιο  κοντά στο να κερδίσετε το ipad mini.<br /><br />
                <b>Δημοσιεύστε στο Facebook</b> κάνοντας 'Like', έτσι ώστε να φέρετε περισσότερους φίλους σας στο <span style="color: #4297d7; font-weight: bold;">OurSpace</span>!

</span>
       <p style="text-align:center; margin-top: 15px;"><a target="_blank" href="http://www.joinourspace.eu/ContestTermsandConditionsel/tabid/316/language/el-GR/Default.aspx">Όροι και Προϋποθέσεις</a></p>
        </asp:Panel>






         <asp:Panel ID="pnlReferralCsCz" runat="server" Visible="false">
            Přiveď co nejvíce uživatelů a je Tvůj!
            <br />
            <br />
            Osobní odkaz na sdílení: 
            <div style="text-align: center; font-size: 14px; font-weight: bold; color: #4C98D7;
                margin: 5px 0;">
                <asp:Label ID="lbl_personalSharingUrlCsCz" CssClass="personalSharingUrl" runat="server"></asp:Label>
            </div>

             <span>Pokaždé, když někdo registrovaný přes tento odkaz použije OurSpace a dosáhne alespoň 20 <b><span class=" info-icon pointsAndLevelsInfo"
                style="display: inline-block; line-height: 15px; padding-left: 15px; margin-left: 3px;">
                 bodů</span></b>, posuneš se o krok blíž k výhře. Sdílením na Facebooku získáš další přátele na <span style="color: #4297d7; font-weight: bold;">OurSpace</span>!</span>




            
               <p style="text-align:center; margin-top: 15px;"><a  target="_blank"  href="http://www.joinourspace.eu/ContestTermsandConditionsel/tabid/317/language/cs-CZ/Default.aspx">Pravidla a podmínky</a></p>
        </asp:Panel>







        <asp:Panel ID="pnlReferralDeAt" runat="server" Visible="false">
            Bring as many users as you can to <span style="color: #4297d7; font-weight: bold;"><b>OurSpace</b></span>
            and win an iPad Mini
            <br />
            <br />
            Your <b>personal</b> sharing link:
            <div style="text-align: center; font-size: 14px; font-weight: bold; color: #4C98D7;
                margin: 5px 0;">
                <asp:Label ID="lbl_personalSharingUrlDeAt" CssClass="personalSharingUrl" runat="server"></asp:Label>
            </div>
            <span>Everytime someone registers using your link and has at least 20 <b><span class=" info-icon pointsAndLevelsInfo"
                style="display: inline-block; line-height: 15px; padding-left: 15px; margin-left: 3px;">
                points</span></b> on the platform, you get one step closer to winning the prize.<br />
                <br />
                <b>Post to Facebook</b> with the "Like" button so <b>you</b> bring more friends
                to <span style="color: #4297d7; font-weight: bold;">OurSpace</span>!</span>
        
             <p style="text-align:center; margin-top: 15px;"><a  target="_blank"  href="http://www.joinourspace.eu/ContestTermsandConditionsel/tabid/318/language/de-AT/Default.aspx">Terms and conditions</a></p>
        </asp:Panel>

    </div>
</div>
<div class="cleared">
</div>
