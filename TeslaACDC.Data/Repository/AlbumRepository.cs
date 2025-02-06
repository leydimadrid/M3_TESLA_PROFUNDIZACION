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

    public async Task<List<Album>> GetAllAlbums()
    {
        return await _context.Albums
            .Include(x => x.Songs)
            .Include(x => x.Artist)
            .ToListAsync();
    }

    public async Task<TEntity> AddAlbum(TEntity album)
    {
        await _dbSet.AddAsync(album);
        await _context.SaveChangesAsync();
        return album;
    }

    public async Task<Album> FindAlbumById(TId id)
    {
        return await _context.Albums.FindAsync(id);
    }

    public async Task<List<Album>> FindAlbumByName(string name)
    {
        return await _context.Albums.Where(x => x.Name.ToLower().Contains(name)).ToListAsync();
    }

    public async Task<List<Album>> FindAlbumByRange(int year1, int year2)
    {
        return await _context.Albums.Where(album => album.Year >= year1 && album.Year <= year2).ToListAsync();
    }

    public async Task<Album> UpdateAlbum(Album album)
    {
        await _context.SaveChangesAsync();
        return album;
    }

    public async Task DeleteAlbum(TEntity album)
    {
        _dbSet.Remove(album);
        await _context.SaveChangesAsync();
    }


}
