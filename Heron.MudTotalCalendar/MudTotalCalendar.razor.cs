using Microsoft.AspNetCore.Components;

namespace Heron.MudTotalCalendar;

public partial class MudTotalCalendar : MudCalendar.MudCalendar
{
    /// <summary>
    /// The data used to calculate the totals to be displayed in the calendar.
    /// </summary>
    [Parameter]
    public List<Value> Values { get; set; } = new();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // Default to not showing the week and day views because they don't have totals.
        ShowWeek = false;
        ShowDay = false;
        MonthCellMinHeight = 100;
    }
}