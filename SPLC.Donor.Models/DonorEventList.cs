using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SPLC.Donor.Models
{
    public class DonorEventList
    {
        #region Private Variables
            private static string _ConnStr = "";
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
        public DateTime UpdatedInfoDateTime { get; set;}
        public string UpdatedInfo_User { get;set;}

        public bool IsValid { get; set; }
        
        #endregion

        #region Constructors

        public DonorEventList()
        { }

        public DonorEventList(string pConnString,string pUser)
        {
            IsValid = false;
            _ConnStr = pConnString;
            _User = pUser;
        }

        public DonorEventList(string pConnString, string pUser, int pID)
        {
            IsValid = false;
            _ConnStr = pConnString;
            _User = pUser;
            pk_DonorEventList = pID;
            Load();
        }

        #endregion

        #region Standard Methods

        public void AddNew()
        {
            string strSql = @"INSERT INTO DonorEventList (fk_Event,fk_DonorList,Date_Added,User_Added) VALUES (@fk_Event,
                                @fk_DonorList,getdate(),@User_Added); SELECT SCOPE_IDENTITY()";

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlCommand cmd = new SqlCommand(strSql, Conn);
            cmd.Parameters.AddWithValue("@fk_Event", fk_Event);
            cmd.Parameters.AddWithValue("@fk_DonorList", fk_DonorList);
            cmd.Parameters.AddWithValue("@User_Added", _User);
            string strES = cmd.ExecuteScalar().ToString();
            Conn.Close();

            if (int.Parse(strES) > 0)
            {
                pk_DonorEventList = int.Parse(strES);
                Load();
            }
        }


        public void Create()
        {
            string sql = @"INSERT INTO DonorEventList (fk_Event, fk_DonorList, Date_Added, User_Added, TicketsRequested, WaitingListOrder, Attending, UpdatedInfo) " + 
                            "VALUES (@fk_Event, @fk_DonorList, getdate(), @User_Added, 0, 0, 0, 0); SELECT SCOPE_IDENTITY()";

            var conn = new SqlConnection(_ConnStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fk_Event", fk_Event);
            cmd.Parameters.AddWithValue("@fk_DonorList", fk_DonorList);
            cmd.Parameters.AddWithValue("@User_Added", _User);
            var result = cmd.ExecuteScalar().ToString();
            conn.Close();

            if (int.Parse(result) > 0)
            {
                pk_DonorEventList = int.Parse(result);
                Load();
            }
        }


        public void Load(int pEventID,string pDonorListID)
        {
            string strSql = @"IF EXISTS(SELECT pk_DonorEventList FROM DonorEventList WHERE fk_Event=@fk_Event AND fk_DonorList=@fk_DonorList)
                                BEGIN (SELECT pk_DonorEventList FROM DonorEventList WHERE fk_Event=@fk_Event AND fk_DonorList=@fk_DonorList) END
                                ELSE BEGIN SELECT -1 END";

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlCommand cmd = new SqlCommand(strSql, Conn);
            cmd.Parameters.AddWithValue("@fk_Event", pEventID);
            cmd.Parameters.AddWithValue("@fk_DonorList", pDonorListID);
            string strES = cmd.ExecuteScalar().ToString();
            Conn.Close();

            if (int.Parse(strES) > 0)
            {
                pk_DonorEventList = int.Parse(strES);
                Load();
            }
        }

        private void Load()
        {
            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            string strSQL = "SELECT TOP 1 * FROM DonorEventList WHERE pk_DonorEventList=@pk_DonorEventList";
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            cmd.Parameters.AddWithValue("@pk_DonorEventList", pk_DonorEventList);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

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

                string strT = dr["WaitingListOrder"].ToString();

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
            Conn.Close();

        }

        public void Update()
        {

            SqlConnection Conn = new SqlConnection(_ConnStr);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DonorEventList WHERE pk_DonorEventList=@pk_DonorEventList", Conn);
            SqlParameter param = new SqlParameter("@pk_DonorEventList", SqlDbType.Int);
            param.Value = pk_DonorEventList;
            da.SelectCommand.Parameters.Add(param);

            SqlCommandBuilder db = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataRow dr = ds.Tables[0].Rows[0];

            if (Response_Date > DateTime.Parse("1/1/2000"))
                dr["Response_Date"] = Response_Date;

            dr["Response_Type"] = Response_Type;
            dr["TicketsRequested"] = TicketsRequested;

            if (WaitingList_Date > DateTime.Parse("1/1/2000"))
                dr["WaitingList_Date"] = WaitingList_Date;

            dr["WaitingListOrder"] = WaitingListOrder;
            dr["Attending"] = Attending;

            if (TicketsMailed_Date > DateTime.Parse("1/1/2000"))
                dr["TicketsMailed_Date"] = TicketsMailed_Date;

            dr["TicketsMailed_User"] = TicketsMailed_User;
            dr["DonorComments"] = DonorComments;
            dr["SPLCComments"] = SPLCComments;

            dr["UpdatedInfo"] = UpdatedInfo;

            if (UpdatedInfoDateTime > DateTime.Parse("1/1/2000"))
                dr["UpdatedInfoDateTime"] = UpdatedInfoDateTime;
            else
                dr["UpdatedInfoDateTime"] = DBNull.Value;

            dr["UpdatedInfo_User"] = UpdatedInfo_User;

            da.Update(ds.Tables[0]);
            da.Dispose();
            Conn.Close();
        }

        #endregion

        #region Custom Modules

        public bool IsRegistered(string pDonorID,int pEventID)
        {
            //string strSql = "IF EXISTS(SELECT TOP 1 pk_Event FROM EventList WHERE EventName=@EventName " +
            //    " AND convert(varchar(10), StartDate, 120) = convert(datetime,'" + pDate.ToShortDateString() + "',120)) " +
            //    " BEGIN SELECT 'True' END ELSE BEGIN SELECT 'False' END";

            //SqlConnection Conn = new SqlConnection(_ConnStr);
            //Conn.Open();
            //SqlCommand cmd = new SqlCommand(strSql, Conn);
            //cmd.Parameters.AddWithValue("@EventName", EventName);
            //string strES = cmd.ExecuteScalar().ToString();
            //Conn.Close();

            //if (strES.Equals("True"))
            //    return true;
            //else
                return false;
        }

        public bool RegisterUser()
        {

            return true;
        }

        public int GetTicketCountForEvent()
        {
            string strSql = @"SELECT CASE WHEN
                            (
                            SELECT SUM(TicketsRequested) 
                            FROM DonorEventList 
                            WHERE fk_Event=@fk_Event AND WaitingList_Date IS NULL) IS NULL THEN '0'
                            ELSE
	                            (SELECT SUM(TicketsRequested) 
                            FROM DonorEventList 
                            WHERE fk_Event=@fk_Event AND WaitingList_Date IS NULL)
                            END";

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlCommand cmd = new SqlCommand(strSql, Conn);
            cmd.Parameters.AddWithValue("@fk_Event", fk_Event);
            string strES = cmd.ExecuteScalar().ToString();
            Conn.Close();

            return int.Parse(strES);
        }

        public int GetNextWaitListNumber()
        {
            string strSql = @"SELECT MAX(WaitingListOrder) FROM DonorEventList WHERE fk_Event=@fk_Event";

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlCommand cmd = new SqlCommand(strSql, Conn);
            cmd.Parameters.AddWithValue("@fk_Event", fk_Event);
            string strES = cmd.ExecuteScalar().ToString();
            Conn.Close();

            return int.Parse(strES) + 1;
        }

        public DataTable GetRecentResponses(int pRowCount)
        {
            string strSQL = @"SELECT TOP " + pRowCount.ToString() + @" pk_DonorEventList,EventName,(CASE WHEN Attending = 1 THEN 'Yes' ELSE 'No' END) AS Attending,TicketsRequested,AccountName,Response_Date 
                                FROM DonorEventList DEL
                                LEFT JOIN EventList EL ON DEL.fk_Event = EL.pk_Event
                                LEFT JOIN DonorList DL ON DEL.fk_DonorList = DL.pk_DonorList
                                WHERE Response_Date IS NOT NULL AND TicketsMailed_Date IS NULL 
                                ORDER BY Response_Date DESC";

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL,Conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            Conn.Close();

            return dt;
        }

        public DataTable GetWaitingList_Search(string pEventID,string pDonorID,string pLastName,string pDonorType)
        {
            string strSQL = @"SELECT pk_DonorList,pk_DonorEventList,AccountName,MembershipYear,WaitingList_Date,WaitingListOrder,TicketsRequested,DonorType 
                                FROM DonorEventList DEL
                                LEFT JOIN EventList EL ON DEL.fk_Event = EL.pk_Event
                                LEFT JOIN DonorList DL ON DEL.fk_DonorList = DL.pk_DonorList
                                WHERE WaitingList_Date IS NOT NULL";

            if (!pEventID.Equals(""))
                strSQL += " AND pk_Event='" + pEventID + "' ";

            if (!pDonorID.Equals(""))
                strSQL += " AND pk_DonorList LIKE '%" + pDonorID + "%' ";

            if (!pLastName.Equals(""))
                strSQL += " AND AccountName LIKE '%" + pLastName + "%' ";

            if (!pDonorType.Equals(""))
                strSQL += " AND DonorType = '" + pDonorType + "' ";

            strSQL += " ORDER BY WaitingListOrder";

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, Conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            Conn.Close();

            return dt;
        }

        public DataTable GetDonorEventList_Search(string pEventID, string pDonorID, string pLastName,int pTopRecCount,bool pShowMailOnly)
        {
            string strSQL = @"SELECT TOP " + pTopRecCount.ToString() + @" pk_DonorEventList,pk_DonorList,AccountName,DonorType,Response_Date,MembershipYear,Attending,TicketsRequested,TicketsMailed_Date 
                                FROM DonorEventList DEL
                                LEFT JOIN EventList EL
                                ON DEL.fk_Event = EL.pk_Event
                                LEFT JOIN DonorList DL
                                ON DEL.fk_DonorList = DL.pk_DonorList
                                WHERE pk_DonorList IS NOT NULL ";
            if (pShowMailOnly)
                strSQL += @" AND TicketsMailed_Date IS NULL ";
            
            if (!pEventID.Equals(""))
                strSQL += " AND fk_Event='" + pEventID + "' ";

            if (!pDonorID.Equals(""))
                strSQL += " AND pk_DonorList LIKE '%" + pDonorID + "%' ";

            if (!pLastName.Equals(""))
                strSQL += " AND AccountName LIKE '%" + pLastName + "%' ";

            strSQL += " ORDER BY DEL.Response_Date DESC";

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, Conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            Conn.Close();

            return dt;
        }

        public DataTable GetDonorList_Search(string pEventID, string pNamePart, int pTopRecCount)
        {

            string strSQL = "SELECT ";

            if (pTopRecCount > 0)
                strSQL += " TOP " + pTopRecCount.ToString();
            
            strSQL += @" pk_DonorEventList,pk_DonorList,AccountName,DonorType,Response_Date,MembershipYear,
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
                strSQL += " AND AccountName LIKE '%' + @pNamePart + '%' ";

            strSQL += " ORDER BY AccountName ";

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            cmd.Parameters.AddWithValue("@fk_Event", pEventID);
            if (!pNamePart.Equals(""))
                cmd.Parameters.AddWithValue("@pNamePart", pNamePart);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            Conn.Close();

            return dt;
        }

        public DataTable GetDonorEventList_ByEvent(int pEventID,string pSort = "")
        {
            string strSQL = @" SELECT *,CASE WHEN Attending = 0 THEN 'No' ELSE 
				CASE WHEN WaitingList_Date IS NOT NULL THEN 'Wait List' ELSE 'Yes' END
		   END AS Attend
                                FROM DonorEventList DEL
                                LEFT JOIN EventList EL
                                ON DEL.fk_Event = EL.pk_Event
                                LEFT JOIN DonorList DL
                                ON DEL.fk_DonorList = DL.pk_DonorList
                                WHERE fk_Event=@fk_Event AND Response_Date IS NOT NULL
                                ORDER BY ";

            if(pSort.Length > 0)
                strSQL += pSort;
            else
                strSQL += "Response_Date DESC";

          

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();

            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            cmd.Parameters.AddWithValue("@fk_Event", pEventID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            Conn.Close();

            return dt;
        }

        public DataTable GetDonorEventMailedTickets_ByEvent(int pEventID, string pSort = "")
        {
            string strSQL = @" SELECT *
                                FROM DonorEventList DEL
                                LEFT JOIN EventList EL
                                ON DEL.fk_Event = EL.pk_Event
                                LEFT JOIN DonorList DL
                                ON DEL.fk_DonorList = DL.pk_DonorList
                                WHERE fk_Event=@fk_Event AND Attending=1 AND TicketsMailed_Date IS NULL
                                ORDER BY ";

            if (pSort.Length > 0)
                strSQL += pSort;
            else
                strSQL += "Response_Date DESC";



            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            cmd.Parameters.AddWithValue("@fk_Event", pEventID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            Conn.Close();

            return dt;
        }

        public void MailCards()
        {

            SqlConnection Conn = new SqlConnection(_ConnStr);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DonorEventList WHERE pk_DonorEventList=@pk_DonorEventList", Conn);
            SqlParameter param = new SqlParameter("@pk_DonorEventList", SqlDbType.Int);
            param.Value = pk_DonorEventList;
            da.SelectCommand.Parameters.Add(param);

            SqlCommandBuilder db = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataRow dr = ds.Tables[0].Rows[0];

            if(Response_Date < DateTime.Parse("1/1/2000"))
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
            Conn.Close();
        }

        public bool ValidateRegistration(string pDELID)
        {
            return true;
        }

        public int GetDonorEventListID(string pDonorID, int pEventID, bool blLoad)
        {
            string strSql = @"IF EXISTS (SELECT TOP 1 pk_DonorEventList FROM DonorEventList WHERE fk_Event=@fk_Event AND fk_DonorList=@fk_DonorList)
                                BEGIN (SELECT TOP 1 pk_DonorEventList FROM DonorEventList WHERE fk_Event=@fk_Event AND fk_DonorList=@fk_DonorList) END
                                ELSE BEGIN SELECT -1 END";

            SqlConnection Conn = new SqlConnection(_ConnStr);
            Conn.Open();
            SqlCommand cmd = new SqlCommand(strSql, Conn);
            cmd.Parameters.AddWithValue("@fk_Event", pEventID);
            cmd.Parameters.AddWithValue("@fk_DonorList", pDonorID);
            string strES = cmd.ExecuteScalar().ToString();
            Conn.Close();

            if(blLoad && int.Parse(strES) > 0)
            {
                pk_DonorEventList = int.Parse(strES);
                Load();
            }

            return int.Parse(strES);
        }

        #endregion
    }
}
