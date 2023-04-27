using System;
using AngleSharp.Dom;
using FluentAssertions;
using Heron.MudTotalCalendar.UnitTests.Viewer.TestComponents.TotalCalendar;

namespace Heron.MudTotalCalendar.UnitTests.Components;

[SetCulture("en-GB")]
public class TotalCalendarTests : BunitTest
{
    [Test]
    public void SimpleTest()
    {
        var cut = Context.RenderComponent<TotalCalendarTest>();

        cut.FindAll("div.mud-calendar").Count.Should().Be(1);
    }

    [Test]
    [SetCulture("en-US")]
    public void WeekStartSunday()
    {
        var cut = Context.RenderComponent<TotalCalendarTest>();
        var comp = cut.FindComponent<MudTotalCalendar>();
        
        comp.SetParam(x => x.CurrentDay, new DateTime(2023, 2, 1));

        comp.FindAll("div.mud-cal-month-view div.mud-cal-month-cell")[7].ClassList
            .Contains("mud-cal-total-cell").Should().BeTrue();
    }
}