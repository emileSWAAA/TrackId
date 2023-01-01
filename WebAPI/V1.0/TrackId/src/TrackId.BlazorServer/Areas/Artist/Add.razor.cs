using System;
using Microsoft.AspNetCore.Components;
using TrackId.Business.Dto;
using TrackId.Business.Services;

namespace TrackId.BlazorServer.Areas.Artist
{
	public abstract class ArtistAddBase : ComponentBase
	{
		[Inject]
		private ILogger<ArtistAddBase> _logger { get; set; }
		[Inject]
		private IArtistService _artistService { get; set; }

		public ArtistDto ArtistDto { get; set; } = new ArtistDto();
		public bool Success { get; set; } = false;

		public async Task OnSubmit()
		{
			try
			{
				var result = await _artistService.AddAsync(ArtistDto, CancellationToken.None);
				if (result == null)
				{
					// TODO: snackbar handle failed
					return;
				}

				Success = true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something went wrong.");
				// TODO: Snackbar hadle
			}

            return;
        }
    }
}

