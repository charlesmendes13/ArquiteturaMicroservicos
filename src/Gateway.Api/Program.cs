using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);

// Option

var accessToken = builder.Configuration.GetSection("AccessToken");

// JWT

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = "Token";
})
.AddJwtBearer("Token", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(accessToken["Secret"])),
        ValidateIssuer = true,
        ValidIssuer = accessToken["Iss"],
        ValidateAudience = true,
        ValidAudience = accessToken["Aud"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true
    };
});

// Ocelot

builder.Services.AddOcelot(builder.Configuration); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// JWT

app.UseAuthentication();

app.MapControllers();

app.Run();

await app.UseOcelot();
