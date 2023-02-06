using Microsoft.AspNetCore.Components;

namespace Heron.MudTotalCalendar;

public partial class MudTotalCalendar : MudCalendar.MudCalendar
{
    [Parameter]
    public List<Value> Values { get; set; } = new();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // Default to not showing the week and day views because they don't have totals.
        ShowWeek = false;
        ShowDay = false;
        MonthMinHeight = 100;
    }
}