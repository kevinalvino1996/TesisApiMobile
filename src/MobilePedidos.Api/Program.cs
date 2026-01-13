using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MobilePedidos.Api.Extensions;
using MobilePedidos.Api.Middlewares;
using Serilog;
using System.Text;

// Configurar Serilog desde appsettings.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
        .Build())
    .CreateLogger();

try
{
    Log.Information("Iniciando MobilePedidos API...");

    var builder = WebApplication.CreateBuilder(args);

    // Agregar Serilog como el proveedor de logging (lee la configuración de appsettings)
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

    builder.Services.AddControllers();

    // Configurar servicios mediante las extensiones creadas (Inyección de Dependencias).
    builder.Services.AddInfrastructure();
    builder.Services.AddApplication();

    //Desactiva la respuesta automática de error de .NET. Esto permite que tu propio Middleware y FluentValidation tomen el control total de los mensajes de error.
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });


    builder.Services.AddEndpointsApiExplorer();
    //builder.Services.AddSwaggerGen();
    //habilitar Authorize
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MobilePedidos API", Version = "v1" });

        // 1. Definir el esquema de seguridad JWT
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Autenticación JWT usando el esquema Bearer. Ejemplo: 'Bearer {token}'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        // 2. Hacer que Swagger use el esquema definido globalmente
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    });

    //mapeador automático para convertir Entidades en DTOs
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Fluent Validation / Habilita la validación automática para que, antes de entrar al Controller, se verifiquen las reglas de tus DTOs.
    builder.Services.AddFluentValidationAutoValidation();


    var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();


    // Configuración CORS , desde appsettingjson
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigins", policy =>
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod());
    });

    //configuración de autenticación
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["DatcorpJwtAPI:Issuer"],
            ValidAudience = builder.Configuration["DatcorpJwtAPI:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["DatcorpJwtAPI:Secret"]!))
        };

    });
    //Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
    var app = builder.Build();

    //Middleware GLOBAL, Crucial: Es el primer guardia. Si ocurre cualquier error después de este punto, él lo captura y lo devuelve como un JSON ordenado.
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    // Agregar middleware de logging HTTP (registra requests/responses)
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseHttpsRedirection();

    app.UseCors("AllowSpecificOrigins");
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    Log.Information("MobilePedidos API iniciada correctamente");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "La aplicación falló al iniciar");
}
finally
{
    Log.CloseAndFlush();
}
