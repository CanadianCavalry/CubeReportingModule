<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" Inherits="CubeReportingModule.Admin.ManageUsers" Codebehind="ManageUsers.aspx.cs"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <%-- Label to display the result of each operation. Hidden at initial page load --%>
    <div class="UserSelect">
        <p>
            <asp:Label ID="ActionStatus" runat="server" CssClass="StatusMessage"></asp:Label> 
        </p>
             <b>Select a user:</b>
            <br />
            <br /> 
            <%-- Drop down list of all users is populated by database call from code behind --%>
             <asp:DropDownList ID="UserList" runat="server" AutoPostBack="True" 
                  DataTextField="UserName" DataValueField="UserName" OnSelectedIndexChanged="UserList_SelectedIndexChanged1">
             </asp:DropDownList>
    </div>
    <div class="RoleSelect">
        <b>Select the user's new role:</b>
        <br />
        <asp:RadioButtonList runat="server" ID="RoleList" RepeatDirection="Vertical" AutoPostBack="true"
             OnSelectedIndexChanged="RoleList_SelectedIndexChanged">
            <asp:ListItem Text="Client" Value="0" />
            <asp:ListItem Text="BasicUser" Value="1" />
            <asp:ListItem Text="Admin" Value="2" Enabled="false" />
        </asp:RadioButtonList>
        <br /> 
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
