using System;
using AngleSharp.Dom;
using FluentAssertions;
using Heron.MudCalendar;
using Heron.MudTotalCalendar.UnitTests.Viewer.TestComponents.TotalCalendar;
using MudBlazor;

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

        comp.Find("div.mud-cal-month-view div.mud-cal-month-cell:nth-child(8) div.mud-cal-total-cell").Should()
            .NotBeNull();
    }

    [Test]
    public void ShowDayView()
    {
        var cut = Context.RenderComponent<TotalCalendarViewTest>();
        var comp = cut.FindComponent<MudTotalCalendar>();

        comp.FindComponent<EnumSwitch<CalendarView>>().Find("div").ClassList.Should().NotContain("d-none");
        comp.FindComponent<EnumSwitch<CalendarView>>().FindAll("button")[2].TextContent.Should().Be("Day");
    }
    
    [Test]
    public void ShowWeekView()
    {
        var cut = Context.RenderComponent<TotalCalendarViewTest>();
        var comp = cut.FindComponent<MudTotalCalendar>();

        comp.FindComponent<EnumSwitch<CalendarView>>().Find("div").ClassList.Should().NotContain("d-none");
        comp.FindComponent<EnumSwitch<CalendarView>>().FindAll("button")[1].TextContent.Should().Be("Week");
    }
    
    [Test]
    public void CellClick()
    {
        var cut = Context.RenderComponent<TotalCalendarEventsTest>();
        var comp = cut.FindComponent<MudTotalCalendar>();
        var textField = cut.FindComponent<MudTextField<string>>();
        
        // Month View
        comp.SetParam(x => x.CurrentDay, new DateTime(2023, 1, 1));
        comp.Find("div.mud-cal-month-cell.mud-cal-month-link").Click();
        textField.Instance.Text.Should().Be("26");
        
        // Week View
        comp.SetParam(x => x.View, CalendarView.Week);
        comp.SetParam(x => x.CurrentDay, new DateTime(2023, 1, 13));
        comp.Find("div.mud-cal-week-layer a").Click();
        textField.Instance.Text.Should().Be("9");
        
        // Day View
        comp.SetParam(x => x.View, CalendarView.Day);
        comp.SetParam(x => x.CurrentDay, new DateTime(2023, 1, 8));
        comp.Find("div.mud-cal-week-layer a").Click();
        textField.Instance.Text.Should().Be("8");
    }
}