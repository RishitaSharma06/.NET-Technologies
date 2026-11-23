
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PlacementDashboard
{
    public partial class PlacementDashboard : System.Web.UI.Page
    {
        string cs = ConfigurationManager
            .ConnectionStrings["PlacementDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboard();
            }
        }

        void LoadDashboard()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Students", con);

                int total = Convert.ToInt32(cmd.ExecuteScalar());
                lblTotal.Text = total.ToString();

                cmd.CommandText =
                    "SELECT COUNT(*) FROM Students WHERE PlacementStatus='Placed'";

                int placed = Convert.ToInt32(cmd.ExecuteScalar());
                lblPlaced.Text = placed.ToString();

                cmd.CommandText = "SELECT COUNT(*) FROM Companies";

                lblCompanies.Text =
                    cmd.ExecuteScalar().ToString();

                double rate = total == 0 ? 0 :
                    (placed * 100.0 / total);

                lblRate.Text = rate.ToString("0.0") + "%";

                cmd.CommandText = "SELECT * FROM Students";

                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                gvStudents.DataSource = dt;
                gvStudents.DataBind();
            }
        }
    }
}
