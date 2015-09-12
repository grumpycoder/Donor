<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Event.aspx.cs" Inherits="SPLC.Donor.Management.Event" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormScripts/Event.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $("#tabs").tabs({
                show: function() {
                    var sel = $('#tabs').tabs('option', 'active');
                    $("#<%= hfTabs.ClientID %>").val(sel);
                },
                active: <%= hfTabs.Value %>
                });
        })

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dialog-modal" title="Notification">
  <p></p>
</div>

    <asp:HiddenField ID="hfPK" runat="server" />
    <input id="hvTF" value="" type="hidden" />


    <h1><asp:Label ID="lblHeader" runat="server" /></h1>
    <div class="default_maindiv">

    <asp:Panel ID="pnlAddEvent" runat="server">
        <table style="width:600pt; border-bottom: solid 2px black; padding-bottom:5px;">
                <tr>
                    <th>Event Name:</th>
                    <td><asp:TextBox ID="txtNEventName" runat="server" Width="200pt" /></td>
                    <th>Event Date:</th>
                    <td><asp:TextBox ID="txtNEventDate" runat="server" Enabled="true" /></td>
                    <td><asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="defaultButton" OnClick="btnAddNew_Click" /></td>
                </tr>
                <tr>
                    <td colspan="5"><asp:Label ID="lblError" runat="server" ForeColor="Red" /></td>
                </tr>
            </table>
    </asp:Panel>

    <asp:Panel ID="pnlEditEvent" runat="server" Visible="false">
        <div id="tabs">

  <ul>

    <li><a href="#tabs-1">Event Details</a></li>
    <li><a href="#tabs-2">Donor Lists</a></li>
    <li><a href="#tabs-3">Image Upload</a></li>
    <li><a href="#tabs-4">Header HTML</a></li>
    <li><a href="#tabs-5">FAQ HTML</a></li>
    <li><a href="#tabs-6">Yes HTML</a></li>
    <li><a href="#tabs-7">No HTML</a></li>
    <li><a href="#tabs-8">Wait HTML</a></li>

  </ul>

  <div id="tabs-1">

      <div class="default_hypelink_div">
        <asp:Panel ID="pnlEvent" runat="server">
            <table style="width:600pt; border-bottom: solid 2px black; padding-bottom:5px;">
                <tr>
                    <th>Event Name:</th>
                    <td><asp:TextBox ID="txtEventName" runat="server" Width="200pt" /></td>
                    <th>Event Date:</th>
                    <td><asp:TextBox ID="txtEventDate" runat="server" Enabled="false" /></td>
                </tr>
                <tr>
                    <th>Display Name:</th>
                    <td><asp:TextBox ID="txtDisplayName" runat="server" Width="200pt" /></td>
                </tr>
            </table>
        </asp:Panel>

        <div id="dvDetails" >
            <table style="width:600pt; border-bottom: solid 2px black; padding-bottom:5px;">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <th>Active:</th>
                                <td><asp:CheckBox ID="cbActive" runat="server" /> (Unchecked=Canceled)</td>
                            </tr>
                            <tr>
                                <th>Speaker:</th>
                                <td><asp:TextBox ID="txtSpeaker" runat="server" Width="200pt" /></td>
                            </tr>
                            <tr>
                                <th>Venue Name:</th>
                                <td><asp:TextBox ID="txtVName" runat="server" Width="200pt" /></td>
                            </tr>
                            <tr>
                                <th>Venue Address:</th>
                                <td><asp:TextBox ID="txtVAddress" runat="server" Width="200pt" /></td>
                            </tr>
                            <tr>
                                <th>Venue City:</th>
                                <td><asp:TextBox ID="txtVCity" runat="server" /></td>
                            </tr>
                            <tr>
                                <th>Venue State:</th>
                                <td>
                                    
                                    <asp:DropDownList ID="ddlState" runat="server">
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
                                <th>Venue Zip Code:</th>
                                <td><asp:TextBox ID="txtVZipCode" runat="server" Width="80pt" /></td>
                            </tr>
                            <tr>
                                <th>Capacity:</th>
                                <td><asp:TextBox ID="txtCapasity" runat="server" Width="40pt" /></td>
                            </tr>
                        </table>

                    </td>

                    <td style="vertical-align:top;">
                        <table>
                            <tr>
                                <th>Tickets Allowed:</th>
                                <td><asp:TextBox ID="txtTicketsAllowed" runat="server" Width="15pt" /></td>
                            </tr>
                            <tr>
                                <th>Start Time:</th>
                                <td><asp:TextBox ID="txtSTimeHour" runat="server" Width="15pt" Text="00" />:<asp:TextBox ID="txtSTimeMin" runat="server" Width="15pt" Text="00" />
                                    <asp:DropDownList ID="ddlStartTime" runat="server">
                                        <asp:ListItem Text="AM" Value="AM" />
                                        <asp:ListItem Text="PM" Value="PM" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>End Time:</th>
                                <td><asp:TextBox ID="txtETimeHour" runat="server" Width="15pt" Text="00" />:<asp:TextBox ID="txtETimeMin" runat="server" Width="15pt" Text="00" />
                                    <asp:DropDownList ID="ddlEndTime" runat="server">
                                        <asp:ListItem Text="AM" Value="AM" />
                                        <asp:ListItem Text="PM" Value="PM" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>Doors Open Time:</th>
                                <td><asp:TextBox ID="txtOOTimeHour" runat="server" Width="15pt" Text="00" />:<asp:TextBox ID="txtOOTimeMin" runat="server" Width="15pt" Text="00" />
                                    <asp:DropDownList ID="ddlOOTime" runat="server">
                                        <asp:ListItem Text="AM" Value="AM" />
                                        <asp:ListItem Text="PM" Value="PM" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td><br /></td>
                            </tr>
                            <tr>
                                <th>Online Close Time:</th>
                                <td><asp:TextBox ID="txtOnlineCloseDate" runat="server" />
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>

            <table style="width:600pt; border-bottom: solid 2px black; padding-bottom:5px;">
                <tr>
                    <th>Ticket URL:</th>
                    <td><asp:HyperLink ID="hlTicketURL" runat="server">HyperLink</asp:HyperLink></td>
                </tr>
                <tr>
                    <th>RSVP Event URL:</th>
                    <td><asp:HyperLink ID="hlEventURL" runat="server">HyperLink</asp:HyperLink></td>
                </tr>
            </table>

            
 
         </div>



    </div>
    
  </div>

  <div id="tabs-2">


      <div class="default_maindiv">
  <div class="default_hypelink_div">
      <asp:Panel ID="pnlUpload" runat="server" Visible="true">
          <asp:HiddenField ID="hfTabs" runat="server" Value="0"/>
      <asp:FileUpload ID="fuDonorFile" runat="server" /><br /><br />
      <asp:Button ID="btnUpload" runat="server" Text="Upload Guest List" OnClick="btnUpload_Click" CssClass="defaultButton" />
      <br /><br />
      <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>

      </asp:Panel>
  </div>

    <asp:Panel ID="pnlGrid" runat="server" Width="600pt" Height="300" ScrollBars="Vertical" Visible="false">
        <asp:GridView ID="gvErrors" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="Row Number" DataField="pk_DonorList" />
                <asp:BoundField HeaderText="Error" DataField="UploadNotes" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
          <asp:Label ID="lblMessage" runat="server" Text="" />
