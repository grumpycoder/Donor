using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SPLC.Donor.Models;

namespace SPLC.Donor.RSVP
{
    /// <summary>
    /// Summary description for ihandler
    /// </summary>
    public class ihandler : IHttpHandler
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        public void ProcessRequest(HttpContext context)
        {
            SqlConnection Conn = new SqlConnection(_ConnStr);
            SqlCommand cmd = new SqlCommand(@"SELECT Header_Image FROM EventList WHERE pk_Event=" + context.Request.QueryString["eid"].ToString(), Conn);
            Conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                context.Response.ContentType = "image/jpg";
                try { context.Response.BinaryWrite((byte[])dr["Header_Image"]); }
                catch { }
            }

            Conn.Close();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}