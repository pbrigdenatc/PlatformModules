<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_News.View" AutoEventWireup="false"
    CodeBehind="View.ascx.cs" %>

<%--<p style="text-align: right; margin-bottom: 25px;">
  
</p>--%>
<asp:SqlDataSource ID="sqldtsrc_TopTopics" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
    
    
    SelectCommand="SELECT TOP (3) Forum_Forums.Name,
 Forum_Threads.Replies, Forum_Threads.ThreadID, Forum_Threads.ForumID, 
 Forum_Posts.Subject, Ourspace_Forum_Thread_Info.ThreadLanguage, 
 Forum_Posts.Body, Ourspace_Forum_Thread_Info.phaseId
  FROM Forum_Threads INNER JOIN Forum_Forums 
  ON Forum_Threads.ForumID = Forum_Forums.ForumID 
  INNER JOIN Forum_Posts ON Forum_Threads.ThreadID = Forum_Posts.ThreadID
   INNER JOIN Ourspace_Forum_Thread_Info 
   ON    Forum_Threads.ThreadID = Ourspace_Forum_Thread_Info.ThreadId
     WHERE (Forum_Posts.ParentPostID = 0 AND Ourspace_Forum_Thread_Info.ThreadLanguage = @lang AND Ourspace_Forum_Thread_Info.ThreadLanguage <> 'el-GR')   OR ((@lang = 'el-GR') AND Forum_Posts.ParentPostID = 0  AND  (Forum_Threads.ThreadID = 1945 OR Forum_Threads.ThreadID = 2203)) ORDER BY Forum_Threads.Replies DESC">
    <SelectParameters>
        <asp:SessionParameter Name="lang" SessionField="newsTopTopicsLang" />
    </SelectParameters>
</asp:SqlDataSource>
<div class="title-wrapper recent-events-title-wrapper">

    <h2>
        <asp:Label ID="lbl_RecentActivitiesTitle" resourcekey="RecentActivities" runat="server" Text="lbl_TopTopics"></asp:Label></h2>
    <asp:LinkButton ID="lnkbtn_ViewRecentActivitiesLangSwitchAll" 
        CssClass="all-topics-link" resourcekey="RecentActivitiesAllLanguages" 
        runat="server" onclick="lnkbtn_ViewRecentActivitiesLangSwitchAll_Click"></asp:LinkButton>
           <asp:LinkButton ID="lnkbtn_ViewRecentActivitiesLangSwitchOwn" 
        CssClass="all-topics-link" resourcekey="RecentActivitiesOwnLanguage" visible="false"
        runat="server" onclick="lnkbtn_ViewRecentActivitiesLangSwitchOwn_Click"></asp:LinkButton>
        

        <asp:LinkButton ID="lnkbtn_ViewRecentActivitiesLangEu" 
        CssClass="all-topics-link" resourcekey="RecentActivitiesEu" visible="false"
        runat="server" onclick="lnkbtn_ViewRecentActivitiesLangSwitchEu_Click"></asp:LinkButton>

        </div>
<asp:SqlDataSource ID="sqldtsrc_RecentActivities" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
        
        
    
    SelectCommand="SELECT TOP (2) Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage, Ourspace_Forum_Thread_Info.ThreadId, Forum_Threads.ForumID FROM Forum_Posts INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Posts.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Threads ON Forum_Posts.ThreadID = Forum_Threads.ThreadID INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID WHERE (Forum_Groups.ModuleID = 381) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = @lang) OR (Forum_Groups.ModuleID = 381) AND (@allLang = 1) ORDER BY Forum_Posts.CreatedDate DESC">
    <SelectParameters>
        <asp:SessionParameter Name="lang" SessionField="newsRecentActivitiesLang" />
        <asp:SessionParameter DefaultValue="2" Name="allLang" 
            SessionField="recentActivitiesAllLang" />
    </SelectParameters>
    </asp:SqlDataSource>



    <asp:SqlDataSource ID="sqldtsrc_RecentActivitiesEu" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
        
        
    
    SelectCommand="SELECT TOP (2) Forum_Posts.Subject, Forum_Posts.Body, Forum_Posts.CreatedDate, Forum_Posts.UserID, Ourspace_Forum_Thread_Info.ThreadLanguage, Ourspace_Forum_Thread_Info.ThreadId, Forum_Threads.ForumID FROM Forum_Posts INNER JOIN Ourspace_Forum_Thread_Info ON Forum_Posts.ThreadID = Ourspace_Forum_Thread_Info.ThreadId INNER JOIN Forum_Threads ON Forum_Posts.ThreadID = Forum_Threads.ThreadID INNER JOIN Forum_Forums ON Forum_Threads.ForumID = Forum_Forums.ForumID INNER JOIN Forum_Groups ON Forum_Forums.GroupID = Forum_Groups.GroupID WHERE (Forum_Groups.ModuleID = 381) AND (Ourspace_Forum_Thread_Info.ThreadLanguage = 'en-EU') AND (Forum_Groups.ModuleID = 381) ORDER BY Forum_Posts.CreatedDate DESC">
    </asp:SqlDataSource>


