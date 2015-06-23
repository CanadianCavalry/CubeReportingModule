<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="True" CodeBehind="ReportOptions.aspx.cs" Inherits="CubeReportingModule.Pages.ReportOptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div id="SelectOptions" class="selectOptions" runat="server">
        <h1 id="OptionsHeader" runat="server"></h1>
        <div id="OptionControls" runat="server" />
        <asp:button runat="server" ID="SubmitReportButton" OnClick="SubmitReportButton_Click" Text="Generate Report"></asp:button>
        <button type="reset">Reset</button>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
