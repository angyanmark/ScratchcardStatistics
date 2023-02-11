using ScratchcardStatistics.Services;

var scratchcards = await FileService.ReadScratchcardsAsync();
var statistics = StatisticsService.GetStatistics(scratchcards);
await FileService.WriteStatisticsAsync(statistics);
Console.Write(statistics);