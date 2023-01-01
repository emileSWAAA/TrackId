using System;
using Microsoft.AspNetCore.Components;
using TrackId.Business.Dto;
using TrackId.Business.Services;
using TrackId.Data.Interfaces;

namespace TrackId.BlazorServer.Areas.Artist
{
	public abstract class ArtistIndexBase : ComponentBase
	{
		[Inject]
		private IArtistService _artistService { get; set; }
		[Inject]
		private ILogger<ArtistIndexBase> _logger { get; set; }

		public IEnumerable<ArtistDto> Artists { get; set; } = new List<ArtistDto>();
		public int PageIndex { get; set; } = 0;
		public int PageSize { get; set; } = 20;

        protected async override Task OnInitializedAsync()
        {
			try
			{
				var pagedResult = await _artistService.GetPagedListAsync(
					pageIndex: PageIndex,
					pageSize: PageSize,
					CancellationToken.None);

				Artists = pagedResult.Items;
				StateHasChanged();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something went wrong.");
			}
		}
    }
}

