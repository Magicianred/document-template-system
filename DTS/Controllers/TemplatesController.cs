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
            logger.LogInformation("status: {status} : {message}.",
                status,
                message
                );
        }
        private void LogWarning(string message, int status)
        {
            logger.LogWarning("status: {status} : {message}.",
                status,
                message
                );
        }

        [HttpGet]
        public async Task<IActionResult> GetTemplates()
        {
            LogBeginOfRequest();
            try
            {
                var query = new GetTemplatesQuery();
                var templates = await templateService.GetTemplatesQuery.HandleAsync(query);
                LogEndOfRequest($"Success {templates.Count} elements Found", 200);
                return Ok(templates);
            }
            catch (InvalidOperationException)
            {
                LogEndOfRequest("Failed templates list is empty", 404);
                return NotFound();
            }            
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveTemplates()
        {
            LogBeginOfRequest();
            try
            {
                var query = new GetActiveTemplatesQuery();
                var templates = await templateService.GetActiveTemplatesQuery.HandleAsync(query);
                LogEndOfRequest($"Success {templates.Count} elements Found", 200);
                return Ok(templates);
            }
            catch (InvalidOperationException)
            {
                LogEndOfRequest("Failed templates list is empty", 404);
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplate([FromRoute] int id)
        {
            LogBeginOfRequest();
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad Request", 400);
                return BadRequest(ModelState);
            }
            try
            {
                var query = new GetTemplateByIdQuery(id);
                var template = await templateService.GetTemplateByIdQuery.HandleAsync(query);
                LogEndOfRequest($"Success return {template}", 200);
                return Ok(template);
            }
            catch (KeyNotFoundException e)
            {
                LogEndOfRequest($"Failed template with id {id} not found", 404);
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
            LogBeginOfRequest();
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad Request", 400);
                return BadRequest(ModelState);
            }
            try
            {
                var query = new GetTemplatesByUserQuery(id);
                var editorTemplates = await templateService.GetTemplatesByUserQuery.HandleAsync(query);
                LogEndOfRequest($"Success {editorTemplates.Count} elements found", 200);
                return Ok(editorTemplates);
            }
            catch (KeyNotFoundException)
            {
                string errorMessage = $"User not found or not an editor.";
                LogEndOfRequest("Failed" + errorMessage, 404);
                return NotFound(errorMessage);
            }

        }

        [HttpGet("states")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserTypes()
        {
            LogBeginOfRequest();
            try
            {
                var query = new GetTemplateStatesQuery();
                var states = await templateService.GetTemplateStatesQuery.HandleAsync(query);
                LogEndOfRequest($"Success {states.Count} elements found", 200);
                return Ok(states);
            }
            catch (KeyNotFoundException)
            {
                LogEndOfRequest("Failed state list is empty", 404);
                return NotFound();
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplateData([FromRoute] int id, [FromBody] TemplateUpdateInput newTemplateData)
        {
            LogBeginOfRequest();
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad Request", 400);
                return BadRequest(ModelState);
            }

            try
            {
                var command = new UpdateTemplateDataCommand(id, newTemplateData);
                await templateService.UpdateTemplateDataCommand.HandleAsync(command);
                LogEndOfRequest("Success", 204);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                LogEndOfRequest($"Failed template with id {id} not found", 404);
                return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await repository.Templates.Exists(id))
                {
                    LogEndOfRequest($"Failed template with id {id} not found", 404);
                    return NotFound();
                }
                else
                {
                    LogWarning("Database concurency exception", 500);
                    return StatusCode(500);
                }
            }
        }

        [HttpPut("{tempId}/{verId}")]
        public async Task<IActionResult> SetActiveVersion([FromRoute] int verId, [FromRoute] int tempId)
        {
            LogBeginOfRequest();
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad Request", 400);
                return BadRequest(ModelState);
            }

            try
            {
                var command = new ActivateTemplateVersionCommand(verId, tempId);
                await templateService.ActivateTemplateVersionCommand.HandleAsync(command);
                LogEndOfRequest("Success", 200);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                LogEndOfRequest($"Failed template id {tempId} or version id {verId} not found", 404);
                return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await repository.Templates.Exists(tempId))
                {
                    LogEndOfRequest($"Failed template with id {tempId} not found", 404);
                    return NotFound();
                }
                else
                {
                    LogWarning("Database concurency exception", 500);
                    return StatusCode(500);
                }
            }
        }

        [HttpPut("template/{id}/version")]
        public async Task<IActionResult> AddNewVersion([FromRoute] int id, [FromBody] TemplateVersionInput templateInput)
        {
            LogBeginOfRequest();
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad Request", 400);
                return BadRequest(ModelState);
            }

            var command = new AddTemplateVersionCommand(id, templateInput);
            await templateService.AddTemplateVersionCommand.HandleAsync(command);
            LogEndOfRequest("Success", 204);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostTemplate([FromBody] TemplateVersionInput templateInput)
        {
            LogBeginOfRequest();
            try
            {
                if (!ModelState.IsValid)
                {
                    LogEndOfRequest("Failed Bad Request", 400);
                    return BadRequest(ModelState);
                }

                var command = new AddTemplateCommand(templateInput);
                await templateService.AddTemplateCommand.HandleAsync(command);
                LogEndOfRequest("Success", 200);
                return Ok();
            }
            catch (Exception)
            {
                LogEndOfRequest($"Failed user with id {templateInput.AuthorId} not found", 404);
                return NotFound();
            }
        }

        [HttpPost("form/{id}")]
        public async Task<IActionResult> PostUserFilledFields([FromRoute] int id, [FromBody] object data)
        {
            LogBeginOfRequest();
            try
            {
                var query = new FillInTemplateQuery(id, data);
                var filledDocument = await templateService.FillInTemplateQuery.HandleAsync(query);
                LogEndOfRequest("Success", 200);
                return Ok(filledDocument);
            }
            catch (Exception)
            {
                string errorMessage = "Template does not exist or is inactive";
                LogEndOfRequest("Failed " + errorMessage, 404);
                return BadRequest(errorMessage);
            }
        }

        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate([FromRoute] int id)
        {
            LogBeginOfRequest();
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad Request", 400);
                return BadRequest(ModelState);
            }
            try
            {
                var command = new DeactivateTemplateCommand(id);
                await templateService.DeactivateTemplateCommand.HandleAsync(command);
                LogEndOfRequest("Success", 200);
                return Ok();

            } catch (KeyNotFoundException)
            {
                LogEndOfRequest($"Failed template with id {id} not found", 404);
                return NotFound();
            }
        }

        [HttpDelete("version/{id}")]
        public async Task<IActionResult> DeleteTemplateVersion([FromRoute] int id)
        {
            LogBeginOfRequest();
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad Request", 400);
                return BadRequest(ModelState);
            }
            try
            {
                var command = new DeactivateTemplateVersionCommand(id);
                await templateService.DeactivateTemplateVersionCommand.HandleAsync(command);
                LogEndOfRequest("Success", 200);
                return Ok();
            } catch (KeyNotFoundException)
            {
                LogEndOfRequest($"Failed template version with id {id} not found", 404);
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