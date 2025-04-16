using Microsoft.EntityFrameworkCore;
using PaintCalculation.WebApi.Data;
using PaintCalculation.WebApi.Models;
using PaintCalculation.WebApi.Models.Dtos;

namespace PaintCalculation.WebApi.Endpoints;
public static class MeasuresEndpoints
{
    public static void MapWallSegmentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/Measures");

        group.MapGet("/full", async (AppDbContext db) =>
            await db.Measure
                .Include(m => m.Separates)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync()).RequireAuthorization();


        group.MapPost("/full", async (AppDbContext db, CreateMeasureRequest request) =>
        {
            if (request.Separates == null || !request.Separates.Any())
                return Results.BadRequest("É necessário adicionar pelo menos uma medida separada.");

            var measure = new Measure
            {
                Description = request.Description,
                Area = request.Area,
                Separates = request.Separates.Select(s => new SeparateMeasure
                {
                    Width = s.Width,
                    Height = s.Height
                }).ToList()
            };

            measure.Area = measure.Separates.Sum(s => s.Width * s.Height);

            db.Measure.Add(measure);
            await db.SaveChangesAsync();

            return Results.Created($"/Measures/full/{measure.Id}", measure);
        }).RequireAuthorization();

        group.MapDelete("/full/{id}", async (AppDbContext db, long id) =>
        {
            var measure = await db.Measure.FindAsync(id);

            if (measure is null)
                return Results.NotFound();

            db.Measure.Remove(measure);
            await db.SaveChangesAsync();

            return Results.NoContent();
        }).RequireAuthorization();

    }

}