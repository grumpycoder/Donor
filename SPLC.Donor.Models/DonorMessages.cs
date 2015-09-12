using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace SPLC.Donor.Models
{
    public class DonorMessages
    {
        #region Private Variables

        private string _ConnStr = "";

        #endregion

        #region Accessors

        /********** Table Design *************************/
        public Guid pk_Message { get; set; }
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public string MessageDescription { get; set; }
        public DateTime Date_Added { get; set; }
        public string User_Added { get; set; }
        /********** End Table Design *************************/

        #endregion

        #region Constructor

        public DonorMessages(string pConnStr)
        {
            _ConnStr = pConnStr;
        }

        ~DonorMessages()
        { }

        #endregion

        #region Methods

        // Load
        // Exists
        // AddNew
        
        /// <summary>
        /// Add new message with details
        /// </summary>
        public void AddNew()
        {

            try
            {
                SqlConnection Conn = new SqlConnection(_ConnStr);
                Conn.Open();

                string strSQL = "INSERT INTO DonorMessages  (MessageId,MessageText,MessageDescription,User_Added) VALUES (@MessageId, @MessageText, @MessageDescription,@User_Added)";
                SqlCommand cmd = new SqlCommand(strSQL, Conn);

                SqlParameter param = new SqlParameter("@MessageId", SqlDbType.Int);
                param.Value = MessageId;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@MessageText", SqlDbType.NVarChar,50);
                param.Value = MessageId;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@MessageDescription", SqlDbType.NVarChar,1000);
                param.Value = MessageDescription;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@User_Added", SqlDbType.NVarChar, 50);
                param.Value = User_Added;
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
                Conn.Close();
            }
            catch
            {
            }
        }

        // Delete
        // Update

        #endregion
    }
}
