using System;
using System.Configuration;
using System.Linq;
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
            var specialEventId = new[] { "2041", "2042"};

            MasterPageFile = "~/RSVP.Master";

            if(specialEventId.Contains(eventId)) MasterPageFile = "~/RSVPNoBrand.Master";
        }
    }
}