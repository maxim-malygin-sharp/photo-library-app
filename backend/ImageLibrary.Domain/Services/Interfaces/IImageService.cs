using ImageLibrary.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageLibrary.Domain.Services
{
    public interface IImageService
    {
        Task<IList<AlbumImageDTO>> GetImagesByAlbumIdAsync(int albumId);
    }
}