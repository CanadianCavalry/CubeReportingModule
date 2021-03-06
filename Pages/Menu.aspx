﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CubeReportingModule.Pages.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div class="LeftPane">
        <%-- Static menu buttons --%>
        <asp:Button runat="server" ID="Adhoc" CssClass="MenuButton" Text="Create Report" OnClick="CreateReport_Click" />
        <br />
        <asp:Button runat="server" ID="Templates" CssClass="MenuButton" Text="Manage Report Templates" OnClick="Templates_Click" />
        <br />
        <asp:Button runat="server" ID="Events" CssClass="MenuButton" Text="Modify Scheduled Reports" OnClick="Events_Click" />
        <br />
        <asp:Button runat="server" ID="Help" CssClass="MenuButton" Text="Help" OnClick="Help_Click" />
        <br />
    </div>
    <asp:Panel runat="server" ID="Reports" Cssclass="RightPane" ScrollBars="Vertical">
        <%-- Dynamically generate report buttons from reports table in database --%>
       <asp:Repeater ItemType="CubeReportingModule.Models.GRAReport"
            SelectMethod="GetReports" runat="server">
            <ItemTemplate>
                <button name="Report" class="MenuButton" type="submit" value="<%# Item.ReportId %>"><%# Item.Name %></button>
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
    <div id="AdminPane" runat="server">
        <p style="text-align: center; font-size: 24px;">Admin Control Panel</p>
        <div class="LeftPane">
            <asp:Button runat="server" ID="AddUser" CssClass="AdminButton" Text="Add/Remove Users" OnClick="AddUser_Click" />
            <asp:Button runat="server" ID="Accounts" CssClass="AdminButton" Text="Manage Roles" OnClick="Accounts_Click" />
        </div>
        <div class="RightPane">
            <asp:Button runat="server" ID="Logs" CssClass="AdminButton" Text="View Access Logs" OnClick="Logs_Click" />
            <asp:Button runat="server" ID="ResetPasswords" CssClass="AdminButton" Text="User Security" OnClick="ResetPasswords_Click" />
        </div>
    </div>
</asp:Content>
