<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="AddTemplate.aspx.cs" Inherits="CubeReportingModule.Admin.AddTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <h2>
        <b>Create New Report Template</b>
    </h2>
    <br />
    Tables to select from:
    <br />
    <asp:DropDownList runat="server" ID="FromClause"></asp:DropDownList>
    <br />
    <br />
    Columns to select:
    <br />
    <asp:DropDownList runat="server" ID="SelectClause"></asp:DropDownList>
    <br />
    <br />
    Where clause(optional):
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
