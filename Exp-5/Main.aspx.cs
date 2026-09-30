using System;
using System.Data;

namespace AcademicLeaveSystem
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check Session
            if (Session["StudentName"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                lblWelcome.Text =
                    "Welcome, " +
                    Session["StudentName"].ToString();

                CreateLeaveTable();
                ShowLeaves();
            }
        }


        // Academic Calendar
        protected void calAcademic_SelectionChanged(
            object sender, EventArgs e)
        {
            lblDate.Text =
                "Selected Date: " +
                calAcademic.SelectedDate.ToShortDateString();
        }


        // Create Leave Table
        void CreateLeaveTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Leave Date");
            dt.Columns.Add("Reason");
            dt.Columns.Add("Leave Type");
            dt.Columns.Add("Status");

            Session["Leaves"] = dt;
        }


        // Apply Leave
        protected void btnApply_Click(
            object sender, EventArgs e)
        {
            if (txtLeaveDate.Text == "")
            {
                lblMessage.Text =
                    "Please select leave date.";

                return;
            }

            if (txtReason.Text.Trim() == "")
            {
                lblMessage.Text =
                    "Please enter reason.";

                return;
            }

            DataTable dt =
                (DataTable)Session["Leaves"];

            DataRow row = dt.NewRow();

            row["Leave Date"] =
                txtLeaveDate.Text;

            row["Reason"] =
                txtReason.Text;

            row["Leave Type"] =
                rblLeaveType.SelectedValue;

            row["Status"] =
                "Pending";

            dt.Rows.Add(row);

            Session["Leaves"] = dt;

            ShowLeaves();

            lblMessage.Text =
                "Leave applied successfully.";

            txtLeaveDate.Text = "";
            txtReason.Text = "";
        }


        // Display Leave Records
        void ShowLeaves()
        {
            DataTable dt =
                (DataTable)Session["Leaves"];

            gvLeave.DataSource = dt;
            gvLeave.DataBind();
        }


        // Logout
        protected void btnLogout_Click(
            object sender, EventArgs e)
        {
            Session.Clear();

            Response.Redirect("Login.aspx");
        }
    }
}
