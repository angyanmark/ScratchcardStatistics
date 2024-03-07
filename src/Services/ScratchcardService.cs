using System.Net.Http.Json;

using ScratchcardStatistics.Models;

namespace ScratchcardStatistics.Services;

public class ScratchcardService(HttpClient _httpClient)
{
    private Scratchcard[]? _scratchcards;

    public async Task<Scratchcard[]> GetScratchcardsAsync()
    {
        if (_scratchcards is not null)
        {
            return _scratchcards;
        }

        var scratchcards = await _httpClient.GetFromJsonAsync<Scratchcard[]>("scratchcards/scratchcards.json") ?? [];
        _scratchcards = [.. scratchcards.OrderByDescending(s => s.ReleaseDate).ThenByDescending(s => s.Price)];
        return _scratchcards;
    }

    public async Task<Scratchcard> GetScratchcardAsync(string modifiedName) =>
        (await GetScratchcardsAsync()).Single(s => string.Equals(s.Name.Replace(' ', '_'), modifiedName));
}
