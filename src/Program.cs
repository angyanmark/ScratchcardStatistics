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
    .AddSingleton(sp => new HttpClient { BaseAddress = new(builder.HostEnvironment.BaseAddress) })
    .AddSingleton<IInitialize>(sp => sp.GetRequiredService<IScratchcardService>())
    .AddSingleton<IScratchcardService, ScratchcardService>()
    .AddMudServices();

await builder.Build().RunAsync();
