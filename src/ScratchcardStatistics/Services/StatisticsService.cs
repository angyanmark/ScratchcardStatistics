using ScratchcardStatistics.Constants;
using ScratchcardStatistics.Models;
using System.Globalization;
using System.Text;

namespace ScratchcardStatistics.Services;

public static class StatisticsService
{
    private static readonly CultureInfo cultureInfo = new("hu-HU");

    public static string GetStatistics(List<Scratchcard> scratchcards)
    {
        var sb = new StringBuilder();
        sb.AppendLine("# Kaparós sorsjegy statisztikák 🍀");
        sb.AppendLine("A [Szerencsejáték Zrt. által kiadott](https://bet.szerencsejatek.hu/sorsjegyek \"Szerencsejáték Zrt. - Kaparós sorsjegyek\") kaparós sorsjegyek statisztikái.");
        sb.AppendLine();
        sb.AppendLine("## Rendezve megjelenés szerint");
        AppendTable(sb, scratchcards.OrderByDescending(s => s.ReleaseDate).ThenByDescending(s => s.Price));
        sb.AppendLine();
        sb.AppendLine("## Rendezve várható érték százalék szerint");
        AppendTable(sb, scratchcards.OrderByDescending(s => s.ExpectedValuePercent).ThenByDescending(s => s.ChanceOfWinningPercent));
        return sb.ToString();
    }

    private static void AppendTable(StringBuilder sb, IOrderedEnumerable<Scratchcard> scratchcards)
    {
        sb.AppendLine("Név|Megjelenés|Ár|Várható érték|Várható érték %|Nyerési esély|Nyerési esély %|Vehető");
        sb.AppendLine("---|:---:|---:|---:|:---:|:---:|:---:|:---:");
        foreach (var scratchcard in scratchcards)
        {
            sb.AppendLine(scratchcard.ToTableRow());
        }
    }

    private static string ToTableRow(this Scratchcard scratchcard) =>
        $"[{scratchcard.Name}]({Folders.SubFolder}/{scratchcard.Name.Replace(' ', '_')}.md)|{scratchcard.ReleaseDate.ToString("d", cultureInfo)}|{scratchcard.Price.ToString("C0", cultureInfo)}|" +
        $"{scratchcard.ExpectedValue.ToString("C0", cultureInfo)}|{scratchcard.ExpectedValuePercent.ToString("P0", cultureInfo)}|"+
        $"1:{scratchcard.ChanceOfWinningToOne.ToString("N2", cultureInfo)}|{scratchcard.ChanceOfWinningPercent.ToString("P2", cultureInfo)}|{(scratchcard.IsAvailable ? "✅" : "❌")}";

    public static List<(string name, string statistics)> GetScratchcardStatistics(List<Scratchcard> scratchcards)
    {
        var individualStatistics = new List<(string, string)>();
        foreach (var scratchcard in scratchcards)
        {
            individualStatistics.Add((scratchcard.Name, GetScratchcardStatistics(scratchcard)));
        }
        return individualStatistics;
    }

    private static string GetScratchcardStatistics(Scratchcard scratchcard)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"## {scratchcard.Name}");
        sb.AppendLine();
        sb.AppendLine($"Ár: **{scratchcard.Price.ToString("C0", cultureInfo)}**<br/>");
        sb.AppendLine($"Főnyeremény: **{scratchcard.Jackpot.ToString("C0", cultureInfo)}**<br/>");
        sb.AppendLine($"Megjelenés: **{scratchcard.ReleaseDate.ToString("d", cultureInfo)}**<br/>");
        if (scratchcard.EndDate.HasValue)
        {
            sb.AppendLine($"Utolsó értékesítés: **{scratchcard.EndDate.Value.ToString("d", cultureInfo)}**<br/>");
        }
        sb.AppendLine($"Nyerési esély: **1:{scratchcard.ChanceOfWinningToOne.ToString("N2", cultureInfo)}**<br/>");
        sb.AppendLine($"Kibocsátott darabszám: **{scratchcard.TotalSupply.ToString("N0", cultureInfo)} db**<br/>");
        sb.AppendLine();
        sb.AppendLine("### Nyereménystruktúra:");
        AppendTable(sb, scratchcard);
        return sb.ToString();
    }

    private static void AppendTable(StringBuilder sb, Scratchcard scratchcard)
    {
        sb.AppendLine("Darab|Nyeremény|Esély");
        sb.AppendLine("---:|---:|:---:");
        foreach (var structure in scratchcard.PrizeStructure)
        {
            sb.AppendLine(structure.ToTableRow(scratchcard.TotalSupply));
        }
    }

    private static string ToTableRow(this KeyValuePair<int, int> structure, int totalSupply) =>
        $"{structure.Value.ToString("N0", cultureInfo)} db|{structure.Key.ToString("C0", cultureInfo)}|1:{(1 / ((decimal)structure.Value / totalSupply)).ToString("#,##0.##", cultureInfo)}";
}