using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MwTech.Application.Common.Behaviours;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Services.ComarchService;
using MwTech.Application.Services.IfsService;
using MwTech.Application.Services.ProductCostService;
using MwTech.Application.Services.ProductCsvService;
using MwTech.Application.Services.ProductService;
using MwTech.Application.Services.RecipeService;
using System.Reflection;

namespace MwTech.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Konfiguracja Mediatora
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        });
        

        //
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        //
        services.AddScoped<IProductCostService,ProductCostService>();
        services.AddScoped<IProductCsvService,ProductCsvService>();
        services.AddScoped<IEstimatedProductCostService,EstimatedProductCostService>();
        services.AddScoped<IProductService,ProductService>();
        services.AddScoped<IComarchService,ComarchService>();
        services.AddScoped<IIfsService,IfsService>();
        services.AddScoped<IRecipeService,RecipeService>();

        return services;
    }


}