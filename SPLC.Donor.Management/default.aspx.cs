using System;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using SPLC.Donor.Models;
using System.Linq;
using System.Web.UI.WebControls;

namespace SPLC.Donor.Management
{
    public partial class _default : Page
    {
        private const string BaseDate = "1/1/2000";

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = User.Identity.Name;

            var browser = Request.Browser;

            if (!Page.IsPostBack)
            {
                var eventList = new EventList(User.Identity.Name);

                ddlEvents.DataSource = eventList.GetEvents();
                ddlEvents.DataTextField = "EName";
                ddlEvents.DataValueField = "pk_Event";
                ddlEvents.DataBind();

                ddlEvent2.DataSource = eventList.GetEvents();
                ddlEvent2.DataTextField = "EName";
                ddlEvent2.DataValueField = "pk_Event";
                ddlEvent2.DataBind();
            }

            if (browser.Type.Contains("Safari"))
            {
                pnlPC.Visible = false;
                pnlIPad.Visible = true;

                if (Page.IsPostBack) return;

                var donorEventList = new DonorEventList(User.Identity.Name);

                gvDonorList.DataSource = donorEventList.GetDonorList_Search(ddlEvent2.SelectedValue, txtLName.Text, 100);
                gvDonorList.DataBind();
            }
            else
            {
                pnlPC.Visible = true;
                pnlIPad.Visible = false;

                lblMessage.ForeColor = System.Drawing.Color.Red;

                if (Page.IsPostBack) return;

                var donorEventList = new DonorEventList(User.Identity.Name);

                gvRegistrations.DataSource = donorEventList.GetRecentResponses(15);
                gvRegistrations.DataBind();
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
                    if (txtAttending.Text.Equals(""))
                        txtAttending.Text = "0";

                    // Validate Data Entry
                    if (!chkGuest.Checked)
                    {
                        if (txtDonorID.Text.Length.Equals(0))
                            throw new Exception("Please enter a valid Donor ID!");
                    }

                    if (chkAttending.Checked && int.Parse(txtAttending.Text) < 1)
                        throw new Exception("Please enter the number of attendees!");

                    if (!chkAttending.Checked && int.Parse(txtAttending.Text) > 0)
                        throw new Exception("If not attending please remove the number of attendees!");

                    var eventList = new EventList(User.Identity.Name, int.Parse(ddlEvents.SelectedValue));
                    var donorEventList = new DonorEventList(User.Identity.Name);
                    DonorList donorList;

                    if (chkGuest.Checked)
                    {
                        donorList = new DonorList();
                        donorList.AddNewGuestToEvent(eventList.pk_Event);

                        donorEventList.fk_Event = eventList.pk_Event;
                        donorEventList.fk_DonorList = donorList.pk_DonorList;
                        donorEventList.AddNew();

                    }
                    else
                    {

                        var specialEventCodes = new[] { "jbond", "jsncc", "naacp", "splcj", "jbhrc", "bondj", "jhbms" };
                        var finderNumber = txtDonorID.Text.Trim();


                        if (specialEventCodes.Contains(finderNumber.ToLower()))
                        {
                            var guid = Guid.NewGuid();

                            var key = finderNumber + guid.ToString().Replace("-", "").Substring(0, 5).ToUpper();
                            var donor = new DonorList() { pk_DonorList = key.ToUpper(), IsValid = true, AccountType = "Guest", DonorType = "Guest" };
                            donor.Create();

                            var del = new DonorEventList("") { fk_Event = eventList.pk_Event, fk_DonorList = key };
                            del.Create();
                            finderNumber = key;
                        }

                        donorEventList.Load(eventList.pk_Event, finderNumber);

                        if (donorEventList.pk_DonorEventList <= 0)
                            throw new Exception("Donor ID is not registered with this Event!");

                        if (donorEventList.Response_Date > DateTime.Parse(BaseDate))
                            throw new Exception("Donor has already registered for this Event!");
                    }

                    donorList = new DonorList(donorEventList.fk_DonorList);

                    // Update Donor Information
                    if (UpdateDonorList(donorList))
                    {
                        donorEventList.UpdatedInfo = true;
                        donorEventList.UpdatedInfoDateTime = DateTime.Now;
                        donorEventList.UpdatedInfo_User = donorList.AccountName;
                    }

                    // Register User
                    donorEventList.Response_Date = DateTime.Now;
                    donorEventList.Response_Type = "SPLC Admin";

                    if (chkAttending.Checked)
                    {
                        donorEventList.Attending = true;
                        lblMessage.Text = "Donor is registered";
                    }
                    else
                    {
                        donorEventList.Attending = false;
                        lblMessage.Text = "Donor is not attending.";
                    }

                    // Register User
                    if (donorEventList.GetTicketCountForEvent() > eventList.Capacity && chkAttending.Checked)
                    {
                        // Add to Waiting List
                        donorEventList.Response_Date = DateTime.Now;
                        donorEventList.Response_Type = "SPLC Admin";

                        donorEventList.WaitingList_Date = DateTime.Now;
                        donorEventList.WaitingListOrder = donorEventList.GetNextWaitListNumber();
                        donorEventList.TicketsRequested = int.Parse(txtAttending.Text);
                        lblMessage.Text = "Donor was added to the Waiting List";
                    }
                    else
                    {

                        donorEventList.TicketsRequested = int.Parse(txtAttending.Text);
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }

                    //                    DonorEL.Update();
                    donorEventList.SaveChanges();
                    txtDonorID.Text = "";
                    txtAttending.Text = "0";
                    chkAttending.Checked = false;

                    if (donorList.EmailAddress.Equals("")) return;

                    var donorEmail = new DonorEmail(User.Identity.ToString(), ConfigurationManager.AppSettings["EmailTemplatesURL"], donorList, donorEventList);
                    donorEmail.SendEmail();
                    ClearControl(Form);
                }

            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = ex.Message; // "Donor ID is not registered with this Event!";
            }

        }

        private bool UpdateDonorList(DonorList donorList)
        {
            var result = false;

            if (!donorList.AccountName.Equals(txtName.Text))
            {
                donorList.AccountName = txtName.Text;
                result = true;
            }

            if (!donorList.AddressLine1.Equals(txtAddress.Text))
            {
                donorList.AddressLine1 = txtAddress.Text;
                result = true;
            }

            if (!donorList.AddressLine2.Equals(txtAddress2.Text))
            {
                donorList.AddressLine2 = txtAddress2.Text;
                result = true;
            }

            if (!donorList.AddressLine3.Equals(txtAddress3.Text))
            {
                donorList.AddressLine3 = txtAddress3.Text;
                result = true;
            }


            if (!donorList.City.Equals(txtCity.Text))
            {
                donorList.City = txtCity.Text;
                result = true;
            }

            if (!donorList.State.Equals(ddlState.SelectedValue))
            {
                donorList.State = ddlState.SelectedValue;
                result = true;
            }

            if (!donorList.PostCode.Equals(txtZipCode.Text))
            {
                donorList.PostCode = txtZipCode.Text;
                result = true;
            }

            if (!donorList.PhoneNumber.Equals(txtPhone.Text))
            {
                donorList.PhoneNumber = txtPhone.Text;
                result = true;
            }

            if (!donorList.EmailAddress.Equals(txtEmail.Text))
            {
                donorList.EmailAddress = txtEmail.Text;
                result = true;
            }

            if (result)
                donorList.Save();
            //                donorList.Update();

            return result;
        }

        protected void chkGuest_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGuest.Checked)
            {
                txtDonorID.Text = "";
                txtDonorID.Enabled = false;

                txtName.Text = "";
                txtAddress.Text = "";
                txtAddress2.Text = "";
                txtAddress3.Text = "";
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
            if (txtDonorID.Text.Length < 5) return;

            var specialEventCodes = new[] { "jbond", "jsncc", "naacp", "splcj", "jbhrc", "bondj", "jhbms" };
            var finderNumber = txtDonorID.Text.Trim();

            if (finderNumber.Length == 5)
            {
                if (!specialEventCodes.Contains(finderNumber.ToLower())) return;

                //                lblMessage.Text = $"Found promo {finderNumber}";
                pnlDemo.Enabled = true;
                btnRegisterUser.Visible = true;

                return;
            }



            try
            {
                var donorEventList = new DonorEventList(User.Identity.Name);
                donorEventList.GetDonorEventListID(txtDonorID.Text, int.Parse(ddlEvents.SelectedValue), true);

                if (donorEventList.IsValid)
                {
                    var donorList = new DonorList(donorEventList.fk_DonorList);
                    if (donorList.IsValid)
                    {
                        txtName.Text = donorList.AccountName;
                        txtAddress.Text = donorList.AddressLine1;
                        txtAddress2.Text = donorList.AddressLine2;
                        txtAddress3.Text = donorList.AddressLine3;
                        txtCity.Text = donorList.City;
                        ddlState.SelectedValue = donorList.State;
                        txtZipCode.Text = donorList.PostCode;
                        txtPhone.Text = donorList.PhoneNumber;
                        txtEmail.Text = donorList.EmailAddress;

                        pnlDemo.Enabled = true;
                        btnRegisterUser.Visible = true;
                    }
                    else
                        throw new Exception("Donor ID is not valid.");
                }
                else
                    throw new Exception("Donor ID does not exist for this event.");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnSearchDonor_Click(object sender, EventArgs e)
        {
            var donorEventList = new DonorEventList(User.Identity.Name);

            gvDonorList.DataSource = donorEventList.GetDonorList_Search(ddlEvent2.SelectedValue, txtLName.Text, 0);
            gvDonorList.DataBind();
        }

        private void ClearControl(Control control)
        {
            var textbox = control as TextBox;
            if (textbox != null)
                textbox.Text = string.Empty;

            var dropDownList = control as DropDownList;
            if (dropDownList != null && dropDownList.ID.Contains("ddlState"))
                dropDownList.SelectedIndex = 0;

            foreach (Control childControl in control.Controls)
            {
                ClearControl(childControl);
            }
        }
    }
}