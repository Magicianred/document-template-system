using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTS.Models;
using DTSContext = DTS.Data.DTSContext;
using DTS.Models.DTOs;
using DTS.Helpers;

namespace DTS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private const int _activeStatusRowID = 1;
        private const int _inactiveStatusRowID = 2;
        private readonly DTSContext _context;


        public TemplatesController(DTSContext context)
        {
            _context = context;
        }

        // GET: api/Templates
        [HttpGet]
        public async Task<IEnumerable<AllTemplates>> GetTemplates()
        {
            

            var templates = await _context.Templates
                .Include(template => template.TemplateState)
                .Include(template => template.TemplateVersions)
                .ToListAsync();

            var templatesDTOs = new List<AllTemplates>();

            foreach (var template in templates)
            {
                templatesDTOs.Add(new AllTemplates
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
                .Where(temp => temp.TemplateID == id && temp.TemplateState.State == "Active")
                .SingleOrDefaultAsync();

            if (template == null)
            {
                return NotFound();
            }

            return Ok(new SpecificTemplate
            {
                TemplateId = id,
                TemplateVersion = template.TemplateVersion,
                CreationTime = template.CreationData,
                CreatorName = template.User.Name + "  " +template.User.Surname,
                CreatorMail = template.User.Email
            });
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

            var userMatchMap = new TemplateParser().ParseFields(formBase);

            return Ok(userMatchMap);
        }

        

        // GET: api/editor/5
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

            // Need owner column in Template table
            var templates = await _context.Templates
                .Include(temp => temp.TemplateVersions)
                .Where(temp => temp.ID == id)
                .ToListAsync();

            if (templates == null)
            {
                return NotFound();
            }



            return Ok(templates);
        }

        // PUT: api/Templates/2
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplateData([FromRoute] int id, [FromBody] TemplateUpdateInput template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var temp = _context.Templates.FindAsync(id).Result;


            if (temp == null)
            {
                return BadRequest();
            }


            
            temp.TemplateState = await _context.TemplateStates.FindAsync(template.StateId);
            temp.Name = template.Name;

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

        // PUT: api/Templates/2/1
        [HttpPut("{tempId}/{verId}")]
        public async Task<IActionResult> SetActiveVersion([FromRoute] int verId, [FromRoute] int tempId, [FromBody] Template template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (tempId != template.ID)
            {
                return BadRequest();
            }


            foreach (var templateVersion in _context.TemplateVersions)
            {
                if (templateVersion.TemplateID == tempId && templateVersion.ID != verId)
                {
                    templateVersion.TemplateState = await _context.TemplateStates.FindAsync(_inactiveStatusRowID);
                }
                else if (templateVersion.TemplateID == tempId)
                {
                    templateVersion.TemplateState = await _context.TemplateStates.FindAsync(_inactiveStatusRowID);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemplateExists(tempId))
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

        // PUT: api/Templates/versions/2/
        [HttpPut("{id}/version")]
        public async Task<IActionResult> AddNewVersion([FromRoute] int id, [FromBody] TemplateVersionInput templateInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var templateVC = new TemplateVersionControl()
            {
                TemplateVersion = templateInput.Template,
                TemplateID = id,
                UserID = templateInput.AuthorId,
                TemplateState = _context.TemplateStates.Find(_inactiveStatusRowID),

            };
            _context.TemplateVersions.Add(templateVC);

           
            await _context.SaveChangesAsync();
            

            return NoContent();
        }

        // POST: api/Templates
        [HttpPost]
        public async Task<IActionResult> PostTemplate([FromBody] TemplateVersionInput templateInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = new Template()
            {
                Name = templateInput.TemplateName,
                TemplateState = _context.TemplateStates.Find(_inactiveStatusRowID),
            };

            _context.Templates.Add(template);

            var templateVC = new TemplateVersionControl()
            {
                TemplateVersion = templateInput.Template,
                TemplateID = template.ID,
                UserID = templateInput.AuthorId,
                TemplateState = _context.TemplateStates.Find(_inactiveStatusRowID),

            };
            _context.TemplateVersions.Add(templateVC);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemplate", new { id = templateVC.ID }, templateVC);
        }

        // POST: api/Templates/form/
        [HttpPost("form/{id}")]
        public async Task<IActionResult> PostUserFilledFields([FromRoute] int id, [FromBody] object data)
        {


            var template = await _context.TemplateVersions
                .Where(temp => temp.TemplateID == id && temp.TemplateState.State == "Active")
                .SingleOrDefaultAsync();

            template.TemplateVersion = new JsonInputParser().FillTemplateFromJson(data, template);

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

            template.TemplateState = await _context.TemplateStates.FindAsync(_inactiveStatusRowID);
            await _context.SaveChangesAsync();

            return Ok(template);
        }

        private bool TemplateExists(int id)
        {
            return _context.Templates.Any(e => e.ID == id);
        }

        [HttpDelete("version/{id}")]
        public async Task<IActionResult> DeleteTemplateVersion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = await _context.TemplateVersions.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            template.TemplateState = await _context.TemplateStates.FindAsync(_inactiveStatusRowID);
            await _context.SaveChangesAsync();

            return Ok(template);
        }

    }
}