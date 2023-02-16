using System.Text;
using Heron.MudCalendar;
using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;

namespace Heron.MudTotalCalendar;

public partial class TotalMonthView : MonthView
{
    [Parameter]
    public List<Value> Values { get; set; } = new();

    protected virtual string GetTotalClassname(ValueDefinition definition)
    {
        return new CssBuilder("mud-cal-total")
            .AddClass(definition.Class)
            .Build();
    }

    protected virtual string GetTotalStyle(ValueDefinition definition)
    {
        return definition.Style;
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

    protected override List<CalendarCell> BuildCells()
    {
        // TODO: This is a copy of the MonthView.BuildCells() method.  It needs to be refactored to avoid the duplication.
        var cells = new List<CalendarCell>();
        var monthStart = new DateTime(Calendar.CurrentDay.Year, Calendar.CurrentDay.Month, 1);
        var monthEnd = monthStart.AddMonths(1).AddDays(-1);

        var range = new CalendarDateRange(Calendar.CurrentDay, CalendarView.Month);
        var totalMonth = new TotalCell { MonthTotal = true };
        if (range.Start != null && range.End != null)
        {
            var date = range.Start.Value;
            var lastDate = range.End.Value;
            var totalWeek = new TotalCell { WeekTotal = true };
            while (date <= lastDate)
            {
                var cell = new TotalCell { Date = date };
                cells.Add(cell);
                cell.AddValues(Values.Where(v => v.Date == date).ToList());
                if (date.Date == DateTime.Today) cell.Today = true;
                cell.Items = Calendar.Items.Where(i => i.Start >= date && i.Start < date.AddDays(1)).ToList();
                if (date < monthStart || date > monthEnd)
                {
                    cell.Outside = true;
                }
                else
                {
                    totalMonth.AddValues(cell.Values);
                }

                totalWeek.AddValues(cell.Values);

                // If last day of week then add the week total
                if (CalendarDateRange.GetDayOfWeek(date) == 6)
                {
                    cells.Add(totalWeek);
                    totalWeek = new TotalCell { WeekTotal = true };
                }

                date = date.AddDays(1);
            }
        }

        cells.Add(totalMonth);

        return cells;
    }
}