using ScratchcardStatistics.Models;
using System.Net.Http.Json;

namespace ScratchcardStatistics.Services.Scratchcards;

public class ScratchcardService(HttpClient httpClient) : IScratchcardService
{
    private List<Scratchcard> _scratchcards = [];
    private readonly HttpClient _httpClient = httpClient;

    public async Task InitializeAsync()
    {
        var scratchcards = await _httpClient.GetFromJsonAsync<List<Scratchcard>>("scratchcards/scratchcards.json");
        _scratchcards = [.. scratchcards!.OrderByDescending(s => s.ReleaseDate).ThenByDescending(s => s.Price)];
    }

    public List<Scratchcard> GetScratchcards() => _scratchcards;

    public Scratchcard GetScratchcard(string modifiedName) => _scratchcards.Single(s => string.Equals(s.Name.Replace(' ', '_'), modifiedName));
}
