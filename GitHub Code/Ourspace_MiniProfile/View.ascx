<%@ Control language="C#" Inherits="DotNetNuke.Modules.Ourspace_MiniProfile.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
<%--<div id="fb-root"></div>--%>
<div id="mini-profile">
    <asp:Image ID="img_Profile" CssClass="profilePicture" runat="server" />
<asp:Label ID="lblHello" resourcekey="Hello" runat="server"
    Text="Hello"></asp:Label> 
    <asp:HyperLink ID="hprlnkName" runat="server"  CssClass="mini-profile-name"><asp:Label ID="lblName"  runat="server" Text=""></asp:Label></asp:HyperLink><br />
<asp:Label ID="lblYouHave" runat="server" resourcekey="YouHave" Text="You have"></asp:Label>:<br />
<asp:Label ID="lblPointsNo" runat="server" Text=""></asp:Label> <asp:Label ID="lblPoints" resourcekey="Points" runat="server" Text="points"></asp:Label> (<asp:Label ID="lblLevel" runat="server" Text="Level"></asp:Label> <asp:Label
    ID="lblLevelNo" runat="server" Text="0"></asp:Label>)
    <div class="cleared"></div>
   
 
<div>
<asp:HyperLink ID="hprlnkPointsInfo" CssClass="pointsAndLevelsInfo" runat="server" resourcekey="PointsAndLevels" Text="What are Points and Levels?"></asp:HyperLink>
</div>




</div>