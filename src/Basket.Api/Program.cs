using Basket.Application.AutoMapper;
using Basket.Domain.Interfaces.Repositories;
using Basket.Domain.Interfaces.Services;
using Basket.Domain.Services;
using Basket.Infrastructure.Context;
using Basket.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// IoC

builder.Services.AddTransient<IBasketService, BasketService>();
builder.Services.AddTransient<IBasketRepository, BasketRepository>();

// AutoMapper

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Context

builder.Services.AddDbContext<BasketContext>(option =>
     option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// JWT

var accessToken = builder.Configuration.GetSection("AccessToken");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = accessToken["Iss"],
        ValidAudience = accessToken["Aud"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(accessToken["Secret"])),
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true
    };
});

builder.Services.AddControllers();

// Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Microservice Basket",
        Description = "Microservice of Basket",
        Version = "v1"
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Swagger

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// JWT

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
