<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="CubeReportingModule.Admin.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPane" runat="server">
    <div class="AddUserPanel">
        <%-- Create new user wizard --%>
        <span class="BoldTitle">Fill out the fields below to create a new user:</span>
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
            HorizontalAlign="Center">
            <WizardSteps>
                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server" Title="">
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                </asp:CompleteWizardStep>
            </WizardSteps>
        </asp:CreateUserWizard>
        <br />
        <br />
        <span class="BoldTitle">Remove User:</span>
        <br />
        <br />
        <asp:DropDownList runat="server" ID="DeleteUserList" Width="100px"></asp:DropDownList>
        <asp:Button runat="server" ID="RemoveUser" OnClick="RemoveUser_Click" OnClientClick="return confirm('Delete selected user?)" Text="Remove" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomPane" runat="server">
</asp:Content>
