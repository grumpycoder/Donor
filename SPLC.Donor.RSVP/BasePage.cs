using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;

namespace SPLC.Donor.RSVP
{
    public class BasePage : Page
    {
        private string ConnectionString { get; set; }

        protected BasePage()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();
        }

        protected override void OnPreInit(EventArgs e)
        {
            int eventId; 
            int.TryParse(Request["eid"], out eventId);

            MasterPageFile = eventId > 20 ? "~/RSVPNoBrand.Master" : "~/RSVP.Master";
        }
    }
}