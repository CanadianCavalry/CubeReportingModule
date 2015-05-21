<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" Inherits="CubeReportingModule.Admin.ManageUsers" Codebehind="ManageUsers.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <p align="center"> 
        <asp:Label ID="ActionStatus" runat="server" CssClass="StatusMessage"></asp:Label> 
    </p>
    <h3>Manage Roles By User</h3> 
    <p> 
         <b>Select a User:</b> 
         <asp:DropDownList ID="UserList" runat="server" AutoPostBack="True" 
              DataTextField="UserName" DataValueField="UserName" OnSelectedIndexChanged="UserList_SelectedIndexChanged1"> 

         </asp:DropDownList> 
    </p> 
    <p> 
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
