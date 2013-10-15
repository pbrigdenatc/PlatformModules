'
' DotNetNuke® - http://www.dotnetnuke.com
' Copyright (c) 2002-2011
' by DotNetNuke Corporation
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'
Option Strict On
Option Explicit On

Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Services.FileSystem
Imports DotNetNuke.Modules.Ourspace_Utilities
Imports System.Globalization


Namespace DotNetNuke.Modules.Forum



    ''' <summary>
    ''' Renders the Posts view UI.  
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Public Class Posts
        Inherits ForumObject

#Region "Private Members"

        Private _ThreadID As Integer
        Private _PostCollection As List(Of PostInfo)
        Private _objThread As ThreadInfo
        Private _PostPage As Integer = 0
        Private _TrackedForum As Boolean = False
        Private _TrackedThread As Boolean = False
        ' ATC
        Const PROPOSE_YOUR_TOPIC_MODULEID As Integer = 415

#Region "Controls"

        'CP: NOTE: Telerik conversion
        'Private trcRating As Telerik.Web.UI.RadRating
        Private trcRating As DotNetNuke.Wrapper.UI.WebControls.DnnRating
        Private ddlViewDescending As DotNetNuke.Web.UI.WebControls.DnnComboBox
        Private chkEmail As CheckBox
        Private ddlThreadStatus As DotNetNuke.Web.UI.WebControls.DnnComboBox
        Private cmdThreadAnswer As LinkButton
        Private cmdPostThumbs As LinkButton
        Private cmdPostTranslate As LinkButton
        Private cmdSwitchFeedbackProposal As LinkButton
        Private txtForumSearch As TextBox
        Private cmdForumSearch As ImageButton
        Private hsThreadAnswers As New Hashtable
        Private hsPostThumbs As New Hashtable
        Private hsTranslatedPosts As New Hashtable
        Private hsSwitchFeedbackProposal As New Hashtable
        Private hsPostThumbsDown As New Hashtable
        Private hsPostTranslate As New Hashtable
        Private rblstPoll As RadioButtonList
        Private cmdVote As LinkButton
        Private cmdBookmark As ImageButton
        Private tagsControl As DotNetNuke.Web.UI.WebControls.Tags
        Private txtQuickReply As TextBox
        Private cmdSubmit As LinkButton
        Private cmdThreadSubscribers As LinkButton

#End Region

        ''' <summary>
        ''' This is used to determine the permissions for the current user/forum combination. 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private ReadOnly Property objSecurity() As ModuleSecurity
            Get
                Return New ModuleSecurity(ModuleID, TabID, ForumID, CurrentForumUser.UserID)
            End Get
        End Property

        ''' <summary>
        ''' The PostID being rendered, if the user was not directed here via a postid, the threadid is used.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private ReadOnly Property PostID() As Integer
            Get
                If HttpContext.Current.Request.QueryString("postid") IsNot Nothing Then
                    Return Convert.ToInt32(HttpContext.Current.Request.QueryString("postid"))
                Else
                    Return -1
                End If
            End Get
        End Property

        ''' <summary>
        ''' The ThreadID for all the posts being rendered.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Property ThreadID() As Integer
            Get
                If HttpContext.Current.Request.QueryString("threadid") IsNot Nothing Then
                    Return Convert.ToInt32(HttpContext.Current.Request.QueryString("threadid"))
                Else
                    Return _ThreadID
                End If
            End Get
            Set(ByVal Value As Integer)
                _ThreadID = Value
            End Set
        End Property

        ''' <summary>
        ''' The ThreadInfo object of the ThreadID being rendered.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Property objThread() As ThreadInfo
            Get
                Return _objThread
            End Get
            Set(ByVal Value As ThreadInfo)
                _objThread = Value
            End Set
        End Property

        ''' <summary>
        ''' The collection of posts being rendered.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Property PostCollection() As List(Of PostInfo)
            Get
                Return _PostCollection
            End Get
            Set(ByVal Value As List(Of PostInfo))
                _PostCollection = Value
            End Set
        End Property

        ''' <summary>
        ''' The page index being rendered (of the thread).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Property PostPage() As Integer
            Get
                Return _PostPage
            End Get
            Set(ByVal Value As Integer)
                _PostPage = Value
            End Set
        End Property

        ''' <summary>
        ''' If the user is tracking the containing forum (email notifications). 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Property TrackedForum() As Boolean
            Get
                Return _TrackedForum
            End Get
            Set(ByVal Value As Boolean)
                _TrackedForum = Value
            End Set
        End Property

        ''' <summary>
        ''' if the user is tracking the thread (email notifications). 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Property TrackedThread() As Boolean
            Get
                Return _TrackedThread
            End Get
            Set(ByVal Value As Boolean)
                _TrackedThread = Value
            End Set
        End Property

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Updates the thread status
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks>
        ''' </remarks>
        Protected Sub ddlThreadStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim ThreadStatus As Integer = ddlThreadStatus.SelectedIndex
            Dim ctlThread As New ThreadController

            Dim ModeratorID As Integer = -1
            If CurrentForumUser.UserID <> objThread.StartedByUserID Then
                ModeratorID = CurrentForumUser.UserID
            End If

            ctlThread.ChangeThreadStatus(ThreadID, CurrentForumUser.UserID, ThreadStatus, 0, ModeratorID, PortalID)

            Forum.Components.Utilities.Caching.UpdateThreadCache(ThreadID)
        End Sub

        ''' <summary>
        ''' This Event turns the users thread tracking on/off.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks>
        ''' </remarks>
        Protected Sub chkEmail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim ctlTracking As New TrackingController
            ctlTracking.TrackingThreadCreateDelete(ForumID, ThreadID, CurrentForumUser.UserID, chkEmail.Checked, ModuleID)
            'Forum.Components.Utilities.Caching.UpdateThreadCache(ThreadId)
        End Sub

        ''' <summary>
        ''' Applies the user's thread rating.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks>This needs to have redirect link generated from link utils.</remarks>
        Protected Sub trcRating_Rate(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim rate As Double = trcRating.Value

            If rate > 0 Then
                Dim ctlThread As New ThreadController
                ctlThread.ThreadRateAdd(ThreadID, CurrentForumUser.UserID, rate)
            End If

            Forum.Components.Utilities.Caching.UpdateThreadCache(ThreadID)
        End Sub

        ''' <summary>
        ''' Adds a user's vote to the data store for a specific poll.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub cmdVote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            ' get the user's vote and put it in the data store
            Dim intAnswerID As Integer = CInt(rblstPoll.SelectedValue)
            ' update user voting, when page is redrawn it will handle checking if user voted
            Dim cntUserAnswer As New UserAnswerController
            Dim objUserAnswer As New UserAnswerInfo

            objUserAnswer.UserID = CurrentForumUser.UserID
            objUserAnswer.PollID = objThread.PollID
            objUserAnswer.AnswerID = intAnswerID

            cntUserAnswer.AddUserAnswer(objUserAnswer)
            ' update user answer cache - 
        End Sub

        ''' <summary>
        ''' Adds or remove the current thread to the users bookmark list 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub cmdBookmark_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
            Dim BookmarkCtl As New BookmarkController
            Select Case cmdBookmark.AlternateText
                Case ForumControl.LocalizedText("RemoveBookmark")
                    BookmarkCtl.BookmarkCreateDelete(ThreadID, CurrentForumUser.UserID, False, ModuleID)
                    'Change ImageButton to support AJAX
                    cmdBookmark.AlternateText = ForumControl.LocalizedText("AddBookmark")
                    cmdBookmark.ToolTip = ForumControl.LocalizedText("AddBookmark")
                    cmdBookmark.ImageUrl = objConfig.GetThemeImageURL("forum_bookmark.") & objConfig.ImageExtension
                Case ForumControl.LocalizedText("AddBookmark")
                    BookmarkCtl.BookmarkCreateDelete(ThreadID, CurrentForumUser.UserID, True, ModuleID)
                    'Change ImageButton to support AJAX
                    cmdBookmark.AlternateText = ForumControl.LocalizedText("RemoveBookmark")
                    cmdBookmark.ToolTip = ForumControl.LocalizedText("RemoveBookmark")
                    cmdBookmark.ImageUrl = objConfig.GetThemeImageURL("forum_nobookmark.") & objConfig.ImageExtension
            End Select
        End Sub

        ''' <summary>
        ''' This takes moderators/forum admin to moderator screen with the thread loaded to view subscribers. 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub cmdThreadSubscribers_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim url As String
            url = Utilities.Links.ThreadEmailSubscribers(TabID, ModuleID, ForumID, ThreadID)
            MyBase.BasePage.Response.Redirect(url, False)
        End Sub

        ''' <summary>
        ''' This Event sets the users view preference ascending/descending and saves to 
        ''' the db. (Descending by default)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks>Anonymous users can see both views but it doesn't save to db when changed.
        ''' </remarks>
        Protected Sub ddlViewDescending_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            ForumControl.Descending = CType(ddlViewDescending.SelectedIndex, Boolean)

            Dim ctlPost As New PostController
            PostCollection = ctlPost.PostGetAll(ThreadID, PostPage, CurrentForumUser.PostsPerPage, ForumControl.Descending, PortalID)

            If CurrentForumUser.UserID > 0 Then
                Dim ctlForumUser As New ForumUserController
                ctlForumUser.UpdateUsersView(CurrentForumUser.UserID, PortalID, ForumControl.Descending)
            End If
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString())

        End Sub

        ''' <summary>
        ''' This directs the user to the search results of this particular forum. It searches this forum and the subject, body of the post. 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub cmdForumSearch_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
            If txtForumSearch.Text.Trim <> String.Empty Then
                Dim url As String
                url = Utilities.Links.ContainerSingleForumSearchLink(TabID, ForumID, txtForumSearch.Text)
                MyBase.BasePage.Response.Redirect(url, False)
            End If
        End Sub

        ''' <summary>
        ''' Submits a quickly reply to the posting API (which is related to an existing thread). 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If Len(txtQuickReply.Text) > 0 Then
                Dim RemoteAddress As String = "0.0.0.0"
                Dim strSubject As String = Utilities.ForumUtils.SetReplySubject(objThread.Subject)

                If Not HttpContext.Current.Request.UserHostAddress Is Nothing Then
                    RemoteAddress = HttpContext.Current.Request.UserHostAddress
                End If

                Dim cntPostConnect As New PostConnector
                Dim PostMessage As PostMessage

                Dim textReply As String = txtQuickReply.Text

                'textReply = textReply.Replace(vbCrLf, "<br />")
                'textReply = textReply.Replace(Environment.NewLine, "<br />")
                ' Hammond
                textReply = textReply.Replace(ControlChars.Lf, "<br />")
                'textReply = textReply.Replace("\n", "<br />")

                PostMessage = cntPostConnect.SubmitInternalPost(TabID, ModuleID, PortalID, CurrentForumUser.UserID, strSubject, textReply, ForumID, objThread.ThreadID, -1, objThread.IsPinned, False, False, objThread.ThreadStatus, "", RemoteAddress, objThread.PollID, False, objThread.ThreadID, objThread.Terms, False, "")

                Select Case PostMessage
                    Case PostMessage.PostApproved
                        '	Dim ReturnURL As String = NavigateURL()

                        '	If objModSecurity.IsModerator Then
                        '		If Not ViewState("UrlReferrer") Is Nothing Then
                        '			ReturnURL = (CType(ViewState("UrlReferrer"), String))
                        '		Else
                        '			ReturnURL = Utilities.Links.ContainerViewForumLink(TabID, objForum.ForumID, False)
                        '		End If
                        '	Else
                        '		ReturnURL = Utilities.Links.ContainerViewForumLink(TabID, ForumId, False)
                        '	End If

                        '	Response.Redirect(ReturnURL, False)
                    Case PostMessage.PostModerated
                        'tblNewPost.Visible = False
                        'tblOldPost.Visible = False
                        'tblPreview.Visible = False
                        'cmdCancel.Visible = False
                        'cmdBackToEdit.Visible = False
                        'cmdSubmit.Visible = False
                        'cmdPreview.Visible = False
                        'cmdBackToForum.Visible = True
                        'rowModerate.Visible = True
                        'tblPoll.Visible = False
                    Case Else
                        'lblInfo.Visible = True
                        'lblInfo.Text = Localization.GetString(PostMessage.ToString() + ".Text", LocalResourceFile)
                End Select
                txtQuickReply.Text = ""
                'Forum.ThreadInfo.ResetThreadInfo(ThreadId)

                Dim ctlPost As New PostController
                PostCollection = ctlPost.PostGetAll(ThreadID, PostPage, CurrentForumUser.PostsPerPage, ForumControl.Descending, PortalID)
                ' we need to redirect the user here to make sure the page is redrawn.
            Else
                ' there is no quick reply message entered, yet they clicked submit. Show end user. 
            End If
        End Sub

        ''' <summary>
        ''' Sets a specific post as an answer, only available when thread status is set to 'unresolved'. 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub cmdThreadAnswer_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
            Dim ctlThread As New ThreadController
            Dim answerPostID As Integer
            Dim Argument As String = String.Empty

            If e.CommandName = "MarkAnswer" Then
                Argument = CStr(e.CommandArgument)
                answerPostID = Int32.Parse(Argument)

                Dim ctlPost As New PostController
                Dim objPostInfo As PostInfo
                objPostInfo = ctlPost.GetPostInfo(answerPostID, PortalID)

                Dim ModeratorID As Integer = -1
                If objThread.StartedByUserID <> CurrentForumUser.UserID Then
                    ModeratorID = CurrentForumUser.UserID
                End If

                ctlThread.ChangeThreadStatus(ThreadID, objPostInfo.UserID, ThreadStatus.Answered, answerPostID, ModeratorID, PortalID)

                Forum.Components.Utilities.Caching.UpdateThreadCache(ThreadID)
            End If
        End Sub
        ' ATC Start
        Protected Sub OnThumbsCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
            VotePost(CInt(e.CommandArgument), CBool(e.CommandName))
        End Sub

        ' ATC Start
        Protected Sub OnTranslateCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
            'VotePost(CInt(e.CommandArgument), CBool(e.CommandName))

            'clientId = joinourspace_eu
            'clientSecret = Y/usBFdwOtdOG8md/Ar7XpLW82PmWepVDeWtvHHyUj8=
            ' kai 8a odigisw kai mexri leoforo alexandras gia sena, kai apo leoforo alexandras pali xalandri
            Dim translateBtn As LinkButton = CType(sender, LinkButton)
            Dim util As New Ourspace_Utilities.View
            Dim translated As String

            translated = util.TranslateText(HttpContext.Current.Application, "", CultureInfo.CurrentUICulture.Name.Substring(0, 2), HttpUtility.HtmlDecode(e.CommandName.ToString()))
            If (Not translated.Contains("#NLA#")) Then
                hsTranslatedPosts.Add(Convert.ToInt32(e.CommandArgument), translated)
            Else
                hsTranslatedPosts.Add(Convert.ToInt32(e.CommandArgument), "#NLA#")
            End If
        End Sub

        ' ATC End



        ' ATC Start
        Protected Sub OnSwitchFeedbackProposalCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
            'VotePost(CInt(e.CommandArgument), CBool(e.CommandName))

            Dim numb As String = "herro"


            If (e.CommandName.Equals("False")) Then
                SwitchPostToProposal(Convert.ToInt32(e.CommandArgument))
            ElseIf (e.CommandName.Equals("True")) Then
                SwitchPostToFeedback(Convert.ToInt32(e.CommandArgument))

            End If
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString())





        End Sub


#End Region

