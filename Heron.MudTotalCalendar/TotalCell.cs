using Heron.MudCalendar;

namespace Heron.MudTotalCalendar;

public class TotalCell : CalendarCell
{
    public List<Value> Values { get; set; } = new();
    
    public bool WeekTotal { get; set; }
    
    public bool MonthTotal { get; set; }

    public TotalCell()
    {
    }

    public TotalCell(CalendarCell cell)
    {
        Date = cell.Date;
        Items = cell.Items;
        Outside = cell.Outside;
        Today = cell.Today;
    }

    public void AddValues(List<Value> values)
    {
        foreach (var value in values)
        {
            var currentValue = Values.SingleOrDefault(v => v.Definition.Name == value.Definition.Name);
            if (currentValue == null)
            {
                currentValue = new Value { Definition = value.Definition, Date = value.Date };
                Values.Add(currentValue);
            }

            currentValue.Amount += value.Amount;
        }
    }
}
