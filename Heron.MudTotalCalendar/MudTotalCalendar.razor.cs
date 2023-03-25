using Heron.MudCalendar.Attributes;
using Microsoft.AspNetCore.Components;

namespace Heron.MudTotalCalendar;

public partial class MudTotalCalendar : MudCalendar.MudCalendar
{
    /// <summary>
    /// The data used to calculate the totals to be displayed in the calendar.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Calendar.Behavior)]
    public List<Value> Values { get; set; } = new();

    /// <summary>
    /// If true the totals for each day are shown.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Calendar.Behavior)]
    public bool ShowDayTotal { get; set; } = true;

    /// <summary>
    /// If true the totals for each week are shown.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Calendar.Behavior)]
    public bool ShowWeekTotal { get; set; } = true;

    /// <summary>
    /// If true the totals for each month are shown.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Calendar.Behavior)]
    public bool ShowMonthTotal { get; set; } = true;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // Default to not showing the week and day views because they don't have totals.
        ShowWeek = false;
        ShowDay = false;
        MonthCellMinHeight = 100;
    }
}