<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportDisplayHTML.aspx.cs" Inherits="CubeReportingModule.Pages.ReportHTMLDisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h3>Report Preview</h3>
    <asp:Button runat="server" ID="CreatePdf" CssClass="NavButton" text="Create PDF" />
    <asp:Button runat="server" ID="Email" CssClass="NavButton" text="Email to User" />
    <form id="form1" runat="server">
    <div>
        <asp:SqlDataSource
          id="SqlDataSource1"
          runat="server"
          DataSourceMode="DataReader"
          ConnectionString="<%# connectionString %>"
          SelectCommand="<%# queryString %>">
        </asp:SqlDataSource>
        <asp:ListView
            id="ReportDisplay"
            runat="server"
            DataTextField=" "
            DataSourceId="SqlDataSource1">
        </asp:ListView>
    </div>
    </form>
</body>
</html>
