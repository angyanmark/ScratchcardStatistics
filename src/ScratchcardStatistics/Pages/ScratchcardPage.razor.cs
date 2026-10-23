using Microsoft.AspNetCore.Components;

using ScratchcardStatistics.Models;
using ScratchcardStatistics.Services;

namespace ScratchcardStatistics.Pages;

public sealed partial class ScratchcardPage
{
    [Parameter]
    public required string PathName { get; init; }

    private Scratchcard? _scratchcard;

    protected override void OnInitialized() =>
        _scratchcard = ScratchcardService.Scratchcards.First(s => string.Equals(s.PathName, PathName));
}
