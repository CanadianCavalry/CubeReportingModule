<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CubeReportingModule.Pages.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div id="LeftPane">
        <asp:Button runat="server" id="Adhoc" class="MenuButton" Text="Create Report"/>
        <br />
        <asp:Button runat="server" id="Events" class="MenuButton" Text="Modify Scheduled Reports"/>
        <br />
        <asp:Button runat="server" id="Help" class="MenuButton" Text="Help"/>
        <br />
    </div>
    <div id="RightPane">
        <asp:Button runat="server" ID="report1" class="MenuButton" Text="Get All Customer Transactions" onclick="report1_Click" />
        <br />
        <asp:Button runat="server" ID="report2" class="MenuButton" Text="Get All Empty Spaces" onclick="report1_Click" />
        <br />
        <asp:Button runat="server" ID="report3" class="MenuButton" Text="Get All Irregularities" onclick="report1_Click" />
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
    <p style="text-align:center; font-size: 24px;">Admin Control Panel</p>
    <div id="LeftPane">
        <asp:Button runat="server" id="Accounts" class="AdminButton" Text="Manage Accounts"/>
    </div>
    <div id="RightPane">
         <asp:Button runat="server" id="Templates" class="AdminButton" Text="Manage Report Templates"/>
    </div>
</asp:Content>
