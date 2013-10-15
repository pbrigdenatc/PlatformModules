<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_Overview.View" AutoEventWireup="false"
    CodeBehind="View.ascx.cs" %>
<%@ Register Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls"
    TagPrefix="dnnweb" %>
&nbsp;&nbsp;<asp:Label ID="lblHaveYOurSay" CssClass="hidden" runat="server" resourcekey="haveYourSay"
    Text=""></asp:Label>
<asp:Panel ID="pnlAdminControls" Visible="false" runat="server">
    <div class="tabs-wrapper">
        <div class="cleared">
        </div>
        <asp:LinkButton ID="lnkbtn_ViewTopicsOverview" resourcekey="TopicsOverview" runat="server"
            CssClass="tab-active" OnClick="lnkbtn_ViewTopicsOverview_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkbtn_ViewReportedPosts" resourcekey="ManageReportedPosts" runat="server"
            CssClass="tab-inactive" OnClick="lnkbtn_ViewReportedPosts_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkbtn_ViewPlatformStatistics" resourcekey="PlatformStatistics"
            runat="server" CssClass="tab-inactive" OnClick="lnkbtn_ViewPlatformStatistics_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkbtn_ViewUserDetails" resourcekey="UserEmails" runat="server"
            CssClass="tab-inactive" OnClick="lnkbtn_ViewUserDetails_Click"></asp:LinkButton>
        <div class="tab-line">
        </div>
    </div>
