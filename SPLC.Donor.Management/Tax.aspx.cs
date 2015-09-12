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
    public partial class Tax : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TaxYear TaxY = new TaxYear(_ConnStr);
                gvTaxYears.DataSource = TaxY.GetAllTaxYears();
                gvTaxYears.DataBind();
            }
        }
    }
}