using ScratchcardStatistics.Models;
using System.Globalization;

namespace ScratchcardStatistics.Extensions;

public static class ScratchcardExtensions
{
    private static readonly CultureInfo cultureInfo = CultureInfo.GetCultureInfo("hu-HU");

    public static string ToConsoleString(this Scratchcard scratchcard) =>
        GetConsoleString(scratchcard, '\t');

    private static string GetConsoleString(Scratchcard scratchcard, char separator) =>
        $"{scratchcard.Name,-20}{separator}{scratchcard.ReleaseDate.ToShortDateString()}{separator}{scratchcard.Price.ToString("C0", cultureInfo),+10}{separator}{scratchcard.ExpectedValue.ToString("C0", cultureInfo),+10}{separator}{scratchcard.ExpectedValuePercent:P0}{separator}1:{scratchcard.ChanceOfWinningToOne:N2}{separator}{scratchcard.ChanceOfWinningPercent:P2}";
}