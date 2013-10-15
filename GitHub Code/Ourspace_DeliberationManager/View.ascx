<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_DeliberationManager.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<%--
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>

<div class="info-div">
    <div class="info-icon">
        <div>
            <asp:Label ID="lblInfo" runat="server" resourcekey="FindBelow"></asp:Label></div>
        <div class="cleared">
            &#160;</div>
        <asp:HyperLink ID="hprlnk_ProposeTopic" CssClass="action-button fleft" Style="margin-top: 5px;"
            runat="server" resourcekey="SuggestTopicDebate" Text="Suggest NEW Topic-Debate"></asp:HyperLink>
        <div class="cleared">
            &#160;</div>
    </div>
</div>
<div class="info-div" style="margin-top: 15px;">
    <div class="info-icon">
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
<asp:Panel ID="pnl_admin" runat="server" Visible="false">
    <p>
        <asp:LinkButton CssClass="Ourspace_ToolbarLink" Visible="false" ID="lnkbtn_ManageDeliberations"
            runat="server" OnClick="lnkbtn_ManageDeliberations_Click">Manage Discussion Proposals</asp:LinkButton>
        &nbsp;
        <asp:LinkButton CssClass="Ourspace_ToolbarLink" Visible="false" ID="lnkbtn_ManageDiscussionSolutions"
            runat="server" OnClick="lnkbtn_ManageDiscussionSolutions_Click">Manage Discussion Solutions</asp:LinkButton>
    </p>
</asp:Panel>
<div class="cleared">
</div>
<asp:Panel ID="pnlThankYou" Visible="false" runat="server" CssClass="info-div" Style="margin-top: 20px;">
    <div class="info-icon">
        <asp:Label ID="lblThankyou" resourcekey="thankyou" runat="server"></asp:Label>
        <asp:Label ID="Label1"  runat="server"></asp:Label>
        <div class="cleared"></div><br />
        <b><asp:HyperLink ID="hprlnkThankyou" Visible="false" resourcekey="thankyouLink" runat="server"></asp:HyperLink>
    </b></div>
</asp:Panel>
<h2 class="fleft">
    <asp:Label ID="lblCount" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lblTitle" resourcekey="SuggestedTopic-Debates" runat="server" Text="Suggested Topic-Debates"></asp:Label></h2>
<div class="sortingDropdown">
    <div class="debateSortLoading hidden">
    </div>
    <b>
        <asp:Label ID="lblSortBy" runat="server" resourcekey="SortBy" Text="Sort by"></asp:Label>:</b>
    <asp:DropDownList ID="ddlSortDebates" CssClass="ddlSortDebates" runat="server" AutoPostBack="true"
        OnSelectedIndexChanged="ddlSortDebates_SelectedIndexChanged">
        
        <asp:ListItem Value="Date" resourcekey="Date" />
        <asp:ListItem Value="Popularity" resourcekey="Popularity" />
        <asp:ListItem Value="Title" resourcekey="Title" />
    </asp:DropDownList>
</div>
<div class="cleared">
</div>
<%--<dnnweb:DnnComboBox ID="ddlSortDebates" runat="server" OnSelectedIndexChanged="ddlSortDebates_SelectedIndexChanged">
        <
        <Items>
        
         <dnnweb:RadComboBoxItem Text="Date" Value="Date" />
          <dnnweb:RadComboBoxItem Text="Title" Value="Title" />
        </Items>
        </dnnweb:DnnComboBox>--%>
