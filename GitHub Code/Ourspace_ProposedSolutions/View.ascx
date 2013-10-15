<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_ProposedSolutions.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<asp:Panel ID="pnlInWrongPhase" runat="server" Visible="false">
    <div class="info-div">
        <div class="info-icon">
            <asp:Panel ID="pnlInPhase3" Visible="false" runat="server">
                <asp:Label ID="lblInPhase3" runat="server" Text="This topic debate is now in the Voting phase."></asp:Label>
                <asp:HyperLink ID="hprlnk_GoToPhase3" runat="server">Click here to see this topic debate in the Voting phase.</asp:HyperLink>
            </asp:Panel>
            <asp:Panel ID="pnlInPhase4" Visible="false" runat="server">
                <asp:Label ID="lblInPhase4" runat="server" Text="This topic debate is now in the Results phase."></asp:Label>
                <asp:HyperLink ID="hprlnk_GoToPhase4" runat="server">Click here to see this topic debate in the Results phase.</asp:HyperLink>
            </asp:Panel>
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="pnlAdminInstructions" Visible="false" runat="server">
    <div class="info-div">
        <div class="info-icon">
            <asp:Panel ID="pnlInPhase2" Visible="false" runat="server">
                <asp:Label ID="lblInPhase2" runat="server" resourcekey="Phase2AdminDesc" Text="Admin message: Here you can see all proposals made for this topic debate. If you want to promote this phase to phase 3 you have to feature at least 3 solutions, then click on the 'Promote to phase 3' button. Remember that promoting a topic-debate to the voting phase is irreversable."></asp:Label>
                <p>
                    <b>
                        <asp:Label ID="lblFeaturedCount" runat="server" Text="Label"></asp:Label></b>&nbsp;<asp:Label
                            ID="lblFeaturedDesc" resourcekey="haveBeenFeatured" runat="server" Text="solutions have been featured."></asp:Label>
                  
                </p>
                <p>
                    <asp:LinkButton ID="lnkbtnPromoteToPhase3" Visible="false" runat="server" CssClass="action-button fleft"
                        OnClick="lnkbtnPromoteToPhase3_Click" resourcekey="PromoteTo3"></asp:LinkButton>
                </p>
            </asp:Panel>
             <asp:Panel ID="pnlAdminInPhase3" Visible="false" runat="server">
                <asp:Label ID="Label2" runat="server" resourcekey="Phase3AdminDesc" Text="Here you can see the featured solutions and what votes they have received from Ourspace users."></asp:Label>
                 
                <p>
                    
                    <asp:Label ID="Label5" runat="server" resourcekey="PossibleGoPhase4" Text=" It is possible to promote this topic-debate to the Results phase."></asp:Label>
                </p>
                <p>
                    <asp:LinkButton ID="lnkbtnPromoteToPhase4" runat="server" CssClass="action-button fleft"
                        OnClick="lnkbtnPromoteToPhase4_Click"  resourcekey="PromoteTo4"></asp:LinkButton>
                </p>
            </asp:Panel>
            <asp:Panel ID="pnlNotInPhase2" Visible="false" runat="server">
                <asp:Label ID="lblNoTasks" runat="server" Text="There are no administrative tasks that can be done on this page in this phase."></asp:Label>
            </asp:Panel>
            <div class="cleared">
                &nbsp;</div>
        </div>
    </div>
</asp:Panel>
<h2>
    <asp:Label ID="lbl_Title" runat="server" resourcekey="DescriptionOfTopicDebate" Text="Description of this topic-debate"></asp:Label></h2>
