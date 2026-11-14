using System.Collections.Frozen;

namespace ScratchcardStatistics.Models;

public sealed class Scratchcard
{
    public required string Name { get; init; }
    public required ushort Price { get; init; }
    public required DateOnly ReleaseDate { get; init; }
    public required DateOnly? EndDate { get; init; }
    public required uint TotalSupply { get; init; }
    public required FrozenDictionary<uint, uint> PrizeStructure { get; init; }

    public bool IsAvailable => !EndDate.HasValue || EndDate.Value > DateOnly.FromDateTime(DateTime.Now);
    public uint Jackpot => PrizeStructure.Max(ps => ps.Key);
    public decimal ChanceOfWinningToOne => 1 / ChanceOfWinningRatio;
    public decimal ChanceOfWinningRatio => (decimal)PrizeStructure.Sum(ps => ps.Value) / TotalSupply;
    public decimal ExpectedValue => PrizeStructure.Sum(ps => (decimal)ps.Key * ps.Value) / TotalSupply;
    public decimal ExpectedValueRatio => ExpectedValue / Price;
    public string PathName => Name.Replace(' ', '_');
}
