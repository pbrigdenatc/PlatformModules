<%@ Control language="C#" Inherits="DotNetNuke.Modules.Ourspace_SearchBox.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
<div id="search" >
    <asp:LinkButton ID="lnkbtn_Search" runat="server" onclick="lnkbtn_Search_Click"></asp:LinkButton>
    <asp:TextBox ID="txtSearchTerms"  runat="server"></asp:TextBox>

								</div>
                                <div class="hidden">
                                    <asp:Label ID="Label1" runat="server" class="searchWatermarkText" resourcekey="Search" Text="Search"></asp:Label></div>