<asp:HiddenField ID="hdnfld_ThreadId" runat="server" />
<asp:SqlDataSource ID="sqldtsrc_submittedProposals" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Posts.UserID, Forum_Posts.CreatedDate, Users.Username, Users.DisplayName FROM Forum_Posts INNER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID WHERE (Forum_Posts.ThreadID = @threadid) AND (Ourspace_Forum_Post_Thumbs.IsSolution = 1)">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_ThreadId" DefaultValue="-1" Name="threadid"
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_ResultDetails" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Threads.Views, Users.Username,Users.UserId, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Ourspace_Proposals_Thumbs.ThumbsUp, Ourspace_Proposals_Thumbs.ThumbsDown, Users.FirstName, Users.LastName, Ourspace_Forum_Thread_Info.phaseId FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId LEFT OUTER JOIN Ourspace_Proposals_Thumbs ON Forum_Threads.ThreadID = Ourspace_Proposals_Thumbs.ThreadID WHERE (Forum_Groups.ModuleID = 381) AND (Forum_Posts.ParentPostID = 0) AND (Ourspace_Forum_Thread_Info.ThreadId = @threadId)">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_ThreadId" DefaultValue="-1" Name="threadId"
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_AllProposalsFeatured" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Users.UserID, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Users.FirstName, Users.LastName, Ourspace_Forum_Thread_Info.phaseId, Ourspace_Forum_Post_Thumbs.IsSolution, Forum_Posts.PostID, Ourspace_Proposal_Solutions.ThumbsUp, Ourspace_Proposal_Solutions.ThumbsDown, Ourspace_Proposal_Solutions.IsFeatured FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID INNER JOIN Ourspace_Proposal_Solutions ON Forum_Posts.PostID = Ourspace_Proposal_Solutions.PostId WHERE (Forum_Groups.ModuleID = 381) AND (Ourspace_Forum_Thread_Info.ThreadId = @threadId) AND (Ourspace_Forum_Post_Thumbs.IsSolution = 1) AND (Ourspace_Proposal_Solutions.IsFeatured = 'true') ORDER BY ThumbsUp DESC, ThumbsDown ASC">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_ThreadId" DefaultValue="-1" Name="threadId"
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqldtsrc_AllProposals" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    SelectCommand="SELECT Forum_Threads.Views, Users.Username, Users.UserID, Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.ThreadID, Forum_Threads.ForumID, Users.FirstName, Users.LastName, Ourspace_Forum_Thread_Info.phaseId, Ourspace_Forum_Post_Thumbs.IsSolution, Ourspace_Forum_Post_Thumbs.ThumbsUp, Ourspace_Forum_Post_Thumbs.ThumbsDown, Forum_Posts.PostID, Ourspace_Proposal_Solutions.IsFeatured FROM Forum_Threads INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID INNER JOIN Users ON Forum_Posts.UserID = Users.UserID INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Ourspace_Forum_Post_Thumbs ON Forum_Posts.PostID = Ourspace_Forum_Post_Thumbs.PostID LEFT OUTER JOIN Ourspace_Proposal_Solutions ON Forum_Posts.PostID = Ourspace_Proposal_Solutions.PostId LEFT OUTER JOIN Ourspace_Proposals_Thumbs ON Forum_Threads.ThreadID = Ourspace_Proposals_Thumbs.ThreadID WHERE (Forum_Groups.ModuleID = 381) AND (Ourspace_Forum_Thread_Info.ThreadId = @threadId) AND (Ourspace_Forum_Post_Thumbs.IsSolution = 1)  ORDER BY ThumbsUp DESC, ThumbsDown ASC">
    <SelectParameters>
        <asp:ControlParameter ControlID="hdnfld_ThreadId" DefaultValue="-1" Name="threadId"
            PropertyName="Value" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:ListView ID="lstvw_ResultDetails" DataSourceID="sqldtsrc_ResultDetails" runat="server"
    OnItemDataBound="lstvw_DebateProposals_ItemDataBound">
    <EmptyDataTemplate>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="No topic-debate found!"></asp:Label>
            </td>
        </tr>
    </EmptyDataTemplate>
    <ItemTemplate>
        <tr>
            <td runat="server" id="imageTd" class="debate-thumbnail">
                <asp:Image ID="userImage" CssClass="userThumb" runat="server" />
            </td>
            <td runat="server" id="textTd">
                <div>
                    <div id="hidden-title">
                        <%# Eval("Subject") %></div>
                    <asp:HyperLink CssClass="bold-link debate-title" ID="hprlnk_subject" runat="server">
                        <asp:Label ID="SubjectLabel" runat="server" Visible="false" CssClass="" Text='<%# Eval("Subject") %>' /></asp:HyperLink><a
                            href="#" class="bold-link debate-title"></a></div>
                <div style="margin: 3px 0 3px 0; color: #999;">
                    <asp:Label ID="lbl_by" runat="server" resourcekey="By"></asp:Label>&nbsp;<asp:HyperLink ID="hprlnk_userProfile" runat="server">
                        <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;<asp:Label
                            ID="Label1" runat="server" Text='<%# Eval("LastName") %>' />
                    </asp:HyperLink>
                    -
                    <asp:Label ID="lbl_date" runat="server" resourcekey="On"></asp:Label>&nbsp;<asp:Label ID="CreatedDateLabel"
                        runat="server" Text='<%# Eval("CreatedDate") %>' />
                    <asp:Label ID="UsernameLabel" Visible="false" runat="server" Text='<%# Eval("Username") %>' />
                </div>
                <div>
                    <%--<asp:Label ID="BodyLabel" runat="server" Text='<%# GetTrimmedBody( Eval("Body").ToString()) %>' />--%>
                    <asp:Label ID="lbl_Body" runat="server" Text='<%# Eval("Body") %>' />
                    <asp:HyperLink ID="hprlnk_post" runat="server" resourcekey="viewCompleteProposal"> &raquo;
                    </asp:HyperLink></div>
                <div class="favorite-solution">
                </div>
            </td>
        </tr>
        <tr style="">
            <td valign="top" colspan="2">
                <br />
                <asp:Label ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' />
                <asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' />
                <asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("userId") %>' />
            </td>
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
        <table id="Table2" runat="server" style="float: left; clear: left;">
            <tr id="Tr1" runat="server">
                <td id="Td1" runat="server">
                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
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
<h2 class="no-border">
    <asp:Label ID="lbl_ProposalsTitle" runat="server" resourcekey="ProposalsOnTopicDebate" Text="Proposals on this topic-debate"></asp:Label></h2>
