using CSharpFunctionalExtensions;
using MediatR;
using Users.Application.WorksHours.Commands;
using Users.Application.WorksHours.Repositories;

namespace Users.Application.WorksHours.Handlers;
public class WorkHoursHandler : IWorkHoursHandler,
                                IRequestHandler<DeleteWorkHoursCommand, Result>,
                                IRequestHandler<CreateWorkHoursCommand, Result>
{
    private readonly IWorkHoursRepository _workHoursRepository;

    public WorkHoursHandler(IWorkHoursRepository workHoursRepository)
    {
        _workHoursRepository = workHoursRepository;
    }

    public async Task<Result> Handle(DeleteWorkHoursCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _workHoursRepository.AddAsync(request.WorkHours, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao inserir valores:{ex.InnerException.Message}");
        }
    }

    public async Task<Result> Handle(CreateWorkHoursCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _workHoursRepository.DeleteAsync(request.WorkHours, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao remover registro:{ex.InnerException.Message}");
        }
    }
}