using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SPLC.Donor.Models
{
    public class EventList
    {
        private const string BaseDate = "1/1/2000";

        #region Private Variables
        private static string _user = "";
        #endregion

        private string ConnectionString { get; set; }

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
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();
        }

        public EventList(string user) : this()
        {
            _user = user;
        }

        public EventList(string user, int eventId) : this(user)
        {
            pk_Event = eventId;
            Load();
        }

        #endregion

        #region Standard Methods

        private void Load()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var sql = "SELECT TOP 1 * FROM EventList WHERE pk_Event=@pk_Event";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pk_Event", pk_Event);
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            var dr = dt.Rows[0];
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
            conn.Close();

        }

        public bool Exists(string eventName, DateTime startDate)
        {
            var sql = "IF EXISTS(SELECT TOP 1 pk_Event FROM EventList WHERE EventName=@EventName " +
                " AND convert(varchar(10), StartDate, 120) = convert(datetime,'" + startDate.ToShortDateString() + "',120)) " +
                " BEGIN SELECT 'True' END ELSE BEGIN SELECT 'False' END";

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@EventName", EventName);
            var result = cmd.ExecuteScalar().ToString();
            conn.Close();

            return result.Equals("True");
        }

        public void AddNew()
        {

            if (!Exists(EventName, StartDate))
            {
                const string sql = @"INSERT INTO EventList (EventName,DisplayName,StartDate,User_Added,Active,OnlineCloseDate) VALUES  
                                    (@EventName,@EventName,@StartDate,@User_Added,1,@StartDate); SELECT SCOPE_IDENTITY() ";

                var conn = new SqlConnection(ConnectionString);
                conn.Open();
                var cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@EventName", EventName);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@User_Added", _user);

                pk_Event = int.Parse(cmd.ExecuteScalar().ToString());
                conn.Close();
            }
            else
                pk_Event = -1;
        }

        public void Update()
        {

            var conn = new SqlConnection(ConnectionString);
            var da = new SqlDataAdapter("SELECT * FROM EventList WHERE pk_Event=@pk_Event", conn);
            var param = new SqlParameter("@pk_Event", SqlDbType.Int) { Value = pk_Event };
            da.SelectCommand.Parameters.Add(param);

            var ds = new DataSet();
            da.Fill(ds);

            var dr = ds.Tables[0].Rows[0];

            dr["Active"] = Active;

            if (StartDate > DateTime.Parse(BaseDate))
                dr["StartDate"] = StartDate;
            if (EndDate > DateTime.Parse(BaseDate))
                dr["EndDate"] = EndDate;
            if (DoorsOpenDate > DateTime.Parse(BaseDate))
                dr["DoorsOpenDate"] = DoorsOpenDate;
            if (OnlineCloseDate > DateTime.Parse(BaseDate))
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

            if (InActive_Date > DateTime.Parse(BaseDate))
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

            if (Header_Image != null)
                dr["Header_Image"] = ConvertImageToByteArray(Header_Image, ImageFormat.Jpeg);

            dr["HTML_Yes"] = HTML_Yes;
            dr["HTML_No"] = HTML_No;
            dr["HTML_Wait"] = HTML_Wait;

            da.Update(ds.Tables[0]);
            da.Dispose();
            conn.Close();
        }

        #endregion

        #region Custom Methods

        public bool SaveHeaderImage()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var sql = @"UPDATE EventList SET Header_Image=@Header_Image WHERE pk_Event=" + pk_Event;
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Header_Image", SqlDbType.Image, 0).Value = ConvertImageToByteArray(Header_Image, ImageFormat.Jpeg);
            var qResult = cmd.ExecuteNonQuery();
            var result = qResult != 1;

            conn.Close();

            return result;
        }

        private byte[] ConvertImageToByteArray(Image imageToConvert,
                                       ImageFormat formatOfImage)
        {
            byte[] ret;
            using (var ms = new MemoryStream())
            {
                imageToConvert.Save(ms, formatOfImage);
                ret = ms.ToArray();
            }
            return ret;
        }

        public DataTable GetEvents()
        {

            var conn = new SqlConnection(ConnectionString);
            conn.Open();

            var da = new SqlDataAdapter("SELECT pk_Event,EventName + ' | ' + convert(varchar(10), StartDate, 120) " +
                " AS EName FROM EventList", conn);

            var ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            conn.Close();

            return ds.Tables[0];
        }

        public DataTable GetEventsReport()
        {

            var conn = new SqlConnection(ConnectionString);
            conn.Open();

            const string sql = @"SELECT pk_Event, EventName, Capacity, VenueCity,CONVERT(VARCHAR(8), StartDate, 1) AS EventDate,
                                COUNT(fk_Event)AS ADDED, COUNT(Response_Date) AS RESPONSE,SUM(TicketsRequested) AS TicketsRequested,
                                COUNT(WaitingList_Date) AS WaitList,COUNT(TicketsMailed_Date) AS TicketsMailed
                                FROM EventList
                                LEFT JOIN DonorEventList
                                ON EventList.pk_Event = DonorEventList.fk_Event
                                GROUP BY pk_Event, EventName, Capacity, VenueCity, CONVERT(VARCHAR(8), StartDate, 1)
                                ORDER BY CONVERT(VARCHAR(8), StartDate, 1) DESC";

            var da = new SqlDataAdapter(sql, conn);

            var ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            conn.Close();

            return ds.Tables[0];
        }

        public DataTable GetEvents_Search()
        {

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            const string sql = @"SELECT pk_Event,StartDate,EventName,
                                (SELECT SUM(TicketsRequested) FROM DonorEventList WHERE fk_Event=EL.pk_Event
	                                AND Response_Date IS NOT NULL) AS RegCount,
                                (SELECT SUM(TicketsRequested) FROM DonorEventList WHERE fk_Event=EL.pk_Event
	                                AND WaitingList_Date IS NOT NULL) AS WaitCount
                                FROM EventList EL
                                ORDER BY StartDate DESC";
            var da = new SqlDataAdapter(sql, conn);

            var ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            conn.Close();

            return ds.Tables[0];
        }

        #endregion
    }

}
