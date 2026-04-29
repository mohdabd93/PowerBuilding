using Microsoft.JSInterop;
using System;

namespace Powerbuilding.Service
{
    public class UserSessionService
    {
        private readonly IJSRuntime m_js;

        public UserSessionService(IJSRuntime js)
        {
            m_js = js;
        }

        public string? UserId { get; private set; }
        public string? Email { get; private set; }
        public string? UserName { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }

        public bool IsLoggedIn => !string.IsNullOrEmpty(UserId);

        public event Action? OnChange;

        // -----------------------------------
        // Set User (Login)
        // -----------------------------------
        public async Task SetUser(string id, string email, string username,string fName, string lName)
        {
            UserId = id;
            Email = email;
            UserName = username;
            FirstName = fName;
            LastName = lName;

            await m_js.InvokeVoidAsync("localStorage.setItem", "uid", id);
            await m_js.InvokeVoidAsync("localStorage.setItem", "email", email);
            await m_js.InvokeVoidAsync("localStorage.setItem", "username", username);
            await m_js.InvokeVoidAsync("localStorage.setItem", "firstname", fName);
            await m_js.InvokeVoidAsync("localStorage.setItem", "lastname", lName);

            NotifyStateChanged();
        }

        // -----------------------------------
        // Load User (after refresh)
        // -----------------------------------
        public async Task LoadAsync()
        {
            UserId = await m_js.InvokeAsync<string>("localStorage.getItem", "uid");
            Email = await m_js.InvokeAsync<string>("localStorage.getItem", "email");
            UserName = await m_js.InvokeAsync<string>("localStorage.getItem", "username");

            FirstName = await m_js.InvokeAsync<string>("localStorage.getItem", "firstname");
            LastName = await m_js.InvokeAsync<string>("localStorage.getItem", "lastname");

            NotifyStateChanged();
        }

        // -----------------------------------
        // Clear (Logout)
        // -----------------------------------
        public async Task ClearAsync()
        {
            UserId = null;
            Email = null;
            UserName = null;
            FirstName = null;
            LastName = null;

            await m_js.InvokeVoidAsync("localStorage.removeItem", "uid");
            await m_js.InvokeVoidAsync("localStorage.removeItem", "email");
            await m_js.InvokeVoidAsync("localStorage.removeItem", "username");
            await m_js.InvokeVoidAsync("localStorage.removeItem", "firstname");
            await m_js.InvokeVoidAsync("localStorage.removeItem", "lastname");

            NotifyStateChanged();
        }
        private void NotifyStateChanged()
            => OnChange?.Invoke();
    }
}