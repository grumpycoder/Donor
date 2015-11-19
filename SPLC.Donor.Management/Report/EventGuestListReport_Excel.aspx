<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventGuestListReport_Excel.aspx.cs" Inherits="SPLC.Donor.Management.Report.EventGuestListReport_Excel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
<form id="frmExcel" runat="server">
  <asp:GridView ID="gvExcel" runat="server" AutoGenerateColumns="false">
      <Columns>
          <asp:BoundField DataField="SourceCode" HeaderText="Source Code" SortExpression="SourceCode" />
          <asp:BoundField DataField="EventName" HeaderText="Event Name" />
          <asp:BoundField DataField="AccountID" HeaderText="Donor ID" />
          <asp:BoundField DataField="Attend" HeaderText="Attending" />
          <asp:BoundField DataField="pk_DonorList" HeaderText="Finder Number" />
          <asp:BoundField DataField="AccountName" HeaderText="Name" />
          <asp:BoundField DataField="AddressLine1" HeaderText="AddressLine1" />
          <asp:BoundField DataField="AddressLine2" HeaderText="AddressLine2" />
          <asp:BoundField DataField="City" HeaderText="City" />
          <asp:BoundField DataField="State" HeaderText="State" />
          <asp:BoundField DataField="PostCode" HeaderText="ZipCode" />
          <asp:BoundField DataField="PhoneNumber" HeaderText="Phone" />
          <asp:BoundField DataField="EmailAddress" HeaderText="Email" />
          <asp:BoundField DataField="TicketsRequested" HeaderText="Tickets" />
          <asp:BoundField DataField="DonorType" HeaderText="Donor Type" />
          <asp:BoundField DataField="Response_Date" HeaderText="Response" DataFormatString="{0:MM/dd/yyyy}" />
          <asp:BoundField DataField="Response_Type" HeaderText="Response Type" />
          <asp:BoundField DataField="TicketsMailed_Date" HeaderText="Tickets Mailed" DataFormatString="{0:MM/dd/yyyy}" />
          <asp:BoundField DataField="DonorComments" HeaderText="Donor Comments" />
          <asp:BoundField DataField="SPLCComments" HeaderText="SPLC Comments" />
        </Columns>
  </asp:GridView>
</form>
</body>
</html>
