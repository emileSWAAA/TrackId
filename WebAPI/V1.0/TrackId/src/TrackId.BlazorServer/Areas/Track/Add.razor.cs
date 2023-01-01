using System;
using System.Threading;
using Microsoft.AspNetCore.Components;
using TrackId.Business.Dto;
using TrackId.Business.Services;
using Blazorise.Components;

namespace TrackId.BlazorServer.Areas.Track
{
	public abstract class TrackAddBase : ComponentBase, IDisposable
    {
        [Inject]
        private ILogger<TrackAddBase> _logger { get; set; }
        [Inject]
        private ITrackService _trackService { get; set; }
        [Inject]
        private IArtistService _artistService { get; set; }

        private CancellationTokenSource _cts = new();
        private Random _random = new();

        public TrackDto trackDto { get; set; } = new TrackDto();
        public IEnumerable<ArtistDto> Artists { get; set; } = new List<ArtistDto>();

        public string selectedSearchValue { get; set; }
        public string selectedAutoCompleteText { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var pagedList = await _artistService.GetPagedListAsync(pageIndex: 0, pageSize: 10, _cts.Token);
                Artists = pagedList.Items;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong.");
            }
        }

        public async Task SearchArtists(AutocompleteReadDataEventArgs autocompleteReadDataEventArgs)
        {
            if (!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(_random.Next(100));
                if (!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
                {
                    var pagedList = await _artistService.GetByNameAsync(
                        autocompleteReadDataEventArgs.SearchValue,
                        pageIndex:0,
                        pageSize: 20,
                        _cts.Token);
                    Artists = pagedList.Items;
                }
            }
        }

        public void Dispose()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }
    }
}