</asp:Panel>
<div class="cleared">
</div>
<%--<asp:UpdatePanel ID="updtpnl_AllThreads" runat="server">
    <ContentTemplate>--%>
    <asp:Panel ID="updtpnl_AllThreads" runat="server">
        <div class="updatePanel">
            <div class="tabs-wrapper">
                <div class="cleared">
                </div>
                <asp:LinkButton ID="lnkbtnNationalDebates" runat="server" Text="" CssClass="tab-active"
                    resourcekey="NationalDebates" OnClick="lnkbtnNationalDebates_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnkbtnEuDebates" resourcekey="EuDebates" runat="server" Text="EU Debates"
                    CssClass="tab-inactive" OnClick="lnkbtnEuDebates_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lnkbtnFeaturedDebates" resourcekey="Featured" runat="server" Text="EU Debates"
                    CssClass="tab-inactive" OnClick="lnkbtnFeaturedDebates_Click"></asp:LinkButton>
                <div class="tab-line">
                </div>
            </div>
            <div class="info-div">
                <div class="info-icon">
                    <asp:Label ID="lblOverviewInfo" runat="server" resourcekey="overviewInfo"></asp:Label>
                    <asp:Panel ID="pnlEu" runat="server" Visible="false">
                        <asp:Label ID="Label12" runat="server" resourcekey="ViewingEuDebates" Text="You are currently viewing EU Debates."></asp:Label>
                    </asp:Panel>
                    
                     <asp:Panel ID="pnlFeatured" runat="server" Visible="false">
                        <asp:Label ID="Label15" runat="server" resourcekey="FeaturedInfo" Text="You are currently viewing EU Debates."></asp:Label>
                    </asp:Panel>

                    <asp:Panel ID="pnlNational" runat="server">
                        <asp:Panel ID="pnlViewingNational" runat="server">
                            <asp:Label ID="Label10" runat="server" resourcekey="ViewingNationalDebatesOwnLang"
                                Text="You are currently viewing National Debates in your language. You can also"></asp:Label>
                            <asp:LinkButton ID="lnkbtnViewAllLanguages" resourcekey="ViewNatDebAllLangLink" CssClass="bold-link"
                                runat="server" OnClick="lnkbtnViewAllLanguages_Click" Text="click here to view debates in all languages"></asp:LinkButton>.
                        </asp:Panel>
                        <asp:Panel ID="pnlViewingAll" runat="server" Visible="false">
                            <asp:Label ID="Label11" runat="server" resourcekey="ViewingNationalDebatesAllLang"
                                Text="You are currently viewing National Debates in all languages. You can also"></asp:Label>
                            <asp:LinkButton ID="lnkbtnViewOwnLanguage" resourcekey="ViewNatDebOwnLangLink" CssClass="bold-link"
                                runat="server" OnClick="lnkbtnViewOwnLanguage_Click"></asp:LinkButton>.
                        </asp:Panel>
                    </asp:Panel>
                    <div class="cleared">
                        &nbsp;</div>
                </div>
            </div>
            <asp:ListView ID="lstvw_OverviewItems" runat="server" DataSourceID="sqldtsrc_ActiveDiscussions"
                EnableModelValidation="True" OnItemDataBound="lstvw_OverviewItems_ItemDataBound">
                <EmptyDataTemplate>
                    <table id="Table5" runat="server" style="">
                        <tr>
                            <td>
                                No data was returned.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td runat="server" id="textTd" class="overview-item-wrap-left">
                            <div>
                                <asp:HyperLink CssClass="bold-link debate-title" ID="hprlnk_subject" runat="server">
                                    <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></asp:HyperLink><div
                                        class="category-info">
                                        <asp:Label ID="Label7" runat="server" resourcekey="OpenDebateOn"></asp:Label>&nbsp;<b><asp:Label
                                            ID="Label6" runat="server" Text='<%# GetLocalizedCategory(Eval("Name").ToString()) %>' /></b>
                                    </div>
                            </div>
                            <div class="overview-text-wrap" style="">
                                <asp:Label ID="lbl_by" runat="server" resourcekey="By" Text="By"></asp:Label>&nbsp;<asp:HyperLink
                                    ID="hprlnk_userProfile" runat="server">
                                    <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;<asp:Label
                                        ID="Label1" runat="server" Text='<%# Eval("LastName") %>' /></asp:HyperLink>- <asp:Label ID="lbl_date" resourcekey="On" runat="server" Text="On"></asp:Label>&nbsp;<asp:Label
                                    ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' />
                                <br />
                                <asp:Label ID="lbl_Views" runat="server" resourcekey="Views" Text="Views"></asp:Label>: <asp:Label ID="ViewsLabel" runat="server" Text='<%# Eval("Views") %>' />
                                <div class="overview-small-flag small-flag-<%# Eval("ThreadLanguage") %>">
                                </div>
                                &nbsp; <asp:Label ID="lblRejectedDash" Visible="False" runat="server" Text="-"></asp:Label>&nbsp;<span
                                    style="color: #333;"><b><i><asp:Label ID="lblRejected" resourcekey="rejected" runat="server"
                                        Visible="false" Text=""></asp:Label></i></b></span><asp:Label ID="lblRejectReasonId"
                                            Visible="false" runat="server" Text='<%# Eval("rejectReasonId") %>'></asp:Label><asp:Label
                                                ID="UsernameLabel" Visible="false" runat="server" Text='<%# Eval("Username") %>' />
                            </div>
                            <div>
                                <asp:Label ID="lbl_Body" runat="server" Text='<%# Eval("Body") %>' Visible="false" />
                                <asp:HyperLink ID="hprlnk_post" Visible="false" runat="server">View complete proposal &raquo;
                                </asp:HyperLink><asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' /><asp:Label
                                    ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' /></div>
                        </td>
                        <td class="overview-item-wrap">
                            <asp:Label ID="lblUserId" Visible="false" runat="server" Text='<%# Eval("UserID") %>' />
                            <asp:Label ID="lblPhaseId" runat="server" Visible="false" Text='<%# Eval("phaseId") %>' />
                            <div class="thread-phases-container">
                                <table cellspacing="0" cellpadding="0" border="0" align="center" class="phases-arrow-wrap">
                                    <tbody>
                                        <tr id="">
                                            <td>
                                                <div runat="server" id="phase1" class="">
                                                    <asp:HyperLink ID="hprlnk_Phase1" runat="server">
                                                        <asp:Label ID="lblPhase1" runat="server" resourcekey="Suggest" Text="Suggest"></asp:Label></asp:HyperLink></div></td><td>
                                                <div runat="server" id="phase2" class="">
                                                    <asp:HyperLink ID="hprlnk_Phase2" runat="server">
                                                        <asp:Label ID="lblPhase2" runat="server" resourcekey="Join" Text="Join"></asp:Label></asp:HyperLink></div></td><td>
                                                <div runat="server" id="phase3" class="">
                                                    <asp:HyperLink ID="hprlnk_Phase3" runat="server">
                                                        <asp:Label ID="lblPhase3" runat="server" resourcekey="Vote" Text="Vote"></asp:Label></asp:HyperLink></div></td><td>
                                                <div runat="server" id="phase4" class="">
                                                    <asp:HyperLink ID="hprlnk_Phase4" runat="server">
                                                        <asp:Label ID="lblPhase4" runat="server" resourcekey="Results" Text="Results"></asp:Label></asp:HyperLink></div></td></tr></tbody></table></div></td></tr></ItemTemplate><AlternatingItemTemplate>
                    <tr>
                        <td runat="server" id="textTd" class="overview-item-wrap alt-item overview-item-wrap-left">
                            <div>
                                <asp:HyperLink CssClass="bold-link debate-title" ID="hprlnk_subject" runat="server">
                                    <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></asp:HyperLink><div
                                        class="category-info">
                                        <asp:Label ID="Label7" runat="server" resourcekey="OpenDebateOn"></asp:Label>&nbsp;<b><asp:Label
                                            ID="Label6" runat="server" Text='<%# GetLocalizedCategory(Eval("Name").ToString()) %>' /></b>
                                    </div>
                            </div>
                            <div class="overview-text-wrap" style="">
                                <asp:Label ID="lbl_by" runat="server" resourcekey="By" Text="By"></asp:Label>&nbsp;<asp:HyperLink
                                    ID="hprlnk_userProfile" runat="server">
                                    <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;<asp:Label
                                        ID="Label1" runat="server" Text='<%# Eval("LastName") %>' /></asp:HyperLink>- <asp:Label ID="lbl_date" runat="server" resourcekey="On" Text="On"></asp:Label>&nbsp;<asp:Label
                                    ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' />
                                <br />
                                <asp:Label ID="lbl_Views" runat="server" resourcekey="Views" Text="Views"></asp:Label>: <asp:Label ID="ViewsLabel" runat="server" Text='<%# Eval("Views") %>' />
                                <div class="overview-small-flag small-flag-<%# Eval("ThreadLanguage") %>">
                                </div>
                                &nbsp; <asp:Label ID="lblRejectedDash" Visible="False" runat="server" Text="-"></asp:Label>&nbsp;<span
                                    style="color: #333;"><b><i><asp:Label ID="lblRejected" resourcekey="rejected" runat="server"
                                        Visible="false" Text=""></asp:Label></i></b></span><asp:Label ID="lblRejectReasonId"
                                            Visible="false" runat="server" Text='<%# Eval("rejectReasonId") %>'></asp:Label><asp:Label
                                                ID="UsernameLabel" Visible="false" runat="server" Text='<%# Eval("Username") %>' />
                            </div>
                            <div>
                                <asp:Label ID="lbl_Body" runat="server" Text='<%# Eval("Body") %>' Visible="false" />
                                <asp:HyperLink ID="hprlnk_post" Visible="false" runat="server">View complete proposal &raquo;
                                </asp:HyperLink><asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' /><asp:Label
                                    ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' /></div>
                        </td>
                        <td class="overview-item-wrap alt-item">
                            <asp:Label ID="lblUserId" Visible="false" runat="server" Text='<%# Eval("UserID") %>' />
                            <asp:Label ID="lblPhaseId" runat="server" Visible="false" Text='<%# Eval("phaseId") %>' />
                            <div class="thread-phases-container">
                                <table cellspacing="0" cellpadding="0" border="0" align="center" class="phases-arrow-wrap">
                                    <tbody>
                                        <tr id="">
                                            <td>
                                                <div runat="server" id="phase1" class="">
                                                    <asp:HyperLink ID="hprlnk_Phase1" runat="server">
                                                        <asp:Label ID="lblPhase1" runat="server" resourcekey="Suggest" Text="Suggest"></asp:Label></asp:HyperLink></div></td><td>
                                                <div runat="server" id="phase2" class="">
                                                    <asp:HyperLink ID="hprlnk_Phase2" runat="server">
                                                        <asp:Label ID="lblPhase2" runat="server" resourcekey="Join" Text="Join"></asp:Label></asp:HyperLink></div></td><td>
                                                <div runat="server" id="phase3" class="">
                                                    <asp:HyperLink ID="hprlnk_Phase3" runat="server">
                                                        <asp:Label ID="lblPhase3" runat="server" resourcekey="Vote" Text="Vote"></asp:Label></asp:HyperLink></div></td><td>
                                                <div runat="server" id="phase4" class="">
                                                    <asp:HyperLink ID="hprlnk_Phase4" runat="server">
                                                        <asp:Label ID="lblPhase4" runat="server" resourcekey="Results" Text="Results"></asp:Label></asp:HyperLink></div></td></tr></tbody></table></div></td></tr></AlternatingItemTemplate><LayoutTemplate>
                    <table id="Table6" runat="server" style="float: left; clear: left; margin-top: 13px;
                        width: 100%">
                        <tr id="Tr5" runat="server">
                            <td id="Td5" runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="0" style="width: 100%">
                                    <tr>
                                        <td class="title-bar topic-padded">
                                            <asp:Label ID="Label8" resourcekey="Topic" runat="server"></asp:Label></td><td class="title-bar">
                                            <div class="header-separator">
                                                <asp:LinkButton runat="server" ID="SortByName" CommandName="Sort" CssClass="AjaxLoadingTrigger"
                                                    CommandArgument="phaseId">
                                                    <asp:Label ID="Label9" resourcekey="Status" runat="server"></asp:Label></asp:LinkButton></div></td></tr><tr>
                                        <td>
                                        </td>
                                        <td>
                                            <div class="phase-icon-wrap">
                                                <div class="phase4-icon phase-icon" title="Results">
                                                </div>
                                                <div class="phase3-icon phase-icon" title="Vote">
                                                </div>
                                                <div class="phase2-icon phase-icon" title="Join">
                                                </div>
                                                <div class="phase1-icon phase-icon" title="Suggest">
                                                </div>
                                                <div class="cleared">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="Tr1" runat="server">
                            <td id="Td1" runat="server" style="" class="pager-wrapper">
                                <asp:DataPager ID="DataPager1" runat="server" PageSize="10">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Link" PreviousPageText="&laquo;" ButtonCssClass="pager-link"
                                            ShowNextPageButton="False" ShowPreviousPageButton="True" />
                                        <asp:NumericPagerField NumericButtonCssClass="pager-link" CurrentPageLabelCssClass="pager-link-inactive" />
                                        <asp:NextPreviousPagerField ButtonType="Link" NextPageText="&raquo;" ButtonCssClass="pager-link"
                                            ShowNextPageButton="True" ShowPreviousPageButton="False" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
            <asp:HiddenField ID="hdnfldLanguage" runat="server" />
            <asp:SqlDataSource ID="sqldtsrc_ActiveDiscussions" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
                SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Users.FirstName, Users.LastName, Forum_Groups.ModuleID, Ourspace_Forum_Thread_Info.phaseId, Ourspace_Forum_Thread_Info.ThreadLanguage, Forum_Forums.Name, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.rejectReasonId FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Posts.ParentPostID = 0) AND (Forum_Groups.ModuleID = 381 OR Forum_Groups.ModuleID = 415) AND (Ourspace_Forum_Thread_Info.ThreadLanguage &lt;&gt; 'en-EU') ORDER BY Forum_Posts.CreatedDate DESC">
            </asp:SqlDataSource>

             <asp:SqlDataSource ID="sqldtsrc_FeaturedDiscussions" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
                SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Users.FirstName, Users.LastName, Forum_Groups.ModuleID, Ourspace_Forum_Thread_Info.phaseId, Ourspace_Forum_Thread_Info.ThreadLanguage, Forum_Forums.Name, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.rejectReasonId FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Posts.ParentPostID = 0) AND (Forum_Groups.ModuleID = 381 OR Forum_Groups.ModuleID = 415) AND (Ourspace_Forum_Thread_Info.isFeatured = 1) ORDER BY Forum_Posts.CreatedDate DESC">
            </asp:SqlDataSource>
            <br />
            <asp:SqlDataSource ID="sqldtsrc_ActiveDiscussionsPerLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
                SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Users.FirstName, Users.LastName, Forum_Groups.ModuleID, Ourspace_Forum_Thread_Info.phaseId, Ourspace_Forum_Thread_Info.ThreadLanguage, Forum_Forums.Name, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.rejectReasonId FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Posts.ParentPostID = 0) AND (Forum_Groups.ModuleID = 381 OR Forum_Groups.ModuleID = 415) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = @language) ORDER BY Forum_Posts.CreatedDate DESC">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hdnfldLanguage" DefaultValue="-1" Name="language"
                        PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>

