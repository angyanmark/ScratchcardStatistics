namespace ScratchcardStatistics.Models;

public record Scratchcard(
    string Name,
    int Price,
    DateOnly ReleaseDate,
    DateOnly? EndDate,
    int TotalSupply,
    Dictionary<int, int> PrizeStructure)
{
    public bool IsAvailable => !EndDate.HasValue || EndDate.Value > DateOnly.FromDateTime(DateTime.Now);
    public int Jackpot => PrizeStructure.Max(ps => ps.Key);
    public decimal ChanceOfWinningToOne => 1 / ChanceOfWinningPercent;
    public decimal ChanceOfWinningPercent => (decimal)PrizeStructure.Sum(ps => ps.Value) / TotalSupply;
    public decimal ExpectedValue => PrizeStructure.Sum(ps => (long)ps.Key * ps.Value) / TotalSupply;
    public decimal ExpectedValuePercent => ExpectedValue / Price;
    public string PathName => Name.Replace(' ', '_');
}
