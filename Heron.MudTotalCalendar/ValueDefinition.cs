using System.Text;

namespace Heron.MudTotalCalendar;

public class ValueDefinition
{
    public string Name { get; set; } = string.Empty;

    public string Units { get; set; } = string.Empty;

    public bool PrefixUnits { get; set; }

    public string FormatString { get; set; } = string.Empty;
    
    public Func<double, string>? FormatFunc  { get; set; }

    public string Class { get; set; } = string.Empty;
    
    public string Style { get; set; } = string.Empty;

    public static string FormatValue(Value value)
    {
        var sb = new StringBuilder();
        if (value.Definition.PrefixUnits)
        {
            sb.Append(value.Definition.Units);
            sb.Append(' ');
        }

        sb.Append(value.Definition.FormatFunc == null
            ? value.Amount.ToString(value.Definition.FormatString)
            : value.Definition.FormatFunc(value.Amount));

        if (!value.Definition.PrefixUnits)
        {
            sb.Append(' ');
            sb.Append(value.Definition.Units);
        }

        return sb.ToString();
    }
}