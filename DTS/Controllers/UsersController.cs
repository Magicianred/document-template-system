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

namespace DTS.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryWrapper repository;
        private readonly IUserService userService;

        public UsersController(IRepositoryWrapper repository, IUserService userService)
        {
            this.repository = repository;
            this.userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var query = new GetUsersQuery();
                var users = await userService.GetUsersQuery.HandleAsync(query);
                return Ok(users);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var query = new GetUserByIdQuery(id);
                var user = await userService.GetUserByIdQuery.HandleAsync(query);
                return Ok(user);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("status/{status}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersByStatus(string status)
        {
            try
            {
                var query = new GetUsersByStatusQuery(status);
                var users = await userService.GetUsersByStatusQuery.HandleAsync(query);
                return Ok(users);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"No users with {status} status or is invalid");
            }
        }

        [HttpGet("type/{type}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersByType(string type)
        {
            try
            {
                var query = new GetUsersByTypeQuery(type);
                var users = await userService.GetUsersByTypeQuery.HandleAsync(query);
                return Ok(users);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"No users with {type} type or is invalid");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeUserPersonalData([FromRoute] int id, [FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!VerifyIfUserIdEqualsTokenClaimName(id))
            {
                return BadRequest();
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
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(503, "Server overload try again later");
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
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
            var command = new ChangeUserTypeCommand(id, type);
            try
            {
                await userService.ChangeUserTypeCommand.HandleAsync(command);
                return NoContent();
            } catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}/activate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var command = new ActivateUserCommand(id);
                await userService.ActivateUserCommand.HandleAsync(command);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BlockUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var command = new BlockUserCommand(id);
                await userService.BlockUserCommand.HandleAsync(command);
                return Ok();
            } catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}