﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="ReportOptions.aspx.cs" Inherits="CubeReportingModule.Pages.ReportOptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div class="selectOptions">
        <h1><% Response.Write(reportName); %></h1>
        <asp:Repeater ItemType="CubeReportingModule.Models.ReportOption"
            SelectMethod="GetReportOptions" runat="server">
            <ItemTemplate>
                <div>
                    <label for="<%# Item.Label %>"><%# Item.Label %>:</label>
                    <%# Item.toString() %>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:SqlDataSource 
            ID="CompanyNameSelect" runat="server" 
            DataSourceMode="DataReader" 
            SelectCommand="SELECT Company_Name FROM Org_Company WHERE Company_Name IS NOT NULL AND Company_Name != '' ORDER BY Company_Name ASC" 
            ConnectionString="Data Source=204.174.60.182;Initial Catalog=GainTest;Persist Security Info=True;User ID=Michelle;Password=SRGTChronos3">
        </asp:SqlDataSource>
        <asp:ListBox
            id="ClientListBox" runat="server" DataTextField="Company_Name" DataSourceID="CompanyNameSelect" Height="200">
        </asp:ListBox>

        <button type="submit">Generate Report</button>
        <button type="reset">Reset</button>
        <button type="button">Back</button>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
