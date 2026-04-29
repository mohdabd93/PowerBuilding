using Domain.Entities;
using System.Net.Http.Json;

namespace Powerbuilding.Service
{
    public class AuthClientService
    {
        private readonly HttpClient m_httpClient;

        public AuthClientService(IHttpClientFactory httpClientFactory)
        {
            m_httpClient = httpClientFactory.CreateClient("API");
        }

        //--------------------------------------------------
        // Create Invite
        //--------------------------------------------------
        public async Task<string?> CreateInviteAsync(string email)
        {
            var result = await m_httpClient.PostAsJsonAsync("api/Auth/invite", new { Email = email });

            if (!result.IsSuccessStatusCode)
                return null;

            var response = await result.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            return response?["link"];
        }

        //--------------------------------------------------
        // Validate Invite
        //--------------------------------------------------
        public async Task<bool> ValidateInviteAsync(string token)
        {
            return await m_httpClient.GetFromJsonAsync<bool>($"api/Auth/validate?token={token}");
        }

        //--------------------------------------------------
        // Register
        //--------------------------------------------------
        public async Task<string?> RegisterAsync(RegisterRequest model)
        {
            var result = await m_httpClient.PostAsJsonAsync("api/auth/register", model);

            if (result.IsSuccessStatusCode)
                return null;

            return await result.Content.ReadAsStringAsync();
        }

        //--------------------------------------------------
        // Login (returns full ApiResponse)
        //--------------------------------------------------
        public async Task<ApiResponse<object>?> LoginAsync(LoginRequest model)
        {
            var result = await m_httpClient.PostAsJsonAsync("api/auth/login", model);
            return await result.Content.ReadFromJsonAsync<ApiResponse<object>>();
        }

        //--------------------------------------------------
        // Logout
        //--------------------------------------------------
        public async Task<bool> LogoutAsync()
        {
            var result = await m_httpClient.PostAsync("api/auth/logout", null);
            return result.IsSuccessStatusCode;
        }

        //--------------------------------------------------
        // Get current user info
        //--------------------------------------------------
        public async Task<ApiResponse<object>?> GetUserInfoAsync()
        {
            var response = await m_httpClient.GetAsync("api/auth/userinfo");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
        }

        //--------------------------------------------------
        //Forget password
        //--------------------------------------------------
        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var res = await m_httpClient.PostAsJsonAsync("api/auth/forgot-password", email);

            return res.IsSuccessStatusCode;
        }
        //--------------------------------------------------
        //reset password
        //--------------------------------------------------
        public async Task<ApiResponse<object>> ResetPasswordAsync(ResetPasswordRequest model)
        {
            var res = await m_httpClient.PostAsJsonAsync("api/auth/reset-password", model);

            var json = await res.Content.ReadFromJsonAsync<ApiResponse<object>>();
             
            if (res.IsSuccessStatusCode && json != null)
            {
                return json;
            }
             
            if (json != null)
            {
                return json;
            }
             
            var errorText = await res.Content.ReadAsStringAsync();

            return new ApiResponse<object>
            {
                Success = false,
                Error = string.IsNullOrWhiteSpace(errorText)
                    ? "Unknown error occurred"
                    : errorText
            };
        }
    }
}