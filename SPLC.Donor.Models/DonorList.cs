using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

namespace SPLC.Donor.Models
{
    public class DonorList
    {
        private string ConnectionString { get; set; }

        #region Private Variables

        #endregion

        #region Accessors

        public string pk_DonorList { get; set; }
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
        public string AccountType { get; set; }
        public string DonorType { get; set; }

        #endregion


        #region Constructors

        public DonorList()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();
            IsValid = false;
        }

        public DonorList(string donorListId) : this()
        {
            pk_DonorList = donorListId;
            Load();
        }

        #endregion

        #region Standard Methods

        private void Load()
        {
            var conn = new SqlConnection(ConnectionString);
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
            var conn = new SqlConnection(ConnectionString);
            var da = new SqlDataAdapter("SELECT TOP 1 * FROM DonorList WHERE pk_DonorList=@pk_DonorList", conn);
            var param = new SqlParameter("@pk_DonorList", SqlDbType.NVarChar, 15) { Value = pk_DonorList };
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
            using (var cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                var cmd = new SqlCommand()
                {
                    CommandText = "INSERT INTO DonorList " +
                                  "(pk_DonorList, KeyName, AccountType, AddressLine1, AddressLine2, City, State, PostCode, PhoneNumber, DonorType)" +
                                  "VALUES(@pk_DonorList, @KeyName, @AccountType, @AddressLine1, @AddressLine2, @City, @State, @PostCode, @PhoneNumber, @DonorType)",
                    Connection = cn,
                    CommandType = CommandType.Text
                };
                cmd.Parameters.AddWithValue("pk_DonorList", pk_DonorList);
                cmd.Parameters.AddWithValue("KeyName", KeyName ?? "");
                cmd.Parameters.AddWithValue("AccountType", AccountType ?? "Individual");
                cmd.Parameters.AddWithValue("DonorType", DonorType?? "Guest");
                cmd.Parameters.AddWithValue("AddressLine1", AddressLine1 ?? "");
                cmd.Parameters.AddWithValue("AddressLine2", AddressLine2 ?? "");
                cmd.Parameters.AddWithValue("City", City ?? "");
                cmd.Parameters.AddWithValue("State", State ?? "");
                cmd.Parameters.AddWithValue("PostCode", PostCode ?? "");
                cmd.Parameters.AddWithValue("PhoneNumber", PhoneNumber ?? "");

                cmd.ExecuteNonQuery();
            }
        }

        public void Save()
        {
            using (var cn = new SqlConnection(ConnectionString))
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

                cmd.Parameters.AddWithValue("AccountType", AccountType ?? "Individual");
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

            var conn = new SqlConnection(ConnectionString);
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

            var conn = new SqlConnection(ConnectionString);
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
            var conn = new SqlConnection(ConnectionString);
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