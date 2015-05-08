<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="ReportDisplayHTML.aspx.cs" Inherits="CubeReportingModule.Pages.ReportDisplayHTML" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <h3>Report Preview</h3>
    <asp:Button runat="server" ID="CreatePdf" CssClass="NavButton" text="Create PDF" />
    <asp:Button runat="server" ID="Email" CssClass="NavButton" text="Email to User" />
    <div>
        <asp:SqlDataSource>
          id="SqlDataSource1"
          runat="server"
          DataSourceMode="DataReader"
          ConnectionString="<% =connectionString %>"
          SelectCommand="select Loc_Box_Max - Loc_Box_Current AS AvailableSpace,Locator_Id,Loc_Row_Id,Loc_Size_Code,Last_Update,Last_By from Locator where Loc_Box_Max - Loc_Box_Current > 0 and Loc_Box_Max - Loc_Box_Current < 12 Order BY Loc_Row_Id,AvailableSpace DESC";
        <%--  SelectCommand="<% =queryString %>">--%>
        </asp:SqlDataSource>
        <asp:ListView
            id="ReportDisplay"
            runat="server"
            DataTextField=" "
            DataSourceId="SqlDataSource1">
        </asp:ListView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