<div class="proposals-wrapper">
    <asp:ListView ID="lstvw_DebateProposals" runat="server" DataSourceID="sqldtsrc_DebateProposalsOwnLangByDate"
        EnableModelValidation="True" OnItemCommand="lstvw_DebateProposals_ItemCommand"
        OnItemDataBound="lstvw_DebateProposals_ItemDataBound" OnDataBound="lstvw_DebateProposals_DataBound">
        <EmptyDataTemplate>
            <table id="Table5" runat="server" style="">
                <tr>
                    <td>
                        <div class="info-div" style="margin-top: 15px;">
                            <div class="info-icon">
                                <asp:Label ID="Label4" runat="server" resourcekey="NoResults" Text=""></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr>
                <td runat="server" id="imageTd" class="debate-thumbnail reply-item">
                    <asp:Literal ID="ltrlImage" runat="server"></asp:Literal>
                </td>
                <td runat="server" id="textTd" class="reply-item">
                    <div>
                        <asp:HyperLink CssClass="bold-link debate-title" NavigateUrl="" ID="hprlnk_subject" runat="server">
                            <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></asp:HyperLink><a
                                href="#" class="bold-link debate-title"></a></div><div style="margin: 3px 0 3px 0; color: #999;">
                        <asp:Label ID="lbl_by" runat="server" resourcekey="By" Text="By"></asp:Label>&nbsp;<asp:HyperLink
                            ID="hprlnk_userProfile" runat="server">
                            <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;<asp:Label
                                ID="lbl_LastName" runat="server" Text='<%# Eval("LastName") %>' />
                        </asp:HyperLink>&nbsp;- <asp:Label ID="lbl_date" runat="server" resourcekey="On" Text="On"></asp:Label>&nbsp;<asp:Label
                            ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' />
                        - <asp:Label ID="lbl_Views" runat="server" resourcekey="Views" Text="Views"></asp:Label>: <asp:Label ID="ViewsLabel" runat="server" Text='<%# Eval("Views") %>' />
                        - <asp:Label ID="lbl_Votes" runat="server" resourcekey="Votes" Text="Votes"></asp:Label>: <asp:LinkButton ID="LinkButton3" CssClass="ThumbsUpButton-display" CommandName="RateThreadUp"
                            CommandArgument='<%# Eval("ThreadID") %>' Style="border: none;" runat="server">
                            <asp:Label ID="lbl_ThumbsUp" runat="server" Text='<%# Eval("ThumbsUp") %>'></asp:Label></asp:LinkButton><asp:LinkButton
                                ID="LinkButton4" CssClass="ThumbsDownButton-display" CommandName="RateThreadDown"
                                CommandArgument='<%# Eval("ThreadID") %>' Style="border: none;" runat="server">
                                <asp:Label ID="lbl_ThumbsDown" runat="server" Text='<%# Eval("ThumbsDown") %>'></asp:Label></asp:LinkButton>&nbsp; <asp:Label ID="lblRejectedDash" runat="server" Text="-"></asp:Label><span style="color:#333;"><b><i><asp:Label ID="lblRejected" resourcekey="rejected" runat="server" Visible="false" Text=""></asp:Label></i></b></span><asp:Label ID="lblRejectReasonId" Visible="false" runat="server" Text='<%# Eval("rejectReasonId") %>'></asp:Label><asp:Label
                                    ID="UsernameLabel" Visible="false" runat="server" Text='<%# Eval("Username") %>' />
                        <div class="deliberation-small-flag small-flag-<%# Eval("ThreadLanguage") %>">
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lbl_Body" runat="server" Text='<%# Eval("Body") %>' />
                        <asp:Label ID="lbl_FullBody" Visible="false" runat="server" Text="" />
                        <asp:HyperLink ID="hprlnk_post" Style="white-space: nowrap;" runat="server">
                            <asp:Label ID="Label2" runat="server" resourcekey="CompleteProposalView" Text="Label"></asp:Label>
                            &raquo;
                        </asp:HyperLink></div><div>
