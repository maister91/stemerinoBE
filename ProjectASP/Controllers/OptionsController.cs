using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectASP.Models;

namespace ProjectASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly MemberContext _context;

        public OptionsController(MemberContext context)
        {
            _context = context;
        }

        // GET: api/Options
        [Authorize]
        [HttpGet]
        public IEnumerable<Option> GetOptions()
        {
            return _context.Options;
        }

        // GET: api/Options/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOption([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var option = await _context.Options.FindAsync(id);

            if (option == null)
            {
                return NotFound();
            }

            return Ok(option);
        }

        // PUT: api/Options/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOption([FromRoute] int id, [FromBody] Option option)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != option.OptionID)
            {
                return BadRequest();
            }

            _context.Entry(option).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OptionExists(id))
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

        // POST: api/Options
        [HttpPost]
        public async Task<IActionResult> PostOption([FromBody] Option option)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Options.Add(option);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOption", new { id = option.OptionID }, option);
        }

        // DELETE: api/Options/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOption([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var option = await _context.Options.FindAsync(id);
            if (option == null)
            {
                return NotFound();
            }

            _context.Options.Remove(option);
            await _context.SaveChangesAsync();

            return Ok(option);
        }

        private bool OptionExists(int id)
        {
            return _context.Options.Any(e => e.OptionID == id);
        }
    }
}