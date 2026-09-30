using System;

namespace AcademicLeaveSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check Cookie
            if (!IsPostBack)
            {
                if (Request.Cookies["StudentName"] != null)
                {
                    txtName.Text =
                        Request.Cookies["StudentName"].Value;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                lblMessage.Text =
                    "Please enter student name.";
                return;
            }

            // Store name in Session
            Session["StudentName"] = txtName.Text;

            // Store name in Cookie
            Response.Cookies["StudentName"].Value =
                txtName.Text;

            Response.Cookies["StudentName"].Expires =
                DateTime.Now.AddDays(7);

            Response.Redirect("Main.aspx");
        }
    }
}
