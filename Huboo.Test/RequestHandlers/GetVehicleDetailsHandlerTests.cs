namespace Huboo.Test.RequestHandlers;

public class GetVehicleDetailsHandlerTests
{
    private readonly GetVehicleDetailsHandler subject;
    private readonly Mock<IVehicleInfoApi> mockClient;
    private readonly Mock<IMapper> mockMapper;

    public GetVehicleDetailsHandlerTests()
    {
        mockClient = new Mock<IVehicleInfoApi>();
        mockMapper = new Mock<IMapper>();

        mockMapper.Setup(m => m.ConfigurationProvider)
            .Returns(() => new MapperConfiguration(cfg => { cfg.CreateMap<Vehicle, VehicleInfo>(); }));

        subject = new GetVehicleDetailsHandler(mockClient.Object, mockMapper.Object);
    }

    [Fact]
    public async Task Handle_Calls_GetVehicleInfo()
    {
        var registration = "foobar";
        var request = new GetVehicleDetails(registration);

        var _ = await subject.Handle(request);

        mockClient.Verify(m => m.GetVehicleInfo(registration), Times.Once());
    }

    [Fact]
    public async Task Handle_ReturnsInfoOrderedByFirstUsedDate_WhenMultipleVehiclesFound()
    {
        var registration = "foobar";
        var request = new GetVehicleDetails(registration);
        mockClient.Setup(m => m.GetVehicleInfo(registration))
            .ReturnsAsync(new Vehicle[] 
            {
                new()
                {
                    Make = "First",
                    FirstUsedDate = DateTime.UtcNow.AddYears(-2),
                },
                new()
                {
                    Make = "Second",
                    FirstUsedDate = DateTime.UtcNow.AddYears(-1),
                }
            });

        var result = await subject.Handle(request);

        Assert.Equal("Second", result.First().Make);
        Assert.Equal("First", result.Last().Make);
    }
}
