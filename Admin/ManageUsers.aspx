<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" Inherits="CubeReportingModule.Admin.ManageUsers" Codebehind="ManageUsers.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <%-- Label to display the result of each operation. Hidden at initial page load --%>
    <p align="center"> 
        <asp:Label ID="ActionStatus" runat="server" CssClass="StatusMessage"></asp:Label> 
    </p>
    <h3>Manage Roles By User</h3> 
    <p> 
         <b>Select a User:</b> 
        <%-- Drop down list of all users is populated by database call from code behind --%>
         <asp:DropDownList ID="UserList" runat="server" AutoPostBack="True" 
              DataTextField="UserName" DataValueField="UserName" OnSelectedIndexChanged="UserList_SelectedIndexChanged1"> 

         </asp:DropDownList> 
    </p> 
    <p> 
        <%-- Create a checkbox for each available role. Boxes are checked in the code behind --%>
         <asp:Repeater ID="UsersRoleList" runat="server"> 
              <ItemTemplate> 
                   <asp:CheckBox runat="server" ID="RoleCheckBox" AutoPostBack="true" 
                       OnCheckedChanged="RoleCheckBox_CheckChanged"
                        Text='<%# Container.DataItem %>' /> 
                   <br /> 
              </ItemTemplate> 
         </asp:Repeater> 
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
