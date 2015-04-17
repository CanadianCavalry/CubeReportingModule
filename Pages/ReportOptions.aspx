<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="ReportOptions.aspx.cs" Inherits="CubeReportingModule.Pages.ReportOptions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div class="selectOptions">
        <h1>Get All Customer Transactions</h1>
         <div>
            <label for="customername">Customer Name:</label>
            
             <asp:DropDownList runat="server">
                <asp:ListItem Enabled="true" Selected="True" Text="Customer1" Value="customer1" />
                <asp:ListItem Enabled="true" Selected="false" Text="Customer2" Value="customer2" />
                <asp:ListItem Enabled="true" Selected="false" Text="Customer3" Value="customer3" />
                <asp:ListItem Enabled="true" Selected="false" Text="Customer4" Value="customer4" />
                <asp:ListItem Enabled="true" Selected="false" Text="Customer5" Value="customer5" />
            </asp:DropDownList>
         </div>
         <div>
            <label for="dates">From Date:</label>
            <asp:Calendar ID="Calendar" runat="server">
                <TodayDayStyle />
            </asp:Calendar>
        </div>
        <div>
            <label for="dates">To Date:</label>
            <asp:Calendar ID="Calendar1" runat="server">
                <TodayDayStyle />
            </asp:Calendar>
        </div>
        <button type="submit">Submit</button>
        <button type="reset">Reset</button>
        <button type="button">Back</button>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