</asp:Panel>
  <%--  </ContentTemplate>
</asp:UpdatePanel>--%>
<div class="cleared">
</div>
<asp:Panel ID="pnlAdmin" Visible="false" runat="server">
    <asp:Panel runat="server" ID="adminSuccessPanel" Visible="false">
        <div class="ok-div" style="margin-top: 15px;">
            <div class="ok-icon">
                <asp:Label ID="lblOkMessage" runat="server" Text=""></asp:Label></div></div></asp:Panel><asp:ListView ID="lstvw_reportedPosts" runat="server" DataSourceID="sqldtrsrc_ReportedPosts"
        EnableModelValidation="True" OnItemDataBound="lstvw_reportedPosts_ItemDataBound"
        OnItemCommand="lstvw_reportedPosts_ItemCommand">
        <EmptyDataTemplate>
            <table id="Table1" runat="server" style="">
                <tr>
                    <td>
                        <div class="info-div" style="margin-top: 15px;">
                            <div class="info-icon">
                                <asp:Label ID="lblNoReports" runat="server" Text="There are no reported posts that require attention."></asp:Label></div></div></td></tr></table></EmptyDataTemplate><ItemTemplate>
            <tr>
                <td class="overview-item-wrap">
                    <p class="gray-text">
                        <asp:HyperLink ID="hprlnk_GoToProfile" runat="server">
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("LastName") %>' /></asp:HyperLink>- <asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' />
                    </p>
                    <div style="">
                        <asp:Label ID="BodyLabel" runat="server" Text='<%# Eval("Body") %>' />
                    </div>
                    <p>
                        <b>
                            <asp:Label ID="Label13" runat="server" resourcekey="Reason"></asp:Label></b><asp:Label
                                ID="ReasonLabel" runat="server" Text='<%# Eval("Reason") %>' /></p>
                </td>
                <td class="postActions overview-item-wrap">
                    <p>
                        <asp:HyperLink ID="hprlnk_GoToPost" resourcekey="GoToPost" Target="_blank" runat="server"></asp:HyperLink></p><p>
                        <asp:LinkButton ID="lnkbtn_AcknowledgeReport" CommandArgument='<%# Eval("PostID") %>'
                            CommandName="RemoveFromList" runat="server" resourcekey="MarkAsSolved"></asp:LinkButton><%--<asp:CheckBox ID="AddressedCheckBox" runat="server" Checked='<%# Eval("Addressed") %>'
                    Enabled="false" />--%></p><p>
                        <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("PostID") %>' CommandName="RequestRemoval"
                            runat="server" resourcekey="RequestPostRemoval"></asp:LinkButton></p><asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("UserID") %>' />
                    <asp:Label Visible="false" ID="ForumIDLabel" runat="server" Text='<%# Eval("ForumID") %>' />
                    <asp:Label Visible="false" ID="PostIDLabel" runat="server" Text='<%# Eval("PostID") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td class="overview-item-wrap alt-item">
                    <p class="gray-text">
                        <asp:HyperLink ID="hprlnk_GoToProfile" runat="server">
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("LastName") %>' /></asp:HyperLink>- <asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' />
                    </p>
                    <div style="">
                        <asp:Label ID="BodyLabel" runat="server" Text='<%# Eval("Body") %>' />
                    </div>
                    <p>
                        <b>
                            <asp:Label ID="Label4" runat="server" resourcekey="Reason" Text="Label"></asp:Label>:</b> <asp:Label ID="ReasonLabel" runat="server" Text='<%# Eval("Reason") %>' /></p>
                </td>
                <td class="postActions overview-item-wrap">
                    <p>
                        <asp:HyperLink ID="hprlnk_GoToPost" Target="_blank" resourcekey="GoToPost" runat="server">Go to post</asp:HyperLink></p><p>
                        <asp:LinkButton ID="lnkbtn_AcknowledgeReport" CommandArgument='<%# Eval("PostID") %>'
                            CommandName="RemoveFromList" resourcekey="MarkAsSolved" runat="server"></asp:LinkButton></p><p>
                        <asp:LinkButton ID="LinkButton1" resourcekey="RequestPostRemoval" CommandArgument='<%# Eval("PostID") %>'
                            CommandName="RequestRemoval" runat="server"></asp:LinkButton></p><asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("UserID") %>' />
                    <asp:Label Visible="false" ID="ForumIDLabel" runat="server" Text='<%# Eval("ForumID") %>' />
                    <asp:Label Visible="false" ID="PostIDLabel" runat="server" Text='<%# Eval("PostID") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <LayoutTemplate>
            <table id="Table2" runat="server" cellpadding="10">
                <tr id="Tr2" runat="server">
                    <td id="Td2" runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" cellpadding="10" style="">
                            <tr id="Tr3" runat="server" style="">
                                <td id="Th4" runat="server" class="title-bar" colspan="2">
                                    <b>
                                        <asp:Label ID="Label5" runat="server" resourcekey="ReportedPosts" Text="Reported posts"></asp:Label></b></td></tr><tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
