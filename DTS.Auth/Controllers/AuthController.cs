using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTS.Auth.Helpers;
using DTS.Auth.Models.DTO;
using DTS.Auth.Services;
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
        private readonly IRequestMonitor requestMonitor;
        private readonly ICredentialsRestrictionValidation credentialsRestriction;

        public AuthController(IAuthServiceWrapper services, IConfiguration tokenSettingsSection, IRequestMonitor monitor)
        {
            this.services = services;
            var tokenSettings = tokenSettingsSection.Get<TokenConfig>();
            this.tokenHelper = new TokenHelper(tokenSettings.Secret, tokenSettings.ExpirationTime);
            this.requestMonitor = monitor;
            this.credentialsRestriction = new DefaultRestriction();
        }
        
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInForm form)
        {
            if (VerifyRequestLimit())
            {
                return StatusCode(429, "Reached request limit. Come back after few minutes");
            }

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
                    form.Email,
                    credentialsRestriction
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
            if (VerifyRequestLimit())
            {
                return StatusCode(429, "Reached request limit. Come back after few minutes");
            } 

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await services.Login.HandleAsync(new LoginQuery(
                    credentials.Login,
                    credentials.Password,
                    requestMonitor
                    ));

                var tokenDTO = new Token
                {
                    Content = tokenHelper.WriteToken(tokenHelper.GetNewToken(user.Id, user.Type.Name))
                };

                return Ok(tokenDTO);

            }
            catch (KeyNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPut("login")]
        public async Task<IActionResult> ChangeCredentials([FromBody] ChangeCredentialsForm changeCredentialsForm)
        {
            if (VerifyRequestLimit())
            {
                return StatusCode(429, "Reached request limit. Come back after few minutes");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await services.Login.HandleAsync(new LoginQuery(
                    changeCredentialsForm.Login,
                    changeCredentialsForm.Password,
                    requestMonitor
                    ));

                await services.ChangeUserLoginAndPassword.HandleAsync(new ChangeUserLoginAndPasswordCommand(
                    user.Id,
                    changeCredentialsForm.NewLogin,
                    changeCredentialsForm.NewPassword,
                    credentialsRestriction
                    ));
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Unauthorized(e.Message);
            }
        }

    private bool VerifyRequestLimit()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            return requestMonitor.VerifyRequestRateLimit(ip);
        }
    }
}
