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
        public async Task<IActionResult> SignIn() { return StatusCode(501); }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn() { return StatusCode(501); }
    }
}
