using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampusPortal.Data;
using SmartCampusPortal.Models;

namespace SmartCampusPortal.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly CampusDbContext _context;

        public ServiceRequestsController(CampusDbContext context)
        {
            _context = context;
        }

        // GET: /ServiceRequests
        public async Task<IActionResult> Index(string? categoryFilter)
        {
            var query = _context.ServiceRequests.AsQueryable();

            if (!string.IsNullOrEmpty(categoryFilter) && categoryFilter != "All")
            {
                query = query.Where(r => r.Category == categoryFilter);
            }

            ViewBag.SelectedCategory = categoryFilter ?? "All";
            var requests = await query.OrderByDescending(r => r.CreatedAt).ToListAsync();
            return View(requests);
        }

        // GET: /ServiceRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ServiceRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequest request)
        {
            if (ModelState.IsValid)
            {
                request.CreatedAt = DateTime.Now;
                request.Status = "Open";
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // POST: /ServiceRequests/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _context.ServiceRequests.FindAsync(id);
            if (ticket != null)
            {
                _context.ServiceRequests.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
