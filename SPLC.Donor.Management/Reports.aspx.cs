using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SPLC.Donor.Models;

namespace SPLC.Donor.Management
{
    public partial class Reports : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EventList EL = new EventList(_ConnStr, User.Identity.Name);

                ddlEvents.DataSource = EL.GetEvents();
                ddlEvents.DataTextField = "EName";
                ddlEvents.DataValueField = "pk_Event";
                ddlEvents.DataBind();

            }
        }

        protected void btnEventReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Report/EventReport.aspx");
        }

        protected void btnEventGuestReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Report/EventGuestListReport.aspx?eid=" + ddlEvents.SelectedValue.ToString());
        }

        protected void btnMailTicketsReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Report/MailedTicketeReport.aspx?eid=" + ddlEvents.SelectedValue.ToString());
        }

        protected void btnDonorDemoChangeReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Report/DonorDemographicChangeReport.aspx");
        }
    }
}