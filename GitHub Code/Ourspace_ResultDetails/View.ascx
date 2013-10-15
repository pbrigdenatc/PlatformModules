<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_ResultDetails.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<h2>
    <asp:Label ID="lblTitle" runat="server" Text="Details of this topic-debate"></asp:Label></h2>
   
<asp:ListView ID="lstvw_ResultDetails" DataSourceID="sqldtsrc_ResultDetails" runat="server"
    OnItemDataBound="lstvw_DebateProposals_ItemDataBound">
    <EmptyDataTemplate>
        <tr>
            <td>
                No data
            </td>
        </tr>
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
                    <asp:HyperLink CssClass="bold-link debate-title" ID="hprlnk_subject" runat="server">
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></asp:HyperLink><a
                            href="#" class="bold-link debate-title"></a><%--  <table style="margin: 0 0 10px 10px;float:right;">
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
                </table>--%></div><div style="margin: 3px 0 3px 0; color: #999;">
                    <asp:Label ID="lbl_by" runat="server" Text="By"></asp:Label><asp:HyperLink ID="hprlnk_userProfile" runat="server">
                   <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Eval("LastName") %>' />
                        </asp:HyperLink>- <asp:Label ID="lbl_date" runat="server" Text="On"></asp:Label><asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' />
                    - <asp:Label ID="lbl_Views" runat="server" Text="Views"></asp:Label>: <asp:Label ID="ViewsLabel" runat="server" Text='<%# Eval("Views") %>' />
                    <asp:Label ID="UsernameLabel" Visible="false" runat="server" Text='<%# Eval("Username") %>' />
                </div>
                <div>
                    <%--<asp:Label ID="BodyLabel" runat="server" Text='<%# GetTrimmedBody( Eval("Body").ToString()) %>' />--%>
                    <asp:Label ID="lbl_Body" runat="server" Text='<%# Eval("Body") %>' />
                    <asp:HyperLink ID="hprlnk_post" runat="server">View complete proposal &raquo;
                    </asp:HyperLink></div><div class="favorite-solution">
                    <asp:LinkButton ID="lnkbtn_ReadTopicDebate" runat="server">Read all the topic debate</asp:LinkButton></div></td></tr><tr style="">
            <td valign="top" colspan="2">
                <br />
                <asp:Label ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' />
                <asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' />
                <asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("UserID") %>' />
            </td>
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
        <table id="Table2" runat="server" style="float: left; clear: left;">
            <tr id="Tr1" runat="server">
                <td id="Td1" runat="server">
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
        </table>
    </LayoutTemplate>
</asp:ListView>
<div class="cleared">
</div>
<h2>
    <asp:Label ID="Label1" runat="server" Text="Best proposals for this topic-debate"></asp:Label></h2><div style="text-align:right;">
        <asp:HyperLink ID="hprlnk_ViewAllProposals" runat="server" Text="View all proposals for this topic debate..."></asp:HyperLink></div><asp:ListView 
    ID="lstvw_Solutions" runat="server" DataSourceID="sqldtsrc_ResultSolutions"
    OnItemDataBound="lstvw_Solutions_ItemDataBound" 
    onitemcommand="lstvw_Solutions_ItemCommand"><ItemTemplate>

        <table class="thumbs-wrapper">
            <tbody>
                <tr>
                    <td valign="top">
                        <asp:Label ID="lbl_currentRow" runat="server" CssClass="solution-counter" Text=""></asp:Label></td><td valign="middle" align="left" class="Forum_ThumbsCell">
                        <a class="ThumbsUpSolution">
                            <%# Eval("ThumbsUp") %></a><a class="ThumbsDownSolution"><%# Eval("ThumbsDown") %></a>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="solution-text-wrapper">
            <asp:HyperLink ID="hprlnk_SolutionTitle" CssClass="bold-link debate-title" runat="server"> <%# Eval("Subject") %></asp:HyperLink><br /><div style="color: #999;">
                <asp:Label ID="lbl_by" runat="server" Text="By"></asp:Label> <asp:HyperLink ID="hprlnk_userProfile" runat="server">
               <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Eval("LastName") %>' />
                    </asp:HyperLink> - <asp:Label ID="lbl_date" runat="server" Text="On"></asp:Label> <asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>' /></div>
            <div class="solution-text-separator">
            </div>
            <asp:Label ID="lbl_Body" runat="server" Text='<%# Eval("Body") %>'></asp:Label>
             <asp:Panel ID="pnlTranslationBtnWrap" runat="server" CssClass="margT">
               
                <asp:LinkButton ID="lnkbtnTranslateProposal" CssClass="BtnTranslatePost" CommandName="translateProposal" CommandArgument='<%# Eval("Body") %>' resourcekey="SeeTranslation" runat="server">See translation</asp:LinkButton><asp:Label ID="lblLoadingTranslation" resourcekey="TranslationLoading" CssClass="hidden" runat="server" Text="Label"></asp:Label><b><asp:Label ID="lblServiceDown" Visible="false" runat="server" resourcekey="TranslationServiceDown" Text=""></asp:Label></b></asp:Panel>
            </div><div class="cleared">
         <asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("UserID") %>' />
        </div>
        </ItemTemplate><EmptyDataTemplate>
        No Solutions found</EmptyDataTemplate><LayoutTemplate>
        <div id="itemPlaceholderContainer" runat="server" border="0" style="">
            <div id="itemPlaceholder" runat="server">
            </div>
        </div>
    </LayoutTemplate>
