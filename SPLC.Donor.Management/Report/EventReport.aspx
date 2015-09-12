<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="EventReport.aspx.cs" Inherits="SPLC.Donor.Management.Report.EventReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Event Report</h1>

<div class="default_maindiv">
  <div class="default_hypelink_div" style=" width:690pt; margin-left:10pt;">

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
                  <asp:BoundField DataField="EventDate" HeaderText="Date" />
                  <asp:BoundField DataField="EventName" HeaderText="Event Name" />
                  <asp:BoundField DataField="ADDED" HeaderText="Count" />
                  <asp:BoundField DataField="RESPONSE" HeaderText="Responded" />
                  <asp:BoundField DataField="TicketsRequested" HeaderText="Requested" />
                  <asp:BoundField DataField="Capacity" HeaderText="Capacity" />
                  <asp:BoundField DataField="TicketsMailed" HeaderText="Mailed" />
                  <asp:BoundField DataField="WaitList" HeaderText="Wait List" />
                  <asp:HyperLinkField DataNavigateUrlFields="pk_Event" DataNavigateUrlFormatString="~/Event.aspx?eid={0}" Text="EDIT" />
              </Columns>
          </asp:GridView>
      </asp:Panel>



      </div>
    </div>
</asp:Content>
