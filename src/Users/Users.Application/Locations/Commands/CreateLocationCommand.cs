using CSharpFunctionalExtensions;
using MediatR;
using Users.Infrastructure.Data;

namespace Users.Application.Locations.Commands;
public class CreateLocationCommand : IRequest<Result>
{
    public Location Location { get; set; }

    public CreateLocationCommand(Location location)
    {
        Location = location;
    }
}