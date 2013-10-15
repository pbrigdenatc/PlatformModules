<%@ Control language="C#" Inherits="DotNetNuke.Modules.OurspaceNotifications.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>

<asp:Repeater ID="NotificationsRepeater" runat="server" 
    onitemdatabound="NotificationsRepeater_ItemDataBound">
<HeaderTemplate><ul class="notifications-list"></HeaderTemplate>
<ItemTemplate>
    <li><a href="#"><%#Eval("Creator")%> </a> <%#Eval("ActionType")%> your post about
        <a href='<%#Eval("PostUrl")%>'><%#Eval("PostSubject")%></a>.</li>
</ItemTemplate>
<FooterTemplate></ul>
    <asp:Label ID="lblEmptyData"
        Text="No new notifications" runat="server" Visible="false">
 </asp:Label>
</FooterTemplate>



</asp:Repeater>

