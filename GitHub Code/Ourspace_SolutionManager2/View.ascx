<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_SolutionManager2.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
    <div id="hidden-title">
        <asp:Label ID="Label3" runat="server" resourcekey="Vote"></asp:Label></div>
    <div class="info-div">
<div class="info-icon">
<div>
<asp:Label ID="lbl_Phase3Info" runat="server" resourcekey="Phase3Info"  ></asp:Label>
</div>
<div class="cleared"></div>
</div>
</div>
<div class="info-div" style="margin-top:15px;">
            <div class="info-icon">
               
                <asp:Panel ID="pnlNational" runat="server">
                    <asp:Panel ID="pnlViewingNational" runat="server"   >
                        <asp:Label ID="Label10" runat="server" resourcekey="ViewingNationalDebatesOwnLang" Text="You are currently viewing National Debates in your language. You can also"></asp:Label>
                        
                        <asp:LinkButton ID="lnkbtnViewAllLanguages" resourcekey="ViewNatDebAllLangLink" CssClass="bold-link" runat="server" OnClick="lnkbtnViewAllLanguages_Click" Text="click here to view debates in all languages"></asp:LinkButton>.
                    </asp:Panel>
                    <asp:Panel ID="pnlViewingAll" runat="server" Visible="false">
                    <asp:Label ID="Label11" runat="server" resourcekey="ViewingNationalDebatesAllLang" Text="You are currently viewing National Debates in all languages. You can also"></asp:Label>
                        
                        
                        <asp:LinkButton ID="lnkbtnViewOwnLanguage" resourcekey="ViewNatDebOwnLangLink" CssClass="bold-link" runat="server" OnClick="lnkbtnViewOwnLanguage_Click"></asp:LinkButton>.
                    </asp:Panel>
                </asp:Panel>
                <div class="cleared">
                    &nbsp;</div>
            </div>
        </div>
<h2 class="fleft">
<asp:Label ID="lblTitlePart1" runat="server" resourcekey="VotingTopicDebatesPart1"  Text=""></asp:Label><asp:Label ID="lblTopicsCount" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lblTitle" runat="server" resourcekey="VotingTopicDebates"  Text="Topic-Debates for voting"></asp:Label>
    </h2>

    <div class="sortingDropdown">
<div class="debateSortLoading hidden"></div>
    <b><asp:Label ID="lblSort" runat="server" resourcekey="SortBy"  Text="Sort by"></asp:Label>:</b>
 <asp:DropDownList ID="ddlSortDebates" CssClass="ddlSortDebates"   runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlSortDebates_SelectedIndexChanged">
        <asp:ListItem  Value="Popularity" resourcekey="Popularity" />
        <asp:ListItem  Value="Date" resourcekey="Date" />
          <asp:ListItem  Value="Title" resourcekey="Title" />
        </asp:DropDownList>
        </div>
            <div class="cleared"></div>


<div class="proposals-wrapper">
<asp:ListView ID="lstvw_ResultsSnippets" runat="server" DataSourceID="sqldtsrc_ActiveDiscussionsOwnLang"
    EnableModelValidation="True" OnDataBound="lstvw_ResultsSnippets_DataBound"  OnItemDataBound="lstvw_ResultsSnippets_ItemDataBound"  >

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
                 <asp:HyperLink CssClass="bold-link debate-title" ID="hprlnk_subject" runat="server">  <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></asp:HyperLink><a href="#" class="bold-link debate-title"></a><%--  <table style="margin: 0 0 10px 10px;float:right;">
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
                </table>--%></div><div style="margin: 3px 0 3px 0;  color: #999;">
                    <asp:Label ID="lbl_by" resourcekey="By" runat="server" Text="By"></asp:Label><asp:Label ID="lbl_UserId" runat="server" Visible="false" Text='<%# Eval("UserId") %>' />
                    <asp:HyperLink ID="hprlnk_userProfile" runat="server">
                    <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Eval("LastName") %>' />
