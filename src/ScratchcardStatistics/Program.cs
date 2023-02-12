using ScratchcardStatistics.Services;

var scratchcards = await FileService.ReadScratchcardsAsync();
var statistics = StatisticsService.GetStatistics(scratchcards);
var scratchcardStatistics = StatisticsService.GetScratchcardStatistics(scratchcards);

FileService.DeleteStatistics();
await FileService.WriteStatisticsAsync(statistics);
await FileService.WriteScratchcardStatisticsAsync(scratchcardStatistics);

Console.WriteLine("Statistics generated.");