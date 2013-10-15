<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_ResultsManager.View"    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<div class="info-div">
    <div class="info-icon">
        <div>
            <asp:Label ID="lbl_Phase4Info" runat="server" resourcekey="Phase4Info" Text="Label"></asp:Label>
        </div>
        <div class="cleared">
        </div>
    </div>
</div>
<div class="info-div" style="margin-top:15px;">
            <div class="info-icon">
               
                <asp:Panel ID="pnlNational" runat="server">
                    <asp:Panel ID="pnlViewingNational" runat="server" >
                        <asp:Label ID="Label10" runat="server" resourcekey="ViewingNationalDebatesOwnLang" Text="You are currently viewing National Debates in your language. You can also"></asp:Label>
                        
                        <asp:LinkButton ID="lnkbtnViewAllLanguages" resourcekey="ViewNatDebAllLangLink" CssClass="bold-link" runat="server" OnClick="lnkbtnViewAllLanguages_Click" Text="click here to view debates in all languages"></asp:LinkButton>.
                    </asp:Panel>
                    <asp:Panel ID="pnlViewingAll" runat="server"   Visible="false">
                    <asp:Label ID="Label11" runat="server" resourcekey="ViewingNationalDebatesAllLang" Text="You are currently viewing National Debates in all languages. You can also"></asp:Label>
                        
                        
                        <asp:LinkButton ID="lnkbtnViewOwnLanguage" resourcekey="ViewNatDebOwnLangLink" CssClass="bold-link" runat="server" OnClick="lnkbtnViewOwnLanguage_Click"></asp:LinkButton>.
                    </asp:Panel>
                </asp:Panel>
                <div class="cleared">
                    &nbsp;</div>
            </div>
        </div>
<h2 class="fleft">
<asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblTitle" runat="server" resourcekey="TopicDebates"></asp:Label></h2>

      <div class="sortingDropdown">
<div class="debateSortLoading hidden"></div>
    <b><asp:Label ID="lblSortBy" runat="server" resourcekey="SortBy" Text="Sort by"></asp:Label>:</b>
 <asp:DropDownList ID="ddlSortDebates" CssClass="ddlSortDebates"   runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlSortDebates_SelectedIndexChanged">
        <asp:ListItem  Value="Popularity" resourcekey="Popularity" />
        <asp:ListItem  Value="Date" resourcekey="Date" />
          <asp:ListItem  Value="Title" resourcekey="Title" />
        </asp:DropDownList>
        </div>
            <div class="cleared"></div>