</asp:HyperLink>&nbsp;-&nbsp;<asp:Label ID="lbl_date" runat="server" resourcekey="On" Text="On"></asp:Label>&nbsp;<asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' />
                    -&nbsp;<asp:Label ID="lbl_Views" resourcekey="Views" runat="server" Text="Views"></asp:Label>: <asp:Label ID="ViewsLabel" runat="server" Text='<%# Eval("Views") %>' /> <div class="deliberation-small-flag small-flag-<%# Eval("ThreadLanguage") %>">
                            </div>
                     
                    
                   <%-- <asp:LinkButton ID="LinkButton3" CssClass="ThumbsUpButton-display" CommandName="RateThreadUp"
                                CommandArgument='<%# Eval("ThreadID") %>' Style="border: none;" runat="server">
                                <asp:Label ID="lbl_ThumbsUp" runat="server" Text='<%# Eval("ThumbsUp") %>'></asp:Label></asp:LinkButton><asp:LinkButton
                                    ID="LinkButton4" CssClass="ThumbsDownButton-display" CommandName="RateThreadDown" CommandArgument='<%# Eval("ThreadID") %>'
                                    Style="border: none;" runat="server">
                                    <asp:Label ID="lbl_ThumbsDown" runat="server" Text='<%# Eval("ThumbsDown") %>'></asp:Label></asp:LinkButton>--%><asp:Label ID="UsernameLabel" Visible="false" runat="server" Text='<%# Eval("Username") %>' />
                </div>
                <div>

                    <%--<asp:Label ID="BodyLabel" runat="server" Text='<%# GetTrimmedBody( Eval("Body").ToString()) %>' />--%>
                   <asp:Label ID="lbl_Body" runat="server" Text='<%# Eval("Body") %>' />
                    <asp:HyperLink ID="hprlnk_post" runat="server">
                        <asp:Label ID="Label2" runat="server" resourcekey="CompleteProposal"></asp:Label> &raquo;
                    </asp:HyperLink></div></td></tr><tr style="">
            <td valign="top" colspan="2">
              
                <br />
                <%--<asp:LinkButton ID="lnkbtn_ApproveThread" CommandName="ApproveThread" CssClass="Ourspace_ToolbarLink"
                    CommandArgument='<%# Eval("ThreadID") %>' runat="server">Approve</asp:LinkButton>--%><asp:Label ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' />
                <asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' />
            </td>
          
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
        <table id="Table6" runat="server" style="float: left; clear: left;">
            <tr id="Tr5" runat="server">
                <td id="Td5" runat="server">
                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                        <%--  <tr runat="server" style="">
                            <th runat="server">
                                Views
                            </th>
                            <th runat="server">
                                Username
                            </th>
                           
                            <th runat="server">
                                ThreadID</th>
                            <th runat="server">
                                ForumID</th><th id="Th1" runat="server">
                                </th>
                            <th id="Th2" runat="server">
                            </th>
                        </tr>--%>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="Tr1" runat="server">
                    <td id="Td1" runat="server" style="" class="pager-wrapper">
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="5">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="pager-link" 
                                    ShowNextPageButton="False" ShowPreviousPageButton="True" />
                                <asp:NumericPagerField NumericButtonCssClass="pager-link" CurrentPageLabelCssClass="pager-link-inactive" />
                                <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="pager-link"  
                                    ShowNextPageButton="True" ShowPreviousPageButton="False" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
        </table>
    </LayoutTemplate>
</asp:ListView>
</div>


<asp:ListView ID="lstvw_ActiveDiscussions" runat="server" Visible="false" DataSourceID="sqldtsrc_ActiveDiscussions"
    EnableModelValidation="True" OnItemCommand="lstvw_ActiveDiscussions_ItemCommand"
    OnItemDataBound="lstvw_ActiveDiscussions_ItemDataBound" DataKeyNames="ThreadID">
    <EmptyDataTemplate>
        <table id="Table3" runat="server" style="">
            <tr>
                <td>
                    No data was returned. </td></tr></table></EmptyDataTemplate><ItemTemplate>
        <tr style="">
            <td style="">
                <asp:HiddenField ID="hdnfld_CurrentProposal" runat="server" Value='<%# Eval("ThreadID") %>' />
                <div class="highlighted-header">
                    <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></div>
                ><asp:Label ID="lbl_UserId" runat="server" Visible="false" Text='<%# Eval("UserId") %>' />
                
                <div style="border-top:1px solid #eee; background-color:#f3f3f3; margin-bottom:5px;">
                <div style=" font-style:italic; padding:20px;"><asp:Label ID="lbl_by" runat="server" Text="By "></asp:Label><asp:HyperLink ID="hprlnk_userProfile" runat="server">
                <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("FirstName") %>' />
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("LastName") %>' />
                      </asp:HyperLink><asp:Label ID="lbl_date" runat="server" Text="on"></asp:Label><asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' />
                <asp:Label ID="UsernameLabel" Visible="false" runat="server" Text='<%# Eval("Username") %>' />
                </div><div style="padding: 40px; padding-top:0; padding-bottom:10px; font-style:italic;">
                    <asp:Label ID="BodyLabel" runat="server" Text='<%# Eval("Body") %>' />
                </div>
                <div style="float:left; padding-left:20px; width:200px;">
                    <asp:Label ID="lbl_Views" runat="server" Text="Views"></asp:Label>: <b><asp:Label ID="ViewsLabel" runat="server" Text='<%# Eval("Views") %>' /></b></div>
                       
                <div style="padding: 0 20px 20px 0; float:right; width:200px;">
                    <div style="text-align: right; ">
                        <asp:HyperLink ID="hprlnk_post" runat="server">View complete proposal &raquo;
                        </asp:HyperLink></div></div><div style="clear:both;"></div>
