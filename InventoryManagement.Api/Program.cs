using Common.Interfaces.Products.Command;
using Common.Interfaces.Products.Query;
using FluentValidation;
using InventoryManagement.Api.Middlewares;
using InventoryManagement.Application.Common.Behaviors;
using InventoryManagement.Application.Common.Interfaces.Authentication;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Command;
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Queries;
using InventoryManagement.Application.Common.Services;
using InventoryManagement.Application.Common.Settings;
using InventoryManagement.Application.Features.InventoryMovements.Command;
using InventoryManagement.Infrastructure.Authentication;
using InventoryManagement.Infrastructure.Persistence;
using InventoryManagement.Infrastructure.Persistence.Repositories.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Repositories.Commands;
using System.Data;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Inventory API",
        Version = "1.0"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDbConnection>(_ =>
    new SqlConnection(connectionString));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreateInventoryMovementCommand>();

    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
         .Where(x => x.Value?.Errors.Count > 0).ToDictionary(
             kvp => kvp.Key,
             kvp => kvp.Value?.Errors.Select(e => "Formato inválido").ToArray()
         );

        return new BadRequestObjectResult(new
        {
            title = "Validation error",
            errors
        });
    };
});
builder.Services.AddValidatorsFromAssemblyContaining<CreateInventoryMovementCommand>();

builder.Services.AddScoped<IInventoryStockCalculator, InventoryStockCalculator>();

builder.Services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
builder.Services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
builder.Services.AddScoped<IInventoryMovementQueryRepository, InventoryMovementQueryRepository>();

builder.Services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
builder.Services.AddScoped<ICategoryCommandRepository, CategoryCommandRepository>();
builder.Services.AddScoped<IInventoryMovementCommandRepository, InventoryMovementCommandRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()
                   ?? throw new InvalidOperationException("JwtSettings no está configurado");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

    var retries = 10;

    while (retries > 0)
    {
        try
        {
            db.Database.Migrate();
            break;
        }
        catch
        {
            retries--;
            Thread.Sleep(5000); 
        }
    }
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();