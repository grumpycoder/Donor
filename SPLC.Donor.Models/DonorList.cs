using System;
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
        public string KeyName { get; set; }
        public string AccountName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsValid { get; set; }

        //public string AccountType { get; set; }
        //public string AccountID { get; set; }
        //public string InsideSal { get; set; }
        //public string OutSideSal { get; set; }
        //public string HHOutsideSal { get; set; }
        //public string AddressLine3 { get; set; }
        //public string AddressLine4 { get; set; }
        //public string AddressLine5 { get; set; }
        //public string CountryIDTrans { get; set; }
        //public string StateDescription { get; set; }
        //public bool SPLCLeadCouncil { get; set; }
        //public int MembershipYear { get; set; }
        //public int YearsSince { get; set; }
        //public float HPC { get; set; }
        //public DateTime LastPaymentDate { get; set; }
        //public float LastPaymentAmount { get; set; }
        //public string SourceCode { get; set; }

        #endregion


        #region Constructors

        public DonorList()
        {
            IsValid = false;
        }

        public DonorList(string pConnString, string pUser)
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
            var conn = new SqlConnection(_ConnStr);
            conn.Open();
            const string sql = "SELECT TOP 1 * FROM DonorList WHERE pk_DonorList=@pk_DonorList";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pk_DonorList", pk_DonorList);
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
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
            conn.Close();

        }

        public void Update()
        {
            var conn = new SqlConnection(_ConnStr);
            var da = new SqlDataAdapter("SELECT TOP 1 * FROM DonorList WHERE pk_DonorList=@pk_DonorList", conn);
            var param = new SqlParameter("@pk_DonorList", SqlDbType.NVarChar, 15) {Value = pk_DonorList};
            da.SelectCommand.Parameters.Add(param);
            
            var ds = new DataSet();
            da.Fill(ds);

            var dr = ds.Tables[0].Rows[0];
            dr["KeyName"] = KeyName;
            dr["AccountName"] = AccountName;
            dr["AddressLine1"] = AddressLine1;
            dr["City"] = City;
            dr["State"] = State;
            dr["PostCode"] = PostCode;
            dr["PhoneNumber"] = PhoneNumber;
            dr["EmailAddress"] = EmailAddress;

            da.Update(ds.Tables[0]);
            da.Dispose();
            conn.Close();
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

        public void Save()
        {
            using (var cn = new SqlConnection(_ConnStr))
            {
                cn.Open();
                var cmd = new SqlCommand()
                {
                    CommandText = "UPDATE DonorList SET " +
                                  "AccountType = @AccountType, KeyName = @KeyName, " + 
                                  "InsideSal = @InsideSal, OutsideSal = @OutsideSal, " +
                                  "AccountName = @AccountName, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, City = @City, " +
                                  "State = @State, PostCode = @PostCode, EmailAddress = @EmailAddress, PhoneNumber = @PhoneNumber " +
                                  "WHERE pk_DonorList = @pk_DonorList",
                    Connection = cn,
                    CommandType = CommandType.Text
                };
                cmd.Parameters.AddWithValue("pk_DonorList", pk_DonorList);

                cmd.Parameters.AddWithValue("AccountType", "Individual");
                cmd.Parameters.AddWithValue("KeyName", KeyName);
                cmd.Parameters.AddWithValue("InsideSal", AccountName);
                cmd.Parameters.AddWithValue("OutsideSal", AccountName);
                cmd.Parameters.AddWithValue("AccountName", AccountName);
                cmd.Parameters.AddWithValue("AddressLine1", AddressLine1);
                cmd.Parameters.AddWithValue("AddressLine2", AddressLine2);
                cmd.Parameters.AddWithValue("City", City);
                cmd.Parameters.AddWithValue("State", State);
                cmd.Parameters.AddWithValue("PostCode", PostCode);
                cmd.Parameters.AddWithValue("EmailAddress", EmailAddress);
                cmd.Parameters.AddWithValue("PhoneNumber", PhoneNumber);

                if (AddressLine2 == null) cmd.Parameters["AddressLine2"].Value = DBNull.Value; 

                cmd.ExecuteNonQuery();
            }
        }

        #endregion



        public DataTable GetDonorTypes()
        {

            var conn = new SqlConnection(_ConnStr);
            conn.Open();
            var da = new SqlDataAdapter(@"SELECT DISTINCT DonorType FROM DonorList ORDER BY DonorType", conn);

            var ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            conn.Close();

            return ds.Tables[0];
        }

        public DataTable GetDonorDemoUpdates(string pSort = "")
        {

            var conn = new SqlConnection(_ConnStr);
            conn.Open();
            var sql = @"SELECT *
                                FROM DonorEventList DE
                                LEFT JOIN DonorList DL
                                ON DE.fk_DonorList = DL.pk_DonorList
                                WHERE UpdatedInfo = 1 ";

            if (pSort.Length > 0)
                sql += "ORDER BY " + pSort;

            var da = new SqlDataAdapter(sql, conn);

            var ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            conn.Close();

            return ds.Tables[0];
        }

        public void AddNewGuestToEvent(int eventId)
        {
            var conn = new SqlConnection(_ConnStr);
            conn.Open();
            var cmd = new SqlCommand("p_NewGuest", conn);
            //TODO: Why is this commented?
            //cmd.Parameters.AddWithValue("@fk_Event", eventId);

            var strR = cmd.ExecuteScalar().ToString();
            conn.Close();

            if (strR.Length <= 5) return;
            pk_DonorList = strR;
            Load();
        }

    }
}