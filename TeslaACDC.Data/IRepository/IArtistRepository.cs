using System;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Data.IRepository;

public interface IArtistRepository<TId, TEntity>
where TId : struct
where TEntity : BaseEntity<TId>
{
    Task<List<Artist>> ToListAsync();
    Task<TEntity> AddAsync(TEntity artist);
    Task<Artist> FindAsync(TId id);
    Task<Artist> UpdateAsync(Artist artist);
    Task<bool> DeleteAsync(TId id);
}
