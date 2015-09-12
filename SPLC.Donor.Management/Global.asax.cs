using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SPLC.Donor.Management
{
    public class Global : System.Web.HttpApplication
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Application_Start(object sender, EventArgs e)
        {
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Write entry when user starts application
            var DM = new Models.DonorMessages(_ConnStr)
            {
                MessageId = 100,
                MessageText = "Session Started",
                MessageDescription = "User started new session with application",
                User_Added = User.Identity.Name.ToString()
            };
            DM.AddNew();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}