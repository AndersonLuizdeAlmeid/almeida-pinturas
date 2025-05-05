using APIGateway.WebApi.Utils;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var secret = builder.Configuration.GetValue<string>("Jwt:Secret");
if (string.IsNullOrEmpty(secret))
{
    throw new Exception("A chave secreta JWT n�o est� configurada.");
}
Key.SetSecret(secret);

var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", corsPolicy =>
    {
        corsPolicy
            .WithOrigins(
                "http://almeida-pinturas.site:3000",
                "https://almeida-pinturas.site:3000"   // caso configurarmos HTTPS
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddAuthorization();

//RATE LIMITING
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

//LOGS
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseCors("AllowAll");
app.UseAuthentication(); 
app.UseAuthorization();
app.MapReverseProxy();
app.Run();