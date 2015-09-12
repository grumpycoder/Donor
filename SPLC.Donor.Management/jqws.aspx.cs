using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Net;
using System.IO;
using System.Xml;
using System.Configuration;
using SPLC.Donor.Models;


namespace SPLC.Donor.Management
{
    public partial class jqws : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.ContentType = "text/HTML";
            Response.ContentEncoding = Encoding.UTF8;

            StringBuilder sbReturn = new StringBuilder("");
            //HttpEncoder httE = new HttpEncoder();

            if (Request["fn"] != null)
            {
                switch (Request["fn"].ToString())
                {
                    case "Sample":
                        sbReturn = Sample();
                        break;
                    case "AddNewEvent":
                        sbReturn = AddNewEvent(Request["p1"].ToString(),Request["p2"].ToString());
                        break;
                    case "UpdateEvent":

                        string strS = Request["p3"].ToString();

                        sbReturn = UpdateEvent(Request["p1"].ToString(), Request["p2"].ToString(),Request["p3"].ToString());
                        break;
                    case "UpdateDonorEventList":
                        sbReturn = UpdateDonorEventList(Request["p1"].ToString(), Request["p2"].ToString(), Request["p3"].ToString());
                        break;
                    case "UpdateDonorList":
                        sbReturn = UpdateDonorList(Request["p1"].ToString(), Request["p2"].ToString(), Request["p3"].ToString(),Request["p4"].ToString());
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
            StringBuilder sbReturn = new StringBuilder();

            sbReturn.Append("HTML CODE");
 

            return sbReturn;
        }

        private StringBuilder AddNewEvent(string pEName, string pDate)
        {
            StringBuilder sbReturn = new StringBuilder();

            try
            {
                // Check if the date is valid
                try { DateTime EDate = DateTime.Parse(pDate); }
                catch { throw new Exception("Error"); }

                var EL = new EventList(_ConnStr,User.Identity.Name)
                {
                    EventName = pEName,
                    StartDate = DateTime.Parse(pDate)
                };
                EL.AddNew();

                if (EL.pk_Event.Equals(0))
                    throw new Exception("Error");
                else if (EL.pk_Event.Equals(-1))
                    throw new Exception("Duplicate");
                else
                    sbReturn.Append(EL.pk_Event.ToString());
            }
            catch
            {
                sbReturn.Append("Error");
            }
            
            return sbReturn;
        }

        private StringBuilder UpdateDonorList(string pID, string pField, string pValue, string pDELID)
        {
            StringBuilder sbReturn = new StringBuilder();

            try
            {
                DonorList DL = new DonorList(pID);

                
                switch (pField)
                {
                    case "AccountName":
                        DL.AccountName = pValue;
                        break;
                    case "AddressLine1":
                        DL.AddressLine1 = pValue;
                        break;
                    case "City":
                        DL.City = pValue;
                        break;
                    case "State":
                        DL.State = pValue;
                        break;
                    case "PostCode":
                        DL.PostCode = pValue;
                        break;
                    case "PhoneNumber":
                        DL.PhoneNumber = pValue;
                        break;
                    case "Email":
                        DL.EmailAddress = pValue;
                        break;
                }

                DL.Update();

                DonorEventList DEL = new DonorEventList(User.Identity.Name, int.Parse(pDELID));
                DEL.UpdatedInfo = true;
                DEL.UpdatedInfoDateTime = DateTime.Now;
                DEL.UpdatedInfo_User = User.Identity.Name;
                DEL.Update();

                sbReturn.Append("True");
            }
            catch
            {
                sbReturn.Append("Error");
            }

            return sbReturn;
        }

        private StringBuilder UpdateDonorEventList(string pID, string pField, string pValue)
        {
            StringBuilder sbReturn = new StringBuilder();

            try
            {
                DonorEventList DEL = new DonorEventList(User.Identity.Name, int.Parse(pID));
                
                switch (pField)
                {
                    case "TicketsRequested":
                        DEL.TicketsRequested = int.Parse(pValue);
                        break;
                    case "Attending":
                        DEL.Attending = bool.Parse(pValue);
                        break;
                    case "SPLCComments":
                        DEL.SPLCComments = pValue;
                        break;
                }

                DEL.Update();

                sbReturn.Append("True");
            }
            catch (Exception EX)
            {
                sbReturn.Append("Error");
            }

            return sbReturn;
        }

        private StringBuilder UpdateEvent(string pID, string pField, string pValue)
        {
            StringBuilder sbReturn = new StringBuilder();

            try
            {
                EventList EL = new EventList(_ConnStr, User.Identity.Name, int.Parse(pID));

                switch (pField)
                {
                    case "name":
                        if (pValue.Equals("True"))
                            EL.Active = true;
                        else
                            EL.Active = false;
                        break;
                    case "EventName":
                        EL.EventName = pValue;
                        break;
                    case "DisplayName":
                        EL.DisplayName = pValue;
                        break;
                    case "Speaker":
                        EL.Speaker = pValue;
                        break;
                    case "EventDate":
                        string strDate = pValue + " " + EL.StartDate.Hour.ToString() + ":" + EL.StartDate.Minute.ToString();
                        EL.StartDate = DateTime.Parse(strDate);
                        // When the Event Date is changed allso update the Date for End Date and Doors Open Date
                        strDate = pValue + " " + EL.EndDate.Hour.ToString() + ":" + EL.EndDate.Minute.ToString();
                        EL.EndDate = DateTime.Parse(strDate);
                        strDate = pValue + " " + EL.DoorsOpenDate.Hour.ToString() + ":" + EL.DoorsOpenDate.Minute.ToString();
                        EL.DoorsOpenDate = DateTime.Parse(strDate);
                        break;
                    case "VenueName":
                        EL.VenueName = pValue;
                        break;
                    case "VenueAddress":
                        EL.VenueAddress = pValue;
                        break;
                    case "VenueCity":
                        EL.VenueCity = pValue;
                        break;
                    case "VenueState":
                        EL.VenueState = pValue;
                        break;
                    case "VenueZipCode":
                        EL.VenueZipCode = pValue;
                        break;
                    case "Capacity":
                        EL.Capacity = int.Parse(pValue);
                        break;
                    case "ImageURL":
                        EL.ImageURL = pValue;
                        break;
                    case "StartDate":
                        EL.StartDate = DateTime.Parse(pValue);
                        break;
                    case "EndDate":
                        EL.EndDate = DateTime.Parse(pValue);
                        break;
                    case "DoorsOpenDate":
                        EL.DoorsOpenDate = DateTime.Parse(pValue);
                        break;
                    case "OnlineCloseDate":
                        EL.OnlineCloseDate = DateTime.Parse(pValue);
                        break;
                    case "Active":
                        EL.Active = bool.Parse(pValue);
                        if(!bool.Parse(pValue))
                        {
                            EL.InActive_Date = DateTime.Now;
                            EL.InActive_User = User.Identity.Name;
                        }
                        break;
                    case "TicketsAllowed":
                        EL.TicketsAllowed = int.Parse(pValue);
                        break;
                        
                }

                EL.Update();

                sbReturn.Append("True");
            }
            catch(Exception EX)
            {
                sbReturn.Append("Error");
            }

            return sbReturn;
        }
    }
}