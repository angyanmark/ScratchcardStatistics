using ScratchcardStatistics.Models;
using System.Text.Json;

namespace ScratchcardStatistics.Services;

public static class FileService
{
    public static async Task<List<Scratchcard>> ReadScratchcardsAsync()
    {
        var scratchcards = new List<Scratchcard>();
        foreach (var filePath in Directory.EnumerateFiles("Scratchcards"))
        {
            var json = await File.ReadAllTextAsync(filePath);
            scratchcards.Add(JsonSerializer.Deserialize<Scratchcard>(json)!);
        }
        return scratchcards;
    }

    public static async Task WriteStatisticsAsync(string statistics) =>
        await File.WriteAllTextAsync("STATISTICS.md", statistics);
}