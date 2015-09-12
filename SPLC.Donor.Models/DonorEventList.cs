using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SPLC.Donor.Models
{
    public class DonorEventList
    {
        private const string BaseDate = "1/1/2000";
        private string ConnectionString { get; set; }

        #region Private Variables
        private static string _User = "";
        #endregion

        #region Accessors

        public int pk_DonorEventList { get; set; }
        public int fk_Event { get; set; }
        public string fk_DonorList { get; set; }
        public DateTime Date_Added { get; set; }
        public string User_Added { get; set; }
        public DateTime Response_Date { get; set; }
        public string Response_Type { get; set; }
        public int TicketsRequested { get; set; }
        public DateTime WaitingList_Date { get; set; }
        public int WaitingListOrder { get; set; }
        public bool Attending { get; set; }
        public DateTime TicketsMailed_Date { get; set; }
        public string TicketsMailed_User { get; set; }
        public string DonorComments { get; set; }
        public string SPLCComments { get; set; }
        public bool UpdatedInfo { get; set; }
        public DateTime UpdatedInfoDateTime { get; set; }
        public string UpdatedInfo_User { get; set; }

        public bool IsValid { get; set; }

        #endregion

        #region Constructors

        public DonorEventList()
        {
            IsValid = false;
            ConnectionString = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();
        }

        public DonorEventList(string pUser) : this()
        {
            _User = pUser;
        }

        public DonorEventList(string pUser, int donorEventListId) : this(pUser)
        {
            pk_DonorEventList = donorEventListId;
            Load();
        }

        #endregion

        #region Standard Methods

        public void AddNew()
        {
            const string sql = @"INSERT INTO DonorEventList (fk_Event,fk_DonorList,Date_Added,User_Added) VALUES (@fk_Event,
                                @fk_DonorList,getdate(),@User_Added); SELECT SCOPE_IDENTITY()";

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fk_Event", fk_Event);
            cmd.Parameters.AddWithValue("@fk_DonorList", fk_DonorList);
            cmd.Parameters.AddWithValue("@User_Added", _User);
            var result = cmd.ExecuteScalar().ToString();
            conn.Close();

            if (int.Parse(result) <= 0) return;

            pk_DonorEventList = int.Parse(result);
            Load();
        }

        public void Create()
        {
            const string sql = @"INSERT INTO DonorEventList (fk_Event, fk_DonorList, Date_Added, User_Added, TicketsRequested, WaitingListOrder, Attending, UpdatedInfo) " +
                               "VALUES (@fk_Event, @fk_DonorList, getdate(), @User_Added, 0, 0, 0, 0); SELECT SCOPE_IDENTITY()";

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fk_Event", fk_Event);
            cmd.Parameters.AddWithValue("@fk_DonorList", fk_DonorList);
            cmd.Parameters.AddWithValue("@User_Added", _User);
            var result = cmd.ExecuteScalar().ToString();
            conn.Close();

            if (int.Parse(result) <= 0) return;

            pk_DonorEventList = int.Parse(result);
            Load();
        }

        public void Load(int eventId, string donorListId)
        {
            var sql = @"IF EXISTS(SELECT pk_DonorEventList FROM DonorEventList WHERE fk_Event=@fk_Event AND fk_DonorList=@fk_DonorList)
                                BEGIN (SELECT pk_DonorEventList FROM DonorEventList WHERE fk_Event=@fk_Event AND fk_DonorList=@fk_DonorList) END
                                ELSE BEGIN SELECT -1 END";

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fk_Event", eventId);
            cmd.Parameters.AddWithValue("@fk_DonorList", donorListId);
            var result = cmd.ExecuteScalar().ToString();
            conn.Close();

            if (int.Parse(result) <= 0) return;

            pk_DonorEventList = int.Parse(result);
            Load();
        }

        private void Load()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            const string sql = "SELECT TOP 1 * FROM DonorEventList WHERE pk_DonorEventList=@pk_DonorEventList";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pk_DonorEventList", pk_DonorEventList);
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];

                fk_Event = int.Parse(dr["fk_Event"].ToString());
                fk_DonorList = dr["fk_DonorList"].ToString();
                Date_Added = DateTime.Parse(dr["Date_Added"].ToString());
                User_Added = dr["User_Added"].ToString();

                if (!dr["Response_Date"].ToString().Equals(""))
                    Response_Date = DateTime.Parse(dr["Response_Date"].ToString());

                Response_Type = dr["Response_Type"].ToString();
                TicketsRequested = int.Parse(dr["TicketsRequested"].ToString());

                if (dr["WaitingList_Date"] != null)
                {
                    if (!dr["WaitingList_Date"].ToString().Equals(""))
                        WaitingList_Date = DateTime.Parse(dr["WaitingList_Date"].ToString());
                }

                if (!dr["WaitingListOrder"].Equals(""))
                {
                    WaitingListOrder = int.Parse(dr["WaitingListOrder"].ToString());
                }

                Attending = bool.Parse(dr["Attending"].ToString());

                if (!dr["TicketsMailed_Date"].ToString().Equals(""))
                    TicketsMailed_Date = DateTime.Parse(dr["TicketsMailed_Date"].ToString());

                TicketsMailed_User = dr["TicketsMailed_User"].ToString();
                DonorComments = dr["DonorComments"].ToString();
                SPLCComments = dr["SPLCComments"].ToString();
                UpdatedInfo = bool.Parse(dr["UpdatedInfo"].ToString());

                if (!dr["UpdatedInfoDateTime"].ToString().Equals(""))
                    UpdatedInfoDateTime = DateTime.Parse(dr["UpdatedInfoDateTime"].ToString());

                UpdatedInfo_User = dr["UpdatedInfo_User"].ToString();

                IsValid = true;
            }


            cmd.Dispose();
            da.Dispose();
            conn.Close();

        }

        public void Update()
        {

            var conn = new SqlConnection(ConnectionString);
            var da = new SqlDataAdapter("SELECT * FROM DonorEventList WHERE pk_DonorEventList=@pk_DonorEventList", conn);
            var param = new SqlParameter("@pk_DonorEventList", SqlDbType.Int) { Value = pk_DonorEventList };
            da.SelectCommand.Parameters.Add(param);

            var ds = new DataSet();
            da.Fill(ds);

            var dr = ds.Tables[0].Rows[0];

            if (Response_Date > DateTime.Parse(BaseDate))
                dr["Response_Date"] = Response_Date;

            dr["Response_Type"] = Response_Type;
            dr["TicketsRequested"] = TicketsRequested;

            if (WaitingList_Date > DateTime.Parse(BaseDate))
                dr["WaitingList_Date"] = WaitingList_Date;

            dr["WaitingListOrder"] = WaitingListOrder;
            dr["Attending"] = Attending;

            if (TicketsMailed_Date > DateTime.Parse(BaseDate))
                dr["TicketsMailed_Date"] = TicketsMailed_Date;

            dr["TicketsMailed_User"] = TicketsMailed_User;
            dr["DonorComments"] = DonorComments;
            dr["SPLCComments"] = SPLCComments;

            dr["UpdatedInfo"] = UpdatedInfo;

            if (UpdatedInfoDateTime > DateTime.Parse(BaseDate))
                dr["UpdatedInfoDateTime"] = UpdatedInfoDateTime;
            else
                dr["UpdatedInfoDateTime"] = DBNull.Value;

            dr["UpdatedInfo_User"] = UpdatedInfo_User;

            da.Update(ds.Tables[0]);
            da.Dispose();
            conn.Close();
        }

        public void SaveChanges()
        {
            using (var cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                var cmd = new SqlCommand()
                {
                    CommandText = "UPDATE DonorEventList SET " +
                                  "Response_Date = @Response_Date, Response_Type = @Response_Type, TicketsRequested = @TicketsRequested, WaitingList_Date = @WaitingList_Date, " +
                                  "WaitingListOrder = @WaitingListOrder, Attending = @Attending, TicketsMailed_Date = @TicketsMailed_Date, TicketsMailed_User = @TicketsMailed_User, " +
                                  "DonorComments = @DonorComments, SPLCComments = @SPLCComments, UpdatedInfo = @UpdatedInfo, UpdatedInfoDateTime = @UpdatedInfoDateTime, UpdatedInfo_User = @UpdatedInfo_User " +
                                  "WHERE pk_DonorEventList=@pk_DonorEventList",
                    Connection = cn,
                    CommandType = CommandType.Text
                };
                cmd.Parameters.AddWithValue("pk_DonorEventList", pk_DonorEventList);

                cmd.Parameters.AddWithValue("Response_Date", Response_Date);
                cmd.Parameters.AddWithValue("Response_Type", Response_Type);
                cmd.Parameters.AddWithValue("TicketsRequested", TicketsRequested);
                cmd.Parameters.AddWithValue("WaitingList_Date", WaitingList_Date);
                cmd.Parameters.AddWithValue("WaitingListOrder", WaitingListOrder);
                cmd.Parameters.AddWithValue("Attending", Attending);
                cmd.Parameters.AddWithValue("TicketsMailed_Date", TicketsMailed_Date);
                cmd.Parameters.AddWithValue("TicketsMailed_User", TicketsMailed_User);
                cmd.Parameters.AddWithValue("DonorComments", DonorComments);
                cmd.Parameters.AddWithValue("SPLCComments", SPLCComments);
                cmd.Parameters.AddWithValue("UpdatedInfo", UpdatedInfo);
                cmd.Parameters.AddWithValue("UpdatedInfoDateTime", UpdatedInfoDateTime);
                cmd.Parameters.AddWithValue("UpdatedInfo_User", UpdatedInfo_User);

                if (!CheckDatePassedDefault(Response_Date)) cmd.Parameters["Response_Date"].Value = DBNull.Value;
                if (!CheckDatePassedDefault(WaitingList_Date)) cmd.Parameters["WaitingList_Date"].Value = DBNull.Value;
                if (!CheckDatePassedDefault(TicketsMailed_Date)) cmd.Parameters["TicketsMailed_Date"].Value = DBNull.Value;
                if (!CheckDatePassedDefault(UpdatedInfoDateTime)) cmd.Parameters["UpdatedInfoDateTime"].Value = DBNull.Value;

                cmd.ExecuteNonQuery();
            }
        }

        private bool CheckDatePassedDefault(DateTime checkDate)
        {
            return checkDate > DateTime.Parse(BaseDate);
        }

        #endregion

        #region Custom Modules

