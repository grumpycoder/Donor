<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="WaitingListView.aspx.cs" Inherits="SPLC.Donor.Management.WaitingListView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h1>Waiting List</h1>
<div class="default_maindiv">
  <div class="default_hypelink_div">

      <table style="width:600pt;">
          <tr>
              <th>Events:</th>
              <td><asp:DropDownList ID="ddlEvents" runat="server" /></td>
              <th>Donor ID:</th>
              <td><asp:TextBox ID="txtDonorID" runat="server" /></td>
              <th>Name:</th>
              <td><asp:TextBox ID="txtName" runat="server" /></td>
              <td><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="defaultButton" OnClick="btnSearch_Click" /></td>
          </tr>
          <tr>
              <th>Donor Type:</th>
              <td><asp:DropDownList ID="ddlDonorType" runat="server" /></td>
          </tr>
      </table>
      <asp:UpdatePanel ID="upWaitList" runat="server">
          <Triggers>
              <asp:PostBackTrigger ControlID="btnSearch" />
          </Triggers>
          <ContentTemplate>
      <asp:Panel ID="pnlDonorEvents" runat="server" Height="300pt" Width="650pt" ScrollBars="Auto" >
          <asp:GridView ID="gvDonorEvents" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Width="600pt">
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
                  <asp:BoundField DataField="pk_DonorList" HeaderText="Donor ID" />
                  <asp:BoundField DataField="AccountName" HeaderText="Name" />
                  <asp:BoundField DataField="DonorType" HeaderText="Donor Type" />
                  <asp:BoundField DataField="MembershipYear" HeaderText="Membership Year" />
                  <asp:BoundField DataField="WaitingList_Date" HeaderText="Response Date" />
                  <asp:BoundField DataField="WaitingListOrder" HeaderText="Order" />
                  <asp:BoundField DataField="TicketsRequested" HeaderText="Tickets" />
                  <asp:HyperLinkField DataNavigateUrlFields="pk_DonorEventList" DataNavigateUrlFormatString="~/DonorEventListDetails.aspx?delid={0}" Text="EDIT" />
              </Columns>
          </asp:GridView>
      </asp:Panel>

          </ContentTemplate></asp:UpdatePanel>


  </div>
</div>

</asp:Content>
