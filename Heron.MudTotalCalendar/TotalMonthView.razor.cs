using Heron.MudCalendar;
using MudBlazor.Extensions;
using MudBlazor.Utilities;

namespace Heron.MudTotalCalendar;

public partial class TotalMonthView : MonthView
{
    private MudTotalCalendar TotalCalendar => (MudTotalCalendar)Calendar;

    private bool ShowTotalColumn => TotalCalendar.ShowWeekTotal;
    
    protected override int Columns => ShowTotalColumn ? 8 : 7;

    private int TotalRows => Rows + (TotalCalendar.ShowMonthTotal ? 1 : 0);
    
    protected override string GridStyle =>
        new StyleBuilder()
            .AddStyle("grid-template-columns", $"repeat({Columns}, minmax(10px, 1fr))")
            .AddStyle("grid-template-rows",
                $"repeat({TotalRows}, {(100.0 / TotalRows).ToInvariantString()}%)",
                Calendar.MonthCellMinHeight == 0)
            .Build();
    
    protected override string ContentGridStyle =>
        new StyleBuilder()
            .AddStyle("grid-template-rows",
                $"repeat({TotalRows}, {(100.0 / TotalRows).ToInvariantString()}%)",
                Calendar.MonthCellMinHeight == 0)
            .Build();

    protected virtual string MonthTotalDayStyle(int index)
    {
        return new StyleBuilder()
            .AddStyle("border-top", "1px solid var(--mud-palette-table-lines)")
            .AddStyle("border-right", "1px solid var(--mud-palette-table-lines)", index == Columns - 2)
            .AddStyle("width", $"{(100.0 / Columns).ToInvariantString()}%")
            .Build();
    }

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