using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectASP.Models;
using ProjectASP.Services;

namespace ProjectASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikersController : Controller
    {
        private readonly MemberContext _context;
        private IUserService _userService;

        public GebruikersController(IUserService userService, MemberContext context)
        {
            _userService = userService;
            _context = context;

        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Gebruiker userParam)
        {
            var gebruiker = _userService.Authenticate(userParam.Gebruikersnaam, userParam.Wachtwoord);
            if (gebruiker == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                return Ok(gebruiker); }


        // GET: api/Gebruikers
        [HttpGet]
        public IEnumerable<Gebruiker> GetGebruikers()
        {
            return _context.Gebruikers.Include(a=> a.stems) ;
        }

        // GET: api/Gebruikers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGebruiker([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gebruiker = await _context.Gebruikers.Include(a=> a.stems).Include(a => a.pollGebruikers).FirstOrDefaultAsync( a => a.GebruikerID == id);

            if (gebruiker == null)
            {
                return NotFound();
            }

            return Ok(gebruiker);
        }

        // PUT: api/Gebruikers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGebruiker([FromRoute] long id, [FromBody] Gebruiker gebruiker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gebruiker.GebruikerID)
            {
                return BadRequest();
            }

            _context.Entry(gebruiker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GebruikerExists(id))
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

        // POST: api/Gebruikers
        [HttpPost]
        public async Task<IActionResult> PostGebruiker([FromBody] Gebruiker gebruiker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Gebruikers.Add(gebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGebruiker", new { id = gebruiker.GebruikerID }, gebruiker);
        }

        // DELETE: api/Gebruikers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGebruiker([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gebruiker = await _context.Gebruikers.FindAsync(id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            _context.Gebruikers.Remove(gebruiker);
            await _context.SaveChangesAsync();

            return Ok(gebruiker);
        }

        private bool GebruikerExists(long id)
        {
            return _context.Gebruikers.Any(e => e.GebruikerID == id);
        }
    }
}