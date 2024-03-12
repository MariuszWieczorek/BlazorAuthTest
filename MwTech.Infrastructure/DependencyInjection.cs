using GymManager.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Dictionaries;
using MwTech.Domain.Entities;
using MwTech.Infrastructure.Encryption;
using MwTech.Infrastructure.Identity;
using MwTech.Infrastructure.Persistence;
using MwTech.Infrastructure.Services;
using SimpleShop.Infrastructure.Services;

namespace MwTech.Infrastructure;

public static class DependencyInjection
{




    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {

        #region konfiguracja szyfrowania

        // var testKey = new KeyInfo();
        // jednorazowo z powyższej metody za pomocą debugera kopiujemy wygenerowane klucze
        // i przypisujemy je na stałe w metodzie poniżej

        var encryptionService = new EncryptionService(new KeyInfo("kk3zd3HAIZjiZnDUhuU9OMASs4eljyPBZ1WbFdgC3UE=", "4ITbLvvo3BWGObJRFH4wDg=="));

        services.AddSingleton<IEncryptionService>(encryptionService);

        #endregion

        #region Konfiguracja połączeń z bazami danych

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IOracleDbContext, OracleDbContext>();
        services.AddScoped<IComarchDbContext, ComarchDbContext>();
        services.AddScoped<IScadaIfsDbContext, ScadaIfsDbContext>();
        services.AddScoped<IQrCodeGenerator, QrCodeGenerator>();

        var mainConnectionString = configuration.GetConnectionString("LocalConnection");
        var oracleConnectionString = configuration.GetConnectionString("Oracle");
        var scadaIfsConnectionString = configuration.GetConnectionString("ScadaIfs");
        var comarchConnectionString = configuration.GetConnectionString("Comarch");

        // w przypadku szyfrowania, gdy connection string zaszyfrowany
        // var connectionString = encryptionService.Decrypt(configuration.GetConnectionString("DefaultConnection"));


        // sqlServerOptions => sqlServerOptions.CommandTimeout(60)
        // dodane aby zwiększyć timeout z domyślnych 30 sekund
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(mainConnectionString,
        sqlServerOptions => sqlServerOptions.CommandTimeout(60))
               .EnableSensitiveDataLogging());


        services.AddDbContext<OracleDbContext>(options =>
        options.UseOracle(oracleConnectionString)
               .EnableSensitiveDataLogging());


        services.AddDbContext<ScadaIfsDbContext>(options =>
        options.UseSqlServer(scadaIfsConnectionString)
               .EnableSensitiveDataLogging());

        services.AddDbContext<ComarchDbContext>(options =>
        options.UseSqlServer(comarchConnectionString)
       .EnableSensitiveDataLogging());

        #endregion

        #region Konfiguracja Identity

        //  Rejestrujemy własną klasę zastępującą domyślną
        //  odpowiedzialną za kontrolę poprawności hasła w obiekcie typu ApplicationUser

        services.AddTransient<IPasswordValidator<ApplicationUser>, CustomPasswordValidator2>();

        services.AddTransient<IUserValidator<ApplicationUser>, CustomUserValidator2>();

