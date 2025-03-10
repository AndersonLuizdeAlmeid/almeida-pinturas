using Users.Application.Users.Repositories;

namespace Users.WebApi.DependencyInjection;
public static class RepositoriesInjection
{
    public static IServiceCollection AddRepositoriesInjection(this IServiceCollection services) 
        => services.AddScoped<IUserRepository, UserRepository>();
}