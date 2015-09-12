<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="DonorEventListView.aspx.cs" Inherits="SPLC.Donor.Management.DonorEventListView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h1>Guest List</h1>
<div class="default_maindiv">
  <div class="default_hypelink_div">

      <table style="width:600pt;">
          <tr>
              <td colspan="5"><asp:Label ID="lblMess" runat="server" ForeColor="Red">(NOTE: Display only shows TOP 500 records)</asp:Label></td>
              <td colspan="2"><asp:CheckBox ID="chMailListOnly" runat="server" Text="Show Mail List Only" /></td>
          </tr>
          <tr>
              <th>Events:</th>
              <td><asp:DropDownList ID="ddlEvents" runat="server" /></td>
              <th>Donor ID:</th>
              <td><asp:TextBox ID="txtDonorID" runat="server" /></td>
              <th>Name:</th>
              <td><asp:TextBox ID="txtName" runat="server" /></td>
              <td><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="defaultButton" OnClick="btnSearch_Click" /></td>
          </tr>
      </table>
      <asp:UpdatePanel ID="upWaitList" runat="server">
          <Triggers>
              <asp:PostBackTrigger ControlID="btnSearch" />
          </Triggers>
          <ContentTemplate>
      <asp:Panel ID="pnlDonorEvents" runat="server" Height="300pt" Width="650pt" ScrollBars="Auto" >
          <asp:GridView ID="gvDonorEvents" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Width="600pt"
               OnRowCommand="gvDonorEvents_RowCommand">
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
                  <asp:BoundField DataField="Response_Date" HeaderText="Response Date" />
                  <asp:BoundField DataField="TicketsRequested" HeaderText="Tickets" />
                  <asp:BoundField DataField="TicketsMailed_Date" HeaderText="Tickets Sent" />
                  <asp:ButtonField ButtonType="Button" Text="Send" />
                  <asp:HyperLinkField DataNavigateUrlFields="pk_DonorEventList" DataNavigateUrlFormatString="~/DonorEventListDetails.aspx?delid={0}" Text="EDIT" />
              </Columns>
          </asp:GridView>
      </asp:Panel>

          </ContentTemplate></asp:UpdatePanel>


  </div>
</div>

</asp:Content>