<asp:ListView ID="lstvw_RecentActivities" runat="server" DataSourceID="sqldtsrc_RecentActivities"
    EnableModelValidation="True" OnItemDataBound="lstvw_RecentActivities_ItemDataBound">
  
    <EmptyDataTemplate>
        <table runat="server" style="">
            <tr>
                <td>
                    No data was returned. </td></tr></table></EmptyDataTemplate>
                    
                   
    <ItemTemplate>
        <tr style="">
            <td valign="top" class="news-user-thumb">
                <asp:Image ID="img_profileMini" runat="server" />
            </td>
            <td valign="top">
                <b>
                    <asp:HyperLink ID="hprlnk_UserProfile" runat="server" CssClass="bold-link">
                        <asp:Label ID="lbl_Name" runat="server" Text="Label"></asp:Label>
                    </asp:HyperLink></b>
                    <asp:Label ID="Label1" runat="server" resourcekey="HadHisSay" Text="had his say in"></asp:Label>
                    <asp:HyperLink ID="hprlnk_Topic" runat="server" CssClass="bold-link">
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></asp:HyperLink>&nbsp; <p>
                  <asp:Label ID="BodyLabel"
                        runat="server" Text='<%# Eval("Body") %>' />
                   </p></td><td valign="top" class="flag-wrap">
                <asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>'
                    Visible="false" />
                <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fleft">
                </div>
                <asp:Label ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' />
                <asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' />
                <asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("UserID") %>' />
                <asp:Label ID="ThreadLanguageLabel" runat="server" Visible="false" Text='<%# Eval("ThreadLanguage") %>' />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border-bottom: 1px dotted #b7d3ec;">
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
     <tr style="">
            <td valign="top"  class="news-user-thumb alt-item">
                <asp:Image ID="img_profileMini" runat="server" />
            </td>
            <td valign="top" class="alt-item">
                <b>
                    <asp:HyperLink ID="hprlnk_UserProfile" runat="server" CssClass="bold-link">
                        <asp:Label ID="lbl_Name" runat="server" Text="Label"></asp:Label>
                    </asp:HyperLink></b>&nbsp;<asp:Label ID="Label1" runat="server" resourcekey="HadHisSay" Text="had his say in"></asp:Label>&nbsp;<asp:HyperLink ID="hprlnk_Topic" runat="server" CssClass="bold-link">
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></asp:HyperLink>&nbsp; <p>
                  <asp:Label ID="BodyLabel"
                        runat="server" Text='<%# Eval("Body") %>' />
                   </p></td><td valign="top" class="alt-item flag-wrap">
                <asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>'
                    Visible="false" />
                <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fleft">
                </div>
                <asp:Label ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' />
                <asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' />
                <asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("UserID") %>' />
                <asp:Label ID="ThreadLanguageLabel" runat="server" Visible="false" Text='<%# Eval("ThreadLanguage") %>' />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border-bottom: 1px dotted #b7d3ec;" class="alt-item">
            </td>
        </tr>
    </AlternatingItemTemplate>
    <LayoutTemplate>
        <table runat="server" class="recent-events-table-wrap" cellpadding="0" cellspacing="0">
            <tr runat="server">
                <td runat="server">
                    <table id="itemPlaceholderContainer" class="recent-events-table" runat="server" border="0"
                        style="">
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server">
                <td runat="server" style="">
                </td>
            </tr>
        </table>
    </LayoutTemplate>
   
</asp:ListView>








