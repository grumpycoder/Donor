using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SPLC.Donor.Models
{
    public class UserList
    {
        #region Private Variables
        private static string _ConnStr = "";
        private static string _User = "";
        #endregion

        #region Accessors

        public int pk_UserList { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; }

        public bool Exists { get; set; }

        #endregion

        #region Constructors

        public UserList()
        { Exists = false; }

        public UserList(string pConnString,string pUser)
        {
            Exists = false;
            _ConnStr = pConnString;
            _User = pUser;
            UserName = pUser;
            Load();
        }

        public UserList(string pConnString, string pUser, int pID)
        {
            Exists = false;
            _ConnStr = pConnString;
            _User = pUser;
            pk_UserList = pID;
            Load();
        }

        #endregion

        #region Standard Methods

        private void Load()
        {
            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Conn;

            if(pk_UserList > 0)
            {
                cmd.CommandText = "SELECT TOP 1 * FROM UserList WHERE pk_UserList=@pk_UserList";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@pk_UserList", pk_UserList);
            }
            else
            {
                cmd.CommandText = "SELECT TOP 1 * FROM UserList WHERE UserName=@UserName";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserName", UserName);
            }
           
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmd.Dispose();
            da.Dispose();
            Conn.Close();

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                pk_UserList = int.Parse(dr["pk_UserList"].ToString());
                UserName = dr["UserName"].ToString();
                FullName = dr["FullName"].ToString();
                Role = int.Parse(dr["Role"].ToString());

                Exists = true;
            }
        }

        #endregion

        #region Custom Methods

        public bool IsInRole(int pRole)
        {
            if (Role >= pRole)
                return true;
            else
                return false;
        }

        public bool IsUserInRole(string pUN, int pRole)
        {
            try
            {
                SqlConnection Conn = new SqlConnection(_ConnStr);
                Conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;

                cmd.CommandText = "SELECT TOP 1 * FROM UserList WHERE UserName=@UserName";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserName", UserName);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow dr = dt.Rows[0];

                cmd.Dispose();
                da.Dispose();
                Conn.Close();

                if (int.Parse(dr["Role"].ToString()) >= pRole)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
