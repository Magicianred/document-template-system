using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTS.Auth.Helpers;
using DTS.Auth.Models.DTO;
using DTS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DTS.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiceWrapper services;
        private readonly ITokenHelper tokenHelper;

        public AuthController(IAuthServiceWrapper services, IConfiguration tokenSettingsSection)
        {
            this.services = services;
            var tokenSettings = tokenSettingsSection.Get<TokenConfig>();
            this.tokenHelper = new TokenHelper(tokenSettings.Secret, tokenSettings.ExpirationTime);
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Working...");
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
            }
            catch (Exception e)
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
                    credentials.Password,
                    tokenHelper
                    ));
                if (token == null)
                {
                    return Unauthorized($"{credentials.Login} is not Active");
                }
                return Ok(tokenHelper.WriteToken(token));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
