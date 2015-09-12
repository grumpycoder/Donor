<%@ Page Title="" Language="C#" MasterPageFile="~/RSVP.Master" AutoEventWireup="true" CodeBehind="DonorEvent.aspx.cs" Inherits="SPLC.Donor.RSVP.DonorEvent" %>
<%@ Register src="faq.ascx" tagname="faq" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/themes/DonorEvent.css" rel="stylesheet" />
    <script src="Scripts/DonorEvent.js" type="text/javascript"></script>
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
        <a name="content-top" id="content-top"></a>
        <div id="content-header" class="clear-block">
            <h1 class="eventTitle"><asp:Label ID="lblEvent" runat="server" /></h1>
                                       
            <div id="socnet-container" class="region region-socnet">
                <div id="block-block-16" class="block block-block region-odd even region-count-1 count-4 clear-block">
                    <div>
                        <div class="block-content">
                            <!--paging_filter-->
                            <div>
                                <div>
                                    <asp:Literal ID="ltHeader" runat="server" />
                                </div>
                                <asp:ValidationSummary ID="ValidationSummary1" CssClass="required" runat="server" />
                                <asp:Label ID="lblMessage" runat="server" />
                               
                                <div>
                                    <br/>
                                    <p class="txtMarginBottom">Please RSVP by selecting one of the options below.</p>
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel ID="upGuests" runat="server"><ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <b>Attending: </b>
                                            </td>
                                            <td>
                                                
                                                <asp:Label ID="NumGuestLabel"  CssClass="labelFW" runat="server" Text="Number of Tickets:"></asp:Label>
                                                <asp:DropDownList ID="ddlNoGuests" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="attendingRadio" runat="server" AutoPostBack="true" OnSelectedIndexChanged="attendingRadio_SelectedIndexChanged1">
                                                    <asp:ListItem Value="0" Text=" Yes, I will attend"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text=" No, I am not able to attend"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="rfvYesNo" runat="server" ControlToValidate="attendingRadio" ErrorMessage="Please select Yes or No if you are attending."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    </ContentTemplate></asp:UpdatePanel>
                                </div>
                                <br />
                                <div>
                                    <p class="txtMarginBottom">You may update your information below.<asp:Button ID="btnSubmit" runat="server" CssClass="SubmitButton" Text="Submit" OnClick="btnSubmit_Click" BackColor="#006699" /></p>
                                    <table>
                                        <%--<tr>
                                            <td>
                                                <b>Name:</b>
                                                
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="txtnoborder txtMarginBottom height" MaxLength="256"  Width="400px" /><br/>
                                                <asp:TextBox ID="txtMailingAddress" MaxLength="256" runat="server" CssClass="txtMarginBottom txtnoborder height" Width="250px" /><asp:TextBox ID="txtAddress2" runat="server" CssClass="txtMarginBottom txtnoborder" Width="180px" /><br/>
                                                <asp:TextBox ID="txtCity" runat="server" CssClass="txtSpace txtMarginBottom txtnoborder height" Width="100px" /> 
                                                <asp:DropDownList ID="ddlState" runat="server" Width="125px" CssClass="txtSpace txtnoborder height">
                                        <asp:ListItem Text="--" Value="" />
                                        <asp:ListItem Value="AL">Alabama</asp:ListItem>
	<asp:ListItem Value="AK">Alaska</asp:ListItem>
	<asp:ListItem Value="AZ">Arizona</asp:ListItem>
	<asp:ListItem Value="AR">Arkansas</asp:ListItem>
	<asp:ListItem Value="CA">California</asp:ListItem>
	<asp:ListItem Value="CO">Colorado</asp:ListItem>
	<asp:ListItem Value="CT">Connecticut</asp:ListItem>
	<asp:ListItem Value="DC">District of Columbia</asp:ListItem>
	<asp:ListItem Value="DE">Delaware</asp:ListItem>
	<asp:ListItem Value="FL">Florida</asp:ListItem>
	<asp:ListItem Value="GA">Georgia</asp:ListItem>
	<asp:ListItem Value="HI">Hawaii</asp:ListItem>
	<asp:ListItem Value="ID">Idaho</asp:ListItem>
	<asp:ListItem Value="IL">Illinois</asp:ListItem>
	<asp:ListItem Value="IN">Indiana</asp:ListItem>
	<asp:ListItem Value="IA">Iowa</asp:ListItem>
	<asp:ListItem Value="KS">Kansas</asp:ListItem>
	<asp:ListItem Value="KY">Kentucky</asp:ListItem>
	<asp:ListItem Value="LA">Louisiana</asp:ListItem>
	<asp:ListItem Value="ME">Maine</asp:ListItem>
	<asp:ListItem Value="MD">Maryland</asp:ListItem>
	<asp:ListItem Value="MA">Massachusetts</asp:ListItem>
	<asp:ListItem Value="MI">Michigan</asp:ListItem>
	<asp:ListItem Value="MN">Minnesota</asp:ListItem>
	<asp:ListItem Value="MS">Mississippi</asp:ListItem>
	<asp:ListItem Value="MO">Missouri</asp:ListItem>
	<asp:ListItem Value="MT">Montana</asp:ListItem>
	<asp:ListItem Value="NE">Nebraska</asp:ListItem>
	<asp:ListItem Value="NV">Nevada</asp:ListItem>
	<asp:ListItem Value="NH">New Hampshire</asp:ListItem>
	<asp:ListItem Value="NJ">New Jersey</asp:ListItem>
	<asp:ListItem Value="NM">New Mexico</asp:ListItem>
	<asp:ListItem Value="NY">New York</asp:ListItem>
	<asp:ListItem Value="NC">North Carolina</asp:ListItem>
	<asp:ListItem Value="ND">North Dakota</asp:ListItem>
	<asp:ListItem Value="OH">Ohio</asp:ListItem>
	<asp:ListItem Value="OK">Oklahoma</asp:ListItem>
	<asp:ListItem Value="OR">Oregon</asp:ListItem>
	<asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
	<asp:ListItem Value="RI">Rhode Island</asp:ListItem>
	<asp:ListItem Value="SC">South Carolina</asp:ListItem>
	<asp:ListItem Value="SD">South Dakota</asp:ListItem>
	<asp:ListItem Value="TN">Tennessee</asp:ListItem>
	<asp:ListItem Value="TX">Texas</asp:ListItem>
	<asp:ListItem Value="UT">Utah</asp:ListItem>
	<asp:ListItem Value="VT">Vermont</asp:ListItem>
	<asp:ListItem Value="VA">Virginia</asp:ListItem>
	<asp:ListItem Value="WA">Washington</asp:ListItem>
	<asp:ListItem Value="WV">West Virginia</asp:ListItem>
	<asp:ListItem Value="WI">Wisconsin</asp:ListItem>
	<asp:ListItem Value="WY">Wyoming</asp:ListItem>
                                    </asp:DropDownList>
                                                <asp:TextBox ID="txtZipCode" runat="server" CssClass="txtnoborder height" Width="100px" MaxLength="5" />
                                                <asp:RequiredFieldValidator ID="ZipCodeRequiredFieldValidator" CssClass="required" runat="server" ErrorMessage="Please enter a zip code." ControlToValidate="txtZipCode">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="ZipCodeRegularExpressionValidator" runat="server" ErrorMessage="Zip Code must have 5 numeric values." ControlToValidate="txtZipCode" Display="None" ValidationExpression="^\d{5}(-\d{4})?$"></asp:RegularExpressionValidator><br/>
                                                <b> Phone Number: </b><asp:TextBox ID="txtPhoneNumber" MaxLength="13" CssClass="txtnoborder height" Width="120" runat="server"/>&nbsp; &nbsp;<%--<asp:Label ID="Label1" runat="server" ForeColor="red" Text="*"/>--%>
                                                <b>Email Address: </b><asp:TextBox ID="txtEmail" runat="server" CssClass="txtnoborder height" Width="180px" TextMode="Email" />
                                                <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ErrorMessage="Please enter an email address." ControlToValidate="txtEmail" Display="None"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server" ErrorMessage="Please enter a valid email address." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" Display="None"></asp:RegularExpressionValidator>
                                                
                                                
                                            </td>
                                        </tr>
                                    </table>
                                    <br/>
                                    <p><b>Comments: </b>(Seating is limited but if you would like additional tickets please enter your request here.)</p>
                                    <asp:TextBox ID="txtComments" MaxLength="1000" runat="server" TextMode="MultiLine" CssClass="txtMarginBottom comment-Resize" Width="580px" Rows="3"></asp:TextBox>
                                    
                                    <br />

                    <asp:Literal ID="ltFAQ" runat="server" />
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="content-area">
            <div id="content-area-top">
                <div class="node node-detail has-no-comments" id="node-5834">
                    <div class="node-inner">
                        <div class="node-contents">
                            <div class="node-body">
                                <div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>



</div>
</div>
</asp:Content>
