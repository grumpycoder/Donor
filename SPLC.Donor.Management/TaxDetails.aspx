<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="TaxDetails.aspx.cs" Inherits="SPLC.Donor.Management.TaxDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h1>Tax Year Details</h1>
<div class="default_maindiv">
  <div >

      <table>
          <tr>
              <th>Year:</th>
              <th style="text-align:left;"><asp:Label id="lblTaxYear" runat="server" /></th>
          </tr>
          <tr>
              <th>Start Date:</th>
              <td><asp:TextBox ID="txtSDate" runat="server" Width="70" />
                  <ajaxToolkit:CalendarExtender ID="ceSDate" runat="server" TargetControlID="txtSDate" />
              </td>
          </tr>
          <tr>
              <th>End Date:</th>
              <td><asp:TextBox ID="txtEDate" runat="server" Width="70" />
                  <ajaxToolkit:CalendarExtender ID="ceEDate" runat="server" TargetControlID="txtEDate" />
              </td>
          </tr>
          <tr>
              <th>Email Address:</th>
              <td><asp:TextBox ID="txtEmail" runat="server" Width="250" />
                  <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Required" ControlToValidate="txtEmail" ForeColor="Red" /></td>
          </tr>
          <tr>
              <th>Phone Number:</th>
              <td><asp:TextBox ID="txtPhone" runat="server" Width="100" />
                  <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ErrorMessage="Required" ControlToValidate="txtPhone" ForeColor="Red" />
              </td>
          </tr>
          <tr>
              <td colspan="2"><asp:Button ID="btnSave" runat="server" CssClass="defaultButton" Text="Save" OnClick="btnSave_Click" /></td>
          </tr>
          <tr>
              <td><asp:Label ID="lblMessage" runat="server" ForeColor="Red" /></td>
          </tr>
      </table>

  </div>
</div>
</asp:Content>
