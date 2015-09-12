using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SPLC.Donor.Models
{
    public class DonorMessages
    {

        #region Accessors

        /********** Table Design *************************/
        public Guid pk_Message { get; set; }
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public string MessageDescription { get; set; }
        public DateTime Date_Added { get; set; }
        public string User_Added { get; set; }
        /********** End Table Design *************************/

        private string ConnectionString { get; set; }

        #endregion

        #region Constructor

        public DonorMessages()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();
        }

        #endregion

        #region Methods

        public void AddNew()
        {

            try
            {
                var conn = new SqlConnection(ConnectionString);
                conn.Open();

                const string sql = "INSERT INTO DonorMessages  (MessageId,MessageText,MessageDescription,User_Added) VALUES (@MessageId, @MessageText, @MessageDescription,@User_Added)";
                var cmd = new SqlCommand(sql, conn);

                var param = new SqlParameter("@MessageId", SqlDbType.Int) {Value = MessageId};
                cmd.Parameters.Add(param);

                param = new SqlParameter("@MessageText", SqlDbType.NVarChar,50) {Value = MessageId};
                cmd.Parameters.Add(param);

                param = new SqlParameter("@MessageDescription", SqlDbType.NVarChar,1000) {Value = MessageDescription};
                cmd.Parameters.Add(param);

                param = new SqlParameter("@User_Added", SqlDbType.NVarChar, 50) {Value = User_Added};
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
            }
        }

        #endregion
    }
}
