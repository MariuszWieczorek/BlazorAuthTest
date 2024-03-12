using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MwTech.Application;
using MwTech.Application.Common.Interfaces;
using MwTech.Infrastructure;
using MwTech.WebApi.Extensions;
using MwTech.WebApi.Middlewares;
using NLog.Web;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// logowanie do pliku
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddNLogWeb();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});

builder.Services.AddCulture();

// Add services to the container.
// Dodajemy serwisy zdefiniowane w warstwie Application i Infrastructure
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["JWTSettings:ValidIssuer"],
        ValidAudience = builder.Configuration["JWTSettings:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:SecurityKey"]))
    };
});


// Add services to the container.

builder.Services.AddControllers();

// DefaultApiVersion - Ustawia domyślną wersję API. Zazwyczaj będzie to v1.0.
// ReportApiVersions - Raportuje obsługiwane wersje API w api-supported-versions nagłówku odpowiedzi.
// AssumeDefaultVersionWhenUnspecified - Używa, DefaultApiVersiongdy klient nie podał jawnej wersji.
// ApiVersionReader - Konfiguruje sposób odczytu wersji API określonej przez klienta.
// Wartość domyślna to QueryStringApiVersionReader.

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
})
    .AddApiExplorer(options =>
    {
           options.GroupNameFormat = "'v'VVV";
        // options.SubstituteApiVersionInUrl = true;
        //      options.FormatGroupName = (group, version) => $"{group} - {version}";
    });
    
    
   

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerBearerAuthorization();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Chcemy użyć już wstrzykniêtych implementacji.
// W metodzie rozszerzaj¹cej typ IServiceCollection
// W Warstwie Infrastructure
// Musimy utworzyæ za pomoc¹ usinga nowy scope.

using (var scope = app.Services.CreateScope())
{
    app.UseRequestLocalization(
        app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>
        ().Value);

    app.UseInfrastructure(
        scope.ServiceProvider.GetRequiredService<IApplicationDbContext>(),
        app.Services.GetService<IAppSettingsService>(),
        app.Services.GetService<IWebHostEnvironment>(),
        app.Services.GetService<IEmail>()
         );
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/Swagger/v1/swagger.json", "v1");
        options.SwaggerEndpoint($"/Swagger/v2/swagger.json", "v2");
    });
}

// Test trybu Developerskiego
var logger = app.Services.GetService<ILogger<Program>>();
if (app.Environment.IsDevelopment())
{
    logger.LogInformation("DEVELOPMENT Mode");
}
else
{
    logger.LogInformation("PRODUCTION Mode");
}



app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);

// udostępnienie zasobów statycznych
app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = "/wwwroot",
    EnableDefaultFiles = true
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

