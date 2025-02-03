
using TeslaACDC.Data.Models;

namespace TeslaACDC.Data.IRepository;

public interface IAlbumRepository<TId, TEntity>
where TId : struct
where TEntity : BaseEntity<TId>
{
    Task<List<Album>> ToListAsync();
    Task<TEntity> AddAsync(TEntity album);
    Task<Album> FindAsync(TId id);
    Task<Album> UpdateAsync(Album album);
    Task<bool> DeleteAsync(TId id);
}
