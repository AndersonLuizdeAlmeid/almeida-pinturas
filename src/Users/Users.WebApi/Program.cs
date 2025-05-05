using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Users.Infrastructure.Data;
using Users.WebApi;
using Users.WebApi.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInjection(builder.Configuration);
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

var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience, // mesmo que no Gateway

            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Convert.FromBase64String(builder.Configuration["Jwt:Secret"])
            )
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddSingleton<RabbitMQPublisher>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
