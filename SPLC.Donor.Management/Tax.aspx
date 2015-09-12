<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="Tax.aspx.cs" Inherits="SPLC.Donor.Management.Tax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Tax Years</h1>
<div class="default_maindiv">
  <div class="default_hypelink_div">

      <asp:Panel ID="pnlTaxYears" runat="server" Height="300pt" Width="650pt" ScrollBars="Auto" >
          <asp:GridView ID="gvTaxYears" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Width="600pt">
              <AlternatingRowStyle BackColor="White" />
              <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
              <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
              <RowStyle BackColor="#FFFBD6" ForeColor="#333333" HorizontalAlign="Center" />
              <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
              <SortedAscendingCellStyle BackColor="#FDF5AC" />
              <SortedAscendingHeaderStyle BackColor="#4D0000" />
              <SortedDescendingCellStyle BackColor="#FCF6C0" />
              <SortedDescendingHeaderStyle BackColor="#820000" />
              <Columns>
                  <asp:BoundField DataField="TaxYear" HeaderText="Year" />
                  <asp:BoundField DataField="Start_DateTime" HeaderText="Start Date" DataFormatString="{0:MM/dd/yyyy}" />
                  <asp:BoundField DataField="End_DateTime" HeaderText="End Date" DataFormatString="{0:MM/dd/yyyy}" />
                  <asp:BoundField DataField="SupportEmail" HeaderText="Email" />
                  <asp:BoundField DataField="SupportPhone" HeaderText="Phone" />
                  <asp:HyperLinkField DataNavigateUrlFields="TaxYear" DataNavigateUrlFormatString="~/TaxDetails.aspx?year={0}" Text="EDIT" />
              </Columns>
          </asp:GridView>
      </asp:Panel>

  </div>
</div>

</asp:Content>
