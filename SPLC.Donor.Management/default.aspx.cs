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

namespace SPLC.Donor.Management
{
    public partial class _default : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = User.Identity.Name.ToString();

            HttpBrowserCapabilities browser = Request.Browser;
            //lblMessage.Text = browser.Type.ToString();

            if (!Page.IsPostBack)
            {
                EventList EL = new EventList(_ConnStr, User.Identity.Name);

                ddlEvents.DataSource = EL.GetEvents();
                ddlEvents.DataTextField = "EName";
                ddlEvents.DataValueField = "pk_Event";
                ddlEvents.DataBind();

                ddlEvent2.DataSource = EL.GetEvents();
                ddlEvent2.DataTextField = "EName";
                ddlEvent2.DataValueField = "pk_Event";
                ddlEvent2.DataBind();
            }

            if (browser.Type.ToString().Contains("Safari"))
            {
                pnlPC.Visible = false;
                pnlIPad.Visible = true;

                if (!Page.IsPostBack)
                {
                    //lblTest.Text = Page.User.Identity.Name.ToString();

                    DonorEventList DEL = new DonorEventList(_ConnStr, User.Identity.Name);

                    gvDonorList.DataSource = DEL.GetDonorList_Search(ddlEvent2.SelectedValue.ToString(),txtLName.Text.ToString(),100);
                    gvDonorList.DataBind();

                }
            }
            else
            {
                pnlPC.Visible = true;
                pnlIPad.Visible = false;

                //lblMessage.Text = "";
                lblMessage.ForeColor = System.Drawing.Color.Red;

                if (!Page.IsPostBack)
                {
                    //lblTest.Text = Page.User.Identity.Name.ToString();

                    DonorEventList DEL = new DonorEventList(_ConnStr, User.Identity.Name);

                    gvRegistrations.DataSource = DEL.GetRecentResponses(15);
                    gvRegistrations.DataBind();

                }
            }  


                        
        }

        protected void btnRegisterUser_Click(object sender, EventArgs e)
        {
            try
            {

                if (!Page.IsValid)
                {
                    lblMessage.Text = "NO";
                }
                else
                {



                    if (txtAttending.Text.ToString().Equals(""))
                        txtAttending.Text = "0";

                    // Validate Data Entry
                    if (!chkGuest.Checked)
                    {
                        if (txtDonorID.Text.Length.Equals(0))
                            throw new Exception("Please enter a valid Donor ID!");
                    }

                    if (chkAttending.Checked && int.Parse(txtAttending.Text.ToString()) < 1)
                        throw new Exception("Please enter the number of attendees!");

                    if (!chkAttending.Checked && int.Parse(txtAttending.Text.ToString()) > 0)
                        throw new Exception("If not attending please remove the number of attendees!");

                    EventList EL = new EventList(_ConnStr, User.Identity.Name, int.Parse(ddlEvents.SelectedValue.ToString()));
                    DonorEventList DonorEL = new DonorEventList(_ConnStr, User.Identity.Name);
                    DonorList DL;

                    if (chkGuest.Checked)
                    {
                        DL = new DonorList(_ConnStr, User.Identity.Name);
                        DL.AddNewGuestToEvent(EL.pk_Event);

                        DonorEL.fk_Event = EL.pk_Event;
                        DonorEL.fk_DonorList = DL.pk_DonorList;
                        DonorEL.AddNew();

                    }
                    else
                    {
                        DL = new DonorList(_ConnStr, User.Identity.Name, txtDonorID.Text.ToString().Trim());
                        DonorEL.Load(EL.pk_Event, txtDonorID.Text.ToString().Trim());

                        if (DonorEL.pk_DonorEventList <= 0)
                            throw new Exception("Donor ID is not registered with this Event!");

                        if (DonorEL.Response_Date > DateTime.Parse("1/1/2000"))
                            throw new Exception("Donor has already registered for this Event!");
                    }

                    DL = new DonorList(_ConnStr, User.Identity.Name, DonorEL.fk_DonorList);

                    // Update Donor Information
                    if (UpdateDonorList(DL))
                    {
                        DonorEL.UpdatedInfo = true;
                        DonorEL.UpdatedInfoDateTime = DateTime.Now;
                        DonorEL.UpdatedInfo_User = DL.AccountName;
                    }

                    // Register User
                    DonorEL.Response_Date = DateTime.Now;
                    DonorEL.Response_Type = "SPLC Admin";

                    if (chkAttending.Checked)
                    {
                        DonorEL.Attending = true;
                        lblMessage.Text = "Donor is registered";
                    }
                    else
                    {
                        DonorEL.Attending = false;
                        lblMessage.Text = "Donor is not attending.";
                    }

                    // Register User
                    if (DonorEL.GetTicketCountForEvent() > EL.Capacity && chkAttending.Checked)
                    {
                        // Add to Waiting List
                        DonorEL.Response_Date = DateTime.Now;
                        DonorEL.Response_Type = "SPLC Admin";

                        DonorEL.WaitingList_Date = DateTime.Now;
                        DonorEL.WaitingListOrder = DonorEL.GetNextWaitListNumber();
                        DonorEL.TicketsRequested = int.Parse(txtAttending.Text.ToString());
                        lblMessage.Text = "Donor was added to the Waiting List";
                    }
                    else
                    {

                        DonorEL.TicketsRequested = int.Parse(txtAttending.Text.ToString());
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }

                    DonorEL.Update();
                    txtDonorID.Text = "";
                    txtAttending.Text = "0";
                    chkAttending.Checked = false;

                    if (!DL.EmailAddress.Equals(""))
                    {
                        DonorEmail DMail = new DonorEmail(_ConnStr, User.Identity.ToString(), ConfigurationManager.AppSettings["EmailTemplatesURL"].ToString(), DL, DonorEL);
                        DMail.SendEmail();
                    }
                }

            }
            catch(Exception EX)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = EX.Message; // "Donor ID is not registered with this Event!";
            }

        }


        private void SendEmail(DonorList pDL, DonorEventList pDEL)
        {
            try
            {
                var sbMsg = new StringBuilder();


                if (pDEL.WaitingList_Date > DateTime.Parse("1/1/2000"))
                {
                    switch (pDEL.Attending)
                    {
                        case true:
                            sbMsg.Append(System.IO.File.ReadAllText(Server.MapPath("Templates/wait_yes.html")));
                            break;
                        case false:
                            sbMsg.Append(System.IO.File.ReadAllText(Server.MapPath("Templates/rsvp_no.html")));
                            break;
                    }
                }
                else
                {
                    switch (pDEL.Attending)
                    {
                        case true:
                            sbMsg.Append(System.IO.File.ReadAllText(Server.MapPath("Templates/rsvp_yes.html")));
                            break;
                        case false:
                            sbMsg.Append(System.IO.File.ReadAllText(Server.MapPath("Templates/rsvp_no.html")));
                            break;
                    }
                }

                sbMsg = sbMsg.Replace("@{DATE}", DateTime.Today.ToString("MMMM dd, yyyy"));
                sbMsg = sbMsg.Replace("@{SALUTATION}", pDL.AccountName);

                EventList EL = new EventList(_ConnStr, User.Identity.Name, pDEL.fk_Event);

                sbMsg = sbMsg.Replace("@{DisplayName}", EL.DisplayName);
                sbMsg = sbMsg.Replace("@{City}", EL.VenueCity);
                sbMsg = sbMsg.Replace("@{StartDate}", EL.StartDate.ToLongDateString());
                sbMsg = sbMsg.Replace("@{StartTime}", EL.StartDate.ToShortTimeString());

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.EnableSsl = false;

                MailMessage mail = new MailMessage();
                mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                mail.IsBodyHtml = true;
                mail.Subject = "SPLC Event Confirmation";

                mail.Body = sbMsg.ToString();
                mail.To.Add(new MailAddress(pDL.EmailAddress.ToString()));
                smtpClient.Send(mail);

            }
            catch (Exception EX)
            { }
        }


        private bool UpdateDonorList(DonorList pDL)
        {
            bool blReturn = false;

            if (!pDL.AccountName.Equals(txtName.Text.ToString()))
            {
                pDL.AccountName = txtName.Text.ToString();
                blReturn = true;
            }

            if (!pDL.AddressLine1.Equals(txtAddress.Text.ToString()))
            {
                pDL.AddressLine1 = txtAddress.Text.ToString();
                blReturn = true;
            }

            if (!pDL.City.Equals(txtCity.Text.ToString()))
            {
                pDL.City = txtCity.Text.ToString();
                blReturn = true;
            }

            if (!pDL.State.Equals(ddlState.SelectedValue.ToString()))
            {
                pDL.State = ddlState.SelectedValue.ToString();
                blReturn = true;
            }

            if (!pDL.PostCode.Equals(txtZipCode.Text.ToString()))
            {
                pDL.PostCode = txtZipCode.Text.ToString();
                blReturn = true;
            }

            if (!pDL.PhoneNumber.Equals(txtPhone.Text.ToString()))
            {
                pDL.PhoneNumber = txtPhone.Text.ToString();
                blReturn = true;
            }

            if (!pDL.EmailAddress.Equals(txtEmail.Text.ToString()))
            {
                pDL.EmailAddress = txtEmail.Text.ToString();
                blReturn = true;
            }

            if (blReturn)
                pDL.Update();

            return blReturn;
        }

        protected void chkGuest_CheckedChanged(object sender, EventArgs e)
        {
            if(chkGuest.Checked)
            {
                txtDonorID.Text = "";
                txtDonorID.Enabled = false;

                txtName.Text = "";
                txtAddress.Text = "";
                txtCity.Text = "";
                ddlState.SelectedIndex = 0;
                txtZipCode.Text = "";
                txtPhone.Text = "";
                txtEmail.Text = "";
                chkAttending.Checked = false;
                txtAttending.Text = "0";

                pnlDemo.Enabled = true;
                btnRegisterUser.Visible = true;
            }
            else
            {
                txtDonorID.Enabled = true;
                pnlDemo.Enabled = false;
                btnRegisterUser.Visible = false;
            }
        }

        protected void txtDonorID_TextChanged(object sender, EventArgs e)
        {
            if(txtDonorID.Text.Length > 0)
            {
                try
                {
                    DonorEventList DEL = new DonorEventList(_ConnStr, User.Identity.Name);
                    DEL.GetDonorEventListID(txtDonorID.Text.ToString(), int.Parse(ddlEvents.SelectedValue.ToString()), true);

                    if (DEL.IsValid)
                    {
                        DonorList DL = new DonorList(_ConnStr, User.Identity.Name, DEL.fk_DonorList);
                        if (DL.IsValid)
                        {
                            txtName.Text = DL.AccountName;
                            txtAddress.Text = DL.AddressLine1;
                            txtCity.Text = DL.City;
                            ddlState.SelectedValue = DL.State;
                            txtZipCode.Text = DL.PostCode;
                            txtPhone.Text = DL.PhoneNumber;
                            txtEmail.Text = DL.EmailAddress;

                            pnlDemo.Enabled = true;
                            btnRegisterUser.Visible = true;
                        }
                        else
                            throw new Exception("Donor ID is not valid.");
                    }
                    else
                        throw new Exception("Donor ID does not exist for this event.");
                }
                catch(Exception EX)
                {
                    lblMessage.Text = EX.Message;
                }
                
            }
        }

        protected void btnSearchDonor_Click(object sender, EventArgs e)
        {
            DonorEventList DEL = new DonorEventList(_ConnStr, User.Identity.Name);

            gvDonorList.DataSource = DEL.GetDonorList_Search(ddlEvent2.SelectedValue.ToString(), txtLName.Text.ToString(),0);
            gvDonorList.DataBind();
        }


    }
}