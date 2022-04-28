
namespace Huboo.App.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddRefitClients(this IServiceCollection services) => services
        .AddSingleton<RefitSettings, CustomRefitSettings>()
        .AddRefitClient<IVehicleInfoApi>(provider => provider.GetRequiredService<RefitSettings>())
        .ConfigureHttpClient((provider, client) => 
        {
            client.BaseAddress = new Uri("https://beta.check-mot.service.gov.uk");
            client.DefaultRequestHeaders.Add("x-api-key", 
                provider.GetRequiredService<IConfiguration>().GetValue<string>("ApiKey"));
        }).Services;

    public static IServiceCollection AddMappers(this IServiceCollection services)
        => services.AddAutoMapper(typeof(DependencyInjection));
}
