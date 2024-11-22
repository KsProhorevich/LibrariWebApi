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

// Регистрация AutoMapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ILibraryDbContext).Assembly));
});

// Регистрация приложений и сервисов
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);
builder.Services.AddControllers();

// Добавление CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // Разрешить запросы только с этого домена
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Добавление аутентификации с использованием JWT
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

// Добавление версии API
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Формат версии API, например, v1, v2
    options.SubstituteApiVersionInUrl = true; // Включение версии API в URL
});

// Добавление конфигурации Swagger
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

// Добавление версии API
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true; // Если версия не указана, используется версия по умолчанию
    options.DefaultApiVersion = new ApiVersion(1, 0); // Версия по умолчанию
    options.ReportApiVersions = true; // Отображение информации о версиях в заголовках ответа
});

var app = builder.Build();

// Конфигурация Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();

    // Генерация документации для всех версий API
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(config =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            config.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
        config.RoutePrefix = string.Empty; // Swagger доступен по корню сайта
    });
}
app.UseCors("AllowReactApp");
// Пользовательская обработка исключений
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Маршрутизация контроллеров
app.MapControllers();

// Инициализация базы данных
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<LibraryDbContext>();
        DbInitializer.Initialize(context); // Инициализация базы данных
    }
    catch (Exception exception)
    {
        // Логирование или обработка ошибок инициализации базы
    }
}

// Запуск приложения
app.Run();
