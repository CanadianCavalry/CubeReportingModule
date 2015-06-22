<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="True" CodeBehind="ResetPasswords.aspx.cs" Inherits="CubeReportingModule.Admin.ResetPasswords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <h3 class="CenterOnly"><b>User Security:</b></h3>
    <p>
        <asp:Label ID="ActionStatus" runat="server" CssClass="StatusMessage"></asp:Label> 
    </p>
    <div class="AdminLeftPane">
        <asp:DropDownList runat="server" AutoPostBack="true" ID="UserResetList"
                DataTextField="UserName" DataValueField="UserName">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="ResetPasswordButton" runat="server" Text="Reset Password" OnClick="ResetPasswordButton_Click" OnClientClick="return confirm('Reset users password?)" />
        <asp:Button ID="LockAccountButton" runat="server" Text="Lock Account" OnClick="LockAccountButton_Click" OnClientClick="return confirm('Lock account?)" />
        <asp:Button ID="UnlockAccountButton" runat="server" Text="Unlock Account" OnClick="UnlockAccountButton_Click" OnClientClick="return confirm('Unlock account?)" />
    </div>
    <div class="AdminRightPane">
        <asp:Label runat="server" ID="UserName">
        </asp:Label>
        <br />
        <asp:Label runat="server" ID="UserEmail">
        </asp:Label>
        <br />
        <asp:Label runat="server" ID="UserSuspended">
        </asp:Label>
        <br />
        <asp:Label runat="server" ID="UserLastLogin">
        </asp:Label>
        <br />
        <asp:Label runat="server" ID="UserLastActivity">
        </asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