//        public bool IsRegistered(string donorId, int eventId)
//        {
//            //TODO: Why is this commented
//            //string strSql = "IF EXISTS(SELECT TOP 1 pk_Event FROM EventList WHERE EventName=@EventName " +
//            //    " AND convert(varchar(10), StartDate, 120) = convert(datetime,'" + pDate.ToShortDateString() + "',120)) " +
//            //    " BEGIN SELECT 'True' END ELSE BEGIN SELECT 'False' END";
//
//            //SqlConnection Conn = new SqlConnection(ConnectionString);
//            //Conn.Open();
//            //SqlCommand cmd = new SqlCommand(strSql, Conn);
//            //cmd.Parameters.AddWithValue("@EventName", EventName);
//            //string strES = cmd.ExecuteScalar().ToString();
//            //Conn.Close();
//
//            //if (strES.Equals("True"))
//            //    return true;
//            //else
//            return false;
//        }

//        public bool RegisterUser()
//        {
//            return true;
//        }

        public int GetTicketCountForEvent()
        {
            const string sql = @"SELECT CASE WHEN
                            (
                            SELECT SUM(TicketsRequested) 
                            FROM DonorEventList 
                            WHERE fk_Event=@fk_Event AND WaitingList_Date IS NULL) IS NULL THEN '0'
                            ELSE
	                            (SELECT SUM(TicketsRequested) 
                            FROM DonorEventList 
                            WHERE fk_Event=@fk_Event AND WaitingList_Date IS NULL)
                            END";

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fk_Event", fk_Event);
            var result = cmd.ExecuteScalar().ToString();
            conn.Close();

            return int.Parse(result);
        }

        public int GetNextWaitListNumber()
        {
            const string sql = @"SELECT MAX(WaitingListOrder) FROM DonorEventList WHERE fk_Event=@fk_Event";

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fk_Event", fk_Event);
            var result = cmd.ExecuteScalar().ToString();
            conn.Close();

            return int.Parse(result) + 1;
        }

        public DataTable GetRecentResponses(int rowCount)
        {
            var sql = @"SELECT TOP " + rowCount + @" pk_DonorEventList,EventName,(CASE WHEN Attending = 1 THEN 'Yes' ELSE 'No' END) AS Attending,TicketsRequested,AccountName,Response_Date 
                                FROM DonorEventList DEL
                                LEFT JOIN EventList EL ON DEL.fk_Event = EL.pk_Event
                                LEFT JOIN DonorList DL ON DEL.fk_DonorList = DL.pk_DonorList
                                WHERE Response_Date IS NOT NULL AND TicketsMailed_Date IS NULL 
                                ORDER BY Response_Date DESC";

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var da = new SqlDataAdapter(sql, conn);

            var dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            conn.Close();

            return dt;
        }

        public DataTable GetWaitingList_Search(string eventId, string donorId, string lastName, string donorType)
        {
            var sql = @"SELECT pk_DonorList,pk_DonorEventList,AccountName,MembershipYear,WaitingList_Date,WaitingListOrder,TicketsRequested,DonorType 
                                FROM DonorEventList DEL
                                LEFT JOIN EventList EL ON DEL.fk_Event = EL.pk_Event
                                LEFT JOIN DonorList DL ON DEL.fk_DonorList = DL.pk_DonorList
                                WHERE WaitingList_Date IS NOT NULL";

            if (!eventId.Equals(""))
                sql += " AND pk_Event='" + eventId + "' ";

            if (!donorId.Equals(""))
                sql += " AND pk_DonorList LIKE '%" + donorId + "%' ";

            if (!lastName.Equals(""))
                sql += " AND AccountName LIKE '%" + lastName + "%' ";

            if (!donorType.Equals(""))
                sql += " AND DonorType = '" + donorType + "' ";

            sql += " ORDER BY WaitingListOrder";

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var da = new SqlDataAdapter(sql, conn);

            var dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            conn.Close();

            return dt;
        }

        public DataTable GetDonorEventList_Search(string eventId, string donorId, string lastName, int topRecCount, bool showMailOnly)
        {
            var sql = @"SELECT TOP " + topRecCount + @" pk_DonorEventList,pk_DonorList,AccountName,DonorType,Response_Date,MembershipYear,Attending,TicketsRequested,TicketsMailed_Date 
                                FROM DonorEventList DEL
                                LEFT JOIN EventList EL
                                ON DEL.fk_Event = EL.pk_Event
                                LEFT JOIN DonorList DL
                                ON DEL.fk_DonorList = DL.pk_DonorList
                                WHERE pk_DonorList IS NOT NULL ";
            if (showMailOnly)
                sql += @" AND TicketsMailed_Date IS NULL ";

            if (!eventId.Equals(""))
                sql += " AND fk_Event='" + eventId + "' ";

            if (!donorId.Equals(""))
                sql += " AND pk_DonorList LIKE '%" + donorId + "%' ";

            if (!lastName.Equals(""))
                sql += " AND AccountName LIKE '%" + lastName + "%' ";

            sql += " ORDER BY DEL.Response_Date DESC";

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var da = new SqlDataAdapter(sql, conn);

            var dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            conn.Close();

            return dt;
        }

        public DataTable GetDonorList_Search(string eventId, string pNamePart, int topRecCount)
        {

            var sql = "SELECT ";

            if (topRecCount > 0)
                sql += " TOP " + topRecCount;

            sql += @" pk_DonorEventList,pk_DonorList,AccountName,DonorType,Response_Date,MembershipYear,
                                CASE WHEN Attending='False' THEN 'No' ELSE 'Yes' END AS Attending,
                                TicketsRequested,TicketsMailed_Date 
                                FROM DonorEventList DEL
                                LEFT JOIN EventList EL
                                ON DEL.fk_Event = EL.pk_Event
                                LEFT JOIN DonorList DL
                                ON DEL.fk_DonorList = DL.pk_DonorList
                                WHERE pk_DonorList IS NOT NULL 
                                AND fk_Event=@fk_Event";

            if (!pNamePart.Equals(""))
                sql += " AND AccountName LIKE '%' + @pNamePart + '%' ";

            sql += " ORDER BY AccountName ";

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fk_Event", eventId);
            if (!pNamePart.Equals(""))
                cmd.Parameters.AddWithValue("@pNamePart", pNamePart);

            var da = new SqlDataAdapter(cmd);

            var dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            conn.Close();

            return dt;
        }

        public DataTable GetDonorEventList_ByEvent(int eventId, string sort = "")
        {
            var sql = @" SELECT *,CASE WHEN Attending = 0 THEN 'No' ELSE 
				CASE WHEN WaitingList_Date IS NOT NULL THEN 'Wait List' ELSE 'Yes' END
		   END AS Attend
                                FROM DonorEventList DEL
                                LEFT JOIN EventList EL
                                ON DEL.fk_Event = EL.pk_Event
                                LEFT JOIN DonorList DL
                                ON DEL.fk_DonorList = DL.pk_DonorList
                                WHERE fk_Event=@fk_Event AND Response_Date IS NOT NULL
                                ORDER BY ";

            if (sort.Length > 0)
                sql += sort;
            else
                sql += "Response_Date DESC";



            var conn = new SqlConnection(ConnectionString);
            conn.Open();

            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fk_Event", eventId);

            var da = new SqlDataAdapter(cmd);

            var dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            conn.Close();

            return dt;
        }

        public DataTable GetDonorEventMailedTickets_ByEvent(int eventId, string sort = "")
        {
            var sql = @" SELECT *
                                FROM DonorEventList DEL
                                LEFT JOIN EventList EL
                                ON DEL.fk_Event = EL.pk_Event
                                LEFT JOIN DonorList DL
                                ON DEL.fk_DonorList = DL.pk_DonorList
                                WHERE fk_Event=@fk_Event AND Attending=1 AND TicketsMailed_Date IS NULL
                                ORDER BY ";

            if (sort.Length > 0)
                sql += sort;
            else
                sql += "Response_Date DESC";

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fk_Event", eventId);

            var da = new SqlDataAdapter(cmd);

            var dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            conn.Close();

            return dt;
        }

        public void MailCards()
        {

            var conn = new SqlConnection(ConnectionString);
            var da = new SqlDataAdapter("SELECT * FROM DonorEventList WHERE pk_DonorEventList=@pk_DonorEventList", conn);
            var param = new SqlParameter("@pk_DonorEventList", SqlDbType.Int) { Value = pk_DonorEventList };
            da.SelectCommand.Parameters.Add(param);

            var ds = new DataSet();
            da.Fill(ds);

            var dr = ds.Tables[0].Rows[0];

            if (Response_Date < DateTime.Parse("1/1/2000"))
            {
                dr["Response_Date"] = DateTime.Now;
                dr["Response_Type"] = "SPLC Admin";
            }

            dr["TicketsRequested"] = TicketsRequested;

            if (WaitingList_Date > DateTime.Parse("1/1/2000"))
            {
                dr["WaitingList_Date"] = DBNull.Value;
                dr["WaitingListOrder"] = 0;
            }

            dr["TicketsMailed_Date"] = TicketsMailed_Date;
            dr["TicketsMailed_User"] = TicketsMailed_User;

            da.Update(ds.Tables[0]);
            da.Dispose();
            conn.Close();
        }

//        public bool ValidateRegistration(string deleteId)
//        {
//            return true;
//        }

        public int GetDonorEventListID(string donorId, int eventId, bool loadFromDb)
        {
            const string sql = @"IF EXISTS (SELECT TOP 1 pk_DonorEventList FROM DonorEventList WHERE fk_Event=@fk_Event AND fk_DonorList=@fk_DonorList)
                                BEGIN (SELECT TOP 1 pk_DonorEventList FROM DonorEventList WHERE fk_Event=@fk_Event AND fk_DonorList=@fk_DonorList) END
                                ELSE BEGIN SELECT -1 END";

            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fk_Event", eventId);
            cmd.Parameters.AddWithValue("@fk_DonorList", donorId);
            var result = cmd.ExecuteScalar().ToString();
            conn.Close();

            if (loadFromDb && int.Parse(result) > 0)
            {
                pk_DonorEventList = int.Parse(result);
                Load();
            }

            return int.Parse(result);
        }

        #endregion
    }
}
