using ImageLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageLibrary
{
    public interface IImageService
    {
        Task<IEnumerable<AlbumImageInfo>> GetImagesByAlbumIdAsync(int albumId);
    }
}