<asp:Label ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' />
                        <asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' />
                        <asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("UserId") %>' /></div>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td runat="server" id="imageTd" class="debate-thumbnail reply-item alt-item">
                    <asp:Literal ID="ltrlImage" runat="server"></asp:Literal></td><td runat="server" id="textTd" class="reply-item alt-item">
                    <div>
                        <asp:HyperLink CssClass="bold-link debate-title" ID="hprlnk_subject" runat="server">
                            <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></asp:HyperLink><a
                                href="#" class="bold-link debate-title"></a></div><div style="margin: 3px 0 3px 0; color: #999;">
                        <asp:Label ID="lbl_by" runat="server" resourcekey="By" Text="By"></asp:Label>&nbsp;<asp:HyperLink
                            ID="hprlnk_userProfile" runat="server">
                            <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;<asp:Label
                                ID="lbl_LastName" runat="server" Text='<%# Eval("LastName") %>' />
                        </asp:HyperLink>&nbsp;- <asp:Label ID="lbl_date" runat="server" resourcekey="On" Text="On"></asp:Label>&nbsp;<asp:Label
                            ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' />
                        - <asp:Label ID="lbl_Views" runat="server" resourcekey="Views" Text="Views"></asp:Label>: <asp:Label ID="ViewsLabel" runat="server" Text='<%# Eval("Views") %>' />
                        - <asp:Label ID="lbl_Votes" resourcekey="Votes" runat="server" Text="Votes"></asp:Label>: <asp:LinkButton ID="LinkButton3" CssClass="ThumbsUpButton-display" CommandName="RateThreadUp"
                            CommandArgument='<%# Eval("ThreadID") %>' Style="border: none;" runat="server">
                            <asp:Label ID="lbl_ThumbsUp" runat="server" Text='<%# Eval("ThumbsUp") %>'></asp:Label></asp:LinkButton><asp:LinkButton
                                ID="LinkButton4" CssClass="ThumbsDownButton-display" CommandName="RateThreadDown"
                                CommandArgument='<%# Eval("ThreadID") %>' Style="border: none;" runat="server">
                                <asp:Label ID="lbl_ThumbsDown" runat="server" Text='<%# Eval("ThumbsDown") %>'></asp:Label></asp:LinkButton>&nbsp; <asp:Label ID="lblRejectedDash" runat="server" Text="-"></asp:Label><span style="color:#333;"><b><i><asp:Label ID="lblRejected" resourcekey="rejected" runat="server" Visible="false" Text=""></asp:Label></i></b></span><asp:Label ID="lblRejectReasonId" Visible="false" runat="server" Text='<%# Eval("rejectReasonId") %>'></asp:Label><asp:Label
                                    ID="UsernameLabel" Visible="false" runat="server" Text='<%# Eval("Username") %>' />
                        <div class="deliberation-small-flag small-flag-<%# Eval("ThreadLanguage") %>">
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lbl_Body" runat="server" Text='<%# Eval("Body") %>' />
                        <asp:Label ID="lbl_FullBody" Visible="false" runat="server" Text="" />
                        <asp:HyperLink ID="hprlnk_post" Style="white-space: nowrap;" runat="server">
                            <asp:Label ID="Label2" runat="server" resourcekey="CompleteProposalView" Text="Label"></asp:Label>
                            &raquo;
                        </asp:HyperLink></div><div>