</div>

  </div>

  <div id="tabs-3">
      <asp:FileUpload ID="fuImage" runat="server" /><br /><br />
      <asp:Button ID="btnImageUpload" runat="server" Text="Upload Header Image File" CssClass="defaultButton" OnClick="btnImageUpload_Click"  />
      <br /><br />
      <asp:Label ID="lblImageMessage" runat="server" ForeColor="Red"></asp:Label>
      
      <asp:Image ID="imgHeader" runat="server" />
     
  </div>

  <div id="tabs-4">
      <asp:UpdatePanel ID="upHeader" runat="server"><ContentTemplate>
          <asp:Button ID="btnUpdateHeader" runat="server" Text="Save Header" CssClass="defaultButton" OnClick="btnUpdateHeader_Click" /><br /><br />

    <div class="default_maindiv">
        <asp:TextBox ID="txtHeader" TextMode="MultiLine" Columns="120" Rows="15" runat="server"></asp:TextBox>
        <ajaxToolkit:HtmlEditorExtender ID="heeHeader" TargetControlID="txtHeader" runat="server" DisplaySourceTab="True" EnableSanitization="false"></ajaxToolkit:HtmlEditorExtender>

    </div>

    </ContentTemplate></asp:UpdatePanel>
  </div>

  <div id="tabs-5">
      <asp:UpdatePanel ID="upFAQ" runat="server"><ContentTemplate>
          <asp:Button ID="btnUpdateFAQ" runat="server" Text="Save FAQ" CssClass="defaultButton" OnClick="btnUpdateFAQ_Click" /><br /><br />

    <div class="default_maindiv">
        <asp:TextBox ID="txtFAQ" TextMode="MultiLine" Columns="120" Rows="15" runat="server"></asp:TextBox>
        <ajaxToolkit:HtmlEditorExtender ID="heeFAQ" TargetControlID="txtFAQ" runat="server" DisplaySourceTab="True" EnableSanitization="false"></ajaxToolkit:HtmlEditorExtender>

    </div>

    </ContentTemplate></asp:UpdatePanel>
  </div>
  
  <div id="tabs-6">
      <asp:UpdatePanel ID="upYes" runat="server"><ContentTemplate>
          <asp:Button ID="btnUpdateYes" runat="server" Text="Save Yes" CssClass="defaultButton" OnClick="btnUpdateYes_Click" /><br /><br />

    <div class="default_maindiv">
        <asp:TextBox ID="txtYes" TextMode="MultiLine" Columns="120" Rows="15" runat="server"></asp:TextBox>
        <ajaxToolkit:HtmlEditorExtender ID="heeYes" TargetControlID="txtYes" runat="server" DisplaySourceTab="True" EnableSanitization="false"></ajaxToolkit:HtmlEditorExtender>

    </div>

    </ContentTemplate></asp:UpdatePanel>
  </div>

  <div id="tabs-7">
      <asp:UpdatePanel ID="upNo" runat="server"><ContentTemplate>
          <asp:Button ID="btnUpdateNo" runat="server" Text="Save No" CssClass="defaultButton" OnClick="btnUpdateNo_Click" /><br /><br />

    <div class="default_maindiv">
        <asp:TextBox ID="txtNo" TextMode="MultiLine" Columns="120" Rows="15" runat="server"></asp:TextBox>
        <ajaxToolkit:HtmlEditorExtender ID="heeNo" TargetControlID="txtNo" runat="server" DisplaySourceTab="True" EnableSanitization="false"></ajaxToolkit:HtmlEditorExtender>

    </div>

    </ContentTemplate></asp:UpdatePanel>
  </div>

  <div id="tabs-8">
      <asp:UpdatePanel ID="upWait" runat="server"><ContentTemplate>
          <asp:Button ID="btnUpdateWait" runat="server" Text="Save Wait" CssClass="defaultButton" OnClick="btnUpdateWait_Click" /><br /><br />

    <div class="default_maindiv">
        <asp:TextBox ID="txtWait" TextMode="MultiLine" Columns="120" Rows="15" runat="server"></asp:TextBox>
        <ajaxToolkit:HtmlEditorExtender ID="heeWait" TargetControlID="txtWait" runat="server" DisplaySourceTab="True" EnableSanitization="false"></ajaxToolkit:HtmlEditorExtender>

    </div>

    </ContentTemplate></asp:UpdatePanel>
  </div>

            </div>
</asp:Panel>





    
</div>
</asp:Content>
