namespace Infrastructure.Logger.Options;

public class SerilogOptions
{
    public string MinimumLevel { get; set; } = "Information";
    public bool WriteToFile { get; set; } = true;
    public string FilePath { get; set; } = "logs/app-.log";
    public string RollingInterval { get; set; } = "Day";
    public string TemplateFormat { get; set; } = "[{Timestamp:HH:mm:ss} {Level:u3}] {CorrelationId} {Message:lj} {Properties:j}{NewLine}{Exception}";
}