#Region "Public Methods"

        ''' <summary>
        ''' Instantiates this class, sets the page title and does a security check.
        ''' </summary>
        ''' <param name="forum"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal forum As DNNForum)
            MyBase.New(forum)

            Dim user As ForumUserInfo = CurrentForumUser

            If PostID > 0 Then
                Dim objPostCnt As New PostController
                Dim objPost As PostInfo = objPostCnt.GetPostInfo(PostID, PortalID)
                ThreadID = objPost.ThreadID

                ' we need to determine which page to return based on number of posts in this thread, the users posts per page count, and their asc/desc view, where this post is
                Dim cntThread As New ThreadController()
                objThread = cntThread.GetThread(ThreadID)

                ' we need to see if there is a content item for the thread, if not create one.
                If objThread.ContentItemId < 1 Then
                    Dim cntContent As New Content
                    objThread.ModuleID = ModuleID
                    objThread.TabID = TabID
                    objThread.SitemapInclude = objPost.ParentThread.ContainingForum.EnableSitemap

                    cntContent.CreateContentItem(objThread, TabID)

                    DotNetNuke.Modules.Forum.Components.Utilities.Caching.UpdateThreadCache(objThread.ThreadID)
                    objThread = cntThread.GetThread(ThreadID)
                End If

                Dim TotalPosts As Integer = objThread.Replies + 1
                Dim TotalPages As Integer = (CInt(TotalPosts / CurrentForumUser.PostsPerPage))
                Dim ThreadPageToShow As Integer = 1

                If user.ViewDescending Then
                    ThreadPageToShow = CInt(Math.Ceiling((objPost.PostsAfter + 1) / CurrentForumUser.PostsPerPage))
                Else
                    ThreadPageToShow = CInt(Math.Ceiling((objPost.PostsBefore + 1) / CurrentForumUser.PostsPerPage))
                End If
                PostPage = ThreadPageToShow
            Else
                If ThreadID > 0 Then
                    Dim cntThread As New ThreadController()
                    objThread = cntThread.GetThread(ThreadID)

                    ' we need to see if there is a content item for the thread, if not create one.
                    If objThread.ContentItemId < 1 Then
                        Dim cntContent As New Content
                        objThread.ModuleID = ModuleID
                        objThread.TabID = TabID
                        objThread.SitemapInclude = objThread.ContainingForum.EnableSitemap

                        cntContent.CreateContentItem(objThread, TabID)

                        DotNetNuke.Modules.Forum.Components.Utilities.Caching.UpdateThreadCache(objThread.ThreadID)
                        objThread = cntThread.GetThread(ThreadID)
                    End If

                    ' We need to make sure the user's thread pagesize can handle this 
                    '(problem is, a link can be posted by one user w/ page size of 5 pointing to page 2, if logged in user has pagesize set to 15, there is no page 2)
                    If Not HttpContext.Current.Request.QueryString("threadpage") Is Nothing Then
                        Dim urlThreadPage As Integer = Int32.Parse(HttpContext.Current.Request.QueryString("threadpage"))
                        Dim TotalPosts As Integer = objThread.Replies + 1

                        Dim TotalPages As Integer = CInt(Math.Ceiling(TotalPosts / CurrentForumUser.PostsPerPage))
                        Dim ThreadPageToShow As Integer

                        ' We need to check if it is possible for a pagesize in the URL for the user browsing (happens when coming from posted link by other user)
                        If TotalPages >= urlThreadPage Then
                            ThreadPageToShow = urlThreadPage
                        Else
                            ' We know for this user, total pages > user posts per page. Because of this, we know its not user using page change so show thread as normal
                            ThreadPageToShow = 0
                        End If
                        PostPage = ThreadPageToShow
                    End If
                End If
            End If

            ' If the thread info is nothing, it is probably a deleted thread
            If objThread Is Nothing Then
                ' we should consider setting type of redirect here?

                MyBase.BasePage.Response.Redirect(Utilities.Links.NoContentLink(TabID, ModuleID), True)
            End If

            ' Make sure the forum is active 
            If Not objThread.ContainingForum.IsActive Then
                ' we should consider setting type of redirect here?

                MyBase.BasePage.Response.Redirect(Utilities.Links.NoContentLink(TabID, ModuleID), True)
            End If

            ' User might access this page by typing url so better check permission on parent forum
            If Not (objThread.ContainingForum.PublicView) Then
                If Not objSecurity.IsAllowedToViewPrivateForum Then
                    ' we should consider setting type of redirect here?

                    MyBase.BasePage.Response.Redirect(Utilities.Links.UnAuthorizedLink(), True)
                End If
            End If

            If objConfig.OverrideTitle Then
                Dim Title As String
                Dim Subject As String

                If objThread.Subject.Length > Constants.SEO_TITLE_LIMIT Then
                    Subject = objThread.Subject.Substring(0, Constants.SEO_TITLE_LIMIT)
                Else
                    Subject = objThread.Subject
                End If

                If Not Subject.Length > Constants.SEO_TITLE_LIMIT Then
                    Title = Subject

                    Subject += " - " & objThread.ContainingForum.Name
                    If Not Subject.Length > Constants.SEO_TITLE_LIMIT Then
                        Title = Subject

                        Subject += " - " & Me.BaseControl.PortalName
                        If Not Subject.Length > Constants.SEO_TITLE_LIMIT Then
                            Title = Subject
                        End If
                    End If
                Else
                    Title = Subject
                End If

                MyBase.BasePage.Title = Title
            End If

            If objConfig.OverrideDescription Then
                Dim Description As String

                If objThread.Subject.Length < Constants.SEO_DESCRIPTION_LIMIT Then
                    Description = objThread.Subject
                Else
                    Description = objThread.Subject.Substring(0, Constants.SEO_DESCRIPTION_LIMIT)
                End If

                MyBase.BasePage.Description = Description
            End If

            If objConfig.OverrideKeyWords Then
                Dim KeyWords As String = ""
                Dim keyCount As Integer = 0

                If objThread.ContainingForum.ParentID = 0 Then
                    KeyWords = objThread.ContainingForum.Name
                    keyCount = 1
                Else
                    KeyWords = objThread.ContainingForum.ParentForum.Name + "," + objThread.ContainingForum.Name
                    keyCount = 2
                End If

                If objConfig.EnableTagging Then
                    For Each Term As Entities.Content.Taxonomy.Term In objThread.Terms
                        If keyCount < Constants.SEO_KEYWORDS_LIMIT Then
                            KeyWords += "," + Term.Name
                            keyCount += 1
                        Else
                            Exit For
                        End If
                    Next

                    ' If we haven't hit the keyword limit, let's add portal name to the list.
                    If keyCount < Constants.SEO_KEYWORDS_LIMIT Then
                        KeyWords += "," + Me.BaseControl.PortalName
                    End If
                End If

                MyBase.BasePage.KeyWords = KeyWords
            End If

            If PostPage > 0 Then
                PostPage = PostPage - 1
            Else
                PostPage = 0
            End If
        End Sub

        ''' <summary>
        ''' This is the first class that runs as part of New().  This could be invoked in Render as well but is not
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        Public Overrides Sub CreateChildControls()
            Controls.Clear()

            'CP: NOTE: Telerik conversion
            'Me.trcRating = New Telerik.Web.UI.RadRating
            Me.trcRating = New DotNetNuke.Wrapper.UI.WebControls.DnnRating
            With trcRating
                .Skin = "Office2007"
                .SelectionMode = Telerik.Web.UI.RatingSelectionMode.Continuous
                .IsDirectionReversed = False
                .Orientation = Orientation.Horizontal
                .Precision = Telerik.Web.UI.RatingPrecision.Half
                .ItemCount = objConfig.RatingScale
                AddHandler trcRating.Rate, AddressOf trcRating_Rate
                .AutoPostBack = True
            End With

            ' display tracking option only if user authenticated
            If CurrentForumUser.UserID > 0 Then
                ' Thread Status Dropdownlist
                Me.ddlThreadStatus = New DotNetNuke.Web.UI.WebControls.DnnComboBox
                With ddlThreadStatus
                    .ID = "lstThreadStatus"
                    .Width = Unit.Parse("150")
                    .AutoPostBack = True
                    .ClearSelection()
                End With

                ' Email notification checkbox
                chkEmail = New CheckBox
                With chkEmail
                    .CssClass = "Forum_NormalTextBox"
                    .ID = "chkEmail"
                    .Text = ForumControl.LocalizedText("MailWhenReply").Replace("[ThreadSubject]", "<b>" & objThread.Subject & "</b>")
                    .TextAlign = TextAlign.Left
                    .AutoPostBack = True
                    .Checked = False
                End With
            End If

            ' Forum view (newest to oldest/oldest to newest) dropdownlist
            ddlViewDescending = New DotNetNuke.Web.UI.WebControls.DnnComboBox
            With ddlViewDescending
                .ID = "lstViewDescending"
                .Width = Unit.Parse("150")
                .AutoPostBack = True
                'CP: NOTE: Telerik conversion
                '.Items.Add(New Telerik.Web.UI.RadComboBoxItem(ForumControl.LocalizedText("OldestToNewest")))
                '.Items.Add(New Telerik.Web.UI.RadComboBoxItem(ForumControl.LocalizedText("NewestToOldest")))
                .Items.Add(New DotNetNuke.Wrapper.UI.WebControls.DnnComboBoxItem(ForumControl.LocalizedText("OldestToNewest")))
                .Items.Add(New DotNetNuke.Wrapper.UI.WebControls.DnnComboBoxItem(ForumControl.LocalizedText("NewestToOldest")))
                .ClearSelection()
            End With

            txtForumSearch = New TextBox
            With txtForumSearch
                .CssClass = "Forum_NormalTextBox"
                .ID = "txtForumSearch"
                .Width = Unit.Parse("150")
            End With

            Me.cmdForumSearch = New ImageButton
            With cmdForumSearch
                .CssClass = "Forum_Profile"
                .ID = "cmdForumSearch"
                .AlternateText = ForumControl.LocalizedText("Search")
                .ToolTip = ForumControl.LocalizedText("Search")
                .ImageUrl = objConfig.GetThemeImageURL("s_lookup.") & objConfig.ImageExtension
            End With

            'Polls
            Me.rblstPoll = New RadioButtonList
            With rblstPoll
                .CssClass = "Forum_NormalTextBox"
                .ID = "rblstPoll"
            End With

            Me.cmdVote = New LinkButton
            With cmdVote
                .CssClass = "Forum_ToolbarLink no-float vote-button"
                .ID = "cmdVote"
                .Text = ForumControl.LocalizedText("Vote")
            End With

            If CurrentForumUser.UserID > 0 Then
                Me.cmdBookmark = New ImageButton
                With cmdBookmark
                    .CssClass = "Forum_Profile"
                    .ID = "cmdBookmark"
                End With
                Dim BookmarkCtl As New BookmarkController
                If BookmarkCtl.BookmarkCheck(CurrentForumUser.UserID, ThreadID, ModuleID) = True Then
                    With cmdBookmark
                        .AlternateText = ForumControl.LocalizedText("RemoveBookmark")
                        .ToolTip = ForumControl.LocalizedText("RemoveBookmark")
                        .ImageUrl = objConfig.GetThemeImageURL("forum_nobookmark.") & objConfig.ImageExtension
                    End With
                Else
                    With cmdBookmark
                        .AlternateText = ForumControl.LocalizedText("AddBookmark")
                        .ToolTip = ForumControl.LocalizedText("AddBookmark")
                        .ImageUrl = objConfig.GetThemeImageURL("forum_bookmark.") & objConfig.ImageExtension
                    End With
                End If
            End If

            If Not CurrentForumUser.UserID > 0 Then
                ddlViewDescending.Visible = False
            End If

            ' Tags
            Me.tagsControl = New DotNetNuke.Web.UI.WebControls.Tags
            With tagsControl
                .ID = "tagsControl"
                ' if we come up w/ our own tagging window, this needs to be changed to false.
                .AllowTagging = HttpContext.Current.Request.IsAuthenticated
                .NavigateUrlFormatString = DotNetNuke.Common.Globals.NavigateURL(objConfig.SearchTabID, "", "Tag={0}")
                .RepeatDirection = "Horizontal"
                .Separator = ","
                ' TODO: We may want to show this in future, for now we are leaving categories out of the mix.
                .ShowCategories = False
                .ShowTags = True
                .AddImageUrl = "~/images/add.gif"
                .CancelImageUrl = "~/images/lt.gif"
                .SaveImageUrl = "~/images/save.gif"
                .CssClass = "SkinObject"
            End With

            ' Quick Reply
            Me.txtQuickReply = New TextBox
            With txtQuickReply
                .CssClass = "Forum_NormalTextBox"
                .ID = "txtQuickReply"
                .Width = Unit.Percentage(99)
                .Height = 150
                .TextMode = TextBoxMode.MultiLine
                '.Text
            End With

            Me.cmdSubmit = New LinkButton
            With cmdSubmit
                .CssClass = "Forum_Link"
                .ID = "cmdSubmit"
                .Text = ForumControl.LocalizedText("cmdSubmit")
            End With

            Me.cmdThreadSubscribers = New LinkButton
            With cmdThreadSubscribers
                .CssClass = "Forum_Profile"
                .ID = "cmdThreadSubscribers"
                .Text = ForumControl.LocalizedText("cmdThreadSubscribers")
            End With

            BindControls()
            AddControlHandlers()
            AddControlsToTree()

            For Each post As PostInfo In PostCollection
                Me.cmdThreadAnswer = New System.Web.UI.WebControls.LinkButton
                With cmdThreadAnswer
                    .CssClass = "Forum_AnswerText"
                    .ID = "cmdThreadAnswer" + post.PostID.ToString()
                    .Text = ForumControl.LocalizedText("MarkAnswer")
                    .CommandName = "MarkAnswer"
                    .CommandArgument = post.PostID.ToString()
                    AddHandler cmdThreadAnswer.Command, AddressOf cmdThreadAnswer_Click
                End With
                hsThreadAnswers.Add(post.PostID, cmdThreadAnswer)
                Controls.Add(cmdThreadAnswer)

                ' ATC Start
                ' Threads can not be rated

                If (ModuleID = PROPOSE_YOUR_TOPIC_MODULEID Or post.ParentPostID <> 0) Then

                    Me.cmdPostThumbs = New System.Web.UI.WebControls.LinkButton
                    With cmdPostThumbs
                        .CssClass = "ThumbsUpButton"
                        .ID = "cmdPostThumbsUp" + post.PostID.ToString()
                        '.Text = ForumControl.LocalizedText("Thumbs Up")
                        .CommandArgument = CStr(post.PostID)
                        .CommandName = "true"
                        AddHandler cmdPostThumbs.Command, AddressOf OnThumbsCommand
                    End With
                    hsPostThumbs.Add(post.PostID, cmdPostThumbs)
                    Controls.Add(cmdPostThumbs)

                    Me.cmdPostThumbs = New System.Web.UI.WebControls.LinkButton
                    With cmdPostThumbs
                        .CssClass = "ThumbsDownButton"
                        .ID = "cmdPostThumbsDown" + post.PostID.ToString()
                        '.Text = ForumControl.LocalizedText("Thumbs Down")
                        .CommandArgument = CStr(post.PostID)
                        .CommandName = "false"
                        AddHandler cmdPostThumbs.Command, AddressOf OnThumbsCommand
                    End With
                    hsPostThumbsDown.Add(post.PostID, cmdPostThumbs)
                    Controls.Add(cmdPostThumbs)
                Else

                End If

                Me.cmdPostTranslate = New System.Web.UI.WebControls.LinkButton
                With cmdPostTranslate
                    .CssClass = "BtnTranslatePost"
                    .ID = "cmdTranslate" + post.PostID.ToString()
                    '.Text = ForumControl.LocalizedText("Thumbs Down")
                    .CommandArgument = CStr(post.PostID)
                    .CommandName = post.Body

                    AddHandler cmdPostTranslate.Command, AddressOf OnTranslateCommand
                End With
                hsPostTranslate.Add(post.PostID, cmdPostTranslate)
                Controls.Add(cmdPostTranslate)



                ' Add switch to feedback/proposal buttons


                Me.cmdSwitchFeedbackProposal = New System.Web.UI.WebControls.LinkButton
                With cmdSwitchFeedbackProposal
                    .CssClass = "Forum_Link"
                    .ID = "cmdSwitchFeedbackProposal" + post.PostID.ToString()
                    '.Text = ForumControl.LocalizedText("Thumbs Up")
                    .CommandArgument = CStr(post.PostID)
                    .CommandName = post.IsSolution.ToString()
                    AddHandler cmdSwitchFeedbackProposal.Command, AddressOf OnSwitchFeedbackProposalCommand
                End With
                hsSwitchFeedbackProposal.Add(post.PostID, cmdSwitchFeedbackProposal)
                Controls.Add(cmdSwitchFeedbackProposal)


                ' ATC End



            Next
        End Sub

        ''' <summary>
        ''' Does the actual calls for rendering the UI in logical order to build wr
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks>
        ''' </remarks>
        Public Overrides Sub Render(ByVal wr As HtmlTextWriter)
            RenderTableBegin(wr, "tblForumContainer", "Forum_Container", "", "100%", "0", "0", "", "", "0")


            ' Getting phaseId of current thread
            Dim drPhase As IDataReader = Nothing
            Dim drLang As IDataReader = Nothing
            'Dim lang As String = "en-GB"
            Dim phase As String = ""
            drPhase = DotNetNuke.Modules.Forum.DataProvider.Instance().ThreadGetPhase(objThread.ThreadID)
            'drLang = DotNetNuke.Modules.Forum.DataProvider.Instance().ThreadGetLang(objThread.ThreadID)
            If (drPhase.Read()) Then
                phase = CStr((drPhase.Item(0)))
            End If

            'If (drLang.Read()) Then
            '    lang = CStr((drLang.Item(0)))
            'End If

            'wr.Write("<h1>" & lang & "</h1>")

            ' ATC Hiding certain elements when viewing thread proposals
            If (ModuleID <> PROPOSE_YOUR_TOPIC_MODULEID) Then
                'RenderNavBar(wr, objConfig, ForumControl)
                'RenderSearchBar(wr)
                RenderTopBreadcrumb(wr)
            Else


                'wr.Write("<div class=""info-div""><div class=""info-icon""><div>")
                'wr.Write(Localization.GetString("ThreadDiscussionPhaseInstructions", objConfig.SharedResourceFile))
                'wr.Write("</div></div></div>")

            End If

            Dim Url As String
            Url = ""
            Dim parameters As String()
            Dim isFacebook As New Boolean
            isFacebook = False
            '  serv.GetOurSpaceUserImgUrl(HttpContext.Current.Server, CurrentForumUser.UserID)
            If (HttpContext.Current.Request.QueryString("facebook") IsNot Nothing) Then
                isFacebook = True
            End If
            If (phase = "3" Or phase = "4") Then
                wr.Write("<div class=""info-div""><div class=""info-icon""><div>")
                wr.Write(Localization.GetString("ThreadDiscussionClosedInstructions", objConfig.SharedResourceFile))
                wr.Write(" ")
                If (phase = "3") Then

                    If isFacebook Or TabID = 259 Then
                        parameters = {"threadid=" & ThreadID, "facebook=1"}
                        Url = DotNetNuke.Common.Globals.NavigateURL(279, "", parameters)
                        RenderLinkButton(wr, Url, Localization.GetString("GoToCurrentPhase", objConfig.SharedResourceFile), "Forum_Link")
                    Else
                        Dim tabid As New Integer
                        Dim cult As String
                        cult = CultureInfo.CurrentUICulture.Name
                        If (cult.Equals("en-GB")) Then
                            tabid = 200
                        ElseIf (cult.Equals("el-GR")) Then
                            tabid = 201
                        ElseIf (cult.Equals("cs-CZ")) Then
                            tabid = 202
                        ElseIf (cult.Equals("de-AT")) Then
                            tabid = 203
                        End If

                        parameters = {"threadid=" & ThreadID}

                        Url = DotNetNuke.Common.Globals.NavigateURL(tabid, "", parameters)
                        Url = Url.Replace("en-GB", CultureInfo.CurrentUICulture.Name)
                        RenderLinkButton(wr, Url, Localization.GetString("GoToCurrentPhase", objConfig.SharedResourceFile), "Forum_Link")


                    End If
                ElseIf (phase = "4") Then
                    If isFacebook Or TabID = 259 Then
                        parameters = {"result=" & ThreadID, "facebook=1"}
                        Url = DotNetNuke.Common.Globals.NavigateURL(275, "", parameters)
                        RenderLinkButton(wr, Url, Localization.GetString("GoToCurrentPhase", objConfig.SharedResourceFile), "Forum_Link")
                    Else




                        parameters = {"result=" & ThreadID}
                        Url = DotNetNuke.Common.Globals.NavigateURL(196, "", parameters)
                        Url = Url.Replace("en-GB", CultureInfo.CurrentUICulture.Name)
                        RenderLinkButton(wr, Url, Localization.GetString("GoToCurrentPhase", objConfig.SharedResourceFile), "Forum_Link")
                    End If
                End If


                wr.Write("</div></div></div>")
            End If

            'emena dn 
            If (CurrentForumUser.IsInRole("Administrators")) Then
                RenderTopThreadButtons(wr)
            End If

            RenderThread(wr, phase)
            If (CurrentForumUser.IsInRole("Administrators")) Then
                RenderBottomThreadButtons(wr)
                If (ModuleID <> PROPOSE_YOUR_TOPIC_MODULEID) Then
                    RenderBottomBreadCrumb(wr)
                End If
                RenderTags(wr)
                RenderQuickReply(wr)
                'RenderThreadOptions(wr)
            End If





            'RenderThreadOptions(wr)







            RenderTableEnd(wr)
            'RenderTableEnd(wr)
            'increment the thread view count
            Dim ctlThread As New ThreadController
            ctlThread.IncrementThreadViewCount(ThreadID)

            'if the user is logged in increment the user's thread view count
            'i.e. how many times a user has viewed a thread
            If (ProfileUserID > -1) Then
                ctlThread.IncrementUserThreadViewCount(ProfileUserID, ThreadID)
            End If


            'update the UserThread record
            If HttpContext.Current.Request.IsAuthenticated Then
                Dim userThreadController As New UserThreadsController
                Dim userThread As New UserThreadsInfo
                userThread = userThreadController.GetThreadReadsByUser(CurrentForumUser.UserID, ThreadID)

                If Not userThread Is Nothing Then
                    userThread.LastVisitDate = Now
                    ' Add error handling Just in case because of constraints and data integrity - This is highly unlikely to occur so do it here instead of the database(performance reasons)
                    Try
                        userThreadController.Update(userThread)
                        UserThreadsController.ResetUserThreadReadCache(userThread.UserID, ThreadID)
                    Catch exc As Exception
                        LogException(exc)
                    End Try
                Else
                    userThread = New UserThreadsInfo
                    With userThread
                        .UserID = CurrentForumUser.UserID
                        .ThreadID = ThreadID
                        .LastVisitDate = Now
                    End With
                    userThreadController.Add(userThread)
                    UserThreadsController.ResetUserThreadReadCache(userThread.UserID, userThread.ThreadID)
                End If
                ' Not sure we should keep this, we are basically updating a thread cache item if a new view was added. Is this really necessary?
                Forum.Components.Utilities.Caching.UpdateThreadCache(ThreadID)
            End If
        End Sub

#End Region

#Region "Private Methods"

        ''' <summary>
        ''' Sets handlers for certain server controls
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        Private Sub AddControlHandlers()
            Try
                If objConfig.EnableThreadStatus And CurrentForumUser.UserID > 0 Then
                    AddHandler ddlThreadStatus.SelectedIndexChanged, AddressOf ddlThreadStatus_SelectedIndexChanged
                End If

                If objConfig.MailNotification And CurrentForumUser.UserID > 0 Then
                    AddHandler chkEmail.CheckedChanged, AddressOf chkEmail_CheckedChanged
                End If

                If objConfig.EnableRatings Then
                    AddHandler trcRating.Rate, AddressOf trcRating_Rate
                End If

                If CurrentForumUser.UserID > 0 Then
                    AddHandler cmdBookmark.Click, AddressOf cmdBookmark_Click
                    AddHandler cmdThreadSubscribers.Click, AddressOf cmdThreadSubscribers_Click
                    ' Move out to support anon posting (if we allow quick reply via anonymous posting)
                    AddHandler cmdSubmit.Click, AddressOf cmdSubmit_Click
                    ' Move otu to support anon poll voting (after posting is supported)
                    AddHandler cmdVote.Click, AddressOf cmdVote_Click
                End If

                AddHandler ddlViewDescending.SelectedIndexChanged, AddressOf ddlViewDescending_SelectedIndexChanged
                AddHandler cmdForumSearch.Click, AddressOf cmdForumSearch_Click


            Catch exc As Exception
                LogException(exc)
            End Try
        End Sub

        ''' <summary>
        ''' Adds the controls to the control tree
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        Private Sub AddControlsToTree()
            Try
                If objConfig.EnableThreadStatus And CurrentForumUser.UserID > 0 Then
                    Controls.Add(ddlThreadStatus)
                End If

                If objConfig.MailNotification And CurrentForumUser.UserID > 0 Then
                    Controls.Add(chkEmail)
                End If

                If objConfig.EnableRatings Then
                    Controls.Add(trcRating)
                End If

                Controls.Add(rblstPoll)

                If CurrentForumUser.UserID > 0 Then
                    Controls.Add(cmdBookmark)
                    Controls.Add(cmdThreadSubscribers)

                    ' move for anon posting (if we allow quick reply via anonymous posting)
                    Controls.Add(txtQuickReply)
                    Controls.Add(cmdSubmit)
                    Controls.Add(cmdVote)
                End If

                Controls.Add(tagsControl)
                Controls.Add(ddlViewDescending)
                Controls.Add(txtForumSearch)
                Controls.Add(cmdForumSearch)
            Catch exc As Exception
                LogException(exc)
            End Try
        End Sub

        ''' <summary>
        ''' Binds data to the available controls to the end user
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        Private Sub BindControls()
            Try
                Dim ctlPost As New PostController

                If objConfig.EnableRatings Then
                    BindRating()
                End If

                ' All enclosed items are user specific, so we must have a userID
                If CurrentForumUser.UserID > 0 Then
                    If objConfig.EnableThreadStatus And objThread.ContainingForum.EnableForumsThreadStatus Then
                        ddlThreadStatus.Visible = True
                        ddlThreadStatus.Items.Clear()

                        'CP: NOTE: Telerik conversion
                        'ddlThreadStatus.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(Localization.GetString("NoneSpecified", objConfig.SharedResourceFile), "0"))
                        'ddlThreadStatus.Items.Insert(1, New Telerik.Web.UI.RadComboBoxItem(Localization.GetString("Unanswered", objConfig.SharedResourceFile), "1"))
                        'ddlThreadStatus.Items.Insert(2, New Telerik.Web.UI.RadComboBoxItem(Localization.GetString("Answered", objConfig.SharedResourceFile), "2"))
                        'ddlThreadStatus.Items.Insert(3, New Telerik.Web.UI.RadComboBoxItem(Localization.GetString("Informative", objConfig.SharedResourceFile), "3"))
                        ddlThreadStatus.Items.Insert(0, New DotNetNuke.Wrapper.UI.WebControls.DnnComboBoxItem(Localization.GetString("NoneSpecified", objConfig.SharedResourceFile), "0"))
                        ddlThreadStatus.Items.Insert(1, New DotNetNuke.Wrapper.UI.WebControls.DnnComboBoxItem(Localization.GetString("Unanswered", objConfig.SharedResourceFile), "1"))
                        ddlThreadStatus.Items.Insert(2, New DotNetNuke.Wrapper.UI.WebControls.DnnComboBoxItem(Localization.GetString("Answered", objConfig.SharedResourceFile), "2"))
                        ddlThreadStatus.Items.Insert(3, New DotNetNuke.Wrapper.UI.WebControls.DnnComboBoxItem(Localization.GetString("Informative", objConfig.SharedResourceFile), "3"))
                    Else
                        ddlThreadStatus.Visible = False
                    End If
                    'polling changes
                    If objThread.ThreadStatus = ThreadStatus.Poll Then
                        'CP: NOTE: Telerik conversion
                        'Dim statusEntry As New Telerik.Web.UI.RadComboBoxItem(Localization.GetString("Poll", objConfig.SharedResourceFile), ThreadStatus.Poll.ToString())
                        Dim statusEntry As New DotNetNuke.Wrapper.UI.WebControls.DnnComboBoxItem(Localization.GetString("Poll", objConfig.SharedResourceFile), ThreadStatus.Poll.ToString())
                        ddlThreadStatus.Items.Add(statusEntry)
                    End If

                    ddlThreadStatus.SelectedIndex = CType(objThread.ThreadStatus, Integer)

                    ' display tracking option only if user is authenticated and the forum module allows tracking
                    If objConfig.MailNotification Then
                        ' check to see if the user is tracking at the forum level
                        For Each objTrackForum As TrackingInfo In CurrentForumUser.TrackedForums(ModuleID)
                            If objTrackForum.ForumID = ForumID Then
                                TrackedForum = True
                                Exit For
                            End If
                        Next

                        If Not TrackedForum Then
                            Dim arrTrackThreads As List(Of TrackingInfo) = CurrentForumUser.TrackedThreads(ModuleID)
                            Dim objTrackThread As TrackingInfo

                            ' check to see if the user is tracking at the thread level
                            For Each objTrackThread In arrTrackThreads
                                If objTrackThread.ThreadID = ThreadID Then
                                    TrackedThread = True
                                    chkEmail.Checked = True
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    If (CurrentForumUser.ViewDescending) Then
                        ForumControl.Descending = True
                        ddlViewDescending.Items.FindItemByText(ForumControl.LocalizedText("NewestToOldest")).Selected = True
                    Else
                        ForumControl.Descending = False
                        ddlViewDescending.Items.FindItemByText(ForumControl.LocalizedText("OldestToNewest")).Selected = True
                    End If

                    ' Handle Polls
                    If objThread.PollID > 0 Then
                        Dim cntAnswer As New AnswerController
                        Dim arrAnswers As List(Of AnswerInfo)

                        arrAnswers = cntAnswer.GetPollAnswers(objThread.PollID)
                        If arrAnswers.Count > 0 Then
                            rblstPoll.DataTextField = "Answer"
                            rblstPoll.DataValueField = "AnswerID"
                            rblstPoll.DataSource = arrAnswers
                            rblstPoll.DataBind()

                            rblstPoll.SelectedIndex = 0
                        End If
                    End If
                Else
                    ForumControl.Descending = CType(ddlViewDescending.SelectedIndex, Boolean)
                    'CP - COMEBACK: Add way to display rating but don't allow voting (for anonymous users)
                    trcRating.Enabled = False
                End If

                tagsControl.ContentItem = DotNetNuke.Entities.Content.Common.Util.GetContentController().GetContentItem(objThread.ContentItemId)

                PostCollection = ctlPost.PostGetAll(ThreadID, PostPage, CurrentForumUser.PostsPerPage, ForumControl.Descending, PortalID)
            Catch exc As Exception
                LogException(exc)
            End Try
        End Sub

        ''' <summary>
        ''' Binds the current users rating to the rating control, also enables/disables the control.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub BindRating()
            trcRating.Value = CDec(objThread.Rating)
            trcRating.ToolTip = objThread.RatingText

            If Not CurrentForumUser.UserID > 0 Then
                trcRating.Enabled = False
            End If
        End Sub

        ''' <summary>
        ''' Renders the Rating selector, current rating image, search textbox and button
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks>
        ''' </remarks>
        Private Sub RenderSearchBar(ByVal wr As HtmlTextWriter)
            RenderRowBegin(wr) '<tr>

            ' left cap
            RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "")

            RenderCellBegin(wr, "", "", "100%", "", "", "", "") ' <td>
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "", "0") ' <table>
            RenderRowBegin(wr) '<tr>

            RenderCellBegin(wr, "", "", "100%", "left", "", "", "") ' <td>
            RenderTableBegin(wr, "", "", "", "", "2", "0", "", "", "0")  ' <table>
            RenderRowBegin(wr) '<tr>

            '[skeel] Display bookmark image button here
            If CurrentForumUser.UserID > 0 Then
                RenderCellBegin(wr, "", "", "", "left", "", "", "") ' <td> 
                cmdBookmark.RenderControl(wr)
                RenderCellEnd(wr) ' </td>
            End If

            ' Display rating only if user is authenticated
            If PostCollection.Count > 0 Then
                'check to see if new setting, enable ratings is enabled
                If objConfig.EnableRatings And objThread.ContainingForum.EnableForumsRating Then
                    RenderCellBegin(wr, "", "", "", "left", "", "", "") ' <td> 
                    'CP - Sub in ajax image rating solution here for ddl
                    trcRating.RenderControl(wr)

                    ' See if user has set status, if so we need to bind it
                    RenderCellEnd(wr) ' </td>

                    RenderCellBegin(wr, "", "", "", "left", "", "", "")  ' <td> '
                    RenderCellEnd(wr) ' </td>
                End If
            Else
                RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td> 
                wr.Write("&nbsp;")
                RenderCellEnd(wr) ' </td>
            End If

            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </td>

            RenderCellBegin(wr, "", "", "100%", "right", "middle", "", "")
            RenderTableBegin(wr, 0, 0, "InnerTable") '<table>
            RenderRowBegin(wr) ' <tr>
            RenderCellBegin(wr, "", "", "", "", "middle", "", "") ' <td>
            txtForumSearch.RenderControl(wr)
            RenderCellEnd(wr) ' </td>

            RenderCellBegin(wr, "", "", "", "", "middle", "", "") ' <td>
            cmdForumSearch.RenderControl(wr)
            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>

            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </td>
            RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "")
            RenderRowEnd(wr) ' </tr>
        End Sub

        ''' <summary>
        ''' Renders the row w/ the navigation breadcrumb
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks></remarks>
        Private Sub RenderTopBreadcrumb(ByVal wr As HtmlTextWriter)
            RenderRowBegin(wr) '<tr>
            RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "") ' <td></td>
            RenderCellBegin(wr, "Forum_BreadCrumb_Top_Wrapper", "", "100%", "left", "bottom", "", "") ' <td>
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "", "0") ' <table>
            RenderRowBegin(wr) ' <tr>
            RenderCellBegin(wr, "", "", "100%", "", "", "2", "") ' <td> 

            Dim tempForumID As Integer
            If Not HttpContext.Current.Request.QueryString("forumid") Is Nothing Then
                tempForumID = Int32.Parse(HttpContext.Current.Request.QueryString("forumid"))
            End If
            Dim ChildGroupView As Boolean = False
            If CType(ForumControl.TabModuleSettings("groupid"), String) <> String.Empty Then
                ChildGroupView = True
            End If
            wr.Write(Utilities.ForumUtils.BreadCrumbs(TabID, ModuleID, ForumScope.Posts, objThread, objConfig, ChildGroupView))
            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </Tr>
            RenderRowBegin(wr) '<tr>

            RenderCellBegin(wr, "", "", "100%", "", "", "2", "") ' <td> 
            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </Tr>
            RenderRowBegin(wr) '<tr>

            RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "")
            RenderCellBegin(wr, "", "", "100%", "", "", "", "") ' <td> 
            RenderCellEnd(wr) ' </td>

            RenderRowEnd(wr) ' </Tr>
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </Td>
            RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "")
            RenderRowEnd(wr) ' </Tr>
        End Sub

        ''' <summary>
        ''' Renders the area directly above the post including: New Thread, prev/next
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks>
        ''' </remarks>
        Private Sub RenderTopThreadButtons(ByVal wr As HtmlTextWriter)
            Dim fSubject As String
            Dim url As String

            If PostCollection.Count > 0 Then
                Dim firstPost As PostInfo = CType(PostCollection(0), PostInfo)
                fSubject = String.Format("&nbsp;{0}", firstPost.Subject)
                ' filter bad words if required in forum settings
                If ForumControl.objConfig.FilterSubject Then
                    fSubject = Utilities.ForumUtils.FormatProhibitedWord(fSubject, firstPost.CreatedDate, PortalID)
                End If
            Else
                fSubject = ForumControl.LocalizedText("NoPost")
            End If

            RenderRowBegin(wr) '<tr>
            RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "")

            RenderCellBegin(wr, "", "", "100%", "left", "", "", "") '<td>
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "left", "", "0") ' <table>
            RenderRowBegin(wr) '<tr>
            RenderCellBegin(wr, "", "", "70%", "left", "middle", "", "")  '<td>
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "", "0") '<Table>            
            RenderRowBegin(wr) '<tr>

            RenderCellBegin(wr, "", "", "", "", "middle", "", "")   '<td>           

            ' new thread button
            'Remove LoggedOnUserID limitation if wishing to implement Anonymous Posting
            If (CurrentForumUser.UserID > 0) And (Not ForumID = -1) Then
                If Not objThread.ContainingForum.PublicPosting Then
                    If objSecurity.IsAllowedToStartRestrictedThread Then
                        RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "0") '<Table>            
                        RenderRowBegin(wr) '<tr>
                        url = Utilities.Links.NewThreadLink(TabID, ForumID, ModuleID)
                        RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 

                        If CurrentForumUser.IsBanned Then
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("NewThread"), "Forum_Link", False)
                        Else
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("NewThread"), "Forum_Link")
                        End If

                        RenderCellEnd(wr) ' </Td>

                        If CurrentForumUser.IsBanned Or (Not objSecurity.IsAllowedToPostRestrictedReply) Or (objThread.IsClosed) Then
                            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                            wr.Write("&nbsp;")
                            RenderCellEnd(wr) ' </Td>
                            RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link", False)
                            RenderCellEnd(wr) ' </Td>
                        Else
                            url = Utilities.Links.NewPostLink(TabID, ForumID, objThread.ThreadID, "reply", ModuleID)
                            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                            wr.Write("&nbsp;")
                            RenderCellEnd(wr) ' </Td>
                            RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link")
                            RenderCellEnd(wr) ' </Td>
                        End If

                        '[skeel] moved delete thread here
                        If CurrentForumUser.UserID > 0 AndAlso (objSecurity.IsForumModerator) Then

                            url = Utilities.Links.ThreadDeleteLink(TabID, ModuleID, ForumID, ThreadID, False)
                            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                            wr.Write("&nbsp;")
                            RenderCellEnd(wr) ' </Td>
                            RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("DeleteThread"), "Forum_Link")
                            RenderCellEnd(wr) ' </Td>
                        End If

                        RenderRowEnd(wr) ' </tr>
                        RenderTableEnd(wr) ' </table>
                    ElseIf objSecurity.IsAllowedToPostRestrictedReply Then
                        RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "0") '<Table>            
                        RenderRowBegin(wr) '<tr>

                        If CurrentForumUser.IsBanned Or objThread.IsClosed Then
                            url = Utilities.Links.NewPostLink(TabID, ForumID, objThread.ThreadID, "reply", ModuleID)

                            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                            wr.Write("&nbsp;")
                            RenderCellEnd(wr) ' </Td>
                            RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link", False)
                            RenderCellEnd(wr) ' </Td>
                        Else
                            url = Utilities.Links.NewPostLink(TabID, ForumID, objThread.ThreadID, "reply", ModuleID)
                            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                            wr.Write("&nbsp;")
                            RenderCellEnd(wr) ' </Td>
                            RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link")
                            RenderCellEnd(wr) ' </Td>
                        End If

                        RenderRowEnd(wr) ' </tr>
                        RenderTableEnd(wr) ' </table>
                    Else
                        ' user cannot start thread or make a reply
                        wr.Write("&nbsp;")
                    End If
                Else
                    ' no posting restrictions
                    RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "0") '<Table>            
                    RenderRowBegin(wr) '<tr>
                    url = Utilities.Links.NewThreadLink(TabID, ForumID, ModuleID)
                    RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 

                    If CurrentForumUser.IsBanned Then
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("NewThread"), "Forum_Link", False)
                    Else
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("NewThread"), "Forum_Link")
                    End If

                    RenderCellEnd(wr) ' </Td>

                    If CurrentForumUser.IsBanned Or objThread.IsClosed Then
                        RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                        wr.Write("&nbsp;")
                        RenderCellEnd(wr) ' </Td>
                        RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link", False)
                        RenderCellEnd(wr) ' </Td>
                    Else
                        url = Utilities.Links.NewPostLink(TabID, ForumID, objThread.ThreadID, "reply", ModuleID)
                        RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                        wr.Write("&nbsp;")
                        RenderCellEnd(wr) ' </Td>
                        RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link")
                        RenderCellEnd(wr) ' </Td>
                    End If

                    '[skeel] moved delete thread here
                    If CurrentForumUser.UserID > 0 AndAlso (objSecurity.IsForumModerator) Then
                        url = Utilities.Links.ThreadDeleteLink(TabID, ModuleID, ForumID, ThreadID, False)
                        RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                        wr.Write("&nbsp;")
                        RenderCellEnd(wr) ' </Td>
                        RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("DeleteThread"), "Forum_Link")
                        RenderCellEnd(wr) ' </Td>
                    End If

                    RenderRowEnd(wr) ' </tr>
                    RenderTableEnd(wr) ' </table>
                End If
            End If

            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </td>

            ' Thread navigation
            RenderCellBegin(wr, "", "", "30%", "right", "", "", "")  '<td> 
            RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "0") '<Table>            
            RenderRowBegin(wr) '<tr>
            Dim PreviousEnabled As Boolean = False
            Dim EnabledText As String = "Disabled"

            If Not (objThread.PreviousThreadID = 0) Then
                If Not (objThread.IsPinned) Then
                    PreviousEnabled = True
                    EnabledText = "Previous"
                End If
            End If

            If PreviousEnabled Then
                RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "", "", "")  ' <td> ' 
            Else
                RenderCellBegin(wr, "Forum_NavBarButtonDisabled", "", "", "", "", "", "")   ' <td> ' 
            End If
            RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "0") '<Table>            
            RenderRowBegin(wr) '<tr>

            url = Utilities.Links.ContainerViewThreadLink(TabID, ForumID, objThread.PreviousThreadID)

            RenderCellBegin(wr, "", "", "", "", "", "", "")  ' <td> ' 
            If PreviousEnabled Then
                RenderLinkButton(wr, url, ForumControl.LocalizedText("Previous"), "Forum_Link_Left")
            Else
                RenderDivBegin(wr, "", "Forum_NormalBold")
                wr.Write(ForumControl.LocalizedText("Previous"))
                RenderDivEnd(wr)
            End If
            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </td>    

            RenderCellBegin(wr, "", "", "", "", "", "", "")  ' <td> 
            wr.Write("&nbsp;")
            RenderCellEnd(wr) ' </td>

            Dim NextEnabled As Boolean = False
            Dim NextText As String = "Disabled"
            If Not (objThread.NextThreadID = 0) Then
                If Not (objThread.IsPinned = True) Then
                    NextEnabled = True
                    NextText = "Next"
                End If
            End If

            If NextEnabled Then
                RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "", "", "")  ' <td> '
            Else
                RenderCellBegin(wr, "Forum_NavBarButtonDisabled", "", "", "", "", "", "")   ' <td> '
            End If

            RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "0") '<Table>            
            RenderRowBegin(wr) '<tr>
            RenderCellBegin(wr, "", "", "", "", "", "", "")  ' <td> ' 

            If NextEnabled Then
                url = Utilities.Links.ContainerViewThreadLink(TabID, ForumID, objThread.NextThreadID)
                RenderLinkButton(wr, url, ForumControl.LocalizedText("Next"), "Forum_Link")
            Else
                RenderDivBegin(wr, "", "Forum_NormalBold")
                wr.Write(ForumControl.LocalizedText("Next"))
                RenderDivEnd(wr)
            End If
            RenderCellEnd(wr) ' </td>   
            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </td>

            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>

            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table> 
            RenderCellEnd(wr) ' </td>
            RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "")
            RenderRowEnd(wr) ' </tr>       
        End Sub

        ''' <summary>
        ''' This area is used to render all individual posts and the footer  as well as any poll related UI (by calling other methods)
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks></remarks>
        Private Sub RenderThread(ByVal wr As HtmlTextWriter, ByVal phase As String)

            'CP - Spacer Row between final post footer and bottom panel
            RenderRowBegin(wr) '<tr>
            RenderCapCell(wr, objConfig.GetThemeImageURL("height_spacer.gif"), "", "")
            RenderCellBegin(wr, "", "", "100%", "", "", "", "") '<td>
            RenderCellEnd(wr) ' </td> 
            RenderCapCell(wr, objConfig.GetThemeImageURL("height_spacer.gif"), "", "")
            RenderRowEnd(wr) ' </tr>
            'End spacer row

            ' Handle polls
            If objThread.ContainingForum.AllowPolls And objThread.PollID > 0 And CurrentForumUser.UserID > 0 Then
                RenderPoll(wr)
            End If

            ' Loop round rows in selected thread (These are rows w/ user avatar/alias, post body)

            RenderPosts(wr, phase)
            RenderFooter(wr)
        End Sub

        ''' <summary>
        ''' Renders a poll or the results (possibly thank you message if show results are off) if one is attached to a thread.
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks></remarks>
        Private Sub RenderPoll(ByVal wr As HtmlTextWriter)
            'new row
            RenderRowBegin(wr) '<tr>                
            RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "")

            ' middle master column (this will hold a table, the poll post will display here, then render a spacer row to seperate it from the other posts 
            RenderCellBegin(wr, "", "", "", "left", "middle", "", "")
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "", "0") ' <table> 
            RenderRowBegin(wr) ' <tr>
            RenderCellBegin(wr, "", "", "100%", "", "", "", "") ' <td>

            ' table to hold poll header
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "", "0") ' <table>
            RenderRowBegin(wr, "") ' <tr>
            RenderCapCell(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "Forum_HeaderCapLeft", "") ' <td><img /></td>
            RenderCellBegin(wr, "Forum_Header", "", "", "", "", "", "")    '<td>

            Dim cntPoll As New PollController
            Dim objPoll As New PollInfo
            objPoll = cntPoll.GetPoll(objThread.PollID)

            RenderDivBegin(wr, "", "bold") ' <span>
            'wr.Write("&nbsp;" & ForumControl.LocalizedText("Poll") & ": " & objPoll.Question)
            wr.Write("&nbsp;" & objPoll.Question)
            RenderDivEnd(wr) ' </span>
            RenderCellEnd(wr) ' </td> 
            RenderCapCell(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "Forum_HeaderCapRight", "") ' <td><img /></td>
            RenderRowEnd(wr) ' </tr>

            RenderTableEnd(wr) ' </table>

            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) ' </tr>

            RenderRowBegin(wr) ' <tr>
            RenderCellBegin(wr, "Forum_Avatar", "", "100%", "center", "middle", "", "")    '<td>

            Dim showPoll As Boolean = True
            If Not objPoll.PollClosed Then
                For Each objUserAnswer As UserAnswerInfo In objPoll.UserAnswers
                    If objUserAnswer.UserID = CurrentForumUser.UserID Then
                        showPoll = False
                        Exit For
                    End If
                Next
            End If

            If showPoll And (Not objPoll.PollClosed) Then
                ' if the user hasn't voted, show the the poll
                rblstPoll.RenderControl(wr)
                cmdVote.RenderControl(wr)
                ' Not implemented
                'If (LoggedOnUserID = ThreadInfo.StartedByUserID) Or (Security.IsForumModerator) Then
                '    'cmdViewResults.RenderControl(wr)
                'End If
            Else
                ' check to see if we are able to show user results
                If objPoll.ShowResults Or ((CurrentForumUser.UserID = objThread.StartedByUserID) Or (objSecurity.IsForumModerator)) Then
                    ' show results
                    RenderTableBegin(wr, "", "", "", "", "0", "0", "center", "middle", "")  ' <table> 

                    For Each objAnswer As AnswerInfo In objPoll.Answers
                        Dim cntAnswer As New AnswerController
                        objAnswer = cntAnswer.GetAnswer(objAnswer.AnswerID)

                        ' create a row representing results
                        RenderRowBegin(wr) ' <tr>
                        RenderCellBegin(wr, "", "", "", "left", "top", "", "")    '<td>

                        ' show answer
                        RenderDivBegin(wr, "", "Forum_Normal") ' <span>
                        wr.Write(objAnswer.Answer & "&nbsp;")
                        RenderDivEnd(wr) ' </span>
                        RenderCellEnd(wr) ' </td>

                        ' handle calculation
                        Dim Percentage As Double
                        If objPoll.TotalVotes = 0 Then
                            Percentage = 0
                        Else
                            Percentage = (objAnswer.AnswerCount / objPoll.TotalVotes) * 100
                        End If

                        Dim strVoteCount As String
                        strVoteCount = objAnswer.AnswerCount.ToString()
                        strVoteCount = strVoteCount + " " + Localization.GetString("Votes", objConfig.SharedResourceFile)

                        ' show image
                        RenderCellBegin(wr, "poll-bar-image-container", "", "", "left", "middle", "", "")    '<td>
                        If CType(Percentage, Integer) > 0 Then
                            RenderImage(wr, objConfig.GetThemeImageURL("poll_capleft.") & objConfig.ImageExtension, strVoteCount, "")
                            ' handle this biatch
                            Dim i As Integer = 0
                            For i = 0 To CType(Percentage, Integer)
                                RenderImage(wr, objConfig.GetThemeImageURL("poll_bar.") & objConfig.ImageExtension, strVoteCount, "")
                            Next
                            RenderImage(wr, objConfig.GetThemeImageURL("poll_capright.") & objConfig.ImageExtension, strVoteCount, "")
                        End If
                        wr.Write("&nbsp;")
                        RenderCellEnd(wr) ' </td>

                        ' show percentage
                        RenderCellBegin(wr, "poll-vote-percentage-container", "", "", "right", "top", "", "")    '<td>
                        RenderDivBegin(wr, "", "Forum_Normal") ' <span>
                        wr.Write(FormatNumber(Percentage, 2).ToString() & " %")
                        RenderDivEnd(wr) ' </span>
                        RenderCellEnd(wr) ' </td>
                        RenderRowEnd(wr) ' </tr>
                    Next

                    RenderRowBegin(wr) ' <tr>
                    RenderCellBegin(wr, "", "", "100%", "center", "middle", "3", "")       '<td>
                    RenderDivBegin(wr, "", "Forum_NormalBold") ' <span>
                    wr.RenderBeginTag(HtmlTextWriterTag.B)
                    wr.Write(Localization.GetString("TotalVotes", objConfig.SharedResourceFile) & " " & objPoll.TotalVotes.ToString())
                    wr.RenderEndTag()
                    RenderDivEnd(wr) ' </span>
                    RenderCellEnd(wr) ' </td>
                    RenderRowEnd(wr) ' </tr>

                    '' View Details Row (Not Implemented)
                    'RenderRowBegin(wr) ' <tr>
                    'RenderCellBegin(wr, "", "", "100%", "center", "middle", "3", "")    '<td>
                    'RenderSpanBegin(wr, "", "Forum_Normal") ' <span>
                    'wr.Write("Total Votes: " & objPoll.TotalVotes.ToString())
                    'RenderSpanEnd(wr) ' </span>
                    'RenderCellEnd(wr) ' </td>
                    'RenderRowEnd(wr) ' </tr>

                    RenderTableEnd(wr) ' </table>
                Else
                    RenderDivBegin(wr, "", "Forum_Normal") ' <span>
                    wr.Write(objPoll.TakenMessage)
                    RenderDivEnd(wr) ' </span>
                End If
            End If
            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr>

            RenderRowBegin(wr) '<tr> 
            RenderCellBegin(wr, "Forum_SpacerRow", "", "", "", "", "", "")  ' <td>
            RenderImage(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "", "")
            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </td> 
            RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "")

            RenderRowEnd(wr) ' </tr>
        End Sub

        ''' <summary>
        ''' posts make up all rows in between (fourth row to third to last row, numerous rows)
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks></remarks>
        Private Sub RenderPosts(ByVal wr As HtmlTextWriter, ByVal phase As String)
            ' use a counter to determine odd/even for alternating colors (via css)
            Dim intPostCount As Integer = 1
            Dim totalPostCount As Integer = PostCollection.Count
            Dim currentCount As Integer = 1

            RenderRowBegin(wr) '<tr>                
            RenderCapCell(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "", "") ' <td><img/></td>
            RenderCellBegin(wr, "", "", "100%", "", "top", "", "")  ' <td>
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "center", "", "0")    ' <table> 


            For Each Post As PostInfo In PostCollection
                Dim postCountIsEven As Boolean = ThreadIsEven(intPostCount)
                Me.RenderPost(wr, Post, postCountIsEven, phase)


                If (Post.ParentPostID = 0 And ModuleID <> PROPOSE_YOUR_TOPIC_MODULEID And Not CurrentForumUser.ViewDescending) Then
                    RenderRowBegin(wr) '<tr>  
                    RenderCellBegin(wr, "", "", "", "", "", "2", "")
                    wr.Write("<h2 class='feedback-label'>" & ForumControl.LocalizedText("Feedback") & " (" & objThread.TotalPosts - 1 & ")</h2>")
                    RenderCellEnd(wr)
                    RenderRowEnd(wr) '<tr>    
                End If



                ' spacer row should be displayed in flat view only
                If Not currentCount = totalPostCount Then
                    RenderSpacerRow(wr)
                    currentCount += 1
                End If
                intPostCount += 1

                ' inject Advertisment into forum post list
                If (objConfig.AdsAfterFirstPost AndAlso intPostCount = 2) OrElse ((objConfig.AddAdverAfterPostNo <> 0) AndAlso ((intPostCount - 1) Mod objConfig.AddAdverAfterPostNo = 0)) Then
                    Me.RenderAdvertisementPost(wr)
                    RenderSpacerRow(wr)
                End If
            Next
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </td> 
            RenderCapCell(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "", "") ' <td><img/></td>
            RenderRowEnd(wr) ' </tr>
        End Sub

        ''' <summary>
        ''' Renders the entire table structure of a single post
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <param name="Post"></param>
        ''' <param name="PostCountIsEven"></param>
        ''' <remarks>
        ''' </remarks>
        Private Sub RenderPost(ByVal wr As HtmlTextWriter, ByVal Post As PostInfo, ByVal PostCountIsEven As Boolean, ByVal phase As String)
            Dim authorCellClass As String
            Dim bodyCellClass As String

            If (Post.ParentPostID = 0) Then
                If PostCountIsEven Then
                    authorCellClass = "Forum_Avatar_First"
                    bodyCellClass = "Forum_PostBody_Container_First"
                Else
                    authorCellClass = "Forum_Avatar_Alt_First"
                    bodyCellClass = "Forum_PostBody_Container_Alt_First"
                End If
            Else
                ' these classes to set bg color of cells
                If PostCountIsEven Then
                    authorCellClass = "Forum_Avatar"
                    bodyCellClass = "Forum_PostBody_Container"
                Else
                    authorCellClass = "Forum_Avatar_Alt"
                    bodyCellClass = "Forum_PostBody_Container_Alt"
                End If
            End If





            'Add per post header - better UI can add more info
            Dim strPostedDate As String = String.Empty
            Dim newpost As Boolean
            strPostedDate = Utilities.ForumUtils.GetCreatedDateInfo(Post.CreatedDate, objConfig, "").ToString

            RenderRowBegin(wr) ' <tr>
            RenderCellBegin(wr, "", "", "100%", "left", "middle", "2", "")  '<td>
            '[skeel] Check if first new post and add bookmark used for navigation
            If HttpContext.Current.Request IsNot Nothing Then
                If HttpContext.Current.Request.IsAuthenticated Then
                    If Post.NewPost(CurrentForumUser.UserID) Then
                        RenderPostBookmark(wr, "unread")
                        newpost = True
                    End If
                End If
            End If

            '[skeel] add Bookmark to post
            'RenderPostBookmark(wr, "p" & CStr(Post.PostID))
            RenderPostBookmark(wr, CStr(Post.PostID))

            ' ATC BIG REMOVAL START

            ''Make table to hold per post header
            'RenderTableBegin(wr, "", "", "", "100%", "0", "0", "center", "middle", "0")  ' <table> 
            'RenderRowBegin(wr) ' <tr>
            'RenderCellBegin(wr, "", "", "100%", "left", "middle", "", "")   '<td>
            'RenderTableBegin(wr, "", "", "", "100%", "0", "0", "center", "middle", "0")  ' <table> 

            'RenderRowBegin(wr) ' <tr>
            'RenderCapCell(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "Forum_HeaderCapLeft", "") ' <td><img /></td>


            '' start post status image
            'RenderCellBegin(wr, "Forum_Header", "", "1%", "left", "", "", "") '<td>
            '' display "new" image if this post is new since last time user visited the thread
            'If HttpContext.Current.Request IsNot Nothing Then
            '    If HttpContext.Current.Request.IsAuthenticated Then
            '        If Post.NewPost(CurrentForumUser.UserID) Then
            '            RenderImage(wr, objConfig.GetThemeImageURL("s_new.") & objConfig.ImageExtension, ForumControl.LocalizedText("UnreadPost"), "")
            '        Else
            '            RenderImage(wr, objConfig.GetThemeImageURL("s_old.") & objConfig.ImageExtension, ForumControl.LocalizedText("ReadPost"), "")
            '        End If
            '    Else
            '        RenderImage(wr, objConfig.GetThemeImageURL("s_new.") & objConfig.ImageExtension, ForumControl.LocalizedText("UnreadPost"), "")
            '    End If
            'Else
            '    RenderImage(wr, objConfig.GetThemeImageURL("s_new.") & objConfig.ImageExtension, ForumControl.LocalizedText("UnreadPost"), "")
            'End If
            'RenderCellEnd(wr) ' </td> 

            'RenderCellBegin(wr, "Forum_Header", "", "89%", "left", "", "", "")      '<td>
            ''RenderDivBegin(wr, "", "Forum_PostDate") ' <span>
            '' wr.Write(strPostedDate)
            '' RenderDivEnd(wr) ' </span>
            'RenderCellEnd(wr) ' </td> 

            ''RenderCellBegin(wr, "Forum_Header", "", "", "right", "", "", "")       '<td>
            '' Start ATC

            ''If Not UserHasVotedPost(Post.PostID, ProfileUserID) Then

            ''    If hsPostThumbs.ContainsKey(Post.PostID) Then
            ''        'RenderCellEnd(wr) ' </td> 
            ''        RenderCellBegin(wr, "Forum_Header", "", "", "right", "", "", "")
            ''        cmdPostThumbs = CType(hsPostThumbs(Post.PostID), LinkButton)
            ''        cmdPostThumbs.CommandArgument = Post.PostID.ToString
            ''        cmdPostThumbs.RenderControl(wr)
            ''        'wr.Write("Agrees")
            ''        'wr.Write("&nbsp;")
            ''        RenderCellEnd(wr) ' </td> 
            ''    End If

            ''    If hsPostThumbsDown.ContainsKey(Post.PostID) Then
            ''        'RenderCellBegin(wr, "Forum_Header", "", "", "right", "", "", "")
            ''        RenderCellBegin(wr, "Forum_Header", "", "", "right", "", "", "")
            ''        cmdPostThumbs = CType(hsPostThumbsDown(Post.PostID), LinkButton)
            ''        cmdPostThumbs.CommandArgument = Post.PostID.ToString
            ''        cmdPostThumbs.RenderControl(wr)
            ''        RenderCellEnd(wr) ' </td> 
            ''    End If



            ''End If
            ''RenderCellBegin(wr, "Forum_Header", "", "", "right", "", "", "")    '<td>
            ' '' Retrieving post thumbs values
            ''Dim dr As IDataReader = Nothing
            ''Try
            ''    If (Post.ParentPostID <> 0) Then
            ''        Dim thumbsUp As Integer
            ''        Dim thumbsDown As Integer
            ''        dr = DotNetNuke.Modules.Forum.DataProvider.Instance().PostGetThumbs(Post.PostID)
            ''        If dr.Read Then
            ''            thumbsUp = Convert.ToInt32(dr("thumbsUp"))
            ''            thumbsDown = Convert.ToInt32(dr("thumbsDown"))

            ''            wr.Write("Thumbs Up:" & thumbsUp & "<br/>")
            ''            wr.Write("Thumbs Down:" & thumbsDown)
            ''        End If
            ''    End If





            ''Finally
            ''    If dr IsNot Nothing Then
            ''        dr.Close()
            ''    End If
            ''End Try

            ''RenderCellEnd(wr) ' </td> 
            '' End ATC
            'RenderCellBegin(wr, "Forum_Header", "", "", "right", "", "", "")    '<td>

            '' if the user is the original author or a moderator AND this is the original post
            'If ((CurrentForumUser.UserID = Post.ParentThread.StartedByUserID) Or (objSecurity.IsForumModerator)) And Post.ParentPostID = 0 Then
            '    If Post.ParentThread.ThreadStatus = ThreadStatus.Poll Then
            '        ddlThreadStatus.Enabled = False
            '    End If
            '    ddlThreadStatus.RenderControl(wr)
            '    'wr.Write("&nbsp;")
            'Else
            '    ' this is either not the original post or the user is not the author or a moderator
            '    ' If the thread is answered AND this is the post accepted as the answer
            '    If Post.ParentThread.ThreadStatus = ThreadStatus.Answered And (Post.ParentThread.AnswerPostID = Post.PostID) And objThread.ContainingForum.EnableForumsThreadStatus Then
            '        RenderDivBegin(wr, "", "Forum_AnswerText") ' <span>
            '        wr.Write(ForumControl.LocalizedText("AcceptedAnswer"))
            '        wr.Write("&nbsp;")
            '        RenderDivEnd(wr) ' </span>
            '        ' If the thread is NOT answered AND this user started the post or is a moderator of some sort
            '    ElseIf ((CurrentForumUser.UserID = Post.ParentThread.StartedByUserID) Or (objSecurity.IsForumModerator)) And (Post.ParentThread.ThreadStatus = ThreadStatus.Unanswered) And objThread.ContainingForum.EnableForumsThreadStatus Then
            '        ' Select the proper command argument (set before rendering)
            '        If hsThreadAnswers.ContainsKey(Post.PostID) Then
            '            cmdThreadAnswer = CType(hsThreadAnswers(Post.PostID), LinkButton)
            '            cmdThreadAnswer.CommandArgument = Post.PostID.ToString
            '            cmdThreadAnswer.RenderControl(wr)
            '            wr.Write("&nbsp;")
            '            wr.Write("&nbsp;")
            '        End If
            '        ' all that can be left worth displaying is if the post is the original, show the status icon
            '    Else
            '        wr.Write("&nbsp;")
            '    End If
            'End If
            'RenderCellEnd(wr) ' </td> 

            'RenderCapCell(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "Forum_HeaderCapRight", "")

            'RenderRowEnd(wr) ' </tr>
            'RenderTableEnd(wr) ' </table>
            'RenderCellEnd(wr) ' </td> 

            'RenderRowEnd(wr) ' </tr>
            'RenderTableEnd(wr) ' </table>


            ' ATC BIG REMOVAL END

            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) ' </tr>

            RenderRowBegin(wr) ' <tr>

            ' Author area
            RenderCellBegin(wr, authorCellClass, "", "9%", "center", "top", "1", "1")   ' <td>
            Me.RenderPostAuthor(wr, Post, PostCountIsEven)
            RenderCellEnd(wr) ' </td> 

            ' post area
            ' cell for post details (subject, buttons)
            RenderCellBegin(wr, bodyCellClass, "100%", "91%", "left", "top", "", "")      '<td>


            ' <span>
            ' wr.Write(strPostedDate)
            ' </span>
            'If (Post.ParentPostID <> 0) Then
            RenderDivBegin(wr, "", "Forum_PostDate")

            ' Dim parameters As String()
            ' parameters = {"user=" & Post.Author.UserID}
            Dim url As String

            '   Dim serv As New Ourspace_Utilities.View
            Dim util As New Ourspace_Utilities.View
            Dim isFacebook As New Boolean
            isFacebook = False
            '  serv.GetOurSpaceUserImgUrl(HttpContext.Current.Server, CurrentForumUser.UserID)
            If (TabID = 259 Or TabID = 271) Then
                isFacebook = True
            End If
            url = util.GetUserProfileLink(Post.Author.UserID, CultureInfo.CurrentUICulture.Name, isFacebook)

            '.GetOurSpaceUserImgUrl(HttpContext.Current.Server, author.UserID), author.FirstName & " " & author.LastName, "")







            'url = DotNetNuke.Common.Globals.NavigateURL(71, "", parameters)
            wr.Write(ForumControl.LocalizedText("ByCap") & " <a href='" & url & "' class='bold-link'>" & Post.Author.FirstName & " " & Post.Author.LastName & "</a> ")
            If (Post.Author.IsInRole("Collaborator")) Then
                wr.Write("<img style='vertical-align: text-bottom;' src='http://www.joinourspace.eu/images/star.png' title='" & ForumControl.LocalizedText("Collaborator") & "'/>&nbsp;")
            End If
            If (Post.Author.IsInRole("DecisionMaker")) Then
                wr.Write("<img style='vertical-align: text-bottom;' src='http://www.joinourspace.eu/images/library.png' title='" & ForumControl.LocalizedText("DecisionMaker") & "'/>&nbsp;")
            End If

            wr.Write(ForumControl.LocalizedText("on") & " " & Post.CreatedDate.ToString("dd.MM.yyyy, HH:mm"))




            cmdSwitchFeedbackProposal = CType(hsSwitchFeedbackProposal(Post.PostID), LinkButton)
            'cmdSwitchFeedbackProposal.Text = "Switch"
            cmdSwitchFeedbackProposal.CommandArgument = Post.PostID.ToString

            ' wr.Write("down:" & thumbsDown & " ")









            If (Post.ParentPostID <> 0) Then
                If (Post.IsSolution) Then
                    wr.Write("<div class='proposalIndicator'><img style='vertical-align: text-bottom;' src='http://www.joinourspace.eu/images/proposal.png' title='" & ForumControl.LocalizedText("Proposal") & "'/>&nbsp;" & ForumControl.LocalizedText("Proposal"))

                    cmdSwitchFeedbackProposal.Text = ForumControl.LocalizedText("SwitchToFeedback")
                    wr.Write("</div>")
                Else
                    cmdSwitchFeedbackProposal.Text = ForumControl.LocalizedText("SwitchToProposal")

                End If

                If (CurrentForumUser.IsInRole("Collaborator") Or CurrentForumUser.IsInRole("Administrator")) Then
                    wr.Write("<div style='float:right;' class='proposalIndicator'>")

                    cmdSwitchFeedbackProposal.RenderControl(wr)
                    wr.Write("</div>")
                End If


            End If

            RenderDivEnd(wr)
            'End If

            RenderPostHeader(wr, Post, PostCountIsEven, phase)
            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr>
        End Sub

        ''' <summary>
        ''' Builds the left cell for RenderPost (author, rank, avatar area)
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <param name="Post"></param>
        ''' <param name="PostCountIsEven"></param>
        ''' <remarks>
        ''' </remarks>
        Private Sub RenderPostAuthor(ByVal wr As HtmlTextWriter, ByVal Post As PostInfo, ByVal PostCountIsEven As Boolean)
            If Not Post Is Nothing Then
                Dim author As ForumUserInfo = Post.Author
                Dim authorOnline As Boolean = (author.EnableOnlineStatus AndAlso author.IsOnline AndAlso (ForumControl.objConfig.EnableUsersOnline))
                Dim url As String

                ' table to display integrated media, user alias, poster rank, avatar, homepage, and number of posts.
                RenderTableBegin(wr, "", "Forum_PostAuthorTable", "", "100%", "0", "0", "", "", "")

                ' row to display user alias and online status
                RenderRowBegin(wr) '<tr> 

                'link to user profile, always display in both views
                If Not objConfig.EnableExternalProfile Then
                    url = author.UserCoreProfileLink
                Else
                    url = Utilities.Links.UserExternalProfileLink(author.UserID, objConfig.ExternalProfileParam, objConfig.ExternalProfilePage, objConfig.ExternalProfileUsername, author.Username)
                End If

                RenderCellBegin(wr, "", "", "", "", "middle", "", "") ' <td>

                ' display user online status
                If objConfig.EnableUsersOnline Then
                    'RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "") ' <table>
                    'RenderRowBegin(wr) ' <tr>
                    'RenderCellBegin(wr, "", "", "", "", "middle", "", "")   ' <td> 
                    'If authorOnline Then
                    '    RenderImage(wr, objConfig.GetThemeImageURL("s_online.") & objConfig.ImageExtension, ForumControl.LocalizedText("imgOnline"), "")
                    'Else
                    '    RenderImage(wr, objConfig.GetThemeImageURL("s_offline.") & objConfig.ImageExtension, ForumControl.LocalizedText("imgOffline"), "")
                    'End If
                    'RenderCellEnd(wr) ' </td>

                    'RenderCellBegin(wr, "", "", "", "", "middle", "", "")    ' <td>
                    'wr.Write("&nbsp;")
                    'RenderTitleLinkButton(wr, url, author.SiteAlias, "Forum_Profile", ForumControl.LocalizedText("ViewProfile"))
                    'RenderCellEnd(wr) ' </td>

                    'Dim objSecurity2 As New Forum.ModuleSecurity(ModuleID, TabID, -1, CurrentForumUser.UserID)

                    'If objSecurity2.IsModerator Then
                    '    RenderCellBegin(wr, "", "", "", "", "middle", "", "")    ' <td>
                    '    wr.Write("&nbsp;")
                    '    RenderImageButton(wr, Utilities.Links.UCP_AdminLinks(TabID, ModuleID, author.UserID, UserAjaxControl.Profile), objConfig.GetThemeImageURL("s_edit.") & objConfig.ImageExtension, ForumControl.LocalizedText("EditProfile"), "")
                    '    RenderCellEnd(wr) ' </td>
                    'End If

                    'RenderRowEnd(wr) ' </tr>
                    'RenderTableEnd(wr) ' </table>
                Else
                    ' ATC removed START
                    'RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "") ' <table>
                    'RenderRowBegin(wr) ' <tr>
                    'RenderCellBegin(wr, "", "", "", "", "middle", "", "")   ' <td> 
                    'RenderTitleLinkButton(wr, url, author.SiteAlias, "Forum_Profile", ForumControl.LocalizedText("ViewProfile"))
                    'RenderCellEnd(wr) ' </td>

                    'Dim objSecurity2 As New Forum.ModuleSecurity(ModuleID, TabID, -1, CurrentForumUser.UserID)

                    'If objSecurity2.IsModerator Then
                    '    RenderCellBegin(wr, "", "", "", "", "middle", "", "")    ' <td>
                    '    wr.Write("&nbsp;")
                    '    RenderImageButton(wr, Utilities.Links.UCP_AdminLinks(TabID, ModuleID, author.UserID, UserAjaxControl.Profile), objConfig.GetThemeImageURL("s_edit.") & objConfig.ImageExtension, ForumControl.LocalizedText("EditProfile"), "")
                    '    RenderCellEnd(wr) ' </td>
                    'End If

                    'RenderRowEnd(wr) ' </tr>
                    'RenderTableEnd(wr) ' </table>
                    ' ATC removed END
                End If

                RenderCellEnd(wr) ' </td>
                RenderRowEnd(wr) ' </tr> (end user alias/online)  

                ' display user ranking 
                If (objConfig.Ranking) Then
                    Dim authorRank As PosterRank = Utilities.ForumUtils.GetRank(author, ForumControl.objConfig)
                    Dim rankImage As String = String.Format("Rank_{0}." & objConfig.ImageExtension, CType(authorRank, Integer).ToString)
                    Dim rankURL As String = objConfig.GetThemeImageURL(rankImage)
                    Dim RankTitle As String = Utilities.ForumUtils.GetRankTitle(authorRank, objConfig)

                    RenderRowBegin(wr) ' <tr> (start ranking row)
                    RenderCellBegin(wr, "", "", "", "", "top", "", "") ' <td>
                    ' ATC removed ranking image START
                    'If objConfig.EnableRankingImage Then
                    '    RenderImage(wr, rankURL, RankTitle, "")
                    'Else
                    '    RenderDivBegin(wr, "", "Forum_NormalSmall")
                    '    wr.Write(RankTitle)
                    '    RenderDivEnd(wr)
                    'End If
                    ' ATC removed ranking image END
                    RenderCellEnd(wr) ' </td>
                    RenderRowEnd(wr) ' </tr>
                End If

                ' display user avatar
                ' If objConfig.EnableUserAvatar AndAlso (String.IsNullOrEmpty(author.AvatarComplete) = False) Then
                RenderRowBegin(wr) ' <tr> (start avatar row)
                RenderCellBegin(wr, "Forum_UserAvatar", "", "", "", "top", "", "") ' <td>
                'wr.Write("<br />")
                'If objConfig.EnableProfileAvatar And author.UserID > 0 Then
                '    If Not author.IsSuperUser Then
                '        Dim WebVisibility As UserVisibilityMode
                '        WebVisibility = author.Profile.ProfileProperties(objConfig.AvatarProfilePropName).Visibility

                '        Select Case WebVisibility
                '            Case UserVisibilityMode.AdminOnly
                '                If objSecurity.IsForumAdmin Then
                '                    RenderProfileAvatar(author, wr)
                '                End If
                '            Case UserVisibilityMode.AllUsers
                '                RenderProfileAvatar(author, wr)
                '            Case UserVisibilityMode.MembersOnly
                '                If CurrentForumUser.UserID > 0 Then
                '                    RenderProfileAvatar(author, wr)
                '                End If
                '        End Select
                '    End If
                'Else
                '    If author.UserID > 0 Then
                '        RenderImage(wr, author.AvatarComplete, author.SiteAlias & "'s " & ForumControl.LocalizedText("Avatar"), "")
                '    End If
                'End If






                '   Dim serv As New Ourspace_Utilities.View
                Dim util As New Ourspace_Utilities.View
                '  serv.GetOurSpaceUserImgUrl(HttpContext.Current.Server, CurrentForumUser.UserID)
                RenderImage(wr, util.GetOurSpaceUserImgUrl(HttpContext.Current.Server, author.UserID), author.FirstName & " " & author.LastName, "")
                'RenderImage(wr, GetOurSpaceUserImage(author.UserID), CurrentForumUser.DisplayName, "")



                RenderCellEnd(wr) ' </td>
                RenderRowEnd(wr) ' </tr>
                'End If

                ' display system avatars (ie. DNN Core avatar)
                If objConfig.EnableSystemAvatar AndAlso (Not author.SystemAvatars = String.Empty) Then
                    Dim SystemAvatar As String
                    For Each SystemAvatar In author.SystemAvatarsComplete.Trim(";"c).Split(";"c)
                        If SystemAvatar.Length > 0 AndAlso (Not SystemAvatar.ToLower = "standard") Then
                            Dim SystemAvatarUrl As String = SystemAvatar
                            RenderRowBegin(wr) ' <tr> (start system avatar row) 
                            RenderCellBegin(wr, "Forum_NormalSmall", "", "", "", "top", "", "") ' <td>
                            wr.Write("<br />")
                            RenderImage(wr, SystemAvatarUrl, author.SiteAlias & "'s " & ForumControl.LocalizedText("Avatar"), "")
                            RenderCellEnd(wr) ' </td>
                            RenderRowEnd(wr) ' </tr>
                        End If
                    Next

                End If

                'Now for RoleBased Avatars
                If objConfig.EnableRoleAvatar AndAlso (Not author.RoleAvatar = ";") Then
                    Dim RoleAvatar As String
                    For Each RoleAvatar In author.RoleAvatarComplete.Trim(";"c).Split(";"c)
                        If RoleAvatar.Length > 0 AndAlso (Not RoleAvatar.ToLower = "standard") Then
                            Dim RoleAvatarUrl As String = RoleAvatar
                            RenderRowBegin(wr) ' <tr> (start system avatar row) 
                            RenderCellBegin(wr, "Forum_NormalSmall", "", "", "", "top", "", "") ' <td>
                            wr.Write("<br />")
                            RenderImage(wr, RoleAvatarUrl, author.SiteAlias & "'s " & ForumControl.LocalizedText("Avatar"), "")
                            RenderCellEnd(wr) ' </td>
                            RenderRowEnd(wr) ' </tr>
                        End If
                    Next
                End If

                'Author information
                RenderRowBegin(wr) ' <tr> 
                RenderCellBegin(wr, "Forum_NormalSmall", "", "", "", "top", "", "") ' <td>

                'Homepage
                If author.UserID > 0 Then
                    Dim WebSiteVisibility As UserVisibilityMode
                    WebSiteVisibility = author.Profile.ProfileProperties("Website").Visibility

                    Select Case WebSiteVisibility
                        Case UserVisibilityMode.AdminOnly
                            If objSecurity.IsForumAdmin Then
                                RenderWebSiteLink(author, wr)
                            End If
                        Case UserVisibilityMode.AllUsers
                            RenderWebSiteLink(author, wr)
                        Case UserVisibilityMode.MembersOnly
                            If CurrentForumUser.UserID > 0 Then
                                RenderWebSiteLink(author, wr)
                            End If
                    End Select

                    'Region
                    Dim CountryVisibility As UserVisibilityMode
                    CountryVisibility = author.Profile.ProfileProperties("Country").Visibility

                    Select Case CountryVisibility
                        Case UserVisibilityMode.AdminOnly
                            If objSecurity.IsForumAdmin Then
                                RenderCountry(author, wr)
                            End If
                        Case UserVisibilityMode.AllUsers
                            RenderCountry(author, wr)
                        Case UserVisibilityMode.MembersOnly
                            If CurrentForumUser.UserID > 0 Then
                                RenderCountry(author, wr)
                            End If
                    End Select
                End If

                'Name

                'wr.Write("<div class='author-name'>" + author.DisplayName + "</div>")
                'Joined
                Dim strJoinedDate As String
                Dim displayCreatedDate As DateTime = Utilities.ForumUtils.ConvertTimeZone(CType(author.Membership.CreatedDate, DateTime), objConfig)
                strJoinedDate = ForumControl.LocalizedText("Joined") & ": " & displayCreatedDate.ToShortDateString
                'wr.Write(strJoinedDate)

                'Post count
                'RenderDivBegin(wr, "spAuthorPostCount", "Forum_NormalSmall")
                'wr.Write(ForumControl.LocalizedText("PostCount").Replace("[PostCount]", author.PostCount.ToString))
                'RenderDivEnd(wr)

                RenderCellEnd(wr) ' </td>
                RenderRowEnd(wr) ' </tr>
            End If

            RenderTableEnd(wr) ' </table>  (End of user avatar/alias table, close td next)
        End Sub

        ''' <summary>
        ''' Renders the user's profile avatar. 
        ''' </summary>
        ''' <param name="author"></param>
        ''' <param name="wr"></param>
        ''' <remarks></remarks>
        Private Sub RenderProfileAvatar(ByVal author As ForumUserInfo, ByVal wr As HtmlTextWriter)
            ' This needs to be rendered w/ specified size
            If objConfig.EnableProfileUserFolders Then
                ' The link click below (duplicated from core profile page) presents some serious issues under volume. 
                'imgUserProfileAvatar.ImageUrl = DotNetNuke.Common.Globals.LinkClick("fileid=" & author.AvatarFile.FileId.ToString(), PortalSettings.ActiveTab.TabID, Null.NullInteger)
                If author.AvatarCoreFile IsNot Nothing Then
                    Dim imgUserProfileAvatar As New Image

                    imgUserProfileAvatar.ImageUrl = author.AvatarComplete
                    DotNetNuke.Web.UI.Utilities.CreateThumbnail(author.AvatarCoreFile, imgUserProfileAvatar, objConfig.UserAvatarWidth, objConfig.UserAvatarHeight)

                    imgUserProfileAvatar.RenderControl(wr)
                    imgUserProfileAvatar.Visible = True

                End If
            Else
                ' If we are here, file stored as name and not id (in UserProfile table).
                'CP: NOTE: Telerik conversion
                ' Dim rbiProfileAvatar As New Telerik.Web.UI.RadBinaryImage
                Dim rbiProfileAvatar As New DotNetNuke.Wrapper.UI.WebControls.DnnBinaryImage
                rbiProfileAvatar.Width = objConfig.UserAvatarWidth
                rbiProfileAvatar.Height = objConfig.UserAvatarHeight
                rbiProfileAvatar.ImageUrl = author.AvatarComplete

                rbiProfileAvatar.RenderControl(wr)
            End If

            ' Below is for use when no Telerik integration is going on. (Uncomment line below, comment out lines above)
            'RenderImage(wr, author.AvatarComplete, author.SiteAlias & "'s " & ForumControl.LocalizedText("Avatar"), "", objConfig.UserAvatarWidth.ToString(), objConfig.UserAvatarHeight.ToString())
        End Sub

        ''' <summary>
        ''' Renders the user's website (as a link). 
        ''' </summary>
        ''' <param name="author"></param>
        ''' <param name="wr"></param>
        ''' <remarks></remarks>
        Private Sub RenderWebSiteLink(ByVal author As ForumUserInfo, ByVal wr As HtmlTextWriter)
            If Len(author.UserWebsite) > 0 Then
                wr.Write("<br />")
                RenderLinkButton(wr, author.UserWebsite, Replace(author.UserWebsite, "http://", ""), "Forum_Profile", "", True, objConfig.NoFollowWeb)
            End If
        End Sub

        ''' <summary>
        ''' Renders the user's country. 
        ''' </summary>
        ''' <param name="author"></param>
        ''' <param name="wr"></param>
        ''' <remarks></remarks>
        Private Sub RenderCountry(ByVal author As ForumUserInfo, ByVal wr As HtmlTextWriter)
            If objConfig.DisplayPosterRegion And Len(author.Profile.Region) > 0 Then
                wr.Write("<br />" & ForumControl.LocalizedText("Region") & ": " & author.Profile.Region)
            End If
        End Sub

        ''' <summary>
        ''' Builds the post details: subject, user location, edited, created date
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <param name="Post"></param>
        ''' <param name="PostCountIsEven"></param>
        ''' <remarks></remarks>
        Private Sub RenderPostHeader(ByVal wr As HtmlTextWriter, ByVal Post As PostInfo, ByVal PostCountIsEven As Boolean, ByVal phase As String)
            Dim detailCellClass As String = String.Empty
            Dim buttonCellClass As String = String.Empty
            Dim strSubject As String = String.Empty
            Dim strCreatedDate As String = String.Empty
            Dim strAuthorLocation As String = String.Empty
            Dim url As String = String.Empty

            If PostCountIsEven Then
                detailCellClass = "Forum_PostDetails"
                buttonCellClass = "Forum_PostButtons"
            Else
                detailCellClass = "Forum_PostDetails_Alt"
                buttonCellClass = "Forum_PostButtons_Alt"
            End If

            If ForumControl.objConfig.FilterSubject Then
                strSubject = Utilities.ForumUtils.FormatProhibitedWord(Post.Subject, Post.CreatedDate, PortalID)
            Else
                strSubject = Post.Subject
            End If

            'CP - Possible change for foreign culture date displays
            strCreatedDate = ForumControl.LocalizedText("PostedDateTime")
            Dim displayCreatedDate As DateTime = Utilities.ForumUtils.ConvertTimeZone(Post.CreatedDate, objConfig)
            strCreatedDate = strCreatedDate.Replace("[CreatedDate]", displayCreatedDate.ToString("dd MMM yy"))
            strCreatedDate = strCreatedDate.Replace("[PostTime]", displayCreatedDate.ToString("t"))

            ' display poster location 
            If (Not objConfig.DisplayPosterLocation = ShowPosterLocation.None) Then
                If ((objConfig.DisplayPosterLocation = ShowPosterLocation.ToAdmin) AndAlso (objSecurity.IsForumAdmin)) OrElse (objConfig.DisplayPosterLocation = ShowPosterLocation.ToAll) Then
                    If (Not Post.RemoteAddr.Length = 0) AndAlso (Not Post.RemoteAddr = "127.0.0.1") AndAlso (Not Post.RemoteAddr = "::1") Then
                        strAuthorLocation = String.Format("&nbsp;({0})", Utilities.ForumUtils.LookupCountry(Post.RemoteAddr))
                        ' This will show the ip in italics (This should only show to moderators) 
                        If objSecurity.IsForumModerator Then
                            strAuthorLocation = strAuthorLocation & "<EM> (" & Post.RemoteAddr & ")</EM>"
                        End If
                    End If
                End If
            End If
            'RenderTableBegin(wr, Post.PostID.ToString, "", "100%", "100%", "0", "0", "", "", "0") ' <table>
            RenderTableBegin(wr, "", "", "100%", "100%", "0", "0", "", "", "0") ' <table>
            RenderRowBegin(wr) ' <tr>


            RenderCellBegin(wr, "", "", "100%", "", "", "", "") ' <td>

            'ATC start
            ' 
            If (Post.ParentPostID = 0) Then

                RenderTableBegin(wr, "", "Parent_Post_Header", "", "100%", "0", "0", "", "", "0") ' <table>



            Else
                RenderTableBegin(wr, "", "Post_Header", "", "100%", "0", "0", "", "", "0") ' <table>
            End If
            ' ATC End
            'RenderRowBegin(wr) ' <tr>

            'RenderCellBegin(wr, detailCellClass, "", "100%", "left", "top", "", "") ' <td>

            ''[skeel] Subject now works as a direct link to a specific post!
            ''RenderDivBegin(wr, "spCreatedDate", "Forum_Normal") ' <span>

            ' '' Regular Forum diplayed thread title in all posts, design only displays it in the first one


            ''wr.Write("&nbsp;")
            ''wr.Write(strAuthorLocation)
            ' '' display edited tag if post has been modified
            ''If (Post.UpdatedByUser > 0) Then
            ''    ' if the person who edited the post is a moderator and hide mod edits is enabled, we don't want to show edit details.
            ''    'CP - Impersonate
            ''    Dim objPosterSecurity As New ModuleSecurity(ModuleID, TabID, ForumID, CurrentForumUser.UserID)
            ''    If Not (objConfig.HideModEdits And objPosterSecurity.IsForumModerator) Then
            ''        wr.Write("&nbsp;")
            ''        ' ATC removing edited icon
            ''        'RenderImage(wr, objConfig.GetThemeImageURL("s_edit.") & objConfig.ImageExtension, String.Format(ForumControl.LocalizedText("ModifiedBy") & " {0} {1}", Post.LastModifiedAuthor.SiteAlias, " " & ForumControl.LocalizedText("on") & " " & Post.UpdatedDate.ToString), "")
            ''    End If
            ''End If

            ''RenderDivEnd(wr) ' </span> 
            '' TODO
            'If (Post.ParentPostID = 0) Then
            '    RenderDivBegin(wr, "", "Forum_NormalTEST") ' <span>
            '    Dim dr As IDataReader = Nothing
            '    Dim phase As String = ""
            '    dr = DotNetNuke.Modules.Forum.DataProvider.Instance().ThreadGetPhase(ThreadID)

            '    RenderTableBegin(wr, "", "Forum_ThreadDetailsTable", "", "100%", "0", "0", "", "", "0") ' <table>
            '    RenderRowBegin(wr) ' <tr>
            '    RenderCellBegin(wr, "", "", "", "", "top", "", "") ' <td>



            '    'here we add participants and posts
            '    RenderTableBegin(wr, 0, 0, "")
            '    RenderRowBegin(wr)

            '    RenderCellBegin(wr, "", "", "", "", "", "", "")

            '    Dim cntForum As New ForumController
            '    Dim forum As New ForumInfo
            '    forum = cntForum.GetForumItemCache(_objThread.ForumID)
            '    ForumControl.LocalizedText(forum.Name)

            '    wr.Write(ForumControl.LocalizedText("Posts") & ": <span class='color'>" & _objThread.TotalPosts & "</span>")
            '    wr.Write("&nbsp;&nbsp;&nbsp;")
            '    wr.Write(ForumControl.LocalizedText("Topic") & ": <span class='color'>" & forum.Name & "</span>")
            '    RenderCellEnd(wr)
            '    RenderCellBegin(wr, "", "", "", "right", "", "", "")






            '    '


            '    If dr.Read Then
            '        'TODO localise text
            '        phase = CStr(dr("phaseId"))


            '        wr.Write("Current phase: <div title=""Phase " & phase & """ class=""phase_icon current_thread_phase_" & phase & """></div>")
            '        'wr.Write("Current Phase:" & phase & " ")

            '    Else
            '        wr.Write("Current phase: <div title=""Phase " & 1 & """ class=""phase_icon current_thread_phase_" & 1 & """></div>")

            '    End If
            '    RenderCellEnd(wr)

            '    RenderRowEnd(wr)

            '    RenderTableEnd(wr)

            '    RenderCellEnd(wr)
            '    RenderRowEnd(wr)

            '    RenderTableEnd(wr)


            '    RenderDivEnd(wr)
            'End If



            'RenderCellEnd(wr) ' </td> 





            ''CP- Add back in row seperation 
            'RenderRowEnd(wr) '</tr>    







            RenderRowBegin(wr) ' <tr>

            RenderCellBegin(wr, buttonCellClass, "", "", "left", "top", "", "") ' <td>
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "", "0") ' <table>
            RenderRowBegin(wr) ' <tr>

            RenderCellBegin(wr, "", "", "5%", "left", "top", "", "") ' <td>
            If (True) Then
                'Me.RenderLinkButton(wr, Utilities.Links.ContainerViewPostLink(TabID, Post.ForumID, Post.PostID), strSubject, "Forum_NormalBold")
                wr.Write("<div class='hidden-title-override'>" & strSubject & "</div>")
                'Me.RenderLinkButton(wr, Utilities.Links.ContainerViewPostLink(TabID, Post.ForumID, Post.PostID), strSubject, "")
            Else
                'RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "", "0") ' <table>
            End If
            ' '' display edited tag if post has been modified
            ''If (Post.UpdatedByUser > 0) Then
            ''	' if the person who edited the post is a moderator and hide mod edits is enabled, we don't want to show edit details.
            ''	'CP - Impersonate
            ''	Dim objPosterSecurity As New ModuleSecurity(ModuleID, TabID, ForumId, LoggedOnUser.UserID)
            ''	If Not (objConfig.HideModEdits And objPosterSecurity.IsForumModerator) Then
            ''		wr.Write("&nbsp;")
            ''		RenderImage(wr, objConfig.GetThemeImageURL("s_edit.") & objConfig.ImageExtension, String.Format(ForumControl.LocalizedText("ModifiedBy") & " {0} {1}", Post.LastModifiedAuthor.SiteAlias, " " & ForumControl.LocalizedText("on") & " " & Post.UpdatedDate.ToString), "")
            ''	End If
            ''End If
            RenderCellEnd(wr) ' </td> 

            ' (in flatview or selected, display commands on right)
            RenderCellBegin(wr, "", "", "95%", "right", "middle", "", "") ' <td>
            Me.RenderCommands(wr, Post)
            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) '</tr>    
            RenderTableEnd(wr) ' </table> 
            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) '</tr>    
            RenderTableEnd(wr) ' </table> 
            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) '</tr>    

            RenderRowBegin(wr) ' <tr>

            Dim postBodyClass As String = String.Empty
            If PostCountIsEven Then
                postBodyClass = "Forum_PostBody"


            Else
                postBodyClass = "Forum_PostBody_Alt"
            End If
            'If (Post.IsSolution) Then
            'postBodyClass += " " & "solutionPost"
            'End If

            RenderCellBegin(wr, postBodyClass, "100%", "80%", "left", "top", "", "") ' <td>
            Me.RenderPostBody(wr, Post, PostCountIsEven, phase)
            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr>

            RenderTableEnd(wr) ' </table> 
        End Sub

        ''' <summary>
        ''' Renders the body of a post including signature and attachments
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <param name="Post"></param>
        ''' <param name="PostCountIsEven"></param>
        ''' <remarks></remarks>
        Private Sub RenderPostBody(ByVal wr As HtmlTextWriter, ByVal Post As PostInfo, ByVal PostCountIsEven As Boolean, ByVal phase As String)
            Dim author As ForumUserInfo = Post.Author
            Dim cleanBody As String = String.Empty
            Dim cleanSignature As String = String.Empty
            Dim attachmentClass As String = String.Empty
            Dim bodyForumText As Utilities.PostContent
            Dim url As String = String.Empty
            Dim userHasVotedPostVar As Boolean = UserHasVotedPost(Post.PostID, ProfileUserID)
            Dim thumbsUp As Integer
            Dim thumbsDown As Integer

            If Post.ParseInfo = PostParserInfo.None Or Post.ParseInfo = PostParserInfo.File Then
                'Nothing to Parse or just an Attachment not inline
                bodyForumText = New Utilities.PostContent(System.Web.HttpUtility.HtmlDecode(Post.Body), objConfig)
            Else
                If Post.ParseInfo < PostParserInfo.Inline Then
                    'Something to parse, but not any inline instances
                    bodyForumText = New Utilities.PostContent(System.Web.HttpUtility.HtmlDecode(Post.Body), objConfig, Post.ParseInfo)
                Else
                    'At lease Inline to Parse
                    If CurrentForumUser.UserID > 0 Then
                        bodyForumText = New Utilities.PostContent(System.Web.HttpUtility.HtmlDecode(Post.Body), objConfig, Post.ParseInfo, Post.AttachmentCollection(objConfig.EnableAttachment), True)
                    Else
                        bodyForumText = New Utilities.PostContent(System.Web.HttpUtility.HtmlDecode(Post.Body), objConfig, Post.ParseInfo, Post.AttachmentCollection(objConfig.EnableAttachment), False)
                    End If
                End If
            End If
            
            ' Imagine you move there, settle down and then they all come back
            'We will NOT support emoticons or BBCode (quotes/code) in Signatures
            Dim Signature As Utilities.PostContent = New Utilities.PostContent(System.Web.HttpUtility.HtmlDecode(author.Signature), objConfig)

            If ForumControl.objConfig.EnableBadWordFilter Then
                cleanBody = Utilities.ForumUtils.FormatProhibitedWord(bodyForumText.ProcessHtml(), Post.CreatedDate, PortalID)
                cleanSignature = Utilities.ForumUtils.FormatProhibitedWord(Signature.ProcessHtml(), Post.CreatedDate, PortalID)
            Else
                cleanBody = bodyForumText.ProcessHtml()
                cleanSignature = Signature.ProcessHtml()
            End If



            If hsTranslatedPosts.ContainsKey(Post.PostID) Then
                If Not hsTranslatedPosts(Post.PostID).ToString().Contains("#NLA#") Then
                    cleanBody = hsTranslatedPosts(Post.PostID).ToString()
                Else
                    cleanBody &= "<br/><b>" & ForumControl.LocalizedText("TranslationServiceDown") & "</b>"
                End If

            End If





            If PostCountIsEven Then
                attachmentClass = "Forum_Attachments"
            Else
                attachmentClass = "Forum_Attachments_Alt"
            End If

            RenderTableBegin(wr, "tblPostBody" & Post.PostID.ToString, "", "100%", "100%", "0", "0", "left", "", "0") ' should be 0, contains all post body elements already taking max height
            ' row for post body
            RenderRowBegin(wr) '<Tr>
            ' cell for post body, set cell attributes           
            RenderCellBegin(wr, "", "", "100%", "left", "top", "", "") ' <td>

            RenderDivBegin(wr, "spBody", "Forum_Normal Forum_Post_Body")    ' <div>
            wr.Write(cleanBody)
            wr.Write("<div class='cleared'></div><br/>")

            ' We dont need to show the "See translation" button if the content has already been translated
            If hsPostTranslate.ContainsKey(Post.PostID) And Not hsTranslatedPosts.ContainsKey(Post.PostID) Then
                cmdPostTranslate = CType(hsPostTranslate(Post.PostID), LinkButton)
                cmdPostTranslate.Text = ForumControl.LocalizedText("SeeTranslation")
                cmdPostTranslate.CommandArgument = Post.Body
                cmdPostTranslate.RenderControl(wr)
            Else

            End If
            'If Not hsTranslatedPosts(Post.PostID).ToString().Equals("#NLA#") Then
            ' wr.Write("<a class='translated-by-info' target='_blank' href='http://www.microsofttranslator.com/'>(Translated by Bing)</a>")
            ' End If


            wr.Write("<span class='hidden'>" & ForumControl.LocalizedText("TranslationLoading") & "</span>")
            RenderDivEnd(wr) ' </div>

            If objConfig.EnableUserSignatures Then
                ' insert signature if exists
                If Len(author.Signature) > 0 Then
                    RenderDivBegin(wr, "", "Forum_Normal")
                    wr.RenderBeginTag(HtmlTextWriterTag.Hr) ' <hr>
                    wr.RenderEndTag() ' </hr>
                    If objConfig.EnableHTMLSignatures Then
                        wr.Write(cleanSignature)
                    Else
                        wr.Write(cleanSignature)
                    End If
                    RenderDivEnd(wr) ' </span>
                End If
            End If

            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr> done with post body

            ' Report abuse
            RenderRowBegin(wr) '<tr> 
            'test bodycell
            RenderCellBegin(wr, "", "1px", "100%", "left", "", "", "") ' <td>

            If objConfig.EnablePostAbuse Then
                url = Utilities.Links.ReportToModsLink(TabID, ModuleID, Post.PostID)

                ' create table to hold link and image
                RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "middle", "0") ' <table>

                RenderRowBegin(wr) ' <tr>

                Dim renderSpace As Boolean = False

                If Post.PostReported > 0 And CurrentForumUser.IsInRole("Collaborator") Then
                    RenderCellBegin(wr, "", "", "25", "left", "middle", "", "") ' <td>
                    ' make a link to take users to see whom reported this post and why
                    RenderImage(wr, objConfig.GetThemeImageURL("s_postabuse.") & objConfig.ImageExtension, Post.PostReported.ToString & " " & Localization.GetString("AbuseReports", ForumControl.objConfig.SharedResourceFile), "")
                    wr.Write("&nbsp;")
                    RenderCellEnd(wr) ' </td>
                    renderSpace = False
                End If



                If (Post.ParentPostID = 0) Then
                    'RenderCellBegin(wr, "", "", "", "left", "middle", "", "") ' <td>

                    ' XFBML Version
                    ' wr.Write("<div id=""fb-root""></div><script src=""http://connect.facebook.net/en_US/all.js#xfbml=1""></script><fb:like href="""" send=""false""  width=""450"" show_faces=""true"" font=""verdana""></fb:like>")
                    Dim params As String() = New String(2) {"forumid=" & ForumID, "threadid=" & ThreadID, "scope=posts"}
                    Dim likeUrl As String = NavigateURL(TabID, "", params)
                    'wr.Write("<iframe src=""http://www.facebook.com/plugins/like.php?app_id=129007753847347&amp;href=" & likeUrl & "&amp;send=true&amp;layout=button_count&amp;width=450&amp;show_faces=true&amp;action=like&amp;colorscheme=light&amp;font&amp;height=25"" scrolling=""no"" frameborder=""0"" style=""border:none; overflow:hidden; width:450px; height:25px;"" allowTransparency=""true""></iframe>")
                    ' wr.Write("<iframe src=""http://www.facebook.com/plugins/like.php?app_id=129007753847347&amp;href=" & likeUrl & "&amp;send=true&amp;layout=standard&amp;width=450&amp;show_faces=true&amp;action=like&amp;colorscheme=light&amp;font&amp;height=25"" scrolling=""no"" frameborder=""0"" style=""border:none; overflow:hidden; width:450px; height:25px;"" allowTransparency=""true""></iframe>")
                    ' wr.Write("<a href=""https://twitter.com/share"" class=""twitter-share-button"" data-count=""horizontal"">Tweet</a><script type=""text/javascript"" src=""//platform.twitter.com/widgets.js""></script>")
                    'wr.Write("<iframe src=""//www.facebook.com/plugins/like.php?href=" & likeUrl & "&amp;send=false&amp;layout=button_count&amp;width=450&amp;show_faces=true&amp;action=like&amp;colorscheme=light&amp;font&amp;height=21&amp;appId=220955987953254"" scrolling=""no"" frameborder=""0"" style=""border:none; overflow:hidden; width:450px; height:21px;"" allowTransparency=""true""></iframe>")
                    'wr.Write("    <iframe allowtransparency='true' frameborder='0' scrolling='no' src='//platform.twitter.com/widgets/tweet_button.html' style='width:130px; height:20px;'></iframe>")

                    'wr.Write("<!-- AddThis Button BEGIN --><div style='margin-top:10px;' class='addthis_toolbox addthis_default_style '><a class='addthis_button_facebook'></a><a class='addthis_button_twitter'></a><a class='addthis_button_google'></a><a class='addthis_button_youtube'></a><a class='addthis_button_email'></a><a class='addthis_button_print'></a><a class='addthis_button_favorites'></a><a class='addthis_button_google_plusone'></a></div><script type='text/javascript' src='http://s7.addthis.com/js/250/addthis_widget.js#pubid=xa-4ead340652e4640c'></script><!-- AddThis Button END -->")
                    'wr.Write("<div class='addthis_toolbox addthis_pill_combo'><a class='addthis_button_tweet' tw:count='horizontal'></a><a class='addthis_button_facebook_like'></a><a class='addthis_counter addthis_pill_style'></a></div>")
                    ' wr.Write("<script type='text/javascript' src='http://s7.addthis.com/js/250/addthis_widget.js#pubid=YOUR-PROFILE-ID'></script>")
                    'wr.Write("<!-- AddThis Button BEGIN --><div style='margin-top:10px;'><a class='addthis_button addthis_pill_style' href='http://www.addthis.com/bookmark.php?v=250&amp;pubid=xa-4eb0152822796196'><img src='http://s7.addthis.com/static/btn/v2/lg-share-en.gif' width='125' height='16' alt='Bookmark and Share' style='border:0'/></a></div><script type='text/javascript' src='http://s7.addthis.com/js/250/addthis_widget.js#pubid=xa-4eb0152822796196'></script><!-- AddThis Button END -->")
                    ' wr.Write("<!-- AddThis Button BEGIN --><div class='addthis_toolbox addthis_default_style '><a class='addthis_button_facebook_like' fb:like:layout='button_count'></a><a class='addthis_button_tweet'></a><a class='addthis_button_google_plusone' g:plusone:size='medium'  style='width:80px;'></a><a class='addthis_counter addthis_pill_style addthis:ui_hover_direction='-1'></a></div><script type='text/javascript' src='http://s7.addthis.com/js/250/addthis_widget.js#pubid=xa-4e92e7b0385e8abc'></script><!-- AddThis Button END -->")
                    ' RenderCellEnd(wr) ' </td>
                End If


                ' ATC End

                RenderCellBegin(wr, "Forum_ReplyCell Forum_ReportCell", "", "", "left", "top", "", "") ' <td>


                wr.Write("<table class='reply-table'><tr>")
                ' ATC Adding reply button to each post
                url = Utilities.Links.NewPostLink(TabID, ForumID, objThread.ThreadID, "reply", ModuleID)


                wr.Write("<td valign='middle' colspan='2' class='Forum_NavBarButton'>")



                wr.Write("<span style='white-space:nowrap;'>")

                If (Post.ParentPostID <> 0 And phase = "2") Then
                    wr.Write("<span  class=""action-button fleft replyButton""   >" & ForumControl.LocalizedText("Reply") & "</span>")
                    'wr.Write("<div class=""repliesOr fleft"">" & ForumControl.LocalizedText("Or") & "</div>")

                    'wr.Write("<div class=""submit-proposal-btn-wrapper fleft margR""><span class=""action-button fright"">" & ForumControl.LocalizedText("SubmitProposal") & "</span><div class=""cleared""></div></div>")
                    wr.Write("<span class='submit-proposal-btn-wrapper'><span class="" action-button fleft margR margL"">" & ForumControl.LocalizedText("SubmitProposal") & "</span></span>")

                    url = Utilities.Links.NewPostLink(TabID, ForumID, Post.PostID, "quote", ModuleID)
                    wr.Write("<a href=""" & url & """ class=""action-button fleft margR"">" & ForumControl.LocalizedText("Quote") & "</a>")

                    url = Utilities.Links.ReportToModsLink(TabID, ModuleID, Post.PostID)

                    url = url.Replace("language/en-GB", "language/" + CultureInfo.CurrentUICulture.Name)
                    RenderLinkButton(wr, url, ForumControl.LocalizedText("ReportAbuse"), "regular-link report-link-height  report-reply")
                ElseIf (Post.ParentPostID = 0 And ModuleID <> PROPOSE_YOUR_TOPIC_MODULEID And phase = "2") Then
                    wr.Write("<span  class=""action-button fleft replyButton"">" & ForumControl.LocalizedText("Reply") & "</span>")
                    'wr.Write("<div class=""repliesOr fleft"">" & ForumControl.LocalizedText("Or") & "</div>")
                    wr.Write("<span class='submit-proposal-btn-wrapper'><span class=""submit-proposal-btn-wrapper action-button fleft margR margL"">" & ForumControl.LocalizedText("SubmitProposal") & "</span></span>")
                    url = Utilities.Links.NewPostLink(TabID, ForumID, Post.PostID, "quote", ModuleID)
                    wr.Write("<a href=""" & url & """ class=""action-button fleft margR"">" & ForumControl.LocalizedText("Quote") & "</a>")

                    url = Utilities.Links.ReportToModsLink(TabID, ModuleID, Post.PostID)
                    RenderLinkButton(wr, url, ForumControl.LocalizedText("ReportAbuse"), "regular-link report-link-height")


                    ' Quote link

                    If Not CurrentForumUser.IsBanned Then

                    Else
                       
                    End If








                Else
                    url = Utilities.Links.ReportToModsLink(TabID, ModuleID, Post.PostID)
                    RenderLinkButton(wr, url, ForumControl.LocalizedText("ReportAbuse"), "regular-link report-link")
                End If

                'url = DotNetNuke.Common.Globals.NavigateURL(72, "")

                'wr.Write("<a class='invite-link-height' href='" & url & "'>" & ForumControl.LocalizedText("InviteFriend") & "&nbsp;</a>")


                'ATC Adding reply button to each post


                ' ATC removing report button temporarily
                ' Warn link
                wr.Write("</span>")

                wr.Write("</td>")


                If (Post.ParentPostID = 0) Then

                    ' wr.Write("<td valign='top' class='Forum_NavBarButton'>")

                      '  wr.Write("</td>")
                End If
                wr.Write("</tr></table>")
                RenderCellEnd(wr) ' </td>


                'RenderCellBegin(wr, "Forum_ThumbsCellText", "", "", "left", "", "", "")    '<td>
                ' RenderCellEnd(wr) ' </td>
                wr.Write("</tr><tr>")

                If renderSpace Then
                    RenderCellBegin(wr, "", "", "", "right", "middle", "", "") ' <td>
                    wr.Write("&nbsp;")
                    RenderCellEnd(wr) ' </td>
                End If
                If (ModuleID = PROPOSE_YOUR_TOPIC_MODULEID Or Post.ParentPostID <> 0) Then
                    If CurrentForumUser.UserID > 0 Then

                        ' ATC Start
                        If Not userHasVotedPostVar Then

                            RenderCellBegin(wr, "", "", "", "right", "top", "2", "")
                            RenderTableBegin(wr, "", "", "", "", "", "", "", "", "")
                            RenderRowBegin(wr)

                            RenderCellBegin(wr, "Forum_ThumbsCell", "", "", "left", "middle", "", "") ' <td>
                            ' Retrieving post thumbs values
                            Dim dr As IDataReader = Nothing
                            Try
                                If (ModuleID = PROPOSE_YOUR_TOPIC_MODULEID Or Post.ParentPostID <> 0) Then

                                    dr = DotNetNuke.Modules.Forum.DataProvider.Instance().PostGetThumbs(Post.PostID)
                                    If dr.Read Then
                                        thumbsUp = Convert.ToInt32(dr("thumbsUp"))
                                        thumbsDown = Convert.ToInt32(dr("thumbsDown"))

                                        'wr.Write("Thumbs Up:" & thumbsUp & " ")
                                        'wr.Write("Thumbs Down:" & thumbsDown)
                                    End If
                                End If

                            Finally
                                If dr IsNot Nothing Then
                                    dr.Close()
                                End If
                            End Try


                            If hsPostThumbs.ContainsKey(Post.PostID) Then
                                cmdPostThumbs = CType(hsPostThumbs(Post.PostID), LinkButton)
                                cmdPostThumbs.Text = "" & thumbsUp & " "
                                cmdPostThumbs.CommandArgument = Post.PostID.ToString
                                cmdPostThumbs.RenderControl(wr)
                                ' wr.Write("up:" & thumbsUp & " ")
                            End If

                            'wr.Write("Agree")
                            If hsPostThumbsDown.ContainsKey(Post.PostID) Then
                                cmdPostThumbs = CType(hsPostThumbsDown(Post.PostID), LinkButton)
                                cmdPostThumbs.Text = "" & thumbsDown & " "
                                cmdPostThumbs.CommandArgument = Post.PostID.ToString
                                cmdPostThumbs.RenderControl(wr)
                                ' wr.Write("down:" & thumbsDown & " ")

                            End If



                            RenderCellEnd(wr) ' </td>
                            RenderRowEnd(wr) ' </td>
                            RenderTableEnd(wr) ' </td>
                            RenderCellEnd(wr) ' </td>
                        Else
                            RenderCellBegin(wr, "", "", "", "right", "top", "", "")
                            RenderTableBegin(wr, "", "", "", "", "", "", "", "", "")
                            RenderRowBegin(wr)

                            RenderCellBegin(wr, "Forum_ThumbsCell", "", "", "left", "middle", "", "") ' <td>

                            ' Retrieving post thumbs values
                            Dim dr As IDataReader = Nothing
                            Try
                                If (ModuleID = PROPOSE_YOUR_TOPIC_MODULEID Or Post.ParentPostID <> 0) Then

                                    dr = DotNetNuke.Modules.Forum.DataProvider.Instance().PostGetThumbs(Post.PostID)
                                    If dr.Read Then
                                        thumbsUp = Convert.ToInt32(dr("thumbsUp"))
                                        thumbsDown = Convert.ToInt32(dr("thumbsDown"))

                                        'wr.Write("Thumbs Up:" & thumbsUp & " ")
                                        'wr.Write("Thumbs Down:" & thumbsDown)
                                    End If
                                End If

                            Finally
                                If dr IsNot Nothing Then
                                    dr.Close()
                                End If
                            End Try
                            ' RenderLinkButton(wr, "", thumbsUp & "", "ThumbsUpButton already-voted")
                            'RenderLinkButton(wr, "", thumbsDown & "", "ThumbsDownButton already-voted")
                            wr.Write("<span class=""ThumbsUpButton  already-voted"">" & thumbsUp & "</span>")

                            wr.Write("<span class=""ThumbsDownButton  already-voted"">" & thumbsDown & "</span>")
                            RenderCellEnd(wr) ' </td>
                            RenderRowEnd(wr) ' </td>
                            RenderTableEnd(wr) ' </td>
                            RenderCellEnd(wr) ' </td>
                        End If

                        renderSpace = False
                    Else
                        If True Then
                            RenderCellBegin(wr, "", "", "", "right", "top", "2", "")
                            RenderTableBegin(wr, "", "", "", "", "", "", "", "", "")
                            RenderRowBegin(wr)

                            RenderCellBegin(wr, "Forum_ThumbsCell", "", "", "left", "middle", "", "") ' <td>

                            ' Retrieving post thumbs values
                            Dim dr As IDataReader = Nothing
                            Try
                                If (ModuleID = PROPOSE_YOUR_TOPIC_MODULEID Or Post.ParentPostID <> 0) Then

                                    dr = DotNetNuke.Modules.Forum.DataProvider.Instance().PostGetThumbs(Post.PostID)
                                    If dr.Read Then
                                        thumbsUp = Convert.ToInt32(dr("thumbsUp"))
                                        thumbsDown = Convert.ToInt32(dr("thumbsDown"))

                                        'wr.Write("Thumbs Up:" & thumbsUp & " ")
                                        'wr.Write("Thumbs Down:" & thumbsDown)
                                    End If
                                End If

                            Finally
                                If dr IsNot Nothing Then
                                    dr.Close()
                                End If
                            End Try
                            'RenderLinkButton(wr, "", thumbsUp & "", "ThumbsUpButton  please-log-in")
                            wr.Write("<span class=""ThumbsUpButton  please-log-in"">" & thumbsUp & "</span>")

                            wr.Write("<span class=""ThumbsDownButton  please-log-in"">" & thumbsDown & "</span>")
                            'RenderLinkButton(wr, "", thumbsDown & "", "ThumbsDownButton  please-log-in")
                            RenderCellEnd(wr) ' </td>
                            RenderRowEnd(wr) ' </td>
                            RenderTableEnd(wr) ' </td>
                            RenderCellEnd(wr) ' </td>
                        End If
                    End If
                    'renderSpace = False
                End If
                RenderRowEnd(wr) ' </tr> 
                RenderTableEnd(wr) ' </table>
            Else
                wr.Write("&nbsp;")
            End If

            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr> 

            ''CP-ADD - New per post rating (preparing UI) - Not Implemented
            'RenderRowBegin(wr) '<tr> 
            'RenderCellBegin(wr, postBodyClass, "100%", "100%", "", "", "", "")
            'RenderPerPostRating(wr)
            'RenderCellEnd(wr) ' </td>
            'RenderRowEnd(wr) ' </tr> done with perPostRating

            'New Attachments type
            'Select Case Post.ParseInfo
            'Case 4, 5, 6, 7, 15
            If objConfig.EnableAttachment AndAlso Post.AttachmentCollection(objConfig.EnableAttachment).Count > 0 Then
                RenderRowBegin(wr)
                RenderCellBegin(wr, "", "1px", "100%", "left", "middle", "", "") ' <td>
                RenderTableBegin(wr, "", "attachments-table", "", "", "0", "0", "", "middle", "0") ' <table>

                RenderTableEnd(wr)

                RenderCellEnd(wr)
                RenderRowEnd(wr)

                RenderRowBegin(wr) '<tr> 
                RenderCellBegin(wr, attachmentClass, "1px", "100%", "left", "middle", "", "") ' <td>





                Dim count As Integer = 0
                For Each objFile As AttachmentInfo In Post.AttachmentCollection(objConfig.EnableAttachment)
                    'Here we only handle attachments not inline type
                    If (count < 1) Then
                        wr.Write("<div class='postImages'>")
                    End If
                    If (objFile.Extension.Equals("png") Or objFile.Extension.Equals("jpg") Or objFile.Extension.Equals("jpeg") Or objFile.Extension.Equals("gif")) Then


                        If objFile.Inline = False Then



                            Dim strlink As String
                            Dim strFileName As String
                            Dim strlinkFilePath As String
                            If (objConfig.AnonDownloads = False) Then
                                If HttpContext.Current.Request.IsAuthenticated = False Then

                                    Exit For

                                Else

                                End If

                            Else
                                'AnonDownloads are Enabled
                                strlink = FormatURL("FileID=" & objFile.FileID, False, True)
                                strFileName = objFile.LocalFileName
                                'FileInfo(FileInfo = objFile.










                                'RenderImageButton(wr, strlink, objConfig.GetThemeImageURL("s_attachment.") & objConfig.ImageExtension, "", "", True)


                                'RenderLinkButton(wr, strlink, strFileName, "Forum_Link", "", True, False)



                                Dim FileController As New FileController()
                                Dim file As FileInfo = FileController.GetFileById(objFile.FileID, 0)
                                strlinkFilePath = "http://" & HttpContext.Current.Request.ServerVariables("SERVER_NAME") & "/Portals/0/" & file.RelativePath
                                '
                                'return PortalSettings.HomeDirectory + fi.Folder + fi.FileName;

                                'http://proxy.boxresizer.com/convert?resize=80x80&shape=pad&source=http%3A%2F%2Fexample.com%2Fuploads%2F39c0c11af.png
                                Dim encodedStrLink As String = HttpUtility.UrlEncode(strlinkFilePath)
                                Dim entireImgSource As String = "http://proxy.boxresizer.com/convert?resize=90x90&source=" & encodedStrLink

                                wr.Write("<a target='_blank' title='" & strFileName & "' href='" & strlinkFilePath & "'  rel='lightbox[" & Post.PostID & "]'><img src='" & entireImgSource & "' /></a>")




                            End If

                        End If
                    End If
                    If (count < 1) Then
                        wr.Write("</div>")
                    End If
                    count = count + 1
                Next







                ' create table to hold link and image
                RenderTableBegin(wr, "", "", "", "", "0", "0", "", "middle", "0") ' <table>

                For Each objFile As AttachmentInfo In Post.AttachmentCollection(objConfig.EnableAttachment)
                    'Here we only handle attachments not inline type
                    If objFile.Inline = False Then

                        RenderRowBegin(wr) ' <tr>
                        RenderCellBegin(wr, "", "", "", "left", "middle", "", "") ' <td>

                        Dim strlink As String
                        Dim strFileName As String

                        If (objConfig.AnonDownloads = False) Then
                            If HttpContext.Current.Request.IsAuthenticated = False Then
                                'AnonDownloads are Disabled
                                strFileName = Localization.GetString("NoAnonDownloads", ForumControl.objConfig.SharedResourceFile)

                                RenderCellBegin(wr, "", "", "", "left", "middle", "", "") ' <td>
                                RenderImage(wr, objConfig.GetThemeImageURL("s_attachment.") & objConfig.ImageExtension, "", "")
                                RenderCellEnd(wr) ' </td>

                                RenderCellBegin(wr, "", "", "", "left", "middle", "", "") ' <td>
                                wr.Write("&nbsp;")
                                wr.Write("<span class=Forum_NormalBold>" & strFileName & "</span>")
                                RenderCellEnd(wr) ' </td>

                                'We only want to display this information once..
                                RenderCellEnd(wr) ' </td>
                                RenderRowEnd(wr) ' </tr>
                                Exit For

                            Else
                                'User is Authenticated
                                strlink = FormatURL("FileID=" & objFile.FileID, False, True)
                                strFileName = objFile.LocalFileName

                                RenderCellBegin(wr, "", "", "", "left", "middle", "", "") ' <td>
                                RenderImageButton(wr, objFile.FileName, objConfig.GetThemeImageURL("s_attachment.") & objConfig.ImageExtension, "", "", True)
                                RenderCellEnd(wr) ' </td>

                                RenderCellBegin(wr, "", "", "", "left", "middle", "", "") ' <td>
                                wr.Write("&nbsp;")
                                '

                                '
                                RenderLinkButton(wr, strlink, strFileName, "Forum_Link", "", True, False)
                                RenderCellEnd(wr) ' </td>
                            End If

                        Else
                            'AnonDownloads are Enabled
                            strlink = FormatURL("FileID=" & objFile.FileID, False, True)
                            strFileName = objFile.LocalFileName

                            RenderCellBegin(wr, "", "", "", "left", "middle", "", "") ' <td>
                            RenderImageButton(wr, strlink, objConfig.GetThemeImageURL("s_attachment.") & objConfig.ImageExtension, "", "", True)
                            RenderCellEnd(wr) ' </td>

                            RenderCellBegin(wr, "", "", "", "left", "middle", "", "") ' <td>
                            wr.Write("&nbsp;")
                            RenderLinkButton(wr, strlink, strFileName, "Forum_Link", "", True, False)




                            RenderCellEnd(wr) ' </td>


                        End If

                        RenderCellEnd(wr) ' </td>
                        RenderRowEnd(wr) ' </tr> 
                    End If
                Next

                RenderTableEnd(wr) ' </table>
                RenderCellEnd(wr) ' </td>
                RenderRowEnd(wr) ' </tr> 

            End If
            'End Select
            RenderTableEnd(wr) ' </table> 
        End Sub

        ''' <summary>
        ''' Formats the URL used for attachments
        ''' </summary>
        ''' <param name="Link"></param>
        ''' <param name="TrackClicks"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FormatURL(ByVal Link As String, ByVal TrackClicks As Boolean) As String
            Return Common.Globals.LinkClick(Link, TabID, ModuleID, TrackClicks)
        End Function

        ''' <summary>
        ''' Formats the URL used for attachments (new version)
        ''' </summary>
        ''' <param name="Link"></param>
        ''' <param name="TrackClicks"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FormatURL(ByVal Link As String, ByVal TrackClicks As Boolean, ByVal ForceDownload As Boolean) As String
            Return Common.Globals.LinkClick(Link, TabID, ModuleID, TrackClicks, ForceDownload)
        End Function

        ''' <summary>
        ''' This allows for spacing between posts
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks></remarks>
        Private Sub RenderSpacerRow(ByVal wr As HtmlTextWriter)
            RenderRowBegin(wr) '<tr> 
            RenderCellBegin(wr, "Forum_SpacerRow", "", "", "", "", "", "")  ' <td>
            RenderImage(wr, objConfig.GetThemeImageURL("height_spacer.gif"), "", "")
            RenderCellEnd(wr)

            RenderCellBegin(wr, "Forum_SpacerRow", "", "", "", "", "", "")  ' <td>
            RenderImage(wr, objConfig.GetThemeImageURL("height_spacer.gif"), "", "")
            RenderCellEnd(wr) '</td>
            RenderRowEnd(wr) ' </tr>
        End Sub

        ''' <summary>
        ''' Footer w/ paging (second to last row)
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks>
        ''' </remarks>
        Private Sub RenderFooter(ByVal wr As HtmlTextWriter)
            Dim pageCount As Integer = CInt(Math.Floor((objThread.Replies) / CurrentForumUser.PostsPerPage)) + 1
            Dim pageCountInfo As New StringBuilder

            pageCountInfo.Append(ForumControl.LocalizedText("PageCountInfo"))
            pageCountInfo.Replace("[PageNumber]", (PostPage + 1).ToString)
            pageCountInfo.Replace("[PageCount]", pageCount.ToString)

            ' Start the footer row
            RenderRowBegin(wr)
            RenderCapCell(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "", "") ' <td><img/></td>

            RenderCellBegin(wr, "", "", "", "left", "middle", "", "") ' <td> 
            RenderTableBegin(wr, "", "margT", "", "100%", "0", "0", "", "", "0") ' <table>
            RenderRowBegin(wr) ' <tr>
            RenderCapCell(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "Forum_FooterCapLeft", "") ' <td><img/></td>

            RenderCellBegin(wr, "Forum_Footer", "", "", "", "", "", "") ' <td>
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "", "0") ' <table>
            RenderRowBegin(wr) ' <tr>

            RenderCellBegin(wr, "", "", "70%", "", "", "", "")  ' <td>
            RenderDivBegin(wr, "spPageCounting", "Forum_FooterText") ' <span>
            If (pageCount > 1) Then
                RenderDivBegin(wr, "", "Forum_FooterText") ' <span>
                RenderPostPaging(wr, pageCount)
                wr.Write("&nbsp;")
                wr.Write("&nbsp;" & pageCountInfo.ToString)
                RenderDivEnd(wr) ' </span>
            End If

            RenderDivEnd(wr) ' </span>
            RenderCellEnd(wr) ' </td> 

            RenderCellBegin(wr, "", "", "30%", "right", "", "", "")   ' <td> 

            If (ModuleID <> PROPOSE_YOUR_TOPIC_MODULEID) Then
                ddlViewDescending.RenderControl(wr)
            End If


            ' Close paging
            RenderCellEnd(wr) ' </td>   
            RenderRowEnd(wr) ' </tr>  
            wr.Write("<tr><td colspan='2'>")
            ' Display tracking option if user is authenticated and post count > 0 and user not track parent forum (make sure tracking is enabled)
            'CP - Seperating so we can show user they are tracking at forum level if need be
            If (PostCollection.Count > 0) AndAlso (CurrentForumUser.UserID > 0) And (objConfig.MailNotification) Then
                If objSecurity.IsForumAdmin Then
                    cmdThreadSubscribers.RenderControl(wr)
                    wr.Write("<br />")
                End If

                If TrackedForum Then
                Else
                    wr.Write("<div style='margin-top:10px;'>")
                    wr.Write("<div class='info-div'><div class='info-icon'>")
                    chkEmail.RenderControl(wr)
                    wr.Write("</div></div>")
                    wr.Write("</div>")
                End If
            End If
            wr.Write("</td></tr>")
            RenderTableEnd(wr) ' </table>  



            RenderCellEnd(wr) ' </td>   
            RenderCapCell(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "Forum_FooterCapRight", "") ' <td><img/></td>
            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </td>
            RenderCapCell(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "", "") ' <td><img/></td>
            RenderRowEnd(wr) ' </tr>  
        End Sub

        ''' <summary>
        ''' Renders the bottom prev/next buttons.
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks>
        ''' </remarks>
        Private Sub RenderBottomThreadButtons(ByVal wr As HtmlTextWriter)
            Dim url As String = String.Empty

            RenderRowBegin(wr) '<tr>
            RenderCapCell(wr, objConfig.GetThemeImageURL("height_spacer.gif"), "", "")
            RenderCellBegin(wr, "", "", "100%", "", "", "", "") '<td>
            RenderCellEnd(wr) ' </td> 
            RenderCapCell(wr, objConfig.GetThemeImageURL("height_spacer.gif"), "", "")
            RenderRowEnd(wr) ' </tr>

            RenderRowBegin(wr) '<tr>
            RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "")
            RenderCellBegin(wr, "", "", "100%", "", "", "", "") '<td>
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "", "0") ' <table>
            RenderRowBegin(wr) '<tr>

            RenderCellBegin(wr, "", "", "50%", "left", "middle", "", "") ' <td> '
            ' new thread button
            'Remove LoggedOnUserID limitation if wishing to implement Anonymous Posting
            If (CurrentForumUser.UserID > 0) And (Not ForumID = -1) Then
                If Not objThread.ContainingForum.PublicPosting Then
                    If objSecurity.IsAllowedToStartRestrictedThread Then

                        RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "0") '<Table>            
                        RenderRowBegin(wr) '<tr>
                        url = Utilities.Links.NewThreadLink(TabID, ForumID, ModuleID)
                        RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 

                        If CurrentForumUser.IsBanned Then
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("NewThread"), "Forum_Link", False)
                        Else
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("NewThread"), "Forum_Link")
                        End If

                        RenderCellEnd(wr) ' </Td>

                        If CurrentForumUser.IsBanned Or (Not objSecurity.IsAllowedToPostRestrictedReply) Or (objThread.IsClosed) Then
                            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                            wr.Write("&nbsp;")
                            RenderCellEnd(wr) ' </Td>
                            RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link", False)
                            RenderCellEnd(wr) ' </Td>
                        Else
                            url = Utilities.Links.NewPostLink(TabID, ForumID, objThread.ThreadID, "reply", ModuleID)
                            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                            wr.Write("&nbsp;")
                            RenderCellEnd(wr) ' </Td>
                            RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link")
                            RenderCellEnd(wr) ' </Td>
                        End If

                        '[skeel] moved delete thread here
                        If CurrentForumUser.UserID > 0 AndAlso (objSecurity.IsForumModerator) Then
                            url = Utilities.Links.ThreadDeleteLink(TabID, ModuleID, ForumID, ThreadID, False)
                            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                            wr.Write("&nbsp;")
                            RenderCellEnd(wr) ' </Td>
                            RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("DeleteThread"), "Forum_Link")
                            RenderCellEnd(wr) ' </Td>
                        End If

                        RenderRowEnd(wr) ' </tr>
                        RenderTableEnd(wr) ' </table>
                    ElseIf objSecurity.IsAllowedToPostRestrictedReply Then
                        RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "0") '<Table>            
                        RenderRowBegin(wr) '<tr>

                        If CurrentForumUser.IsBanned Or objThread.IsClosed Then
                            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                            wr.Write("&nbsp;")
                            RenderCellEnd(wr) ' </Td>
                            RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link", False)
                            RenderCellEnd(wr) ' </Td>
                        Else
                            url = Utilities.Links.NewPostLink(TabID, ForumID, objThread.ThreadID, "reply", ModuleID)
                            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                            wr.Write("&nbsp;")
                            RenderCellEnd(wr) ' </Td>
                            RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                            RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link")
                            RenderCellEnd(wr) ' </Td>
                        End If

                        RenderRowEnd(wr) ' </tr>
                        RenderTableEnd(wr) ' </table>
                    Else
                        wr.Write("&nbsp;")
                    End If
                Else
                    RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "0") '<Table>            
                    RenderRowBegin(wr) '<tr>
                    url = Utilities.Links.NewThreadLink(TabID, ForumID, ModuleID)
                    RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                    If CurrentForumUser.IsBanned Then
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("NewThread"), "Forum_Link", False)
                    Else
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("NewThread"), "Forum_Link")
                    End If
                    RenderCellEnd(wr) ' </Td>

                    If CurrentForumUser.IsBanned Or objThread.IsClosed Then
                        RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                        wr.Write("&nbsp;")
                        RenderCellEnd(wr) ' </Td>
                        RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link", False)
                        RenderCellEnd(wr) ' </Td>
                    Else
                        url = Utilities.Links.NewPostLink(TabID, ForumID, objThread.ThreadID, "reply", ModuleID)
                        RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                        wr.Write("&nbsp;")
                        RenderCellEnd(wr) ' </Td>
                        RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link")
                        RenderCellEnd(wr) ' </Td>
                    End If

                    '[skeel] moved delete thread here
                    If CurrentForumUser.UserID > 0 AndAlso (objSecurity.IsForumModerator) Then
                        url = Utilities.Links.ThreadDeleteLink(TabID, ModuleID, ForumID, ThreadID, False)
                        RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td>
                        wr.Write("&nbsp;")
                        RenderCellEnd(wr) ' </Td>
                        RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "middle", "", "") ' <td> 
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("DeleteThread"), "Forum_Link")
                        RenderCellEnd(wr) ' </Td>
                    End If

                    RenderRowEnd(wr) ' </tr>
                    RenderTableEnd(wr) ' </table>
                End If
            End If

            RenderCellEnd(wr) ' </Td>

            RenderCellBegin(wr, "", "", "50%", "right", "", "", "") ' <td> ' 
            RenderTableBegin(wr, "", "", "100%", "", "0", "0", "", "", "0") '<Table>            
            RenderRowBegin(wr) '<tr>

            Dim PreviousEnabled As Boolean = False
            Dim EnabledText As String = "Disabled"
            If Not objThread.PreviousThreadID = 0 Then
                If Not objThread.IsPinned Then
                    PreviousEnabled = True
                    EnabledText = "Previous"
                End If
            End If

            If PreviousEnabled Then
                RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "", "", "")  ' <td> ' 
            Else
                RenderCellBegin(wr, "Forum_NavBarButtonDisabled", "", "", "", "", "", "")   ' <td> ' 
            End If

            RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "0") '<Table>            
            RenderRowBegin(wr) '<tr>

            url = Utilities.Links.ContainerViewThreadLink(TabID, ForumID, objThread.PreviousThreadID)

            RenderCellBegin(wr, "", "", "", "", "", "", "")  ' <td> ' 

            If PreviousEnabled Then
                'RenderLinkButton(wr, url, ForumControl.LocalizedText("Previous"), "Forum_Link_Left")
            Else
                'RenderDivBegin(wr, "", "Forum_NormalBold")
                'wr.Write(ForumControl.LocalizedText("Previous"))
                'RenderDivEnd(wr)
            End If
            RenderCellEnd(wr) ' </td>

            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </td>

            RenderCellBegin(wr, "", "", "", "", "", "", "")  ' <td> 
            wr.Write("&nbsp;")
            RenderCellEnd(wr) ' </td>

            'next button
            Dim NextEnabled As Boolean = False
            Dim NextText As String = "Disabled"
            If Not objThread.NextThreadID = 0 Then
                If Not objThread.IsPinned Then
                    NextEnabled = True
                    NextText = "Next"
                End If
            End If

            If NextEnabled Then
                RenderCellBegin(wr, "Forum_NavBarButton", "", "", "", "", "", "")  ' <td> 
            Else
                RenderCellBegin(wr, "Forum_NavBarButtonDisabled", "", "", "", "", "", "")   ' <td> 
            End If

            RenderTableBegin(wr, "", "", "", "", "0", "0", "", "", "0") '<Table>            
            RenderRowBegin(wr) '<tr>
            RenderCellBegin(wr, "", "", "", "", "", "", "")  ' <td> 

            If NextEnabled Then
                'url = Utilities.Links.ContainerViewThreadLink(TabID, ForumID, objThread.NextThreadID)
                'RenderLinkButton(wr, url, ForumControl.LocalizedText("Next"), "Forum_Link")
            Else
                'RenderDivBegin(wr, "", "Forum_NormalBold")
                'wr.Write(ForumControl.LocalizedText("Next"))
                'RenderDivEnd(wr)
            End If
            RenderCellEnd(wr) ' </td>   
            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>

            ' enclosing table for prev/next
            wr.RenderEndTag() ' </Td>
            wr.RenderEndTag() ' </Tr>
            RenderTableEnd(wr) ' </table> 

            wr.RenderEndTag() ' </Td>
            wr.RenderEndTag() ' </Tr>
        End Sub

        ''' <summary>
        ''' Renders the bottom breadcrumb.
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks></remarks>
        Private Sub RenderBottomBreadCrumb(ByVal wr As HtmlTextWriter)
            RenderRowBegin(wr) '<Tr>

            RenderCellBegin(wr, "", "", "", "left", "", "2", "") ' <td> 
            Dim ChildGroupView As Boolean = False
            If CType(ForumControl.TabModuleSettings("groupid"), String) <> String.Empty Then
                ChildGroupView = True
            End If
            wr.Write(Utilities.ForumUtils.BreadCrumbs(TabID, ModuleID, ForumScope.Posts, objThread, objConfig, ChildGroupView))
            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) ' </tr> 
        End Sub

        ''' <summary>
        ''' Renders the tags area, which is blow the bottom breadcrumb. 
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks>We are only allowing tagging in public forums, because of security concerns in tag search results (we go one level deeper in perms than core).</remarks>
        Private Sub RenderTags(ByVal wr As HtmlTextWriter)
            If objThread.ContainingForum.PublicView AndAlso objConfig.EnableTagging Then
                RenderRowBegin(wr) '<tr>

                RenderCellBegin(wr, "", "", "98%", "left", "", "2", "") ' <td> 
                tagsControl.RenderControl(wr)
                RenderCellEnd(wr) ' </td> 

                If objSecurity.IsForumModerator Then
                    ' reserved for an edit button, or control, to manage tags.
                    'RenderCellBegin(wr, "", "", "5%", "left", "", "", "")	' <td> 
                    'tagsControl.RenderControl(wr)
                    'RenderCellEnd(wr) ' </td> 
                End If

                RenderRowEnd(wr) ' </tr>  
            End If
        End Sub

        ''' <summary>
        ''' Determines if we should render the quick reply section based on several conditions, also adds a bottom row for padding.
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks></remarks>
        Private Sub RenderQuickReply(ByVal wr As HtmlTextWriter)
            If objConfig.EnableQuickReply Then
                If (CurrentForumUser.UserID > 0) And (Not objThread.ForumID = -1) Then
                    If Not objThread.ContainingForum.PublicPosting Then
                        If CurrentForumUser.IsBanned = False And objThread.IsClosed = False Then
                            If objSecurity.IsAllowedToPostRestrictedReply Then
                                QuickReply(wr)
                            End If
                        End If
                    Else
                        If CurrentForumUser.IsBanned = False And objThread.IsClosed = False Then
                            QuickReply(wr)
                        End If
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' Renders the bottom area that includes date drop down, view subscribers link (for admin) and notification checkbox.
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks></remarks>
        Private Sub RenderThreadOptions(ByVal wr As HtmlTextWriter)
            RenderRowBegin(wr) '<tr>
            RenderCellBegin(wr, "", "", "100%", "right", "", "2", "") ' <td> 

            If PostCollection.Count > 0 Then
                wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                wr.AddAttribute(HtmlTextWriterAttribute.Src, objConfig.GetThemeImageURL("spacer.gif"))
                wr.AddAttribute(HtmlTextWriterAttribute.Alt, "")
                wr.RenderBeginTag(HtmlTextWriterTag.Img) ' <Img>
                wr.RenderEndTag() ' </Img>
                ddlViewDescending.RenderControl(wr)
            End If

            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) ' </tr>   

            ' Notifications row
            RenderRowBegin(wr) '<tr>
            RenderCellBegin(wr, "", "", "", "right", "", "2", "")   ' <td> 
            wr.Write("<br />")

            ' Display tracking option if user is authenticated and post count > 0 and user not track parent forum (make sure tracking is enabled)
            'CP - Seperating so we can show user they are tracking at forum level if need be
            If (PostCollection.Count > 0) AndAlso (CurrentForumUser.UserID > 0) And (objConfig.MailNotification) Then
                If objSecurity.IsForumAdmin Then
                    cmdThreadSubscribers.RenderControl(wr)
                    wr.Write("<br />")
                End If

                If TrackedForum Then
                Else

                    chkEmail.RenderControl(wr)
                End If
            End If

            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) ' </tr>

            'Close the table
            RenderTableEnd(wr) ' </table> 

            RenderCellEnd(wr) ' </td> 
            RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "")
            RenderRowEnd(wr) ' </tr>   

            ' render bottom spacer row
            RenderRowBegin(wr) '<tr>
            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td> 
            RenderCellEnd(wr) ' </td> 
            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td> 
            wr.Write("<br />")
            RenderCellEnd(wr) ' </td> 
            RenderCellBegin(wr, "", "", "", "", "", "", "") ' <td> 
            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) ' </tr>  
        End Sub

        ''' <summary>
        ''' Renders available post reply/quote/moderate, etc.  buttons
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <param name="Post"></param>
        ''' <remarks>
        ''' </remarks>
        Private Sub RenderCommands(ByVal wr As HtmlTextWriter, ByVal Post As PostInfo)
            Dim author As ForumUserInfo = Post.Author
            Dim url As String = String.Empty

            ' Render reply/mod buttons if necessary
            ' First see if the user has the ability to post
            ' remove logged on limitation if wishing to implement Anonymous posting
            If CurrentForumUser.UserID > 0 Then
                If (Not objThread.ContainingForum.PublicPosting And objSecurity.IsAllowedToPostRestrictedReply) Or (objThread.ContainingForum.PublicPosting = True) Then
                    ' move link (logged user is admin and this is the first post in the thread)
                    'start table for mod/reply buttons
                    RenderTableBegin(wr, "tblCommand_" & Post.PostID.ToString, "", "", "", "4", "0", "", "", "0")     ' <table>
                    RenderRowBegin(wr)


                    '' ATC Start
                    'RenderCellBegin(wr, "Forum_ReplyCell", "", "", "", "", "", "")
                    'If CurrentForumUser.IsBanned Then
                    '    RenderLinkButton(wr, "www.google.com", "", "ThumbsUpButton", False)
                    'Else
                    '    RenderLinkButton(wr, "www.google.com", "", "ThumbsUpButton")
                    'End If
                    'RenderCellEnd(wr)

                    'RenderCellBegin(wr, "Forum_ReplyCell", "", "", "", "", "", "")
                    'If CurrentForumUser.IsBanned Then
                    '    RenderLinkButton(wr, "www.down.com", "", "ThumbsDownButton", False)
                    'Else
                    '    RenderLinkButton(wr, "www.down.com", "", "ThumbsDownButton")
                    'End If
                    'RenderCellEnd(wr)


                    'Never Remove LoggedOnUserID limitation EVEN if wishing to implement Anonymous Posting - ParentPostID is so we know this is the first post in a thread to move it
                    If CurrentForumUser.UserID > 0 And (objSecurity.IsForumModerator) AndAlso (Post.ParentPostID = 0) Then
                        url = Utilities.Links.ThreadMoveLink(TabID, ModuleID, ForumID, ThreadID)

                        RenderCellBegin(wr, "Forum_ReplyCell", "", "", "", "", "", "")
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("Move"), "Forum_Link")
                        RenderCellEnd(wr)
                    ElseIf CurrentForumUser.UserID > 0 And (objSecurity.IsForumModerator) Then
                        ' We dont care about splitting in Ourspace
                        ' Split thread
                        'url = Utilities.Links.ThreadSplitLink(TabID, ModuleID, ForumID, Post.PostID)

                        ' RenderCellBegin(wr, "Forum_ReplyCell", "", "", "", "", "", "")
                        ' RenderLinkButton(wr, url, ForumControl.LocalizedText("Split"), "Forum_Link")
                        ' RenderCellEnd(wr)
                    End If

                    'Never Remove LoggedOnUserID limitation EVEN if wishing to implement Anonymous Posting
                    If CurrentForumUser.UserID > 0 AndAlso (objSecurity.IsForumModerator) Then
                        url = Utilities.Links.PostDeleteLink(TabID, ModuleID, ForumID, Post.PostID, False)

                        RenderCellBegin(wr, "Forum_ReplyCell", "", "", "", "", "", "")
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("Delete"), "Forum_Link")
                        RenderCellEnd(wr)
                    End If

                    'Never Remove LoggedOnUserID limitation EVEN if wishing to implement Anonymous Posting - Anonymous cannot edit post
                    If CurrentForumUser.UserID > 0 AndAlso (objSecurity.IsForumModerator) Then
                        url = Utilities.Links.NewPostLink(TabID, ForumID, Post.PostID, "edit", ModuleID)

                        RenderCellBegin(wr, "Forum_ReplyCell", "", "", "", "", "", "")
                        RenderLinkButton(wr, url, ForumControl.LocalizedText("Edit"), "Forum_Link")
                        RenderCellEnd(wr)
                        'don't allow non mod, forum admin or anything other than a moderator to edit a closed forum post (if the forum is not moderated, or the user is trusted)
                    ElseIf CurrentForumUser.UserID > 0 And (Post.ParentThread.ContainingForum.IsActive) And ((CurrentForumUser.UserID = Post.Author.UserID) AndAlso (Post.ParentThread.ContainingForum.IsModerated = False Or author.IsTrusted Or objSecurity.IsUnmoderated)) Then

                        '[skeel] check for PostEditWindow
                        If objConfig.PostEditWindow = 0 Then
                            url = Utilities.Links.NewPostLink(TabID, ForumID, Post.PostID, "edit", ModuleID)
                            RenderCellBegin(wr, "Forum_ReplyCell", "", "", "", "", "", "")

                            If CurrentForumUser.IsBanned Then
                                'RenderLinkButton(wr, url, ForumControl.LocalizedText("Edit"), "Forum_Link", False)
                            Else
                                ' RenderLinkButton(wr, url, ForumControl.LocalizedText("Edit"), "Forum_Link")
                            End If

                            RenderCellEnd(wr)
                        Else
                            If Post.CreatedDate.AddMinutes(CDbl(objConfig.PostEditWindow)) > Now Then
                                url = Utilities.Links.NewPostLink(TabID, ForumID, Post.PostID, "edit", ModuleID)
                                RenderCellBegin(wr, "Forum_ReplyCell", "", "", "", "", "", "")

                                If CurrentForumUser.IsBanned Then
                                    RenderLinkButton(wr, url, ForumControl.LocalizedText("Edit"), "Forum_Link", False)
                                Else
                                    RenderLinkButton(wr, url, ForumControl.LocalizedText("Edit"), "Forum_Link")
                                End If

                                RenderCellEnd(wr)
                            End If
                        End If
                    End If

                    'First check if the thread is opened, if not then handle for single situation
                    If CurrentForumUser.UserID > 0 AndAlso (Not Post.ParentThread.IsClosed) And (Post.ParentThread.ContainingForum.IsActive) Then
                        If Not Post.ParentThread.ContainingForum.PublicPosting Then
                            ' see if user can reply
                            If objSecurity.IsAllowedToPostRestrictedReply Then
                                url = Utilities.Links.NewPostLink(TabID, ForumID, Post.PostID, "quote", ModuleID)
                                ' Quote link
                                RenderCellBegin(wr, "Forum_ReplyCell", "", "", "", "", "", "")
                                If CurrentForumUser.IsBanned Then
                                    RenderLinkButton(wr, url, ForumControl.LocalizedText("Quote"), "Forum_Link", False)
                                Else
                                    RenderLinkButton(wr, url, ForumControl.LocalizedText("Quote"), "Forum_Link")
                                End If
                                RenderCellEnd(wr)

                                url = Utilities.Links.NewPostLink(TabID, ForumID, Post.PostID, "reply", ModuleID)

                                ' Reply link                    
                                RenderCellBegin(wr, "Forum_ReplyCell", "", "", "", "", "", "")
                                If CurrentForumUser.IsBanned Then
                                    ' ATC Removed link buttons
                                    'RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link", False)
                                Else
                                    ' ATC Removed link buttons
                                    'RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link")
                                End If
                                RenderCellEnd(wr)
                            End If
                        Else
                            url = Utilities.Links.NewPostLink(TabID, ForumID, Post.PostID, "quote", ModuleID)
                            ' Quote link
                            RenderCellBegin(wr, "Forum_ReplyCell", "", "", "", "", "", "")
                            If CurrentForumUser.IsBanned Then
                                ' ATC Removed link buttons
                                '   RenderLinkButton(wr, url, ForumControl.LocalizedText("Quote"), "Forum_Link", False)
                            Else
                                ' ATC Removed link buttons
                                '  RenderLinkButton(wr, url, ForumControl.LocalizedText("Quote"), "Forum_Link")
                            End If
                            RenderCellEnd(wr)



                            url = Utilities.Links.NewPostLink(TabID, ForumID, Post.PostID, "reply", ModuleID)

                            ' Reply link                    
                            RenderCellBegin(wr, "Forum_ReplyCell", "", "", "", "", "", "")
                            If CurrentForumUser.IsBanned Then
                                ' ATC Removed link buttons
                                'RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link", False)
                            Else
                                ' ATC Removed link buttons
                                'RenderLinkButton(wr, url, ForumControl.LocalizedText("Reply"), "Forum_Link")
                            End If
                            RenderCellEnd(wr)
                        End If
                    End If

                    RenderRowEnd(wr) ' </tr>
                    RenderTableEnd(wr) ' </table>
                Else
                    ' User cannot post, which means no moderation either
                    RenderTableBegin(wr, "tblCommand_" & Post.PostID.ToString, "", "", "", "4", "0", "", "", "0")     ' <table>
                    RenderRowBegin(wr)
                    RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "left")
                    RenderRowEnd(wr) ' </tr>
                    RenderTableEnd(wr) ' </table>
                End If
            Else
                ' User cannot post, which means no moderation either
                RenderTableBegin(wr, "tblCommand_" & Post.PostID.ToString, "", "", "", "4", "0", "", "", "0")     ' <table>
                RenderRowBegin(wr)
                RenderCapCell(wr, objConfig.GetThemeImageURL("spacer.gif"), "", "left")
                RenderRowEnd(wr) ' </tr>
                RenderTableEnd(wr) ' </table>
            End If
        End Sub

        ''' <summary>
        ''' Determins if post is even or odd numbered row
        ''' </summary>
        ''' <param name="Count"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' </remarks>
        Private Function ThreadIsEven(ByVal Count As Integer) As Boolean
            If Count Mod 2 = 0 Then
                'even
                Return True
            Else
                'odd
                Return False
            End If
        End Function

        ''' <summary>
        ''' Just relevant to paging
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <param name="PageCount"></param>
        ''' <remarks>
        ''' </remarks>
        Private Sub RenderPostPaging(ByVal wr As HtmlTextWriter, ByVal PageCount As Integer)
            ' First, previous, next, last thread hyperlinks
            Dim backwards As Boolean
            Dim forwards As Boolean
            Dim url As String = String.Empty

            If PostPage <> 0 Then
                backwards = True
            End If

            If PostPage <> PageCount - 1 Then
                forwards = True
            End If

            If (backwards) Then
                ' < Previous 
                url = Utilities.Links.ContainerViewThreadPagedLink(TabID, ForumID, ThreadID, PostPage)
                wr.AddAttribute(HtmlTextWriterAttribute.Href, url)
                wr.AddAttribute(HtmlTextWriterAttribute.Class, "Forum_FooterText")
                wr.RenderBeginTag(HtmlTextWriterTag.A) '<a>
                wr.Write(ForumControl.LocalizedText("Previous"))
                wr.RenderEndTag() ' </A>

                wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                wr.AddAttribute(HtmlTextWriterAttribute.Src, objConfig.GetThemeImageURL("spacer.gif"))
                wr.AddAttribute(HtmlTextWriterAttribute.Alt, "")
                wr.RenderBeginTag(HtmlTextWriterTag.Img) ' <Img>
                wr.RenderEndTag() ' </Img>
            End If

            ' If thread spans several pages, then display text like (Page 1, 2, 3, ..., 5)
            Dim displayPage As Integer = PostPage + 1
            Dim startCap As Integer = Math.Max(4, displayPage - 1)
            Dim endCap As Integer = Math.Min(PageCount - 1, displayPage + 1)
            Dim sepStart As Boolean = False
            Dim sepEnd As Boolean = False
            Dim iPost As Integer

            For iPost = 1 To PageCount
                url = Utilities.Links.ContainerViewThreadPagedLink(TabID, ForumID, ThreadID, iPost)

                If iPost <= 3 Then
                    If iPost <> displayPage Then
                        wr.AddAttribute(HtmlTextWriterAttribute.Href, url)
                    End If
                    wr.AddAttribute(HtmlTextWriterAttribute.Class, "Forum_FooterText")
                    wr.RenderBeginTag(HtmlTextWriterTag.A) '<a>
                    wr.Write(iPost)
                    wr.RenderEndTag() ' </A>

                    wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                    wr.AddAttribute(HtmlTextWriterAttribute.Src, objConfig.GetThemeImageURL("spacer.gif"))
                    wr.AddAttribute(HtmlTextWriterAttribute.Alt, "")
                    wr.RenderBeginTag(HtmlTextWriterTag.Img) ' <Img>
                    wr.RenderEndTag() ' </Img>
                End If

                If (iPost > 3 AndAlso iPost < startCap) AndAlso (Not sepStart) Then
                    wr.AddAttribute(HtmlTextWriterAttribute.Class, "Forum_Link")
                    wr.AddAttribute(HtmlTextWriterAttribute.Id, "spStartCap")
                    wr.RenderBeginTag(HtmlTextWriterTag.Span) ' <span>
                    wr.Write("...")
                    wr.RenderEndTag() ' </Span>
                    sepStart = True
                End If

                If iPost >= startCap AndAlso iPost <= endCap Then
                    If iPost <> displayPage Then
                        'wr.AddAttribute(HtmlTextWriterAttribute.Href, GetURL(Document, Page, String.Format("threadpage={0}", iPost), "postid=&action="))
                        wr.AddAttribute(HtmlTextWriterAttribute.Href, url)
                    End If
                    wr.AddAttribute(HtmlTextWriterAttribute.Class, "Forum_FooterText")
                    wr.RenderBeginTag(HtmlTextWriterTag.A)
                    wr.Write(iPost)
                    wr.RenderEndTag() ' A

                    wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                    wr.AddAttribute(HtmlTextWriterAttribute.Src, objConfig.GetThemeImageURL("spacer.gif"))
                    wr.AddAttribute(HtmlTextWriterAttribute.Alt, "")
                    wr.RenderBeginTag(HtmlTextWriterTag.Img) ' <Img>
                    wr.RenderEndTag() ' </Img>
                End If

                If (iPost > 3) AndAlso (iPost > endCap AndAlso iPost < PageCount) AndAlso (Not sepEnd) Then
                    wr.AddAttribute(HtmlTextWriterAttribute.Class, "Forum_FooterText")
                    wr.AddAttribute(HtmlTextWriterAttribute.Id, "spEndCap")
                    wr.RenderBeginTag(HtmlTextWriterTag.Span) '<span>
                    wr.Write("...")
                    wr.RenderEndTag() ' </Span>
                    sepEnd = True
                End If

                If iPost = PageCount AndAlso iPost > 3 Then
                    If iPost <> displayPage Then
                        wr.AddAttribute(HtmlTextWriterAttribute.Href, url)
                    End If
                    wr.AddAttribute(HtmlTextWriterAttribute.Class, "Forum_FooterText")
                    wr.RenderBeginTag(HtmlTextWriterTag.A) ' <a>
                    wr.Write(iPost)
                    wr.RenderEndTag() ' </A>

                    wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                    wr.AddAttribute(HtmlTextWriterAttribute.Src, objConfig.GetThemeImageURL("spacer.gif"))
                    wr.AddAttribute(HtmlTextWriterAttribute.Alt, "")
                    wr.RenderBeginTag(HtmlTextWriterTag.Img) ' <Img>
                    wr.RenderEndTag() ' </Img>
                End If
            Next

            If (forwards) Then
                ' Next >
                url = Utilities.Links.ContainerViewThreadPagedLink(TabID, ForumID, ThreadID, PostPage + 2)
                wr.AddAttribute(HtmlTextWriterAttribute.Href, url)
                wr.AddAttribute(HtmlTextWriterAttribute.Class, "Forum_FooterText")
                wr.RenderBeginTag(HtmlTextWriterTag.A) '<a>
                wr.Write(ForumControl.LocalizedText("Next"))
                wr.RenderEndTag() ' </A>

                wr.AddAttribute(HtmlTextWriterAttribute.Border, "0")
                wr.AddAttribute(HtmlTextWriterAttribute.Src, objConfig.GetThemeImageURL("spacer.gif"))
                wr.AddAttribute(HtmlTextWriterAttribute.Alt, "")
                wr.RenderBeginTag(HtmlTextWriterTag.Img) ' <Img>
                wr.RenderEndTag() ' </Img>
            End If
        End Sub

        ''' <summary>
        ''' Builds a bookmark for RenderPost, used to navigate directly to a specific post
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <param name="BookMark"></param>
        ''' <remarks>
        ''' </remarks>
        Private Sub RenderPostBookmark(ByVal wr As HtmlTextWriter, ByVal BookMark As String)
            wr.Write("<a name=""" & BookMark & """></a>")
        End Sub

        ''' <summary>
        ''' Renders a textbox on the screen for a quickly reply to threads.
        ''' </summary>
        ''' <param name="wr"></param>
        ''' <remarks></remarks>
        Private Sub QuickReply(ByVal wr As HtmlTextWriter)
            RenderRowBegin(wr) '<tr>
            RenderCellBegin(wr, "", "", "100%", "left", "middle", "2", "") ' <td> 
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "", "0") ' <table>
            RenderRowBegin(wr) ' <tr>
            RenderCapCell(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "Forum_HeaderCapLeft", "") ' <td><img/></td>
            RenderCellBegin(wr, "Forum_Header", "", "100%", "", "", "", "") ' <td>
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "", "0") ' <table>
            RenderRowBegin(wr) ' <tr>
            RenderCellBegin(wr, "", "", "100%", "", "", "", "")  ' <td>
            RenderDivBegin(wr, "", "Forum_HeaderText") ' <span>
            wr.Write("&nbsp;" & "Quick Reply")
            RenderDivEnd(wr) ' </span>
            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) ' </tr>   
            RenderTableEnd(wr) ' </table>  
            RenderCellEnd(wr) ' </td>   
            RenderCapCell(wr, objConfig.GetThemeImageURL("headfoot_height.gif"), "Forum_HeaderCapRight", "") ' <td><img/></td>
            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr>  

            ' Show quick reply textbox row
            RenderRowBegin(wr) '<tr>
            RenderCellBegin(wr, "Forum_UCP_HeaderInfo", "", "", "left", "middle", "2", "") ' <td> 
            RenderTableBegin(wr, "", "", "", "100%", "0", "0", "", "", "0") ' <table>
            RenderRowBegin(wr) ' <tr>
            RenderCellBegin(wr, "", "", "125px", "", "top", "", "") ' <td>
            RenderDivBegin(wr, "", "Forum_NormalBold") ' <span>
            wr.Write("&nbsp;" & "Body")
            RenderDivEnd(wr) ' </span>
            RenderCellEnd(wr) ' </td> 
            RenderCellBegin(wr, "", "", "", "left", "", "", "") ' <td>
            txtQuickReply.RenderControl(wr)
            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </td>
            RenderRowEnd(wr) ' </tr>  

            ' Submit Row
            RenderRowBegin(wr) '<tr>
            RenderCellBegin(wr, "", "", "", "center", "middle", "2", "")    ' <td> 
            RenderTableBegin(wr, "", "", "", "125px", "0", "0", "", "", "0")    ' <table>
            RenderRowBegin(wr) ' <tr>
            RenderCellBegin(wr, "Forum_NavBarButton", "", "125px", "", "", "", "")  ' <td>
            cmdSubmit.RenderControl(wr)
            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) ' </tr>
            RenderTableEnd(wr) ' </table>
            RenderCellEnd(wr) ' </td> 
            RenderRowEnd(wr) ' </tr>  
        End Sub

        ''' <summary>
        ''' Renders structure of Advertisement content
        ''' </summary>
        ''' <history>
        ''' 	[b.waluszko]	21/10/2010	Created
        ''' </history>
        Private Sub RenderAdvertisementPost(ByVal wr As HtmlTextWriter)
            RenderRowBegin(wr) ' <tr>
            wr.Write("<td class=""AdvertisementPost"" colspan=""2"">")
            wr.Write(objConfig.AdvertisementText)

            'check if there are some banners to render
            Dim advertController As New AdvertController
            Dim bannerController As New Vendors.BannerController

            Dim adverts As IEnumerable(Of AdvertInfo)
            adverts = advertController.VendorsGet(Me.ModuleID).Where(Function(ad) ad.IsEnabled = True)

            'first check vendors
            If (adverts IsNot Nothing) AndAlso adverts.Count > 0 Then
                wr.Write("<br/>")
                For Each advert As AdvertInfo In adverts
                    Dim banners As List(Of Vendors.BannerInfo)

                    'second check banners connected to vendors
                    banners = advertController.BannersGet(advert.VendorId)
                    If (banners IsNot Nothing) AndAlso banners.Count > 0 Then
                        For Each b As Vendors.BannerInfo In banners
                            advertController.BannerViewIncrement(b.BannerId)
                            Dim fileController As New FileController
                            Dim fileInfo As DotNetNuke.Services.FileSystem.FileInfo = fileController.GetFileById(Integer.Parse(b.ImageFile.Split(Char.Parse("="))(1)), PortalID)
                            wr.Write(bannerController.FormatBanner(advert.VendorId, b.BannerId, b.BannerTypeId, b.BannerName, fileInfo.RelativePath, b.Description, b.URL, b.Width, b.Height, "L", objConfig.CurrentPortalSettings.HomeDirectory) & "&nbsp;")
                        Next

                    End If

                Next
            End If

            wr.Write("</td>")
            RenderRowEnd(wr) ' </tr>
        End Sub

        ' ATC Start
        Protected Sub VotePost(ByVal postID As Integer, ByVal vote As Boolean)
            Dim cntPost As New PostController
            cntPost.PostAddThumbs(postID, ProfileUserID, vote)
        End Sub

        ' ATC Start
        Protected Sub SwitchPostToProposal(ByVal postID As Integer)
            Dim cntPost As New PostController
            cntPost.SwitchPostToProposal(postID)
        End Sub

        Protected Sub SwitchPostToFeedback(ByVal postID As Integer)
            Dim cntPost As New PostController
            cntPost.SwitchPostToFeedback(postID)
        End Sub



        Protected Function UserHasVotedPost(ByVal postID As Integer, ByVal vote As Integer) As Boolean
            Dim cntPost As New PostController
            Dim test As New Boolean
            test = cntPost.PostUserCheck(postID, ProfileUserID)
            Return cntPost.PostUserCheck(postID, ProfileUserID)
        End Function

        '
        '  PostGetThumbs


        Function GetOurSpaceUserImage(ByVal userId As Integer) As String
            Dim strPath As String = HttpContext.Current.Server.MapPath(".\\Portals\\" & PortalID & "\\Users\\" & userId.ToString("000") + "\\")
            If (IO.Directory.Exists(strPath)) Then

                If (userId <= 9) Then

                    strPath = (ResolveUrl("~/Portals/" & PortalID & "/Users/" & userId.ToString("000") & "/" & userId.ToString("00")))

                Else

                    strPath = ResolveUrl("~/Portals/" & PortalID & "/Users/" + userId.ToString("000") & "/" & userId)

                End If
                strPath &= "/" & userId & "/" & userId & "_50.jpg?" & DateTime.Now.Ticks
                Return strPath

            End If
            Return ResolveUrl("~/images/no-avatar.png")
        End Function

        ' ATC End

#End Region

    End Class

End Namespace