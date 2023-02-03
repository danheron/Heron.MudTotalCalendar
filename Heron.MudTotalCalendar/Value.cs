namespace Heron.MudTotalCalendar;

public class Value
{
    public DateTime Date { get; set; }
    
    public ValueDefinition Definition { get; set; } = new();
    
    public double Amount { get; set; }
}