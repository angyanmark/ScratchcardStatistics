using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

using ScratchcardStatistics.Models;
using ScratchcardStatistics.Services;

namespace ScratchcardStatistics.Pages;

public sealed partial class ScratchcardsPage
{
    private readonly GridSort<Scratchcard> _nameSort = GridSort<Scratchcard>.ByAscending(x => x.Name);
    private readonly GridSort<Scratchcard> _chanceOfWinningToOneSort = GridSort<Scratchcard>.ByAscending(x => x.ChanceOfWinningToOne);
    private readonly GridSort<Scratchcard> _isAvailableSort = GridSort<Scratchcard>.ByAscending(x => x.IsAvailable);

    private string _filterName = string.Empty;
    private bool? _filterIsAvailable = true;

    private IQueryable<Scratchcard>? _scratchcardsFiltered;

    protected override void OnInitialized() =>
        FilterChanged();

    private void NameChanged(ChangeEventArgs args)
    {
        if (args.Value is string value)
        {
            _filterName = value;
            FilterChanged();
        }
    }

    private void NameCleared()
    {
        if (string.IsNullOrWhiteSpace(_filterName))
        {
            _filterName = string.Empty;
            FilterChanged();
        }
    }

    private void IsAvailableChanged(bool? isSelected)
    {
        _filterIsAvailable = isSelected;
        FilterChanged();
    }

    private void FilterChanged() =>
        _scratchcardsFiltered = ScratchcardService.Scratchcards
            .Where(s => s.Name.Contains(_filterName, StringComparison.OrdinalIgnoreCase))
            .Where(s => !_filterIsAvailable.HasValue || s.IsAvailable == _filterIsAvailable)
            .OrderByDescending(s => s.ReleaseDate)
            .ThenByDescending(s => s.Price)
            .AsQueryable();
}
