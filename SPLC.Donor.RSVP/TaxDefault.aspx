<%@ Page Title="" Language="C#" MasterPageFile="~/TaxReceipt.Master" AutoEventWireup="true" CodeBehind="TaxDefault.aspx.cs" Inherits="SPLC.Donor.RSVP.TaxDefault" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="TaxDiv">
        <br />
        <br />
        Tax Receipt <asp:Label ID="lblTaxYear" runat="server" />
        <br />
        <br />
    </div>

    <asp:Panel ID="pnOther" runat="server" Visible="false">

    <div class="SupportID">
        The dates to view your tax receipt for donations made to SPLC in <asp:Label ID="lblYear2" runat="server" />, will be available only between
        <asp:Label id="lblStartDate" runat="server" /> and <asp:Label id="lblEndDate" runat="server" />.  
        <br/>
        <br/>
        For help please contact us at: <asp:Label ID="lblPhone" runat="server" /> or <asp:HyperLink ID="hlEmail" runat="server" /> 
        <br />
    </div>



    </asp:Panel>





    <asp:Panel ID="pnlReceipt" runat="server" Visible="false">

    <div class="SupportID">
        To view and print your tax receipt for donations made to SPLC in <asp:Label ID="lblYear" runat="server" />, please enter the Supporter ID number that was provided to you.
        <br/>
        <br/>
        Your <asp:Label ID="lblYear3" runat="server" /> tax receipt will be available through <asp:Label ID="lblDateGood" runat="server" />.
        <br />
        <br/>
        Thank you for your generous support of the Southern Poverty Law Center. 
        <br />
        <br />

        <table class="UserInputDiv">
            <tr>
                <td>
                    <asp:Label ID="SupporterIdLabel" runat="server" Text="Supporter ID:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="SupporterIdTextBox" runat="server" MaxLength="13" Height="16px" Width="171px" ></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <asp:CheckBox ID="PrivacyPolicyCheckBox" runat="server" />
                <label id="cb1">I have read the</label> <a class="PrivacyPolicy" href="http://www.splcenter.org/privacy-policy" target="_blank">privacy policy</a>.
                    <br /><br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" /> 
            <br />
            <br />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" /><br /><br />
                </td>
            </tr>

        </table>

            If you do not have your Supporter ID, please contact us by 

            <asp:HyperLink ID="hlEmail2" runat="server" >email</asp:HyperLink> or call <asp:Label ID="lblPhone2" runat="server" />.
        </div>
        <div id="dialog"></div>

        </asp:Panel>

</asp:Content>
