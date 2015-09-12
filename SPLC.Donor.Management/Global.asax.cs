using System;
using System.Web;
using SPLC.Donor.Models;

namespace SPLC.Donor.Management
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Write entry when user starts application
            var donorMessages = new DonorMessages()
            {
                MessageId = 100,
                MessageText = "Session Started",
                MessageDescription = "User started new session with application",
                User_Added = User.Identity.Name
            };
            donorMessages.AddNew();
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