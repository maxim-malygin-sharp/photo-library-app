using ImageLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageLibrary.Domain.Services
{
    public interface IImageLibraryClient
    {
        Task<IEnumerable<AlbumImageDTO>> GetImagesByAlbumIdAsync(int albumId);
    }
}
