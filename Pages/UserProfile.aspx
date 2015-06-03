<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="CubeReportingModule.Pages.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div class="CenterOnly">
        <asp:Label runat="server" ID="ProfileUserName">
        </asp:Label>
        <br />
        <br />
        <asp:Label runat="server" ID="ProfileUserEmail">
        </asp:Label>
        <br />
        <br />
        New Email Address:
        <asp:TextBox runat="server" ID="EmailUpdate">
        </asp:TextBox>
        <br />
        <br />
        Change Password:
        <asp:TextBox runat="server" ID="NewPasswordText">
        </asp:TextBox>
        <br />
        <br />
        Re-type Password:
        <asp:TextBox runat="server" ID="ConfirmNewPasswordText">
        </asp:TextBox>
        <br />
        <br />
        <asp:Button runat="server" ID="ProfileUpdateButton" Text="Save Changes"/>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
