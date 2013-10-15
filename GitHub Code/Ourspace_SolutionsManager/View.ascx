<%@ Control language="C#" Inherits="DotNetNuke.Modules.Ourspace_SolutionsManager.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
Solutions2!<br />
<br />
<br />
<br />


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
                        EnableClientScript="False"></asp:RegularExpressionValidator>
            </td>
        </tr>
    </EditItemTemplate>
    <EmptyDataTemplate>
        <table id="Table1" runat="server" style="">
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
                    EnableClientScript="False"></asp:RegularExpressionValidator><%-- <asp:TextBox style="width:30px;" ID="TextBox1" runat="server" Text='<%# Bind("ThumbsUp") %>' /> --%>
            </td>
            <td>
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
                        EnableClientScript="False"></asp:RegularExpressionValidator>
            </td>
        </tr>
    </InsertItemTemplate>
    <ItemTemplate>
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
        <asp:Label ID="lbl_NoSolutions" runat="server" Text="No data was returned." Visible="False"></asp:Label><table id="Table2"
            runat="server">
            <tr id="Tr1" runat="server">
                <td id="Td1" runat="server">
                    <table class="solutions-table" id="itemPlaceholderContainer" runat="server" border="0"
                        style="">
                        <tr id="Tr2" runat="server" style="">
                            <th id="Th7" runat="server">
                            </th>
                            <th id="Th5" runat="server">
                                Order
                            </th>
                            <th id="Th1" runat="server">
                                Text
                            </th>
                            <th id="Th2" runat="server">
                                Thumbs<br />
                                Up
                            </th>
                            <th id="Th3" runat="server">
                                Thumbs<br />
                                Down
                            </th>
                            <%-- %><th id="Th6" runat="server">
                                SolutionID </th>--%>
                            <th id="Th8" runat="server">
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="Tr3" runat="server">
                <td id="Td2" runat="server" style="">
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
<asp:ListView runat="server">
</asp:ListView>

