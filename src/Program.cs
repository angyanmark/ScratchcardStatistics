using System.Globalization;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;

using ScratchcardStatistics;
using ScratchcardStatistics.Interfaces;
using ScratchcardStatistics.Services.Scratchcards;

var culture = new CultureInfo("hu-HU");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .AddSingleton<IScratchcardService, ScratchcardService>()
    .AddSingleton<IInitialize>(sp => sp.GetRequiredService<IScratchcardService>())
    .AddMudServices();

await builder.Build().RunAsync();
