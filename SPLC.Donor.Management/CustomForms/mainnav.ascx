<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mainnav.ascx.cs" Inherits="SPLC.Donor.Management.CustomForms.mainnav" %>

<div>
<asp:Image ID="imgHome" ImageUrl="~/images/login_assets/home_icon.png" runat="server" />
                        <asp:HyperLink ID="hlInitiatives" ImageUrl="~/images/navbuttons/home.png" runat="server"
                            Visible="true" NavigateUrl="~/default.aspx" CssClass="af_headerlink">Home</asp:HyperLink>
                        <asp:Image ID="imgSep1" ImageUrl="~/images/login_assets/menu_top_sep.png" runat="server" />
                        <asp:Image ID="imgAdmin" ImageUrl="~/images/login_assets/admin_icon.png" runat="server" />
                        <asp:HyperLink ID="hlAdmin" ImageUrl="~/images/navbuttons/newevent.png" runat="server"
                            NavigateUrl="~/Event.aspx" CssClass="af_headerlink">New Event</asp:HyperLink>
                        
                        <asp:Image ID="imgSearchSeparater" ImageUrl="~/images/login_assets/menu_top_sep.png" runat="server" />
                        <asp:Image ID="imgConfig" ImageUrl="~/images/login_assets/settings_icon19.png" runat="server" />
                        <asp:HyperLink ID="hlAppConfig" ImageUrl="~/images/navbuttons/reports.png" runat="server"
                            NavigateUrl="~/Reports.aspx" CssClass="af_headerlink">Reports</asp:HyperLink>

                        <asp:Image ID="imTaxSeparater" ImageUrl="~/images/login_assets/menu_top_sep.png" runat="server" />
                        <asp:HyperLink ID="hlTax" ImageUrl="~/images/navbuttons/tax.png" runat="server"
                            NavigateUrl="~/Tax.aspx" CssClass="af_headerlink">Tax</asp:HyperLink>

</div>
<div style="margin-top:4pt;">

<asp:HyperLink ID="HyperLink1" ImageUrl="~/images/navbuttons/eventlist.png" runat="server"
                            NavigateUrl="~/eventlistview.aspx" CssClass="af_headerlink">Event List</asp:HyperLink>
<asp:HyperLink ID="HyperLink2" ImageUrl="~/images/navbuttons/guestlist.png" runat="server"
                            NavigateUrl="~/donoreventlistview.aspx" CssClass="af_headerlink">Donor Event List</asp:HyperLink>
<asp:HyperLink ID="HyperLink3" ImageUrl="~/images/navbuttons/waitinglist.png" runat="server"
                            NavigateUrl="~/waitinglistview.aspx" CssClass="af_headerlink">Waiting List</asp:HyperLink>
    </div>