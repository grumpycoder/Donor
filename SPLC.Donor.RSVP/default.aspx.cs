﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SPLC.Donor.Models;

namespace SPLC.Donor.RSVP
{
    public partial class _default : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // If the parameter EID is null or the expired date has passed then redirect to the Event Expiration page
                if (Request["eid"] != null)
                {
                    var eventList = new EventList(ConnectionString, User.Identity.Name, int.Parse(Request["eid"]));

                    if (eventList.OnlineCloseDate < DateTime.Parse("1/1/2000"))
                        throw new Exception("No Close Date");

                    if (eventList.OnlineCloseDate < DateTime.Now)
                        throw new Exception("Expired");

                    if (!eventList.Active)
                        throw new Exception("Not Active");

                    // Write Page
                    var donorEventList = new DonorEventList(ConnectionString, User.Identity.Name)
                    {
                        fk_Event = eventList.pk_Event
                    };

                    if (donorEventList.GetTicketCountForEvent() > eventList.Capacity)
                        pnlCapacity.Visible = true;

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
                    throw new Exception("Invalid EID");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Response.Redirect("eventexpired.aspx?eid=NULL");
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var finderNumber = txtFinderNumber.Text;
            var specialEventCodes = new[] { "JBGEN15106", "SNCC151006", "NAACP15106", "SPLC151006", "HRCJB15106", "JBLC151006" };
            var pk_Event = int.Parse(Request["eid"]);

            if (specialEventCodes.Contains(finderNumber))
            {
                var guid = Guid.NewGuid();

                var key = finderNumber.Substring(0, 4) + guid.ToString().Replace("-", "").Substring(0, 6).ToUpper();
                var donor = new DonorList(ConnectionString, "") { pk_DonorList = key };
                donor.Create();

                var donorEventList = new DonorEventList(ConnectionString, "") { fk_Event = pk_Event, fk_DonorList = key };
                //                donorEventList.Create();
                donorEventList.AddNew();

                donorEventList.GetDonorEventListID(donor.pk_DonorList, pk_Event, true);
                Session["SPLC.Donor.RSVP.DL"] = donor;
                Session["SPLC.Donor.RSVP.DEL"] = donorEventList;

                Response.Redirect("DonorEvent.aspx");
            }

            try
            {
                if (txtFinderNumber.Text.Equals(""))
                    throw new Exception("There appears to be a problem with the information that you have entered, please check the information and try again or call 334-956-8200 for assistance.");

                var donorList = new DonorList(ConnectionString, "", txtFinderNumber.Text.Trim());

                if (!donorList.IsValid)
                    throw new Exception("There appears to be a problem with the information that you have entered, please check the information and try again or call 334-956-8200 for assistance.");

                Session["SPLC.Donor.RSVP.DL"] = donorList;

                var donorEventList = new DonorEventList(ConnectionString, "");
                donorEventList.GetDonorEventListID(donorList.pk_DonorList, int.Parse(Request["eid"]), true);

                if (!donorEventList.IsValid)
                    throw new Exception("There appears to be a problem with the information that you have entered, please check the information and try again or call 334-956-8200 for assistance.");

                if (donorEventList.Response_Date > DateTime.Parse("1/1/2000"))
                    throw new Exception("The code you have entered has already been used. If you need to change your reservation please call Courtney at 334-956-8269.");

                Session["SPLC.Donor.RSVP.DEL"] = donorEventList;

                Response.Redirect("DonorEvent.aspx");

            }
            catch (Exception ex)
            {
                ReservationCodeCustomValidator.ErrorMessage = ex.Message;
                ReservationCodeCustomValidator.IsValid = false;
            }

        }
    }
}