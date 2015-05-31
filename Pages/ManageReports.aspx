<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="True" CodeBehind="ManageReports.aspx.cs" Inherits="CubeReportingModule.Pages.ManageReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <h1>Manage Reports</h1>
    <asp:GridView runat="server"
        ID="Display"
        ItemType="CubeReportingModule.Models.Report"
        DataKeyNames="ReportId"
        SelectMethod="GetReportsAsQuery"
        UpdateMethod="Display_UpdateItem"
        DeleteMethod="Display_DeleteItem"
        AllowSorting="true"
        AllowPaging="true"
        OnPageIndexChanging="Display_PageIndexChanging"
        PageSize="10"
        PagerSettings-PageButtonCount="5"
        PagerSettings-Mode="NumericFirstLast"
        PagerSettings-FirstPageText="First"
        PagerSettings-LastPageText="Last"
        AutoGenerateColumns="False">
        <Columns>
            <asp:DynamicField DataField="Name" HeaderText="Report Name" />
            <asp:DynamicField DataField="SelectClause" HeaderText="Select" />
            <asp:DynamicField DataField="FromClause" HeaderText="From" />
            <asp:DynamicField DataField="WhereClause" HeaderText="Where" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="Modify" 
                        Visible="<%# SetModifyVisibility(Item.Creator) %>" Text="Modify" 
                        UseSubmitBehavior="False"
                        CommandName="Modify"
                        CommandArgument="<%# Item.ReportId %>" 
                        OnClick="Modify_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="Delete" 
                        Visible="<%# SetDeleteVisibility(Item.Creator) %>" Text="Delete" 
                        UseSubmitBehavior="False" 
                        CommandName="Delete"
                        CommandArgument="<%# Item.ReportId %>" 
                        OnClick="Delete_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
