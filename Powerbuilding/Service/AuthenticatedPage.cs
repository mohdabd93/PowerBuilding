using Microsoft.AspNetCore.Components;
using Powerbuilding.Service;

namespace Powerbuilding.Components;

public class AuthenticatedPage : ComponentBase
{
    [Inject] public UserSessionService UserSession { get; set; } = default!;
    [Inject] public NavigationManager Nav { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await UserSession.LoadAsync();

            if (!UserSession.IsLoggedIn)
            {
                Nav.NavigateTo("/login", true);
                return;
            }

            await OnPageLoadAsync();
            StateHasChanged();
        }
    }
     
    protected virtual Task OnPageLoadAsync() => Task.CompletedTask;
}