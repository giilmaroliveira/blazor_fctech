using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExampleSPA.Shared.Models;

namespace ExampleSPA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantsController : ControllerBase
    {
        private readonly ConsultantsContext _context;

        public ConsultantsController(ConsultantsContext context)
        {
            _context = context;
        }

        // GET: api/Consultants
        [HttpGet]
        public IEnumerable<Consultants> GetConsultants()
        {
            return _context.Consultants;
        }

        // GET: api/Consultants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultants([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consultants = await _context.Consultants.FindAsync(id);

            if (consultants == null)
            {
                return NotFound();
            }

            return Ok(consultants);
        }

        // PUT: api/Consultants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsultants([FromRoute] int id, [FromBody] Consultants consultants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != consultants.Id)
            {
                return BadRequest();
            }

            _context.Entry(consultants).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultantsExists(id))
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

        // POST: api/Consultants
        [HttpPost]
        public async Task<IActionResult> PostConsultants([FromBody] Consultants consultants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Consultants.Add(consultants);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsultants", new { id = consultants.Id }, consultants);
        }

        // DELETE: api/Consultants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultants([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consultants = await _context.Consultants.FindAsync(id);
            if (consultants == null)
            {
                return NotFound();
            }

            _context.Consultants.Remove(consultants);
            await _context.SaveChangesAsync();

            return Ok(consultants);
        }

        private bool ConsultantsExists(int id)
        {
            return _context.Consultants.Any(e => e.Id == id);
        }
    }
}