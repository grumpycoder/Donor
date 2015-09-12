<%@ Page Title="" Language="C#" MasterPageFile="~/TaxReceipt.Master" AutoEventWireup="true" CodeBehind="TaxReceipt.aspx.cs" Inherits="SPLC.Donor.RSVP.TaxReceipt1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <br/>
        <asp:Label ID="InstructionLabel" runat="server" Font-Size="8pt" CssClass="InstructionLabel" >To download a PDF of your tax receipt, click <img id="ctl00_ContentPlaceHolder1_ReportViewer1_ctl05_ctl04_ctl00_ButtonImg" src="/Reserved.ReportViewerWebControl.axd?OpType=Resource&amp;Version=11.0.2802.16&amp;Name=Microsoft.Reporting.WebForms.Icons.Export.gif" alt="Export drop down menu" style="border-style:None;height:16px;width:16px;"/> in the toolbar below.  </asp:Label>
        <br/><br/>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <rsweb:ReportViewer ID="rvTaxReceipt"  runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="685px" Width="680px">
            <LocalReport ReportPath="Templates\TaxReport.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="CONSTR" Name="TaxReceipt" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>

    </div>
</asp:Content>
