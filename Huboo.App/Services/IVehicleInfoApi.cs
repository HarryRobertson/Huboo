namespace Huboo.App.Services;

public interface IVehicleInfoApi
{
    [Get("/trade/vehicles/mot-tests?registration={registration}")]
    Task<IEnumerable<Vehicle>> GetVehicleInfo(string registration);
}