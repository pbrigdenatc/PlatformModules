<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_PeopleSearch.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<%@ Register Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls"
    TagPrefix="dnnweb" %>
<div class="hidden-title">
    <%= DotNetNuke.Services.Localization.Localization.GetString("searchTitle.Text", LocalResourceFile)%></div>
<h2>
    <%= DotNetNuke.Services.Localization.Localization.GetString("searchPeople.Text", LocalResourceFile)%></h2>
<div class="hidden personSearchBtnWrapper" id="">
    <span id="peopleSearch"></span>
</div>


<div class="tabs-wrapper">
                <div class="cleared">
                </div>
               
    <asp:LinkButton ID="lnkbtn_HallOfFame" CssClass="tab-active" runat="server" onclick="lnkbtn_HallOfFame_Click">Hall of Fame</asp:LinkButton>
     <asp:LinkButton ID="lnkbtn_ContestProgress" CssClass="tab-inactive" runat="server" 
                    onclick="lnkbtn_ContestProgress_Click">Contest progress</asp:LinkButton>

                <div class="tab-line">
                </div>
            </div>










<dnnweb:DnnGrid AutoGenerateColumns="False" ID="grid_allPeople" DataSourceID="sqldtsrc_AllPeople"
    AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" runat="server">
    <PagerStyle Mode="NextPrevAndNumeric" />
    <GroupingSettings CaseSensitive="false" />
    <MasterTableView TableLayout="Fixed">
        <Columns>
            <dnnweb:DnnGridBoundColumn HeaderText="First Name" DataField="DisplayName" UniqueName="DisplayName"
                SortExpression="DisplayName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                ShowFilterIcon="false" />
        </Columns>
        <ItemTemplate>
            <div class="search-people-item">
                <img src="<%# GetUserImgUrl(Eval("UserId").ToString()) %>" alt="" />
                <b><a class="userProfileLink" href="<%# GetUserProfileUrl(Eval("UserId").ToString()) %>">
                    <%# Eval("DisplayName")%></b>
                </a>
                <div class="small-flag-<%# Eval("lang") %> search-people-flag">
                </div>
                <span class="userProfileLink">
                   (<asp:Label ID="lblLevel" resourcekey="Level" runat="server" Text=""></asp:Label>  <span><asp:Label ID="Label1"  runat="server" Text='<%# GetUserLevelAndName( Eval("points").ToString())%>'></asp:Label></span>)
               </span>
                <div class="points">
                    <span class="points-large"><span>
                        <%# Eval("points")%></span></span>&nbsp; <span>
                            <%# GetPointsNoun( Eval("points").ToString())
                            %></span>
        
                </div>
            </div>
        </ItemTemplate>
        <PagerTemplate>
            <asp:Panel ID="PagerPanel" CssClass="rgNumPart" Style="padding: 6px; line-height: 24px"
                runat="server">
                <div style="float: left">
                </div>
                <div style="margin: 0px; float: right;">
                    <%# (int)DataBinder.Eval(Container, "Paging.FirstIndexInPage") + 1 %>
                    -
                    <%# (int)DataBinder.Eval(Container, "Paging.LastIndexInPage") + 1 %>
                    <%# DotNetNuke.Services.Localization.Localization.GetString("of.Text", LocalResourceFile)%>
                    <%# DataBinder.Eval(Container, "Paging.DataSourceCount")%>
                </div>
                <div style="width: 260px; margin: 0px; padding: 0px; float: left; margin-right: 10px;
                    white-space: nowrap;">
                    <asp:LinkButton ID="Button2" runat="server" OnClientClick="changePage('prev'); return false;"
                        CommandName="Page" CommandArgument="Prev" Text="<" CssClass="PagerButton PrevPage" />
                    <span style="vertical-align: middle;">
                        <%# DotNetNuke.Services.Localization.Localization.GetString("Page.Text", LocalResourceFile)%>:
                        <%# (int)DataBinder.Eval(Container, "Paging.CurrentPageIndex") + 1 %></span>
                    <span style="vertical-align: middle;">
                        <%# DotNetNuke.Services.Localization.Localization.GetString("of.Text", LocalResourceFile)%>
                        <%# DataBinder.Eval(Container, "Paging.PageCount")%>
                    </span>
                    <asp:LinkButton ID="Button3" runat="server" OnClientClick="changePage('next'); return false;"
                        CommandName="Page" CommandArgument="Next" Text=">" CssClass="PagerButton NextPage" />
                </div>
                <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
            </asp:Panel>
        </PagerTemplate>
    </MasterTableView>