</asp:Panel>
<asp:Panel ID="pnlStatistics" runat="server" Visible="false">
    <div class="info-div">
        <div class="info-icon">
            <asp:Label ID="lblStatDesc" runat="server" resourcekey="RangeDesc" Text="Choose different date ranges to see platform activities over different periods of time."></asp:Label><div
                style="margin-bottom: 8px;">
            </div>
            <b>
                <asp:Label ID="lblFrom" runat="server" resourcekey="From" Text="From"></asp:Label>:</b>&nbsp;&nbsp; <asp:DropDownList ID="ddlFromDay" runat="server">
                <asp:ListItem Text="1" Value="1" />
                <asp:ListItem Text="2" Value="2" />
                <asp:ListItem Text="3" Value="3" />
                <asp:ListItem Text="4" Value="4" />
                <asp:ListItem Text="5" Value="5" />
                <asp:ListItem Text="6" Value="6" />
                <asp:ListItem Text="7" Value="7" />
                <asp:ListItem Text="8" Value="8" />
                <asp:ListItem Text="9" Value="9" />
                <asp:ListItem Text="10" Value="10" />
                <asp:ListItem Text="11" Value="11" />
                <asp:ListItem Text="12" Value="12" />
                <asp:ListItem Text="13" Value="13" />
                <asp:ListItem Text="14" Value="14" />
                <asp:ListItem Text="15" Value="15" />
                <asp:ListItem Text="16" Value="16" />
                <asp:ListItem Text="17" Value="17" />
                <asp:ListItem Text="18" Value="18" />
                <asp:ListItem Text="19" Value="19" />
                <asp:ListItem Text="20" Value="20" />
                <asp:ListItem Text="21" Value="21" />
                <asp:ListItem Text="22" Value="22" />
                <asp:ListItem Text="23" Value="23" />
                <asp:ListItem Text="24" Value="24" />
                <asp:ListItem Text="25" Value="25" />
                <asp:ListItem Text="26" Value="26" />
                <asp:ListItem Text="27" Value="27" />
                <asp:ListItem Text="28" Value="28" />
                <asp:ListItem Text="29" Value="29" />
                <asp:ListItem Text="30" Value="30" />
                <asp:ListItem Text="31" Value="31" />
            </asp:DropDownList>
            <asp:DropDownList ID="ddlFromMonth" runat="server">
                <asp:ListItem Text="1" Value="1" />
                <asp:ListItem Text="2" Value="2" />
                <asp:ListItem Text="3" Value="3" />
                <asp:ListItem Text="4" Value="4" />
                <asp:ListItem Text="5" Value="5" />
                <asp:ListItem Text="6" Value="6" />
                <asp:ListItem Text="7" Value="7" />
                <asp:ListItem Text="8" Value="8" />
                <asp:ListItem Text="9" Value="9" />
                <asp:ListItem Text="10" Value="10" />
                <asp:ListItem Text="11" Value="11" />
                <asp:ListItem Text="12" Value="12" />
            </asp:DropDownList>
            <asp:DropDownList ID="ddlFromYear" runat="server">
                <asp:ListItem Text="2010" Value="2010" />
                <asp:ListItem Text="2011" Value="2011" />
                <asp:ListItem Text="2012" Value="2012" />
                <asp:ListItem Text="2013" Value="2013" />
                <asp:ListItem Text="2014" Value="2014" />
                <asp:ListItem Text="2015" Value="2015" />
                <asp:ListItem Text="2016" Value="2016" />
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b><asp:Label ID="lblTo" runat="server" resourcekey="To" Text="To"></asp:Label></b>:&nbsp;<asp:DropDownList
                    ID="ddlToDay" runat="server">
                    <asp:ListItem Text="1" Value="1" />
                    <asp:ListItem Text="2" Value="2" />
                    <asp:ListItem Text="3" Value="3" />
                    <asp:ListItem Text="4" Value="4" />
                    <asp:ListItem Text="5" Value="5" />
                    <asp:ListItem Text="6" Value="6" />
                    <asp:ListItem Text="7" Value="7" />
                    <asp:ListItem Text="8" Value="8" />
                    <asp:ListItem Text="9" Value="9" />
                    <asp:ListItem Text="10" Value="10" />
                    <asp:ListItem Text="11" Value="11" />
                    <asp:ListItem Text="12" Value="12" />
                    <asp:ListItem Text="13" Value="13" />
                    <asp:ListItem Text="14" Value="14" />
                    <asp:ListItem Text="15" Value="15" />
                    <asp:ListItem Text="16" Value="16" />
                    <asp:ListItem Text="17" Value="17" />
                    <asp:ListItem Text="18" Value="18" />
                    <asp:ListItem Text="19" Value="19" />
                    <asp:ListItem Text="20" Value="20" />
                    <asp:ListItem Text="21" Value="21" />
                    <asp:ListItem Text="22" Value="22" />
                    <asp:ListItem Text="23" Value="23" />
                    <asp:ListItem Text="24" Value="24" />
                    <asp:ListItem Text="25" Value="25" />
                    <asp:ListItem Text="26" Value="26" />
                    <asp:ListItem Text="27" Value="27" />
                    <asp:ListItem Text="28" Value="28" />
                    <asp:ListItem Text="29" Value="29" />
                    <asp:ListItem Text="30" Value="30" />
                    <asp:ListItem Text="31" Value="31" />
                </asp:DropDownList>
            <asp:DropDownList ID="ddlToMonth" runat="server">
                <asp:ListItem Text="1" Value="1" />
                <asp:ListItem Text="2" Value="2" />
                <asp:ListItem Text="3" Value="3" />
                <asp:ListItem Text="4" Value="4" />
                <asp:ListItem Text="5" Value="5" />
                <asp:ListItem Text="6" Value="6" />
                <asp:ListItem Text="7" Value="7" />
                <asp:ListItem Text="8" Value="8" />
                <asp:ListItem Text="9" Value="9" />
                <asp:ListItem Text="10" Value="10" />
                <asp:ListItem Text="11" Value="11" />
                <asp:ListItem Text="12" Value="12" />
            </asp:DropDownList>
            <asp:DropDownList ID="ddlToYear" runat="server">
                <asp:ListItem Text="2010" Value="2010" />
                <asp:ListItem Text="2011" Value="2011" />
                <asp:ListItem Text="2012" Value="2012" />
                <asp:ListItem Text="2013" Value="2013" />
                <asp:ListItem Text="2014" Value="2014" />
                <asp:ListItem Text="2015" Value="2015" />
                <asp:ListItem Text="2016" Value="2016" />
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" resourcekey="Refresh"
                Text="Refresh" ID="btnRefresh" OnClick="btnRefresh_Click" />
            <asp:Label ID="lblInvalidFrom" Visible="false" resourcekey="InvalidFromDate" Text="Invalid from date"
                runat="server"></asp:Label>&nbsp;&nbsp; <asp:Label ID="lblInvalidTo" Visible="false" resourcekey="InvalidToDate" Text="Invalid to date"
                runat="server"></asp:Label></div></div><div class="cleared">
    </div>
    <h1 style="margin-top: 19px;">
        <asp:Label ID="lblStatRange" runat="server" Text=""></asp:Label></h1><div class="statCategory statFirst">
        <span class="debate-title bold-link">
            <asp:Label ID="lblNewUsers" resourcekey="NewUsers" runat="server" Text=""></asp:Label></span><br /><br /><%--<asp:Label ID="lblUsersAll" runat="server" Text=""></asp:Label>--%><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagEU_sm.png" /><asp:Label
            ID="Label14" runat="server" Text="-"></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagGB_sm.png" /><asp:Label
            ID="lblUsersEnGb" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagGR_sm.png" /><asp:Label
            ID="lblUsersElGr" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagDE_sm.png" /><asp:Label
            ID="lblUsersDeAt" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagCZ_sm.png" /><asp:Label
            ID="lblUsersCsCz" runat="server" Text=""></asp:Label></div><div class="statCategory">
        <span class="debate-title bold-link">
            <asp:Label ID="lblNewThreads" resourcekey="NewThreads" runat="server" Text=""></asp:Label></span><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagEU_sm.png" /><asp:Label
            ID="lblThreadsEnEu" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagGB_sm.png" /><asp:Label
            ID="lblThreadsEnGb" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagGR_sm.png" /><asp:Label
            ID="lblThreadsElGr" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagDE_sm.png" /><asp:Label
            ID="lblThreadsDeAt" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagCZ_sm.png" /><asp:Label
            ID="lblThreadsCsCz" runat="server" Text=""></asp:Label></div><div class="statCategory">
        <span class="debate-title bold-link">
            <asp:Label ID="lblNewPosts" runat="server" resourcekey="NewPosts"></asp:Label></span><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagEU_sm.png" /><asp:Label
            ID="lblPostsEnEu" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagGB_sm.png" /><asp:Label
            ID="lblPostsEnGb" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagGR_sm.png" /><asp:Label
            ID="lblPostsElGr" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagDE_sm.png" /><asp:Label
            ID="lblPostsDeAt" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagCZ_sm.png" /><asp:Label
            ID="lblPostsCsCz" runat="server" Text=""></asp:Label></div><div class="statCategory">
        <span class="debate-title bold-link">
            <asp:Label ID="lblNewPostThumbs" resourcekey="NewPostThumbs" runat="server" Text=""></asp:Label></span><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagEU_sm.png" /><asp:Label
            ID="lblLikesEnEu" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagGB_sm.png" /><asp:Label
            ID="lblLikesEnGb" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagGR_sm.png" /><asp:Label
            ID="lblLikesElGr" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagDE_sm.png" /><asp:Label
            ID="lblLikesDeAt" runat="server" Text=""></asp:Label><br /><br /><img src="http://www.joinourspace.eu/Portals/_default/Skins/ourspace/images/flagCZ_sm.png" /><asp:Label
            ID="lblLikesCsCz" runat="server" Text=""></asp:Label></div><div class="cleared">
    </div>
