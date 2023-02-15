using Microsoft.AspNetCore.Components;

namespace Heron.MudTotalCalendar.UnitTests.Mocks;

public class MockNavigationManager : NavigationManager
{
    public MockNavigationManager() : base() =>
        this.Initialize("http://localhost:2112/", "http://localhost:2112/test");

    protected override void NavigateToCore(string uri, bool forceLoad) =>
        this.WasNavigateInvoked = true;

    public bool WasNavigateInvoked { get; private set; }
}