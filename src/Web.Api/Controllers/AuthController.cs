using Core.Application.DTOs;
using Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("signin")]
        public async Task<ActionResult<AuthenticationResponse>> SignInAsync(SignInRequest request)
        {
            try
            {
                // Authenticate user and generate authentication token
                bool succeeded = await _authService.SignInAsync(request);

                if (!succeeded)
                {
                    // To do: display error messages
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("signup")]
        public async Task<ActionResult<AuthenticationResponse>> SignUpAsync(SignUpRequest request)
        {
            try
            {
                // Register new user
                AuthenticationResponse response = await _authService.SignUpAsync(request);

                if (response == null || !response.Succeeded)
                {
                    // To do: display error messages
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("signout")]
        public async Task<ActionResult> SignOutAsync()
        {
            // Log out user
            await _authService.SignOutAsync();

            return NoContent();
        }

        [HttpPost("reset")]
        public async Task<ActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            try
            {
                // Send password reset request for user
                AuthenticationResponse response = await _authService.ResetPasswordAsync(request);

                if (response == null || !response.Succeeded)
                {
                    return BadRequest();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("confirm")]
        public async Task<ActionResult> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            try
            {
                // Confirm email address of user
                await _authService.ConfirmEmailAsync(request);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/auth/whoami
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(User.Identity.Name);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}