<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_Phase1ThreadInfo.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>

<div class="info-div">
    <div class="info-icon">
              <asp:Label ID="lblPhase1Instructions" resourcekey="ThreadDiscussionPhaseInstructions" runat="server"
    Text=""></asp:Label>

    <asp:Label ID="lblPhase2Instructions" Visible="false" resourcekey="ThreadNewPhaseInstr" runat="server"
    Text=""></asp:Label>
        
        <asp:HyperLink ID="hprlnk_GoToCurrentThreadPhase" Visible="false" resourcekey="goToCurrentPhase" runat="server"></asp:HyperLink>

    <div class="cleared"></div>
        <asp:LinkButton ID="lnkbtn_RejectThread" CssClass="action-button fright" runat="server"
    resourcekey="reject" OnClick="lnkbtn_RejectThread_Click"></asp:LinkButton><asp:LinkButton
        ID="lnkbtn_ApproveThread" CssClass="action-button fright margR" runat="server"
        resourcekey="approve" OnClick="lnkbtn_ApproveThread_Click"></asp:LinkButton>
<asp:ListView ID="lstvwRejectionReason" runat="server" DataSourceID="sqldtsrc_RejectionReason"
    EnableModelValidation="True" onitemdatabound="lstvwRejectionReason_ItemDataBound">
    <ItemTemplate>
        <tr style="">
            <td>
                <ul style="margin-bottom:0;"><li><asp:Label ID="resourceKeyLabel" runat="server" resourcekey='<%# Eval("resourceKey") %>' />.
               </li></ul>
            </td>
            <%--   <td>
                
            </td>
            <td>
                <asp:Label ID="descriptionLabel" runat="server" 
                    Text='<%# Eval("description") %>' />
            </td>--%>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
  
    </EmptyDataTemplate>
    <LayoutTemplate>
        <div>
            <table id="Table1" runat="server">
                <tr id="Tr1" runat="server">
                    <td id="Td1" runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr id="Tr2" runat="server" style="">
                                <td>
                                    <asp:Label ID="lblRejectedIntro" resourcekey="rejectedIntro" runat="server" Text=""></asp:Label>:
                                </td>
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
        </div>
    </LayoutTemplate>
</asp:ListView>
    <div class="cleared"></div>


        
        <asp:Panel ID="pnlRejectThread" Visible="false" runat="server">
    <div style="width: 735px; float: right;" class="info-divx">
        <b>
            <asp:Label ID="Label1" runat="server" resourcekey="whyReject" Text="Why are you rejecting this topic?"></asp:Label></b>
            <div class="cleared"></div>
        <div style="width: 415px; float: left;" class="margT">
            <asp:RadioButtonList ID="rdbtnlst_RejectionReasons"  runat="server" DataSourceID="sqldtsrc_GetRejectionReasons"
                DataTextField="description" DataValueField="id" RepeatLayout="Flow">
            </asp:RadioButtonList>
        
        </div>
        <div style="width: 300px; float: right;">
            <b><asp:Label ID="Label2" runat="server" resourcekey="Comment" Text="Comment"></asp:Label></b>
            <asp:TextBox ID="txtRejectionComment" class="fright margB" Rows="3" Width="100%"
                runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
        <br />
        <div style="margin-top: 15px;">
            <asp:LinkButton ID="lnkBtnCancelRejection" CssClass="action-button fright " runat="server"
                OnClick="lnkBtnCancelRejection_Click" resourcekey="Cancel">Cancel</asp:LinkButton>
            <asp:LinkButton ID="lnkBtn_SubmitRejection" CssClass="action-button fright margR"
                runat="server" OnClick="lnkBtn_SubmitRejection_Click" resourcekey="Submit"></asp:LinkButton></div>
    </div>
    <div class="cleared">
    </div>
</asp:Panel>
           <div class="cleared"></div>
    

    </div>
</div>

<asp:SqlDataSource ID="sqldtsrc_RejectionReason" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Ourspace_ThreadRejectionReasons.resourceKey, Ourspace_Forum_Thread_Info.rejectComment, Ourspace_ThreadRejectionReasons.description FROM Ourspace_ThreadRejectionReasons INNER JOIN Ourspace_Forum_Thread_Info ON Ourspace_ThreadRejectionReasons.id = Ourspace_Forum_Thread_Info.rejectReasonId WHERE (Ourspace_Forum_Thread_Info.ThreadId = @threadId) AND (Ourspace_Forum_Thread_Info.rejectReasonId &gt; 0)">
    <SelectParameters>
        <asp:QueryStringParameter Name="threadId" QueryStringField="threadid" />
    </SelectParameters>
</asp:SqlDataSource>


<asp:SqlDataSource ID="sqldtsrc_GetRejectionReasons" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT * FROM [Ourspace_ThreadRejectionReasons]">
</asp:SqlDataSource>
