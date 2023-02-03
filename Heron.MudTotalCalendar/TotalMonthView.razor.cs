using System.Text;
using Heron.MudCalendar;
using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;

namespace Heron.MudTotalCalendar;

public partial class TotalMonthView<T> : MonthView<T> where T : CalendarItem
{
    [Parameter]
    public List<Value> Values { get; set; } = new();
    
    protected virtual string GetDayTotalStyle(ValueDefinition definition)
    {
        return $"background-color: {definition.Color}";
    }
    
    protected virtual string GetWeekTotalStyle(ValueDefinition definition)
    {
        //return $"background-color: {definition.Color}; filter: brightness(0.9);";
        return GetDayTotalStyle(definition);
    }
    
    protected virtual string GetMonthTotalStyle(ValueDefinition definition)
    {
        //return $"background-color: {definition.Color}; filter: brightness(0.85);";
        return GetDayTotalStyle(definition);
    }
    
    protected virtual string FormatValue(Value value)
    {
        var sb = new StringBuilder();
        if (value.Definition.PrefixUnits)
        {
            sb.Append(value.Definition.Units);
            sb.Append(' ');
        }

        sb.Append(value.Amount.ToString(value.Definition.FormatString));

        if (!value.Definition.PrefixUnits)
        {
            sb.Append(' ');
            sb.Append(value.Definition.Units);
        }

        return sb.ToString();
    }

    protected override void BuildCells()
    {
        var cells = new List<CalendarCell<T>>();
        var monthStart = new DateTime(CurrentDay.Year, CurrentDay.Month, 1);
        var monthEnd = monthStart.AddMonths(1).AddDays(-1);

        var range = new CalendarDateRange(CurrentDay, CalendarView.Month);
        var totalMonth = new TotalCell<T> { MonthTotal = true };
        if (range.Start != null && range.End != null)
        {
            var date = range.Start.Value;
            var lastDate = range.End.Value;
            var totalWeek = new TotalCell<T> { WeekTotal = true };
            while (date <= lastDate)
            {
                var cell = new TotalCell<T> { Date = date };
                cells.Add(cell);
                cell.AddValues(Values.Where(v => v.Date == date).ToList());
                if (date.Date == DateTime.Today) cell.Today = true;
                cell.Items = Items.Where(i => i.Start >= date && i.Start < date.AddDays(1)).ToList();
                if (date < monthStart || date > monthEnd)
                {
                    cell.Outside = true;
                }
                else
                {
                    totalMonth.AddValues(cell.Values);
                }

                totalWeek.AddValues(cell.Values);

                // TODO: Handle cultures where week starts on Sunday
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    cells.Add(totalWeek);
                    totalWeek = new TotalCell<T> { WeekTotal = true };
                }

                date = date.AddDays(1);
            }
        }

        cells.Add(totalMonth);

        Cells = cells;
    }
}