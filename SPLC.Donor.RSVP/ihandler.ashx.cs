using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace SPLC.Donor.RSVP
{
    public class ihandler : IHttpHandler
    {
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        public void ProcessRequest(HttpContext context)
        {
            var conn = new SqlConnection(ConnStr);
            var cmd = new SqlCommand(@"SELECT Header_Image FROM EventList WHERE pk_Event=" + context.Request.QueryString["eid"], conn);
            conn.Open();

            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                context.Response.ContentType = "image/jpg";
                try { context.Response.BinaryWrite((byte[])dr["Header_Image"]); }
                catch { }
            }

            conn.Close();
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