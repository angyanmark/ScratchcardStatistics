namespace ScratchcardStatistics.Models;

public sealed record Scratchcard(
    string Name,
    uint Price,
    DateOnly ReleaseDate,
    DateOnly? EndDate,
    uint TotalSupply,
    Dictionary<uint, uint> PrizeStructure)
{
    public bool IsAvailable => !EndDate.HasValue || EndDate.Value > DateOnly.FromDateTime(DateTime.Now);
    public uint Jackpot => PrizeStructure.Max(ps => ps.Key);
    public decimal ChanceOfWinningToOne => 1 / ChanceOfWinningPercent;
    public decimal ChanceOfWinningPercent => (decimal)PrizeStructure.Sum(ps => ps.Value) / TotalSupply;
    public decimal ExpectedValue => PrizeStructure.Sum(ps => ps.Key * ps.Value) / TotalSupply;
    public decimal ExpectedValuePercent => ExpectedValue / Price;
    public string PathName => Name.Replace(' ', '_');
}
