using ScratchcardStatistics.Interfaces;
using ScratchcardStatistics.Models;

namespace ScratchcardStatistics.Services.Scratchcards;

public interface IScratchcardService : IInitialize
{
    Scratchcard[] GetScratchcards();
    Scratchcard GetScratchcard(string modifiedName);
}
