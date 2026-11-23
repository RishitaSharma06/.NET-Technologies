using System.ComponentModel.DataAnnotations;
namespace StudentCourseRegistration.Models;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2       and 50 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your course.")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "Course must be between 2 and 60 characters.")]
    public string Course { get; set; } = string.Empty;

    [Range(1, 8, ErrorMessage = "Semester must be between 1 and 8.")]
    public int Semester { get; set; }
}
