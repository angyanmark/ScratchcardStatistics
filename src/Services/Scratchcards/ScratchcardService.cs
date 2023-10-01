using ScratchcardStatistics.Models;
using System.Net.Http.Json;

namespace ScratchcardStatistics.Services.Scratchcards;

public class ScratchcardService : IScratchcardService
{
    private List<Scratchcard> _scratchcards = new();
    private readonly HttpClient _httpClient;

    public ScratchcardService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task InitializeAsync()
    {
        var scratchcards = await _httpClient.GetFromJsonAsync<List<Scratchcard>>("scratchcards/scratchcards.json");
        _scratchcards = scratchcards!.OrderByDescending(s => s.ReleaseDate).ThenByDescending(s => s.Price).ToList();
    }

    public List<Scratchcard> GetScratchcards() => _scratchcards;

    public Scratchcard GetScratchcard(string modifiedName) => _scratchcards.Single(s => string.Equals(s.Name.Replace(' ', '_'), modifiedName));
}
