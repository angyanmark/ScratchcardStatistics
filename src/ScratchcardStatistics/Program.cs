using ScratchcardStatistics.Services;

var scratchcards = await ScratchcardService.GetScratchcardsAsync();
DisplayService.Display(scratchcards);