        //  LocalizedIdentityErrorDescriber - klasa z komunikatami walidacji 

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
       {
           options.SignIn.RequireConfirmedAccount = true;
           options.User.RequireUniqueEmail = true;
           
           //options.Lockout.AllowedForNewUsers = true;
           //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
           //options.Lockout.MaxFailedAccessAttempts = 3;

           // options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz._@";
           options.Password = new PasswordOptions
           {
               RequireDigit = false,
               RequiredLength = 5,
               RequireLowercase = false,
               RequireUppercase = false,
               RequireNonAlphanumeric = false,
           };
       })
       .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
       .AddRoleManager<RoleManager<IdentityRole>>()
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultUI()
       .AddDefaultTokenProviders();

        #endregion

        #region Policy

        services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyDict.AdminAccess, policy =>
                    policy.RequireRole(RolesDict.Administrator));

                options.AddPolicy(PolicyDict.AccountAccess, policy =>
                    policy.RequireAssertion(context =>
                        context.User.IsInRole(RolesDict.Administrator)
                        || context.User.IsInRole(RolesDict.Ksiegowy)
                        || context.User.IsInRole(RolesDict.TechnologManager)
                        || context.User.IsInRole(RolesDict.TechnologMieszanek)
                        || context.User.IsInRole(RolesDict.TechnologOpon)
                        || context.User.IsInRole(RolesDict.TechnologDetek)
                ));


                options.AddPolicy(PolicyDict.TechnologiaMieszanek, policy =>
                    policy.RequireAssertion(context =>
                        context.User.IsInRole(RolesDict.Administrator)
                        || context.User.IsInRole(RolesDict.TechnologManager)
                        || context.User.IsInRole(RolesDict.TechnologMieszanek)
                ));

                options.AddPolicy(PolicyDict.Technologia, policy =>
                    policy.RequireAssertion(context =>
                    context.User.IsInRole(RolesDict.Administrator)
                    || context.User.IsInRole(RolesDict.TechnologManager)
                    || context.User.IsInRole(RolesDict.TechnologMieszanek)
                    || context.User.IsInRole(RolesDict.TechnologDetek)
                    || context.User.IsInRole(RolesDict.TechnologOpon)
                    || context.User.IsInRole(RolesDict.TechnologPomoc)
                    || context.User.IsInRole(RolesDict.InzynierProcesu)

                ));


                options.AddPolicy(PolicyDict.ProductDetailsAccess, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(RolesDict.Administrator)
                    || context.User.IsInRole(RolesDict.TechnologManager)
                    || context.User.IsInRole(RolesDict.TechnologMieszanek)
                    || context.User.IsInRole(RolesDict.TechnologDetek)
                    || context.User.IsInRole(RolesDict.TechnologOpon)
                    || context.User.IsInRole(RolesDict.Ksiegowy)
                    || context.User.IsInRole(RolesDict.TechnologPomoc)
                    || context.User.IsInRole(RolesDict.InzynierProcesu)
                ));

                options.AddPolicy(PolicyDict.ProductModifyAccess, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(RolesDict.Administrator)
                    || context.User.IsInRole(RolesDict.TechnologManager)
                    || context.User.IsInRole(RolesDict.TechnologMieszanek)
                    || context.User.IsInRole(RolesDict.TechnologDetek)
                    || context.User.IsInRole(RolesDict.TechnologOpon)
                    || context.User.IsInRole(RolesDict.TechnologPomoc)
                    || context.User.IsInRole(RolesDict.InzynierProcesu)
                ));


                options.AddPolicy(PolicyDict.CalculateTkwAccess, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(RolesDict.Administrator)
                    || context.User.IsInRole(RolesDict.TechnologManager)
                    || context.User.IsInRole(RolesDict.TechnologMieszanek)
                    || context.User.IsInRole(RolesDict.TechnologDetek)
                    || context.User.IsInRole(RolesDict.TechnologOpon)
                    || context.User.IsInRole(RolesDict.TechnologManager)
                    || context.User.IsInRole(RolesDict.Ksiegowy)
                ));

                options.AddPolicy(PolicyDict.ScadaAccess, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(RolesDict.Administrator)
                    || context.User.IsInRole(RolesDict.TechnologManager)
                    || context.User.IsInRole(RolesDict.TechnologMieszanek)
                    || context.User.IsInRole(RolesDict.TechnologDetek)
                    || context.User.IsInRole(RolesDict.TechnologOpon)
                    || context.User.IsInRole(RolesDict.TechnologManager)
                    || context.User.IsInRole(RolesDict.TechnologPomoc)
                    || context.User.IsInRole(RolesDict.InzynierProcesu)
                ));


            });

        #endregion


        services.AddHttpContextAccessor();


        // serwis z ustawieniami aplikacji
        services.AddSingleton<IAppSettingsService, AppSettingsService>();
        services.AddSingleton<IEmail, Email>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IHttpContext, MyHttpContext>();
        services.AddScoped<IRoleManagerService, RoleManagerService>();
        services.AddScoped<IUserRoleManagerService, UserRoleManagerService>();
        services.AddScoped<IUserManagerService, UserManagerService>();
        services.AddScoped<IUserClaimManagerService, UserClaimManagerService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }


    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app,
            IApplicationDbContext context,
            IAppSettingsService appSettingsService,
            IWebHostEnvironment webHostEnvironment,
            IEmail email
        )
    {
        appSettingsService.Update(context).GetAwaiter().GetResult();
        email.Update(appSettingsService).GetAwaiter().GetResult();

        return app;
    }


}