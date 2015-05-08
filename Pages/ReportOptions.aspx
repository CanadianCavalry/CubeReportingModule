<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="ReportOptions.aspx.cs" Inherits="CubeReportingModule.Pages.ReportOptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div class="selectOptions">
        <h1><%# reportName %></h1>
        <asp:Repeater ItemType="CubeReportingModule.Models.ReportOption"
            SelectMethod="allOptions" runat="server">
            <ItemTemplate>
                <div>
                    <label for="<%# Item.Label %>"><%# Item.Label %>:</label>
<%--                    <% Item.toString(); %>--%>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <label for="customername">Customer Name:</label>
        <asp:SqlDataSource 
            ID="CompanyNameSelect" runat="server" 
            DataSourceMode="DataReader" 
            SelectCommand="SELECT Company_Name FROM Org_Company ORDER BY Company_Name ASC" 
            ConnectionString="Data Source=204.174.60.182;Initial Catalog=GainTest;Persist Security Info=True;User ID=Michelle;Password=SRGTChronos3">
        </asp:SqlDataSource>
        <asp:ListBox
            id="ClientListBox" runat="server" DataTextField="Company_Name" DataSourceID="CompanyNameSelect">
        </asp:ListBox>

    </div>
    <div align="center">
        <label for="dates">From Date:</label>
        <asp:Calendar ID="Calendar" runat="server">
            <TodayDayStyle />
        </asp:Calendar>
    </div>
    <div align="center">
        <label for="dates">To Date:</label>
        <asp:Calendar ID="Calendar1" runat="server">
            <TodayDayStyle />
        </asp:Calendar>
    </div>
    <button align="center" type="submit">Submit</button>
    <button align="center" type="reset">Reset</button>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
