<%@ Page Title="" Language="C#" MasterPageFile="~/RSVP.Master" AutoEventWireup="true" CodeBehind="eventexpired.aspx.cs" Inherits="SPLC.Donor.RSVP.eventexpired" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div class="container">
        <section class="hero">
            <asp:Image ID="hdrImage" runat="server" ImageUrl="Images/EventImages/MD_RSVP_DC_7.jpg" AlternateText="SPLC header photo" Height="100%" Width="100%" />
        </section>

        <div class="container">
            <p class="message">We're sorry but this is not an active page. Please check the URL of the page you are looking for and try again. If you have questions please call Courtney Faulkner at 334-956-8269</p>
        </div>
    </div>
    

<%--    Legacy code left behind so not to recompile--%>
    <asp:Panel runat="server" ID="pnlContentBefore"></asp:Panel>
</asp:Content>
