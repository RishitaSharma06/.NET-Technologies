using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PlacementDashboard
{
    public class PlacementDAL
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["PlacementConnStr"].ConnectionString;

        public DataTable GetDashboardSummary()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT 
                        COUNT(*) AS TotalDrives,
                        SUM(CASE WHEN Status = 'Selected' THEN 1 ELSE 0 END) AS TotalPlaced,
                        ISNULL(MAX(CASE WHEN Status = 'Selected' THEN PackageLPA END), 0) AS HighestPackage,
                        ISNULL(CAST(AVG(CASE WHEN Status = 'Selected' THEN PackageLPA END) AS DECIMAL(5,2)), 0) AS AvgPackage
                    FROM Placements";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetCompanySummary()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT CompanyName, COUNT(*) AS Hires, MAX(PackageLPA) AS MaxLPA
                    FROM Placements
                    WHERE Status = 'Selected'
                    GROUP BY CompanyName
                    ORDER BY MaxLPA DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetPlacements(string branchFilter = "All", string statusFilter = "All")
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Placements WHERE 1=1";
                if (branchFilter != "All") query += " AND Branch = @Branch";
                if (statusFilter != "All") query += " AND Status = @Status";
                query += " ORDER BY DriveDate DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                if (branchFilter != "All") cmd.Parameters.AddWithValue("@Branch", branchFilter);
                if (statusFilter != "All") cmd.Parameters.AddWithValue("@Status", statusFilter);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetPlacementById(int placementId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Placements WHERE PlacementId = @Id", con);
                cmd.Parameters.AddWithValue("@Id", placementId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void InsertPlacement(string name, string enrollment, string branch, string company, string role, decimal lpa, string status, DateTime driveDate)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"INSERT INTO Placements (StudentName, EnrollmentNo, Branch, CompanyName, Role, PackageLPA, Status, DriveDate)
                                 VALUES (@Name, @Enroll, @Branch, @Company, @Role, @LPA, @Status, @Date)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Enroll", enrollment);
                cmd.Parameters.AddWithValue("@Branch", branch);
                cmd.Parameters.AddWithValue("@Company", company);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@LPA", lpa);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Date", driveDate);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePlacement(int id, string studentName, string company, string role, decimal lpa, string status)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"UPDATE Placements 
                                 SET StudentName=@Name, CompanyName=@Company, Role=@Role, PackageLPA=@LPA, Status=@Status 
                                 WHERE PlacementId=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", studentName);
                cmd.Parameters.AddWithValue("@Company", company);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@LPA", lpa);
                cmd.Parameters.AddWithValue("@Status", status);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePlacement(int id)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Placements WHERE PlacementId=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
