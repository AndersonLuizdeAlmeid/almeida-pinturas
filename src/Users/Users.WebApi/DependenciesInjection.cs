using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Users.WebApi.DependencyInjection;

namespace Users.WebApi;
public static class DependenciesInjection
{
    public static IServiceCollection AddInjection(this IServiceCollection service)
    {
        service.AddMediatRInjection();
        service.AddQueriesInjection();
        service.AddRepositoriesInjection();

        return service;
    }
}