using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTS.Data;
using DTS.Models;
using DTSContext = DTS.Data.DTSContext;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace DTS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private const int _activeStatusRowID = 1;
        private const string TemplateFieldsPattern = "&lt;([#@])([/sA-Za-z_-]*)&gt;";
        private readonly DTSContext _context;


        public TemplatesController(DTSContext context)
        {
            _context = context;
        }

        // GET: api/Templates
        [HttpGet]
        public async Task<IEnumerable<AllTemplatesDTO>> GetTemplates()
        {
            

            var templates = await _context.Templates
                .Include(template => template.TemplateState)
                .Include(template => template.TemplateVersions)
                .ToListAsync();

            var templatesDTOs = new List<AllTemplatesDTO>();

            foreach (var template in templates)
            {
                templatesDTOs.Add(new AllTemplatesDTO
                {
                    ID = template.ID,
                    Name = template.Name,
                    VersionCount = template.TemplateVersions.Count,
                });
            }

            return templatesDTOs;
        }

        // GET: api/Templates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplate([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Passed negative id value");
            }
            var template = await _context.TemplateVersions
                .Include(temp => temp.User)
                .Include(temp => temp.TemplateState)
                .Where(temp => temp.TemplateID == id && temp.TemplateState.State == "Active")
                .SingleOrDefaultAsync();

            if (template == null)
            {
                return NotFound();
            }

            return Ok(template);
        }

        // GET: api/Templates/form/5
        [HttpGet("form/{id}")]
        public async Task<IActionResult> GetTemplateForm([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Passed negative id value");
            }

            var template = await _context.TemplateVersions
                .Where(temp => temp.TemplateID == id && temp.TemplateState.State == "Active")
                .SingleOrDefaultAsync();

            if (template == null)
            {
                return NotFound();
            }

            var formBase = template.TemplateVersion;

            MatchCollection matches = Regex.Matches(formBase, TemplateFieldsPattern);

            var userMatchMap = new Dictionary<string, string>();

            foreach (var match in matches)
            {
                var startPattern = "(&lt;*)";
                var endPattern = "(&gt;*)";
                var replacement = "";
                Regex beginningRegex = new Regex(startPattern);
                Regex endingRegex = new Regex(endPattern);

                var valueWithBeginningCleared = beginningRegex.Replace(match.ToString(), replacement);

                var finalValue = endingRegex.Replace(valueWithBeginningCleared, replacement);
                userMatchMap.TryAdd(match.ToString(), finalValue);
            }



            return Ok(userMatchMap);
        }

        // GET: api/Templates/5
        [HttpGet("editor/{id}")]
        public async Task<IActionResult> GetEditorsTemplates([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Passed negative id value");
            }

            var user = await _context.Users
                .Include(u => u.Type)
                .Where(u => u.ID == id)
                .SingleOrDefaultAsync();

            if (user == null || user.Type.Type != "Editor")
            {
                return BadRequest("User not found or not an editor");
            }

            var templates = await _context.TemplateVersions
                .Include(temp => temp.TemplateState)
                .Where(temp => temp.UserID == id)
                .ToListAsync();

            if (templates == null)
            {
                return NotFound();
            }



            return Ok(templates);
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

        // POST: api/Templates/form/
        [HttpPost("form/{id}")]
        public async Task<IActionResult> PostUserFilledFields([FromRoute] int id, [FromBody] object data)
        {
            var userInput = new Dictionary<string, string>();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(data), userInput);

            var template = await _context.TemplateVersions
                .Where(temp => temp.TemplateID == id && temp.TemplateState.State == "Active")
                .SingleOrDefaultAsync();
                
            foreach (var input in userInput)
            {
                template.TemplateVersion = template.TemplateVersion.Replace(input.Key, input.Value);
            }

            return Ok(template.TemplateVersion);
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