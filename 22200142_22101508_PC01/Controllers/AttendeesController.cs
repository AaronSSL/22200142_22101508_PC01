using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC1.DOMAIN.CORE.Entities;
using PC1.DOMAIN.INFRAESTRUCTURES.Data;

namespace _22200142_22101508_PC01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        private readonly EventManagementDbContext _context;

        public AttendeesController(EventManagementDbContext context)
        {
            _context = context;
        }

        // GET: api/Attendees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendees>>> GetAttendees()
        {
            return await _context.Attendees.ToListAsync();
        }

        // GET: api/Attendees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendees>> GetAttendees(int id)
        {
            var attendees = await _context.Attendees.FindAsync(id);

            if (attendees == null)
            {
                return NotFound();
            }

            return attendees;
        }

        // PUT: api/Attendees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendees(int id, Attendees attendees)
        {
            if (id != attendees.Id)
            {
                return BadRequest();
            }

            _context.Entry(attendees).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendeesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Attendees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attendees>> PostAttendees(Attendees attendees)
        {
            _context.Attendees.Add(attendees);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendees", new { id = attendees.Id }, attendees);
        }

        // DELETE: api/Attendees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendees(int id)
        {
            var attendees = await _context.Attendees.FindAsync(id);
            if (attendees == null)
            {
                return NotFound();
            }

            _context.Attendees.Remove(attendees);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendeesExists(int id)
        {
            return _context.Attendees.Any(e => e.Id == id);
        }
    }
}
