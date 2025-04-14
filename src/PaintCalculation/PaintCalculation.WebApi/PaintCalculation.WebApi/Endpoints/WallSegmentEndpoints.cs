using Microsoft.EntityFrameworkCore;
using PaintCalculation.WebApi.Data;
using PaintCalculation.WebApi.Models;

namespace PaintCalculation.WebApi.Endpoints;
public static class WallSegmentEndpoints
{
    public static void MapWallSegmentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/wall-segments");

        group.MapGet("/", async (AppDbContext db) =>
            await db.WallSegments.ToListAsync());

        group.MapPost("/", async (AppDbContext db, WallSegment segment) =>
        {
            db.WallSegments.Add(segment);
            await db.SaveChangesAsync();
            return Results.Created($"/api/wall-segments/{segment.Id}", segment);
        });

        group.MapDelete("/", async (AppDbContext db) =>
        {
            db.WallSegments.RemoveRange(db.WallSegments);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }

}