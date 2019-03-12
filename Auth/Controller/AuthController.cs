using Auth.Models.DTO;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        public async Task<IActionResult> LogIn([FromBody] UserCredentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var token = await services.Login.HandleAsync(new LoginQuery(
                    credentials.Login,
                    credentials.Password
                    ));
                if (token == null)
                {
                    return Unauthorized($"{credentials.Login} is not Active");
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                return Ok(tokenHandler.WriteToken(token));

            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
