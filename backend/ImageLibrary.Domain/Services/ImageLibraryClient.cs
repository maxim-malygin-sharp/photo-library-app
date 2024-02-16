using ImageLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImageLibrary.Domain.Services
{
    public class ImageLibraryClient : IImageLibraryClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _url;

        public ImageLibraryClient(IHttpClientFactory httpClientFactory, AppConfig configuration)
        {
            _httpClientFactory = httpClientFactory;
            _url = configuration.ImageLibraryApi;
        }

        public async Task<IEnumerable<AlbumImageDTO>> GetImagesByAlbumIdAsync(int albumId)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/photos?albumId={albumId}");
                var response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<IList<AlbumImageDTO>>(data);

                    return items;
                }
                else
                {
                    throw new Exception($"Couldn't get images response with code: {response.StatusCode}");
                }
            }

            return Enumerable.Empty<AlbumImageDTO>();
        }
    }
}
