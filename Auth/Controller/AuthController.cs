using Auth.Models.DTO;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    class AuthController : ControllerBase
    {
        private readonly IAuthServiceWrapper services;

        public AuthController(IAuthServiceWrapper services)
        {
            this.services = services;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await services.SignIn.HandleAsync(new SignInCommand(
                    form.Login,
                    form.Password,
                    form.Name,
                    form.Surname,
                    form.Email
                    ));
                return Ok();
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn() { return StatusCode(501); }
    }
}
