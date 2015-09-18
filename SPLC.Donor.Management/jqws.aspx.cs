using System;
using System.Text;
using SPLC.Donor.Models;

namespace SPLC.Donor.Management
{
    public partial class jqws : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.ContentType = "text/HTML";
            Response.ContentEncoding = Encoding.UTF8;

            var sbReturn = new StringBuilder("");

            if (Request["fn"] != null)
            {
                switch (Request["fn"])
                {
                    case "Sample":
                        sbReturn = Sample();
                        break;
                    case "AddNewEvent":
                        sbReturn = AddNewEvent(Request["p1"], Request["p2"]);
                        break;
                    case "UpdateEvent":
                        sbReturn = UpdateEvent(Request["p1"], Request["p2"], Request["p3"]);
                        break;
                    case "UpdateDonorEventList":
                        sbReturn = UpdateDonorEventList(Request["p1"], Request["p2"], Request["p3"]);
                        break;
                    case "UpdateDonorList":
                        sbReturn = UpdateDonorList(Request["p1"], Request["p2"], Request["p3"], Request["p4"]);
                        break;
                    default:
                        sbReturn = sbReturn.Append("NULL");
                        break;
                }
            }
            Response.Write(sbReturn);
        }


        private StringBuilder Sample()
        {
            var sbReturn = new StringBuilder();
            sbReturn.Append("HTML CODE");
            return sbReturn;
        }

        private StringBuilder AddNewEvent(string eventName, string startDate)
        {
            var sbReturn = new StringBuilder();

            try
            {
                // Check if the date is valid
                try
                {
                    var eDate = DateTime.Parse(startDate);
                }
                catch
                {
                    throw new Exception("Error");
                }

                var eventList = new EventList(User.Identity.Name)
                {
                    EventName = eventName,
                    StartDate = DateTime.Parse(startDate)
                };
                eventList.AddNew();

                if (eventList.pk_Event.Equals(0))
                    throw new Exception("Error");
                else if (eventList.pk_Event.Equals(-1))
                    throw new Exception("Duplicate");
                else
                    sbReturn.Append(eventList.pk_Event.ToString());
            }
            catch
            {
                sbReturn.Append("Error");
            }

            return sbReturn;
        }

        private StringBuilder UpdateDonorList(string id, string field, string value, string donorEventListId)
        {
            var sbReturn = new StringBuilder();

            try
            {
                var donorList = new DonorList(id);

                switch (field)
                {
                    case "AccountName":
                        donorList.AccountName = value;
                        break;
                    case "AddressLine1":
                        donorList.AddressLine1 = value;
                        break;
                    case "City":
                        donorList.City = value;
                        break;
                    case "State":
                        donorList.State = value;
                        break;
                    case "PostCode":
                        donorList.PostCode = value;
                        break;
                    case "PhoneNumber":
                        donorList.PhoneNumber = value;
                        break;
                    case "Email":
                        donorList.EmailAddress = value;
                        break;
                }

                //                donorList.Update();
                donorList.Save();

                var donorEventList = new DonorEventList(User.Identity.Name, int.Parse(donorEventListId))
                {
                    UpdatedInfo = true,
                    UpdatedInfoDateTime = DateTime.Now,
                    UpdatedInfo_User = User.Identity.Name
                };
                donorEventList.Update();

                sbReturn.Append("True");
            }
            catch
            {
                sbReturn.Append("Error");
            }

            return sbReturn;
        }

        private StringBuilder UpdateDonorEventList(string id, string field, string value)
        {
            var sbReturn = new StringBuilder();

            try
            {
                var donorEventList = new DonorEventList(User.Identity.Name, int.Parse(id));

                switch (field)
                {
                    case "TicketsRequested":
                        donorEventList.TicketsRequested = int.Parse(value);
                        break;
                    case "Attending":
                        donorEventList.Attending = bool.Parse(value);
                        break;
                    case "SPLCComments":
                        donorEventList.SPLCComments = value;
                        break;
                }

//                donorEventList.Update();
                donorEventList.SaveChanges();
                sbReturn.Append("True");
            }
            catch (Exception ex)
            {
                sbReturn.Append("Error");
            }

            return sbReturn;
        }

        private StringBuilder UpdateEvent(string id, string field, string value)
        {
            var sbReturn = new StringBuilder();

            try
            {
                var eventList = new EventList(User.Identity.Name, int.Parse(id));

                switch (field)
                {
                    case "name":
                        if (value.Equals("True"))
                            eventList.Active = true;
                        else
                            eventList.Active = false;
                        break;
                    case "EventName":
                        eventList.EventName = value;
                        break;
                    case "DisplayName":
                        eventList.DisplayName = value;
                        break;
                    case "Speaker":
                        eventList.Speaker = value;
                        break;
                    case "EventDate":
                        string strDate = value + " " + eventList.StartDate.Hour + ":" + eventList.StartDate.Minute;
                        eventList.StartDate = DateTime.Parse(strDate);
                        // When the Event Date is changed allso update the Date for End Date and Doors Open Date
                        strDate = value + " " + eventList.EndDate.Hour + ":" + eventList.EndDate.Minute;
                        eventList.EndDate = DateTime.Parse(strDate);
                        strDate = value + " " + eventList.DoorsOpenDate.Hour + ":" + eventList.DoorsOpenDate.Minute;
                        eventList.DoorsOpenDate = DateTime.Parse(strDate);
                        break;
                    case "VenueName":
                        eventList.VenueName = value;
                        break;
                    case "VenueAddress":
                        eventList.VenueAddress = value;
                        break;
                    case "VenueCity":
                        eventList.VenueCity = value;
                        break;
                    case "VenueState":
                        eventList.VenueState = value;
                        break;
                    case "VenueZipCode":
                        eventList.VenueZipCode = value;
                        break;
                    case "Capacity":
                        eventList.Capacity = int.Parse(value);
                        break;
                    case "ImageURL":
                        eventList.ImageURL = value;
                        break;
                    case "StartDate":
                        eventList.StartDate = DateTime.Parse(value);
                        break;
                    case "EndDate":
                        eventList.EndDate = DateTime.Parse(value);
                        break;
                    case "DoorsOpenDate":
                        eventList.DoorsOpenDate = DateTime.Parse(value);
                        break;
                    case "OnlineCloseDate":
                        eventList.OnlineCloseDate = DateTime.Parse(value);
                        break;
                    case "Active":
                        eventList.Active = bool.Parse(value);
                        if (!bool.Parse(value))
                        {
                            eventList.InActive_Date = DateTime.Now;
                            eventList.InActive_User = User.Identity.Name;
                        }
                        break;
                    case "TicketsAllowed":
                        eventList.TicketsAllowed = int.Parse(value);
                        break;

                }

//                eventList.Update();
                eventList.SaveChanges();
                sbReturn.Append("True");
            }
            catch (Exception ex)
            {
                sbReturn.Append("Error");
            }

            return sbReturn;
        }
    }
}