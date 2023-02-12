using ScratchcardStatistics.Constants;
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

    public static void DeleteStatistics()
    {
        if (Directory.Exists(Folders.RootFolder))
        {
            Directory.Delete(Folders.RootFolder, true);
        }
    }

    public static async Task WriteStatisticsAsync(string statistics)
    {
        Directory.CreateDirectory(Folders.RootFolder);
        await File.WriteAllTextAsync(@$"{Folders.RootFolder}\STATISTICS.md", statistics);
    }

    public static async Task WriteScratchcardStatisticsAsync(List<(string name, string statistics)> scratchcardStatistics)
    {
        Directory.CreateDirectory(@$"{Folders.RootFolder}\{Folders.SubFolder}");
        foreach (var scratchcard in scratchcardStatistics)
        {
            await File.WriteAllTextAsync(@$"{Folders.RootFolder}\{Folders.SubFolder}\{scratchcard.name.Replace(' ', '_')}.md", scratchcard.statistics);
        }
    }
}