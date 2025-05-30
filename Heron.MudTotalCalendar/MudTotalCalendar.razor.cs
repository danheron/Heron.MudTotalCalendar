using System.Diagnostics.CodeAnalysis;
using Heron.MudCalendar;
using Heron.MudCalendar.Attributes;
using Heron.MudCalendar.Services;
using Microsoft.AspNetCore.Components;

namespace Heron.MudTotalCalendar;

/// <summary>
/// Calendar component for displaying numerical data with totals.
/// </summary>
public partial class MudTotalCalendar<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T> : MudCalendar<T> where T:CalendarItem
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
    /// <remarks>
    /// Defaults to <c>true</c>.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.Calendar.Behavior)]
    public bool ShowDayTotal { get; set; } = true;

    /// <summary>
    /// If true the totals for each week are shown.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>true</c>.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.Calendar.Behavior)]
    public bool ShowWeekTotal { get; set; } = true;

    /// <summary>
    /// If true the totals for each month are shown.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>true</c>.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.Calendar.Behavior)]
    public bool ShowMonthTotal { get; set; } = true;
    
    private JsService? _jsService;
    
    public MudTotalCalendar()
    {
        // Default to not showing the week and day views because they don't have totals.
        ShowWeek = false;
        ShowDay = false;
        MonthCellMinHeight = 100;
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await SetLinks();
        }
    }
    
    private async Task SetLinks()
    {
        // Check if link is already set
        _jsService ??= new JsService(JsRuntime);
        var head = await _jsService.GetHeadContent();
        if (!string.IsNullOrEmpty(head) && head.Contains("Heron.MudTotalCalendar.min.css")) return;

        // Add link
        _jsService.OnLinkLoaded += (_, _) => Refresh();
        await _jsService.AddLink("_content/Heron.MudTotalCalendar/Heron.MudTotalCalendar.min.css", "stylesheet");
    }
}