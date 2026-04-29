using API.Data;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Services
{
    public class AuthService
    {
        private readonly AppDbContext m_appDbContext;
        private UserManager<AppUser> m_userManager;
        private SignInManager<AppUser> m_SinManager;
        private readonly IHttpContextAccessor m_httpContextAccessor;
        private readonly EmailService m_email;
        public AuthService(AppDbContext appDbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                            IHttpContextAccessor httpContextAccessor, EmailService email)
        {
            m_appDbContext = appDbContext;
            m_userManager = userManager;
            m_SinManager = signInManager;
            m_httpContextAccessor = httpContextAccessor;
            m_email = email;
        }

        //--------------------------------------------------
        // Create Invite
        //--------------------------------------------------
        public async Task<string> CreateInviteAsync(string email)
        {
            var token = Guid.NewGuid().ToString();
            var invite = new Invite()
            {
                Email = email,
                Token = token,
                IsUsed = false
            };
            await m_appDbContext.Invites.AddAsync(invite);
            await m_appDbContext.SaveChangesAsync();
            return $"http://pbapp.somee.com/register?token={token}";
        }

        //--------------------------------------------------
        // Validate Invite
        //--------------------------------------------------
        public async Task<bool> ValidateInviteAsync(string token)
        {
            return await m_appDbContext.Invites.AnyAsync(t => t.Token == token && !t.IsUsed);
        }


        //--------------------------------------------------
        // Register using Invite
        //--------------------------------------------------
        public async Task<ApiResponse<object>> RegisterAsync(string token, string password, string firstName, string lastName)
        {
            var invite = await m_appDbContext.Invites
                .FirstOrDefaultAsync(x => x.Token == token && !x.IsUsed);

            if (invite == null)
                return new ApiResponse<object>
                {
                    Success = false,
                    Error = "Invalid or used invite"
                };

            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = invite.Email,
                Email = invite.Email,
                FirstName = firstName,
                LastName = lastName
            };

            var result = await m_userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return new ApiResponse<object>
                {
                    Success = false,
                    Error = string.Join(", ", result.Errors.Select(e => e.Description))
                };

            invite.IsUsed = true;
            await m_appDbContext.SaveChangesAsync();

            return new ApiResponse<object>
            {
                Success = true
            };
        }

        //--------------------------------------------------
        // Login
        //--------------------------------------------------
        public async Task<ApiResponse<object>> LoginAsync(string email, string password)
        {
            var user = await m_userManager.FindByEmailAsync(email);

            if (user == null)
                return new ApiResponse<object>
                {
                    Success = false,
                    Error = "User not found"
                };

            var result = await m_SinManager.CheckPasswordSignInAsync(user, password, false);

            if (!result.Succeeded)
                return new ApiResponse<object>
                {
                    Success = false,
                    Error = "Invalid password"
                };
            await m_SinManager.SignInAsync(user, isPersistent: true);
            return new ApiResponse<object>
            {
                Success = true,
                Data = new
                {
                    user.Id,
                    user.Email,
                    user.UserName,
                    user.FirstName,
                    user.LastName
                }
            };
        }


        //--------------------------------------------------
        // Get Current User Id
        //--------------------------------------------------
        public string? GetUserId()
        {
            return m_httpContextAccessor.HttpContext?
                .User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        //--------------------------------------------------
        // Get Current Email
        //--------------------------------------------------
        public string? GetEmail()
        {
            return m_httpContextAccessor.HttpContext?
                .User.FindFirstValue(ClaimTypes.Email);
        }

        //--------------------------------------------------
        // Get Current Username
        //--------------------------------------------------
        public string? GetUserName()
        {
            return m_httpContextAccessor.HttpContext?
                .User.Identity?.Name;
        }
         
        //--------------------------------------------------
        // Get Current User Info (Production Ready)
        //--------------------------------------------------
        public async Task<ApiResponse<object>> GetCurrentUserAsync()
        {
            var httpContext = m_httpContextAccessor.HttpContext;

            if (httpContext?.User?.Identity == null || !httpContext.User.Identity.IsAuthenticated)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Error = "User is not authenticated"
                };
            }

            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Error = "User id not found in claims"
                };
            }

            var user = await m_userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Error = "User not found"
                };
            }

            return new ApiResponse<object>
            {
                Success = true,
                Data = new
                {
                    user.Id,
                    user.Email,
                    user.UserName,
                    user.FirstName,
                    user.LastName
                }
            };
        }
        //--------------------------------------------------
        // Logout
        //--------------------------------------------------
        public async Task<ApiResponse<object>> SignOutAsync()
        {
            await m_SinManager.SignOutAsync();

            return new ApiResponse<object>
            {
                Success = true
            };
        }

        //--------------------------------------------------
        // Generate Reset Token
        //--------------------------------------------------
        public async Task<ApiResponse<object>> ForgotPasswordAsync(string email)
        {
            var user = await m_userManager.FindByEmailAsync(email);

            if (user == null)
            {
                // security 
                return new ApiResponse<object> { Success = true };
            }

            var token = await m_userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);

         //   var resetLink = $"https://pbapp.somee.com/reset-password?email={email}&token={encodedToken}";

        
            await m_email.SendResetPasswordAsync(email, encodedToken);

            return new ApiResponse<object>
            {
                Success = true
            };
        }
        //--------------------------------------------------
        // Forget password
        //--------------------------------------------------
        public async Task<ApiResponse<object>> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await m_userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Error = "Invalid request"
                };
            }

            var decodedToken = Uri.UnescapeDataString(token);

            var result = await m_userManager.ResetPasswordAsync(user, decodedToken, newPassword);

            if (!result.Succeeded)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Error = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            return new ApiResponse<object>
            {
                Success = true
            };
        }
    }
}
