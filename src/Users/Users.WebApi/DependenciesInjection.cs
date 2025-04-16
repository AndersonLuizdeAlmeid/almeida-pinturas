using Users.Application.Authentications.JwtToken;
using Users.WebApi.DependencyInjection;
using Users.WebApi.RabbitMQ;

namespace Users.WebApi;
public static class DependenciesInjection
{
    public static IServiceCollection AddInjection(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddMediatRInjection();
        service.AddQueriesInjection();
        service.AddRepositoriesInjection();
        service.AddScoped<RabbitMQPublisher>();

        Key.SetSecret(configuration.GetValue<string>("Jwt:Secret"));

        return service;
    }
}