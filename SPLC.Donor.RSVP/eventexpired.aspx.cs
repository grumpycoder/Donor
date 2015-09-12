using System;

namespace SPLC.Donor.RSVP
{
    public partial class eventexpired : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["eid"] != null)
            {
                try
                {
                    if (Request["eid"].Equals("NULL")) return;
                    // Add image URL to the page
                    pnlContentBefore.Visible = true;
                }
                catch
                {
                    Response.Redirect("eventexpired.aspx?eid=NULL");
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx?eid=3");
        }
    }
}