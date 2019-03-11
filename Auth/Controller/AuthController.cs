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
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn() { return StatusCode(501); }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn() { return StatusCode(501); }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut() { return StatusCode(501); }
    }
}
