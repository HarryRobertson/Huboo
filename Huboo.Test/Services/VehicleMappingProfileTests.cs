namespace Huboo.Test.Services;

public class VehicleMappingProfileTests
{
    private readonly IMapper subject;

    public VehicleMappingProfileTests()
    {
        subject = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile<VehicleMappingProfile>();
        }).CreateMapper();
    }

    private readonly Vehicle vehicle = new Vehicle()
    {
        Make = "foo",
        Model = "bar",
        PrimaryColour = "rgb",
        MotTests = new MotTest[] 
        {
            new() 
            { 
                CompletedDate = DateTime.UtcNow.AddYears(-1),
                ExpiryDate = DateTime.UtcNow.AddYears(0),
                TestResult = "FAILED",
            },
            new() 
            { 
                CompletedDate = DateTime.UtcNow.AddYears(-1),
                ExpiryDate = DateTime.UtcNow.AddYears(0),
                TestResult = "PASSED",
            },
            new() 
            { 
                CompletedDate = DateTime.UtcNow.AddYears(0),
                ExpiryDate = DateTime.UtcNow.AddYears(1),
                TestResult = "PASSED",
            },
        }
    };

    [Fact]
    public void MapVehicleInfo_CorrectlyMapsMake()
    {
        var info = subject.Map<VehicleInfo>(vehicle);

        Assert.Equal("foo", info.Make);
    }

    [Fact]
    public void MapVehicleInfo_CorrectlyMapsModel()
    {
        var info = subject.Map<VehicleInfo>(vehicle);

        Assert.Equal("bar", info.Model);
    }

    [Fact]
    public void MapVehicleInfo_CorrectlyMapsColour()
    {
        var info = subject.Map<VehicleInfo>(vehicle);

        Assert.Equal("rgb", info.Color);
    }

    [Fact]
    public void MapVehicleInfo_CorrectlyDeterminesExpiry()
    {
        var info = subject.Map<VehicleInfo>(vehicle);

        Assert.Equal(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(1)), info.MotExpiry);
    }

    [Fact]
    public void MapVehicleInfo_CorrectlyDeterminesFailureCount()
    {
        var info = subject.Map<VehicleInfo>(vehicle);

        Assert.Equal(1, info.MotFailures);
    }
}
