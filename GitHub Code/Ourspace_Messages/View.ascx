<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_Messages.View" AutoEventWireup="false"
    CodeBehind="View.ascx.cs" %>
<asp:HiddenField ID="hdnfld_profileUserID" runat="server" />
<asp:HiddenField ID="hdnfld_conversationID" runat="server" />
<asp:HiddenField ID="hdnfld_ConversationWithUserID" runat="server" />
<asp:SqlDataSource ID="sqldtsrc_messages" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Ourspace_Messages.MessageID, Ourspace_Messages.ConversationID, Ourspace_Messages.FromUserID, Ourspace_Messages.ToUserID, Ourspace_Messages.Date, Ourspace_Messages.IsRead, Ourspace_Messages.Body, Users.FirstName, Users.LastName FROM Users INNER JOIN Ourspace_Messages ON Users.UserID = Ourspace_Messages.FromUserID WHERE (Ourspace_Messages.ToUserID = @userID) ORDER BY Ourspace_Messages.Date DESC">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_profileUserID" Name="userID" PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:LinkButton ID="lnkbtn_BackToInbox" CssClass="Ourspace_ToolbarLink" runat="server"
    OnClick="lnkbtn_BackToInbox_Click" Visible="False">Back to Inbox</asp:LinkButton>
<asp:ListView ID="lstvw_messages" runat="server" DataKeyNames="MessageID" DataSourceID="sqldtsrc_messages"
    EnableModelValidation="True" OnItemDataBound="lstvw_messages_ItemDataBound" Visible="false">
    <EmptyDataTemplate>
        <table id="Table1" runat="server" style="">
            <tr>
                <td>
                    No data was returned.
                </td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <ItemTemplate>
        <tr style="">
            <td>
                <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
                <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
            </td>
            <td>
                <a class='<%# "messageRead_" + Eval("IsRead") %>'></a>
                <asp:Label ID="MessageIDLabel" runat="server" Text='<%# Eval("MessageID") %>' Visible="false" />
                <asp:Label ID="ConversationIDLabel" runat="server" Visible="false" Text='<%# Eval("ConversationID") %>' />
                <asp:Label ID="FromUserIDLabel" runat="server" Visible="false" Text='<%# Eval("FromUserID") %>' />
                <asp:Label ID="ToUserIDLabel" runat="server" Visible="false" Text='<%# Eval("ToUserID") %>' />
            </td>
            <td>
                <asp:HyperLink ID="hprlnk_conversation" CssClass="GotoMessage_Link" runat="server">
                    <asp:Label ID="BodyLabel" runat="server" Text='<%# Eval("Body") %>' /></asp:HyperLink>
            </td>
            <td>
                <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
            </td>
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
        <table id="Table2" runat="server">
            <tr id="Tr1" runat="server">
                <td id="Td1" runat="server">
                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="Tr3" runat="server">
                <td id="Td2" runat="server" style="">
                    <asp:DataPager ID="DataPager1" runat="server">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="Ourspace_ToolbarLink"
                                ShowFirstPageButton="True" ShowLastPageButton="True" />
                        </Fields>
                    </asp:DataPager>
                </td>
            </tr>
        </table>
    </LayoutTemplate>
</asp:ListView>
<asp:SqlDataSource ID="sqldtsrc_conversation" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Ourspace_Messages.MessageID, Ourspace_Messages.FromUserID, Ourspace_Messages.ToUserID, Ourspace_Messages.Date, Ourspace_Messages.IsRead, Ourspace_Messages.Body, Ourspace_Messages.ConversationID, Users.FirstName, Users.LastName FROM Ourspace_Messages INNER JOIN Users ON Ourspace_Messages.FromUserID = Users.UserID WHERE (Ourspace_Messages.ConversationID = @conversation) AND (Ourspace_Messages.FromUserID = @user ) OR (Ourspace_Messages.ConversationID = @conversation) AND (Ourspace_Messages.ToUserID = @user ) ORDER BY Ourspace_Messages.Date">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_conversationID" Name="conversation" PropertyName="Value" />
        <asp:ControlParameter ControlID="hdnfld_profileUserID" Name="user" PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:ListView ID="lstvw_conversation" runat="server" DataKeyNames="MessageID" DataSourceID="sqldtsrc_conversation"
    EnableModelValidation="True" Visible="False" OnItemDataBound="lstvw_conversation_ItemDataBound">
    <EmptyDataTemplate>
        <table runat="server" style="">
            <tr>
                <td>
                    No data was returned.
                </td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <ItemTemplate>
      <div class="messageBodyDivOuter">
        <div class="messageBodyDiv">
       
        <div class="profileMiniContainer">
       <asp:Image ID="img_profileMini" runat="server" />
          </div>
          <div class="messageDetailsContainer">
          <div class="messageDetailsRow">
               <b> <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
                <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
           </b>
         
                <asp:Label ID="DateLabel" CssClass="MessageDate" runat="server" Text='<%# Eval("Date") %>' />
        </div>
    
           <div class="messageContents">
               <asp:Label ID="MessageIDLabel" Visible="false" runat="server" Text='<%# Eval("MessageID") %>' />
                <asp:Label ID="FromUserIDLabel" Visible="false" runat="server" Text='<%# Eval("FromUserID") %>' />
                <asp:HiddenField ID="hdnfld_FromUserID" Visible="false" Value='<%# Eval("FromUserID") %>' runat="server" />
                <asp:Label ID="ToUserIDLabel" Visible="false" runat="server" Text='<%# Eval("ToUserID") %>' />
                <asp:HiddenField ID="hdnfld_ToUserID" Visible="false" Value='<%# Eval("ToUserID") %>' runat="server" />
                <asp:Label ID="ConversationIDLabel" Visible="false" runat="server" Text='<%# Eval("ConversationID") %>' />
          
                <asp:Label ID="BodyLabel" runat="server" Text='<%# Eval("Body") %>' />
                </div>
                </div>
              
        </div>
        <div class="clear"></div>
        </div>
        
      
    </ItemTemplate>
    <LayoutTemplate>
        <div runat="server" class="conversationMessages">
           
                    <div id="itemPlaceholderContainer" runat="server" border="0" style="">
                        
                        <table id="itemPlaceholder" runat="server">
                        </table>
                    </div>
            
          <div class="conversationPagerContainer">
                    <asp:DataPager ID="DataPager1" runat="server">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="Ourspace_ToolbarLink"
                                ShowFirstPageButton="True" ShowLastPageButton="True" />
                        </Fields>
                    </asp:DataPager>
                    </div>

        </div>
    </LayoutTemplate>
</asp:ListView>
<div class="replyWrapper">
<asp:TextBox ID="txt_reply" runat="server" Visible="false" TextMode="MultiLine"></asp:TextBox>
<asp:LinkButton ID="lnkbtn_reply" runat="server" Visible="false" CssClass="Ourspace_ToolbarLink"
    OnClick="lnkbtn_reply_Click">Reply</asp:LinkButton></div>
