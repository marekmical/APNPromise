using APNPromise.Models;
using APNPromise.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APNPromise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LoginUser([FromBody] LoginRequest loginData)
        {
            var result = _accountService.LoginUser(loginData);
            if (result == null)
                return Unauthorized();
            else
                return Ok(result);
        }
    }
}