</asp:Panel>
<asp:Panel ID="pnlUserDetails" runat="server" Visible="false">
    <asp:ListView ID="ListView1" runat="server" DataSourceID="sqldtsrc_AllUserData" EnableModelValidation="True">
  
     
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>
                        No users found. </td></tr></table></EmptyDataTemplate><ItemTemplate>
            <tr style="">
               
                <td class="overview-item-wrap">
                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
                </td>
                <td  class="overview-item-wrap">
                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
                </td>
                 <td  class="overview-item-wrap"  >
                    <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
        <tr style="">
               
                <td class="overview-item-wrap alt-item">
                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
                </td>
                <td  class="overview-item-wrap alt-item">
                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
                </td>
                 <td  class="overview-item-wrap alt-item"  >
                    <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr runat="server" style="" >
                                <td runat="server"  style="padding-right:15px;">
                                   <b>First Name</b> </td><td runat="server" style="padding-right:15px;">
                                    <b>Last Name</b> </td><td runat="server">
                                    <b>Email</b> </td></tr><tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="">
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="">
                <td>
                    <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
                </td>
                <td>
                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
                </td>
                <td>
                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
</asp:Panel>
<asp:SqlDataSource ID="sqldtrsrc_ReportedPosts" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Posts.UserID, Forum_Posts.Body, Forum_Post_Reported.CreatedDate, Forum_Post_Reported.Reason, Forum_Post_Reported.Addressed, Forum_Forums.ForumID, Forum_Posts.PostID, Users.FirstName, Users.LastName FROM Forum_Post_Reported INNER JOIN Forum_Posts ON Forum_Post_Reported.PostID = Forum_Posts.PostID INNER JOIN Forum_Threads ON Forum_Posts.ThreadID = Forum_Threads.ThreadID INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Users ON Forum_Post_Reported.UserID = Users.UserID WHERE (Forum_Post_Reported.Addressed = 'false' AND Forum_Posts.isApproved = 'true')">
</asp:SqlDataSource>
<!-- Statistics -->
<asp:SqlDataSource ID="sqldtsrc_UsersAll" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT COUNT(*) AS 'count' FROM Users INNER JOIN Ourspace_Forum_User_Info ON Users.UserID = Ourspace_Forum_User_Info.userId INNER JOIN UserProfile ON Users.UserID = UserProfile.UserID INNER JOIN ProfilePropertyDefinition ON UserProfile.PropertyDefinitionID = ProfilePropertyDefinition.PropertyDefinitionID WHERE (ProfilePropertyDefinition.PropertyDefinitionID = 40) AND (Users.CreatedOnDate BETWEEN @from AND @to )">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_UsersEnGb" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT COUNT (*) as 'count'
FROM         Users INNER JOIN
                      Ourspace_Forum_User_Info ON Users.UserID = Ourspace_Forum_User_Info.userId INNER JOIN
                      UserProfile ON Users.UserID = UserProfile.UserID INNER JOIN
                      ProfilePropertyDefinition ON UserProfile.PropertyDefinitionID = ProfilePropertyDefinition.PropertyDefinitionID
                      where ProfilePropertyDefinition.PropertyDefinitionID = 50 and PropertyValue = 'en-GB' AND (Users.CreatedOnDate BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_UsersElGr" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT COUNT (*) as 'count'
FROM         Users INNER JOIN
                      Ourspace_Forum_User_Info ON Users.UserID = Ourspace_Forum_User_Info.userId INNER JOIN
                      UserProfile ON Users.UserID = UserProfile.UserID INNER JOIN
                      ProfilePropertyDefinition ON UserProfile.PropertyDefinitionID = ProfilePropertyDefinition.PropertyDefinitionID
                      where ProfilePropertyDefinition.PropertyDefinitionID = 50 and PropertyValue = 'el-GR'  AND (Users.CreatedOnDate BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_UsersDeAt" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT COUNT (*) as 'count'
FROM         Users INNER JOIN
                      Ourspace_Forum_User_Info ON Users.UserID = Ourspace_Forum_User_Info.userId INNER JOIN
                      UserProfile ON Users.UserID = UserProfile.UserID INNER JOIN
                      ProfilePropertyDefinition ON UserProfile.PropertyDefinitionID = ProfilePropertyDefinition.PropertyDefinitionID
                      where ProfilePropertyDefinition.PropertyDefinitionID = 50 and PropertyValue = 'de-AT'  AND (Users.CreatedOnDate BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_UsersCsCz" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT COUNT (*) as 'count'
FROM         Users INNER JOIN
                      Ourspace_Forum_User_Info ON Users.UserID = Ourspace_Forum_User_Info.userId INNER JOIN
                      UserProfile ON Users.UserID = UserProfile.UserID INNER JOIN
                      ProfilePropertyDefinition ON UserProfile.PropertyDefinitionID = ProfilePropertyDefinition.PropertyDefinitionID
                      where ProfilePropertyDefinition.PropertyDefinitionID = 50 and PropertyValue = 'cs-CZ'  AND (Users.CreatedOnDate BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<!-- Threads -->
<asp:SqlDataSource ID="sqldtsrc_ThreadsAll" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT    COUNT(*) as 'count' FROM Forum_Threads INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Threads.PinnedDate BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ThreadsEnEu" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT    COUNT(*) as 'count' FROM Forum_Threads INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId where ThreadLanguage = 'en-EU' AND (Forum_Threads.PinnedDate BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ThreadsEnGb" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT     COUNT(*) AS 'count' FROM         Forum_Threads INNER JOIN  Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.PostID WHERE  (Ourspace_Forum_Thread_Info.ThreadLanguage = 'en-GB') AND (Forum_Posts.CreatedDate BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ThreadsElGr" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT     COUNT(*) AS 'count' FROM         Forum_Threads INNER JOIN  Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.PostID WHERE  (Ourspace_Forum_Thread_Info.ThreadLanguage = 'el-GR') AND (Forum_Posts.CreatedDate BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ThreadsDeAt" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT     COUNT(*) AS 'count' FROM         Forum_Threads INNER JOIN  Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.PostID WHERE  (Ourspace_Forum_Thread_Info.ThreadLanguage = 'de-AT') AND (Forum_Posts.CreatedDate BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ThreadsCsCz" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT     COUNT(*) AS 'count' FROM         Forum_Threads INNER JOIN  Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.PostID WHERE  (Ourspace_Forum_Thread_Info.ThreadLanguage = 'cs-CZ') AND (Forum_Posts.CreatedDate BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<!-- Posts (By Thread) -->
<asp:SqlDataSource ID="sqldtsrc_PostsAll" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT     COUNT(*) AS 'count' FROM  Forum_Threads INNER JOIN  Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID WHERE (Forum_Posts.CreatedDate  BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_PostsEnEu" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT     COUNT(*) AS 'count' FROM  Forum_Threads INNER JOIN  Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID WHERE     (Ourspace_Forum_Thread_Info.ThreadLanguage = 'en-EU') AND (Forum_Posts.CreatedDate  BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_PostsEnGb" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT     COUNT(*) AS 'count' FROM  Forum_Threads INNER JOIN  Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID WHERE     (Ourspace_Forum_Thread_Info.ThreadLanguage = 'en-GB')  AND (Forum_Posts.CreatedDate  BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_PostsElGr" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT     COUNT(*) AS 'count' FROM  Forum_Threads INNER JOIN  Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID WHERE     (Ourspace_Forum_Thread_Info.ThreadLanguage = 'el-GR')  AND (Forum_Posts.CreatedDate  BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_PostsDeAt" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT     COUNT(*) AS 'count' FROM  Forum_Threads INNER JOIN  Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID WHERE     (Ourspace_Forum_Thread_Info.ThreadLanguage = 'de-AT')  AND (Forum_Posts.CreatedDate  BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_PostsCsCz" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT     COUNT(*) AS 'count' FROM  Forum_Threads INNER JOIN  Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID WHERE     (Ourspace_Forum_Thread_Info.ThreadLanguage = 'cs-CZ')  AND (Forum_Posts.CreatedDate  BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<!--Post Likes (Thumbs Up/Thumbs Down)-->
<asp:SqlDataSource ID="sqldtsrc_LikesEnEu" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand=" SELECT    COUNT( *) as 'count' FROM Forum_Threads INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN   Ourspace_Forum_Post_Thumbs_Log ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs_Log.PostID WHERE     (Ourspace_Forum_Thread_Info.ThreadLanguage = 'en-EU') AND (Ourspace_Forum_Post_Thumbs_Log.Date BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_LikesEnGb" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand=" SELECT    COUNT( *) as 'count' FROM Forum_Threads INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN   Ourspace_Forum_Post_Thumbs_Log ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs_Log.PostID WHERE     (Ourspace_Forum_Thread_Info.ThreadLanguage = 'en-GB') AND (Ourspace_Forum_Post_Thumbs_Log.Date BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_LikesElGr" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand=" SELECT    COUNT( *) as 'count' FROM Forum_Threads INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN   Ourspace_Forum_Post_Thumbs_Log ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs_Log.PostID WHERE     (Ourspace_Forum_Thread_Info.ThreadLanguage = 'el-GR') AND (Ourspace_Forum_Post_Thumbs_Log.Date BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_LikesDeAt" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand=" SELECT    COUNT( *) as 'count' FROM Forum_Threads INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN   Ourspace_Forum_Post_Thumbs_Log ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs_Log.PostID WHERE     (Ourspace_Forum_Thread_Info.ThreadLanguage = 'de-AT') AND (Ourspace_Forum_Post_Thumbs_Log.Date BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_LikesCsCz" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand=" SELECT    COUNT( *) as 'count' FROM Forum_Threads INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN   Ourspace_Forum_Post_Thumbs_Log ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs_Log.PostID WHERE     (Ourspace_Forum_Thread_Info.ThreadLanguage = 'cs-CZ') AND (Ourspace_Forum_Post_Thumbs_Log.Date BETWEEN @from AND @to)">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="2009-01-01" Name="from" SessionField="statsFrom" />
        <asp:SessionParameter DefaultValue="2019-01-01" Name="to" SessionField="statsTo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_AllUserData" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    SelectCommand="SELECT TOP (1000) Users.Email, Users.FirstName, Users.LastName FROM Users INNER JOIN Ourspace_Forum_User_Info ON Users.UserID = Ourspace_Forum_User_Info.userId INNER JOIN UserProfile ON Users.UserID = UserProfile.UserID INNER JOIN ProfilePropertyDefinition ON UserProfile.PropertyDefinitionID = ProfilePropertyDefinition.PropertyDefinitionID WHERE (ProfilePropertyDefinition.PropertyDefinitionID = 50) AND (UserProfile.PropertyValue = @country) AND (Users.IsDeleted ='false')"><SelectParameters><asp:SessionParameter 
            DefaultValue="en-GB" Name="country" SessionField="currentLanguage" /></SelectParameters></asp:SqlDataSource>
<asp:LinkButton 
    ID="lnkbtn_TestLocation" runat="server" 
    onclick="lnkbtn_CopyLanguageToNationality_Click" Visible="False">Copy language to nationality for new users </asp:LinkButton><asp:LinkButton 
    ID="lnkbtn_SetNationalityFromIP" runat="server" 
    onclick="lnkbtn_SetNationalityFromIP_Click" Visible="False" style="display: none">SetNationalityFromIP</asp:LinkButton><%--<asp:Label ID="lblUsersAll" runat="server" Text=""></asp:Label>--%><asp:Label ID="lblCountry" 
    runat="server" Text="Label" Visible="False"></asp:Label><asp:TextBox 
    ID="txtUserIdToGreek" runat="server" Visible="false"></asp:TextBox><asp:LinkButton 
    ID="lnkbtn_switchToGreek" runat="server" Visible="false" onclick="lnkbtn_switchToGreek_Click">Switch to greek</asp:LinkButton>