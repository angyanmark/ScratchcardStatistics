namespace ScratchcardStatistics.Models;

public class Scratchcard
{
    public required string Name { get; init; }
    public required int Price { get; init; }
    public required DateOnly ReleaseDate { get; init; }
    public DateOnly? EndDate { get; init; }
    public required int TotalSupply { get; init; }
    public required Dictionary<int, int> PrizeStructure { get; init; }

    public bool IsAvailable => !EndDate.HasValue;
    public int Jackpot => PrizeStructure.Max(ps => ps.Key);
    public decimal ChanceOfWinningToOne => 1 / ChanceOfWinningPercent;
    public decimal ChanceOfWinningPercent => (decimal)PrizeStructure.Sum(ps => ps.Value) / TotalSupply;
    public decimal ExpectedValue => PrizeStructure.Sum(ps => (long)ps.Key * ps.Value) / TotalSupply;
    public decimal ExpectedValuePercent => ExpectedValue / Price;
}
