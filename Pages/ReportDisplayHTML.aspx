<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="True" CodeBehind="ReportDisplayHTML.aspx.cs" Inherits="CubeReportingModule.Pages.ReportDisplayHTML" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <h3>Report Preview</h3>
    <asp:Button runat="server" ID="CreatePdf" CssClass="NavButton" text="Create PDF" OnClick="CreatePdfButton_Click" />
    <asp:Button runat="server" ID="Email" CssClass="NavButton" text="Email to User" />
    <div>
        <%: GetSelectCommand() %>
        <asp:SqlDataSource 
            runat="server" 
            id="QueryData" 
            DataSourceMode="DataReader"
            ConnectionString="<%$ ConnectionStrings:AppContext %>" />
        <asp:GridView
            runat="server"
            id="Display"
            AllowSorting="true"
            BorderStyle="Double"
            CellPadding="2"
            HorizontalAlign="Center"
            >
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>