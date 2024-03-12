using MwTech.Blazor.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// dodane
var uri = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"] + "api/");
builder.Services.AddClient(uri);
// dodane

await builder.Build().RunAsync();
