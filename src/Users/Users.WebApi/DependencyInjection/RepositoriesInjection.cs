using Users.Application.Locations.Repositories;
using Users.Application.Users.Repositories;
using Users.Application.WorksHours.Repositories;

namespace Users.WebApi.DependencyInjection;
public static class RepositoriesInjection
{
    public static IServiceCollection AddRepositoriesInjection(this IServiceCollection services) 
        => services.AddScoped<IUserRepository, UserRepository>()
                   .AddScoped<ILocationRepository, LocationRepository>()
                   .AddScoped<IWorkHoursRepository, WorkHoursRepository>();
}