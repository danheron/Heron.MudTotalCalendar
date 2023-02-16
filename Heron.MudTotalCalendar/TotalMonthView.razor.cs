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
        var cells = base.BuildCells();

        var totalMonth = new TotalCell { MonthTotal = true };
        var totalWeek = new TotalCell { WeekTotal = true };
        var totalCells = new List<CalendarCell>();
        foreach (var cell in cells)
        {
            // Convert CalendarCell to TotalCell
            var totalCell = new TotalCell(cell);
            totalCells.Add(totalCell);
            totalCell.AddValues(Values.Where(v => v.Date == totalCell.Date).ToList());
            
            // Add to week and month totals
            if (!totalCell.Outside) totalMonth.AddValues(totalCell.Values);
            totalWeek.AddValues(totalCell.Values);

            // If last day of week then add the week total
            if (CalendarDateRange.GetDayOfWeek(totalCell.Date) == 6)
            {
                totalCells.Add(totalWeek);
                totalWeek = new TotalCell { WeekTotal = true };
            }
        }

        totalCells.Add(totalMonth);

        return totalCells;
    }
}