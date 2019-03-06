using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTS.Models;
using DTS.Repositories;
using DTS.Models.DTOs;
using DTS.Helpers;
using System;

namespace DTS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private const int _activeStatusRowID = 1;
        private const int _inactiveStatusRowID = 2;
        private IRepositoryWrapper repository;

        public TemplatesController(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        // GET: api/Templates
        [HttpGet]
        public async Task<IActionResult> GetTemplates()
        {

            try
            {
                var templates = await repository.Templates.FindAllTemplatesAsync();

                var templatesDTOs = new List<AllTemplates>();

                foreach (var template in templates)
                {
                    templatesDTOs.Add(new AllTemplates
                    {
                        ID = template.Id,
                        Name = template.Name,
                        VersionCount = (await repository.TemplatesVersions.FindByTemplateIdAsync(template.Id))
                            .Count()
                    });
                }
                return Ok(templatesDTOs);
            }
            catch (Exception)
            {
                return StatusCode(500, "Some Error in FindAll Templates");
            }            
        }

        // GET: api/Templates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplate([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Passed negative id value");
            }

            var template = await repository.Templates.FindTemplateByIDAsync(id);

            if (template == null)
            {
                return NotFound();
            }

            var templateReturnData = new SpecificTemplate
            {
                ID = template.Id,
                Name = template.Name,
                Versions = new List<SpecificTemplateVersion>()
            };

            var templates = await repository.TemplatesVersions.FindByTemplateIdAsync(template.Id);
            foreach (var tempVersion in templates)
            {
                var creator = await repository.Users.FindUserByIDAsync(tempVersion.CreatedBy);
                templateReturnData.Versions.Add(new SpecificTemplateVersion
                {
                    CreationTime = tempVersion.Date,
                    TemplateVersion = tempVersion.Template,
                    CreatorMail = creator.Email,
                    CreatorName = creator.Name + " " + creator.Surname
                });
            }

            return Ok(templateReturnData);
        }

        // GET: api/Templates/form/5
        [HttpGet("form/{id}")]
        public async Task<IActionResult> GetTemplateForm([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Passed negative id value");
            }

            var templates = await repository.TemplatesVersions
                .FindVersionByConditionAsync(temp => temp.TemplateId == id && temp.State.State == "Active");

            var template = templates.FirstOrDefault();
            if (template == null)
            {
                return NotFound();
            }

            var formBase = template.Template;

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

            var user = await repository.Users.FindUserByIDAsync(id);

            if (user?.Type == null || user.Type.Name != "Editor")
            {
                return BadRequest($"User not found or not an editor.");
            }

            var templates = await repository.TemplatesVersions
                .FindByUserIdAsync(id);
            if (templates.FirstOrDefault() == null)
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

            var temp = await repository.Templates.FindTemplateByIDAsync(id);

            if (temp == null)
            {
                return BadRequest();
            }

            temp.State = await repository.TemplateState.FindStateByIdAsync(template.StateId);
            temp.Name = template.Name;

            try
            {
                await repository.Templates.UpdateAsync(temp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await repository.Templates.Exists(id))
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
        public async Task<IActionResult> SetActiveVersion([FromRoute] int verId, [FromRoute] int tempId, [FromBody] TemplateUpdateInput template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var templateVersions = await repository.TemplatesVersions.FindAllVersions();
            foreach (var templateVersion in templateVersions)
            {
                if (templateVersion.TemplateId == tempId && templateVersion.Id != verId)
                {
                    templateVersion.State = await repository.TemplateState.FindStateByIdAsync(_inactiveStatusRowID);
                }
                else if (templateVersion.TemplateId == tempId)
                {
                    templateVersion.State = await repository.TemplateState.FindStateByIdAsync(_activeStatusRowID);
                }
            }

            try
            {
                await repository.TemplatesVersions.UpdateAsync(templateVersions);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await repository.Templates.Exists(tempId))
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
                Template = templateInput.Template,
                TemplateId = id,
                CreatedBy = templateInput.AuthorId,
                State = await repository.TemplateState.FindStateByIdAsync(_inactiveStatusRowID),

            };
            await repository.TemplatesVersions.CreateAsync(templateVC);

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
                Owner = await repository.Users.FindUserByIDAsync(templateInput.AuthorId),
                State = await repository.TemplateState.FindStateByIdAsync(_inactiveStatusRowID),
            };

            await repository.Templates.CreateAsync(template);

            var templateVC = new TemplateVersionControl()
            {
                Template = templateInput.Template,
                TemplateId = template.Id,
                CreatedBy = templateInput.AuthorId,
                State = await repository.TemplateState.FindStateByIdAsync(_inactiveStatusRowID),

            };
            await repository.TemplatesVersions.CreateAsync(templateVC);

            return CreatedAtAction("GetTemplate", new { id = templateVC.Id }, templateVC);
        }

        // POST: api/Templates/form/
        [HttpPost("form/{id}")]
        public async Task<IActionResult> PostUserFilledFields([FromRoute] int id, [FromBody] object data)
        {
            var templates = await repository.TemplatesVersions
                .FindVersionByConditionAsync(temp => temp.TemplateId == id && temp.State.State == "Active");
            var template = templates.FirstOrDefault();
            if (template == null)
            {
                return BadRequest("Template does not exist or is inactive");
            }

            

            template.Template = new JsonInputParser().FillTemplateFromJson(data, template);

            return Ok(template.Template);
        }

        

        // DELETE: api/Templates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = await repository.Templates.FindTemplateByIDAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            template.State = await repository.TemplateState.FindStateByIdAsync(_inactiveStatusRowID);
            await repository.Templates.UpdateAsync(template);

            return Ok(template);
        }

        [HttpDelete("version/{id}")]
        public async Task<IActionResult> DeleteTemplateVersion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = await repository.TemplatesVersions.FindVersionByIDAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            template.State = await repository.TemplateState.FindStateByIdAsync(_inactiveStatusRowID);
            await repository.TemplatesVersions.UpdateAsync(template);

            return Ok(template);
        }

    }
}