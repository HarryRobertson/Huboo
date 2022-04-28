namespace Huboo.App.RequestHandlers;

public static class DependencyInjection
{
    public static IServiceCollection AddRequestHandlers(this IServiceCollection services)
        => services.AddMediatR(typeof(DependencyInjection));
}