using Heron.MudCalendar;

namespace Heron.MudTotalCalendar;

public partial class TotalMonthView : MonthView
{
    private MudTotalCalendar TotalCalendar => (MudTotalCalendar)Calendar;

    private bool ShowTotalColumn => TotalCalendar.ShowWeekTotal;
    
    protected override int Columns => ShowTotalColumn ? 8 : 7;

    /// <summary>
    /// Classes added to the display of the total.
    /// </summary>
    /// <param name="definition"></param>
    /// <returns></returns>
    protected virtual string GetTotalClassname(ValueDefinition definition)
    {
        return definition.Class;
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