<asp:Label  ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' />
                        <asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' /></div>
                    <asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("UserId") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <LayoutTemplate>
            <table runat="server" style="float: left; clear: left;">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" style="width: 789px;">
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="Tr1" runat="server">
                    <td id="Td1" runat="server" style="" class="pager-wrapper">
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="10">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="pager-link" ShowNextPageButton="False"
                                    ShowPreviousPageButton="True" PreviousPageText="&laquo;" />
                                <asp:NumericPagerField NumericButtonCssClass="pager-link" CurrentPageLabelCssClass="pager-link-inactive" />
                                <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="pager-link" ShowNextPageButton="True"
                                    ShowPreviousPageButton="False" NextPageText="&raquo;" />
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
<asp:ListView ID="lstvw_Solutions" Visible="False" runat="server" DataKeyNames="SolutionID"
    DataSourceID="sqldtsrc_Solutions" EnableModelValidation="True" InsertItemPosition="LastItem"
    OnDataBound="lstvw_Solutions_DataBound">
    <EditItemTemplate>
        <tr style="">
            <td>
                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" ValidationGroup="solutionsEditTemplate" /><asp:Button
                    ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
            </td>
            <td>
                <asp:TextBox ID="PositionTextBox" Style="width: 30px;" runat="server" Text='<%# Bind("Position") %>' />
            </td>
            <td>
                <asp:TextBox ID="TextTextBox" Style="width: 475px; height: 65px;" runat="server"
                    Text='<%# Bind("Text") %>' TextMode="MultiLine" />
            </td>
            <td>
                <asp:TextBox ID="ThumbsUpTextBox" Style="width: 30px;" runat="server" Text='<%# Bind("ThumbsUp") %>' />
            </td>
            <td>
                <asp:TextBox ID="ThumbsDownTextBox" Style="width: 30px;" runat="server" Text='<%# Bind("ThumbsDown") %>' />
            </td>
            <td>
                <asp:Label ID="SolutionIDLabel1" runat="server" Visible="False" Text='<%# Eval("SolutionID") %>' />
            </td>
            <td>
                <asp:TextBox ID="ThreadIDTextBox" runat="server" Text='<%# Bind("ThreadID") %>' Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:RegularExpressionValidator ControlToValidate="PositionTextBox" ID="RegularExpressionValidator2"
                    ValidationGroup="solutionsEditTemplate" runat="server" ErrorMessage="Order must be a number"
                    ValidationExpression="^\d+$" EnableClientScript="False"></asp:RegularExpressionValidator><asp:RegularExpressionValidator
                        ControlToValidate="TextTextBox" ID="RegularExpressionValidator3" ValidationGroup="solutionsEditTemplate"
                        runat="server" ErrorMessage="Text can't be more than 1000 characters." ValidationExpression=".{0,1000}"
                        EnableClientScript="False"></asp:RegularExpressionValidator></td></tr></EditItemTemplate><EmptyDataTemplate>
        <table runat="server" style="">
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <InsertItemTemplate>
        <tr style="">
            <td>
                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" ValidationGroup="solutionsInsertTemplate" /><asp:Button
                    ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
            </td>
            <td>
                <asp:TextBox Style="width: 30px;" ID="PositionTextBox" runat="server" Text='<%# Bind("Position") %>'
                    Rows="3" />
            </td>
            <td>
                <asp:TextBox ID="TextTextBox" Style="width: 475px; height: 65px;" runat="server"
                    Text='<%# Bind("Text") %>' TextMode="MultiLine" />
            </td>
            <td>
                <asp:TextBox Style="width: 30px;" ID="ThumbsUpTextBox" runat="server" Text='<%# Bind("ThumbsUp") %>'
                    ReadOnly="True" />
                <asp:RegularExpressionValidator ControlToValidate="ThumbsUpTextBox" ID="RegularExpressionValidator1"
                    runat="server" ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d+$"
                    EnableClientScript="False"></asp:RegularExpressionValidator><%-- <asp:TextBox style="width:30px;" ID="TextBox1" runat="server" Text='<%# Bind("ThumbsUp") %>' /> --%></td><td>
                <asp:TextBox Style="width: 30px;" ID="ThumbsDownTextBox" runat="server" Text='<%# Bind("ThumbsDown") %>'
                    ReadOnly="True" />
            </td>
            <td>
                <%--<asp:TextBox ID="ThreadIDTextBox" runat="server" Text='<%# Bind("ThreadID") %>' />--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RegularExpressionValidator ControlToValidate="PositionTextBox" ID="RegularExpressionValidator2"
                    ValidationGroup="solutionsInsertTemplate" runat="server" ErrorMessage="Order must be a number"
                    ValidationExpression="^\d+$" EnableClientScript="False"></asp:RegularExpressionValidator><asp:RegularExpressionValidator
                        ControlToValidate="TextTextBox" ID="RegularExpressionValidator3" ValidationGroup="solutionsEditTemplate"
                        runat="server" ErrorMessage="Text can't be more than 1000 characters." ValidationExpression=".{0,1000}"
                        EnableClientScript="False"></asp:RegularExpressionValidator></td></tr></InsertItemTemplate><ItemTemplate>
        <tr style="">
            <td>
                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" /><asp:Button
                    ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
            </td>
            <td>
                <asp:Label ID="PositionLabel" runat="server" Text='<%# Eval("Position") %>' />
            </td>
            <td>
                <asp:Label ID="TextLabel" runat="server" Text='<%# Eval("Text") %>' />
            </td>
            <td>
                <asp:Label ID="ThumbsUpLabel" runat="server" Text='<%# Eval("ThumbsUp") %>' />
            </td>
            <td>
                <asp:Label ID="ThumbsDownLabel" runat="server" Text='<%# Eval("ThumbsDown") %>' />
            </td>
            <td>
                <%--<asp:Label ID="ThreadIDLabel" runat="server" Text='<%# Eval("ThreadID") %>' />, <asp:Label ID="SolutionIDLabel" runat="server" Text='<%# Eval("SolutionID") %>' />--%>
            </td>
            <td>
            </td>
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
        <asp:Label ID="lbl_NoSolutions" runat="server" Text="No data was returned." Visible="False"></asp:Label><table
            runat="server">
            <tr runat="server">
                <td runat="server">
                    <table class="solutions-table" id="itemPlaceholderContainer" runat="server" border="0"
                        style="">
                        <tr runat="server" style="">
                            <th id="Th7" runat="server">
                            </th>
                            <th id="Th5" runat="server">
                                Order </th><th runat="server">
                                Text </th><th runat="server">
                                Thumbs<br /> Up </th><th runat="server">
                                Thumbs<br /> Down </th><%-- %><th id="Th6" runat="server">
                                SolutionID </th>--%> <th id="Th8" runat="server">
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
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
                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" /><asp:Button
                    ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
            </td>
            <td>
                <asp:Label ID="SolutionIDLabel" runat="server" Text='<%# Eval("SolutionID") %>' />
            </td>
            <td>
                <asp:Label ID="ThreadIDLabel" runat="server" Text='<%# Eval("ThreadID") %>' />
            </td>
            <td>
                <asp:Label ID="TextLabel" runat="server" Text='<%# Eval("Text") %>' />
            </td>
            <td>
                <asp:Label ID="ThumbsUpLabel" runat="server" Text='<%# Eval("ThumbsUp") %>' />
            </td>
            <td>
                <asp:Label ID="ThumbsDownLabel" runat="server" Text='<%# Eval("ThumbsDown") %>' />
            </td>
            <td>
                <asp:Label ID="PositionLabel" runat="server" Text='<%# Eval("Position") %>' />
            </td>
        </tr>
    </SelectedItemTemplate>
