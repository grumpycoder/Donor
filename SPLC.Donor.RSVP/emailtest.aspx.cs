using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Configuration;

namespace SPLC.Donor.RSVP
{
    public partial class emailtest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";

                //SmtpClient smtpClient = new SmtpClient();
                //smtpClient.EnableSsl = false;

                //string strHost = smtpClient.Host;
                //int intPort = smtpClient.Port;

                //MailMessage mail = new MailMessage();
                //mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                //mail.IsBodyHtml = false;
                //mail.Subject = txtSubject.Text.ToString();

                //mail.Body = txtSubject.Text.ToString();
                //mail.To.Add(new MailAddress(txtTo.Text.ToString()));
                //smtpClient.Send(mail);

                //lblError.Text = "Send Email was successful!";


            }
            catch(Exception EX)
            {
                lblError.Text = EX.Message;
            }
        }
    }
}