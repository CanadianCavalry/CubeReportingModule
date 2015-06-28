<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="True" CodeBehind="CreateReport.aspx.cs" Inherits="CubeReportingModule.Pages.CreateReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <asp:Panel ID="CreateTemplate" runat="server">
        <h1>Create New Report</h1>
        <asp:Label runat="server" ID="Message" ClientIDMode="Static" CssClass="StatusMessage" />
        <asp:Panel ID="Controls" runat="server">
            <asp:Panel runat="server" ID="NameControls" ClientIDMode="Static">
                Enter Report name:
                <asp:TextBox runat="server" ID="ReportName" ClientIDMode="Static" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ReportName" CssClass="StatusMessage" ErrorMessage="Report name is a required field." />
            </asp:Panel>
            <asp:Panel runat="server" ID="TableControls" ClientIDMode="Static" ScrollBars="Vertical" Height="200px">
                Choose tables:
                <asp:SqlDataSource runat="server" ID="TableQuery" DataSourceMode="DataSet" SelectCommand="SELECT TABLE_NAME FROM information_schema.tables Where TABLE_NAME not like 'GRA%' And TABLE_NAME not like '%aspnet%' Order By TABLE_NAME Asc" ConnectionString="<%$ ConnectionStrings:AppContext %>" />
                <asp:RadioButtonList runat="server" ID="TableNames" ClientIDMode="Static" DataSourceID="TableQuery" DataTextField="TABLE_NAME" DataValueField="TABLE_NAME" />
                <%--                <asp:CheckBoxList runat="server" ID="TableNames" ClientIDMode="Static" DataSourceID="TableQuery" DataTextField="TABLE_NAME" DataValueField="TABLE_NAME" />--%>
                <%--                <asp:RequiredFieldValidator runat="server" ControlToValidate="TableNames" CssClass="StatusMessage" ErrorMessage="You must pick at least one table." />--%>
            </asp:Panel>
            <asp:Panel runat="server" ID="ColumnControls" ClientIDMode="Static" ScrollBars="Vertical">
                Choose columns:
                <asp:SqlDataSource runat="server" ID="ColumnQuery" DataSourceMode="DataSet" ConnectionString="<%$ ConnectionStrings:AppContext %>" />
                <asp:CheckBoxList runat="server" ID="ColumnNames" ClientIDMode="Static" DataSourceID="ColumnQuery" DataTextField="column_name" DataValueField="column_name" />
                <%--                <asp:RequiredFieldValidator runat="server" ControlToValidate="ColumnNames" CssClass="StatusMessage" ErrorMessage="You must pick at least one column." />--%>
            </asp:Panel>
            <asp:Panel runat="server" ID="OptionsControls" ClientIDMode="Static">
                Report Options:
                <asp:Panel runat="server" ID="Options" ClientIDMode="Static" />
                <asp:Button runat="server" ID="AddOption" ClientIDMode="Static" Text="Add Option" OnClick="AddOption_Click" UseSubmitBehavior="false" />
            </asp:Panel>
            <asp:Panel runat="server" ID="AllowControls" ClientIDMode="Static">
                Column Attributes:
                <asp:Panel runat="server" ID="Allows" ClientIDMode="Static" />
            </asp:Panel>
            <asp:Panel runat="server" ID="RestrictionsControls" ClientIDMode="Static">
                Report Restrictions:
                <asp:Panel runat="server" ID="Restrictions" ClientIDMode="Static" />
                <asp:Button runat="server" ID="AddRestriction" ClientIDMode="Static" Text="Add Restriction" OnClick="AddRestriction_Click" UseSubmitBehavior="false" />
            </asp:Panel>
        </asp:Panel>
        <asp:Button runat="server" ID="Next" ClientIDMode="Static" Text="Next" UseSubmitBehavior="false" />
        <asp:Button runat="server" ID="Previous" ClientIDMode="Static" Text="Previous" UseSubmitBehavior="false" />
        <asp:Button runat="server" ID="Summary" ClientIDMode="Static" Text="View Summary" UseSubmitBehavior="false" />
        <asp:Panel runat="server" ID="AdminPanel">
            <asp:Panel runat="server" ID="AdminInput">
                <h3>Admin Query Input</h3>
                Enter a query:
                <asp:TextBox runat="server" ID="QueryInput" />
            </asp:Panel>
            <asp:Button runat="server" ID="AdminNext" ClientIDMode="Static" Text="Next" UseSubmitBehavior="false" />
            <asp:Button runat="server" ID="AdminPrevious" ClientIDMode="Static" Text="Previous" UseSubmitBehavior="false" />
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="ReportSummary">
        <h1>Report Summary</h1>
        <asp:PlaceHolder runat="server" ID="SummaryDisplay" />
        <asp:Button runat="server" ID="Done" ClientIDMode="Static" Text="Done" OnClick="Done_Click" UseSubmitBehavior="false" />
        <asp:Button runat="server" ID="Modify" ClientIDMode="Static" Text="Modify" UseSubmitBehavior="false" />
        <asp:Button runat="server" ID="Cancel" ClientIDMode="Static" Text="Cancel" OnClick="Cancel_Click" UseSubmitBehavior="false" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
