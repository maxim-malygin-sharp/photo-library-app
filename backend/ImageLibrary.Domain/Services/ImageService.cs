using ImageLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageLibrary.Domain.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageLibraryClient _client;

        public ImageService(IImageLibraryClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<AlbumImageInfo>> GetImagesByAlbumIdAsync(int albumId)
        {
            var response = await _client.GetImagesByAlbumIdAsync(albumId);
            var result = response.Select(x => new AlbumImageInfo
            {
                Id = x.Id,
                Title = x.Title
            });

            return result;
        }
    }
}
