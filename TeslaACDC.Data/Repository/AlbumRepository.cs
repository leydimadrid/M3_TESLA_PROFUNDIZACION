using Microsoft.EntityFrameworkCore;

using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Models;
namespace TeslaACDC.Data.Repository;

public class AlbumRepository<TId, TEntity> : IAlbumRepository<TId, TEntity>
    where TId : struct
    where TEntity : BaseEntity<TId>
{

    private readonly TeslaContext _context;
    internal DbSet<TEntity> _dbSet;

    public AlbumRepository(TeslaContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<List<Album>> ToListAsync()
    {
        return await _context.Albums.ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity album)
    {
        await _dbSet.AddAsync(album);
        await _context.SaveChangesAsync();
        return album;
    }

    public async Task<Album> FindAsync(TId id)
    {
        return await _context.Albums.FindAsync(id);
    }

    public async Task<Album> UpdateAsync(Album album)
    {
        await _context.SaveChangesAsync();
        return album;
    }

    public async Task DeleteAsync(TEntity album)
    {
        _dbSet.Remove(album);
        await _context.SaveChangesAsync();
    }
}
