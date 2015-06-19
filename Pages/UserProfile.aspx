<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="CubeReportingModule.Pages.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <asp:Label runat="server" ID="ActionStatus" CssClass="StatusMessage"></asp:Label>
    <br />
    <br />
    <div class="UserDataDisplay">
        <span class="BoldTitle">User Profile</span>
        <br />
        <br />
        <asp:Label runat="server" ID="ProfileUserName">
        </asp:Label>
        <br />
        <asp:Label runat="server" ID="ProfileUserEmail">
        </asp:Label>
    </div>
    <div class="UserProfilePanel">
        <br />
        <br />
        New Email Address:
        <asp:TextBox runat="server" ID="EmailUpdate">
        </asp:TextBox>
        <br />
        <br />
        New Password:
        <asp:TextBox runat="server" ID="NewPasswordText" TextMode="Password" style="width: auto">
        </asp:TextBox>
        <br />
        <br />
        Re-type Password:
        <asp:TextBox runat="server" ID="ConfirmNewPasswordText" TextMode="Password" style="width: auto">
        </asp:TextBox>
        <br />
        <br />
        Current Password:
        <asp:TextBox runat="server" ID="CurrentPasswordText" TextMode="Password" style="width: auto">
        </asp:TextBox>
        <br />
        <br />
        <asp:Button runat="server" ID="ProfileUpdateButton" Text="Save Changes"/>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
