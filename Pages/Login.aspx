<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CubeReportingModule.Pages.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph" CssClass="error" />
     <div class="loginContainer">
         <div>
            <label for="name">Name:</label>
            <input name="name" />
         </div>
         <div>
            <label for="password">Password:</label>
             <input type="password" name="password" />
        </div>
        <asp:Button runat="server" Text="Login" OnClick="Login_Click" />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" Text="Forgot your password?" />
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
