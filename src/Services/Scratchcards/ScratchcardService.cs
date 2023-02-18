using ScratchcardStatistics.Models;
using System.Net.Http.Json;

namespace ScratchcardStatistics.Services.Scratchcards;

public class ScratchcardService : IScratchcardService
{
    private List<Scratchcard> Scratchcards { get; set; } = new();

    private readonly HttpClient httpClient;

    public ScratchcardService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task InitializeAsync()
    {
        var filePaths = await httpClient.GetFromJsonAsync<string[]>("scratchcards/_list.json");
        foreach (var filePath in filePaths)
        {
            var scratchcard = await httpClient.GetFromJsonAsync<Scratchcard>($"scratchcards/{filePath}");
            Scratchcards.Add(scratchcard);
        }
    }

    public List<Scratchcard> GetScratchcards() => Scratchcards;

    public Scratchcard GetScratchcard(string escapedName) =>
        Scratchcards.Single(s => s.Name.Replace(' ', '_') == escapedName);
}