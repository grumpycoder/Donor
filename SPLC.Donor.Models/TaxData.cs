using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SPLC.Donor.Models
{
    public class TaxData
    {
        #region Private Variables
            private static string _ConnStr = "";
        #endregion

            #region Accessors

            public Guid pk_TaxData { get; set; }
            public int TaxYear { get; set; }
            public DateTime Downloaded_DateTIme { get; set; }
            public string TAX_RECEIPT_ID { get; set; }
            public DateTime DONATION_DATE_1 { get; set; }
            public float AMOUNT_1 { get; set; }
            public string DonorName { get; set; }
            public string FinderNumber { get; set; }
            public float Amount { get; set; }
            public DateTime DonationDate { get; set; }
            public DateTime Receipt_DateTime { get; set; }

            public bool IsValid { get; set; }

            #endregion

        #region Constructors

        public TaxData()
        {
            IsValid = false;
        }

        public TaxData(string pConnString)
        {
            _ConnStr = pConnString;
            IsValid = false;
        }

        #endregion

        #region Standard Methods

        public void Load()
        {
            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            string strSQL = "SELECT TOP 1 * FROM TaxData WHERE TaxYear=@TaxYear AND TAX_RECEIPT_ID=@TAX_RECEIPT_ID";
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            cmd.Parameters.AddWithValue("@TaxYear", TaxYear);
            cmd.Parameters.AddWithValue("@TAX_RECEIPT_ID", TAX_RECEIPT_ID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                pk_TaxData = Guid.Parse(dr["pk_TaxData"].ToString());

                if (!dr["Downloaded_DateTIme"].ToString().Equals(""))
                    Downloaded_DateTIme = DateTime.Parse(dr["Downloaded_DateTIme"].ToString());

                IsValid = true;
            }
            else
                IsValid = false;

            cmd.Dispose();
            da.Dispose();
            Conn.Close();

        }

        public void Update()
        {

            SqlConnection Conn = new SqlConnection(_ConnStr);

            string strSQL = "SELECT TOP 1 * FROM TaxData WHERE TaxYear=@TaxYear AND TAX_RECEIPT_ID=@TAX_RECEIPT_ID";
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            cmd.Parameters.AddWithValue("@TaxYear", TaxYear);
            cmd.Parameters.AddWithValue("@TAX_RECEIPT_ID", TAX_RECEIPT_ID);

            SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 1 * FROM TaxData WHERE TaxYear=@TaxYear AND TAX_RECEIPT_ID=@TAX_RECEIPT_ID", Conn);
            SqlParameter param = new SqlParameter("@TaxYear", SqlDbType.Int);
            param.Value = TaxYear;
            da.SelectCommand.Parameters.Add(param);
            param = new SqlParameter("@TAX_RECEIPT_ID", SqlDbType.VarChar, 50);
            param.Value = TAX_RECEIPT_ID;
            da.SelectCommand.Parameters.Add(param);

            SqlCommandBuilder db = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataRow dr = ds.Tables[0].Rows[0];

            if (Downloaded_DateTIme > DateTime.Parse("1/1/2000"))
                dr["Downloaded_DateTIme"] = Downloaded_DateTIme;

            da.Update(ds.Tables[0]);
            da.Dispose();
            Conn.Close();
        }
        

        #endregion


        #region Custom Methods

        public DataTable LoadTable(string pTaxReceiptID, int pTaxYear)
        {
            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            string strSQL = "SELECT TOP 1 * FROM TaxData WHERE TaxYear=@TaxYear AND TAX_RECEIPT_ID=@TAX_RECEIPT_ID";
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            cmd.Parameters.AddWithValue("@TaxYear", pTaxYear);
            cmd.Parameters.AddWithValue("@TAX_RECEIPT_ID", pTaxReceiptID.Trim());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        #endregion
    }
}
