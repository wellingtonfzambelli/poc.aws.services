using Microsoft.OpenApi.Models;
using poc.aws.services.api.Arguments;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace poc.aws.services.api.Filters;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasFile = context.MethodInfo
            .GetParameters()
            .Any(p => p.ParameterType == typeof(CreateProfileRequestDto));

        if (hasFile)
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = context.SchemaGenerator.GenerateSchema(typeof(CreateProfileRequestDto), context.SchemaRepository)
                    }
                }
            };
        }
    }
}