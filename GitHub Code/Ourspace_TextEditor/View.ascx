<%@ Control language="C#" Inherits="DotNetNuke.Modules.Ourspace_TextEditor.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="texteditor" Src="~/controls/texteditor.ascx" %>
<%@ Register TagPrefix="forum" TagName="Attachment" Src="~/DesktopModules/Forum/Controls/AttachmentControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelControl.ascx" %>
   <div class="hidden">
  

       <asp:Label ID="lbl_CurrentPostReply" CssClass="replyPostId" runat="server" Text=""></asp:Label>
       <asp:HiddenField ID="hdnfld_CurrentPostReply" runat="server" />
   <asp:Label ID="lblAlreadyVotedTitle" CssClass="lblAlreadyVotedTitle" resourcekey="AlreadyVoted" runat="server" Text="Already voted!"></asp:Label>
   <div id="dialogs-hider" class="hidden">
<asp:Label ID="lblAlreadyVoted" CssClass="lblAlreadyVoted" resourcekey="YouHaveAlreadyVoted" runat="server" Text="You have already voted!"></asp:Label>
     <div class="dialog-link-wrapper">
   <a class="action-button-inline-dull already-voted-close"><asp:Label ID="Label11" runat="server" resourcekey="Close" Text="Close"></asp:Label></a>
   </div>



    <div class="proposal-success-wrap">
    <asp:Label ID="lblProposalSuccess" runat="server" Text="Your proposal was successfully submitted!"></asp:Label>
    <div class="dialog-link-wrapper">
   <a class="action-button-inline-dull" id="proposal-success-close"><asp:Label ID="Label7" runat="server" resourcekey="Close" Text="Close"></asp:Label></a>

   </div>
    </div>

    <div class="post-success-wrap">
    <asp:Label ID="Label4" runat="server" Text="Your feedback was successfully submitted!"></asp:Label>
    <div class="dialog-link-wrapper">
   <a class="action-button-inline-dull" id="post-success-close"><asp:Label ID="Label8" runat="server" resourcekey="Close" Text="Close"></asp:Label></a>
   </div>
    </div>





  </div>

  <div class="post-please-login-proposal">

    <asp:Label ID="Label5" runat="server" Text="You must first login in order to vote, reply to posts and submit proposals." resourcekey="PleaseLogInMessage"></asp:Label>


     <div class="dialog-link-wrapper">
 <a class="proposal-cancel action-button-inline-dull"><span id="dnn_ctr753_View_lblCloseProposalDialog"><asp:Label ID="Label9" runat="server" resourcekey="Close" Text="Close"></asp:Label></span></a>
         <asp:HyperLink
       ID="HyperLink2" CssClass="action-button-inline popup-login-btn" runat="server">
             <asp:Label ID="Label10" runat="server" resourcekey="LogIn" Text="Label"></asp:Label></asp:HyperLink>
        </div>
    </div>
  </div>
<div class="textEditor-wrapper"><h3>
    <asp:Label ID="Label12" runat="server" resourcekey="ReplyTopic-Debate" Text="Reply on this Topic-Debate"></asp:Label></h3>
 
<div class="feedback-instructions">
   

     
     
    <asp:Label ID="Label6" CssClass="PostPleaseLoginTitle hidden" runat="server" resourcekey="PleaseLogIn" Text=""></asp:Label>

      <asp:Label ID="lblProposalSubmitDialogTitle" CssClass="ProposalDialogTitle hidden" runat="server" resourcekey="SubmitProposal" Text="Submit Proposal-Solution"></asp:Label>
       <asp:Label ID="lblProposalSuccessDialogTitle" CssClass="ProposalSuccessTitle hidden" runat="server" Text="Success!"></asp:Label>
       <asp:Label ID="lblPostSuccessDialogTitle" CssClass="PostSuccessTitle hidden" runat="server" Text="Success!"></asp:Label>
        

    <asp:Label ID="lblUserId" CssClass="currentUserId hidden" runat="server" Text="Label"></asp:Label>

    <asp:Label ID="Label13" runat="server" resourcekey="ImportantNote" Text="Label"></asp:Label>
    <asp:HyperLink Visible="false" ID="hprlnkTermsAndCons" resourcekey="TermsAndCondOfOurspace" runat="server" Text="Terms & Conditions"></asp:HyperLink>
    <asp:Label ID="Label14" runat="server" resourcekey="TermsAndCondOfOurspace" Text="Label"></asp:Label>
