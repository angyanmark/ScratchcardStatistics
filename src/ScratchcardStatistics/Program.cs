using ScratchcardStatistics.Services;

var scratchcards = await LoadService.LoadScratchcardsAsync();
var statistics = StatisticsService.GetStatistics(scratchcards);
Console.Write(statistics);