</dnnweb:DnnGrid>
<div class="info-div"><div class="info-icon">The users that have brought the most new users that have at least 20 points on the platform are listed below.</div></div>

<asp:ListView ID="lstvw_ContestantHallOfFame" runat="server" Visible="false">
<LayoutTemplate>

<table id="itemPlaceholderContainer" class="RadGrid_Simple" runat="server" border="0" style="">
                           <tr id="itemPlaceholder" runat="server">
                           <td></td>
                            </tr>
                        </table>

</LayoutTemplate>
<ItemTemplate>
<tr class="rgRow">
<td>
 <div class="search-people-item">
                <img src="<%# GetUserImgUrl(Eval("referralUserId").ToString()) %>" alt="" />
                <b><a class="userProfileLink" href="<%# GetUserProfileUrl(Eval("referralUserId").ToString()) %>">
                    <%# Eval("DisplayName")%></b>
                </a>
                <div class="small-flag-<%# Eval("PropertyValue") %> search-people-flag">
                </div>
                <span class="userProfileLink">
                   
               </span>
                <div class="points">
                    <span class="points-large"><span>
                        <%# Eval("UserCount")%></span> </span>referrals
        
                </div>
            </div></td>
            </tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr class="rgAltRow">
<td>
 <div class="search-people-item">
                <img src="<%# GetUserImgUrl(Eval("referralUserId").ToString()) %>" alt="" />
                <b><a class="userProfileLink" href="<%# GetUserProfileUrl(Eval("referralUserId").ToString()) %>">
                    <%# Eval("DisplayName")%></b>
                </a>
                <div class="small-flag-<%# Eval("PropertyValue") %> search-people-flag">
                </div>
                <span class="userProfileLink">
                   
               </span>
                <div class="points">
                    <span class="points-large"><span>
                        <%# Eval("UserCount")%></span></span> referrals
        
                </div>
            </div>

</td>
            </tr>
</AlternatingItemTemplate>

<EmptyDataTemplate><div class="info-div"><div class="info-icon">No one has brought any users to OurSpace with at least 20 points yet!</div></div></EmptyDataTemplate>
</asp:ListView>


<asp:LinkButton ID="lnkbtn_testUser" Visible="false" runat="server" 
    onclick="lnkbtn_testUser_Click">Test User</asp:LinkButton>
<asp:Label ID="lblLog" runat="server" Text=""></asp:Label>


<asp:SqlDataSource ID="sqldtsrc_ContestantHallOfFame" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    SelectCommand="SELECT     COUNT(Ourspace_Forum_User_Info.referralUserId) AS UserCount, Users.UserId as referralUserId, Users.DisplayName,Ourspace_Forum_User_Info_1.facebookId, UserProfile.PropertyValue 
                      
FROM         Ourspace_Forum_User_Info INNER JOIN
                      Users ON Ourspace_Forum_User_Info.referralUserId = Users.UserID INNER JOIN
                      UserProfile ON Users.UserID = UserProfile.UserID INNER JOIN
                      Ourspace_Forum_User_Info AS Ourspace_Forum_User_Info_1 ON Users.UserID = Ourspace_Forum_User_Info_1.userId
WHERE     (Ourspace_Forum_User_Info.referralUserId &gt; - 1) AND (UserProfile.PropertyDefinitionID = 50) AND (Ourspace_Forum_User_Info.points &gt; 19)
GROUP BY Users.DisplayName, UserProfile.PropertyValue, Ourspace_Forum_User_Info_1.facebookId, Users.UserId
ORDER BY UserProfile.PropertyValue, UserCount DESC">
    <SelectParameters>
        <asp:SessionParameter Name="userNationality" 
            SessionField="PeopleSearch_UserNationality" />
    </SelectParameters>
</asp:SqlDataSource>
<%-- AND (UserProfile.PropertyValue = @userNationality)--%>

<asp:SqlDataSource ID="sqldtsrc_AllPeople" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Users.UserID, Users.FirstName, Users.LastName, Users.Username, UserProfile.PropertyValue AS lang, Ourspace_Forum_User_Info.points, Users.DisplayName FROM Users INNER JOIN UserProfile ON Users.UserID = UserProfile.UserID INNER JOIN ProfilePropertyDefinition ON UserProfile.PropertyDefinitionID = ProfilePropertyDefinition.PropertyDefinitionID INNER JOIN Ourspace_Forum_User_Info ON Users.UserID = Ourspace_Forum_User_Info.userId WHERE (Users.IsDeleted = 0) AND (UserProfile.PropertyDefinitionID = 40) ORDER BY Ourspace_Forum_User_Info.points DESC">
</asp:SqlDataSource>
