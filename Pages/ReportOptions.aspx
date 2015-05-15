<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="ReportOptions.aspx.cs" Inherits="CubeReportingModule.Pages.ReportOptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div class="selectOptions">
        <h1><%: pageReport.Name %></h1>
        <asp:Repeater ItemType="CubeReportingModule.Models.ReportOption"
            SelectMethod="GetReportOptions" runat="server">
            <ItemTemplate>
                <div>
                    <label for="<%# Item.Label %>"><%# Item.Label %>:</label>
                    <asp:PlaceHolder runat="server" ID="ListBoxControl" Visible='<%# Item.isVisible("ListBox") %>'>
                        <asp:SqlDataSource runat="server" ID='QueryData' DataSourceMode="DataReader" SelectCommand="<%# Item.SelectCommand %>" ConnectionString="<%$ ConnectionStrings:AppContext %>" />
                        <asp:ListBox runat="server" id="ListBox" DataTextField='<%# Item.DataTextField %>' DataSourceID='<%# Item.DataSourceId %>' Height='<%# Item.Height %>' />
<%--                        <asp:ListBox runat="server" id='ClientListBox' DataTextField='<%# Item.DataTextField %>' DataSourceID='<%# Item.DataSourceId %>' Height='<%# Item.Height %>' />--%>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder runat="server" ID="DateControl" Visible='<%# Item.isVisible("Date") %>'>
                        <asp:Calendar ID="Calendar" runat="server">
                            <TodayDayStyle />
                        </asp:Calendar>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder runat="server" ID="RangeControl" Visible='<%# Item.isVisible("Range") %>'>
                        <input runat="server" type="number" name="Floor" min='<%# Item.MinValue %>' max='<%# Item.MaxValue %>' value='<%# Item.MinValue %>' />
                        <input runat="server" type="number" name="Ceiling" min='<%# Item.MinValue %>' max='<%# Item.MaxValue %>' value='<%# Item.MinValue %>' />
                    </asp:PlaceHolder>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <button type="submit">Generate Report</button>
        <button type="reset">Reset</button>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
