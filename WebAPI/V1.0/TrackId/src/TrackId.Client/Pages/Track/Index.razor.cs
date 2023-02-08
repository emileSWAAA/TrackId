using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using TrackId.Contracts.Track;

namespace TrackId.Client.Pages.Track
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private HttpClient _httpClient { get; set; }

        public GetTrackPaginatedResponse Tracks { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;

        protected override async Task OnInitializedAsync()
        {
            Tracks = await GetPaginated();

            await base.OnInitializedAsync();
        }

        private async Task<GetTrackPaginatedResponse> GetPaginated()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<GetTrackPaginatedResponse>($"/track?pageIndex={PageIndex}&pageSize={PageSize}");
                if(result is not null)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                // TODO: handle exception
            }

            return new GetTrackPaginatedResponse();
        }

    }
}