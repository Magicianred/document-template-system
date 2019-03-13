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

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await repository.Users.FindAllUsersAsync();
                var usersDto = new List<ExtendedUserDTO>();
                foreach (var user in users)
                {
                    usersDto.Add(new ExtendedUserDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        Status = user.Status.Name,
                        Type = user.Type.Name
                    });
                }
                return Ok(usersDto);
            } catch (Exception)
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
                var user = await repository.Users.FindUserByIDAsync(id);
                var userDto = new ExtendedUserDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Status = user.Status.Name,
                    Type = user.Type.Name
                };
                return Ok(userDto);
            } catch (Exception)
            {
                return NotFound();
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

        [HttpGet("status/{type}")]
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
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            

            try
            {
                await repository.Users.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await repository.Users.Exists(id))
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

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await repository.Users.FindUserByIDAsync(id);
                
                user.Status = await repository.UserStatus.FindStatusById(3); //3 - BLOCKED
                var userDto = new ExtendedUserDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Status = user.Status.Name,
                    Type = user.Type.Name
                };

                await repository.Users.UpdateAsync(user);
                return Ok(userDto);
            } catch (Exception)
            {
                return NotFound();
            }
        }
    }
}