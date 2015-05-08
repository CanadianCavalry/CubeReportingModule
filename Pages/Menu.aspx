<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CubeReportingModule.Pages.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div class="LeftPane">
        <asp:Button runat="server" ID="Adhoc" CssClass="MenuButton" Text="Create Report" />
        <br />
        <asp:Button runat="server" ID="Events" CssClass="MenuButton" Text="Modify Scheduled Reports" />
        <br />
        <asp:Button runat="server" ID="Help" CssClass="MenuButton" Text="Help" />
        <br />
    </div>
    <div class="RightPane">
        <asp:Repeater ID="ReportButtonsRepeater" ItemType="CubeReportingModule.Models.Report"
            SelectMethod="GetReports" runat="server">
            <ItemTemplate>
                <button name="report" class="MenuButton" type="submit" value="<%# Item.ReportId %>" onclick="reportButton_Click"><%# Item.Name %></button>
<%--                <asp:Button runat="server" CssClass="MenuButton" CommandName="report" CommandArgument="<%# Item.ReportId %>" Text="<%# Item.name %>" OnClick="reportButton_Click" />--%>
                <br />
            </ItemTemplate>
        </asp:Repeater>
<%--        <asp:Button runat="server" ID="report1" CssClass="MenuButton" CommandName="report" CommandArgument="1" Text="Get All Customer Transactions" OnClick="reportButton_Click" />
        <br />
        <asp:Button runat="server" ID="report2" CssClass="MenuButton" CommandName="report" CommandArgument="2" Text="Get All Empty Spaces" OnClick="reportButton_Click" />
        <br />
        <asp:Button runat="server" ID="report3" CssClass="MenuButton" CommandName="report" CommandArgument="3" Text="Get All Irregularities" OnClick="reportButton_Click" />
        <br />--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
    <p style="text-align: center; font-size: 24px;">Admin Control Panel</p>
    <div class="LeftPane">
        <asp:Button runat="server" ID="Accounts" CssClass="AdminButton" Text="Manage Accounts" />
    </div>
    <div class="CenterPane">
        <asp:Button runat="server" ID="Logs" CssClass="AdminButton" Text="View Access Logs" />
    </div>
    <div class="RightPane">
        <asp:Button runat="server" ID="Templates" CssClass="AdminButton" Text="Manage Report Templates" />
    </div>
</asp:Content>
