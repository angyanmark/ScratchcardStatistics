using System.Net.Http.Json;

using ScratchcardStatistics.Models;

namespace ScratchcardStatistics.Services.Scratchcards;

public class ScratchcardService(HttpClient _httpClient) : IScratchcardService
{
    private Scratchcard[] _scratchcards = [];

    public async Task InitializeAsync()
    {
        var scratchcards = await _httpClient.GetFromJsonAsync<Scratchcard[]>("scratchcards/scratchcards.json") ?? [];
        _scratchcards = [.. scratchcards.OrderByDescending(s => s.ReleaseDate).ThenByDescending(s => s.Price)];
    }

    public Scratchcard[] GetScratchcards() => _scratchcards;

    public Scratchcard GetScratchcard(string modifiedName) => _scratchcards.Single(s => string.Equals(s.Name.Replace(' ', '_'), modifiedName));
}