<asp:ListView ID="lstvw_recentActivitiesEu" runat="server" DataSourceID="sqldtsrc_RecentActivitiesEu"
    EnableModelValidation="True" OnItemDataBound="lstvw_RecentActivities_ItemDataBound">
  
    <EmptyDataTemplate>
        <table id="Table5" runat="server" style="">
            <tr>
                <td>
                    No data was returned. </td></tr></table></EmptyDataTemplate>
                    
                   
    <ItemTemplate>
        <tr style="">
            <td valign="top" class="news-user-thumb">
                <asp:Image ID="img_profileMini" runat="server" />
            </td>
            <td valign="top">
                <b>
                    <asp:HyperLink ID="hprlnk_UserProfile" runat="server" CssClass="bold-link">
                        <asp:Label ID="lbl_Name" runat="server" Text="Label"></asp:Label>
                    </asp:HyperLink></b>
                    <asp:Label ID="Label1" runat="server" resourcekey="HadHisSay" Text="had his say in"></asp:Label>
                    <asp:HyperLink ID="hprlnk_Topic" runat="server" CssClass="bold-link">
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></asp:HyperLink>&nbsp; <p>
                  <asp:Label ID="BodyLabel"
                        runat="server" Text='<%# Eval("Body") %>' />
                   </p></td><td valign="top" class="flag-wrap">
                <asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>'
                    Visible="false" />
                <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fleft">
                </div>
                <asp:Label ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' />
                <asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' />
                <asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("UserID") %>' />
                <asp:Label ID="ThreadLanguageLabel" runat="server" Visible="false" Text='<%# Eval("ThreadLanguage") %>' />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border-bottom: 1px dotted #b7d3ec;">
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
     <tr style="">
            <td valign="top"  class="news-user-thumb alt-item">
                <asp:Image ID="img_profileMini" runat="server" />
            </td>
            <td valign="top" class="alt-item">
                <b>
                    <asp:HyperLink ID="hprlnk_UserProfile" runat="server" CssClass="bold-link">
                        <asp:Label ID="lbl_Name" runat="server" Text="Label"></asp:Label>
                    </asp:HyperLink></b>&nbsp;<asp:Label ID="Label1" runat="server" resourcekey="HadHisSay" Text="had his say in"></asp:Label>&nbsp;<asp:HyperLink ID="hprlnk_Topic" runat="server" CssClass="bold-link">
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></asp:HyperLink>&nbsp; <p>
                  <asp:Label ID="BodyLabel"
                        runat="server" Text='<%# Eval("Body") %>' />
                   </p></td><td valign="top" class="alt-item flag-wrap">
                <asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Eval("CreatedDate") %>'
                    Visible="false" />
                <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fleft">
                </div>
                <asp:Label ID="ThreadIDLabel" runat="server" Visible="false" Text='<%# Eval("ThreadID") %>' />
                <asp:Label ID="ForumIDLabel" runat="server" Visible="false" Text='<%# Eval("ForumID") %>' />
                <asp:Label ID="UserIDLabel" runat="server" Visible="false" Text='<%# Eval("UserID") %>' />
                <asp:Label ID="ThreadLanguageLabel" runat="server" Visible="false" Text='<%# Eval("ThreadLanguage") %>' />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border-bottom: 1px dotted #b7d3ec;" class="alt-item">
            </td>
        </tr>
    </AlternatingItemTemplate>
    <LayoutTemplate>
        <table id="Table6" runat="server" class="recent-events-table-wrap" cellpadding="0" cellspacing="0">
            <tr id="Tr5" runat="server">
                <td id="Td5" runat="server">
                    <table id="itemPlaceholderContainer" class="recent-events-table" runat="server" border="0"
                        style="">
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="Tr6" runat="server">
                <td id="Td6" runat="server" style="">
                </td>
            </tr>
        </table>
    </LayoutTemplate>
   
</asp:ListView>















