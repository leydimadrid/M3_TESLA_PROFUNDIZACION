
using TeslaACDC.Data.Models;

namespace TeslaACDC.Business.Interfaces;

public interface IAlbumService
{
    public Task<List<Album>> GetAllAlbums();
    public Task<Album> FindAlbumById(int id);
    public Task<Album> AddAlbum(Album album);
    public Task<Album> UpdateAlbum(int id, Album album);
    public Task DeleteAlbum(int id);

}
