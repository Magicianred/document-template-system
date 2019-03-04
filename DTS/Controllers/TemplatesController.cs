﻿using System;
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
        private const int _activeStatusRowID = 1;
        private readonly AppContext _context;

        public TemplatesController(AppContext context)
        {
            _context = context;
        }

        // GET: api/Templates
        [HttpGet]
        public  async Task<IEnumerable<Template>> GetTemplates()
        {
            var templates = await _context.Templates
                .Include(template => template.TemplateVersions)
                    .ThenInclude(version => version.User)
                .ToListAsync();

            return templates;
        }

        // GET: api/Templates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplate([FromRoute] int id)
        {
            var template = await _context.TemplateVersions
                .Include(temp => temp.User)
                .Include(temp => temp.TemplateState)
                .Where(temp => temp.ID == id && temp.TemplateState.State == "Active")
                .SingleOrDefaultAsync();

            return Ok(template);
        }

        // PUT: api/Templates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemplate([FromRoute] int id, [FromBody] Template template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != template.ID)
            {
                return BadRequest();
            }

            _context.Entry(template).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemplateExists(id))
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
                Name = templateInput.TemplateName,
                TemplateState = _context.TemplateStates.Find(_activeStatusRowID),
            };

            _context.Templates.Add(template);

            var templateVC = new TemplateVersionControl()
            {
                TemplateVersion = templateInput.Template,
                TemplateID = template.ID,
                UserID = templateInput.AuthorId,
                TemplateState = _context.TemplateStates.Find(_activeStatusRowID),

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