</asp:ListView>
<asp:ListView ID="lstvw_ActiveDiscussions" runat="server" DataSourceID="sqldtsrc_ActiveDiscussions"
    EnableModelValidation="True" OnItemCommand="lstvw_ActiveDiscussions_ItemCommand"
    OnItemDataBound="lstvw_DebateProposals_ItemDataBound" DataKeyNames="ThreadID"
    Visible="False">
    <EmptyDataTemplate>
        <table runat="server" style="">
            <tr>
                <td>
                    No data was returned. </td></tr></table></EmptyDataTemplate><ItemTemplate>
        <tr style="">
            <td>
                <asp:Label ID="ViewsLabel" runat="server" Text='<%# Eval("Views") %>' />
            </td>
            <td>
                <asp:Label ID="UsernameLabel" runat="server" Text='<%# Eval("Username") %>' />
            </td>
            <td>
                <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' />
            </td>
            <td>
                <asp:HyperLink ID="hprlnk_post" runat="server">
                    <asp:Label ID="BodyLabel" runat="server" Text='<%# Eval("Body") %>' /></asp:HyperLink></td><td>
                <asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' /><asp:Label
                    ID="ThreadIDLabel" runat="server" Text='<%# Eval("ThreadID") %>' /><asp:Label ID="ForumIDLabel"
                        runat="server" Text='<%# Eval("ForumID") %>' />
            </td>
            <%--<td>
                <asp:Label ID="ThreadIDLabel" runat="server" Text='<%# Eval("ThreadID") %>' />
            </td>
            <td>
                <asp:Label ID="ForumIDLabel" runat="server" Text='<%# Eval("ForumID") %>' />
            </td>--%>
            <td>
                <asp:LinkButton CommandName="ManageSolutions" CommandArgument='<%# Eval("ThreadID") %>'
                    ID="lnkbtn_ManageSolutions" runat="server">Manage Solutions</asp:LinkButton></td><td>
                <%--<table style="width: 65px;">
                    <tr>
                        <td class="Forum_ThumbsCell">
                           <asp:LinkButton ID="LinkButton3" CssClass="ThumbsUpButton" CommandName="RateThreadUp"
                                CommandArgument='<%# Eval("ThreadID") %>' Style="border: none;" runat="server">
                               <asp:Label ID="lbl_ThumbsUp" runat="server" Text='<%# Eval("ThumbsUp") %>'></asp:Label></asp:LinkButton><asp:LinkButton
                                    ID="LinkButton4" CssClass="ThumbsDownButton" CommandName="RateThreadDown" CommandArgument='<%# Eval("ThreadID") %>'
                                    Style="border: none;" runat="server">
                                    <asp:Label ID="lbl_ThumbsDown" runat="server" Text='<%# Eval("ThumbsDown") %>'></asp:Label></asp:LinkButton></td><td>
                        </td>
                    </tr>
                </table>--%>
            </td>
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
        <table runat="server">
            <tr runat="server">
                <td runat="server">
                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                        <tr runat="server" style="">
                            <th runat="server">
                                Views </th><th runat="server">
                                Username </th><th runat="server">
                                Subject </th><th runat="server">
                                Body </th><th runat="server">
                                CreatedDate </th><%-- <th runat="server">
                                ThreadID</th>
                            <th runat="server">
                                ForumID</th>--%><th id="Th3" runat="server">
                                </th>
                            <th id="Th4" runat="server">
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
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
</asp:ListView>

