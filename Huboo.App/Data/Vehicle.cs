namespace Huboo.App.Data;

public class Vehicle 
{
    public string Registration { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public DateTime FirstUsedDate { get; set; }
    public string FuelType { get; set; } = string.Empty;
    public string PrimaryColour { get; set; } = string.Empty;
    public IEnumerable<MotTest> MotTests { get; set; } = Array.Empty<MotTest>();
}
