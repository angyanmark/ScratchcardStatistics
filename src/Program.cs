using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ScratchcardStatistics;
using MudBlazor.Services;
using ScratchcardStatistics.Services.Scratchcards;
using ScratchcardStatistics.Interfaces;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IScratchcardService, ScratchcardService>();
builder.Services.AddSingleton<IInitialize>(sp => sp.GetRequiredService<IScratchcardService>());
builder.Services.AddMudServices();

var culture = new CultureInfo("hu-HU");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await builder.Build().RunAsync();
