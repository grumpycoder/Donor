<%@ Page Title="" Language="C#" MasterPageFile="~/RSVP.Master" AutoEventWireup="true" CodeBehind="DonorEvent.aspx.cs" Inherits="SPLC.Donor.RSVP.DonorEvent" %>

<%--<%@ Register src="faq.ascx" tagname="faq" tagprefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div class="container">
        <section class="hero">
            <asp:Image ID="imgHeader" runat="server"
                AlternateText="SPLC header photo" />
        </section>

        <section>
            <div class="container">
                <h2>
                    <asp:Label ID="lblEvent" runat="server" />
                </h2>

                <asp:Literal ID="ltHeader" runat="server" />

                <asp:Label ID="lblMessage" runat="server" />


                <h2>Please RSVP by selecting one of the options below.</h2>

                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="upGuests" runat="server">
                    <ContentTemplate>

                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="required" runat="server" ForeColor="Red" />

                        <div class="form-group">

                            <h3>Attending</h3>
                            <div class="radio-grp">
                                <asp:RadioButtonList ID="attendingRadio" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="attendingRadio_SelectedIndexChanged1" CssClass="" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Text=" Yes, I will attend" class="radio-btn"></asp:ListItem>
                                    <asp:ListItem Value="1" Text=" No, I am not able to attend" class="radio-btn"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <asp:RequiredFieldValidator ID="rfvYesNo" runat="server" ControlToValidate="attendingRadio" ErrorMessage="Please select Yes or No if you are attending." ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">

                            <h3>Number of Tickets</h3>

                            <div class="select" style="width: 100px;">
                                <asp:DropDownList ID="ddlNoGuests" runat="server" />
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>



                <%--            <h3>You may update your information below--%>
                <%--            </h3>--%>
                <h3>Please provide your name, address, phone number and email
                </h3>

                <div class="form-group">
                    <label>Name <span class="required">*</span></label>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="256" Width="100%" CssClass="input" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="required" runat="server" ErrorMessage="Please enter a name." ControlToValidate="txtName" ForeColor="Red" Visible="True"></asp:RequiredFieldValidator>
                </div>


                <div class="form-group half">
                    <label>Address 1 <span class="required">*</span></label>
                    <asp:TextBox ID="txtMailingAddress" MaxLength="256" runat="server" Width="100%" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="required" runat="server" ErrorMessage="Please enter a street address." ControlToValidate="txtMailingAddress" ForeColor="Red" Visible="true"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group half last">
                    <label>Address 2</label>
                    <asp:TextBox ID="txtAddress2" runat="server" Width="100%" />
                </div>


                <div class="clearfix"></div>

                <div class="form-group third">
                    <label>City <span class="required">*</span></label>
                    <asp:TextBox ID="txtCity" runat="server" Width="100%" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="required" runat="server" ErrorMessage="Please enter a city." ControlToValidate="txtCity" ForeColor="Red" Visible="true"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group third">
                    <label>State <span class="required">*</span></label>
                    <div class="select" style="width: 100%; display: inline-block;">
                        <asp:DropDownList ID="ddlState" runat="server" Width="300px">
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
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="required" runat="server" ErrorMessage="Please enter a state." ControlToValidate="ddlState" ForeColor="Red" Visible="true"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group third last">
                    <label>ZIP code <span class="required">*</span></label>
                    <asp:TextBox ID="txtZipCode" runat="server" MaxLength="5" Width="100%" />
                    <asp:RequiredFieldValidator ID="ZipCodeRequiredFieldValidator" CssClass="required" runat="server" ErrorMessage="Please enter a zip code." ControlToValidate="txtZipCode" ForeColor="Red" Visible="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ZipCodeRegularExpressionValidator" runat="server" ErrorMessage="Zip Code must have 5 numeric values." ControlToValidate="txtZipCode" Display="None" ValidationExpression="^\d{5}(-\d{4})?$" ForeColor="Red"></asp:RegularExpressionValidator>
                </div>

                <div class="clearfix"></div>

                <div class="form-group half">
                    <label>Phone Number <span class="required">*</span></label>
                    <asp:TextBox ID="txtPhoneNumber" MaxLength="13" runat="server" Width="100%" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="required" runat="server" ErrorMessage="Please enter a phone number." ControlToValidate="txtPhoneNumber" ForeColor="Red" Visible="true"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group half last">
                    <label>Email Address <span class="required">*</span></label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" Width="100%" />
                    <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ErrorMessage="Please enter an email address." ControlToValidate="txtEmail" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server" ErrorMessage="Please enter a valid email address." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ForeColor="Red"></asp:RegularExpressionValidator>

                </div>

                <div class="form-group">
                    <label>Comments</label>
                    <asp:TextBox ID="txtComments" MaxLength="1000" runat="server" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox>
                </div>

                <div class="form-group form-actions">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="form-submit" OnClientClick="window.scrollTo = function(x,y) { return true; };" />
                </div>



                <asp:Literal ID="ltFAQ" runat="server" />



            </div>
        </section>
    </div>

</asp:Content>
