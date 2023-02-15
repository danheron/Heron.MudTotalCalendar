using System.Net.Http;
using Heron.MudTotalCalendar.UnitTests.Mocks;
using Microsoft.AspNetCore.Components;
using MudBlazor.Services;

namespace Heron.MudTotalCalendar.UnitTests;

public static class TestContextExtensions
{
    public static void AddTestServices(this Bunit.TestContext ctx)
    {
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        ctx.Services.AddSingleton<NavigationManager>(new MockNavigationManager());
        ctx.Services.AddMudServices(options =>
        {
            options.SnackbarConfiguration.ShowTransitionDuration = 0;
            options.SnackbarConfiguration.HideTransitionDuration = 0;
        });
        ctx.Services.AddScoped(sp => new HttpClient());
        ctx.Services.AddOptions();
    }
}