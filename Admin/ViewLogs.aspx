<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="True" CodeBehind="ViewLogs.aspx.cs" Inherits="CubeReportingModule.Pages.ViewAccessLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <asp:Panel runat="server" ID="Logs">
        <h1>Access Logs</h1>
        <table>
            <asp:Repeater ItemType="CubeReportingModule.Models.AccessLog"
                SelectMethod="GetLogs" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Item.ToString() %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
