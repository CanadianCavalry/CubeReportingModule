<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CubeReportingModule.Pages.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div class="LeftPane">
        <asp:Button runat="server" id="Adhoc" class="MenuButton" Text="Create Report"/>
        <br />
        <asp:Button runat="server" id="Events" class="MenuButton" Text="Modify Scheduled Reports"/>
        <br />
        <asp:Button runat="server" id="Help" class="MenuButton" Text="Help"/>
        <br />
    </div>
    <div class="RightPane">
        <asp:Button runat="server" ID="report1" class="MenuButton" name="report" value="1" Text="Get All Customer Transactions" onclick="report1_Click" />
        <br />
        <asp:Button runat="server" ID="report2" class="MenuButton" name="report" value="2" Text="Get All Empty Spaces" onclick="report1_Click" />
        <br />
        <asp:Button runat="server" ID="report3" class="MenuButton" name="report" value="3" Text="Get All Irregularities" onclick="report1_Click" />
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
    <p style="text-align:center; font-size: 24px;">Admin Control Panel</p>
    <div class="LeftPane">
        <asp:Button runat="server" id="Accounts" class="AdminButton" Text="Manage Accounts"/>
    </div>
    <div class="RightPane">
         <asp:Button runat="server" id="Templates" class="AdminButton" Text="Manage Report Templates"/>
    </div>
</asp:Content>
