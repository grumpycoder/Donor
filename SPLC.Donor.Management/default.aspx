<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SPLC.Donor.Management._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">

        window.onorientationchange = detectIPadOrientation;
        function detectIPadOrientation() {
            if (orientation == 0) {
                $("[id$='pnlIPad']").attr('class', 'IPad_maindiv_P');
                $("[id$='pnlIPDonorList']").attr('class', 'IPad_display_div_P');
            }
            else if (orientation == 90) {
                $("[id$='pnlIPad']").attr('class', 'IPad_maindiv_L');
                $("[id$='pnlIPDonorList']").attr('class', 'IPad_display_div_L');
            }
            else if (orientation == -90) {
                $("[id$='pnlIPad']").attr('class', 'IPad_maindiv_L');
                $("[id$='pnlIPDonorList']").attr('class', 'IPad_display_div_L');
            }
            else if (orientation == 180) {
                $("[id$='pnlIPad']").attr('class', 'IPad_maindiv_P');
                $("[id$='pnlIPDonorList']").attr('class', 'IPad_display_div_P');
            }
        }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<asp:Panel ID="pnlPC" runat="server" Visible="false" CssClass="default_maindiv">

    <div class="default_hypelink_div" style=" width:300pt; margin-left:10pt;">
        <h2>Register Guest For Event</h2>
        <asp:UpdatePanel ID="upRegistration" runat="server"><ContentTemplate>
        <table style="border: double 2px #8A0808; padding:4px 4px 4px 4px;">
            <tr>
                <td>
                    <table>
                        <tr>
                <th>Event Name:</th>
                <td colspan="2"><asp:DropDownList ID="ddlEvents" runat="server" /></td>
            </tr>
            <tr>
                <th>Donor ID:</th>
                <td><asp:TextBox ID="txtDonorID" runat="server" OnTextChanged="txtDonorID_TextChanged" AutoPostBack="true" /></td>
                <th>Guest: <asp:CheckBox ID="chkGuest" runat="server" AutoPostBack="true" OnCheckedChanged="chkGuest_CheckedChanged" /></th>
            </tr>
            <tr>
                <th>Attending #:</th>
                <td><asp:CheckBox ID="chkAttending" runat="server" /><asp:TextBox ID="txtAttending" runat="server" Width="25" Text="0" /></td>
            </tr>
            <tr>
                <td colspan="3"><asp:Label ID="lblMessage" runat="server" ForeColor="Red" /></td>
            </tr>
            
                    </table>

                </td>
            </tr>
            <tr>
               <td>
                   <hr />

                   <asp:Panel ID="pnlDemo" runat="server" Enabled="false">
                       
 
                   <table>
                       <tr>
                         <th>Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtName"></asp:RequiredFieldValidator></th>
                           <td><asp:TextBox ID="txtName" runat="server" Width="300" /></td>
                       </tr>
                       <tr>
                         <th>Address:</th>
                           <td><asp:TextBox ID="txtAddress" runat="server" Width="300" /></td>
                       </tr>
                       <tr>
                         <th>City:</th>
                           <td><asp:TextBox ID="txtCity" runat="server"  />
                               <ajaxToolkit:MaskedEditExtender ID="meeCity" runat="server" TargetControlID="txtCity" Mask="LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL"></ajaxToolkit:MaskedEditExtender>
                           </td>
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
                         <th>ZipCode:</th>
                           <td><asp:TextBox ID="txtZipCode" runat="server" />
                               <ajaxToolkit:MaskedEditExtender ID="meeZipCode" runat="server" TargetControlID="txtZipCode" Mask="99999-9999"></ajaxToolkit:MaskedEditExtender>

                           </td>
                       </tr>
                       <tr>
                         <th>Phone:</th>
                           <td><asp:TextBox ID="txtPhone" runat="server" />
                               <ajaxToolkit:MaskedEditExtender ID="meePhone" runat="server" TargetControlID="txtPhone" Mask="(999) 999-9999"></ajaxToolkit:MaskedEditExtender>
                           </td>
                       </tr>
                       <tr>
                         <th>Email:<asp:RegularExpressionValidator ID="reeEmail" runat="server" ErrorMessage="*" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"></asp:RegularExpressionValidator></th>
                           <td><asp:TextBox ID="txtEmail" runat="server" Width="300" />
                               
                           </td>
                       </tr>
                   </table>
                   </asp:Panel>
               </td>
            </tr>

            <tr>
               <td>
                   <hr />
                   <table>
                       <tr>
                <td><asp:Button ID="btnRegisterUser" runat="server" Text="Register User" CssClass="defaultButton" OnClick="btnRegisterUser_Click" Visible="false"/></td>
            </tr>
                   </table>

               </td>
            </tr>

            
        </table>
            </ContentTemplate></asp:UpdatePanel>


    </div>
    <asp:Panel ID="pnlGrid" runat="server" Height="300pt" Width="350pt" ScrollBars="Vertical" CssClass="default_hypelink_div">
    
        <asp:GridView ID="gvRegistrations" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
            <Columns>
                
                <asp:BoundField HeaderText="Event" DataField="EventName" />
                <asp:BoundField HeaderText="Attending" DataField="Attending" />
                <asp:BoundField HeaderText="Tickets" DataField="TicketsRequested" />
                <asp:BoundField HeaderText="Name" DataField="AccountName" />
                <asp:BoundField HeaderText="Response Date" DataField="Response_Date" />
                <asp:HyperLinkField DataNavigateUrlFields="pk_DonorEventList" 
                    DataNavigateUrlFormatString="~/DonorEventListDetails.aspx?delid={0}" Text="EDIT" />
            </Columns>
        </asp:GridView>

    </asp:Panel>
</asp:Panel>

<asp:Panel ID="pnlIPad" runat="server" Visible="false" CssClass="IPad_maindiv_L">
    <table>
        <tr>
            <td>
                <table class="tbIPAD">

                        <tr>
                            <th>Event:</th>
                            <td colspan="3"><asp:DropDownList ID="ddlEvent2" runat="server" CssClass="fontLarge" /></td>
                        </tr>

                        <tr>
                            <th>Name Part:</th>
                            <td>
                                <asp:TextBox ID="txtLName" runat="server" CssClass="fontLarge" Text="" Width="370" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearchDonor" runat="server" CssClass="IPadButton" OnClick="btnSearchDonor_Click" Text="Search" />
                            </td>
                        </tr>
                </table>
                <hr />
            <asp:Panel ID="pnlIPDonorList" runat="server" ScrollBars="Vertical" CssClass="IPad_display_div_L">

                <asp:GridView ID="gvDonorList" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Font-Size="14pt">
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
            <Columns>
                
                <asp:BoundField HeaderText="AccountName" DataField="AccountName" />
                <asp:BoundField HeaderText="Attending" DataField="Attending" />
                <asp:BoundField HeaderText="Tickets" DataField="TicketsRequested" />
                <asp:BoundField HeaderText="Response Date" DataField="Response_Date" />
            </Columns>
        </asp:GridView>

                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
</asp:Content>
