using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SPLC.Donor.Management
{
    public partial class UploadFile : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblHeader.Text = "Event File Upload";
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fuDonorFile.HasFile)
            {
                string strFilePath = HttpContext.Current.Request.PhysicalApplicationPath + @"Uploads\" + Request["eid"].ToString();

                if (!Directory.Exists(strFilePath))
                    Directory.CreateDirectory(strFilePath);

                string strFileName = Guid.NewGuid().ToString() + ".csv";
                fuDonorFile.SaveAs(strFilePath + @"\" + strFileName);

                try
                {
                    try
                    {
                        SqlConnection ConnT = new SqlConnection(_ConnStr);
                        ConnT.Open();
                        SqlCommand cmd = new SqlCommand("DELETE DonorListStage", ConnT);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        ConnT.Close();
                    }
                    catch(Exception EX)
                    {
                        throw new Exception("T Error: " + EX.Message);
                    }

                    SqlConnection Conn = new SqlConnection(_ConnStr);
                    Conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DonorListStage", Conn);
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    

                    DataSet ds = new DataSet();
                    try { da.Fill(ds); }
                    catch (Exception EX) {  throw new Exception("DA Error: " + EX.Message); }

                    // Read file
                    string line = "";
                    string[] arrLine;
                    
                    
                    StreamReader file = new StreamReader(strFilePath + @"\" + strFileName);
                    line = file.ReadLine();  // Skip the first line of column headers

                    int intRowCount = 1;
                    while((line = file.ReadLine()) != null)
                    {

                        int intCount = line.Split((char)34).Length;
                        string[] arrComma = line.Split((char)34);

                        if (intCount > 2)
                            line = ReplaceComma(line);
                        if(intCount > 4)
                            line = ReplaceComma(line);
                        if (intCount > 6)
                            line = ReplaceComma(line);
                        if (intCount > 8)
                            line = ReplaceComma(line);
                        if (intCount > 10)
                            line = ReplaceComma(line);
                        if (intCount > 12)
                            line = ReplaceComma(line);



                        arrLine = line.Split(",".ToCharArray());
                        DataRow dr = ds.Tables[0].NewRow();

                        if (arrLine.Count() != 31)
                        {
                            dr["pk_DonorList"] = intRowCount.ToString();
                            dr["UploadNotes"] = "Error: Data not in the correct format.";
                        }
                        else
                        {
                            try
                            {
                                dr["pk_DonorList"] = arrLine[28].ToString().Replace(";",",");
                                dr["DonorType"] = arrLine[2].ToString().Replace(";", ",");
                                dr["AccountType"] = arrLine[3].ToString().Replace(";", ",");
                                dr["KeyName"] = arrLine[4].ToString().Replace(";", ",");
                                dr["AccountID"] = arrLine[5].ToString().Replace(";", ",");
                                dr["InsideSal"] = arrLine[6].ToString().Replace(";", ",");
                                dr["OutSideSal"] = arrLine[7].ToString().Replace(";", ",");
                                dr["HHOutsideSal"] = arrLine[8].ToString().Replace(";", ",");
                                dr["AccountName"] = arrLine[9].ToString().Replace(";", ",");
                                dr["AddressLine1"] = arrLine[10].ToString().Replace(";", ",");
                                dr["AddressLine2"] = arrLine[11].ToString().Replace(";", ",");
                                dr["AddressLine3"] = arrLine[12].ToString().Replace(";", ",");
                                dr["AddressLine4"] = arrLine[13].ToString().Replace(";", ",");
                                dr["AddressLine5"] = arrLine[14].ToString().Replace(";", ",");
                                dr["City"] = arrLine[15].ToString().Replace(";", ",");
                                dr["State"] = arrLine[16].ToString().Replace(";", ",");
                                dr["PostCode"] = arrLine[17].ToString().Replace(";", ",");
                                dr["CountryIDTrans"] = arrLine[18].ToString().Replace(";", ",");
                                dr["StateDescription"] = arrLine[19].ToString().Replace(";", ",");
                                dr["EmailAddress"] = arrLine[20].ToString().Replace(";", ",");
                                dr["PhoneNumber"] = arrLine[21].ToString().Replace(";", ",");
                                dr["SourceCode"] = arrLine[29].ToString().Replace(";", ",");

                                if (!arrLine[22].ToString().Equals(""))
                                    dr["SPLCLeadCouncil"] = arrLine[22].ToString();                                

                                if (arrLine[23].ToString().Equals(""))
                                    dr["MembershipYear"] = 0;
                                else
                                {
                                    string[] arrMYear = arrLine[23].Split("/".ToCharArray());
                                    dr["MembershipYear"] = int.Parse(arrMYear[2].ToString());
                                }                                
                                
                                if(arrLine[24].ToString().Equals(""))
                                    dr["YearsSince"] = 0;
                                else
                                    dr["YearsSince"] = int.Parse(arrLine[24].ToString());

                                if (arrLine[25].ToString().Equals(""))
                                    dr["HPC"] = 0;
                                else
                                    dr["HPC"] = float.Parse(arrLine[25].ToString().Replace("$", "").Replace(";", ","));

                                if (!arrLine[26].ToString().Equals(""))
                                    dr["LastPaymentDate"] = DateTime.Parse(arrLine[26].ToString());

                                if (arrLine[27].ToString().Equals(""))
                                    dr["LastPaymentAmount"] = 0;
                                else
                                    dr["LastPaymentAmount"] = float.Parse(arrLine[27].ToString().Replace("$", "").Replace(";", ","));

                                
                                //dr["SourceCode"] = arrLine[29].ToString();

                                //dr["UploadNotes"] = intRowCount.ToString();
                            }
                            catch (Exception EX)
                            {
                                dr["pk_DonorList"] = intRowCount.ToString();
                                dr["UploadNotes"] = "Error: " + EX.Message;
                            }
                        }

                        ds.Tables[0].Rows.Add(dr);

                        intRowCount += 1;
                    }

                    da.Update(ds);

                    cb.Dispose();
                    da.Dispose();

                    SqlDataAdapter daSel = new SqlDataAdapter("SELECT * FROM DonorListStage WHERE UploadNotes IS NOT NULL", Conn);
                    DataSet dsSel = new DataSet();
                    daSel.Fill(dsSel);

                    if (dsSel.Tables[0].Rows.Count > 0)
                    {
                        pnlGrid.Visible = true;
                        gvErrors.DataSource = dsSel.Tables[0];
                        gvErrors.DataBind();
                    }
                    else
                    {
                        pnlGrid.Visible = false;
                    }

                    //Conn.Close();

                    pnlProcess.Visible = true;
                    pnlUpload.Visible = false;
                    pnlGrid.Visible = false;

                }
                catch(Exception EX)
                {
                    lblErrorMessage.Text = EX.Message;
                }
            }
        }


        private string ReplaceComma(string pLine)
        {
            string[] arrComma = pLine.Split((char)34);

            int intS = arrComma[0].Length;
            int intE = arrComma[0].Length + arrComma[1].Length + 2;

            string strPre = pLine.Substring(0, intS);
            string strSub = pLine.Substring(intE, pLine.Length - intE);
            string strMid = pLine.Substring(intS + 1, intE - intS - 3);
            return strPre + strMid.Replace(",", ";") + strSub;
        }

        protected void btnAddList_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection Conn = new SqlConnection(_ConnStr);
                Conn.Open();
                SqlCommand cmd = new SqlCommand("p_LoadDonorList", Conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pk_Event", int.Parse(Request["eid"].ToString()));
                cmd.Parameters.AddWithValue("@TicketCount", 2);
                cmd.Parameters.AddWithValue("@UserAdded", User.Identity.Name.ToString());

                cmd.ExecuteNonQuery();

                //Response.Redirect("Event.aspx?eid=" + Request["eid"].ToString());
                lblMessage.Text = "Donor List has been successfully loaded into the Event.";
                btnAddList.Enabled = false;
            }
            catch(Exception EX)
            {
                lblMessage.Text = EX.Message;
            }
        }

    }
}