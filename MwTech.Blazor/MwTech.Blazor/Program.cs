using MwTech.Blazor.Client;
using MwTech.Blazor.Client.Pages;
using MwTech.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// dodane 
var uri = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"] + "api/");
builder.Services.AddClient(uri);
// dodane


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MwTech.Blazor.Client._Imports).Assembly);

// app.UseStatusCodePagesWithRedirects(locationFormat: "/404");

app.Run();
