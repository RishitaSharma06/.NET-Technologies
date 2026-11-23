using System;
using System.Web.UI.WebControls;

namespace PlacementDashboard
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private readonly PlacementDAL dal = new PlacementDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshDashboard();
            }
        }

        private void RefreshDashboard()
        {
            fvSummary.DataSource = dal.GetDashboardSummary();
            fvSummary.DataBind();

            rptCompanies.DataSource = dal.GetCompanySummary();
            rptCompanies.DataBind();

            BindGrid();
        }

        private void BindGrid()
        {
            gvPlacements.DataSource = dal.GetPlacements(ddlBranch.SelectedValue, ddlStatus.SelectedValue);
            gvPlacements.DataBind();
        }

        protected void FilterChanged(object sender, EventArgs e)
        {
            gvPlacements.PageIndex = 0;
            BindGrid();
        }

        protected void gvPlacements_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlacements.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gvPlacements_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            int selectedId = Convert.ToInt32(gvPlacements.SelectedDataKey.Value);
            dvPlacement.ChangeMode(DetailsViewMode.ReadOnly);
            dvPlacement.DataSource = dal.GetPlacementById(selectedId);
            dvPlacement.DataBind();
        }

        protected void gvPlacements_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPlacements.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvPlacements_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPlacements.EditIndex = -1;
            BindGrid();
        }

        protected void gvPlacements_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvPlacements.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvPlacements.Rows[e.RowIndex];

            string studentName = ((TextBox)row.Cells[0].Controls[0]).Text;
            string company = ((TextBox)row.Cells[2].Controls[0]).Text;
            string role = ((TextBox)row.Cells[3].Controls[0]).Text;
            decimal lpa = Convert.ToDecimal(((TextBox)row.Cells[4].Controls[0]).Text);
            string status = ((DropDownList)row.FindControl("ddlEditStatus")).SelectedValue;

            dal.UpdatePlacement(id, studentName, company, role, lpa, status);

            gvPlacements.EditIndex = -1;
            RefreshDashboard();
        }

        protected void gvPlacements_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvPlacements.DataKeys[e.RowIndex].Value);
            dal.DeletePlacement(id);
            RefreshDashboard();
        }

        protected void btnNewRecord_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            dvPlacement.ChangeMode(DetailsViewMode.Insert);
            dvPlacement.DataBind();
        }

        protected void dvPlacement_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            lblMessage.Text = "";
            if (e.CancelingEdit)
            {
                dvPlacement.ChangeMode(DetailsViewMode.ReadOnly);
                dvPlacement.DataSource = null;
                dvPlacement.DataBind();
                return;
            }

            dvPlacement.ChangeMode(e.NewMode);
            dvPlacement.DataBind();
        }

        protected void dvPlacement_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            string name = e.Values["StudentName"]?.ToString().Trim();
            string enrollment = e.Values["EnrollmentNo"]?.ToString().Trim();
            string branch = e.Values["Branch"]?.ToString() ?? "Computer Engineering";
            string company = e.Values["CompanyName"]?.ToString().Trim();
            string role = e.Values["Role"]?.ToString().Trim();
            string status = e.Values["Status"]?.ToString() ?? "Selected";

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(enrollment) ||
                string.IsNullOrEmpty(company) || string.IsNullOrEmpty(role))
            {
                lblMessage.Text = "Please fill in Student Name, Enrollment No, Company, and Role.";
                e.Cancel = true;
                return;
            }

            if (!decimal.TryParse(e.Values["PackageLPA"]?.ToString(), out decimal lpa))
            {
                lblMessage.Text = "Enter a valid numeric package (e.g., 12.50).";
                e.Cancel = true;
                return;
            }

            DateTime driveDate = DateTime.Today;
            string dateInput = e.Values["DriveDate"]?.ToString().Trim();
            if (!string.IsNullOrEmpty(dateInput) && !DateTime.TryParse(dateInput, out driveDate))
            {
                lblMessage.Text = "Enter Drive Date as YYYY-MM-DD (e.g., 2026-11-23).";
                e.Cancel = true;
                return;
            }

            lblMessage.Text = "";
            dal.InsertPlacement(name, enrollment, branch, company, role, lpa, status, driveDate);

            dvPlacement.ChangeMode(DetailsViewMode.ReadOnly);
            dvPlacement.DataSource = null;
            dvPlacement.DataBind();
            RefreshDashboard();
        }
    }
}