</div>

                <div style="font-weight:bold; padding:10px 15px 10px 15px;  background-color:#D6ECFF;">
                    <asp:Label ID="lbl_solutions" runat="server" Text="Vote for your favourite proposed solutions"></asp:Label>:</div><asp:Label ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' />
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
            </tr><tr>
            <td valign="top">
               
                <asp:LinkButton ID="lnkbtn_ApproveThread" CommandName="ApproveThread" CssClass="Ourspace_ToolbarLink"
                    CommandArgument='<%# Eval("ThreadID") %>' runat="server">Approve</asp:LinkButton><asp:ListView ID="lstvw_Solutions" runat="server" DataKeyNames="SolutionID" DataSourceID="sqldtsrc_Solutions"
                    EnableModelValidation="True" InsertItemPosition="LastItem" OnDataBound="lstvw_Solutions_DataBound"
                    OnItemDataBound="lstvw_Solutions_ItemDataBound" OnItemCommand="lstvw_Solutions_ItemCommand">
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
                        <table id="Table1" runat="server" style="">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <InsertItemTemplate>
                        <tr>
                            <td colspan="7">
                                <asp:Panel ID="pnl_insertTemplate" runat="server">
                                    <table>
                                        <tr id="Tr2" runat="server" style="">
                                            <th id="Th7" runat="server">
                                            </th>
                                            <th id="Th5" runat="server">
                                                Order </th><th id="Th1" runat="server">
                                                Text </th><th id="Th2" runat="server">
                                                Thumbs<br /> Up </th><th id="Th3" runat="server">
                                                Thumbs<br /> Down </th><%-- %><th id="Th6" runat="server">
                                SolutionID </th>--%> <th id="Th8" runat="server">
                                            </th>
                                        </tr>
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
                                                <asp:TextBox ID="TextTextBox" Style="width: 200px; height: 200px;" runat="server"
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
                                                        EnableClientScript="False"></asp:RegularExpressionValidator></td></tr></table></asp:Panel></td></tr></InsertItemTemplate><ItemTemplate>

                    <tr>
                   <td>
                   <div class="solutions-item">
                    <table >

                        <tr style="">
                            <td valign="top">
                           
                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" /><asp:Button
                                    ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />

                                    <table style="width: 65px; margin: 0 10px 0 0;">
                    <tr>
                        <td class="Forum_ThumbsCell">
                       
                            <asp:LinkButton ID="LinkButton1" CssClass="ThumbsUpButton" CommandName="RateSolutionUp"
                                CommandArgument='<%# Eval("solutionID") %>' Style="border: none;" runat="server">
                                <asp:Label ID="lbl_ThumbsUp" runat="server" Text='<%# Eval("ThumbsUp") %>'></asp:Label></asp:LinkButton><asp:LinkButton
                                    ID="LinkButton2" CssClass="ThumbsDownButton" CommandName="RateSolutionDown" CommandArgument='<%# Eval("solutionID") %>'
                                    Style="border: none;" runat="server">
                                    <asp:Label ID="lbl_ThumbsDown" runat="server" Text='<%# Eval("ThumbsDown") %>'></asp:Label></asp:LinkButton></td><td>
                        </td>
                    </tr>
                </table>


                            </td>
                            
                            <td colspan="6"><div style="padding-bottom:10px;">
                                <asp:Label ID="TextLabel" runat="server" Text='<%# Eval("Text") %>' />
                                </div>
                            </td>
                          </tr>
                           
                           
                                <%--<asp:Label ID="ThreadIDLabel" runat="server" Text='<%# Eval("ThreadID") %>' />, <asp:Label ID="SolutionIDLabel" runat="server" Text='<%# Eval("SolutionID") %>' />--%>
                         
                            </table>
                            </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <asp:Label ID="lbl_NoSolutions" runat="server" Text="No data was returned." Visible="False"></asp:Label><table
                            id="Table2" runat="server" cellpadding="0" cellspacing="0">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table class="solutions-table" cellpadding="0" cellspacing="0" id="itemPlaceholderContainer" runat="server" border="0"
                                        style="">
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="" class="pager-wrapper">
                       
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


                <asp:SqlDataSource ID="sqldtsrc_Solutions" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
                    SelectCommand="SELECT * FROM [Ourspace_Proposal_Solutions] WHERE ([ThreadID] = @ThreadID) ORDER BY ThumbsUp DESC, ThumbsDown"
                    ConflictDetection="OverwriteChanges" DeleteCommand="DELETE FROM [Ourspace_Proposal_Solutions] WHERE [SolutionID] = @original_SolutionID"
                    InsertCommand="INSERT INTO [Ourspace_Proposal_Solutions] ([ThreadID], [Text], [ThumbsUp], [ThumbsDown], [Position]) VALUES (@ThreadID, @Text, 0, 0, @Position)"
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
            </td>
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
        <table id="Table4" runat="server">
            <tr id="Tr4" runat="server">
                <td id="Td3" runat="server">
                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
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
    DeleteCommand="DELETE FROM [Ourspace_Proposal_Solutions] WHERE [SolutionID] = @original_SolutionID"
    InsertCommand="INSERT INTO [Ourspace_Proposal_Solutions] ([ThreadID], [Text], [ThumbsUp], [ThumbsDown], [Position]) VALUES (@ThreadID, @Text, 0, 0, @Position)"
    OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [Ourspace_Proposal_Solutions] SET [ThreadID] = @ThreadID, [Text] = @Text, [ThumbsUp] = @ThumbsUp, [ThumbsDown] = @ThumbsDown, [Position] = @Position WHERE [SolutionID] = @original_SolutionID"
    OnSelected="sqldtsrc_Solutions_Selected">
    <DeleteParameters>
        <asp:Parameter Name="original_SolutionID" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <%-- <asp:Parameter Name="ThreadID" Type="Int32" />--%>
        <asp:ControlParameter ControlID="hdnfld_CurrentProposal" Name="ThreadID" PropertyName="Value"
            Type="Int32" />
        <asp:Parameter Name="Text" Type="String" />
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
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ActiveDiscussions" 
    runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Users.FirstName, Users.LastName, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Groups.ModuleID = 381) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.phaseId = 3) ORDER BY Forum_Posts.CreatedDate DESC" 
    ></asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ActiveDiscussionsByDate" 
    runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Users.FirstName, Users.LastName, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Groups.ModuleID = 381) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.phaseId = 3) ORDER BY Forum_Posts.CreatedDate DESC" 
    ></asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ActiveDiscussionsByTitle" 
    runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Users.FirstName, Users.LastName, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Groups.ModuleID = 381) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.phaseId = 3) ORDER BY Forum_Posts.CreatedDate" 
    ></asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ActiveDiscussionsOwnLang" 
    runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Users.FirstName, Users.LastName, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Groups.ModuleID = 381) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.phaseId = 3) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = @lang) ORDER BY Forum_Threads.Views DESC" 
    ><SelectParameters><asp:SessionParameter 
            DefaultValue="en-GB" Name="lang" SessionField="activeDiscussionsOwnLang" /></SelectParameters></asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ActiveDiscussionsOwnLangByDate" 
    runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Users.FirstName, Users.LastName, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Groups.ModuleID = 381) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.phaseId = 3) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = @lang) ORDER BY Forum_Posts.CreatedDate DESC" 
    ><SelectParameters><asp:SessionParameter 
            DefaultValue="en-GB" Name="lang" SessionField="activeDiscussionsOwnLang" /></SelectParameters></asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ActiveDiscussionsOwnLangByTitle" 
    runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Users.FirstName, Users.LastName, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Groups.ModuleID = 381) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.phaseId = 3) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = @lang) ORDER BY Forum_Posts.Subject" 
  ><SelectParameters><asp:SessionParameter 
            DefaultValue="en-GB" Name="lang" SessionField="activeDiscussionsOwnLang" /></SelectParameters></asp:SqlDataSource>
<asp:SqlDataSource 
    ID="sqldtsrc_ActiveDiscussionsByID" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Users.FirstName, Users.LastName, Forum_Posts.UserID FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId WHERE (Forum_Groups.ModuleID = 381) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.phaseId = 3) AND (Forum_Threads.ThreadID = @threadId)"><SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_DiscussionByID" Name="threadId" 
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:HiddenField ID="hdnfld_CurrentProposal" runat="server" />
<asp:HiddenField ID="hdnfld_DiscussionByID" runat="server" />
