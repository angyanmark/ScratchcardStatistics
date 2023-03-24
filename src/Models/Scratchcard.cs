namespace ScratchcardStatistics.Models;

public class Scratchcard
{
    public string Name { get; set; }
    public int Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int TotalSupply { get; set; }
    public Dictionary<int, int> PrizeStructure { get; set; }

    public bool IsAvailable => !EndDate.HasValue;
    public int Jackpot => PrizeStructure.Max(ps => ps.Key);
    public decimal ChanceOfWinningToOne => 1 / ChanceOfWinningPercent;
    public decimal ChanceOfWinningPercent => (decimal)PrizeStructure.Sum(ps => ps.Value) / TotalSupply;
    public decimal ExpectedValue => PrizeStructure.Sum(ps => (long)ps.Key * ps.Value) / TotalSupply;
    public decimal ExpectedValuePercent => ExpectedValue / Price;
}