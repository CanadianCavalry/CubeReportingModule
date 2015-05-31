<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="True" CodeBehind="ResetPasswords.aspx.cs" Inherits="CubeReportingModule.Admin.ResetPasswords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <p>
        <asp:Label ID="ActionStatus" runat="server" CssClass="StatusMessage"></asp:Label> 
    </p>
    <asp:DropDownList runat="server" AutoPostBack="true" ID="UserResetList"
         DataTextField="UserName" DataValueField="UserName" OnSelectedIndexChanged="UserResetList_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="ResetPasswordButton" runat="server" Text="Reset Password" OnClick="ResetPasswordButton_Click" OnClientClick="return confirm('Reset users password?)" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
