using Microsoft.EntityFrameworkCore;
using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Data.Repository;

public class ArtistRepository<TId, TEntity> : IArtistRepository<TId, TEntity>
    where TId : struct
    where TEntity : BaseEntity<TId>
{

    private readonly TeslaContext _context;
    internal DbSet<TEntity> _dbSet;

    public ArtistRepository(TeslaContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    public async Task<List<Artist>> ToListAsync()
    {
        return await _context.Artist.ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity artist)
    {
        await _dbSet.AddAsync(artist);
        await _context.SaveChangesAsync();
        return artist;
    }

    public async Task<Artist> FindAsync(TId id)
    {
        return await _context.Artist.FindAsync(id);

    }

    public async Task<Artist> UpdateAsync(Artist artist)
    {
        await _context.SaveChangesAsync();
        return artist;
    }

    public async Task DeleteAsync(TEntity artist)
    {
        _dbSet.Remove(artist);
        await _context.SaveChangesAsync();
    }
}
