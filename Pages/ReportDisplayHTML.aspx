<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="ReportDisplayHTML.aspx.cs" Inherits="CubeReportingModule.Pages.ReportDisplayHTML" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <h3>Report Preview</h3>
    <asp:Button runat="server" ID="CreatePdf" CssClass="NavButton" text="Create PDF" OnClick="CreatePdfButton_Click" />
    <asp:Button runat="server" ID="Email" CssClass="NavButton" text="Email to User" />
    <div>
        <%= GetSelectCommand() %>
        <%: GetSelectCommand() %>
        <asp:SqlDataSource
          runat="server"
          id="SqlDataSource1"
          DataSourceMode="DataReader"
          SelectCommand="select Loc_Box_Max - Loc_Box_Current AS AvailableSpace,Locator_Id,Loc_Row_Id,Loc_Size_Code,Last_Update,Last_By from Locator where Loc_Box_Max - Loc_Box_Current > 0 and Loc_Box_Max - Loc_Box_Current < 12 Order BY Loc_Row_Id,AvailableSpace DESC"
          ConnectionString="<%$ ConnectionStrings:AppContext %>" />
        <asp:ListView
            id="Display"
            runat="server"
            DataTextField=" "
            DataSourceId="SqlDataSource1">
            <LayoutTemplate>
                <table cellpadding="2" width="640px" border="1" runat="server" id="tblReport">
                    <tr id="Tr1" runat="server">
                        <th id="Th1" runat="server">Available Space</th>
                        <th id="Th2" runat="server">Locator Id</th>
                        <th id="Th3" runat="server">Loc Row Id</th>
                        <th id="Th4" runat="server">Loc Size Code</th>
                        <th id="Th5" runat="server">Last Update</th>
                        <th id="Th6" runat="server">Updated By</th>
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr2" runat="server">
                    <td runat="server">
                        <asp:Label ID="SpaceLabel" runat="server" 
                            Text='<%#Eval("AvailableSpace") %>' />
                    </td>
                    <td id="Td1" runat="server">
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%#Eval("Locator_Id") %>' />
                    </td>
                    <td id="Td2" runat="server">
                        <asp:Label ID="Label2" runat="server" 
                            Text='<%#Eval("Loc_Row_Id") %>' />
                    </td>
                    <td id="Td3" runat="server">
                        <asp:Label ID="Label3" runat="server" 
                            Text='<%#Eval("Loc_Size_Code") %>' />
                    </td>
                    <td id="Td4" runat="server">
                        <asp:Label ID="Label4" runat="server" 
                            Text='<%#Eval("Last_Update") %>' />
                    </td>
                    <td id="Td5" runat="server">
                        <asp:Label ID="Label5" runat="server" 
                            Text='<%#Eval("Last_By") %>' />
                    </td>
                </tr> 
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>