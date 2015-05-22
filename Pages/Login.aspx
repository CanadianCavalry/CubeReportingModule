<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CubeReportingModule.Pages.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph" CssClass="error" />
     <div class="loginContainer">

        <asp:Login 
        ID="Login1" 
        runat="server" 
        CssClass="LoginPanel"
        DestinationPageUrl="Menu.aspx"
        PasswordRecoveryText="Forgotten your password?"
        PasswordRecoveryUrl="~/Pages/RecoverPassword.aspx"
        Align="Center">
        </asp:Login>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>