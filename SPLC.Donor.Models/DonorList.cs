using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SPLC.Donor.Models
{
    public class DonorList
    {
        #region Private Variables
            private static string _ConnStr = "";
            private static string _User = "";
        #endregion

        #region Accessors
            
            public String pk_DonorList { get; set; }
            //public string AccountType { get; set; }
            public string KeyName { get; set; }
            //public string AccountID { get; set; }
            //public string InsideSal { get; set; }
            //public string OutSideSal { get; set; }
            //public string HHOutsideSal { get; set; }
            public string AccountName { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            //public string AddressLine3 { get; set; }
            //public string AddressLine4 { get; set; }
            //public string AddressLine5 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostCode { get; set; }
            //public string CountryIDTrans { get; set; }
            //public string StateDescription { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
            //public bool SPLCLeadCouncil { get; set; }
            //public int MembershipYear { get; set; }
            //public int YearsSince { get; set; }
            //public float HPC { get; set; }
            //public DateTime LastPaymentDate { get; set; }
            //public float LastPaymentAmount { get; set; }
            //public string SourceCode { get; set; }

            public bool IsValid { get; set; }
        
        #endregion
                    

        #region Constructors

        public DonorList()
        {
            IsValid = false;
        }

        public DonorList(string pConnString,string pUser)
        {

            IsValid = false;
            _ConnStr = pConnString;
            _User = pUser;
        }

        public DonorList(string pConnString, string pUser, string pID)
        {
            IsValid = false;
            _ConnStr = pConnString;
            _User = pUser;
            pk_DonorList = pID;
            Load();
        }

        #endregion

        #region Standard Methods

        private void Load()
        {
            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            string strSQL = "SELECT TOP 1 * FROM DonorList WHERE pk_DonorList=@pk_DonorList";
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            cmd.Parameters.AddWithValue("@pk_DonorList", pk_DonorList);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                KeyName = dr["KeyName"].ToString();
                AccountName = dr["AccountName"].ToString();
                AddressLine1 = dr["AddressLine1"].ToString();
                City = dr["City"].ToString();
                State = dr["State"].ToString();
                PostCode = dr["PostCode"].ToString();
                PhoneNumber = dr["PhoneNumber"].ToString();
                EmailAddress = dr["EmailAddress"].ToString();

                IsValid = true;
            } 

            cmd.Dispose();
            da.Dispose();
            Conn.Close();

        }

        public void Update()
        {

            SqlConnection Conn = new SqlConnection(_ConnStr);
            SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 1 * FROM DonorList WHERE pk_DonorList=@pk_DonorList", Conn);
            SqlParameter param = new SqlParameter("@pk_DonorList", SqlDbType.NVarChar,15);
            param.Value = pk_DonorList;
            da.SelectCommand.Parameters.Add(param);

            SqlCommandBuilder db = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataRow dr = ds.Tables[0].Rows[0];

            dr["KeyName"] = KeyName;
            dr["AccountName"] = AccountName;
            dr["AddressLine1"] = AddressLine1;
            dr["City"] = City;
            dr["State"] = State;
            dr["PostCode"] = PostCode;
            dr["PhoneNumber"] = PhoneNumber;
            dr["EmailAddress"] =EmailAddress;

            da.Update(ds.Tables[0]);
            da.Dispose();
            Conn.Close();
        }

        public void Create()
        {
            using (var cn = new SqlConnection(_ConnStr))
            {
                cn.Open();
                var cmd = new SqlCommand()
                {
                    CommandText = "INSERT INTO DonorList " +
                                  "(pk_DonorList, AddressLine1, AddressLine2, City, State, PostCode, PhoneNumber)" +
                                  "VALUES(@pk_DonorList, @AddressLine1, @AddressLine2, @City, @State, @PostCode, @PhoneNumber)",
                    Connection = cn,
                    CommandType = CommandType.Text
                };
                cmd.Parameters.AddWithValue("pk_DonorList", this.pk_DonorList);
//                cmd.Parameters.AddWithValue("KeyName", this.KeyName);
//                cmd.Parameters.AddWithValue("AccountType", this.KeyName);
                cmd.Parameters.AddWithValue("AddressLine1", this.AddressLine1 ?? "");
                cmd.Parameters.AddWithValue("AddressLine2", this.AddressLine2 ?? "");
                cmd.Parameters.AddWithValue("City", this.City ?? "");
                cmd.Parameters.AddWithValue("State", this.State ?? "");
                cmd.Parameters.AddWithValue("PostCode", this.PostCode ?? "");
                cmd.Parameters.AddWithValue("PhoneNumber", this.PhoneNumber ?? "");

                cmd.ExecuteNonQuery();
            }
        }


        #endregion



        public DataTable GetDonorTypes()
        {

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT DISTINCT DonorType FROM DonorList ORDER BY DonorType", Conn);

            DataSet ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            Conn.Close();

            return ds.Tables[0];
        }

        public DataTable GetDonorDemoUpdates(string pSort = "")
        {

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            string strSQL = @"SELECT *
                                FROM DonorEventList DE
                                LEFT JOIN DonorList DL
                                ON DE.fk_DonorList = DL.pk_DonorList
                                WHERE UpdatedInfo = 1 ";

            if (pSort.Length > 0)
                strSQL += "ORDER BY " + pSort;

            SqlDataAdapter da = new SqlDataAdapter(strSQL, Conn);

            DataSet ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            Conn.Close();

            return ds.Tables[0];
        }

        public void AddNewGuestToEvent(int pEventID)
        {
            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlCommand cmd = new SqlCommand("p_NewGuest", Conn);
            //cmd.Parameters.AddWithValue("@fk_Event", pEventID);

            string strR = cmd.ExecuteScalar().ToString();
            Conn.Close();

            if (strR.Length > 5)
            {
                pk_DonorList = strR;
                Load();
            }
        }

    }
}