</div>
<dnn:texteditor Width="100%"  HtmlEncode="true" ID="txtEditor" runat="server" Height="200"  ChooseMode="false" />
<%--<div class="available-chars">Available chracters: 231</div>
    <asp:Label ID="lblPostId" runat="server" CssClass="hidden-post-id" Text=""></asp:Label>
     <asp:Label ID="lblSubject" runat="server" CssClass="hidden-post-subject" Text=""></asp:Label>--%>
     <table class="margB">
      <tr id="rowAttachments" runat="server" width="200px">
		                <td class="Forum_Row_AdminL" valign="top">
		                   
		                </td>
		                <td class="Forum_Row_AdminR" align="left" width="80%">
                        <%--<span class="attach-a-file" id="proposal-attach" >Attach a file</span>--%>

			                <forum:Attachment ID="ctlAttachment"  cssClass="atach" runat="server" Width="375px" />
		                </td>
	                </tr></table>

    <asp:LinkButton ID="lnkbtnSubmitReply" runat="server" 
        CssClass="action-button-inline" onclick="lnkbtnSubmitReply_Click" resourcekey="SubmitReply" Text="Submit reply"></asp:LinkButton>
    &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="or"></asp:Label>
      <a  class="feedback-cancel"><asp:Label ID="Label2" runat="server" resourcekey="Cancel" Text="Cancel"></asp:Label></a>
    
</div>

<div class="textEditor-propose-solution" >
<table>
<tr>
<td class="propose-label-td">

    <dnn:Label Text="Title:" ResourceKey="Title" runat="server" HelpText="The title of your proposal" ID="dnnlblTitle" />

</td>
<td>
    <asp:TextBox ID="txtProposalTitle" runat="server"></asp:TextBox><asp:RequiredFieldValidator
        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProposalTitle" ValidationGroup="submitProposal" ErrorMessage="Please enter a title for your proposal"></asp:RequiredFieldValidator>
   </td>
</tr>
<tr>
<td  class="propose-label-td">

   
      <dnn:Label Text="Description:" ResourceKey="Description" runat="server" HelpText="Your proposal" ID="dnnlblDescription" />
     
</td>
<td  >
    <asp:TextBox ID="txtProposalDescription" runat="server" CssClass="proposalDescription" TextMode="MultiLine"></asp:TextBox>
     
     
     <asp:RequiredFieldValidator
        ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtProposalDescription" ValidationGroup="submitProposal" ErrorMessage="Please enter a description for your proposal"></asp:RequiredFieldValidator>
  
    
    <div id="available-chars-wrap">
     <asp:Label ID="Label3" runat="server" resourcekey="AvailableCharacters" Text="Available characters:"></asp:Label>&nbsp;<span id="available-chars">500</span></div>
   </td>
</tr>
<tr>
<td colspan="2"><div class="proposal-submission-link-wrapper">
 <a  class="proposal-cancel action-button-inline-dull"><asp:Label ID="lblCloseProposalDialog" runat="server" resourcekey="Close" Text="Close"></asp:Label></a>
 
         <asp:LinkButton ID="lnkbtnSubmitProposal" runat="server"  ValidationGroup="submitProposal"
        CssClass="action-button-inline" onclick="lnkbtnSubmitProposal_Click" resourcekey="Submit"></asp:LinkButton></div></td>
</tr>
</table>

</div>
