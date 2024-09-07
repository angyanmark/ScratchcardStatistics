using System.Globalization;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

using ScratchcardStatistics;
using ScratchcardStatistics.Services;

var culture = new CultureInfo("hu-HU");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddSingleton(sp => new HttpClient { BaseAddress = new(builder.HostEnvironment.BaseAddress) })
    .AddSingleton<ScratchcardService>()
    .AddFluentUIComponents();

await builder.Build().RunAsync();
