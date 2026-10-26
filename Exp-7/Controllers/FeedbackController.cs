using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeedbackApp.Data;
using FeedbackApp.Models;

namespace FeedbackApp.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Feedback
        public async Task<IActionResult> Index()
        {
            var feedbacks = await _context.Feedbacks.ToListAsync();
            return View(feedbacks);
        }

        // GET: Feedback/Create
        public IActionResult Create()
        {
            var model = new Feedback
            {
                Name = "Rishita Sharma",
                Email = "rishita.sharma129620@marwadiuniversity.ac.in",
                Rating = 5,
                Comments = "Best Faculty"
            };
            return View(model);
        }

        // POST: Feedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feedback);
        }
    }
}
