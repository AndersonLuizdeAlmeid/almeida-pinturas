using CSharpFunctionalExtensions;
using MediatR;
using Users.Application.Locations.Commands;
using Users.Application.Locations.Queries;
using Users.Application.Locations.Repositories;

namespace Users.Application.Locations.Handlers;
public class LocationHandler : ILocationHandler,
                               IRequestHandler<CreateLocationCommand, Result>,
                               IRequestHandler<ChangeLocationCommand, Result>,
                               IRequestHandler<DeleteLocationCommand, Result>
{
    private readonly ILocationRepository _locationRepository;
    private readonly ILocationQuery _locationQuery;

    public LocationHandler(ILocationRepository locationRepository, ILocationQuery locationQuery)
    {
        _locationRepository = locationRepository;
        _locationQuery = locationQuery;
    }

    public async Task<Result> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _locationRepository.AddAsync(request.Location, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao inserir valores:{ex.InnerException.Message}");
        }
    }

    public async Task<Result> Handle(ChangeLocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var location = await _locationQuery.GetByIdAsync(request.Location.Id);
            location.IsFinished = 1;
            await _locationRepository.ChangeAsync(location, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao alterar valores:{ex.InnerException.Message}");
        }
    }


    public async Task<Result> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _locationRepository.DeleteAsync(request.Location, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao remover registro:{ex.InnerException.Message}");
        }
    }
}