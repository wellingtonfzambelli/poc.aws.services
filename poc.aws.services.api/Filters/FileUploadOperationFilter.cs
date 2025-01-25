using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace poc.aws.services.api.Filters;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileParameters = context.ApiDescription.ParameterDescriptions
            .Where(p => p.ParameterDescriptor.ParameterType == typeof(IFormFile));

        foreach (var parameter in fileParameters)
        {
            var fileParameter = operation.Parameters.FirstOrDefault(p => p.Name == parameter.Name);
            if (fileParameter != null)
            {
                fileParameter.Schema.Type = "string";
                fileParameter.Schema.Format = "binary";
                fileParameter.Description = "Upload image file";
            }
        }
    }
}