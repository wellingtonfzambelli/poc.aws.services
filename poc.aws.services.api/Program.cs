using poc.aws.services.api.Configuration;
using poc.aws.services.api.Filters;
using poc.aws.services.api.Repository.UnitOfWork;
using poc.aws.services.api.Services;
using poc.aws.services.api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDapperConfiguration(builder);
builder.Services.AddS3Configuration(builder);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddTransient<IAwsSQSService, AwsSQSService>();
builder.Services.AddTransient<IAwsS3Service, AwsS3Service>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "POC AWS Services Api"
    });

    // Enable support multipart/form-data
    c.OperationFilter<FileUploadOperationFilter>();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"POC AWS Services Api");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();