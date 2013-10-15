<%@ Control language="C#" Inherits="DotNetNuke.Modules.HallOfFame.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>




<div class="hall-of-fame-wrapper">
<div class="hall-of-fame-header">
   
        <asp:LinkButton ID="weeklyLink" runat="server" resourcekey="cmdWeeklyLink" ></asp:LinkButton> |
   
        <asp:LinkButton ID="monthlyLink" runat="server" resourcekey="cmdMonthlyLink"></asp:LinkButton> |
   
        <asp:LinkButton ID="allLink" runat="server" resourcekey="cmdAllLink" CssClass="selected"></asp:LinkButton>
</div>
<div class="users-row">
    <div class="top-user">
        <div class="user-position">1</div><a href="#"><asp:Image ID="ImageTopUser1" Height="40" Width="40" runat="server" /></a><asp:HyperLink ID="hprlnk_TopUser1" NavigateUrl="#"
                runat="server"></asp:HyperLink><br /><asp:Label ID="LabelTopUser1"  runat="server"></asp:Label>
    </div>
    <div class="top-user">
         <div class="user-position-small">2</div><a href="#"><asp:Image ID="ImageTopUser2" Height="25" Width="25" runat="server" /></a><asp:HyperLink ID="hprlnk_TopUser2" NavigateUrl="#"
                runat="server"></asp:HyperLink><br /><asp:Label ID="LabelTopUser2" runat="server"></asp:Label>
    </div>
    <div class="top-user">
        <div class="user-position-small">3</div><a href="#"><asp:Image Height="25" Width="25" ID="ImageTopUser3" runat="server" /></a><asp:HyperLink ID="hprlnk_TopUser3" NavigateUrl="#"
                runat="server"></asp:HyperLink><br /><asp:Label ID="LabelTopUser3" runat="server"></asp:Label>
     </div>
</div>
<div class="users-row">
    <div class="top-user">
         <div class="user-position-small">4</div><a href="#"><asp:Image ID="ImageTopUser4" Height="25" Width="25" runat="server" /></a><asp:HyperLink ID="hprlnk_TopUser4" NavigateUrl="#"
                runat="server"></asp:HyperLink><br /><asp:Label ID="LabelTopUser4" runat="server"></asp:Label>
    </div>
    <div class="top-user">
         <div class="user-position-small">5</div><a href="#"><asp:Image ID="ImageTopUser5" Height="25" Width="25" runat="server" /></a><asp:HyperLink ID="hprlnk_TopUser5" NavigateUrl="#"
                runat="server"></asp:HyperLink><br /><asp:Label ID="LabelTopUser5" runat="server"></asp:Label>
    </div>
</div>
<div style="clear:both;"></div>
<a href="#" class="all-leaders-link hidden">Check all the Leaderboard...</a></div>