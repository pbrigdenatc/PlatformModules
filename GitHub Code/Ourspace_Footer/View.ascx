<%@ Control language="C#" Inherits="DotNetNuke.Modules.Ourspace_Footer.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
<div class="footer2lft">
	<a class="logo-link-footer" href="#"></a>
	
	&copy;2013 OurSpace Project | 
    <asp:Label ID="Label1" runat="server"  resourcekey="WebCreatedBy"></asp:Label> <a href="http://www.atc.gr">ATC</a>
	</div>
	<div class="footer2rght">
		
                <asp:HyperLink ID="hprlnkTermsAndCons" resourcekey="TermsAndCons" runat="server"></asp:HyperLink>
                 |
			 <asp:HyperLink ID="hprlnkLegal" resourcekey="LegalDisclaimer" runat="server"></asp:HyperLink> |
			 <asp:HyperLink ID="hprlnkPrivacy" resourcekey="PrivacyPolicy" runat="server"></asp:HyperLink> | 
             <asp:HyperLink ID="hprlnkFaq" resourcekey="FAQ" runat="server"></asp:HyperLink> |
             <asp:HyperLink ID="hprlnkGuidelines" resourcekey="Guidelines" runat="server"></asp:HyperLink>
	</div>