using Users.Application.Locations.Queries;
using Users.Application.Users.Queries;
using Users.Application.WorksHours.Queries;

namespace Users.WebApi.DependencyInjection;
public static class QueriesInjection
{
    public static IServiceCollection AddQueriesInjection(this IServiceCollection services)
        => services.AddScoped<IUserQuery, UserQuery>()
                   .AddScoped<ILocationQuery, LocationQuery>()
                   .AddScoped<IWorkHoursQuery, WorkHoursQuery>(); 
}