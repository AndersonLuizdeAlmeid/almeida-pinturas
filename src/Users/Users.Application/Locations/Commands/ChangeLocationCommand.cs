using CSharpFunctionalExtensions;
using MediatR;
using Users.Infrastructure.Data;

namespace Users.Application.Locations.Commands;
public class ChangeLocationCommand : IRequest<Result>
{
    public Location Location { get; set; }

    public ChangeLocationCommand(Location location)
    {
        Location = location;
    }
}