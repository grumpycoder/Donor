using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SPLC.Donor.Models;

namespace SPLC.Donor.RSVP
{
    public partial class eventexpired : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["eid"] != null)
            {
                try
                {
                    if (!Request["eid"].ToString().Equals("NULL"))
                    {
                        EventList EL = new EventList(User.Identity.Name, int.Parse(Request["eid"].ToString()));
                        // Add image URL to the page
                        pnlContentBefore.Visible = true;
                    }
                }
                catch
                {
                    Response.Redirect("eventexpired.aspx?eid=NULL");
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx?eid=3");
        }
    }
}