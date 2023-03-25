namespace Heron.MudTotalCalendar.Docs.Pages;

public partial class Index
{
    private List<Value> BuildTestData()
    {
        var power = new ValueDefinition
        {
            Name = "Power",
            Units = "kWh",
            FormatString = "N2",
            Style = "background-color: #AD1457; color: #ffffff;"
        };
        var offpeak = new ValueDefinition
        {
            Name = "Off-peak",
            Units = "EUR",
            PrefixUnits = true,
            FormatString = "N2",
            Style = "background-color: #1565C0; color: #ffffff;"
        };
        var peak = new ValueDefinition
        {
            Name = "Peak",
            Units = "EUR",
            PrefixUnits = true,
            FormatString = "N2",
            Style = "background-color: #2E7D32; color: #ffffff;"
        };
        var total = new ValueDefinition
        {
            Name = "Total",
            Units = "EUR",
            PrefixUnits = true,
            FormatString = "N2",
            Style = "background-color: #EF6C00; color: #ffffff;"
        };

        return new List<Value>
        {
            // Today
            new() { Date = DateTime.Today, Definition = power, Amount = 3.34 },
            new() { Date = DateTime.Today, Definition = peak, Amount = 4.34 },
            new() { Date = DateTime.Today, Definition = offpeak, Amount = 2.34 },
            new() { Date = DateTime.Today, Definition = total, Amount = 6.68 },
            
            // Tomorrow
            new() { Date = DateTime.Today.AddDays(-1), Definition = power, Amount = 4.89 },
            new() { Date = DateTime.Today.AddDays(-1), Definition = peak, Amount = 5.23 },
            new() { Date = DateTime.Today.AddDays(-1), Definition = offpeak, Amount = 3.32 },
            new() { Date = DateTime.Today.AddDays(-1), Definition = total, Amount = 8.55 },
            
            // The next day
            new() { Date = DateTime.Today.AddDays(-2), Definition = power, Amount = 2.97 },
            new() { Date = DateTime.Today.AddDays(-2), Definition = peak, Amount = 5.78 },
            new() { Date = DateTime.Today.AddDays(-2), Definition = offpeak, Amount = 2.98 },
            new() { Date = DateTime.Today.AddDays(-2), Definition = total, Amount = 8.76 }
        };
    }
}