using Microsoft.EntityFrameworkCore;
using SmartCampusPortal.Models;

namespace SmartCampusPortal.Data
{
    public class CampusDbContext : DbContext
    {
        public CampusDbContext(DbContextOptions<CampusDbContext> options) : base(options)
        {
        }

        public DbSet<ServiceRequest> ServiceRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial campus tickets so the dashboard isn't empty on first run
            modelBuilder.Entity<ServiceRequest>().HasData(
                new ServiceRequest
                {
                    TicketId = 1,
                    StudentName = "Rishita Sharma",
                    EnrollmentNo = "92400103310",
                    Department = "Computer Engineering",
                    Category = "Lab Equipment",
                    Description = "Cisco Packet Tracer lab PC-14 Ethernet port not detecting link.",
                    Priority = "High",
                    Status = "Open",
                    CreatedAt = DateTime.Now.AddHours(-3)
                },
                new ServiceRequest
                {
                    TicketId = 2,
                    StudentName = "Trishaala Sharma",
                    EnrollmentNo = "92400103309",
                    Department = "Computer Engineering",
                    Category = "Wi-Fi / Network",
                    Description = "Campus Wi-Fi dropping frequently in Block B Seminar Hall.",
                    Priority = "Medium",
                    Status = "In Progress",
                    CreatedAt = DateTime.Now.AddHours(-6)
                }
            );
        }
    }
}
