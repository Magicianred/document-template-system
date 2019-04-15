using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTS.Auth.Helpers;
using DTS.Auth.Models.DTO;
using DTS.Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DTS.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiceWrapper services;
        private readonly ITokenHelper tokenHelper;
        private readonly IHashPassword hashHandler;
        private readonly IRequestMonitor requestMonitor;
        private readonly ICredentialsRestrictionValidation credentialsRestriction;
        private readonly ILogger logger;

        public AuthController(IAuthServiceWrapper services, IConfiguration tokenSettingsSection, IRequestMonitor monitor, ILogger<AuthController> logger)
        {
            this.services = services;
            var tokenSettings = tokenSettingsSection.Get<TokenConfig>();
            this.tokenHelper = new TokenHelper(tokenSettings.Secret, tokenSettings.ExpirationTime);
            this.hashHandler = new BCryptHash();
            this.requestMonitor = monitor;
            this.credentialsRestriction = new DefaultRestriction();
            this.logger = logger;
        }

        private void LogBeginOfRequest()
        {
            logger.LogInformation("request ip: {ip}, start request handling.", GetRequestIp());
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

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInForm form)
        {
            LogBeginOfRequest();
            if (VerifyRequestLimit())
            {
                LogEndOfRequest($"Failed request Ip: {GetRequestIp()} reached limit", 429);
                return StatusCode(429, "Reached request limit. Come back after few minutes");
            }

            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad Request", 400);
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
                    hashHandler,
                    credentialsRestriction
                    ));
                LogEndOfRequest($"Success created user {form}", 200);
                return Ok();
            }
            catch (Exception e)
            {
                LogEndOfRequest("Failed " + e.Message, 400);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] UserCredentials credentials)
        {
            LogBeginOfRequest();
            if (VerifyRequestLimit())
            {
                LogEndOfRequest($"Failed request Ip: {GetRequestIp()} reached limit", 429);
                return StatusCode(429, "Reached request limit. Come back after few minutes");
            }

            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad Request", 400);
                return BadRequest(ModelState);
            }

            try
            {
                var user = await services.Login.HandleAsync(new LoginQuery(
                    credentials.Login,
                    credentials.Password,
                    hashHandler,
                    requestMonitor
                    ));

                var tokenDTO = new Token
                {
                    Content = tokenHelper.WriteToken(tokenHelper.GetNewToken(user.Id, user.Type.Name))
                };

                LogEndOfRequest($"Success logged user {user.Id}, return token {tokenDTO.Content}", 200);
                return Ok(tokenDTO);

            }
            catch (KeyNotFoundException e)
            {
                LogEndOfRequest("Failed " + e.Message, 400);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("login")]
        public async Task<IActionResult> ChangeCredentials([FromBody] ChangeCredentialsForm changeCredentialsForm)
        {
            LogBeginOfRequest();
            if (VerifyRequestLimit())
            {
                LogEndOfRequest($"Failed request Ip: {GetRequestIp()} reached limit", 429);
                return StatusCode(429, "Reached request limit. Come back after few minutes");
            }

            if (!ModelState.IsValid)
            {
                LogEndOfRequest("Failed Bad Request", 400);
                return BadRequest(ModelState);
            }

            try
            {
                var user = await services.Login.HandleAsync(new LoginQuery(
                    changeCredentialsForm.Login,
                    changeCredentialsForm.Password,
                    hashHandler,
                    requestMonitor
                    ));

                await services.ChangeUserLoginAndPassword.HandleAsync(new ChangeUserLoginAndPasswordCommand(
                    user.Id,
                    changeCredentialsForm.NewLogin,
                    changeCredentialsForm.NewPassword,
                    hashHandler,
                    credentialsRestriction
                    ));
                LogEndOfRequest("Success", 200);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                LogEndOfRequest("Failed " + e.Message, 400);
                return BadRequest(e.Message);
            }
        }

        private bool VerifyRequestLimit()
        {
            var ip = GetRequestIp();
            return requestMonitor.VerifyRequestRateLimit(ip);
        }

        private string GetRequestIp()
        {
            return HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
