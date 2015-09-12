using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SPLC.Donor.Models;

namespace SPLC.Donor.Management.Report
{
    public partial class EventGuestListReport : System.Web.UI.Page
    {
        private static string _ConnStr = ConfigurationManager.ConnectionStrings["Donor_ConnStr"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpr"] = "AccountName ASC";

                if (Request["eid"] != null)
                {
                    EventList EL = new EventList(User.Identity.Name, int.Parse(Request["eid"].ToString()));

                    lblEvent.Text = EL.EventName.ToString();

                    DataBind();

                    hlExcel.NavigateUrl = "EventGuestListReport_Excel.aspx?eid=" + Request["eid"].ToString();
                }
            }
        }

        protected void gvReport_PageIndexChanging(object sender,GridViewPageEventArgs e)
        {
            DataBind();
        }

        protected void gvReport_Sorting(object sender,GridViewSortEventArgs e)
        {
            DonorEventList DEL = new DonorEventList(User.Identity.Name);
            EventList EL = new EventList(User.Identity.Name, int.Parse(Request["eid"].ToString()));

            

            string[] SortOrder = ViewState["SortExpr"].ToString().Split(' ');
            if (SortOrder[0] == e.SortExpression)
            {
                if (SortOrder[1] == "ASC")
                {
                    ViewState["SortExpr"] = e.SortExpression + " " + "DESC";
                }
                else
                {
                    ViewState["SortExpr"] = e.SortExpression + " " + "ASC";
                }
            }
            else
            {
                ViewState["SortExpr"] = e.SortExpression + " " + "ASC";
            }

            gvReport.PageIndex = 0;
            gvReport.DataSource = DEL.GetDonorEventList_ByEvent(EL.pk_Event, ViewState["SortExpr"].ToString());
            gvReport.DataBind();
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            DataBind(gvReport.PageIndex - 1);
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            DataBind(gvReport.PageIndex + 1);
        }

        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Button btnPrev = (Button)e.Row.FindControl("btnPrev");
                if (gvReport.PageIndex.Equals(0))
                    btnPrev.Visible = false;
                else
                    btnPrev.Visible = true;

                Button btnNext = (Button)e.Row.FindControl("btnNext");
                if (gvReport.PageIndex.Equals(gvReport.PageCount - 1))
                    btnNext.Visible = false;
                else
                    btnNext.Visible = true;
            }
        }

        private void DataBind(int pPageIndex = 0)
        {
            

            if(pPageIndex <= 0)
            {
                pPageIndex = 0;
            }
            else
            {
                if (pPageIndex > gvReport.PageCount)
                    pPageIndex = gvReport.PageCount;

            }

            DonorEventList DEL = new DonorEventList(User.Identity.Name);
            EventList EL = new EventList(User.Identity.Name, int.Parse(Request["eid"].ToString()));

            gvReport.PageIndex = pPageIndex;
            gvReport.DataSource = DEL.GetDonorEventList_ByEvent(EL.pk_Event, ViewState["SortExpr"].ToString());
            gvReport.DataBind();
        }

    }
}