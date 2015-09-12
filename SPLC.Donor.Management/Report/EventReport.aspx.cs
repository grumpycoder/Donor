using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SPLC.Donor.Models;

namespace SPLC.Donor.Management.Report
{
    public partial class EventReport : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                EventList EL = new EventList(_ConnStr, User.Identity.Name);

                gvEventList.DataSource = EL.GetEventsReport();
                gvEventList.DataBind();

            }
        }
    }
}