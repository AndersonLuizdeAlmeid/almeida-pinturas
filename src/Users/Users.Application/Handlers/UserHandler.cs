using CSharpFunctionalExtensions;
using MediatR;
using Users.Application.Commands;
using Users.Infrastructure.Data;

namespace Users.Application.Handlers;
public class UserHandler : IUserHandler,
                           IRequestHandler<CreateUserCommand, Result>,
                           IRequestHandler<ChangeUserCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public UserHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Users.AddAsync(request.User, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao inserir valores:{ex.InnerException.Message}");
        }
    }

    public async Task<Result> Handle(ChangeUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users.FindAsync(request.User.Id);

            if (user == null)
            {
                return Result.Failure("Usuário não encontrado");
            }

            user.Name = request.User.Name;
            user.Email = request.User.Email;
            user.Cpf = request.User.Cpf;
            user.Address = request.User.Address;
            user.PhoneNumber = request.User.PhoneNumber;
            user.BirthdayDate = request.User.BirthdayDate;
            user.IsActive = request.User.IsActive;

            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao Alterar valores: {ex.Message}");
        }
    }
}