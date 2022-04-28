
namespace Huboo.App.Services;

internal class CustomRefitSettings : RefitSettings
{
    public CustomRefitSettings()
        : base(new SystemTextJsonContentSerializer(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = {
                new DateTimeConverterUsingDateTimeParse(),
            }
        }))
        {}
}
