using Documents.Application.Repositories.Documents;
using Documents.Application.Repositories.Folders;
using Documents.Application.Services;
using Documents.Infrastructure.Data;
using Documents.Infrastructure.Data.MongoSettings;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IFolderRepository, FolderRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddHostedService<RabbitMQConsumer>();

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

var app = builder.Build();

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();