<%@ Page Title="" Language="C#" MasterPageFile="~/RSVP.Master" AutoEventWireup="true" CodeBehind="Confirmation.aspx.cs" Inherits="SPLC.Donor.RSVP.Confirmation" %>

<%@ Register Src="faq.ascx" TagName="faq" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div class="container">
        <section class="hero">
            <asp:Image ID="imgHeader" runat="server" AlternateText="SPLC header photo" />
        </section>

        <section>
            <h1 class="eventTitle">
                <asp:Label ID="lblEvent" runat="server" />
            </h1>

            <div>
                <asp:Literal ID="ltHeader" runat="server" />
            </div>

            <asp:Literal ID="litConfirm" runat="server" />

            <asp:Literal ID="ltFAQ" runat="server" />

            <p class="buttonDonate">
                <a href="https://donate.splcenter.org/sslpage.aspx?pid=463">Donate</a>
            </p>

        </section>
    </div>

</asp:Content>
