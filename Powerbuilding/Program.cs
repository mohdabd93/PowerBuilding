using Blazorise;
using Microsoft.AspNetCore.Components.Authorization;
using Powerbuilding.Components;
using Powerbuilding.Service;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------
// Blazor
// -----------------------------------
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// -----------------------------------
// HTTP CLIENT
// -----------------------------------
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7151/");
});

// -----------------------------------
// SERVICES
// -----------------------------------
builder.Services.AddScoped<WorkoutClientService>();
builder.Services.AddScoped<MealClientService>();
builder.Services.AddScoped<SupplementClientService>();
builder.Services.AddScoped<ExerciseClientService>();
builder.Services.AddScoped<AuthClientService>();
builder.Services.AddScoped<WeekPlanClientService>();

builder.Services.AddScoped<UserSessionService>();
builder.Services.AddScoped<AuthenticatedPage>();
builder.Services.AddScoped<ExerciseTemplateClientService>();
// -----------------------------------
// AUTH
// -----------------------------------
builder.Services.AddAuthorizationCore();
 

var app = builder.Build();

// -----------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();