<%-- <asp:TextBox style="width:30px;" ID="TextBox1" runat="server" Text='<%# Bind("ThumbsUp") %>' /> --%><asp:SqlDataSource
    ID="sqldtsrc_Solutions" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT * FROM [Ourspace_Proposal_Solutions] WHERE ([ThreadID] = @ThreadID)"
    ConflictDetection="OverwriteChanges" DeleteCommand="DELETE FROM [Ourspace_Proposal_Solutions] WHERE [SolutionID] = @original_SolutionID"
    InsertCommand="INSERT INTO [Ourspace_Proposal_Solutions] ([ThreadID], [Text], [ThumbsUp], [ThumbsDown], [Position]) VALUES (@ThreadID, @Text, @ThumbsUp, @ThumbsDown, @Position)"
    OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [Ourspace_Proposal_Solutions] SET [ThreadID] = @ThreadID, [Text] = @Text, [ThumbsUp] = @ThumbsUp, [ThumbsDown] = @ThumbsDown, [Position] = @Position WHERE [SolutionID] = @original_SolutionID"
    OnSelected="sqldtsrc_Solutions_Selected">
    <DeleteParameters>
        <asp:Parameter Name="original_SolutionID" Type="Int32" />
        <asp:Parameter Name="original_ThreadID" Type="Int32" />
        <asp:Parameter Name="original_Text" Type="String" />
        <asp:Parameter Name="original_ThumbsUp" Type="Int32" />
        <asp:Parameter Name="original_ThumbsDown" Type="Int32" />
        <asp:Parameter Name="original_Position" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <%-- <asp:Parameter Name="ThreadID" Type="Int32" />--%>
        <asp:ControlParameter ControlID="hdnfld_CurrentProposal" Name="ThreadID" PropertyName="Value"
            Type="Int32" />
        <asp:Parameter Name="Text" Type="String" />
        <asp:Parameter Name="ThumbsUp" Type="Int32" />
        <asp:Parameter Name="ThumbsDown" Type="Int32" />
        <asp:Parameter Name="Position" Type="Int32" />
    </InsertParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_CurrentProposal" Name="ThreadID" PropertyName="Value"
            Type="Int32" DefaultValue="0" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="ThreadID" Type="Int32" />
        <asp:Parameter Name="Text" Type="String" />
        <asp:Parameter Name="ThumbsUp" Type="Int32" />
        <asp:Parameter Name="ThumbsDown" Type="Int32" />
        <asp:Parameter Name="Position" Type="Int32" />
        <asp:Parameter Name="original_SolutionID" Type="Int32" />
        <asp:Parameter Name="original_ThreadID" Type="Int32" />
        <asp:Parameter Name="original_Text" Type="String" />
        <asp:Parameter Name="original_ThumbsUp" Type="Int32" />
        <asp:Parameter Name="original_ThumbsDown" Type="Int32" />
        <asp:Parameter Name="original_Position" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:HiddenField ID="hdnfld_CurrentProposal" runat="server" />