<asp:ListView ID="lstvw_ActiveDiscussions" runat="server" DataSourceID="sqldtsrc_ActiveDiscussions"
    EnableModelValidation="True" OnItemCommand="lstvw_ActiveDiscussions_ItemCommand"
    OnItemDataBound="lstvw_DebateProposals_ItemDataBound" DataKeyNames="ThreadID"
    >
    <EmptyDataTemplate>
        <table id="Table3" runat="server" style="">
            <tr>
                <td>
                    No data was returned.
                </td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <%--<ItemTemplate>
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
                    <asp:Label ID="BodyLabel" runat="server" Text='<%# Eval("Body") %>' /></asp:HyperLink>
            </td>
            <td>
                <asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' /><asp:Label
                    ID="ThreadIDLabel" runat="server" Text='<%# Eval("ThreadID") %>' /><asp:Label ID="ForumIDLabel"
                        runat="server" Text='<%# Eval("ForumID") %>' />
            </td>
            <%--<td>
                <asp:Label ID="ThreadIDLabel" runat="server" Text='<%# Eval("ThreadID") %>' />
            </td>
            <td>
                <asp:Label ID="ForumIDLabel" runat="server" Text='<%# Eval("ForumID") %>' />
            </td>
            <td>
                <asp:LinkButton CommandName="ManageSolutions" CommandArgument='<%# Eval("ThreadID") %>'
                    ID="lnkbtn_ManageSolutions" runat="server">Manage Solutions</asp:LinkButton>
            </td>
            <td>
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
                </table>
            </td>
        </tr>
    </ItemTemplate>--%>
    <ItemTemplate>
        <tr style="">
            <td valign="top">
                <table style="width: 65px; margin: 12px 10px 0 0;">
                    <tr>
                        <td class="Forum_ThumbsCell">
                            <asp:LinkButton ID="LinkButton1" CssClass="ThumbsUpButton" CommandName="RateThreadUp"
                                CommandArgument='<%# Eval("ThreadID") %>' Style="border: none;" runat="server">
                                <asp:Label ID="lbl_ThumbsUp" runat="server" Text='<%# Eval("ThumbsUp") %>'></asp:Label></asp:LinkButton><asp:LinkButton
                                    ID="LinkButton2" CssClass="ThumbsDownButton" CommandName="RateThreadDown" CommandArgument='<%# Eval("ThreadID") %>'
                                    Style="border: none;" runat="server">
                                    <asp:Label ID="lbl_ThumbsDown" runat="server" Text='<%# Eval("ThumbsDown") %>'></asp:Label></asp:LinkButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:LinkButton ID="lnkbtn_ApproveThread" CommandName="ApproveThread" Cssclass="Ourspace_ToolbarLink" CommandArgument='<%# Eval("ThreadID") %>'
                     runat="server">Approve</asp:LinkButton>
            </td>
            <td>
                <h1>
                    <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></h1>
                <asp:Label ID="lbl_by" runat="server" Text="By "></asp:Label>
                <a href="#">
                    <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("FirstName") %>' />
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("LastName") %>' /></a>
                <asp:Label ID="lbl_date" runat="server" Text="on"></asp:Label> <asp:Label ID="CreatedDateLabel"
                    runat="server" Text='<%# Eval("CreatedDate") %>' />
                <asp:Label ID="UsernameLabel" Visible="false" runat="server" Text='<%# Eval("Username") %>' />
                <div style="padding:20px;">
                    <asp:Label ID="BodyLabel"  runat="server" Text='<%# Eval("Body") %>' />
                    
                    
                    </div>
                <div>
                    <asp:Label ID="lbl_Views" runat="server" Text="Views"></asp:Label>:
                    <b><asp:Label ID="ViewsLabel" runat="server" Text='<%# Eval("Views") %>' /></b></div>
                <div>
                    <div style="text-align:right; padding-right:20px;"><asp:HyperLink ID="hprlnk_post" runat="server">View complete proposal &raquo;
                    </asp:HyperLink></div></div>
                      <asp:Label ID="Label1" runat="server"  Visible="false" Text='<%# Eval("ThreadID") %>' />
                            <asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' />
                 <%--<asp:Label ID="ThreadIDLabel" runat="server" Text='<%# Eval("ThreadID") %>' />
                <asp:Label ID="ForumIDLabel" runat="server" Text='<%# Eval("ForumID") %>' />
               <td>
                <asp:Label ID="ThreadIDLabel" runat="server" Text='<%# Eval("ThreadID") %>' />
            </td>
            <td>
                <asp:Label ID="ForumIDLabel" runat="server" Text='<%# Eval("ForumID") %>' />
            </td>--%>
            </td>
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
        <table id="Table4" runat="server">
            <tr id="Tr4" runat="server">
                <td id="Td3" runat="server">
                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                        <tr id="Tr5" runat="server" style="">
                            <th id="Th4" runat="server">
                                Views
                            </th>
                            <th id="Th5" runat="server">
                                Username
                            </th>
                            <th id="Th6" runat="server">
                                Subject
                            </th>
                            <th id="Th7" runat="server">
                                Body
                            </th>
                            <th id="Th8" runat="server">
                                CreatedDate
                            </th>
                            <%-- <th runat="server">
                                ThreadID</th>
                            <th runat="server">
                                ForumID</th>--%><th id="Th9" runat="server">
                                </th>
                            <th id="Th10" runat="server">
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="Tr6" runat="server">
                <td id="Td4" runat="server" style="">
                </td>
            </tr>
        </table>
    </LayoutTemplate>
</asp:ListView>








<asp:SqlDataSource ID="sqldtsrc_Solutions" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
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
<asp:SqlDataSource ID="sqldtsrc_ActiveDiscussions" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID WHERE (Forum_Groups.ModuleID = 381) AND (Forum_Posts.ParentPostID = 0)">
</asp:SqlDataSource>
<asp:HiddenField ID="hdnfld_CurrentProposal" runat="server" />

