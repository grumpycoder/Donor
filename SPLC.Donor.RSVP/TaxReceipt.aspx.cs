using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using SPLC.Donor.Models;
using System.Data;

namespace SPLC.Donor.RSVP
{
    public partial class TaxReceipt1 : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    TaxData taxD = new TaxData(_ConnStr);
                    DataTable dtDonor = new DataTable();
                    //dtDonor = taxD.Load(Request["id"].ToString(), int.Parse(Request["taxyear"].ToString()));

                    // Check if they have already accessed the file


                    rvTaxReceipt.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    rvTaxReceipt.LocalReport.ReportPath = Server.MapPath("Templates/TaxReport.rdlc");

                    GetDonorData(dtDonor);
                
                }
                catch(Exception EX)
                {}
                                
            }
        }

        private void GetDonorData(DataTable pDonorData)
        {
            try
            {
                ReportDataSource rdsMainRec = new ReportDataSource("TaxReceipt", pDonorData);
                rvTaxReceipt.LocalReport.DataSources.Clear();
                rvTaxReceipt.LocalReport.DataSources.Add(rdsMainRec);

                TaxYear TYear = new TaxYear(_ConnStr);
                DataTable dtTaxYear = new DataTable();
                dtTaxYear = TYear.GetTaxYear(int.Parse(Request["taxyear"].ToString()));

                ReportDataSource rdsTaxYear = new ReportDataSource("TaxYear", dtTaxYear);
                rvTaxReceipt.LocalReport.DataSources.Add(rdsTaxYear);


                // Create table with values
                DataTable dtData = new DataTable();
                dtData.Clear();
                dtData.Columns.Add("Donation_Date", typeof(DateTime));
                dtData.Columns.Add("Amount", typeof(float));

                if (pDonorData.Rows.Count > 0)
                {
                    DataRow drRow = pDonorData.Rows[0];

                    for (int i = 1; i <= 25; i++)
                    {
                        try
                        {

                            float fltTry = float.Parse(drRow["AMOUNT_" + i.ToString()].ToString());

                            DataRow dr = dtData.NewRow();
                            dr["Donation_Date"] = drRow["DONATION_DATE_" + i.ToString()];
                            dr["Amount"] = drRow["AMOUNT_" + i.ToString()];
                            dtData.Rows.Add(dr);

                        }
                        catch
                        { }
                       
                    }

                    ReportDataSource rdsListRec = new ReportDataSource("TaxDataList", dtData);
                    rvTaxReceipt.LocalReport.DataSources.Add(rdsListRec);
                }
                
            }
            catch(Exception EX)
            { }
        }
    }
}