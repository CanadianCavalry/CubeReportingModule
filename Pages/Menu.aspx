<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CubeReportingModule.Pages.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div class="LeftPane">
        <%-- Static menu buttons --%>
        <asp:Button runat="server" ID="Adhoc" CssClass="MenuButton" Text="Create Report" OnClick="CreateReport_Click" />
        <br />
        <asp:Button runat="server" ID="Events" CssClass="MenuButton" Text="Modify Scheduled Reports" />
        <br />
        <asp:Button runat="server" ID="Help" CssClass="MenuButton" Text="Help" />
        <br />
    </div>
    <div class="RightPane">
        <%-- Dynamically generate report buttons from reports table in database --%>
       <asp:Repeater ItemType="CubeReportingModule.Models.Report"
            SelectMethod="GetReports" runat="server">
            <ItemTemplate>
                <button name="Report" class="MenuButton" type="submit" value="<%# Item.ReportId %>"><%# Item.Name %></button>
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
    <div id="AdminPane" runat="server">
        <p style="text-align: center; font-size: 24px;">Admin Control Panel</p>
        <div class="LeftPane">
            <asp:Button runat="server" ID="AddUser" CssClass="AdminButton" Text="Add User" OnClick="AddUser_Click" />
            <asp:Button runat="server" ID="Accounts" CssClass="AdminButton" Text="Manage Accounts" OnClick="Accounts_Click" />
        </div>
        <div class="RightPane">
            <asp:Button runat="server" ID="Logs" CssClass="AdminButton" Text="View Access Logs" OnClick="Logs_Click" />
            <asp:Button runat="server" ID="Templates" CssClass="AdminButton" Text="Manage Report Templates" OnClick="Templates_Click" />
        </div>
    </div>
</asp:Content>