<div class="title-wrapper">
    <h2>
        <asp:Label ID="lbl_TopTopics" runat="server" resourcekey="TopTopics" Text="lbl_TopTopics"></asp:Label></h2><asp:HyperLink ID="hprlnk_forum" CssClass="all-topics-link" resourcekey="AllTopics" runat="server"></asp:HyperLink></div><asp:ListView ID="lstvw_TopTopics" runat="server" DataKeyNames="ThreadID" DataSourceID="sqldtsrc_TopTopics"
    EnableModelValidation="True" OnItemDataBound="lstvw_TopTopics_ItemDataBound">
    <EmptyDataTemplate>
        <table id="Table1" runat="server" style="">
            <tr>
                <td>
                    No data was returned. </td></tr></table></EmptyDataTemplate><ItemSeparatorTemplate> <tr>
         <td colspan="3" style="border-top: 1px dotted #b7d3ec;"  class="alt-item">
            </td>
        </tr></ItemSeparatorTemplate>
    <ItemTemplate>
    <tr>
    <td colspan="3"> <div style="height:3px;"></div></td>
    </tr>
        <tr style="">
       <td runat="server" id="imageTd" class="debate-thumbnail" rowspan="2">
            <asp:Literal ID="ltrlImage" runat="server"></asp:Literal></td><td valign="top" runat="server" id="title_td">
                <h2 class="art-postheader">
                  
                       <asp:HyperLink ID="hprlnk_post" runat="server" CssClass="bold-link debate-title">
                    <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /> </asp:HyperLink></h2></td><td valign="top" align="right" style="">
               <div class="news-info">
                <asp:Label ID="NameLabel" runat="server" Text='<%# GetLocalizedCategory( Eval("Name").ToString()) %>' /> - <asp:Label ID="lbl_Posts" runat="server" resourcekey="Posts" Text="Posts"></asp:Label>&nbsp;<asp:Label ID="RepliesLabel" runat="server" Text='<%# Eval("Replies") %>' />
                <asp:Label ID="ThreadIDLabel" Visible="false" runat="server" Text='<%# Eval("ThreadID") %>' />
                <asp:Label ID="ForumIDLabel" Visible="false" runat="server" Text='<%# Eval("ForumID") %>' />
                <asp:Label ID="PhaseIDLabel" Visible="false" runat="server" Text='<%# Eval("phaseId") %>' />
                
               <%-- <asp:HyperLink ID="hprlnk_post" runat="server" CssClass="black-link">
                    <asp:Label ID="lbl_ReadMore" runat="server" Text="Read more &raquo;"></asp:Label></asp:HyperLink>--%>
                <asp:Label ID="ThreadLanguageLabel" runat="server" Visible="false" Text='<%# Eval("ThreadLanguage") %>' />
             <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fright">
                </div></div></td>
        </tr>
        <tr>
            <td colspan="3" style="padding-bottom: 9px; padding-top: 5px;">
                <asp:Label ID="lbl_Body" runat="server" Text='<%# Eval("Body") %>'></asp:Label></td></tr><%-- <tr>
         <td colspan="3" style="border-top: 1px dotted #b7d3ec;"  class="alt-item">
            </td>
        </tr>--%></ItemTemplate><AlternatingItemTemplate>
    <tr>
    <td colspan="3" class="alt-item"> <div style="height:3px;"></div></td>
    </tr>
    <tr >
       <td runat="server" id="imageTd" class="debate-thumbnail alt-item" rowspan="2">
            <asp:Literal ID="ltrlImage" runat="server"></asp:Literal></td><td valign="top" runat="server" id="title_td" class="alt-item">
                <h2 class="art-postheader">
                  
                       <asp:HyperLink ID="hprlnk_post" runat="server" CssClass="bold-link debate-title">
                    <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /> </asp:HyperLink></h2></td><td valign="top" align="right" class="alt-item">
               <div class="news-info">
                <asp:Label ID="NameLabel" runat="server" Text='<%# GetLocalizedCategory( Eval("Name").ToString()) %>' /> - <asp:Label ID="lbl_Posts" runat="server" resourcekey="Posts" Text="Posts"></asp:Label>:&nbsp;<asp:Label ID="RepliesLabel" runat="server" Text='<%# Eval("Replies") %>' />
                <asp:Label ID="ThreadIDLabel" Visible="false" runat="server" Text='<%# Eval("ThreadID") %>' />
                <asp:Label ID="ForumIDLabel" Visible="false" runat="server" Text='<%# Eval("ForumID") %>' />
                   <asp:Label ID="PhaseIDLabel" Visible="false" runat="server" Text='<%# Eval("phaseId") %>' />
               <%-- <asp:HyperLink ID="hprlnk_post" runat="server" CssClass="black-link">
                    <asp:Label ID="lbl_ReadMore" runat="server" Text="Read more &raquo;"></asp:Label></asp:HyperLink>--%>
                <asp:Label ID="ThreadLanguageLabel" runat="server" Visible="false" Text='<%# Eval("ThreadLanguage") %>' />
             <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fright">
                </div></div></td>
        </tr>
        <tr>
            <td colspan="3" style="padding-bottom: 9px; padding-top: 5px;" class="alt-item">
                <asp:Label ID="lbl_Body" runat="server" Text='<%# Eval("Body") %>'></asp:Label></td></tr><%--   <tr>
         <td colspan="3" style="border-top: 1px dotted #b7d3ec;">
            </td>
        </tr>--%></AlternatingItemTemplate><%--<AlternatingItemTemplate>
        <tr style="">
            <td valign="top">
                <h2 class="art-postheader">
                    <span class="highlighted-green">
                        <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' /></span>
                    <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' /></h2>
            </td>
            <td valign="top" align="right" style="width: 170px; padding-top: 5px;">
                <div class="news_flag small-flag-<%# Eval("ThreadLanguage") %> fleft">
                </div>
                <asp:Label ID="lbl_Posts" runat="server" Text="Posts:"></asp:Label>
                <asp:Label ID="RepliesLabel" runat="server" Text='<%# Eval("Replies") %>' />
                <asp:Label ID="ThreadIDLabel" Visible="false" runat="server" Text='<%# Eval("ThreadID") %>' />
                <asp:Label ID="ForumIDLabel" Visible="false" runat="server" Text='<%# Eval("ForumID") %>' />
                |
                <asp:HyperLink ID="hprlnk_post" runat="server" CssClass="black-link">
                    <asp:Label ID="lbl_ReadMore" runat="server" Text="Read more &raquo;"></asp:Label></asp:HyperLink>
                <asp:Label ID="ThreadLanguageLabel" runat="server" Visible="false" Text='<%# Eval("ThreadLanguage") %>' />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="padding-bottom: 20px; padding-top: 5px;">
                <asp:Label ID="lbl_Body" runat="server" Text='<%# Eval("Body") %>'></asp:Label>
            </td>
        </tr>
    </AlternatingItemTemplate>--%><LayoutTemplate>
        <table id="Table2" runat="server" cellpadding="0" cellspacing="0">
            <tr id="Tr1" runat="server">
                <td id="Td1" runat="server">
                    <table id="itemPlaceholderContainer" class="news-table" runat="server" cellpadding="0"
                        cellspacing="0" border="0" style="">
                        <tr runat="server" id="itemPlaceholder">
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="Tr2" runat="server">
                <td id="Td2" runat="server" style="">
                </td>
            </tr>
        </table>
    </LayoutTemplate>
