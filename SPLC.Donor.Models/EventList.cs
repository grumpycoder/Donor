using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace SPLC.Donor.Models
{
    public class EventList
    {
        #region Private Variables
            private static string _ConnStr = "";
            private static string _User = "";
        #endregion

        #region Accessors

        public int pk_Event { get; set; }
        public DateTime Date_Added { get; set; }
        public string User_Added { get; set; }
        public string EventName { get; set; }
        public string DisplayName { get; set; }
        public string Speaker { get; set; }
        public int TicketsAllowed { get; set; }
        public bool Active { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DoorsOpenDate { get; set; }
        public DateTime OnlineCloseDate { get; set; }
        public string VenueName { get; set; }
        public string VenueAddress { get; set; }
        public string VenueCity { get; set; }
        public string VenueState { get; set; }
        public string VenueZipCode { get; set; }
        public int Capacity { get; set; }
        public string ImageURL { get; set; }
        public DateTime? InActive_Date { get; set; }
        public string InActive_User { get; set; }
        public string HTML_Header { get; set; }
        public string HTML_FAQ { get; set; }
        public Image Header_Image { get; set; }
        public string HTML_Yes { get; set; }
        public string HTML_No { get; set; }
        public string HTML_Wait { get; set; }

        #endregion

        #region Constructors

        public EventList()
        { }

        public EventList(string pConnString,string pUser)
        {
            _ConnStr = pConnString;
            _User = pUser;
        }

        public EventList(string pConnString, string pUser,int pID)
        {
            _ConnStr = pConnString;
            _User = pUser;
            pk_Event = pID;

            Load();
        }

        #endregion

        #region Standard Methods

        private void Load()
        {
            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            string strSQL = "SELECT TOP 1 * FROM EventList WHERE pk_Event=@pk_Event";
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            cmd.Parameters.AddWithValue("@pk_Event", pk_Event);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow dr = dt.Rows[0];
            Date_Added = DateTime.Parse(dr["Date_Added"].ToString());
            User_Added = dr["User_Added"].ToString();
            EventName = dr["EventName"].ToString();
            DisplayName = dr["DisplayName"].ToString();
            Speaker = dr["Speaker"].ToString();
            Active = bool.Parse(dr["Active"].ToString());

            if (!dr["StartDate"].ToString().Equals(""))
                StartDate = DateTime.Parse(dr["StartDate"].ToString());

            if (!dr["EndDate"].ToString().Equals(""))
                EndDate = DateTime.Parse(dr["EndDate"].ToString());

            if (!dr["DoorsOpenDate"].ToString().Equals(""))
                DoorsOpenDate = DateTime.Parse(dr["DoorsOpenDate"].ToString());

            if (!dr["OnlineCloseDate"].ToString().Equals(""))
                OnlineCloseDate = DateTime.Parse(dr["OnlineCloseDate"].ToString());

            VenueName = dr["VenueName"].ToString();
            VenueAddress = dr["VenueAddress"].ToString();
            VenueCity = dr["VenueCity"].ToString();
            VenueState = dr["VenueState"].ToString();
            VenueZipCode = dr["VenueZipCode"].ToString();
            Capacity = int.Parse(dr["Capacity"].ToString());
            ImageURL = dr["ImageURL"].ToString();
            TicketsAllowed = int.Parse(dr["TicketsAllowed"].ToString());

            if (!dr["InActive_Date"].ToString().Equals(""))
                InActive_Date = DateTime.Parse(dr["InActive_Date"].ToString());
            
            InActive_User = dr["InActive_User"].ToString();

            HTML_Header = dr["HTML_Header"].ToString();
            HTML_FAQ = dr["HTML_FAQ"].ToString();
            HTML_Yes = dr["HTML_Yes"].ToString();
            HTML_No = dr["HTML_No"].ToString();
            HTML_Wait = dr["HTML_Wait"].ToString();

            cmd.Dispose();
            da.Dispose();
            Conn.Close(); 
                           
        }

        public bool Exists(string pName,DateTime pDate)
        {
            string strSql = "IF EXISTS(SELECT TOP 1 pk_Event FROM EventList WHERE EventName=@EventName " +
                " AND convert(varchar(10), StartDate, 120) = convert(datetime,'" + pDate.ToShortDateString() + "',120)) " +
                " BEGIN SELECT 'True' END ELSE BEGIN SELECT 'False' END";

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlCommand cmd = new SqlCommand(strSql, Conn);
            cmd.Parameters.AddWithValue("@EventName", EventName);
            string strES = cmd.ExecuteScalar().ToString();
            Conn.Close();

            if (strES.Equals("True"))
                return true;
            else
                return false;
        }

        public void AddNew()
        {

            if (!Exists(EventName, StartDate))
            {
                string strSql = @"INSERT INTO EventList (EventName,DisplayName,StartDate,User_Added,Active,OnlineCloseDate) VALUES  
                                    (@EventName,@EventName,@StartDate,@User_Added,1,@StartDate); SELECT SCOPE_IDENTITY() ";

                SqlConnection Conn = new SqlConnection(_ConnStr);
                Conn.Open();
                SqlCommand cmd = new SqlCommand(strSql, Conn);

                cmd.Parameters.AddWithValue("@EventName", EventName);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@User_Added", _User);

                pk_Event = int.Parse(cmd.ExecuteScalar().ToString());
                Conn.Close();
            }
            else
                pk_Event = -1;
        }

        public void Update()
        {

            SqlConnection Conn = new SqlConnection(_ConnStr);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM EventList WHERE pk_Event=@pk_Event", Conn);
            SqlParameter param = new SqlParameter("@pk_Event", SqlDbType.Int);
            param.Value = pk_Event;
            da.SelectCommand.Parameters.Add(param);

            SqlCommandBuilder db = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataRow dr = ds.Tables[0].Rows[0];
            
            dr["Active"] = Active;

            if (StartDate > DateTime.Parse("1/1/2000"))
                dr["StartDate"] = StartDate;
            if (EndDate > DateTime.Parse("1/1/2000"))
                dr["EndDate"] = EndDate;
            if (DoorsOpenDate > DateTime.Parse("1/1/2000"))
                dr["DoorsOpenDate"] = DoorsOpenDate;
            if (OnlineCloseDate > DateTime.Parse("1/1/2000"))
                dr["OnlineCloseDate"] = OnlineCloseDate;

            dr["EventName"] = EventName;
            dr["DisplayName"] = DisplayName;
            dr["Speaker"] = Speaker;
            dr["VenueName"] = VenueName;
            dr["VenueAddress"] = VenueAddress;
            dr["VenueCity"] = VenueCity;
            dr["VenueState"] = VenueState;
            dr["VenueZipCode"] = VenueZipCode;
            dr["Capacity"] = Capacity;
            dr["ImageURL"] = ImageURL;
            dr["TicketsAllowed"] = TicketsAllowed;

            if (InActive_Date > DateTime.Parse("1/1/2000"))
            {
                if (Active)
                {
                    dr["InActive_Date"] = DBNull.Value;
                    InActive_User = "";
                }
                else
                    dr["InActive_Date"] = InActive_Date;
            }
            dr["InActive_User"] = InActive_User;

            dr["HTML_Header"] = HTML_Header;
            dr["HTML_FAQ"] = HTML_FAQ;

            if(Header_Image != null)
                dr["Header_Image"] = ConvertImageToByteArray(Header_Image, System.Drawing.Imaging.ImageFormat.Jpeg);

            dr["HTML_Yes"] = HTML_Yes;
            dr["HTML_No"] = HTML_No;
            dr["HTML_Wait"] = HTML_Wait;

            da.Update(ds.Tables[0]);
            da.Dispose();
            Conn.Close();
        }

        public void Delete()
        { }

        #endregion

        #region Custom Methods

        public bool SaveHeaderImage()
        {
            
            try
            {
                SqlConnection Conn = new SqlConnection(_ConnStr);
                Conn.Open();
                string strSQL = @"UPDATE EventList SET Header_Image=@Header_Image WHERE pk_Event=" + pk_Event.ToString();
                SqlCommand cmd = new SqlCommand(strSQL, Conn);
                cmd.Parameters.Add("@Header_Image", SqlDbType.Image, 0).Value = ConvertImageToByteArray(Header_Image, System.Drawing.Imaging.ImageFormat.Jpeg);
                int qResult = cmd.ExecuteNonQuery();
                if (qResult == 1)
                    throw new Exception("Error");

                Conn.Close();
            }
            catch (Exception EX)
            {
                return false;
            }

            return true;
        }

        private byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert,
                                       System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] Ret;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return Ret;
        }

        public DataTable GetEvents()
        {

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            
            SqlDataAdapter da = new SqlDataAdapter("SELECT pk_Event,EventName + ' | ' + convert(varchar(10), StartDate, 120) " +
                " AS EName FROM EventList", Conn);

            DataSet ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            Conn.Close();

            return ds.Tables[0];
        }

        public DataTable GetEventsReport()
        {

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();

            string strSQL = @"SELECT pk_Event, EventName, Capacity, VenueCity,CONVERT(VARCHAR(8), StartDate, 1) AS EventDate,
                                COUNT(fk_Event)AS ADDED, COUNT(Response_Date) AS RESPONSE,SUM(TicketsRequested) AS TicketsRequested,
                                COUNT(WaitingList_Date) AS WaitList,COUNT(TicketsMailed_Date) AS TicketsMailed
                                FROM EventList
                                LEFT JOIN DonorEventList
                                ON EventList.pk_Event = DonorEventList.fk_Event
                                GROUP BY pk_Event, EventName, Capacity, VenueCity, CONVERT(VARCHAR(8), StartDate, 1)
                                ORDER BY CONVERT(VARCHAR(8), StartDate, 1) DESC";

            SqlDataAdapter da = new SqlDataAdapter(strSQL, Conn);

            DataSet ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            Conn.Close();

            return ds.Tables[0];
        }

        public DataTable GetEvents_Search()
        {

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            string strSQL = @"SELECT pk_Event,StartDate,EventName,
                                (SELECT SUM(TicketsRequested) FROM DonorEventList WHERE fk_Event=EL.pk_Event
	                                AND Response_Date IS NOT NULL) AS RegCount,
                                (SELECT SUM(TicketsRequested) FROM DonorEventList WHERE fk_Event=EL.pk_Event
	                                AND WaitingList_Date IS NOT NULL) AS WaitCount
                                FROM EventList EL
                                ORDER BY StartDate DESC";
            SqlDataAdapter da = new SqlDataAdapter(strSQL, Conn);

            DataSet ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            Conn.Close();

            return ds.Tables[0];
        }
        
        #endregion
    }

}
