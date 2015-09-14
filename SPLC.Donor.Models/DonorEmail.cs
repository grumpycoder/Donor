using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SPLC.Donor.Models
{
    public class DonorEmail
    {
        private const string BaseDate = "1/1/2000";
        private static string _user = "";

        #region Accessors

        public DonorList _DL { get; set; }
        public DonorEventList _DEL { get; set; }
        public string _URL { get; set; }
        public bool IsValid { get; set; }

        private string ConnectionString { get; set; }

        #endregion

        #region Constructors

        public DonorEmail()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();
            IsValid = false;
        }

        public DonorEmail(string user, string url) : this()
        {
            _user = user;
            _URL = url;
        }

        public DonorEmail(string user, string url, DonorList donorList) : this(user, url)
        {
            _DL = donorList;
        }

        public DonorEmail(string user, string url, DonorEventList donorEventList) : this(user, url)
        {
            _DEL = donorEventList;
        }

        public DonorEmail(string user, string url, DonorList donorList, DonorEventList donorEventList) : this(user, url, donorList)
        {
            _DEL = donorEventList;
        }

        #endregion

        #region Standard Methods      
        
        public void SendEmail()
        {

            try
            {
                var msg = new StringBuilder();
                var getUrl = "";

                if (_DEL.WaitingList_Date > DateTime.Parse(BaseDate))
                {
                    switch (_DEL.Attending)
                    {
                        case true:
                            getUrl = _URL + @"/templates/wait_yes.html";
                            break;
                        case false:
                            getUrl = _URL + @"/templates/rsvp_no.html";
                            break;
                    }
                }
                else
                {
                    switch (_DEL.Attending)
                    {
                        case true:
                            getUrl = _URL + @"/templates/rsvp_yes.html";
                            break;
                        case false:
                            getUrl = _URL + @"/templates/rsvp_yes.html";
                            break;
                    }
                }

                var request = (HttpWebRequest)WebRequest.Create(getUrl);
                request.Method = "GET";
                var response = request.GetResponse();
                
                var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                msg.Append(reader.ReadToEnd());
                reader.Close();
                response.Close();

                var eventList = new EventList(_user, _DEL.fk_Event);
                
                if (_DEL.WaitingList_Date > DateTime.Parse(BaseDate))
                {
                    switch (_DEL.Attending)
                    {
                        case true:
                            msg = msg.Replace("@{TEXT}", eventList.HTML_Wait);
                            break;
                        case false:
                            msg = msg.Replace("@{TEXT}", eventList.HTML_No);
                            break;
                    }
                }
                else
                {
                    switch (_DEL.Attending)
                    {
                        case true:
                            msg = msg.Replace("@{TEXT}", eventList.HTML_Yes);
                            break;
                        case false:
                            msg = msg.Replace("@{TEXT}", eventList.HTML_No);
                            break;
                    }
                }

                msg = ParseText(msg, eventList, _DL);

                var smtpClient = new SmtpClient {EnableSsl = false};
                
                var mail = new MailMessage
                {
                    BodyEncoding = Encoding.GetEncoding("utf-8"),
                    IsBodyHtml = true,
                    Subject = "SPLC Event Confirmation",
                    Body = msg.ToString()
                };

                mail.To.Add(new MailAddress(_DL.EmailAddress));
                smtpClient.Send(mail);

            }
            catch (Exception ex)
            {
                var donorMessages = new DonorMessages()
                {
                    MessageId = 2200,
                    MessageText = "ERROR: DonorEmail.SendEmail()",
                    MessageDescription = ex.Message,
                    User_Added = _user
                };
                donorMessages.AddNew();
            }
        }

        public StringBuilder ParseText(StringBuilder message, EventList eventList, DonorList donorList)
        {
            message = ParseTextSubDL(message, donorList);
            message = ParseTextSubEL(message, eventList);
            return message;
        }

        private StringBuilder ParseTextSubDL(StringBuilder message, DonorList donorList)
        {
            message = message.Replace("@{SALUTATION}", donorList.AccountName);
            message = message.Replace("@{FINDERNUMBER}", donorList.pk_DonorList);
            return message;
        }

        public StringBuilder ParseTextSubEL(StringBuilder message, EventList eventList)
        {
            message = message.Replace("@{DATE}", DateTime.Today.ToString("MMMM dd, yyyy"));
            message = message.Replace("@{DisplayName}", eventList.DisplayName);
            message = message.Replace("@{City}", eventList.VenueCity);
            message = message.Replace("@{StartDate}", eventList.StartDate.ToLongDateString());
            message = message.Replace("@{StartTime}", eventList.StartDate.ToShortTimeString());
            return message;
        }

        #endregion
    }
}
