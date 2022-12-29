using Microsoft.AspNetCore.Components;
using System.Text;
using TrackId.Business.Dto;
using TrackId.Business.Services;
using TrackId.Common.Constants;
using TrackId.Data.Interfaces;

namespace TrackId.BlazorServer.Areas.Track
{
    public class TrackIndexBase : ComponentBase
    {
        public IPaginatedList<TrackDto> Tracks { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;


        [Inject]
        private ILogger<TrackIndexBase> _logger { get; set; }
        [Inject]
        private ITrackService _trackService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                Tracks = await _trackService.GetPagedListAsync(PageIndex, PageSize, new CancellationTokenSource().Token);
                Console.WriteLine(Tracks);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong getting tracks.");
            }
        }
    }
}
