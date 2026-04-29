
namespace API.Controllers
{
    using API.Services;
    using Microsoft.AspNetCore.Identity.Data;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService m_authService;

        public AuthController(AuthService authService)
        {
            m_authService = authService;
        }

        //--------------------------------------------------
        // Create Invite
        //--------------------------------------------------
        [HttpPost("invite")]
        public async Task<IActionResult> CreateInvite([FromBody] string email)
        {
            var link = await m_authService.CreateInviteAsync(email);
            return Ok(new { link });
        }

        //--------------------------------------------------
        // Validate Invite
        //--------------------------------------------------
        [HttpGet("validate")]
        public async Task<IActionResult> ValidateInvite([FromQuery] string token)
        {
            var isValid = await m_authService.ValidateInviteAsync(token);
            return Ok(isValid);
        }

        //--------------------------------------------------
        // Register
        //--------------------------------------------------
        [HttpPost("register")]
        public async Task<IActionResult> Register(Domain.Entities.RegisterRequest request)
        {
            try
            {
                var result = await m_authService.RegisterAsync(
                    request.Token,
                    request.Password,
                    request.FirstName,
                    request.LastName
                );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //----------------------------------------------------------
        //Login
        //----------------------------------------------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await m_authService.LoginAsync(request.Email, request.Password);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        //---------------------------------------------------------
        //Logout
        //---------------------------------------------------------
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await m_authService.SignOutAsync();
            return Ok(result);
        }

        //---------------------------------------------------------
        //  
        //---------------------------------------------------------
        [HttpGet("userinfo")]
        public async Task<IActionResult> UserInfo()
        {
            var result = await m_authService.GetCurrentUserAsync();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        //---------------------------------------------------------
        //  
        //---------------------------------------------------------
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var result = await m_authService.ForgotPasswordAsync(email);
            return Ok(result);
        }

        //---------------------------------------------------------
        // Get Current User Info
        //---------------------------------------------------------
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(Domain.Entities.ResetPasswordRequest request)
        {
            var result = await m_authService.ResetPasswordAsync(
                request.Email,
                request.Token,
                request.NewPassword
            );

            return Ok(result);
        }
    }
}
 
