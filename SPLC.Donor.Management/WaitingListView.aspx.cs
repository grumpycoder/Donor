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
    public partial class WaitingListView : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                EventList EL = new EventList(User.Identity.Name);

                ddlEvents.DataSource = EL.GetEvents();
                ddlEvents.DataTextField = "EName";
                ddlEvents.DataValueField = "pk_Event";
                ddlEvents.DataBind();

                ddlEvents.Items.Insert(0, new ListItem("Select Event", ""));
                ddlEvents.SelectedIndex = 0;

                DonorList DL = new DonorList();
                ddlDonorType.DataSource = DL.GetDonorTypes();
                ddlDonorType.DataTextField = "DonorType";
                ddlDonorType.DataValueField = "DonorType";
                ddlDonorType.DataBind();

                ddlDonorType.Items.Insert(0, new ListItem("Select Donor Type", ""));
                ddlDonorType.SelectedIndex = 0;

                DonorEventList DEL = new DonorEventList(User.Identity.Name);

                gvDonorEvents.DataSource = DEL.GetWaitingList_Search("", "", "","");
                gvDonorEvents.DataBind();

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DonorEventList DEL = new DonorEventList(User.Identity.Name);

            gvDonorEvents.DataSource = DEL.GetWaitingList_Search(ddlEvents.SelectedValue.ToString(), txtDonorID.Text.ToString(), txtName.Text.ToString(),ddlDonorType.SelectedValue.ToString());
            gvDonorEvents.DataBind();
        }
    }
}