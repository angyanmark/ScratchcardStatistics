using ScratchcardStatistics.Interfaces;
using ScratchcardStatistics.Models;

namespace ScratchcardStatistics.Services.Scratchcards;

public interface IScratchcardService : IInitialize
{
    List<Scratchcard> GetScratchcards();
    Scratchcard GetScratchcard(string escapedName);
}