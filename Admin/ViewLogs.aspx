<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="True" 
    CodeBehind="ViewLogs.aspx.cs" Inherits="CubeReportingModule.Admin.ViewLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <asp:Panel runat="server" ID="Logs" CssClass="DisplayLogPanel">
        <h1>Access Logs</h1>
        <asp:ObjectDataSource runat="server" 
            ID="LogData" 
            SelectMethod="GetAccessLogList"
            SortParameterName="SortExpression"
            DataObjectTypeName="CubeReportingModule.Models.AccessLog" 
            TypeName="CubeReportingModule.Admin.ViewLogs" />
        <asp:GridView runat="server" 
            ID="Display" 
            AllowPaging="true" 
            AllowSorting="true" 
            DataSourceID="LogData" 
            OnPageIndexChanging="Display_PageIndexChanging" 
            AutoGenerateColumns="false" 
            PageSize="20"
            PagerSettings-PageButtonCount="5"
            PagerSettings-Mode="NumericFirstLast" 
            PagerSettings-FirstPageText="First" 
            PagerSettings-LastPageText="Last"
            Width="100%">
            <Columns>
                <asp:BoundField DataField="Username" HeaderText="User" SortExpression="Username" />
                <asp:BoundField DataField="Description" HeaderText="Action" SortExpression="Description" />
                <asp:BoundField DataField="LogDate" HeaderText="Date" SortExpression="LogDate" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
