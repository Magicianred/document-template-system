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

namespace DTS.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
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

        // GET: api/Users
        [HttpGet]
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

        // GET: api/Users/5
        [HttpGet("{id}")]
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
            } catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetUsersByStatus(string status)
        {
            try
            {
                var query = new GetUsersByStatusQuery(status);
                var users = await userService.GetUsersByStatusQuery.HandleAsync(query);
                return Ok(users);
            } catch (InvalidOperationException)
            {
                return NotFound($"No users with {status} status or is invalid");
            }
        }

        [HttpGet("type/{type}")]
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

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeUserPersonalData([FromRoute] int id, [FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

        [HttpPut("{id}/type/{type}")]
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

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
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