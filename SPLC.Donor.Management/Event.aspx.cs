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
            if (!fuDonorFile.HasFile) return;

            var filePath = HttpContext.Current.Request.PhysicalApplicationPath + @"Uploads\" + Request["eid"];

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            var fileName = Guid.NewGuid() + ".csv";
            fuDonorFile.SaveAs(filePath + @"\" + fileName);

            try
            {
                try
                {
                    var connection = new SqlConnection(_ConnStr);
                    connection.Open();
                    var cmd = new SqlCommand("DELETE DonorListStage", connection);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("T Error: " + ex.Message);
                }

                var conn = new SqlConnection(_ConnStr);
                conn.Open();
                var da = new SqlDataAdapter("SELECT * FROM DonorListStage", conn);

                var cb = new SqlCommandBuilder(da);

                var ds = new DataSet();
                try { da.Fill(ds); }
                catch (Exception ex) { throw new Exception("DA Error: " + ex.Message); }

                // Read file
                var file = new StreamReader(filePath + @"\" + fileName);
                string line;

                var intRowCount = 1;
                while ((line = file.ReadLine()) != null)
                {

                    var intCount = line.Split((char)34).Length;

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

                    var arrLine = line.Split(",".ToCharArray());
                    var dr = ds.Tables[0].NewRow();

                    if (arrLine.Count() != 31)
                    {
                        dr["pk_DonorList"] = intRowCount.ToString();
                        dr["UploadNotes"] = "Error: Data not in the correct format.";
                    }
                    else
                    {
                        try
                        {
                            dr["pk_DonorList"] = arrLine[28].Replace(";", ",");
                            dr["DonorType"] = arrLine[2].Replace(";", ",");
                            dr["AccountType"] = arrLine[3].Replace(";", ",");
                            dr["KeyName"] = arrLine[4].Replace(";", ",");
                            dr["AccountID"] = arrLine[5].Replace(";", ",");
                            dr["InsideSal"] = arrLine[6].Replace(";", ",");
                            dr["OutSideSal"] = arrLine[7].Replace(";", ",");
                            dr["HHOutsideSal"] = arrLine[8].Replace(";", ",");
                            dr["AccountName"] = arrLine[9].Replace(";", ",");
                            dr["AddressLine1"] = arrLine[10].Replace(";", ",");
                            dr["AddressLine2"] = arrLine[11].Replace(";", ",");
                            dr["AddressLine3"] = arrLine[12].Replace(";", ",");
                            dr["AddressLine4"] = arrLine[13].Replace(";", ",");
                            dr["AddressLine5"] = arrLine[14].Replace(";", ",");
                            dr["City"] = arrLine[15].Replace(";", ",");
                            dr["State"] = arrLine[16].Replace(";", ",");
                            dr["PostCode"] = arrLine[17].Replace(";", ",");
                            dr["CountryIDTrans"] = arrLine[18].Replace(";", ",");
                            dr["StateDescription"] = arrLine[19].Replace(";", ",");
                            dr["EmailAddress"] = arrLine[20].Replace(";", ",");
                            dr["PhoneNumber"] = arrLine[21].Replace(";", ",");

                            if (!arrLine[22].Equals(""))
                                dr["SPLCLeadCouncil"] = arrLine[22];

                            if (arrLine[23].Equals(""))
                                dr["MembershipYear"] = 0;
                            else
                            {
                                var arrMYear = arrLine[23].Split("/".ToCharArray());
                                dr["MembershipYear"] = int.Parse(arrMYear[2]);
                            }

                            if (arrLine[24].Equals(""))
                                dr["YearsSince"] = 0;
                            else
                                dr["YearsSince"] = int.Parse(arrLine[24]);

                            if (arrLine[25].Equals(""))
                                dr["HPC"] = 0;
                            else
                                dr["HPC"] = float.Parse(arrLine[25].Replace("$", "").Replace(";", ","));

                            if (!arrLine[26].Equals(""))
                                dr["LastPaymentDate"] = DateTime.Parse(arrLine[26]);

                            if (arrLine[27].Equals(""))
                                dr["LastPaymentAmount"] = 0;
                            else
                                dr["LastPaymentAmount"] = float.Parse(arrLine[27].Replace("$", "").Replace(";", ","));

                            dr["SourceCode"] = arrLine[29];

                        }
                        catch (Exception ex)
                        {
                            dr["pk_DonorList"] = intRowCount.ToString();
                            dr["UploadNotes"] = "Error: " + ex.Message;
                            lblErrorMessage.Text += " | " + ex.Message;
                        }
                    }

                    ds.Tables[0].Rows.Add(dr);

                    try
                    {
                        da.Update(ds);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("c:: " + arrLine[28].Replace(";", ",") + " | " + ex.Message);
                    }

                    intRowCount += 1;
                }

                cb.Dispose();
                da.Dispose();

                var daSel = new SqlDataAdapter("SELECT * FROM DonorListStage WHERE UploadNotes IS NOT NULL", conn);
                var dsSel = new DataSet();
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

                conn.Close();
                LoadRecords();

            }
            catch (Exception ex)
            {
                lblErrorMessage.Text += " | a: " + ex.Message;
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
            eventList.Update();
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
            eventList.Update();
        }

        protected void btnUpdateYes_Click(object sender, EventArgs e)
        {
            var eventList = new EventList(User.Identity.Name, int.Parse(Request["eid"])) { HTML_Yes = txtYes.Text };
            eventList.Update();
        }

        protected void btnUpdateNo_Click(object sender, EventArgs e)
        {
            var eventList = new EventList(User.Identity.Name, int.Parse(Request["eid"]))
            {
                HTML_No = txtNo.Text
            };
            eventList.Update();
        }

        protected void btnUpdateWait_Click(object sender, EventArgs e)
        {
            var eventList = new EventList(User.Identity.Name, int.Parse(Request["eid"])) { HTML_Wait = txtWait.Text };
            eventList.Update();
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