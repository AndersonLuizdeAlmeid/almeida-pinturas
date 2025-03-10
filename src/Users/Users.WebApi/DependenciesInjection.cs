using Users.Application.Authentications.JwtToken;
using Users.WebApi.DependencyInjection;

namespace Users.WebApi;
public static class DependenciesInjection
{
    public static IServiceCollection AddInjection(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddMediatRInjection();
        service.AddQueriesInjection();
        service.AddRepositoriesInjection();

        Key.SetSecret(configuration.GetSection("Secret").Value);

        return service;
    }
}