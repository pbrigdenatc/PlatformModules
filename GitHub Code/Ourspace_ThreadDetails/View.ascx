<%@ Control language="C#" Inherits="DotNetNuke.Modules.Ourspace_ThreadDetails.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
<asp:SqlDataSource ID="sqldtsrc_ThreadInfo" 
    runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>" 
    
    SelectCommand="SELECT Forum_Threads.Replies, Ourspace_Forum_Thread_Info.phaseId, Forum_Forums.Name, Ourspace_Forum_Thread_Info.ThreadLanguage FROM Forum_Threads INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID WHERE (Forum_Threads.ThreadID = @threadId)">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_ThreadId" DefaultValue="-1" 
            Name="threadId" PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:HiddenField ID="hdnfld_ThreadId" runat="server" />
<div id="thread-details-container">
<asp:Repeater ID="rptr_ThreadInfo"  runat="server" 
        DataSourceID="sqldtsrc_ThreadInfo" 
        onitemdatabound="rptr_ThreadInfo_ItemDataBound">
<ItemTemplate>

&bull; 
    <asp:Label ID="Label4" runat="server" Text="" resourcekey="Country"></asp:Label>:    <div class="microprofile-flag small-flag-<%#Eval("ThreadLanguage")%>" style="display: inline-block; margin-left: 4px;"></div> <br />

&bull; 
    <asp:Label ID="Label1" runat="server" Text="" resourcekey="RelatedTo"></asp:Label>:      <asp:HyperLink ID="hprlnk_Category" CssClass="" Text='<%#Eval("Name")%>' runat="server"></asp:HyperLink><br />
&bull; 
    <asp:Label ID="Label2" runat="server" Text="" resourcekey="Currentphase"></asp:Label>: <b><asp:Label ID="lblPhaseName"  runat="server" Text='<%#Eval("phaseId")%>'></asp:Label></b><br />
&bull; 
    <asp:Label ID="Label3" runat="server" Text="" resourcekey="Posts"></asp:Label>:

    <asp:Label ID="lblReplies" runat="server" Text='<%#Eval("Replies")%>'></asp:Label>
 <br /><br />
   </ItemTemplate>
</asp:Repeater>

<asp:Panel ID="pnlLikeButton" runat="server">
  
 <iframe src="//www.facebook.com/plugins/like.php?href=http%3A%2F%2Fwww.joinourspace.eu%2FJoinOpenDebates%2Ftabid%2F62%2Fthreadid%2F<%= GetThreadId() %>%2Fscope%2Fposts%2FDefault.aspx&amp;send=false&amp;layout=button_count&amp;width=200&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font&amp;height=21&amp;appId=220955987953254" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:200px; height:21px;" allowTransparency="true"></iframe>
  </asp:Panel>

 </div>

