using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using SPLC.Donor.Models;

namespace SPLC.Donor.RSVP
{
    public partial class _default : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // If the parameter EID is null or the expired date has passed then redirect to the Event Expiration page
                if (Request["eid"] != null)
                {
                    EventList EL = new EventList(_ConnStr, User.Identity.Name, int.Parse(Request["eid"].ToString()));

                    if (EL.OnlineCloseDate < DateTime.Parse("1/1/2000"))
                        throw new Exception("No Close Date");

                    if (EL.OnlineCloseDate < DateTime.Now)
                        throw new Exception("Expired");

                    if (!EL.Active)
                        throw new Exception("Not Active");

                    // Write Page
                    DonorEventList DEL = new DonorEventList(_ConnStr, User.Identity.Name);
                    DEL.fk_Event = EL.pk_Event;

                    if (DEL.GetTicketCountForEvent() > EL.Capacity)
                        pnlCapacity.Visible = true;

                    // Add HTML from DB
                    StringBuilder sbHeader = new StringBuilder(EL.HTML_Header);
                    DonorEmail dEMail = new DonorEmail();

                    sbHeader = dEMail.ParseTextSubEL(sbHeader, EL);
                    ltHeader.Text = sbHeader.ToString();  // EL.HTML_Header;

                    StringBuilder sbFAQ = new StringBuilder(EL.HTML_FAQ);
                    sbFAQ = dEMail.ParseTextSubEL(sbFAQ, EL);
                    ltFAQ.Text = sbFAQ.ToString();

                    //ltFAQ.Text = EL.HTML_FAQ;

                    lblEvent.Text = EL.DisplayName;

                    imgHeader.ImageUrl = "ihandler.ashx?eid=" + EL.pk_Event.ToString();
                }
                else
                    throw new Exception("Invalid EID");
                //    try
                //    {
                        

                //        // If the Online Close Date is > 1/1/2000 (This means it is not null in DB)
                //        if (EL.OnlineCloseDate > DateTime.Parse("1/1/2000"))
                //        {
                //            // If the Online Close Date < NOW then redirect to evetn expired page
                //            if (EL.OnlineCloseDate < DateTime.Now)
                //            {
                //                Response.Redirect("eventexpired.aspx?eid=NULL");
                //            }
                //            else
                //            {
                //                // Write Page
                //                DonorEventList DEL = new DonorEventList(_ConnStr, User.Identity.Name);
                //                DEL.fk_Event = EL.pk_Event;

                //                if (DEL.GetTicketCountForEvent() > EL.Capacity)
                //                    pnlCapacity.Visible = true;

                //                // Add HTML from DB
                //                ltHeader.Text = EL.HTML_Header;
                //                ltFAQ.Text = EL.HTML_FAQ;

                //                lblEvent.Text = EL.DisplayName;

                //                imgHeader.ImageUrl = "ihandler.ashx?eid=" + EL.pk_Event.ToString();

                //            }
                //        }
                //    }
                //    catch
                //    {
                //        pnlRSVP.Visible = false;
                //        pnlRSVP2.Visible = false;
                //        pnlUnav.Visible = true;
                //        //Response.Redirect("eventexpired.aspx?eid=NULL");
                //    }
                //}
                //else
                //{
                //    //Response.Redirect("eventexpired.aspx?eid=NULL");
                //}
            }
            catch
            {
                Response.Redirect("eventexpired.aspx?eid=NULL");
            }

        }

        /// <summary>
        /// On Submit button click, validate donor then redirect to rsvp page
        /// if valid.  If donor is not valid, display message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFinderNumber.Text.Equals(""))
                    throw new Exception("There appears to be a problem with the information that you have entered, please check the information and try again or call 334-956-8200 for assistance.");

                DonorList DL = new DonorList(_ConnStr, "", txtFinderNumber.Text.ToString().Trim());

                if (!DL.IsValid)
                    throw new Exception("There appears to be a problem with the information that you have entered, please check the information and try again or call 334-956-8200 for assistance.");

                Session["SPLC.Donor.RSVP.DL"] = DL;

                DonorEventList DEL = new DonorEventList(_ConnStr, "");
                DEL.GetDonorEventListID(DL.pk_DonorList, int.Parse(Request["eid"].ToString()),true);

                if (!DEL.IsValid)
                    throw new Exception("There appears to be a problem with the information that you have entered, please check the information and try again or call 334-956-8200 for assistance.");

                if (DEL.Response_Date > DateTime.Parse("1/1/2000"))
                    throw new Exception("The code you have entered has already been used. If you need to change your reservation please call Courtney at 334-956-8269.");

                Session["SPLC.Donor.RSVP.DEL"] = DEL;

                Response.Redirect("DonorEvent.aspx");

            }
            catch(Exception EX)
            {
                ReservationCodeCustomValidator.ErrorMessage = EX.Message;
                ReservationCodeCustomValidator.IsValid = false;
            }

        }
    }
}