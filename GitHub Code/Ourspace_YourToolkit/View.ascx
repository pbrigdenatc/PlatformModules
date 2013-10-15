<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_YourToolkit.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<!--<div style="margin-left: 15px;"><a href="http://hestia.atc.gr/ourspace/ProposeYourTopic/tabid/74/language/en-GB/Default.aspx" id="propose-your-topics-mini-button"></a> &nbsp;&nbsp; <a style="margin-left: 5px;" href="http://hestia.atc.gr/ourspace/CustomForumFB/tabid/62/Default.aspx" id="view-open-debates-mini-button"></a> <a href="/ourspace/VoteBestProposalFB/tabid/122/Default.aspx" id="vote-proposed-topics-mini-button"></a> <a style="margin-left: 5px;" href="ourspace/Results/tabid/158/language/en-GB/Default.aspx" id="vote-proposed-solutions-mini"></a> <div class="cleared"></div> </div>-->
<p>
    <%--<a class="toolkit-propose-suggest" href="/ourspace/ProposeYourTopic/tabid/74/language/en-GB/Default.aspx">
        </a> <a class="toolkit-propose-join" href="/ourspace/CustomForumFB/tabid/62/Default.aspx">
            </a> <a class="toolkit-propose-vote" href="/ourspace/VoteBestProposalFB/tabid/122/Default.aspx">
                </a> <a class="toolkit-propose-view" href="/ourspace/Results/tabid/158/language/en-GB/Default.aspx">
                    </a>--%>
    <span id="toolkit_active" runat="server" class="toolkit-active"></span>
    <span id="toolkit-wrapper">
    <asp:HyperLink ID="hprlnk_suggest" resourcekey="Suggest" CssClass="toolkit-propose-suggest data-block first_element_to_target"   NavigateUrl="/ourspace/ProposeYourTopic/tabid/74/language/en-GB/Default.aspx" runat="server">Suggest Topic Debate</asp:HyperLink>
    <asp:HyperLink ID="hprlnk_join" resourcekey="Join" CssClass="toolkit-propose-join data-block second_element_to_target"        NavigateUrl="/ourspace/CustomForumFB/tabid/62/Default.aspx" runat="server">Join Open Debates</asp:HyperLink>
    <asp:HyperLink ID="hprlnk_vote" resourcekey="Vote" CssClass="toolkit-propose-vote data-block third_element_to_target"        NavigateUrl="/ourspace/VoteBestProposalFB/tabid/122/Default.aspx"  runat="server">Vote Best Proposal</asp:HyperLink>
    <asp:HyperLink ID="hprlnk_view" resourcekey="View" CssClass="toolkit-propose-view data-block fourth_element_to_target"        NavigateUrl="/ourspace/Results/tabid/158/language/en-GB/Default.aspx" runat="server">View Results</asp:HyperLink>
   </span>
   <!-- AddThis Button BEGIN -->
</p>

    <div id="tlyPageGuideWelcome">
        <p>Welcome to my new page! pageguide is here to help you learn more.</p>
        <button class="tlypageguide_start">let's go</button>
        <button class="tlypageguide_ignore">not now</button>
        <button class="tlypageguide_dismiss">got it, thanks</button>
    </div>
    <div class="hidden">
    <ul id="tlyPageGuide" data-tourtitle="Get to know the OurSpace platform">
      <li class="tlypageguide_top" data-tourtarget=".first_element_to_target">
        <div>
          <b>Suggest Topic Debate (Phase 1):</b> Suggest new topics that you would like to discuss on the OurSpace platform. 
        </div>
      </li>
      <li class="tlypageguide_left" data-tourtarget=".second_element_to_target">
        <div>
          <b>Join Open Debates (Phase 2):</b> Discuss approved topics and propose solutions to the problems raised
        </div>
      </li>
      <li class="tlypageguide_right" data-tourtarget=".third_element_to_target">
        <div>
         <b>Vote Best Proposal (Phase 3):</b> Vote the best solution proposals that came up in the discussions
        </div>
      </li>
      <li class="tlypageguide_bottom" data-tourtarget=".fourth_element_to_target">
        <div>
          <b>View Results (Phase 4):</b> See a summary of the outcome of the topics in their final phase.
        </div>
      </li>
    </ul>
    </div>