</asp:ListView>

<div class="title-wrapper">
    <h2>
        <span>
            <asp:Label ID="Label3" runat="server" resourcekey="latestDecisionMakers" Text="Latest Decision Maker Comments"></asp:Label></span></h2></div><asp:ListView ID="ListView1" runat="server" DataSourceID="sqldtsrc_RecentMepActivities"
    EnableModelValidation="True">
  
    <EmptyDataTemplate>
        <table id="Table3" runat="server" style="">
            <tr>
                <td style="padding-top:15px;">
                    There are no recent Decision Maker comments</td></tr></table></EmptyDataTemplate><InsertItemTemplate>
        
    </InsertItemTemplate>
    <ItemTemplate>
        <tr style="">
        <td class="news-user-thumb" valign="top">
<img id="dnn_ctr479_View_lstvw_RecentActivities_ctrl0_img_profileMini" style="border-width:0px;" src='<%# Eval("ThumbnailUrl") %>'>
</td>
            <td valign="top">
                <b>
                    
                        <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
               </b>
                    <asp:Label ID="Label1" runat="server" resourcekey="HadHisSay" Text="had his say in"></asp:Label>
                    
                    <asp:HyperLink ID="hprlnk_Topic" NavigateUrl='<%# Eval("Url") + "#disqus" %>' runat="server" CssClass="bold-link">
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("ThreadName") %>' /></asp:HyperLink>&nbsp; <p>
                  <asp:Label ID="BodyLabel"
                        runat="server" Text='<%# Eval("MessageSummary") %>' /> <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Eval("Url") + "#disqus" %>' runat="server" CssClass="bold-link">
                        <asp:Label ID="Label2" runat="server" Text="Read more.." /></asp:HyperLink>
                   </p></td><td valign="top" class="flag-wrap">

                <div class="news_flag small-flag-<%# Eval("FlagLanguage") %> fleft">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border-bottom: 1px dotted #b7d3ec;">
            </td>
        </tr>
    </ItemTemplate>
    
    <LayoutTemplate>
        <table id="Table4" runat="server" class="recent-events-table-wrap" cellpadding="0" cellspacing="0">
            <tr id="Tr3" runat="server">
                <td id="Td3" runat="server">
                    <table id="itemPlaceholderContainer" class="recent-events-table" runat="server" border="0"
                        style="">
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="Tr4" runat="server">
                <td id="Td4" runat="server" style="">
                </td>
            </tr>
        </table>
    </LayoutTemplate>
   
</asp:ListView>



<div class="hidden">
    <asp:Label ID="lblLog" runat="server" Text=""></asp:Label><br />
    Test: <asp:Label ID="lblTest" runat="server" Text=""></asp:Label></div><asp:SqlDataSource 
    ID="sqldtsrc_RecentMepActivities" runat="server" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
        
        
    
    
    SelectCommand="SELECT TOP (6) * FROM Ourspace_MepComments WHERE (Language = @lang) ORDER BY Position"><SelectParameters>
        <asp:SessionParameter Name="lang" SessionField="newsRecentActivitiesLang" />
        </SelectParameters>
    </asp:SqlDataSource>