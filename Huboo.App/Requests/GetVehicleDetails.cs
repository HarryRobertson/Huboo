namespace Huboo.App.Requests;

public record GetVehicleDetails(string Registration) : IRequest<IEnumerable<VehicleInfo>>;
