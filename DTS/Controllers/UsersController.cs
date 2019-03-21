using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;
using DAL.Repositories;
using DTS.API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using DTS.API.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace DTS.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryWrapper repository;
        private readonly IUserService userService;
        private readonly ILogger<UsersController> logger;

        public UsersController(IRepositoryWrapper repository, IUserService userService, ILogger<UsersController> logger)
        {
            this.repository = repository;
            this.userService = userService;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            LogBeginOfRequest();
            try
            {
                var query = new GetUsersQuery();
                var users = await userService.GetUsersQuery.HandleAsync(query);
                LogEndOfRequest($"Success {users.Count} elements found", 200);
                return Ok(users);
            }
            catch (InvalidOperationException)
            {
                LogEndOfRequest($"Failed user list is empty", 404);
                return NotFound();
            }
        }

        [HttpGet("personal/{id}")]
        public async Task<IActionResult> GetUserPersonalData([FromRoute] int id)
        {
            LogBeginOfRequest();
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Bad request", 400);
                return BadRequest(ModelState);
            }
            try
            {
                var query = new GetUserPersonalDataQuery(id);
                var user = await userService.GetUserPersonalDataQuery.HandleAsync(query);
                LogEndOfRequest($"Success return {user}", 200);
                return Ok(user);
            }
            catch (KeyNotFoundException e)
            {
                LogEndOfRequest($"Failed user with id {id} not found", 404);
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            LogBeginOfRequest();
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Bad request", 400);
                return BadRequest(ModelState);
            }
            try
            {
                var query = new GetUserByIdQuery(id);
                var user = await userService.GetUserByIdQuery.HandleAsync(query);
                LogEndOfRequest($"Success return {user}", 200);
                return Ok(user);
            }
            catch (KeyNotFoundException e)
            {
                LogEndOfRequest($"Failed user with id {id} not found", 404);
                return NotFound(e.Message);
            }
        }

        [HttpGet("status/{status}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersByStatus(string status)
        {
            LogBeginOfRequest();
            try
            {
                var query = new GetUsersByStatusQuery(status);
                var users = await userService.GetUsersByStatusQuery.HandleAsync(query);
                LogEndOfRequest($"Success {users.Count} elements found", 200);
                return Ok(users);
            }
            catch (InvalidOperationException)
            {
                string errorMessage = $"No users with {status} status or is invalid";
                LogEndOfRequest("Failed " + errorMessage, 404);
                return NotFound(errorMessage);
            }
        }

        [HttpGet("type/{type}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> GetUsersByType(string type)
        {
            LogBeginOfRequest();
            try
            {
                var query = new GetUsersByTypeQuery(type);
                var users = await userService.GetUsersByTypeQuery.HandleAsync(query);
                LogEndOfRequest($"Success {users.Count} elements found", 200);
                return Ok(users);
            }
            catch (InvalidOperationException)
            {
                string errorMessage = $"No users with {type} type or is invalid";
                LogEndOfRequest("Failed" + errorMessage, 404);
                return NotFound(errorMessage);
            }
        }

        [HttpGet("types")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserTypes()
        {
            LogBeginOfRequest();
            try 
            {
                var query = new GetUserTypesQuery();
                var types = await userService.GetUserTypesQuery.HandleAsync(query);
                LogEndOfRequest($"Success {types.Count} elements found", 200);
                return Ok(types);
            } catch (KeyNotFoundException)
            {
                LogEndOfRequest("Failed types list is empty", 404);
                return NotFound();
            }
        }

        [HttpGet("statuses")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserStatuses()
        {
            LogBeginOfRequest();
            try
            {
                var query = new GetUserStatusesQuery();
                var statuses = await userService.GetUserStatusesQuery.HandleAsync(query);
                LogEndOfRequest($"Success {statuses.Count} elements found", 200);
                return Ok(statuses);
            }
            catch (KeyNotFoundException)
            {
                LogEndOfRequest("Failed status list is empty", 404);
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeUserPersonalData([FromRoute] int id, [FromBody] UserDTO user)
        {
            LogBeginOfRequest();
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad request", 400);
                return BadRequest(ModelState);
            }

            if (!VerifyIfUserIdEqualsTokenClaimName(id) && !IsUserAdmin())
            {
                LogWarning($"User id: {GetUserIdFromToken()} role: {GetUserTypeFromToken()}, Unauthorized attempt of changing user data", 403);
                return Forbid();
            }

            var command = new ChangeUserPersonalDataCommand(
                id,
                user.Name,
                user.Surname,
                user.Email
                );

            try
            {
                await userService.ChangeUserPersonalDataCommand.HandleAsync(command);
                LogEndOfRequest("Success", 204);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                string errorMessage = "Server overload try again later";
                LogWarning(errorMessage, 503);
                return StatusCode(503, errorMessage);
            }
            catch (KeyNotFoundException e)
            {
                LogEndOfRequest("Failed" + e.Message, 404);
                return NotFound(e.Message);
            }
        }

        private bool IsUserAdmin()
        {
            var userType = GetUserTypeFromToken();
            return userType.Equals("Admin");
        }

        private bool VerifyIfUserIdEqualsTokenClaimName(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0 || userId != id)
            {
                return false;
            }
            return true;
        }


        [HttpPut("{id}/type/{type}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUserType(int id, string type)
        {
            LogBeginOfRequest();
            var command = new ChangeUserTypeCommand(id, type);

            if (VerifyIfUserIdEqualsTokenClaimName(id))
            {
                LogEndOfRequest("Failed Bad request", 400);
                return BadRequest();
            }

            try
            {
                await userService.ChangeUserTypeCommand.HandleAsync(command);
                LogEndOfRequest("Success", 204);
                return NoContent();
            } catch (KeyNotFoundException e)
            {
                LogEndOfRequest("Failed" + e.Message, 404);
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}/activate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad request", 400);
                return BadRequest(ModelState);
            }
            try
            {
                var command = new ActivateUserCommand(id);
                await userService.ActivateUserCommand.HandleAsync(command);
                LogEndOfRequest("Success", 200);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                LogEndOfRequest($"Failed User with id {id} not found", 404);
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BlockUser([FromRoute] int id)
        {
            LogBeginOfRequest();
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad request", 400);
                return BadRequest(ModelState);
            }

            if (VerifyIfUserIdEqualsTokenClaimName(id))
            {
                LogEndOfRequest($"User id {GetUserTypeFromToken()} {GetUserTypeFromToken()}, Trying to block himself", 400);
                return BadRequest();
            }

            try
            {
                var command = new BlockUserCommand(id);
                await userService.BlockUserCommand.HandleAsync(command);
                LogEndOfRequest("Success", 200);
                return Ok();
            } catch (KeyNotFoundException)
            {
                LogEndOfRequest($"Failed User with id {id} not found", 404);
                return NotFound();
            }
        }

        [HttpDelete("{id}/perm")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            LogBeginOfRequest();
            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad request", 400);
                return BadRequest(ModelState);
            }

            try
            {
                var command = new DeleteUserCommand(id);
                await userService.DeleteUserCommand.HandleAsync(command);
                LogEndOfRequest("Success", 200);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                LogEndOfRequest($"Failed can't delete user if status is different than 'Suspended'", 400);
                return BadRequest();
            }
            catch (KeyNotFoundException)
            {
                LogEndOfRequest($"Failed User with id {id} not found", 404);
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