<asp:ListView ID="lstvw_AllProposals" runat="server" OnItemDataBound="lstvw_DebateProposals_ItemDataBound"
    OnItemCommand="lstvw_AllProposals_ItemCommand">
    <EmptyDataTemplate>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="No proposals have been submitted for this topic-debate yet!"></asp:Label>
            </td>
        </tr>
    </EmptyDataTemplate>
    <ItemTemplate>
        <tr>
            <td runat="server" id="imageTd" class="debate-thumbnail proposal-item">
                <asp:Image ID="userImage" CssClass="userThumb" runat="server" />
            </td>
            <td runat="server" id="textTd" class="proposal-item">
                <div id="hidden-title">
                    <%# Eval("Subject") %></div>
                    <asp:Label ID="lbl_Subject" Visible="false" runat="server" Text='<%# Eval("Subject") %>' />
                <div style="margin: 3px 0 9px 0; color: #999;">
                    <asp:Label ID="lbl_by" runat="server" resourcekey="By" Text="By"></asp:Label>&nbsp;<asp:HyperLink ID="hprlnk_userProfile" runat="server">
                  <asp:Label
                        ID="lbl_Name" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Eval("LastName") %>' />
                        </asp:HyperLink>

                    -
                    <asp:Label ID="lbl_date" runat="server" resourcekey="On" Text="On"></asp:Label>&nbsp;<asp:Label ID="CreatedDateLabel"
                        runat="server" Text='<%# Eval("CreatedDate") %>' />
                    <asp:Label ID="UsernameLabel" Visible="false" runat="server" Text='<%# Eval("Username") %>' />
                </div>
                <asp:Panel ID="pnlAdminControls" runat="server">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Featured" resourcekey="Featured" Enabled="false" Checked='<%# getFeaturedStatus( Eval("IsFeatured").ToString()) %>' />
                    <div class="cleared"></div>
                    <asp:LinkButton ID="lnkbtnFeatureSolution" CssClass="action-button fleft" CommandName="FeatureSolution" CommandArgument='<%# Eval("PostId") %>'
                        runat="server" resourcekey="Feature">Feature</asp:LinkButton>
                    <asp:LinkButton ID="lnkbtnUnfeatureSolution" style="margin-left:10px;" CssClass="action-button fleft" CommandName="UnfeatureSolution" CommandArgument='<%# Eval("PostId") %>'
                        runat="server" resourcekey="Unfeature">Unfeature</asp:LinkButton>
                        <div class="cleared"></div>
                </asp:Panel>
                <div style="margin: 3px 0 3px 0;">
                <div style="position:relative;">
                    <asp:Label ID="lblProposalPosition" runat="server" Text=""></asp:Label>
                    </div><b>
                        <asp:Label ID="SubjectLabel" runat="server" CssClass="" Text='<%# Eval("Subject") %>' /></b></div>
                <div>
                    <%--<asp:Label ID="BodyLabel" runat="server" Text='<%# GetTrimmedBody( Eval("Body").ToString()) %>' />--%>
                    <asp:Label ID="lbl_BodyWhole" runat="server" Text='<%# Eval("Body") %>' />
                </div>
                <div class="favorite-solution">
                </div>
            </td>
        </tr>
        <tr style="">
        <td></td>
            <td valign="top">
                <asp:Panel ID="pnlTranslationBtnWrap" runat="server" CssClass="margT">
               
                <asp:LinkButton ID="lnkbtnTranslateProposal" CssClass="BtnTranslatePost" CommandName="translateProposal" CommandArgument='<%# Eval("Body") %>' resourcekey="SeeTranslation" runat="server">See translation</asp:LinkButton>
                    <asp:Label ID="lblLoadingTranslation" resourcekey="TranslationLoading" CssClass="hidden" runat="server" Text="Label"></asp:Label>
                
                <b><asp:Label ID="lblServiceDown" Visible="false" runat="server" resourcekey="TranslationServiceDown" Text=""></asp:Label></b>
               
                <asp:Label ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' />
                <asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' />
                <asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("userId") %>' />
             </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="solutionFooter">
                <table class="margT">
                    <tr>
                        <td valign="middle" align="left" class="Forum_ThumbsCell">
                            <asp:Panel ID="pnlPhase2Voting" runat="server" Visible="false">
                            
                            <asp:LinkButton CommandName="thumbsUp" CommandArgument='<%# Eval("PostId") %>' ID="lnkbtn_ThumbsUp"
                                CssClass="ThumbsUpButton" runat="server" Text='<%# Eval("ThumbsUp") %>'></asp:LinkButton><asp:LinkButton
                                    ID="lnkbtn_ThumbsDown" CommandName="thumbsDown" CssClass="ThumbsDownButton" runat="server"
                                    CommandArgument='<%# Eval("PostId") %>' Text='<%# Eval("ThumbsDown") %>'></asp:LinkButton>
                                    </asp:Panel>


                                     <asp:Panel ID="pnlPhase3Voting" runat="server" Visible="false">
                            
                            <asp:LinkButton CommandName="thumbsUpAgree" CommandArgument='<%# Eval("PostId") %>' ID="lnkbtn_agree"
                                CssClass="AgreeButton" runat="server">
                                <asp:Label ID="Label6" runat="server" resourcekey="Agree"></asp:Label>
                                |
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("ThumbsUp") %>'></asp:Label></asp:LinkButton>
                                
                                <asp:LinkButton
                                    ID="lnkbtn_disagree" CommandName="thumbsDownDisagree" CssClass="DisagreeButton" runat="server"
                                    CommandArgument='<%# Eval("PostId") %>'>
                                    <asp:Label ID="Label7" runat="server" resourcekey="Disagree"></asp:Label>
                                    |
                                      <asp:Label ID="Label8" runat="server" Text='<%# Eval("ThumbsDown") %>'></asp:Label>
                                    </asp:LinkButton>
                                    </asp:Panel>


                        </td>
                    </tr>
                </table>
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
<asp:Label ID="lblPageTitle" runat="server" resourcekey="pageTitle" Text=""></asp:Label>