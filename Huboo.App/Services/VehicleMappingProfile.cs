namespace Huboo.App.Services;
 
internal class VehicleMappingProfile : Profile
{
    public VehicleMappingProfile()
    {
        CreateMap<Vehicle, VehicleInfo>()
            .ForMember(info => info.Make, opt => opt.MapFrom(vehicle => vehicle.Make))
            .ForMember(info => info.Model, opt => opt.MapFrom(vehicle => vehicle.Model))
            .ForMember(info => info.Color, opt => opt.MapFrom(vehicle => vehicle.PrimaryColour))
            .ForMember(info => info.MotExpiry, opt => opt.MapFrom(vehicle 
                => vehicle.MotTests
                    .OrderByDescending(t => t.CompletedDate)
                    .Select(t => DateOnly.FromDateTime(t.ExpiryDate))
                    .FirstOrDefault()))
            .ForMember(info => info.MotFailures, opt => opt.MapFrom(vehicle => (byte)vehicle.MotTests.Count(t => t.TestResult == "FAILED")));
    }
}