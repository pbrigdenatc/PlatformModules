<%@ Control language="C#" Inherits="DotNetNuke.Modules.Ourspace_TranslationWidget.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
<%--<fieldset>
			<label for="speedA">Select a Speed:</label>
			<select name="speedA" id="speedA">
				<option value="Slower">Slower</option>
				<option value="Slow">Slow</option>
				<option value="Medium" selected="selected">Medium</option>

				<option value="Fast">Fast</option>
				<option value="Faster">Faster</option>
			</select>
		</fieldset>


        		<fieldset>
			<label for="filesB">Select a File</label>
			<select name="filesB" id="filesB" class="customicons">
				<option value="mypodcast" class="video">John Resig Podcast</option>
				<option value="myvideo" class="podcast">Scott Gonzales Video</option>
				<option value="myrss" class="train">jQuery RSS XML</option>
			</select>

		</fieldset>--%>
        <div id="translator-wrapper">
<asp:Label ID="lblDescription" runat="server" Text="Use the automatic translation tool to translate user replies."></asp:Label>

<%--<select name="speedA" id="translateLanguage">
				<option value="en">English</option>
				<option value="de">German</option>
				<option value="cs">Czech</option>
				<option value="el">Greek</option>
			</select>--%>
           <div id="translator-ddl">
            <asp:DropDownList CausesValidation="false" ID="translateLanguage" CssClass="translateLanguage" runat="server">
            <asp:ListItem Selected="True" resourcekey="TranslateTo" Text="Translate to..." Value="x"></asp:ListItem>
            <asp:ListItem Text="English" resourcekey="English" Value="en"></asp:ListItem>
             <asp:ListItem Text="German" resourcekey="German" Value="de"></asp:ListItem>
              <asp:ListItem Text="Czech" resourcekey="Czech" Value="cs"></asp:ListItem>
               <asp:ListItem Text="Greek" resourcekey="Greek" Value="el"></asp:ListItem>
                <asp:ListItem Text="Original" resourcekey="Original" Value="-"></asp:ListItem>
            </asp:DropDownList>
            </div>

<div id="translating-status">
    <asp:Label ID="Label1" runat="server" Text="Translating"></asp:Label>...</div>
    <div id="translating-complete">
    <asp:Label ID="Label2" runat="server" Text="Translation complete!"></asp:Label></div>
    <div id="reverting-complete">
    <asp:Label ID="Label3" runat="server" Text="Reverted to original language."></asp:Label></div>
    </div>

<asp:Label ID="lblPostReported" CssClass="lblPostReported hidden" resourcekey="postReported" runat="server" Text="Label"></asp:Label>