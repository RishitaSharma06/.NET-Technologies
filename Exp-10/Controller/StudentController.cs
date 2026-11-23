using Microsoft.AspNetCore.Mvc;
using StudentCourseRegistration.Models;
namespace StudentCourseRegistration.Controllers;
public class StudentController : Controller
{
    // In-memory storage: a database is not required for this activity.
    private static readonly List<Student> Students = new();
    private static int nextId = 1;
    public IActionResult Index() {
        return View(Students);
    }
    [HttpGet]

    public IActionResult Create(){
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Student student)
    {
        if (!ModelState.IsValid){
            return View(student);
        }
        student.Id = nextId++;
        Students.Add(student);
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Details(int id)
    {
        var student = Students.FirstOrDefault(s => s.Id == id);
        if (student == null){
            return NotFound();
        }
        return View(student);
    }
}
