<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_Statistics.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<div style="" class="statistics-wrapper">
    <asp:Label ID="lbl_Info" CssClass="statistics-info" runat="server" resourcekey="UsageStatistics" Text="Usage statistics:"></asp:Label><br />
&bull;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" resourcekey="TotalPosts" Text="Total posts:"></asp:Label>
<asp:ListView ID="lstvw_TotalPosts"  runat="server" DataSourceID="sqldtsrc_TotalPosts"
    EnableModelValidation="True">
    <ItemTemplate>
        <b><asp:Label ID="PostsLabel" runat="server" Text='<%# Eval("Posts") %>' /></b>
    </ItemTemplate>
    <LayoutTemplate>
        <span runat="server"><span id="itemPlaceholderContainer" runat="server" border="0"
            style=""><span id="itemPlaceholder" runat="server"></span></span></span>
    </LayoutTemplate>
</asp:ListView>&nbsp;&nbsp;
&bull;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" resourcekey="TotalTopics" Text="Total topics:"></asp:Label>
<asp:ListView ID="lstvw_TotalTopics" runat="server" DataSourceID="sqldtsrc_TotalTopics"
    EnableModelValidation="True">
    <ItemTemplate>
        <b><asp:Label ID="TopicsLabel" runat="server" Text='<%# Eval("Topics") %>' /></b>
    </ItemTemplate>
    <LayoutTemplate>
        <span id="Span1" runat="server"><span id="itemPlaceholderContainer" runat="server"
            border="0" style=""><span id="itemPlaceholder" runat="server"></span></span>
        </span>
    </LayoutTemplate>
</asp:ListView>
&nbsp;&nbsp;
&bull;&nbsp;&nbsp;<asp:Label ID="Label7" runat="server" resourcekey="TotalMembers" Text="Total members:"></asp:Label>
<asp:ListView ID="lstvw_TotalMembers" runat="server" DataSourceID="sqldtsrc_TotalMembers"
    EnableModelValidation="True">
    <ItemTemplate>
        <b><asp:Label ID="UsersLabel" runat="server" Text='<%# Eval("Users") %>' /></b>
    </ItemTemplate>
    <LayoutTemplate>
        <span id="Span2" runat="server"><span id="itemPlaceholderContainer" runat="server"
            border="0" style=""><span id="itemPlaceholder" runat="server"></span></span>
        </span>
    </LayoutTemplate>
</asp:ListView>
&nbsp;&nbsp;&bull;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" resourcekey="NewestMember" Text=""></asp:Label>: 
<asp:ListView ID="lstvw_NewestMember" runat="server" DataSourceID="sqldtsrc_NewestMember"
    EnableModelValidation="True" 
        onitemdatabound="lstvw_NewestMember_ItemDataBound">
    <ItemTemplate>
        <asp:Label ID="lblUserId" Visible="false" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
    
  <asp:HyperLink ID="hprlnkUserProfile" runat="server"><b>
        <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;<asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' /></b></asp:HyperLink><asp:Label Visible="false" ID="CreatedOnDateLabel" runat="server" Text='<%# Eval("CreatedOnDate") %>' />
   </ItemTemplate>
    <LayoutTemplate>
        <span id="Span1" runat="server"><span id="itemPlaceholderContainer" runat="server"
            border="0" style=""><span id="itemPlaceholder" runat="server"></span></span>
        </span>
    </LayoutTemplate>
</asp:ListView></div>
<asp:SqlDataSource ID="sqldtsrc_TotalPosts" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT COUNT(*) AS Posts FROM Forum_Posts"></asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_TotalTopics" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT COUNT(*) AS Topics FROM Forum_Threads"></asp:SqlDataSource>


<%--<asp:SqlDataSource ID="sqldtsrc_TotalMembers" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT COUNT(*) AS Users FROM Users"></asp:SqlDataSource>--%>

<asp:SqlDataSource ID="sqldtsrc_TotalMembers" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT COUNT(*) AS Users FROM Users INNER JOIN UserProfile ON Users.UserID = UserProfile.UserID INNER JOIN ProfilePropertyDefinition ON UserProfile.PropertyDefinitionID = ProfilePropertyDefinition.PropertyDefinitionID INNER JOIN Ourspace_Forum_User_Info ON Users.UserID = Ourspace_Forum_User_Info.userId WHERE (Users.IsDeleted = 0) AND (UserProfile.PropertyDefinitionID = 50)"></asp:SqlDataSource>


<asp:SqlDataSource ID="sqldtsrc_NewestMember" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT TOP (1) FirstName, LastName, CreatedOnDate, UserId FROM Users ORDER BY CreatedOnDate DESC">
</asp:SqlDataSource>
