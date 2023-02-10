using ScratchcardStatistics.Extensions;
using ScratchcardStatistics.Models;

namespace ScratchcardStatistics.Services;

public static class DisplayService
{
    public static void Display(List<Scratchcard> scratchcards)
    {
        foreach (var scratchcard in scratchcards.OrderByDescending(s => s.ReleaseDate).ThenByDescending(s => s.Price))
        {
            Console.WriteLine(scratchcard.ToConsoleString());
        }
    }
}