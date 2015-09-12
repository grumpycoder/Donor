<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="SPLC.Donor.Management.UploadFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h1><asp:Label ID="lblHeader" runat="server" /></h1>

<div class="default_maindiv">
  <div class="default_hypelink_div">
      <asp:Panel ID="pnlUpload" runat="server" Visible="true">
      <asp:FileUpload ID="fuDonorFile" runat="server" /><br /><br />
      <asp:Button ID="btnUpload" runat="server" Text="Upload Donor File" OnClick="btnUpload_Click" CssClass="defaultButton"  />
      <br /><br />
      <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
          </asp:Panel>
  </div>

    <asp:Panel ID="pnlGrid" runat="server" Width="600pt" Height="300" ScrollBars="Vertical" Visible="false">
        <asp:GridView ID="gvErrors" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="Row Number" DataField="pk_DonorList" />
                <asp:BoundField HeaderText="Error" DataField="UploadNotes" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnlProcess" runat="server" Width="600pt" Height="300" Visible="false">
        <asp:Label ID="lblMessage" runat="server" Text="" /><br /><br />
        <asp:Button ID="btnAddList" runat="server" Text="Add Donor List to Event" OnClick="btnAddList_Click" CssClass="defaultButton" />
    </asp:Panel>
</div>
</asp:Content>
