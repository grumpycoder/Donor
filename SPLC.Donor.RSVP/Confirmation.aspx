<%@ Page Title="" Language="C#" MasterPageFile="~/RSVP.Master" AutoEventWireup="true" CodeBehind="Confirmation.aspx.cs" Inherits="SPLC.Donor.RSVP.Confirmation" %>
<%@ Register src="faq.ascx" tagname="faq" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div id="main-col-inner">
                                <asp:Panel ID="pnlContentBefore" runat="server" CssClass="region-content_before clear-block">
                                
                                    <div>
                                        <div class="field">
                                            <div class="field-items">
                                                <div class="field-item odd">
                                                    <asp:Image ID="imgHeader" runat="server" AlternateText="SPLC header photo" CssClass="HeaderImage" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                
                                </asp:Panel>
                                <div id="page-content" class="clear-block">

    <div id="page-content1" class="clear-block">
         <div id="content-area">
             <div id="content-area-top">
                 <div class="node node-type-page published node-detail node-type-page-detail has-no-comments node-has-region-page-pre-content-before node-has-region-body " id="node-5834">
                     <div class="node-inner">
                         <div class="node-contents node-contents-page">

                             

                             <div class="node-body">
                                 <div class="fieldlayout node-field-content" style="text-align: left;">
                                     <h1 class="eventTitle"><asp:Label ID="lblEvent" runat="server" /></h1>
                                     <asp:Literal ID="ltHeader" runat="server" />
                                     <br />
                                     <br />
                                     <asp:Literal ID="litConfirm" runat="server" />

                                 </div>
                             </div>

                             <br />

                    <asp:Literal ID="ltFAQ" runat="server" />
                             
                         </div>
                     </div>

                     <div class="clear-block">
                         <br />
                         <a href="https://donate.splcenter.org/sslpage.aspx?pid=463" target="_blank">
                             <img src="images/donate.jpg" border="0" />
                         </a>
                     </div>
                 </div>
             </div>
        </div>
                                                                                         
    </div>
</div>
        </div>
</asp:Content>
