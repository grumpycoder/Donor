using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using SPLC.Donor.Models;

namespace SPLC.Donor.RSVP
{
    public partial class DonorEvent : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";

                if (!Page.IsPostBack)
                {
                    DonorEventList DEL = (DonorEventList)Session["SPLC.Donor.RSVP.DEL"];
                    DonorList DL = (DonorList)Session["SPLC.Donor.RSVP.DL"];
                    EventList EL = new EventList(_ConnStr,User.Identity.Name,DEL.fk_Event);

                    if(DL.IsValid)
                    {
                        txtName.Text = DL.AccountName;
                        txtMailingAddress.Text = DL.AddressLine1;
                        txtAddress2.Text = DL.AddressLine2;
                        txtCity.Text = DL.City;
                        ddlState.SelectedValue = DL.State;
                        txtZipCode.Text = DL.PostCode;
                        txtEmail.Text = DL.EmailAddress;
                        txtPhoneNumber.Text = DL.PhoneNumber;

                        ddlNoGuests.Items.Clear();

                        // Add the allowed ticket number
                        for (int i = 0; i <= EL.TicketsAllowed; i++)
                        {
                            ddlNoGuests.Items.Add(new ListItem(i.ToString(), i.ToString()));
                        }

                        // Add HTML from DB

                        StringBuilder sbHeader = new StringBuilder(EL.HTML_Header);
                        DonorEmail dEMail = new DonorEmail();

                        sbHeader = dEMail.ParseTextSubEL(sbHeader, EL);
                        ltHeader.Text = sbHeader.ToString();  // EL.HTML_Header;

                        StringBuilder sbFAQ = new StringBuilder(EL.HTML_FAQ);
                        sbFAQ = dEMail.ParseTextSubEL(sbFAQ, EL);
                        ltFAQ.Text = sbFAQ.ToString();


                        //ltHeader.Text = EL.HTML_Header;
                        //ltFAQ.Text = EL.HTML_FAQ;

                        lblEvent.Text = EL.DisplayName;

                        imgHeader.ImageUrl = "ihandler.ashx?eid=" + EL.pk_Event.ToString();
                        
                    }
                    else
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = @"There appears to be a problem with the information that you have entered, please check 
                                            the information and try again or call 334-956-8200 for assistance.";
                    }
                }

            }
            catch(Exception EX)
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

                DonorEventList DEL = (DonorEventList)Session["SPLC.Donor.RSVP.DEL"];
                DonorList DL = (DonorList)Session["SPLC.Donor.RSVP.DL"];

                
                // Update Donor Information
                if(UpdateDonorList(DL))
                {
                    DEL.UpdatedInfo = true;
                    DEL.UpdatedInfoDateTime = DateTime.Now;
                    DEL.UpdatedInfo_User = DL.AccountName;
                }

                DL.EmailAddress = txtEmail.Text.ToString();
                //DL.Update();

                if (DEL.pk_DonorEventList <= 0)
                    throw new Exception("Donor ID is not registered with this Event!");

                if (DEL.Response_Date > DateTime.Parse("1/1/2000"))
                    throw new Exception(@"It appears you have already registered for this event, please check 
                                            the information and try again or call 334-956-8200 for assistance.");

                EventList EL = new EventList(_ConnStr, User.Identity.Name, DEL.fk_Event);

                if (attendingRadio.SelectedValue.Equals("0"))
                {
                    DEL.Attending = true;
                    DEL.TicketsRequested = int.Parse(ddlNoGuests.SelectedValue.ToString());
                }
                else
                {
                    DEL.Attending = false;
                    DEL.TicketsRequested = 0;
                }
                
                DEL.DonorComments = txtComments.Text.ToString();
                DEL.Response_Date = DateTime.Now;
                DEL.Response_Type = "RSVP Web";

                if (DEL.GetTicketCountForEvent() > EL.Capacity)
                {
                    // Add to Waiting List
                    DEL.WaitingList_Date = DateTime.Now;
                    DEL.WaitingListOrder = DEL.GetNextWaitListNumber();
                 }


                DEL.Update();
                Session["SPLC.Donor.RSVP.DEL"] = DEL;


                DonorEmail DMail = new DonorEmail(_ConnStr, User.Identity.ToString(), ConfigurationManager.AppSettings["EmailTemplatesURL"].ToString(), DL, DEL);
                DMail.SendEmail();

                Response.Redirect("Confirmation.aspx");

            }
            catch(Exception EX)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = EX.Message;
            }

        }

        private bool UpdateDonorList(DonorList pDL)
        {
            bool blReturn = false;

            if(!pDL.AccountName.Equals(txtName.Text.ToString()))
            {
                pDL.AccountName = txtName.Text.ToString();
                blReturn = true;
            }

            if(!pDL.AddressLine1.Equals(txtMailingAddress.Text.ToString()))
            {
                pDL.AddressLine1 = txtMailingAddress.Text.ToString();
                blReturn = true;
            }

            if(!pDL.City.Equals(txtCity.Text.ToString()))
            {
                pDL.City = txtCity.Text.ToString();
                blReturn = true;
            }

            if(!pDL.State.Equals(ddlState.SelectedValue.ToString()))
            {
                pDL.State = ddlState.SelectedValue.ToString();
                blReturn = true;
            }

            if (!pDL.PostCode.Equals(txtZipCode.Text.ToString()))
            {
                pDL.PostCode = txtZipCode.Text.ToString();
                blReturn = true;
            }

            if (!pDL.PhoneNumber.Equals(txtPhoneNumber.Text.ToString()))
            {
                pDL.PhoneNumber = txtPhoneNumber.Text.ToString();
                blReturn = true;
            }

            if (!pDL.EmailAddress.Equals(txtEmail.Text.ToString()))
            {
                pDL.EmailAddress = txtEmail.Text.ToString();
                blReturn = true;
            }

            if(blReturn)
                pDL.Update();

            return blReturn;
        }


        protected void attendingRadio_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (attendingRadio.SelectedValue.Equals("1"))
            {
                ddlNoGuests.Enabled = false;
            }
            else
                ddlNoGuests.Enabled = true;
            
        }

    }
}