using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using SPLC.Donor.Models;

namespace SPLC.Donor.RSVP
{
    public partial class TaxForm : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            TaxYear TYear = new TaxYear(_ConnStr,int.Parse(Request["taxyear"].ToString()));

            TaxData TaxD = new TaxData(_ConnStr);
            DataTable pDonorData = TaxD.LoadTable(Request["id"].ToString(), TYear._TaxYear);

            DataTable dtTaxYear = new DataTable();
            dtTaxYear = TYear.GetTaxYear(int.Parse(Request["taxyear"].ToString()));

            // Create a Document object
            var document = new Document(PageSize.A4, 60, 25, 50, 10);

            // Create a new PdfWrite object, writing the output to a MemoryStream
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            // Open the Document for writing
            document.Open();

            var HeaderFont = FontFactory.GetFont("Helvetica", 7, Font.NORMAL);
            var HeaderFontRed = FontFactory.GetFont("Helvetica", 7, Font.NORMAL, new BaseColor(138, 8, 8));
            var BodyFont = FontFactory.GetFont("Helvetica", 10, Font.NORMAL);
            var BodyFontBold = FontFactory.GetFont("Helvetica", 10, Font.BOLD);
            var BodyOblique = FontFactory.GetFont("Helvetica Oblique", 10, Font.ITALIC);

            var TableFontHr = FontFactory.GetFont("Helvetica", 10, Font.BOLD);
            var TableFont = FontFactory.GetFont("Helvetica", 10, Font.NORMAL);

            // You can add additional elements to the document. Let's add an image in the upper right corner
            var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/SPLC_LOGO.png"));
            logo.SetAbsolutePosition(45, 750);
            logo.ScalePercent(25.0f, 25.0f);
            document.Add(logo);

            int intIndent = 380;

            var Header = new Paragraph("Fighting Hate", HeaderFontRed);
            Header.IndentationLeft = intIndent;
            document.Add(Header);
            Header = new Paragraph("Teaching Tolerance", HeaderFontRed);
            Header.IndentationLeft = intIndent;
            document.Add(Header);
            Header = new Paragraph("Seeking Justice", HeaderFontRed);
            Header.IndentationLeft = intIndent;
            document.Add(Header);

            Header = new Paragraph("   ", HeaderFontRed);
            Header.IndentationLeft = intIndent;
            document.Add(Header);

            Header = new Paragraph("Southern Poverty Law Center", HeaderFont);
            Header.IndentationLeft = intIndent;
            document.Add(Header);
            Header = new Paragraph("400 Washington Avenue", HeaderFont);
            Header.IndentationLeft = intIndent;
            document.Add(Header);
            Header = new Paragraph("Montgomery, AL 36104", HeaderFont);
            Header.IndentationLeft = intIndent;
            document.Add(Header);
            Header = new Paragraph("334.956.8200", HeaderFont);
            Header.IndentationLeft = intIndent;
            document.Add(Header);
            Header = new Paragraph("splcenter.org", HeaderFont);
            Header.IndentationLeft = intIndent;
            document.Add(Header);

            string strCONST = pDonorData.Rows[0]["CONSTITUENT1"].ToString();
            if (pDonorData.Rows[0]["CONSTITUENT2"].ToString().Length > 0)
                strCONST += " and " + pDonorData.Rows[0]["CONSTITUENT2"].ToString();

            Paragraph pStart = new Paragraph(strCONST, BodyFont);
            pStart.SpacingBefore = 40;
            document.Add(pStart);

            document.Add(new Paragraph(pDonorData.Rows[0]["ADDRESSLINE1"].ToString(), BodyFont));

            if (pDonorData.Rows[0]["ADDRESSLINE2"].ToString().Length > 0)
                document.Add(new Paragraph(pDonorData.Rows[0]["ADDRESSLINE2"].ToString(), BodyFont));

            document.Add(new Paragraph(pDonorData.Rows[0]["CITY"].ToString() + ", " + pDonorData.Rows[0]["STATE"].ToString() + "  " + pDonorData.Rows[0]["ZIP"].ToString(), BodyFont));

            document.Add(new Paragraph("2014 Support — Southern Poverty Law Center", BodyFontBold));

            PdfPTable tbl = new PdfPTable(2);
            tbl.WidthPercentage = 61; 
            tbl.HorizontalAlignment = 0;
            tbl.SpacingBefore = 10;
            tbl.SpacingAfter = 20;

            PdfPCell clDateHr = new PdfPCell(new Phrase("Donation Date", TableFontHr));
            clDateHr.HorizontalAlignment = 1;
            tbl.AddCell(clDateHr);

            PdfPCell clAmountHr = new PdfPCell(new Phrase("Amount", TableFontHr));
            clAmountHr.HorizontalAlignment = 1;
            tbl.AddCell(clAmountHr);


            DataTable dtRows = GetDonorData(pDonorData);

            float flTotal = 0;

            foreach(DataRow dr in dtRows.Rows)
            {
                PdfPCell clDate = new PdfPCell(new Phrase(DateTime.Parse(dr["Donation_Date"].ToString()).ToShortDateString(), TableFont));
                clDate.HorizontalAlignment = 2;
                clDate.RightIndent = 5;
                tbl.AddCell(clDate);

                PdfPCell clAmount = new PdfPCell(new Phrase(float.Parse(dr["Amount"].ToString()).ToString("C2"), TableFont));
                clAmount.HorizontalAlignment = 2;
                clAmount.RightIndent = 45;
                tbl.AddCell(clAmount);

                flTotal += float.Parse(dr["Amount"].ToString());
            }

            PdfPCell clTotal = new PdfPCell(new Phrase("TOTAL", TableFontHr));
            clTotal.HorizontalAlignment = 2;
            clTotal.Border = 0;
            tbl.AddCell(clTotal);

            PdfPCell clAmountTl = new PdfPCell(new Phrase(flTotal.ToString("C2"), TableFontHr));
            clAmountTl.HorizontalAlignment = 2;
            clAmountTl.RightIndent = 45;
            tbl.AddCell(clAmountTl);


            document.Add(tbl);
            
            document.Add(new Paragraph("This will serve as your receipt for tax purposes.", BodyOblique));
            document.Add(new Paragraph("The Southern Poverty Law Center is a 501(c)(3) organization.", BodyOblique));
            document.Add(new Paragraph("Gifts to the Center are fully tax-deductible.", BodyOblique));
            document.Add(new Paragraph("No goods or services are ever sent in exchange for gifts.", BodyOblique));

            document.Add(new Paragraph("    ", BodyOblique));

            document.Add(new Paragraph("The information on this receipt is accurate as of " + TYear.Start_DateTime.ToShortDateString() + ".", BodyFont));

            document.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Receipt-{0}.pdf", "123456"));
            Response.BinaryWrite(output.ToArray());

        }

        private DataTable GetDonorData(DataTable pDonorData)
        {
            DataTable dtData = new DataTable();

            try
            {

                // Create table with values
                
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
                }

                
            }
            catch (Exception EX)
            { }

            return dtData;
        }
    }
}