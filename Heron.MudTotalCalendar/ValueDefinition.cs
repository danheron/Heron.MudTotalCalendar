namespace Heron.MudTotalCalendar;

public class ValueDefinition
{
    public string Name { get; set; } = string.Empty;

    public string Units { get; set; } = string.Empty;

    public bool PrefixUnits { get; set; }

    public string FormatString { get; set; } = string.Empty;

    //public string Color { get; set; } = "--mud-palette-primary";
    
    public string Class { get; set; } = string.Empty;
    
    public string Style { get; set; } = string.Empty;
}