using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using SPLC.Donor.Models;

namespace SPLC.Donor.Management
{
    public partial class TaxDetails : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TaxYear TaxY = new TaxYear(_ConnStr, int.Parse(Request["year"].ToString()));

                lblTaxYear.Text = TaxY._TaxYear.ToString();
                txtSDate.Text = TaxY.Start_DateTime.ToShortDateString();
                txtEDate.Text = TaxY.End_DateTime.ToShortDateString();
                txtEmail.Text = TaxY.SupportEmail.ToString();
                txtPhone.Text = TaxY.SupportPhone.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TaxYear TaxY = new TaxYear(_ConnStr, int.Parse(Request["year"].ToString()));
                TaxY.Start_DateTime = DateTime.Parse(txtSDate.Text.ToString());
                TaxY.End_DateTime = DateTime.Parse(txtEDate.Text.ToString());
                TaxY.SupportEmail = txtEmail.Text.ToString();
                TaxY.SupportPhone = txtPhone.Text.ToString();

                TaxY.Update();
            }
            catch(Exception EX)
            {
                lblMessage.Text = EX.Message;
            }
        }
    }
}