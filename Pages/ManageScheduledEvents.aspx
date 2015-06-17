<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="ManageScheduledEvents.aspx.cs" Inherits="CubeReportingModule.Pages.ManageScheduledEvents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <h1>Manage Scheduled Events</h1>
    <asp:GridView runat="server"
        ID="Display"
        ItemType="CubeReportingModule.Models.GRAScheduledEvent"
        DataKeyNames="EventId"
        SelectMethod="GetEventsAsQuery"
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
            <asp:DynamicField DataField="ReportName" HeaderText="Associated report" />
            <asp:DynamicField DataField="RefreshInterval" HeaderText="Refresh interval" />
            <asp:DynamicField DataField="Creator" HeaderText="Creator" />
            <asp:DynamicField DataField="Recipients" HeaderText="Email recipients" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="Modify" 
                        Visible="<%# SetModifyVisibility(Item.Creator) %>" Text="Modify" 
                        UseSubmitBehavior="False"
                        CommandName="Modify"
                        CommandArgument="<%# Item.EventId %>" 
                        OnClick="Modify_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="Delete" 
                        Visible="<%# SetDeleteVisibility(Item.Creator) %>" Text="Delete" 
                        UseSubmitBehavior="False" 
                        CommandName="Delete"
                        CommandArgument="<%# Item.EventId %>" 
                        OnClick="Delete_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:PlaceHolder runat="server" ID="Message" ClientIDMode="Static" />
    <asp:Button runat="server" ID="AddEvent" ClientIDMode="Static" UseSubmitBehavior="False" Text="Add Event" OnClick="AddEvent_Click" />
    <asp:Panel runat="server" ID="EventToAdd" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
