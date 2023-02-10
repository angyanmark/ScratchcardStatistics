namespace ScratchcardStatistics.Models;

public class Scratchcard
{
    public string Name { get; set; }
    public int Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int TotalSupply { get; set; }
    public Dictionary<int, int> PrizeStructure { get; set; }

    public int TopPrize => PrizeStructure.Max(ps => ps.Key);
    public double ChanceOfWinningToOne => 1 / ChanceOfWinningPercent;
    public double ChanceOfWinningPercent => (double)PrizeStructure.Sum(ps => ps.Value) / TotalSupply;
    public double ExpectedValue => PrizeStructure.Sum(ps => (double)(ps.Key * ps.Value) / TotalSupply);
    public double ExpectedValuePercent => ExpectedValue / Price;
}