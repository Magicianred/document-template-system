using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.Repositories;
using DTS.API.Models.DTOs;
using DTS.API.Helpers;
using System;
using DTS.API.Models;
using Microsoft.AspNetCore.Authorization;
using DTS.APi.Models;
using DTS.API.Services;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace DTS.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateService templateService;
        private readonly ILogger logger;
        private readonly IRepositoryWrapper repository;

        public TemplatesController(IRepositoryWrapper repository, ITemplateService templateService, ILogger<TemplatesController> logger)
        {
            this.repository = repository;
            this.templateService = templateService;
            this.logger = logger;
        }

        private void LogBeginOfRequest()
        {
            logger.LogInformation("User id: {userId} type: {userType}, start request handling.",
                GetUserIdFromToken(),
                GetUserTypeFromToken()
                );
        }

        private void LogEndOfRequest(string message, int status)
        {
            logger.LogInformation("status: {status} : {message}",
                status,
                message
                );
        }
        private void LogWarning(string message, int status)
        {
            logger.LogWarning("status: {status} : {message}",
                status,
                message
                );
        }

        [HttpGet]
        public async Task<IActionResult> GetTemplates()
        {
            try
            {
                var query = new GetTemplatesQuery();
                var templates = await templateService.GetTemplatesQuery.HandleAsync(query);
            
                return Ok(templates);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }            
        }


        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTemplate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var query = new GetTemplateByIdQuery(id);
                var template = await templateService.GetTemplateByIdQuery.HandleAsync(query);
                return Ok(template);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpGet("form/{id}")]
        public async Task<IActionResult> GetTemplateForm([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var query = new GetTemplateFormQuery(id);
                var templateForm = await templateService.GetTemplateFormQuery.HandleAsync(query);
                return Ok(templateForm);
         
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Template version with id = {id} does not exist");
            }
        }


        [HttpGet("editor/{id}")]
        public async Task<IActionResult> GetEditorsTemplates([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var query = new GetTemplatesByUserQuery(id);
                var editorTemplates = await templateService.GetTemplatesByUserQuery.HandleAsync(query);
                return Ok(editorTemplates);
            }
            catch (KeyNotFoundException)
            { 
                return NotFound($"User not found or not an editor.");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplateData([FromRoute] int id, [FromBody] TemplateUpdateInput newTemplateData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var command = new UpdateTemplateDataCommand(id, newTemplateData);
                await templateService.UpdateTemplateDataCommand.HandleAsync(command);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
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

        [HttpPut("{tempId}/{verId}")]
        public async Task<IActionResult> SetActiveVersion([FromRoute] int verId, [FromRoute] int tempId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var command = new ActivateTemplateVersionCommand(verId, tempId);
                await templateService.ActivateTemplateVersionCommand.HandleAsync(command);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
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

        }

        [HttpPut("template/{id}/version")]
        public async Task<IActionResult> AddNewVersion([FromRoute] int id, [FromBody] TemplateVersionInput templateInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new AddTemplateVersionCommand(id, templateInput);
            await templateService.AddTemplateVersionCommand.HandleAsync(command);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostTemplate([FromBody] TemplateVersionInput templateInput)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var command = new AddTemplateCommand(templateInput);
                await templateService.AddTemplateCommand.HandleAsync(command);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("form/{id}")]
        public async Task<IActionResult> PostUserFilledFields([FromRoute] int id, [FromBody] object data)
        {
            try
            {
                var query = new FillInTemplateQuery(id, data);
                var filledDocument = await templateService.FillInTemplateQuery.HandleAsync(query);

                return Ok(filledDocument);
            }
            catch (Exception)
            {
                return BadRequest("Template does not exist or is inactive");
            }
        }

        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var command = new DeactivateTemplateCommand(id);
                await templateService.DeactivateTemplateCommand.HandleAsync(command);
                return Ok();

            } catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("version/{id}")]
        public async Task<IActionResult> DeleteTemplateVersion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var command = new DeactivateTemplateVersionCommand(id);
                await templateService.DeactivateTemplateVersionCommand.HandleAsync(command);

                return Ok();
            } catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        private int GetUserIdFromToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var idString = claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            if (idString != null)
            {
                return int.Parse(idString);
            }
            return 0;
        }

        private string GetUserTypeFromToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var type = claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault()?.Value;
            if (type != null)
            {
                return type;
            }
            return null;
        }
    }
}