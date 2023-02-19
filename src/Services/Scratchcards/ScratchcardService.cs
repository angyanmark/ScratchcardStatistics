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
        var scratchcards = await httpClient.GetFromJsonAsync<List<Scratchcard>>("scratchcards/scratchcards.json");
        Scratchcards.AddRange(scratchcards.OrderByDescending(s => s.ReleaseDate).ThenByDescending(s => s.Price));
    }

    public List<Scratchcard> GetScratchcards() => Scratchcards;

    public Scratchcard GetScratchcard(string escapedName) =>
        Scratchcards.Single(s => s.Name.Replace(' ', '_') == escapedName);
}