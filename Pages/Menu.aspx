<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CubeReportingModule.Pages.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div id="LeftPane">
        <asp:Button runat="server" id="Adhoc" Text="Create Report"/>
        <asp:Button runat="server" id="Events" Text="Modify Scheduled Reports"/>
        <asp:Button runat="server" id="Help" Text="Help"/>
    </div>
    <div id="RightPane">
        <asp:Button runat="server" ID="report1" Text="Get All Customer Transactions" onclick="report1_Click" />
    </div>
</asp:Content>
