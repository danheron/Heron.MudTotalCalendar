using System.Text;
using Heron.MudCalendar;
using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;

namespace Heron.MudTotalCalendar;

public partial class TotalMonthView : MonthView
{
    private MudTotalCalendar TotalCalendar => (MudTotalCalendar)Calendar;

    private bool ShowTotalColumn => TotalCalendar.ShowWeekTotal;

    private int ColumnCount => ShowTotalColumn ? 8 : 7;
    
    /// <summary>
    /// Classes added to the display of the total.
    /// </summary>
    /// <param name="definition"></param>
    /// <returns></returns>
    protected virtual string GetTotalClassname(ValueDefinition definition)
    {
        return new CssBuilder("mud-cal-total")
            .AddClass(definition.Class)
            .Build();
    }

    /// <summary>
    /// Styles added to the display of the total.
    /// </summary>
    /// <param name="definition"></param>
    /// <returns></returns>
    protected virtual string GetTotalStyle(ValueDefinition definition)
    {
        return definition.Style;
    }

    /// <summary>
    /// Format the value to a string including units.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected virtual string FormatValue(Value value)
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

    protected override List<CalendarCell> BuildCells()
    {
        var cells = base.BuildCells();
        var values = TotalCalendar.Values;

        var totalMonth = new TotalCell { MonthTotal = true };
        var totalWeek = new TotalCell { WeekTotal = true };
        var totalCells = new List<CalendarCell>();
        foreach (var cell in cells)
        {
            // Convert CalendarCell to TotalCell
            var totalCell = new TotalCell(cell);
            totalCells.Add(totalCell);
            totalCell.AddValues(values.Where(v => v.Date == totalCell.Date).ToList());
            
            // Add to week and month totals
            if (!totalCell.Outside) totalMonth.AddValues(totalCell.Values);
            totalWeek.AddValues(totalCell.Values);

            // If last day of week then add the week total
            if (CalendarDateRange.GetDayOfWeek(totalCell.Date) == 6 && ShowTotalColumn)
            {
                totalCells.Add(TotalCalendar.ShowWeekTotal ? totalWeek : new TotalCell());
                totalWeek = new TotalCell { WeekTotal = true };
            }
        }

        if (TotalCalendar.ShowMonthTotal)
        {
            totalCells.Add(totalMonth);
        }

        return totalCells;
    }
}