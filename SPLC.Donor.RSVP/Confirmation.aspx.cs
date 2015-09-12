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
    public partial class Confirmation : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            DonorEventList DEL = (DonorEventList)Session["SPLC.Donor.RSVP.DEL"];
            DonorList DL = (DonorList)Session["SPLC.Donor.RSVP.DL"];
            EventList EL = new EventList(User.Identity.Name, DEL.fk_Event);

            // Add HTML from DB
            StringBuilder sbHTML = new StringBuilder(EL.HTML_Header);
            DonorEmail dEMail = new DonorEmail();
            sbHTML = dEMail.ParseText(sbHTML, EL,DL);
            ltHeader.Text = sbHTML.ToString();

            StringBuilder sbFAQ = new StringBuilder(EL.HTML_FAQ);
            sbFAQ = dEMail.ParseText(sbFAQ, EL, DL);
            ltFAQ.Text = sbFAQ.ToString();

            lblEvent.Text = EL.DisplayName;
            imgHeader.ImageUrl = "ihandler.ashx?eid=" + EL.pk_Event.ToString();

            string strInfo = "<i>" + EL.DisplayName + "</i>, in " + EL.VenueCity + " at " + EL.StartDate.ToShortTimeString() + ".";

            StringBuilder sbDis = new StringBuilder();
            

            if (DEL.WaitingList_Date > DateTime.Parse("1/1/2000"))
            {
                switch (DEL.Attending)
                {
                    case false:
                        sbDis.Append(EL.HTML_No);
                        break;
                    case true:
                        sbDis.Append(EL.HTML_Wait);
                        break;
                }
            }
            else
            {
                switch (DEL.Attending)
                {
                    case false:
                        sbDis.Append(EL.HTML_No);
                        ltFAQ.Visible = false;
                        break;
                    case true:
                        sbDis.Append(EL.HTML_Yes);
                        break;
                }
            }

            DonorEmail dEM = new DonorEmail();
            sbDis = dEM.ParseText(sbDis,EL,DL);
            
            litConfirm.Text = sbDis.ToString();



        }
    }
}