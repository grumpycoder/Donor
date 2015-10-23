<%@ Page Title="" Language="C#" MasterPageFile="~/RSVPNoBrand.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SPLC.Donor.RSVP._default" %>

<%@ Register Src="faq.ascx" TagName="faq" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div class="container">
        <section class="hero">
            <asp:Image ID="imgHeader" runat="server" AlternateText="SPLC header photo" Height="100%" Width="100%" />
        </section>
        <section>
            <h1 class="eventTitle">
                <asp:Label ID="lblEvent" runat="server" />
            </h1>

            <div>
                <asp:Literal ID="ltHeader" runat="server" />
            </div>


            <asp:Panel ID="pnlRSVP" runat="server">

                <asp:Panel ID="pnlCapacity" runat="server" Visible="false">
                    <p>
                        <strong><u>This event has now reached capacity.  Please continue with the registration process.  Your name will be added to a wait list, and we will contact you by telephone if space becomes available.</u></strong>
                    </p>
                </asp:Panel>



                <div>
                    <div>
                        <p>
                            Enter the reservation code from your invitation below, then click the "Submit" button. 
                        </p>
                    </div>
                    <asp:ValidationSummary ID="LoginValidationSummary" CssClass="required" runat="server" />
                </div>

            </asp:Panel>

            <asp:Panel ID="pnlUnav" runat="server" Visible="false">
                <p>
                    <strong>I'm sorry, the online event rsvp system is currently unavailable. Please try again later or call Courtney Faulkner at 334-956-8269</strong>
                </p>
            </asp:Panel>


            <asp:Panel ID="pnlRSVP2" runat="server">

                <div>
                    <label>Reservation Code:</label>


                    <div>
                        <asp:TextBox ID="txtFinderNumber" MaxLength="13" runat="server"></asp:TextBox>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="form-submit" OnClick="btnSubmit_Click" />
                    </div>



                    <asp:CustomValidator ID="ReservationCodeCustomValidator" runat="server" ErrorMessage="CustomValidator" ControlToValidate="txtFinderNumber" Display="None" Enabled="True"></asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="ReservationCodeRequiredFieldValidator" CssClass="required" runat="server" ControlToValidate="txtFinderNumber" ErrorMessage="Please enter a reservation code.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ReservationCodeRegularExpressionValidator" CssClass="required" runat="server" ControlToValidate="txtFinderNumber" ValidationExpression="[0-9]+" Display="None" ErrorMessage="There appears to be a problem with the information that you have entered, please check the information and try again or call 334-956-8200 for assistance." Enabled="False"></asp:RegularExpressionValidator>


                    <p>
                        <span>For example:</span>
                        <label>Your reservation code is 55566677788</label>
                    </p>
                </div>

                <div>
                    <asp:Literal ID="ltFAQ" runat="server" />
                </div>

            </asp:Panel>


        </section>

    </div>

</asp:Content>
