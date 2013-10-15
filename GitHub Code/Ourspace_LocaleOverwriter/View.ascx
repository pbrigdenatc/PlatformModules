<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_LocaleOverwriter.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<div class="hidden">
<p>&#160;</p>
    Logging in will take you to: <asp:Label ID="lblRedirectToAfterLogin" runat="server" Text=""></asp:Label><br />
Test:  <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>
<asp:Label ID="Label2" runat="server" Text=""></asp:Label><br />
    Current Culture:
    <asp:Label ID="lblCulture" runat="server" Text=""></asp:Label><br />
    Cookie Culture:
    <asp:Label ID="lblCookie" runat="server" Text=""></asp:Label><br />
    User setting:
    <asp:Label ID="lblLangSetting" runat="server" Text=""></asp:Label><br />
    AbsoluteUri:
    <asp:Label ID="lblAbsoluteUri" runat="server" Text=""></asp:Label><br />
    <div id="change-language">
        <div class="info-div">
            <div class="info-icon">
                <div>
                    <div>
                        <asp:Label ID="Label1" runat="server" resourcekey="PleaseSelectLang"></asp:Label>
                    </div>
                </div>
                <div class="cleared">
                    &nbsp;</div>
            </div>
        </div>
        <div class="language-change-object">
            <span title="English (United Kingdom)" class="Language">
             <asp:HyperLink ID="hprlnk_ChangeEnGb" runat="server">
                <img alt="en-GB" src="/images/Flags/en-GB.gif"/>English
                </asp:HyperLink>
                </span>
            <span title="Ελληνικά (Ελλάδα)" class="Language">
                <asp:HyperLink ID="hprlnk_ChangeElGr" runat="server"><img alt="el-GR" src="/images/Flags/el-GR.gif"/>Ελληνικά</asp:HyperLink>

            </span>
            <span title="Čeština (Česká&nbsp;Republika)" class="Language">
              <asp:HyperLink ID="hprlnk_ChangeCsCz" runat="server">
                    <img alt="cs-CZ" src="/images/Flags/cs-CZ.gif"/>Čeština
              </asp:HyperLink>
             </span>
             <span title="Deutsch (Österreich)" class="Language">
                    <asp:HyperLink ID="hprlnk_ChangeDeAt" runat="server">
                        <img alt="de-AT" src="/images/Flags/de-AT.gif"/>Deutsch
                  </asp:HyperLink>
            </span>
        </div>
    </div>
</div>
