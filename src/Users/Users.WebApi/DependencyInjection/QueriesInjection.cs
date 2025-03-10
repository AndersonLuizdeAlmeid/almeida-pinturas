using Users.Application.Users.Queries;

namespace Users.WebApi.DependencyInjection;
public static class QueriesInjection
{
    public static IServiceCollection AddQueriesInjection(this IServiceCollection services)
        => services.AddScoped<IUserQuery, UserQuery>(); 
}