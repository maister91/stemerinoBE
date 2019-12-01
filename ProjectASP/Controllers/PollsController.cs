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
    public class PollsController : ControllerBase
    {
        private readonly MemberContext _context;

        public PollsController(MemberContext context)
        {
            _context = context;
        }

        // GET: api/Polls
        [HttpGet]
        public IEnumerable<Poll> GetPolls()
        {
            var poll = _context.Polls.Include(b => b.options).Select(b =>
      new Poll()
      {
          PollID = b.PollID,
          Topic = b.Topic,
          Count = b.Count,
          options = b.options,

      });
            return poll;
        }

        // GET: api/Polls/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoll([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var poll = await _context.Polls.Include(b => b.options).Select(b =>
        new Poll()
        {
            PollID = b.PollID,
            Topic = b.Topic,
            Count = b.Count,
            options = b.options,
            
        }).SingleOrDefaultAsync(b => b.PollID  == id);

            // var poll = await _context.Polls.FindAsync(id);

            if (poll == null)
            {
                return NotFound();
            }

            return Ok(poll);
        }

        // PUT: api/Polls/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoll([FromRoute] int id, [FromBody] Poll poll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != poll.PollID)
            {
                return BadRequest();
            }

            _context.Entry(poll).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollExists(id))
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

        // POST: api/Polls
        [HttpPost]
        public async Task<IActionResult> PostPoll([FromBody] Poll poll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Polls.Add(poll);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoll", new { id = poll.PollID }, poll);
        }

        // DELETE: api/Polls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoll([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var poll = await _context.Polls.FindAsync(id);
            if (poll == null)
            {
                return NotFound();
            }

            _context.Polls.Remove(poll);
            await _context.SaveChangesAsync();

            return Ok(poll);
        }

        private bool PollExists(int id)
        {
            return _context.Polls.Any(e => e.PollID == id);
        }
    }
}