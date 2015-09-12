<%@ Page Title="" Language="C#" MasterPageFile="~/RSVP.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SPLC.Donor.RSVP._default" %>
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


        <div id="page-content1" class="clear-block">
        <a name="content-top" id="content-top"></a>
        <div id="content-header" class="clear-block">
            <h1 class="eventTitle">
                <asp:Label ID="lblEvent" runat="server" />
                <%--&nbsp;&nbsp;<a href="FAQ/WestPalmBeach-March2014.pdf" target="_blank" class="FAQs">FAQs</a>--%></h1>
                                       
            <div id="socnet-container" class="region region-socnet">
                <div id="block-block-16" class="block block-block region-odd even region-count-1 count-4 clear-block">
                    <div>
                        <div class="block-content">
                            <!--paging_filter-->
                            <div>
                                <div>
                                    <asp:Literal ID="ltHeader" runat="server" />
                                </div>
                                </div>


                        </div></div></div></div></div>


    <div id="page-content1" class="clear-block">
        <a name="content-top" id="content-top"></a>
        <div id="content-header" class="clear-block">
            <h1 class="page-title">Event RSVP</h1>
            <div id="socnet-container">

                <asp:Panel ID="pnlRSVP" runat="server">

                <asp:Panel ID="pnlCapacity" runat="server" Visible="false">
                <p><b><u>This event has now reached capacity.  Please continue with the registration process.  Your name will be added to a wait list, and we will contact you by telephone if space becomes available.</u></b></p>
                </asp:Panel>
                    <div id="block-block-16" class="block block-block even clear-block">
                    <div>
                        <div class="block-content">
                            <!--paging_filter-->
                            <div>
                                
                                <br />
                                <p>
                                    Enter the reservation code from your invitation below, then click the "Submit" button to retrieve your response information.  
                                </p>
                            </div>
                            <br />
                            <asp:ValidationSummary ID="LoginValidationSummary" CssClass="required" runat="server" />
                        </div>
                    </div>
                </div>
                    </asp:Panel>

                <asp:Panel ID="pnlUnav" runat="server" Visible="false">
                    <p><b>I'm sorry, the online event rsvp system is currently unavailable. Please try again later or call Courtney Faulkner at 334-956-8269</b></p>
                </asp:Panel>


            </div>
        </div>

        <asp:Panel ID="pnlRSVP2" runat="server">

        <div id="content-area">
            <div id="content-area-top">
                <div class="node node-detail has-no-comments " id="node-5834">
                    <div class="node-inner">
                        <div class="node-contents">
                            <div class="node-body">
                                
                                <div>
                                    Reservation Code: 
                                    <asp:CustomValidator ID="ReservationCodeCustomValidator" runat="server" ErrorMessage="CustomValidator" ControlToValidate="txtFinderNumber" Display="None"></asp:CustomValidator>
<%--                                    <asp:RequiredFieldValidator ID="ReservationCodeRequiredFieldValidator" CssClass="required" runat="server" ControlToValidate="txtFinderNumber" ErrorMessage="Please enter a reservation code.">*</asp:RequiredFieldValidator>--%>
<%--                                    <asp:RegularExpressionValidator ID="ReservationCodeRegularExpressionValidator"  CssClass="required" runat="server"  ControlToValidate="txtFinderNumber" ValidationExpression="[0-9]+" Display="None" ErrorMessage="There appears to be a problem with the information that you have entered, please check the information and try again or call 334-956-8200 for assistance."></asp:RegularExpressionValidator>--%>
                                    <asp:TextBox ID="txtFinderNumber" MaxLength="13" runat="server"></asp:TextBox>
                                    &nbsp;
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ForeColor="white" BackColor="#006699" OnClick="btnSubmit_Click"  />
                                    <div class="sampleCodeTopMargin">
                                        For example:
                                        <br/>
                                        <asp:Label ID="lblExampleReservationWording" runat="server" CssClass="SampleCodeText" Text="Your reservation code is "/>
                                        <asp:Label ID="lblExampleReservationCode" CssClass="SampleCodeStyle" runat="server" Text="55566677788"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />

                    <asp:Literal ID="ltFAQ" runat="server" />

                </div>
            </div>
        </div>

            </asp:Panel>





    </div>
               
</div>
</div>

    
</asp:Content>
