using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SPLC.Donor.Models;

namespace SPLC.Donor.Management
{
    public partial class DonorEventListDetails : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblHeader.Text = "Donor Event Details";

                if (!Page.IsPostBack)
                {
                    if (Request["delid"] != null)
                    {
                        DonorEventList DEL = new DonorEventList(_ConnStr, User.Identity.Name, int.Parse(Request["delid"].ToString()));
                        EventList EL = new EventList(_ConnStr, User.Identity.Name, DEL.fk_Event);
                        DonorList DL = new DonorList(DEL.fk_DonorList);

                        hfPK.Value = DEL.pk_DonorEventList.ToString();
                        hfDPK.Value = DL.pk_DonorList.ToString();

                        lblEventName.Text = EL.EventName;
                        lblAccountID.Text = DL.pk_DonorList.ToString();


                        if (DEL.TicketsMailed_Date > DateTime.Parse("1/1/2000"))
                        {
                            btnMailCard.Visible = false;
                            lblCardMailed.Visible = true;
                            lblCMailed.Visible = true;
                            lblCMailed.Text = DEL.TicketsMailed_Date.ToShortDateString();
                        }
                        else
                        {
                            btnMailCard.Visible = true;
                            lblCardMailed.Visible = false;
                            lblCMailed.Visible = false;
                        }

                        if (DEL.WaitingList_Date > DateTime.Parse("1/1/2000"))
                            lblWaitListNote.Visible = true;
                        else
                            lblWaitListNote.Visible = false;

                        if (DEL.UpdatedInfo)
                            btnUpdateDemo.Visible = true;
                        else
                            btnUpdateDemo.Visible = false;


                        hfTicketsAllowed.Value = EL.TicketsAllowed.ToString();
                        txtAttending.Text = DEL.TicketsRequested.ToString();
                        
                        if (DEL.Attending)
                            chkAttending.Checked = true;
                        else
                            chkAttending.Checked = false;

                        lblDonorComments.Text = DEL.DonorComments.ToString();

                        txtName.Text = DL.AccountName.ToString();
                        txtAddress.Text = DL.AddressLine1.ToString();
                        txtCity.Text = DL.City.ToString();
                        ddlState.SelectedValue = DL.State.ToString();
                        txtZipCode.Text = DL.PostCode.ToString();
                        txtPhone.Text = DL.PhoneNumber.ToString();
                        txtSPLCComments.Text = DEL.SPLCComments.ToString();
                        txtEmail.Text = DL.EmailAddress.ToString();

                    }
                }
            }

        }

        protected void btnMailCard_Click(object sender, EventArgs e)
        {
            DonorEventList DEL = new DonorEventList(_ConnStr, User.Identity.Name, int.Parse(Request["delid"].ToString()));
            DEL.TicketsMailed_Date = DateTime.Now;
            DEL.TicketsMailed_User = User.Identity.Name;
            DEL.TicketsRequested = int.Parse(txtAttending.Text.ToString());
            DEL.MailCards();

            btnMailCard.Visible = false;
            lblCardMailed.Visible = true;
            lblCMailed.Visible = true;
            lblCMailed.Text = DEL.TicketsMailed_Date.ToShortDateString();

            Response.Redirect("DonorEventListDetails.aspx?delid=" + DEL.pk_DonorEventList.ToString());
        }

        protected void btnUpdateDemo_Click(object sender, EventArgs e)
        {
            DonorEventList DEL = new DonorEventList(_ConnStr, User.Identity.Name, int.Parse(Request["delid"].ToString()));
            DEL.UpdatedInfo = false;
            DEL.UpdatedInfo_User = null;
            DEL.UpdatedInfoDateTime = DateTime.Parse("1/1/1000");
            DEL.Update();

            Response.Redirect("DonorEventListDetails.aspx?delid=" + DEL.pk_DonorEventList.ToString());
        }
    }
}