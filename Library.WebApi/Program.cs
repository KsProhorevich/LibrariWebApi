using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Library.Application.Common.Mappings;
using Library.Persistence;
using Library.Application.Interfaces;
using Library.Application;
using Library.WebApi.Middleware;
using Notes.WebApi;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// ����������� AutoMapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ILibraryDbContext).Assembly));
});

// ����������� ���������� � ��������
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);
builder.Services.AddControllers();

// ���������� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // ��������� ������� ������ � ����� ������
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// ���������� �������������� � �������������� JWT
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Bearer", options =>
{
    options.Authority = "https://localhost:7220/";//44319
    options.Audience = "LibraryWebAPI";
    options.RequireHttpsMetadata = false;
});

// ���������� ������ API
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // ������ ������ API, ��������, v1, v2
    options.SubstituteApiVersionInUrl = true; // ��������� ������ API � URL
});

// ���������� ������������ Swagger
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

// ���������� ������ API
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true; // ���� ������ �� �������, ������������ ������ �� ���������
    options.DefaultApiVersion = new ApiVersion(1, 0); // ������ �� ���������
    options.ReportApiVersions = true; // ����������� ���������� � ������� � ���������� ������
});

var app = builder.Build();

// ������������ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();

    // ��������� ������������ ��� ���� ������ API
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(config =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            config.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
        config.RoutePrefix = string.Empty; // Swagger �������� �� ����� �����
    });
}
app.UseCors("AllowReactApp");
// ���������������� ��������� ����������
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ������������� ������������
app.MapControllers();

// ������������� ���� ������
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<LibraryDbContext>();
        DbInitializer.Initialize(context); // ������������� ���� ������
    }
    catch (Exception exception)
    {
        // ����������� ��� ��������� ������ ������������� ����
    }
}

// ������ ����������
app.Run();
