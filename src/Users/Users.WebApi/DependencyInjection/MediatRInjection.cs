namespace Users.WebApi.DependencyInjection;
public static class MediatRInjection
{
    public static IServiceCollection AddMediatRInjection(this IServiceCollection service)
    {
        var assembly = AppDomain.CurrentDomain.Load("Users.Application");
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        return service;
    }
}