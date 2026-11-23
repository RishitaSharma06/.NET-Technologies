using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampusPortal.Data;
using SmartCampusPortal.Models;

namespace SmartCampusPortal.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusApiController : ControllerBase
    {
        private readonly CampusDbContext _context;

        public CampusApiController(CampusDbContext context)
        {
            _context = context;
        }

        // GET: api/CampusApi/tickets
        [HttpGet("tickets")]
        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetAllTickets()
        {
            return await _context.ServiceRequests
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        // GET: api/CampusApi/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var total = await _context.ServiceRequests.CountAsync();
            var open = await _context.ServiceRequests.CountAsync(t => t.Status == "Open");
            var inProgress = await _context.ServiceRequests.CountAsync(t => t.Status == "In Progress");
            var resolved = await _context.ServiceRequests.CountAsync(t => t.Status == "Resolved");

            return Ok(new { total, open, inProgress, resolved });
        }

        // PATCH: api/CampusApi/status/1
        [HttpPatch("status/{id}")]
        public async Task<IActionResult> UpdateTicketStatus(int id, [FromBody] StatusUpdateDto dto)
        {
            var ticket = await _context.ServiceRequests.FindAsync(id);
            if (ticket == null)
                return NotFound(new { message = "Ticket not found" });

            ticket.Status = dto.Status;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Status updated", ticketId = id, newStatus = ticket.Status });
        }
    }

    public class StatusUpdateDto
    {
        public string Status { get; set; } = string.Empty;
    }
}
