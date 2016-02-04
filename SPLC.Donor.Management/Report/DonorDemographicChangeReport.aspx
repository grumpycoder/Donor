<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="DonorDemographicChangeReport.aspx.cs" Inherits="SPLC.Donor.Management.Report.DonorDemographicChangeReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h1>Donor Demographic Change Report
            <asp:HyperLink ID="hlExcel" runat="server" ImageUrl="../images/excel_icon.png" Target="_blank"></asp:HyperLink>
        </h1>

<div class="default_maindiv">
  <div class="default_hypelink_div" style=" width:690pt; margin-left:10pt;">
      <asp:GridView ID="gvReport" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"
          PageSize="10" OnPageIndexChanging="gvReport_PageIndexChanging" OnSorting="gvReport_Sorting" OnRowDataBound="gvReport_RowDataBound">
          <AlternatingRowStyle BackColor="White" />
          <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
          <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
          <PagerTemplate>
              <table style="width:100%">
                  <tr>
                      <td style="text-align:left;">
                          <asp:Button ID="btnPrev" runat="server" Text="<" CssClass="defaultButton" OnClick="btnPrev_Click" />
                      </td>
                      <td style="text-align:right;"><asp:Button ID="btnNext" runat="server" Text=">" CssClass="defaultButton" OnClick="btnNext_Click" /></td>
                  </tr>
              </table>
          </PagerTemplate>
          <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
          <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
          <SortedAscendingCellStyle BackColor="#FDF5AC" />
          <SortedAscendingHeaderStyle BackColor="#4D0000" />
          <SortedDescendingCellStyle BackColor="#FCF6C0" />
          <SortedDescendingHeaderStyle BackColor="#820000" />
          <Columns>
              <asp:BoundField DataField="SourceCode" HeaderText="Source Code" SortExpression="SourceCode" />
                  <asp:BoundField DataField="pk_DonorList" HeaderText="Donor ID" SortExpression="pk_DonorList" />
              <asp:BoundField DataField="AccountID" HeaderText="Account ID" SortExpression="AccountID" />    
              <asp:BoundField DataField="AccountName" HeaderText="Name" SortExpression="AccountName" />
              
              <asp:BoundField DataField="AddressLine1" HeaderText="Address 1" SortExpression="AddressLine1" />
              <asp:BoundField DataField="AddressLine2" HeaderText="Address 2" SortExpression="AddressLine2" />
              <asp:BoundField DataField="AddressLine3" HeaderText="Address 3" SortExpression="AddressLine3" />
                  <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
              <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
              <asp:BoundField DataField="PostCode" HeaderText="ZipCode" SortExpression="PostCode" />
                  <asp:BoundField DataField="PhoneNumber" HeaderText="Phone" SortExpression="PhoneNumber" />
                  <asp:BoundField DataField="EmailAddress" HeaderText="Email" SortExpression="EmailAddress" />
                  <asp:HyperLinkField DataNavigateUrlFields="pk_DonorEventList" DataNavigateUrlFormatString="~/DonorEventListDetails.aspx?delid={0}" Text="EDIT" />
              </Columns>
          </asp:GridView>

      </div>
    </div>

</asp:Content>
