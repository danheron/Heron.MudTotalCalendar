# MudTotalCalendar
![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/danheron/Heron.MudTotalCalendar/build-test-mudtotalcalendar.yml?branch=dev&logo=github&style=flat-square)
![Codecov](https://img.shields.io/codecov/c/github/danheron/Heron.MudTotalCalendar?logo=codecov&logoColor=white&style=flat-square&token=EP53WKLLLX)
[![GitHub](https://img.shields.io/github/license/danheron/Heron.MudTotalCalendar?color=594ae2&logo=github&style=flat-square)](https://github.com/danheron/Heron.MudTotalCalendar/blob/master/LICENSE)
[![GitHub last commit](https://img.shields.io/github/last-commit/danheron/Heron.MudTotalCalendar?color=594ae2&style=flat-square&logo=github)](https://github.com/danheron/Heron.MudTotalCalendar)
[![Nuget version](https://img.shields.io/nuget/v/Heron.MudTotalCalendar?color=ff4081&label=nuget%20version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Heron.MudTotalCalendar/)

A calendar component for displaying numerical data with totals.

## Documentation

Documentation and examples are available [here](https://danheron.github.io/Heron.MudTotalCalendar).

## Getting Started

MudTotalCalendar relies on MudBlazor. Follow the installation instructions for [MudBlazor](https://mudblazor.com/getting-started/installation)

Once your project is setup with MudBlazor you can install the MudTotalCalendar package

```
dotnet add package Heron.MudTotalCalendar
```

Add the following to `_Imports.razor`

```razor
@using Heron.MudTotalCalendar
```

Add style and script references (optional). This step shouldn't be necessary as the component injects the references into the page as needed. However if you find that calendar is not displaying properly then add the following to your `index.html` or `_Layout.cshtml`/`_Host.cshtml`

```html
<link href="_content/Heron.MudCalendar/Heron.MudCalendar.min.css" rel="stylesheet" />
<link href="_content/Heron.MudTotalCalendar/Heron.MudTotalCalendar.min.css" rel="stylesheet" />
...
<script type="module" src="_content/Heron.MudCalendar/Heron.MudCalendar.min.js"></script>
```

Add the MudCalendar component to your razor page/component

```razor
<MudTotalCalendar T="CalendarItem"/>
```

Check out the examples of how to use and customize the MudTotalCalendar component.

## Support

For any issues or feature requests, please open a new issue on GitHub.
