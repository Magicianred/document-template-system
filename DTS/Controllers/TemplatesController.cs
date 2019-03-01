using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTS.Data;
using DTS.Models;
using AppContext = DTS.Data.AppContext;

namespace DTS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly AppContext _context;

        public TemplatesController(AppContext context)
        {
            _context = context;
        }

        // GET: api/Templates
        [HttpGet]
        public IEnumerable<Template> GetTemplates()
        {
            return _context.Templates;
        }

        // GET: api/Templates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = await _context.Templates.FindAsync(id);

            if (template == null)
            {
                return NotFound();
            }

            return Ok(template);
        }

        // PUT: api/Templates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemplate([FromRoute] int id, [FromBody] TemplateInput templateInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = _context.Templates.SingleOrDefaultAsync(t => t.ID == id);

            if (template == null)
            {
                return BadRequest();
            }

            var templateVC = new TemplateVersionControl()
            {
                TemplateContent = templateInput.Template,
            };

            _context.TemplateVersions.Add(templateVC);
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Templates
        [HttpPost]
        public async Task<IActionResult> PostTemplate([FromBody] TemplateInput templateInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = new Template()
            {
                Name = templateInput.TemplateName
            };
            _context.Templates.Add(template);


            var templateVC = new TemplateVersionControl()
            {
                TemplateContent = templateInput.Template,
            };
            _context.TemplateVersions.Add(templateVC);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemplate", new { id = templateVC.ID }, templateVC);
        }

        // DELETE: api/Templates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = await _context.Templates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();

            return Ok(template);
        }

        private bool TemplateExists(int id)
        {
            return _context.Templates.Any(e => e.ID == id);
        }
    }
}