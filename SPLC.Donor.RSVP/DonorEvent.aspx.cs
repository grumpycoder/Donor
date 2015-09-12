using System;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Web.UI.WebControls;
using SPLC.Donor.Models;

namespace SPLC.Donor.RSVP
{
    public partial class DonorEvent : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";

                if (Page.IsPostBack) return;

                var donorEventList = (DonorEventList)Session["SPLC.Donor.RSVP.DEL"];
                var donorList = (DonorList)Session["SPLC.Donor.RSVP.DL"];
                var eventList = new EventList(ConnectionString, User.Identity.Name, donorEventList.fk_Event);

                if (donorList.IsValid)
                {
                    txtName.Text = donorList.AccountName;
                    txtMailingAddress.Text = donorList.AddressLine1;
                    txtAddress2.Text = donorList.AddressLine2;
                    txtCity.Text = donorList.City;
                    ddlState.SelectedValue = donorList.State;
                    txtZipCode.Text = donorList.PostCode;
                    txtEmail.Text = donorList.EmailAddress;
                    txtPhoneNumber.Text = donorList.PhoneNumber;

                    ddlNoGuests.Items.Clear();

                    // Add the allowed ticket number
                    for (int i = 0; i <= eventList.TicketsAllowed; i++)
                    {
                        ddlNoGuests.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }

                    // Add HTML from DB

                    var sbHeader = new StringBuilder(eventList.HTML_Header);
                    var donorEmail = new DonorEmail();

                    sbHeader = donorEmail.ParseTextSubEL(sbHeader, eventList);
                    ltHeader.Text = sbHeader.ToString();  // EL.HTML_Header;

                    var faq = new StringBuilder(eventList.HTML_FAQ);
                    faq = donorEmail.ParseTextSubEL(faq, eventList);
                    ltFAQ.Text = faq.ToString();

                    lblEvent.Text = eventList.DisplayName;

                    imgHeader.ImageUrl = "ihandler.ashx?eid=" + eventList.pk_Event;

                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = @"There appears to be a problem with the information that you have entered, please check 
                                            the information and try again or call 334-956-8200 for assistance.";
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                if (attendingRadio.SelectedValue.Equals("0") && (ddlNoGuests.SelectedValue.Equals("0")))
                    throw new Exception("Please select the number of tickets that you are requesting.");

                if (attendingRadio.SelectedValue.Equals("1") && (!ddlNoGuests.SelectedValue.Equals("0")))
                    throw new Exception("If you are not attending please select (0) for the number of Guests attending.");

                var donorEventList = (DonorEventList)Session["SPLC.Donor.RSVP.DEL"];
                var donorList = (DonorList)Session["SPLC.Donor.RSVP.DL"];


                // Update Donor Information
                if (UpdateDonorList(donorList))
                {
                    donorEventList.UpdatedInfo = true;
                    donorEventList.UpdatedInfoDateTime = DateTime.Now;
                    donorEventList.UpdatedInfo_User = donorList.AccountName;
                }

                donorList.EmailAddress = txtEmail.Text;
                //DL.Update();

                if (donorEventList.pk_DonorEventList <= 0)
                    throw new Exception("Donor ID is not registered with this Event!");

                if (donorEventList.Response_Date > DateTime.Parse("1/1/2000"))
                    throw new Exception(@"It appears you have already registered for this event, please check 
                                            the information and try again or call 334-956-8200 for assistance.");

                var eventList = new EventList(ConnectionString, User.Identity.Name, donorEventList.fk_Event);

                if (attendingRadio.SelectedValue.Equals("0"))
                {
                    donorEventList.Attending = true;
                    donorEventList.TicketsRequested = int.Parse(ddlNoGuests.SelectedValue);
                }
                else
                {
                    donorEventList.Attending = false;
                    donorEventList.TicketsRequested = 0;
                }

                donorEventList.DonorComments = txtComments.Text;
                donorEventList.Response_Date = DateTime.Now;
                donorEventList.Response_Type = "RSVP Web";

                if (donorEventList.GetTicketCountForEvent() > eventList.Capacity)
                {
                    // Add to Waiting List
                    donorEventList.WaitingList_Date = DateTime.Now;
                    donorEventList.WaitingListOrder = donorEventList.GetNextWaitListNumber();
                }


                donorEventList.Update();
                Session["SPLC.Donor.RSVP.DEL"] = donorEventList;


                var donorEmail = new DonorEmail(ConnectionString, User.Identity.ToString(), ConfigurationManager.AppSettings["EmailTemplatesURL"], donorList, donorEventList);
                donorEmail.SendEmail();

                Response.Redirect("Confirmation.aspx");

            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
            }

        }

        private bool UpdateDonorList(DonorList donorList)
        {
            var blReturn = false;

            if (!donorList.AccountName.Equals(txtName.Text))
            {
                donorList.AccountName = txtName.Text;
                blReturn = true;
            }

            if (!donorList.AddressLine1.Equals(txtMailingAddress.Text))
            {
                donorList.AddressLine1 = txtMailingAddress.Text;
                blReturn = true;
            }

            if (!donorList.City.Equals(txtCity.Text))
            {
                donorList.City = txtCity.Text;
                blReturn = true;
            }

            if (!donorList.State.Equals(ddlState.SelectedValue))
            {
                donorList.State = ddlState.SelectedValue;
                blReturn = true;
            }

            if (!donorList.PostCode.Equals(txtZipCode.Text))
            {
                donorList.PostCode = txtZipCode.Text;
                blReturn = true;
            }

            if (!donorList.PhoneNumber.Equals(txtPhoneNumber.Text))
            {
                donorList.PhoneNumber = txtPhoneNumber.Text;
                blReturn = true;
            }

            if (!donorList.EmailAddress.Equals(txtEmail.Text))
            {
                donorList.EmailAddress = txtEmail.Text;
                blReturn = true;
            }

            if (blReturn)
                donorList.Update();

            return blReturn;
        }


        protected void attendingRadio_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ddlNoGuests.Enabled = !attendingRadio.SelectedValue.Equals("1");
        }
    }
}