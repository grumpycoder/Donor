using System;
using System.Configuration;
using System.Web.UI;

namespace SPLC.Donor.RSVP
{
    public class BasePage: Page
    {
        private string ConnectionString { get; set; }
        
        protected BasePage()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();
        }

        protected override void OnPreInit(EventArgs e)
        {
            var eventId = Request["eid"];
            MasterPageFile = eventId == "21" ? "~/RSVPNoBrand.Master" : "~/RSVP.Master";
        }
    }
}