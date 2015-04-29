<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CubeReportingModule.Pages.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div class="LeftPane">
        <asp:Button runat="server" id="Adhoc" CssClass="MenuButton" Text="Create Report"/>
        <br />
        <asp:Button runat="server" id="Events" CssClass="MenuButton" Text="Modify Scheduled Reports"/>
        <br />
        <asp:Button runat="server" id="Help" CssClass="MenuButton" Text="Help"/>
        <br />
    </div>
    <div class="RightPane">
        <asp:Button runat="server" ID="report1" CssClass="MenuButton" CommandName="report" CommandArgument="1" Text="Get All Customer Transactions" onclick="reportButton_Click" />
        <br />
        <asp:Button runat="server" ID="report2" CssClass="MenuButton" CommandName="report" CommandArgument="2" Text="Get All Empty Spaces" onclick="reportButton_Click" />
        <br />
        <asp:Button runat="server" ID="report3" CssClass="MenuButton" CommandName="report" CommandArgument="3" Text="Get All Irregularities" onclick="reportButton_Click" />
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
    <p style="text-align:center; font-size: 24px;">Admin Control Panel</p>
    <div class="LeftPane">
        <asp:Button runat="server" id="Accounts" CssClass="AdminButton" Text="Manage Accounts"/>
    </div>
    <div class="CenterPane">
        <asp:Button runat="server" ID="Logs" CssClass="AdminButton" Text="View Access Logs" />
    </div>
    <div class="RightPane">
         <asp:Button runat="server" id="Templates" CssClass="AdminButton" Text="Manage Report Templates"/>
    </div>
</asp:Content>