<div class="proposals-wrapper">
    <asp:ListView ID="lstvw_DebateResults" runat="server" DataSourceID="sqldtsrc_DebateResultsOwnLang"
        EnableModelValidation="True" OnItemDataBound="lstvw_DebateProposals_ItemDataBound">
        <EmptyDataTemplate>
            <table id="Table5" runat="server" style="">
            <tr>
                <td>
                   <div class="info-div" style="margin-top:15px;">
            <div class="info-icon"><asp:Label ID="Label4" runat="server" resourcekey="NoResults" Text=""></asp:Label>
            </div>
            </div>
                </td>
            </tr>
        </table>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td runat="server" id="imageTd" class="debate-thumbnail">
                    <asp:Literal ID="ltrlImage" runat="server"></asp:Literal>
                </td>
                <td runat="server" id="textTd">
                    <div>
                        <asp:HyperLink CssClass="bold-link debate-title" ID="hprlnk_subject" runat="server">
                            <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></asp:HyperLink><a
                                href="#" class="bold-link debate-title"></a></div><div style="margin: 3px 0 3px 0; color: #999;">
                        <asp:Label ID="lbl_by" runat="server" Text="By"></asp:Label>&nbsp;<asp:HyperLink ID="hprlnk_userProfile" runat="server">
                            <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;<asp:Label
                                ID="Label1" runat="server" Text='<%# Eval("LastName") %>' /></asp:HyperLink>- <asp:Label ID="lbl_date" runat="server" Text="On"></asp:Label>&nbsp;<asp:Label ID="CreatedDateLabel"
                            runat="server" Text='<%# Eval("CreatedDate") %>' />
                        - <asp:Label ID="lbl_Views" runat="server" Text="Views"></asp:Label>: <asp:Label ID="ViewsLabel" runat="server" Text='<%# Eval("Views") %>' />
                        <asp:Label ID="UsernameLabel" Visible="false" runat="server" Text='<%# Eval("Username") %>' />
                        <div class="deliberation-small-flag small-flag-<%# Eval("ThreadLanguage") %>">
                        </div>
                    </div>
                    <div>
                        <%--<asp:Label ID="BodyLabel" runat="server" Text='<%# GetTrimmedBody( Eval("Body").ToString()) %>' />--%>
                        <asp:Label ID="lbl_Body" runat="server" Text='<%# Eval("Body") %>' />
                        <asp:HyperLink ID="hprlnk_post" runat="server">View complete proposal &raquo;
                        </asp:HyperLink></div><div class="favorite-solution">
                        <asp:Label ID="lbl_FavoriteSolution" runat="server" Text="Favorite Proposed Solution:"></asp:Label></div></td></tr><tr style="">
                <td valign="top" colspan="2">
                    <br />
                    <asp:Label ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' />
                    <asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' />
                    <asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("userId") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table id="Table2" runat="server" style="float: left; clear: left;">
                <tr id="Tr1" runat="server">
                    <td id="Td1" runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="Tr2" runat="server">
                    <td id="Td2" runat="server" style="" class="pager-wrapper">
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="10" PagedControlID="lstvw_DebateResults">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="pager-link" ShowNextPageButton="False"
                                    ShowPreviousPageButton="True" />
                                <asp:NumericPagerField NumericButtonCssClass="pager-link" CurrentPageLabelCssClass="pager-link-inactive" />
                                <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="pager-link" ShowNextPageButton="True"
                                    ShowPreviousPageButton="False" />
                            </Fields>
                        </asp:DataPager>
                         <asp:DataPager ID="DataPager2" Visible="true" runat="server" PageSize="10" PagedControlID="lstvw_DebateResults">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="pager-link" ShowNextPageButton="False"
                                    ShowPreviousPageButton="True" />
                                <asp:NumericPagerField NumericButtonCssClass="pager-link" CurrentPageLabelCssClass="pager-link-inactive" />
                                <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="pager-link" ShowNextPageButton="True"
                                    ShowPreviousPageButton="False" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
             
            </table>
        </LayoutTemplate>
    </asp:ListView>
    <div class="cleared">
    </div>
