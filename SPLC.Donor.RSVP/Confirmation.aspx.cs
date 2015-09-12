using System;
using System.Text;
using SPLC.Donor.Models;

namespace SPLC.Donor.RSVP
{
    public partial class Confirmation : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var donorEventList = (DonorEventList)Session["SPLC.Donor.RSVP.DEL"];
            var donorList = (DonorList)Session["SPLC.Donor.RSVP.DL"];
            var eventList = new EventList(User.Identity.Name, donorEventList.fk_Event);

            // Add HTML from DB
            var html = new StringBuilder(eventList.HTML_Header);
            var donorEmail = new DonorEmail();
            html = donorEmail.ParseText(html, eventList,donorList);
            ltHeader.Text = html.ToString();

            var faq = new StringBuilder(eventList.HTML_FAQ);
            faq = donorEmail.ParseText(faq, eventList, donorList);
            ltFAQ.Text = faq.ToString();

            lblEvent.Text = eventList.DisplayName;
            imgHeader.ImageUrl = "ihandler.ashx?eid=" + eventList.pk_Event;

            var sbDis = new StringBuilder();
            

            if (donorEventList.WaitingList_Date > DateTime.Parse("1/1/2000"))
            {
                switch (donorEventList.Attending)
                {
                    case false:
                        sbDis.Append(eventList.HTML_No);
                        break;
                    case true:
                        sbDis.Append(eventList.HTML_Wait);
                        break;
                }
            }
            else
            {
                switch (donorEventList.Attending)
                {
                    case false:
                        sbDis.Append(eventList.HTML_No);
                        ltFAQ.Visible = false;
                        break;
                    case true:
                        sbDis.Append(eventList.HTML_Yes);
                        break;
                }
            }

            var email = new DonorEmail();
            sbDis = email.ParseText(sbDis,eventList,donorList);
            
            litConfirm.Text = sbDis.ToString();



        }
    }
}