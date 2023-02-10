using ScratchcardStatistics.Models;
using System.Globalization;
using System.Text;

namespace ScratchcardStatistics.Services;

public static class StatisticsService
{
    private static readonly CultureInfo cultureInfo = CultureInfo.GetCultureInfo("hu-HU");
    private static readonly string tableHeader =
        $"| Név                |  Megjelenés   |         Ár | Várható érték | Várható érték % | Nyerési esély | Nyerési esély % |{Environment.NewLine}"
        +"| ------------------ | :-----------: | ---------: | ------------: | :-------------: | :-----------: | :-------------: |";

    public static string GetStatistics(List<Scratchcard> scratchcards)
    {
        var sb = new StringBuilder();
        sb.AppendLine("# Kaparós sorsjegy statisztikák");
        sb.AppendLine("A Magyarországon [kiadott](https://bet.szerencsejatek.hu/sorsjegyek \"Szerencsejáték Zrt. - Kaparós sorsjegyek\") kaparós sorsjegyek statisztikái.");
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
        sb.AppendLine(tableHeader);
        foreach (var scratchcard in scratchcards)
        {
            sb.AppendLine(scratchcard.ToTableRow());
        }
    }

    private static string ToTableRow(this Scratchcard scratchcard) =>
        $"| {scratchcard.Name,-18} | {scratchcard.ReleaseDate:yyyy. MM. dd.} | {scratchcard.Price.ToString("C0", cultureInfo),+10} | {scratchcard.ExpectedValue.ToString("C0", cultureInfo),+13} " +
        $"|       {scratchcard.ExpectedValuePercent:P0}       |    1:{scratchcard.ChanceOfWinningToOne:N2}     |     {scratchcard.ChanceOfWinningPercent:P2}      |";
}