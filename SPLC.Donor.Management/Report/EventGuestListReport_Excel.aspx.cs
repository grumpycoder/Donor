using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;
using System.IO;
using System.Configuration;
using SPLC.Donor.Models;

namespace SPLC.Donor.Management.Report
{
    public partial class EventGuestListReport_Excel : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=EventGuestList-Excel_" + DateTime.Now.ToString() + ".xls";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";

            if (Request["eid"] != null)
            {
                DonorEventList DEL = new DonorEventList(User.Identity.Name);
                EventList EL = new EventList(User.Identity.Name, int.Parse(Request["eid"].ToString()));

                gvExcel.DataSource = DEL.GetDonorEventList_ByEvent(EL.pk_Event);
                gvExcel.DataBind();

            }
        }
    }
}