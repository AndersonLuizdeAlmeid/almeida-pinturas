using Users.Application.Repositories;

namespace Users.WebApi.DependencyInjection;
public static class RepositoriesInjection
{
    public static IServiceCollection AddRepositoriesInjection(this IServiceCollection services) 
        => services.AddScoped<IUserRepository, UserRepository>();
}