using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var jwtSettings = builder.Configuration.GetSection("Jwt");
var secret = jwtSettings["Secret"];
if (string.IsNullOrEmpty(secret))
{
    throw new Exception("A chave secreta JWT não está configurada.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,  
            ValidateAudience = false,
            ValidateLifetime = true, 
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(secret))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BearerPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapReverseProxy();
app.Run();