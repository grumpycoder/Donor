<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="EventListView.aspx.cs" Inherits="SPLC.Donor.Management.EventListView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h1>Event List</h1>
<div class="default_maindiv">
  <div class="default_hypelink_div">

      <asp:Panel ID="pnlEventList" runat="server" Height="300pt" Width="650pt" ScrollBars="Auto" >
          <asp:GridView ID="gvEventList" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Width="600pt">
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
                  <asp:BoundField DataField="StartDate" HeaderText="Date" />
                  <asp:BoundField DataField="EventName" HeaderText="Event Name" />
                  <asp:BoundField DataField="RegCount" HeaderText="Registered" />
                  <asp:BoundField DataField="WaitCount" HeaderText="Waiting" />
                  <asp:HyperLinkField DataNavigateUrlFields="pk_Event" DataNavigateUrlFormatString="~/Event.aspx?eid={0}" Text="EDIT" />
              </Columns>
          </asp:GridView>
      </asp:Panel>

  </div>
</div>

</asp:Content>
