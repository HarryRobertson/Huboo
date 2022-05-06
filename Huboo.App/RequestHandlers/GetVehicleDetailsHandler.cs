namespace Huboo.App.RequestHandlers;

internal class GetVehicleDetailsHandler : IRequestHandler<GetVehicleDetails, IEnumerable<VehicleInfo>>
{
    private readonly IVehicleInfoApi client;
    private readonly IMapper mapper;

    public GetVehicleDetailsHandler(IVehicleInfoApi client, IMapper mapper)
    {
        this.client = client;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<VehicleInfo>> Handle(GetVehicleDetails request, CancellationToken cancellationToken = default)
    {
        try
        {
            var reg = request.Registration.Trim();
            reg = Regex.Replace(reg, @"\s+", "");
            var vehicle = await client.GetVehicleInfo(reg).ConfigureAwait(false);
            var info = vehicle
                .OrderByDescending(v => v.FirstUsedDate)
                .AsQueryable()
                .ProjectTo<VehicleInfo>(mapper.ConfigurationProvider);
            return info.ToList();
        }
        catch (ApiException ex)
        {
            return ex.StatusCode switch
            {
                HttpStatusCode.NotFound => Array.Empty<VehicleInfo>(),
                _ => throw ex,
            };
        }
    }
}
