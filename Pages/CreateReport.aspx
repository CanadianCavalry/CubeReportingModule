<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="CreateReport.aspx.cs" Inherits="CubeReportingModule.Pages.CreateReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <asp:Panel ID="CreateTemplate" runat="server">
        <h1>Create New Report</h1>
        <asp:Panel ID="Controls" runat="server">
            Enter Report name:
            <asp:TextBox runat="server" ID="ReportName" ClientIDMode="Static" />
            <asp:Panel runat="server" ID="TableControls" ClientIDMode="Static" ScrollBars="Vertical">
                Choose tables:
                <asp:SqlDataSource runat="server" ID="TableQuery" DataSourceMode="DataSet" SelectCommand="SELECT TABLE_NAME FROM information_schema.tables Where TABLE_NAME not like 'GRA%' And TABLE_NAME not like 'aspnet%' Order By TABLE_NAME Asc" ConnectionString="<%$ ConnectionStrings:AppContext %>" />
                <asp:CheckBoxList runat="server" ID="TableNames" ClientIDMode="Static" DataSourceID="TableQuery" DataTextField="TABLE_NAME" DataValueField="TABLE_NAME" />
            </asp:Panel>
            <asp:Panel runat="server" ID="ColumnControls" ClientIDMode="Static" Visible="false">
                Choose columns:
                <asp:SqlDataSource runat="server" ID="ColumnQuery" DataSourceMode="DataSet" ConnectionString="<%$ ConnectionStrings:AppContext %>" />
                <asp:CheckBoxList runat="server" ID="ColumnNames" ClientIDMode="Static" DataSourceID="ColumnQuery" DataTextField="column_name" DataValueField="column_name" />
            </asp:Panel>
            <asp:Panel runat="server" ID="OptionsControls" ClientIDMode="Static" Visible="false">
                Report Options:
                <asp:Panel runat="server" ID="Options" ClientIDMode="Static" />
                <asp:Button runat="server" ID="AddOption" ClientIDMode="Static" Text="Add Option" OnClick="AddOption_Click" />
            </asp:Panel>
        </asp:Panel>
        <asp:Button runat="server" ID="Next" ClientIDMode="Static" Text="Next" OnClick="Next_Click" />
        <asp:Button runat="server" ID="Back" ClientIDMode="Static" Text="Back" OnClick="Back_Click" Visible="false" />
        <asp:Button runat="server" ID="Done" ClientIDMode="Static" Text="View Summary" OnClick="Summary_Click" Visible="false" />
    </asp:Panel>
    <asp:Panel runat="server" ID="ReportSummary" Visible="false">
        <h1>Report Summary</h1>
        <asp:Placeholder runat="server" ID="Summary" />
        <asp:Button runat="server" ID="Ok" ClientIDMode="Static" Text="Done" OnClick="Done_Click" />
        <asp:Button runat="server" ID="Modify" ClientIDMode="Static" Text="Modify" OnClick="Modify_Click" />
        <asp:Button runat="server" ID="Cancel" ClientIDMode="Static" Text="Cancel" OnClick="Cancel_Click" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
