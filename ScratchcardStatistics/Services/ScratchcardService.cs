﻿using ScratchcardStatistics.Models;
using System.Text.Json;

namespace ScratchcardStatistics.Services;

public static class ScratchcardService
{
    public static async Task<List<Scratchcard>> GetScratchcardsAsync()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var scratchcards = new List<Scratchcard>();

        foreach (var filePath in Directory.EnumerateFiles("Scratchcards"))
        {
            var json = await File.ReadAllTextAsync(filePath);
            scratchcards.Add(JsonSerializer.Deserialize<Scratchcard>(json, options)!);
        }

        return scratchcards;
    }
}