
using TeslaACDC.Data.DTO;

namespace TeslaACDC.Business.Interfaces;

public interface IAlbumService
{
    Task<List<Album>> GetAlbums();
    Task <List<Album>> AddAlbums(Album album);
}
