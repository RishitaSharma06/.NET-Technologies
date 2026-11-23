using System.ComponentModel.DataAnnotations;

namespace SmartCampusPortal.Models
{
    public class ServiceRequest
    {
        [Key]
        public int TicketId { get; set; }

        [Required(ErrorMessage = "Student Name is required")]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enrollment Number is required")]
        [Display(Name = "Enrollment No.")]
        public string EnrollmentNo { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty; // e.g., Computer Engineering, IT

        [Required]
        public string Category { get; set; } = string.Empty;   // e.g., Wi-Fi/IT, Electrical, Hostel, Lab

        [Required]
        [StringLength(300)]
        public string Description { get; set; } = string.Empty;

        public string Priority { get; set; } = "Medium";       // Low, Medium, High

        public string Status { get; set; } = "Open";           // Open, In Progress, Resolved

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
