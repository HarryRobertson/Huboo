namespace Huboo.App.Models;

public class VehicleInfo
{
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public DateOnly MotExpiry { get; set; }
    public byte MotFailures { get; set; } // potentially dangerous assumption this will never be too small
}