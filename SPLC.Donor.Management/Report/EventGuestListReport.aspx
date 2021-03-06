﻿<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="EventGuestListReport.aspx.cs" Inherits="SPLC.Donor.Management.Report.EventGuestListReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Event Guest List Report (Event:
        <asp:Label ID="lblEvent" runat="server" />)  
                <asp:HyperLink ID="hlExcel" runat="server" ImageUrl="../images/excel_icon.png" Target="_blank"></asp:HyperLink></h1>

    <div class="default_maindiv">
        <div class="default_hypelink_div" style="width: 690pt; margin-left: 10pt;">
            <asp:GridView ID="gvReport" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"
                PageSize="17" OnPageIndexChanging="gvReport_PageIndexChanging" OnSorting="gvReport_Sorting" OnRowDataBound="gvReport_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <PagerTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPrev" runat="server" Text="<" CssClass="defaultButton" OnClick="btnPrev_Click" />
                            </td>
                            <td style="text-align: right;">
                                <asp:Button ID="btnNext" runat="server" Text=">" CssClass="defaultButton" OnClick="btnNext_Click" /></td>
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
                    <asp:BoundField DataField="AccountName" HeaderText="Name" SortExpression="AccountName" />
                    <asp:BoundField DataField="AddressLine1" HeaderText="AddressLine1" SortExpression="AddressLine1" />
                    <asp:BoundField DataField="AddressLine2" HeaderText="AddressLine2" SortExpression="AddressLine2" />
                    <asp:BoundField DataField="AddressLine3" HeaderText="AddressLine3" SortExpression="AddressLine3" />
                    <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                    <asp:BoundField DataField="PhoneNumber" HeaderText="Phone" SortExpression="PhoneNumber" />
                    <asp:BoundField DataField="TicketsRequested" HeaderText="Tickets" SortExpression="TicketsRequested" />
                    <asp:BoundField DataField="DonorType" HeaderText="Donor Type" SortExpression="DonorType" />
                    <asp:BoundField DataField="Response_Date" HeaderText="Response" DataFormatString="{0:MM/dd/yyyy}" SortExpression="Response_Date" />
                    <asp:HyperLinkField DataNavigateUrlFields="pk_DonorEventList" DataNavigateUrlFormatString="~/DonorEventListDetails.aspx?delid={0}" Text="EDIT" />
                </Columns>

            </asp:GridView>

        </div>
    </div>
</asp:Content>

