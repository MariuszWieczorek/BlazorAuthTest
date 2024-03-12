using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MwTech.WebApi.Extensions.Swagger;
public class RemoveVersionFromParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters.Count > 0)
        {
            var versionParameter = operation.Parameters.SingleOrDefault(x => x.Name == "version");
            if (versionParameter != null)
            {
                operation.Parameters.Remove(versionParameter);
            }
        }
    }
}




public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
{
    private readonly IOptions<ApiExplorerOptions> _options;

    public ReplaceVersionWithExactValueInPathFilter(IOptions<ApiExplorerOptions> options)
    {
        _options = options;
    }

    protected ApiVersion DefaultApiVersion => _options.Value.DefaultApiVersion;
    protected string ApiVersionFormat => _options.Value.SubstitutionFormat;


    // path.Key  /api/v{version}/account/register => /api/v1/account/register

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths;
        swaggerDoc.Paths = new OpenApiPaths();

        foreach (var path in paths)
        {
            var oldKey = path.Key;
            var newKey = oldKey.Replace("v{version}", swaggerDoc.Info.Version);
            var value = path.Value;
            if (oldKey.Contains("v{version}"))
            {
                swaggerDoc.Paths.Add(newKey, value);
            }
            
        }

        /*
        var versionSegment = DefaultApiVersion.ToString(ApiVersionFormat);
        foreach (var apiDescription in context.ApiDescriptions)
        {
            //If the version is default remove paths like: api/v1.0/controller
            if (apiDescription.GetApiVersion() == DefaultApiVersion)
            {
                if (apiDescription.RelativePath.Contains(versionSegment))
                {
                    var path = "/" + apiDescription.RelativePath;
                    swaggerDoc.Paths.Remove(path);
                }
            }
            //If the version is not default remove paths like api/controller
            else
            {
                var match = Regex.Match(apiDescription.RelativePath, @"^api\/v\d+");

                if (!match.Success)
                {
                    var path = "/" + apiDescription.RelativePath;
                    swaggerDoc.Paths.Remove(path);
                }
            }
        }
        */

    }
}


/*
public class SwaggerDefaultValues : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var apiDescription = context.ApiDescription;
        operation.Deprecated |= apiDescription.IsDeprecated();

        foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
        {
            var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
            var response = operation.Responses[responseKey];

            foreach (var contentType in response.Content.Keys)
            {
                if (responseType.ApiResponseFormats.All(x => x.MediaType != contentType))
                {
                    response.Content.Remove(contentType);
                }
            }
        }

        if (operation.Parameters == null)
        {
            return;
        }

        foreach (var parameter in operation.Parameters)
        {
            var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

            parameter.Description ??= description.ModelMetadata?.Description;

            if (parameter.Schema.Default == null &&
                 description.DefaultValue != null &&
                 description.DefaultValue is not DBNull &&
                 description.ModelMetadata is ModelMetadata modelMetadata)
            {
                // REF: https://github.com/Microsoft/aspnet-api-versioning/issues/429#issuecomment-605402330
                string json = JsonSerializer.Serialize(description.DefaultValue, modelMetadata.ModelType);
                parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
            }

            parameter.Required |= description.IsRequired;
        }
    }
}

*/