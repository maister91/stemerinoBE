using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private IHostingEnvironment _hostEnvironment;

        public OptionsController(MemberContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostEnvironment = hostingEnvironment;
        }

        // GET: api/Options
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
            option.Votes++;
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
        [HttpPost("{uploadfile}"), DisableRequestSizeLimit]
        public ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                /*string folderName = "Upload";*/
                string angularAssetsPath = "C:/Users/Melih/Documents/Angulare/Stemerino/src/assets/images";
                /*string webRootPath = _hostEnvironment.WebRootPath;*/
                /*string newPath = Path.Combine(webRootPath, folderName);*/
                /*if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }*/
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(angularAssetsPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { fullPath });
                }
               else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // POST: api/Options
        [HttpPost]
        public async Task<IActionResult> PostOption([FromBody] Option option)
        {
            /*string folderName = "Upload";
            string webRootPath = _hostEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);*/
            string imgsrcPath = "assets/images";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!string.IsNullOrEmpty(option.imgpath))
        {
                option.imgpath = option.imgpath.Replace("C:\\fakepath", imgsrcPath);
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