<asp:SqlDataSource ID="sqldtsrc_DebateProposalsCount" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT COUNT(*) AS count FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID LEFT OUTER JOIN Ourspace_Proposals_Thumbs ON Forum_Threads.ThreadID = Ourspace_Proposals_Thumbs.ThreadID WHERE (Forum_Groups.ModuleID = 415)">
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_DebateProposalsByPopularity" 
    runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Forum_Post_Thumbs.ThumbsUp, Ourspace_Forum_Post_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage, Ourspace_Forum_Thread_Info.rejectReasonId FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID WHERE (Forum_Groups.ModuleID = 415) ORDER BY Ourspace_Forum_Post_Thumbs.ThumbsUp DESC, Ourspace_Forum_Post_Thumbs.ThumbsDown"></asp:SqlDataSource>
<asp:SqlDataSource 
    ID="sqldtsrc_DebateProposalsByDate" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Forum_Post_Thumbs.ThumbsUp, Ourspace_Forum_Post_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage, Ourspace_Forum_Thread_Info.rejectReasonId FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID WHERE (Forum_Groups.ModuleID = 415) ORDER BY Forum_Posts.CreatedDate DESC, Ourspace_Forum_Post_Thumbs.ThumbsUp DESC, Ourspace_Forum_Post_Thumbs.ThumbsDown"></asp:SqlDataSource>
<asp:SqlDataSource 
    ID="sqldtsrc_DebateProposalsByTitle" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Forum_Post_Thumbs.ThumbsUp, Ourspace_Forum_Post_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage, Ourspace_Forum_Thread_Info.rejectReasonId FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID WHERE (Forum_Groups.ModuleID = 415) ORDER BY Forum_Posts.Subject, Ourspace_Forum_Post_Thumbs.ThumbsUp DESC, Ourspace_Forum_Post_Thumbs.ThumbsDown"></asp:SqlDataSource>
<asp:SqlDataSource 
    ID="sqldtsrc_DebateProposalsOwnLangByPopularity" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Forum_Post_Thumbs.ThumbsUp, Ourspace_Forum_Post_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage, Ourspace_Forum_Thread_Info.rejectReasonId FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID WHERE (Forum_Groups.ModuleID = 415) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = @lang) ORDER BY Ourspace_Forum_Post_Thumbs.ThumbsUp DESC, Ourspace_Forum_Post_Thumbs.ThumbsDown"><SelectParameters>
        <asp:SessionParameter DefaultValue="en-GB" Name="lang" SessionField="debateProposalsOwnLang" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource 
    ID="sqldtsrc_DebateProposalsOwnLangByDate" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Forum_Post_Thumbs.ThumbsUp, Ourspace_Forum_Post_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage, Ourspace_Forum_Thread_Info.rejectReasonId FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID WHERE (Forum_Groups.ModuleID = 415) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = @lang) ORDER BY Forum_Posts.CreatedDate DESC, Ourspace_Forum_Post_Thumbs.ThumbsUp DESC, Ourspace_Forum_Post_Thumbs.ThumbsDown"><SelectParameters>
        <asp:SessionParameter DefaultValue="en-GB" Name="lang" SessionField="debateProposalsOwnLang" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource 
    ID="sqldtsrc_DebateProposalsOwnLangByTitle" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Forum_Post_Thumbs.ThumbsUp, Ourspace_Forum_Post_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage, Ourspace_Forum_Thread_Info.rejectReasonId FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID WHERE (Forum_Groups.ModuleID = 415) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = @lang) ORDER BY Forum_Posts.Subject, Ourspace_Forum_Post_Thumbs.ThumbsUp DESC, Ourspace_Forum_Post_Thumbs.ThumbsDown"><SelectParameters>
        <asp:SessionParameter DefaultValue="en-GB" Name="lang" SessionField="debateProposalsOwnLang" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ActiveDiscussions" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID WHERE (Forum_Groups.ModuleID = 381) AND (Forum_Posts.ParentPostID = 0)">
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_DebateProposals_bk" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID WHERE (Forum_Groups.ModuleID = 415)">
</asp:SqlDataSource>

<%--<asp:TextBox ID="ThreadIDTextBox" runat="server" Text='<%# Bind("ThreadID") %>' />--%>
