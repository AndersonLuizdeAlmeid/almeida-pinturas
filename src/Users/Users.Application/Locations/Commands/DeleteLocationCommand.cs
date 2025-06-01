using CSharpFunctionalExtensions;
using MediatR;
using Users.Infrastructure.Data;

namespace Users.Application.Locations.Commands;
public class DeleteLocationCommand : IRequest<Result>
{
    public Location Location { get; set; }

    public DeleteLocationCommand(Location location)
    {
        Location = location;
    }
}