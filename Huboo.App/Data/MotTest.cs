namespace Huboo.App.Data;

public class MotTest 
{
    public DateTime CompletedDate { get; set; } // would like to make DateOnly
    public string TestResult { get; set; } = string.Empty; // could be enum
    public DateTime ExpiryDate { get; set; }
    public string OdometerValue { get; set; } = string.Empty;
    public string OdometerUnit { get; set; } = string.Empty;
    public string MotTestNumber { get; set; } = string.Empty;
}
