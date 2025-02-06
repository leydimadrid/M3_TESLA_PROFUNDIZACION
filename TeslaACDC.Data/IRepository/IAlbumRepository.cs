
using TeslaACDC.Data.Models;

namespace TeslaACDC.Data.IRepository;

public interface IAlbumRepository<TId, TEntity>
where TId : struct
where TEntity : BaseEntity<TId>
{
    Task<List<Album>> GetAllAlbums();
    Task<TEntity> AddAlbum(TEntity album);
    Task<Album> FindAlbumById(TId id);
    Task<List<Album>> FindAlbumByName(string name);
    Task<List<Album>> FindAlbumByRange(int year1, int year2);
    Task<Album> UpdateAlbum(Album album);
    Task DeleteAlbum(TEntity album);
}
