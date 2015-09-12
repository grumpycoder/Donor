<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="DonorEventListDetails.aspx.cs" Inherits="SPLC.Donor.Management.DonorEventListDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormScripts/DonorEventListDetails.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dialog-modal" title="Notification">
  <p></p>
</div>

    <asp:HiddenField ID="hfPK" runat="server" />
    <asp:HiddenField ID="hfDPK" runat="server" />
    <input id="hvTF" value="" type="hidden" />

<h1><asp:Label ID="lblHeader" runat="server" /></h1>

    <asp:UpdatePanel ID="upMain" runat="server"><ContentTemplate>
<div class="default_maindiv">
    <div class="default_hypelink_div" style=" width:300pt; margin-left:10pt;">
        <table>
            <tr>
                <th>Event Name:</th>
                <td><asp:Label ID="lblEventName" runat="server" /></td>
            </tr>
        </table>
        <table style="width:600pt;border-top: solid 1pt grey;padding:4pt;">
            <tr>
                <th>Account ID:</th>
                <td><asp:Label ID="lblAccountID" runat="server" /></td>
                <th><asp:Label ID="lblCardMailed" runat="server" Text="Tickets Mailed:" /></th>
                <td><asp:Label ID="lblCMailed" runat="server" />
                    <asp:Button ID="btnMailCard" runat="server" Text="Mail Tickets" CssClass="defaultButton" OnClick="btnMailCard_Click" />
                </td>
                <th><asp:Label ID="lblWaitListNote" runat="server" Text="-- On Waiting List--" ForeColor="Salmon" /></th>
                <th>Attending:</th>
                <td><asp:TextBox ID="txtAttending" runat="server" Width="15pt" /><asp:HiddenField ID="hfTicketsAllowed" runat="server" /></td>
                <td><asp:CheckBox ID="chkAttending" runat="server" /></td>
            </tr>
        </table>

        <table style="width:600pt;border-top: solid 1pt grey;padding:4pt;">
            <tr>
                <th style="width:80pt;">Donor Comments:</th>
                <td style="width:500pt"><asp:Label ID="lblDonorComments" runat="server" /></td>
            </tr>
        </table>
        <table style="width:600pt;border-top: solid 1pt grey; padding-top:4pt;">

                        <tr>
                            <th>Name:</th>
                            <td><asp:TextBox ID="txtName" runat="server" /></td>
                            <th>Phone Number:</th>
                            <td><asp:TextBox ID="txtPhone" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>Address:</th>
                            <td><asp:TextBox ID="txtAddress" runat="server" Width="250" /></td>
                            <td rowspan="4">
                                <asp:Button ID="btnUpdateDemo" runat="server" Text="Updated On System" CssClass="defaultButton" OnClick="btnUpdateDemo_Click" />
                            </td>
                        </tr>
                        <tr>
                            <th>City:</th>
                            <td><asp:TextBox ID="txtCity" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>State:</th>
                            <td><asp:DropDownList ID="ddlState" runat="server">
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
                                    </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <th>Zip Code:</th>
                            <td><asp:TextBox ID="txtZipCode" runat="server" Width="100" /></td>
                        </tr>
            <tr>
                            <th>Email:</th>
                            <td><asp:TextBox ID="txtEmail" runat="server" Width="350" /></td>
                        </tr>
</table>
            <table style="width:600pt;border-top: solid 1pt grey;padding:4pt;">
            <tr>
                <th style="width:80pt; vertical-align:top;">SPLC Comments:</th>
                <td style="width:500pt"><asp:TextBox ID="txtSPLCComments" runat="server" TextMode="MultiLine" Width="600" Height="100" /></td>
            </tr>
        </table>
            
        
    </div>
</div>
        </ContentTemplate></asp:UpdatePanel>
</asp:Content>