</div>
<asp:SqlDataSource ID="sqldtsrc_DebateResults" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Proposals_Thumbs.ThumbsUp, Ourspace_Proposals_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Ourspace_Forum_Thread_Info.phaseId, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Proposals_Thumbs ON Forum_Threads.ThreadID = Ourspace_Proposals_Thumbs.ThreadID WHERE (Forum_Groups.ModuleID = 381) AND (Ourspace_Forum_Thread_Info.phaseId = 4) AND (Forum_Posts.ParentPostID = 0) ORDER BY Ourspace_Proposals_Thumbs.ThumbsUp DESC, Ourspace_Proposals_Thumbs.ThumbsDown"
    OnSelected="sqldtsrc_DebateResults_Selected"></asp:SqlDataSource>

    <asp:SqlDataSource 
    ID="sqldtsrc_DebateResultsByDate" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Proposals_Thumbs.ThumbsUp, Ourspace_Proposals_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Ourspace_Forum_Thread_Info.phaseId, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Proposals_Thumbs ON Forum_Threads.ThreadID = Ourspace_Proposals_Thumbs.ThreadID WHERE (Forum_Groups.ModuleID = 381) AND (Ourspace_Forum_Thread_Info.phaseId = 4) AND (Forum_Posts.ParentPostID = 0) ORDER BY Forum_Posts.CreatedDate DESC, Ourspace_Proposals_Thumbs.ThumbsUp DESC, Ourspace_Proposals_Thumbs.ThumbsDown"
    OnSelected="sqldtsrc_DebateResults_Selected"></asp:SqlDataSource>

    <asp:SqlDataSource 
    ID="sqldtsrc_DebateResultsByTitle" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Proposals_Thumbs.ThumbsUp, Ourspace_Proposals_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Ourspace_Forum_Thread_Info.phaseId, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Proposals_Thumbs ON Forum_Threads.ThreadID = Ourspace_Proposals_Thumbs.ThreadID WHERE (Forum_Groups.ModuleID = 381) AND (Ourspace_Forum_Thread_Info.phaseId = 4) AND (Forum_Posts.ParentPostID = 0) ORDER BY Forum_Posts.Subject, Ourspace_Proposals_Thumbs.ThumbsUp DESC, Ourspace_Proposals_Thumbs.ThumbsDown"
    OnSelected="sqldtsrc_DebateResults_Selected"></asp:SqlDataSource>

    <asp:SqlDataSource 
    ID="sqldtsrc_DebateResultsOwnLang" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Proposals_Thumbs.ThumbsUp, Ourspace_Proposals_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Ourspace_Forum_Thread_Info.phaseId, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Proposals_Thumbs ON Forum_Threads.ThreadID = Ourspace_Proposals_Thumbs.ThreadID WHERE (Forum_Groups.ModuleID = 381) AND (Ourspace_Forum_Thread_Info.phaseId = 4) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = @lang) ORDER BY Ourspace_Proposals_Thumbs.ThumbsUp DESC, Ourspace_Proposals_Thumbs.ThumbsDown"
    OnSelected="sqldtsrc_DebateResults_Selected"><SelectParameters><asp:SessionParameter 
            DefaultValue="en-GB" Name="lang" SessionField="resultsManagerOwnLang" /></SelectParameters></asp:SqlDataSource>
<asp:SqlDataSource 
    ID="sqldtsrc_DebateResultsOwnLangByDate" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Proposals_Thumbs.ThumbsUp, Ourspace_Proposals_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Ourspace_Forum_Thread_Info.phaseId, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Proposals_Thumbs ON Forum_Threads.ThreadID = Ourspace_Proposals_Thumbs.ThreadID WHERE (Forum_Groups.ModuleID = 381) AND (Ourspace_Forum_Thread_Info.phaseId = 4) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = @lang) ORDER BY Forum_Posts.CreatedDate DESC, Ourspace_Proposals_Thumbs.ThumbsUp DESC, Ourspace_Proposals_Thumbs.ThumbsDown"
    OnSelected="sqldtsrc_DebateResults_Selected"><SelectParameters><asp:SessionParameter 
            DefaultValue="en-GB" Name="lang" SessionField="resultsManagerOwnLang" /></SelectParameters></asp:SqlDataSource>
<asp:SqlDataSource 
    ID="sqldtsrc_DebateResultsOwnLangByTitle" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Proposals_Thumbs.ThumbsUp, Ourspace_Proposals_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Ourspace_Forum_Thread_Info.phaseId, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Proposals_Thumbs ON Forum_Threads.ThreadID = Ourspace_Proposals_Thumbs.ThreadID WHERE (Forum_Groups.ModuleID = 381) AND (Ourspace_Forum_Thread_Info.phaseId = 4) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = @lang) ORDER BY Forum_Posts.Subject, Ourspace_Proposals_Thumbs.ThumbsUp DESC, Ourspace_Proposals_Thumbs.ThumbsDown"
    OnSelected="sqldtsrc_DebateResults_Selected"><SelectParameters><asp:SessionParameter 
            DefaultValue="en-GB" Name="lang" SessionField="resultsManagerOwnLang" /></SelectParameters></asp:SqlDataSource>
<asp:SqlDataSource 
    ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
