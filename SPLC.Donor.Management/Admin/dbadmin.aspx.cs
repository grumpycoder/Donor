using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SPLC.Donor.Models;

namespace SPLC.Donor.Management.Admin
{
    public partial class dbadmin : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                try
                {
                    UserList UL = new UserList(_ConnStr, User.Identity.Name);
                    if (!UL.IsInRole(10))
                        throw new Exception("");
                }
                catch
                {
                    Response.Redirect("~/default.aspx");
                }

                lblConn.Text = _ConnStr;
                lblConnTest.Text = "";
                lblMessage.Text = "";

                SqlConnection Conn = new SqlConnection(_ConnStr);
                Conn.Open();

                Conn.Close();
                lblConnTest.Text = "DB Status = Good!";
            }
            catch(Exception EX)
            {
                lblConnTest.Text = "DB Status = " + EX.Message;
            }

        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection Conn = new SqlConnection(_ConnStr);
                Conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(txtSQL.Text.ToString(), Conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                gvResults.DataSource = ds.Tables[0];
                gvResults.DataBind();

                Conn.Close();
            }
            catch(Exception EX)
            {
                lblMessage.Text = "Error: " + EX.Message;
            }
        }
    }
}