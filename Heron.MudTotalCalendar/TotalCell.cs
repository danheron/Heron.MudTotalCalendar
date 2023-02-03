using Heron.MudCalendar;

namespace Heron.MudTotalCalendar;

public class TotalCell<T> : CalendarCell<T> where T : CalendarItem
{
    public List<Value> Values { get; set; } = new();
    
    public bool WeekTotal { get; set; }
    
    public bool MonthTotal { get; set; }

    public void AddValues(List<Value> values)
    {
        foreach (var value in values)
        {
            var currentValue = Values.SingleOrDefault(v => v.Definition.Name == value.Definition.Name);
            if (currentValue == null)
            {
                currentValue = new Value { Definition = value.Definition };
                Values.Add(currentValue);
            }

            currentValue.Amount += value.Amount;
        }
    }
}
