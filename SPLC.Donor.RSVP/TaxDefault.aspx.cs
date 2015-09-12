using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using SPLC.Donor.Models;

namespace SPLC.Donor.RSVP
{
    public partial class TaxDefault : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();
        int intTaxYear = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            intTaxYear = DateTime.Now.Year - 1;

            TaxYear TaxY = new TaxYear(_ConnStr, intTaxYear);
            lblYear.Text = TaxY._TaxYear.ToString();
            lblYear2.Text = TaxY._TaxYear.ToString();
            lblYear3.Text = TaxY._TaxYear.ToString();

            DateTime dtStart = DateTime.Parse(TaxY.Start_DateTime.ToString());
            lblStartDate.Text = TaxY.Start_DateTime.ToShortDateString();
            DateTime dtEnd = DateTime.Parse(TaxY.End_DateTime.ToString());
            lblEndDate.Text = TaxY.End_DateTime.ToShortDateString();
            lblDateGood.Text = dtEnd.ToString("MMMM") + " " + dtEnd.Day.ToString() + ", " + dtEnd.Year.ToString();

            lblPhone.Text = TaxY.SupportPhone.ToString();
            lblPhone2.Text = TaxY.SupportPhone.ToString();
            hlEmail.NavigateUrl = "mailto:" + TaxY.SupportEmail.ToString();
            hlEmail.Text = TaxY.SupportEmail.ToString();
            hlEmail2.NavigateUrl = "mailto:" + TaxY.SupportEmail.ToString();
            
            if(dtStart < DateTime.Now && dtEnd > DateTime.Now)
            {
                pnlReceipt.Visible = true;
                pnOther.Visible = false;
            }
            else
            {
                pnlReceipt.Visible = false;
                pnOther.Visible = true;
            }
            
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (SupporterIdTextBox.Text == "")
                    throw new Exception("Please enter a Supporter ID number.");

                if (!PrivacyPolicyCheckBox.Checked)
                    throw new Exception("Please check the 'I have read the Privacy Policy'");

                //if (Request["taxyear"] == null)
                //    throw new Exception("There is an error with the tax year, please contact SPLC for support.");

                //int intYear = int.Parse(Request["taxyear"].ToString());

                TaxData taxD = new TaxData(_ConnStr);
                taxD.TaxYear = intTaxYear;
                taxD.TAX_RECEIPT_ID = SupporterIdTextBox.Text.ToString();
                taxD.Load();

                string strDT = taxD.Downloaded_DateTIme.ToString();


                if (!taxD.IsValid)
                    throw new Exception("The Supporter ID you entered is not valid.");
                else
                {
                        taxD.Downloaded_DateTIme = DateTime.Now;
                        taxD.Update();

                        Response.Redirect("TaxForm.aspx?taxyear=" + intTaxYear.ToString() + "&id=" + SupporterIdTextBox.Text.ToString());
                }
            }
            catch(Exception EX)
            {
                lblMessage.Text = EX.Message;
            }
        }

    }
}