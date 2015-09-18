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
        private const string BaseDate = "1/1/2000";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            lblHeader.Text = "Donor Event Details";

            if (Page.IsPostBack) return;

            if (Request["delid"] == null) return;

            var donorEventList = new DonorEventList(User.Identity.Name, int.Parse(Request["delid"].ToString()));
            var eventList = new EventList(User.Identity.Name, donorEventList.fk_Event);
            var donorList = new DonorList(donorEventList.fk_DonorList);

            hfPK.Value = donorEventList.pk_DonorEventList.ToString();
            hfDPK.Value = donorList.pk_DonorList;

            lblEventName.Text = eventList.EventName;
            lblAccountID.Text = donorList.pk_DonorList;


            if (donorEventList.TicketsMailed_Date > DateTime.Parse(BaseDate))
            {
                btnMailCard.Visible = false;
                lblCardMailed.Visible = true;
                lblCMailed.Visible = true;
                lblCMailed.Text = donorEventList.TicketsMailed_Date.ToShortDateString();
            }
            else
            {
                btnMailCard.Visible = true;
                lblCardMailed.Visible = false;
                lblCMailed.Visible = false;
            }

            lblWaitListNote.Visible = donorEventList.WaitingList_Date > DateTime.Parse(BaseDate);

            btnUpdateDemo.Visible = donorEventList.UpdatedInfo;


            hfTicketsAllowed.Value = eventList.TicketsAllowed.ToString();
            txtAttending.Text = donorEventList.TicketsRequested.ToString();
                        
            chkAttending.Checked = donorEventList.Attending;

            lblDonorComments.Text = donorEventList.DonorComments;

            txtName.Text = donorList.AccountName;
            txtAddress.Text = donorList.AddressLine1;
            txtCity.Text = donorList.City;
            ddlState.SelectedValue = donorList.State;
            txtZipCode.Text = donorList.PostCode;
            txtPhone.Text = donorList.PhoneNumber;
            txtSPLCComments.Text = donorEventList.SPLCComments;
            txtEmail.Text = donorList.EmailAddress;
        }

        protected void btnMailCard_Click(object sender, EventArgs e)
        {
            
            var donorEventList = new DonorEventList(User.Identity.Name, int.Parse(Request["delid"]))
            {
                TicketsMailed_Date = DateTime.Now,
                TicketsMailed_User = User.Identity.Name,
                TicketsRequested = int.Parse(txtAttending.Text),
                WaitingList_Date = DateTime.Parse(BaseDate), 
                WaitingListOrder = 0
            };
            donorEventList.SaveChanges();

            btnMailCard.Visible = false;
            lblCardMailed.Visible = true;
            lblCMailed.Visible = true;
            lblCMailed.Text = donorEventList.TicketsMailed_Date.ToShortDateString();

            Response.Redirect("DonorEventListDetails.aspx?delid=" + donorEventList.pk_DonorEventList);
        }

        protected void btnUpdateDemo_Click(object sender, EventArgs e)
        {
            var donorEventList = new DonorEventList(User.Identity.Name, int.Parse(Request["delid"]))
            {
                UpdatedInfo = false,
                UpdatedInfo_User = null,
                UpdatedInfoDateTime = DateTime.Parse("1/1/1000")
            };
            donorEventList.Update();

            Response.Redirect("DonorEventListDetails.aspx?delid=" + donorEventList.pk_DonorEventList);
        }
    }
}