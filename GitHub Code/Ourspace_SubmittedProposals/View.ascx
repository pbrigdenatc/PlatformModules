<%@ Control language="C#" Inherits="DotNetNuke.Modules.Ourspace_SubmittedProposals.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
<asp:SqlDataSource ID="sqldtsrc_submittedProposals" 
    runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>" 
    
    
    
    SelectCommand="SELECT TOP(3) Forum_Posts.UserID, Forum_Posts.CreatedDate, Users.Username, Users.DisplayName FROM Forum_Posts INNER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID WHERE (Forum_Posts.ThreadID = @threadid) AND (Ourspace_Forum_Post_Thumbs.IsSolution = 1) ORDER BY Forum_Posts.CreatedDate DESC">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_ThreadId" DefaultValue="-1" 
            Name="threadid" PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<div class="proposed-solutions">
<asp:HiddenField ID="hdnfld_ThreadId" runat="server" />
<asp:Repeater ID="Repeater1" runat="server" 
    DataSourceID="sqldtsrc_submittedProposals" 
    onitemdatabound="Repeater1_ItemDataBound">
    <ItemTemplate>
    
    <div class="proposalRow" >
        <asp:Image ID="imgUser" Width="25" Height="25" runat="server" />
        
        <asp:Label ID="Label2" runat="server"  resourcekey="By" Text=""></asp:Label>: 
        <b><asp:Label ID="lblUserName" Visible="false" runat="server" Text='<%#Eval("Username")%>'></asp:Label>
        <asp:Label ID="lblUserDisplayName" runat="server" Text=""></asp:Label></b><br />
        <asp:Label ID="Label3" runat="server"  resourcekey="On" Text=""></asp:Label>: 
        
        <asp:Label ID="lblDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
        
    </div>
    <div class="cleared"></div>
    </ItemTemplate>
</asp:Repeater>

<asp:HyperLink ID="hprlnk_ViewAllSubmittedProposals" class="view-all-proposals" runat="server" NavigateUrl="#">
    <asp:Label ID="Label1" resourcekey="ViewAllSubmited" runat="server" Text="View all submitted proposals..."></asp:Label></asp:HyperLink>
    </div>