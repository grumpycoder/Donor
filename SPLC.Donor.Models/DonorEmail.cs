using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;

namespace SPLC.Donor.Models
{
    public class DonorEmail
    {
        #region Private Variables
            private static string _ConnStr = "";
            private static string _User = "";
        #endregion

        #region Accessors
            public DonorList _DL { get; set; }
            public DonorEventList _DEL { get; set; }
            public string _URL { get; set; }

            public bool IsValid { get; set; }
        #endregion

        #region Constructors

        public DonorEmail()
        {
            IsValid = false;
        }

        public DonorEmail(string pConnString, string pUser, string pURL, DonorList pDL)
        {
            IsValid = false;
            _ConnStr = pConnString;
            _User = pUser;
            _URL = pURL;
            _DL = pDL;
        }

        public DonorEmail(string pConnString, string pUser, string pURL, DonorEventList pDEL)
        {
            IsValid = false;
            _ConnStr = pConnString;
            _User = pUser;
            _URL = pURL;
            _DEL = pDEL;
        }

        public DonorEmail(string pConnString, string pUser, string pURL, DonorList pDL, DonorEventList pDEL)
        {
            IsValid = false;
            _ConnStr = pConnString;
            _User = pUser;
            _URL = pURL;
            _DL = pDL;
            _DEL = pDEL;
        }

        #endregion

        #region Standard Methods      


        public void SendEmail()
        {
            

            try
            {
                StringBuilder sbMsg = new StringBuilder();
                string strGetURL = "";

                if (_DEL.WaitingList_Date > DateTime.Parse("1/1/2000"))
                {
                    switch (_DEL.Attending)
                    {
                        case true:
                            strGetURL = _URL + @"/templates/wait_yes.html";
                            break;
                        case false:
                            strGetURL = _URL + @"/templates/rsvp_no.html";
                            break;
                    }
                }
                else
                {
                    switch (_DEL.Attending)
                    {
                        case true:
                            strGetURL = _URL + @"/templates/rsvp_yes.html";
                            break;
                        case false:
                            strGetURL = _URL + @"/templates/rsvp_yes.html";
                            break;
                    }
                }

                HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create(strGetURL);
                mRequest.Method = "GET";
                WebResponse mResponse = mRequest.GetResponse();
                StreamReader sr = new StreamReader(mResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                sbMsg.Append(sr.ReadToEnd());
                sr.Close();
                mResponse.Close();

                EventList EL = new EventList(_ConnStr, _User, _DEL.fk_Event);

                
                if (_DEL.WaitingList_Date > DateTime.Parse("1/1/2000"))
                {
                    switch (_DEL.Attending)
                    {
                        case true:
                            sbMsg = sbMsg.Replace("@{TEXT}", EL.HTML_Wait);
                            break;
                        case false:
                            sbMsg = sbMsg.Replace("@{TEXT}", EL.HTML_No);
                            break;
                    }
                }
                else
                {
                    switch (_DEL.Attending)
                    {
                        case true:
                            sbMsg = sbMsg.Replace("@{TEXT}", EL.HTML_Yes);
                            break;
                        case false:
                            sbMsg = sbMsg.Replace("@{TEXT}", EL.HTML_No);
                            break;
                    }
                }

                sbMsg = ParseText(sbMsg, EL, _DL);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.EnableSsl = false;

                string strHost = smtpClient.Host;
                int intPort = smtpClient.Port;

                MailMessage mail = new MailMessage();
                mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                mail.IsBodyHtml = true;
                mail.Subject = "SPLC Event Confirmation";

                mail.Body = sbMsg.ToString();
                mail.To.Add(new MailAddress(_DL.EmailAddress.ToString()));
                smtpClient.Send(mail);

            }
            catch (Exception EX)
            {
                DonorMessages DMError = new DonorMessages(_ConnStr);
                DMError.MessageId = 2200;
                DMError.MessageText = "ERROR: DonorEmail.SendEmail()";
                DMError.MessageDescription = EX.Message;
                DMError.User_Added = _User;
                DMError.AddNew();
             }
        }

        public StringBuilder ParseText(StringBuilder pStringB,EventList pEL, DonorList pDL)
        {
            //pStringB = pStringB.Replace("@{DATE}", DateTime.Today.ToString("MMMM dd, yyyy"));
            //pStringB = pStringB.Replace("@{SALUTATION}", pDL.AccountName);

            //pStringB = pStringB.Replace("@{DisplayName}", pEL.DisplayName);
            //pStringB = pStringB.Replace("@{City}", pEL.VenueCity);
            //pStringB = pStringB.Replace("@{StartDate}", pEL.StartDate.ToLongDateString());
            //pStringB = pStringB.Replace("@{StartTime}", pEL.StartDate.ToShortTimeString());
            pStringB = ParseTextSubDL(pStringB, pDL);
            pStringB = ParseTextSubEL(pStringB, pEL);
            return pStringB;
        }

        public StringBuilder ParseTextSubDL(StringBuilder pStringB, DonorList pDL)
        {
            pStringB = pStringB.Replace("@{SALUTATION}", pDL.AccountName);
            return pStringB;
        }

        public StringBuilder ParseTextSubEL(StringBuilder pStringB, EventList pEL)
        {
            pStringB = pStringB.Replace("@{DATE}", DateTime.Today.ToString("MMMM dd, yyyy"));
            pStringB = pStringB.Replace("@{DisplayName}", pEL.DisplayName);
            pStringB = pStringB.Replace("@{City}", pEL.VenueCity);
            pStringB = pStringB.Replace("@{StartDate}", pEL.StartDate.ToLongDateString());
            pStringB = pStringB.Replace("@{StartTime}", pEL.StartDate.ToShortTimeString());
            return pStringB;
        }

        #endregion
    }
}
