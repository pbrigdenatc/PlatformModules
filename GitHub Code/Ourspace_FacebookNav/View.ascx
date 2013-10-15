<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Ourspace_FacebookNav.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<div id="fb-nav-wrap">
    <div class="tabs-wrapper">
        <div class="cleared">
        </div>
        <%--  <asp:LinkButton ID="lnkbtnNationalDebates" runat="server" Text="" CssClass="tab-active"
                resourcekey="NationalDebates" OnClick="lnkbtnNationalDebates_Click"></asp:LinkButton>
            <asp:LinkButton ID="lnkbtnEuDebates" resourcekey="EuDebates" runat="server" Text="EU Debates"
                CssClass="tab-inactive" OnClick="lnkbtnEuDebates_Click"></asp:LinkButton>--%>
        <div class=" tab-inactive" runat="server" id="dashboardDiv">
            <asp:HyperLink ID="HyperLink6" resourcekey="dashboard" runat="server" CssClass="fb-nav-dashboard"
                NavigateUrl="http://www.joinourspace.eu/tabid/287/language/en-GB/Default.aspx?facebook=1">My Dashboard</asp:HyperLink>
        </div>
        <div class=" tab-inactive" runat="server" id="overviewDiv">
            <asp:HyperLink ID="HyperLink1" CssClass="fb-nav-overview" resourcekey="overview"
                runat="server" NavigateUrl="http://www.joinourspace.eu/FacebookOverview/tabid/251/language/en-GB/Default.aspx?facebook=1">Overview</asp:HyperLink>
        </div>
        <div class=" tab-inactive" runat="server" id="suggestDiv">
            <asp:HyperLink ID="HyperLink2" CssClass="fb-nav-suggest" resourcekey="suggest" runat="server"
                NavigateUrl="http://www.joinourspace.eu/FacebookSuggestTopicDebate/tabid/255/language/en-GB/Default.aspx?facebook=1">Suggest Topic Debate</asp:HyperLink>
        </div>
        <div class=" tab-inactive" runat="server" id="joinDiv">
            <asp:HyperLink ID="HyperLink3" CssClass="fb-nav-join" resourcekey="join" runat="server"
                NavigateUrl="http://www.joinourspace.eu/FacebookJoinTopicDebates/tabid/259/language/en-GB/Default.aspx?facebook=1">Join Topic Debate</asp:HyperLink>
        </div>
        <div class=" tab-inactive" runat="server" id="voteDiv">
            <asp:HyperLink ID="HyperLink4" CssClass="fb-nav-vote" resourcekey="vote" runat="server"
                NavigateUrl="http://www.joinourspace.eu/FacebookVoteBestProposal/tabid/263/language/en-GB/Default.aspx?facebook=1">Vote Best Proposal</asp:HyperLink>
        </div>
        <div class=" tab-inactive" runat="server" id="resultsDiv">
            <asp:HyperLink ID="HyperLink5" CssClass="fb-nav-results" runat="server" resourcekey="results" 
                NavigateUrl="http://www.joinourspace.eu/FacebookViewResults/tabid/267/language/en-GB/Default.aspx?facebook=1">View Results</asp:HyperLink>
        </div>
        <div class="tab-line">
        </div>
    </div>
</div>
