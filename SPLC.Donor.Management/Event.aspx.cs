using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SPLC.Donor.Models;

namespace SPLC.Donor.Management
{
    public partial class Event : Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            if (Request["eid"] == null)
            {
                lblHeader.Text = "New Event";
            }
            else
            {
                pnlAddEvent.Visible = false;
                pnlEditEvent.Visible = true;

                lblHeader.Text = "Event";

                var eventList = new EventList(User.Identity.Name, int.Parse(Request["eid"]));
                hfPK.Value = eventList.pk_Event.ToString();
                txtEventName.Text = eventList.EventName;
                txtDisplayName.Text = eventList.DisplayName;
                txtSpeaker.Text = eventList.Speaker;
                txtEventDate.Text = eventList.StartDate.ToShortDateString();
                txtVName.Text = eventList.VenueName;
                txtVAddress.Text = eventList.VenueAddress;
                txtVCity.Text = eventList.VenueCity;
                txtVZipCode.Text = eventList.VenueZipCode;
                cbActive.Checked = eventList.Active;

                var url = Request.ServerVariables["SERVER_NAME"];

                hlTicketURL.NavigateUrl = "http://rsvp.splcenter.org/" + eventList.VenueCity;
                hlTicketURL.Text = hlTicketURL.NavigateUrl;
                hlTicketURL.Target = "_blank";


                if (url.Contains("donor.splcenter.org"))
                {
                    // Production site
                    hlEventURL.NavigateUrl = ConfigurationManager.AppSettings["EmailTemplatesURL"] +
                                             "/default.aspx?eid=" + eventList.pk_Event;
                    hlEventURL.Text = hlEventURL.NavigateUrl;
                }
                else
                {
                    // DEV or UAT site
                    hlEventURL.NavigateUrl = "http://" + url + "/rsvp/default.aspx?eid=" + eventList.pk_Event;
                    hlEventURL.Text = hlEventURL.NavigateUrl;
                }

                hlEventURL.Target = "_blank";

                ddlState.SelectedValue = eventList.VenueState;
                txtCapasity.Text = eventList.Capacity.ToString();
                txtTicketsAllowed.Text = eventList.TicketsAllowed.ToString();

                txtSTimeHour.Text = GetHour(eventList.StartDate);
                txtSTimeMin.Text = GetMinute(eventList.StartDate);
                ddlStartTime.SelectedValue = GetAMPM(eventList.StartDate);

                txtETimeHour.Text = GetHour(eventList.EndDate);
                txtETimeMin.Text = GetMinute(eventList.EndDate);
                ddlEndTime.SelectedValue = GetAMPM(eventList.EndDate);

                txtOOTimeHour.Text = GetHour(eventList.DoorsOpenDate);
                txtOOTimeMin.Text = GetMinute(eventList.DoorsOpenDate);
                ddlOOTime.SelectedValue = GetAMPM(eventList.DoorsOpenDate);

                txtOnlineCloseDate.Text = eventList.OnlineCloseDate.ToShortDateString();

                txtHeader.Text = eventList.HTML_Header;
                txtFAQ.Text = eventList.HTML_FAQ;

                txtYes.Text = eventList.HTML_Yes;
                txtNo.Text = eventList.HTML_No;
                txtWait.Text = eventList.HTML_Wait;

                imgHeader.Visible = true;
                imgHeader.ImageUrl = "ihandler.ashx?eid=" + eventList.pk_Event;
            }
        }

        private string GetHour(DateTime inputDate)
        {
            var intReturn = inputDate.Hour;

            if (intReturn > 12)
                intReturn = intReturn - 12;

            if (intReturn < 10)
                return "0" + intReturn;
            else
                return intReturn.ToString();
        }

        private string GetMinute(DateTime inputDate)
        {
            if (inputDate.Minute < 10)
                return "0" + inputDate.Minute;
            else
                return inputDate.Minute.ToString();
        }

        private string GetAMPM(DateTime inputDate)
        {
            if (inputDate.Hour > 12)
                return "PM";
            else
                return "AM";
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
                    catch (Exception EX)
                    {
                        throw new Exception("T Error: " + EX.Message);
                    }

                    SqlConnection Conn = new SqlConnection(_ConnStr);
                    Conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DonorListStage", Conn);

                    SqlCommandBuilder cb = new SqlCommandBuilder(da);



                    DataSet ds = new DataSet();
                    try { da.Fill(ds); }
                    catch (Exception EX) { throw new Exception("DA Error: " + EX.Message); }

                    // Read file
                    string line = "";
                    string[] arrLine;


                    StreamReader file = new StreamReader(strFilePath + @"\" + strFileName);
                    line = file.ReadLine();  // Skip the first line of column headers

                    int intRowCount = 1;
                    while ((line = file.ReadLine()) != null)
                    {

                        int intCount = line.Split((char)34).Length;
                        string[] arrComma = line.Split((char)34);

                        if (intCount > 2)
                            line = ReplaceComma(line);
                        if (intCount > 4)
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
                                dr["pk_DonorList"] = arrLine[28].ToString().Replace(";", ",");
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

                                if (!arrLine[22].ToString().Equals(""))
                                    dr["SPLCLeadCouncil"] = arrLine[22].ToString();

                                if (arrLine[23].ToString().Equals(""))
                                    dr["MembershipYear"] = 0;
                                else
                                {
                                    string[] arrMYear = arrLine[23].Split("/".ToCharArray());
                                    dr["MembershipYear"] = int.Parse(arrMYear[2].ToString());
                                }

                                if (arrLine[24].ToString().Equals(""))
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


                                dr["SourceCode"] = arrLine[29].ToString();

                                //dr["UploadNotes"] = intRowCount.ToString();
                            }
                            catch (Exception EX)
                            {
                                dr["pk_DonorList"] = intRowCount.ToString();
                                dr["UploadNotes"] = "Error: " + EX.Message;
                                lblErrorMessage.Text += " | " + EX.Message;
                            }
                        }

                        ds.Tables[0].Rows.Add(dr);

                        try
                        {
                            da.Update(ds);
                        }
                        catch (Exception EX)
                        {
                            throw new Exception("c:: " + arrLine[28].ToString().Replace(";", ",") + " | " + EX.Message);
                        }

                        intRowCount += 1;
                    }

                    try
                    {
                        //da.Update(ds);
                    }
                    catch (Exception EX)
                    {
                        throw new Exception("c:: " + EX.Message);
                    }

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

                    Conn.Close();

                    //pnlProcess.Visible = true;
                    //pnlUpload.Visible = false;
                    //pnlGrid.Visible = false;

                    LoadRecords();


                }
                catch (Exception EX)
                {
                    //lblErrorMessage.Text = EX.Message;
                    lblErrorMessage.Text += " | a: " + EX.Message;
                }
            }
        }

        private bool LoadRecords()
        {
            try
            {
                var conn = new SqlConnection(_ConnStr);
                conn.Open();
                var cmd = new SqlCommand("p_LoadDonorList", conn)
                {
                    CommandTimeout = 500,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@pk_Event", int.Parse(Request["eid"]));
                cmd.Parameters.AddWithValue("@TicketCount", 2);
                cmd.Parameters.AddWithValue("@UserAdded", User.Identity.Name);

                cmd.ExecuteNonQuery();

                conn.Close();

                lblMessage.Text = "Donor List has been successfully loaded into the Event.";
                return true;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text += " | b: " + ex.Message;
                return false;
            }
        }


        private string ReplaceComma(string pLine)
        {
            var arrComma = pLine.Split((char)34);

            var intS = arrComma[0].Length;
            var intE = arrComma[0].Length + arrComma[1].Length + 2;

            var strPre = pLine.Substring(0, intS);
            var strSub = pLine.Substring(intE, pLine.Length - intE);
            var strMid = pLine.Substring(intS + 1, intE - intS - 3);
            return strPre + strMid.Replace(",", ";") + strSub;
        }

        protected void btnUpdateHeader_Click(object sender, EventArgs e)
        {
            var eventList = new EventList(User.Identity.Name, int.Parse(Request["eid"]));
            eventList.HTML_Header = txtHeader.Text;
//            eventList.Update();
            eventList.SaveChanges();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime eDate;
                // Check if the date is valid
                try { eDate = DateTime.Parse(txtNEventDate.Text); }
                catch { throw new Exception("Event Date"); }

                if (txtNEventName.Text.Length <= 0)
                    throw new Exception("Missing Event Name");

                if (eDate < DateTime.Now)
                    throw new Exception("Cannot create an event in the past!");

                var eventList = new EventList(User.Identity.Name)
                {
                    EventName = txtNEventName.Text,
                    StartDate = eDate
                };
                eventList.AddNew();

                if (eventList.pk_Event.Equals(0))
                    throw new Exception("Error");
                else if (eventList.pk_Event.Equals(-1))
                    throw new Exception("Event Name Already Exists");
                else
                    Response.Redirect("event.aspx?eid=" + eventList.pk_Event);
            }
            catch (Exception ex)
            {
                lblError.Text = "Error Adding new Event: " + ex.Message;
            }
        }

        protected void btnUpdateFAQ_Click(object sender, EventArgs e)
        {
            var eventList = new EventList(User.Identity.Name, int.Parse(Request["eid"]));
            eventList.HTML_FAQ = txtFAQ.Text;
//            eventList.Update();
            eventList.SaveChanges();
        }

        protected void btnUpdateYes_Click(object sender, EventArgs e)
        {
            var eventList = new EventList(User.Identity.Name, int.Parse(Request["eid"])) { HTML_Yes = txtYes.Text };
            //            eventList.Update();
            eventList.SaveChanges();
        }

        protected void btnUpdateNo_Click(object sender, EventArgs e)
        {
            var eventList = new EventList(User.Identity.Name, int.Parse(Request["eid"]))
            {
                HTML_No = txtNo.Text
            };
            //            eventList.Update();
            eventList.SaveChanges();
        }

        protected void btnUpdateWait_Click(object sender, EventArgs e)
        {
            var eventList = new EventList(User.Identity.Name, int.Parse(Request["eid"])) { HTML_Wait = txtWait.Text };
            //            eventList.Update();
            eventList.SaveChanges();
        }

        protected void btnImageUpload_Click(object sender, EventArgs e)
        {
            if (!fuImage.HasFile) return;

            var eventList = new EventList(User.Identity.Name, int.Parse(Request["eid"]))
            {
                Header_Image = System.Drawing.Image.FromStream(fuImage.PostedFile.InputStream)
            };
            //            eventList.Update();
            eventList.SaveChanges();

            imgHeader.Visible = true;
            imgHeader.ImageUrl = "ihandler.ashx?eid=" + eventList.pk_Event;
        }

    }
}