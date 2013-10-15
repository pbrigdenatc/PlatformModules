<%@ Control AutoEventWireup="false" Codebehind="AttachmentControl.ascx.vb" Inherits="DotNetNuke.Modules.Forum.WebControls.AttachmentControl" Language="vb" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<asp:Panel ID="pnlContainer" runat="server">
	<table id="tblAttachment" runat="server" cellpadding="0" cellspacing="0">
	    <tr>
		   <td colspan="2" class="uploaded-td"><dnn:label id="plAttachments" runat="server" CssClass="Forum_Row_AdminText" Suffix="" controlname="lstAttachments"></dnn:label></td>
	    </tr>
	    <tr valign="top">
		   <td><asp:ListBox ID="lstAttachments" runat="server" CssClass="Forum_NormalTextBox" Width="450px" Height="40px" /></td>
		   <td>&nbsp;<asp:ImageButton ID="cmdDelete" CssClass="attach-delete" runat="server" ImageUrl="~/images/delete.gif" CausesValidation="false" /></td>
	    </tr>
	    <tr>
		   <td colspan="2" class="upload-td"><dnn:label id="plUpload" runat="server" CssClass="Forum_Row_AdminText" Suffix="" controlname="fuFile"></dnn:label></td>
	    </tr>
	    <tr>
		   <td><asp:FileUpload ID="fuFile" runat="server" width="300px" CssClass="Forum_NormalTextBox " /><asp:LinkButton ID="cmdUpload" runat="server" CssClass="action-button-inline fright" CausesValidation="false" /></td>
		   <td>&nbsp;</td>
	    </tr>
	</table>
	<asp:Label id="lblMessage" runat="server" CssClass="NormalRed" />
</asp:Panel>