using Documents.Application.Repositories;
using Documents.Application.Services;
using Documents.Infrastructure.Data;
using Documents.Infrastructure.Data.MongoSettings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection("MongoDbSettings"));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
