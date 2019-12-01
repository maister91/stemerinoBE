using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectASP.Models;

namespace ProjectASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StemsController : ControllerBase
    {
        private readonly MemberContext _context;

        public StemsController(MemberContext context)
        {
            _context = context;
        }

        // GET: api/Stems
        [HttpGet]
        public IEnumerable<Stem> GetStems()
        {
            return _context.Stems;
        }

        // GET: api/Stems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStem([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stem = await _context.Stems.FindAsync(id);

            if (stem == null)
            {
                return NotFound();
            }

            return Ok(stem);
        }

        // PUT: api/Stems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStem([FromRoute] long id, [FromBody] Stem stem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stem.StemID)
            {
                return BadRequest();
            }

            _context.Entry(stem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StemExists(id))
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

        // POST: api/Stems
        [HttpPost]
        public async Task<IActionResult> PostStem([FromBody] Stem stem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Stems.Add(stem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStem", new { id = stem.StemID }, stem);
        }

        // DELETE: api/Stems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStem([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stem = await _context.Stems.FindAsync(id);
            if (stem == null)
            {
                return NotFound();
            }

            _context.Stems.Remove(stem);
            await _context.SaveChangesAsync();

            return Ok(stem);
        }

        private bool StemExists(long id)
        {
            return _context.Stems.Any(e => e.StemID == id);
        }
    }
}