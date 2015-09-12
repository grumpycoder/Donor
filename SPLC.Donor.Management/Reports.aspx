<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="SPLC.Donor.Management.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h1>Reports</h1>

<div class="default_maindiv">
  <div class="default_hypelink_div" style=" width:690pt; margin-left:10pt;">
      <table style="width:100%">
          <tr>
              <th style="text-align:left;"><asp:Button ID="btnEventReport" runat="server" Text="Run Event Report" CssClass="defaultButton" OnClick="btnEventReport_Click" /></th>
          </tr>
      </table>
      <hr />
      <table style="width:100%">
          <tr>
              <th>Events:</th>
              <td><asp:DropDownList ID="ddlEvents" runat="server" /></td>
          </tr>
          <tr>
              <td></td>
              <td><asp:Button ID="btnEventGuestReport" runat="server" Text="Event Guest Report" CssClass="defaultButton" OnClick="btnEventGuestReport_Click" /></td>
          </tr>
          <tr>
              <td></td>
              <td><asp:Button ID="btnMailTicketsReport" runat="server" Text="Mailed Ticket Report" CssClass="defaultButton" OnClick="btnMailTicketsReport_Click" /></td>
          </tr>
      </table>
      <hr />
      <table style="width:100%">
          <tr>
              <th style="text-align:left;"><asp:Button ID="btnDonorDemoChangeReport" runat="server" Text="Donor Demographic Change Report" CssClass="defaultButton" OnClick="btnDonorDemoChangeReport_Click" /></th>
          </tr>
      </table>


  </div>
</div>
</asp:Content>
