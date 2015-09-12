using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SPLC.Donor.Models
{
    public class TaxYear
    {
        #region Private Variables
            private static string _ConnStr = "";
        #endregion

            #region Accessors

            public Guid pk_TaxYear { get; set; }
            public int _TaxYear { get; set; }
            public DateTime Start_DateTime { get; set; }
            public DateTime End_DateTime { get; set; }
            public string SupportEmail { get; set; }
            public string SupportPhone { get; set; }

            #endregion

        #region Constructors

        public TaxYear()
        { }

        public TaxYear(string pConnString)
        {
            _ConnStr = pConnString;
        }

        public TaxYear(string pConnString, int pTaxYear)
        {
            _ConnStr = pConnString;
            _TaxYear = pTaxYear;
            Load();
        }

        #endregion

        #region Standard Methods

        public void Load()
        {

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            string strSQL = "SELECT TOP 1 * FROM TaxYear WHERE TaxYear=@TaxYear";
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            cmd.Parameters.AddWithValue("@TaxYear", _TaxYear);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];

                if (!dr["Start_DateTime"].ToString().Equals(""))
                    Start_DateTime = DateTime.Parse(dr["Start_DateTime"].ToString());

                if (!dr["End_DateTime"].ToString().Equals(""))
                    End_DateTime = DateTime.Parse(dr["End_DateTime"].ToString());

                SupportEmail = dr["SupportEmail"].ToString();
                SupportPhone = dr["SupportPhone"].ToString();


            }

            cmd.Dispose();
            da.Dispose();
            Conn.Close();
        }

        public void Update()
        {

            SqlConnection Conn = new SqlConnection(_ConnStr);
            SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 1 * FROM TaxYear WHERE TaxYear=@TaxYear", Conn);
            SqlParameter param = new SqlParameter("@TaxYear", SqlDbType.Int);
            param.Value = _TaxYear;
            da.SelectCommand.Parameters.Add(param);

            SqlCommandBuilder db = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataRow dr = ds.Tables[0].Rows[0];

            dr["Start_DateTime"] = Start_DateTime;
            dr["End_DateTime"] = End_DateTime;
            dr["SupportEmail"] = SupportEmail;
            dr["SupportPhone"] = SupportPhone;

            da.Update(ds.Tables[0]);
            da.Dispose();
            Conn.Close();
        }


        #endregion

        #region Custom Methods

        public DataTable GetTaxYear(int pTaxYear)
        {
            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            string strSQL = "SELECT TOP 1 * FROM TaxYear WHERE TaxYear=@TaxYear";
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            cmd.Parameters.AddWithValue("@TaxYear", pTaxYear);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataTable GetAllTaxYears()
        {
            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            string strSQL = "SELECT * FROM TaxYear ORDER BY TaxYear DESC";
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        #endregion
    }
}