</asp:ListView>
<asp:SqlDataSource ID="sqldtsrc_ResultDetails" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Proposals_Thumbs.ThumbsUp, Ourspace_Proposals_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Ourspace_Forum_Thread_Info.phaseId, Forum_Posts.UserID FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Proposals_Thumbs ON Forum_Threads.ThreadID = Ourspace_Proposals_Thumbs.ThreadID WHERE (Forum_Groups.ModuleID = 381) AND (Ourspace_Forum_Thread_Info.phaseId = 4) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.ThreadId = @threadId)"><SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_ThreadId" DefaultValue="-1" Name="threadId"
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ResultSummary" 
    runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    SelectCommand="SELECT TOP (1) Forum_Posts.Body FROM Forum_Posts INNER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID WHERE (Forum_Posts.ThreadID = @threadId) ORDER BY Forum_Posts.CreatedDate DESC"><SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_ThreadId" DefaultValue="-1" Name="threadId"
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ResultSolutions" 
    runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    SelectCommand="SELECT Users.Username, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Users.FirstName, Users.LastName, Ourspace_Proposal_Solutions.ThumbsUp, Ourspace_Proposal_Solutions.ThumbsDown, Users.UserID FROM Forum_Posts INNER JOIN Ourspace_Proposal_Solutions ON Forum_Posts.PostID = Ourspace_Proposal_Solutions.PostId LEFT OUTER JOIN Users ON Forum_Posts.UserID = Users.UserID WHERE (Ourspace_Proposal_Solutions.IsFeatured = 1) AND (Forum_Posts.ThreadID = @threadId)"><SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_ThreadId" DefaultValue="-1" Name="threadId"
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:HiddenField ID="hdnfld_ThreadId" runat="server" Value="1" />
<h2>
    <asp:Label ID="Label2" runat="server" Text="Summary"></asp:Label></h2><asp:ListView ID="lstvw_ResultSummary" runat="server" DataSourceID="sqldtsrc_ResultSummary"
    EnableModelValidation="True">
    <LayoutTemplate>
        <div id="itemPlaceholderContainer" runat="server" border="0" style="">
            <div id="itemPlaceholder" runat="server">
            </div>
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <asp:Label ID="BodyLabel" runat="server" Text='<%# Server.HtmlDecode( Eval("Body").ToString()) %>' />
    </ItemTemplate>
    <EmptyDataTemplate>
        No data</EmptyDataTemplate></asp:ListView>
        <span id="disqus"></span><asp:Panel ID="pnlDisqus" runat="server" Visible="true">
    <div id="disqus_thread"></div>
    <script type="text/javascript">
        /* * * CONFIGURATION VARIABLES: EDIT BEFORE PASTING INTO YOUR WEBPAGE * * */
        var disqus_shortname = 'joinourspace'; // required: replace example with your forum shortname
        var disqus_identifier = '<%= Request.QueryString["result"] %>';

        /* * * DON'T EDIT BELOW THIS LINE * * */
        (function () {
            var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
            dsq.src = 'http://' + disqus_shortname + '.disqus.com/embed.js';
            (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
        })();
    </script>
    <noscript>Please enable JavaScript to view the <a href="http://disqus.com/?ref_noscript">comments powered by Disqus.</a></noscript>
    <a href="http://disqus.com" class="dsq-brlink">comments powered by <span class="logo-disqus">Disqus</span></a> </asp:Panel>