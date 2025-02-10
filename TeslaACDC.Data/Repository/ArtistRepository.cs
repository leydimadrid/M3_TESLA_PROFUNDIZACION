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
    public async Task<List<Artist>> GetAllArtist()
    {
        return await _context.Artist.ToListAsync();
    }

    public async Task<Artist> AddArtist(Artist artist)
    {
        await _context.Artist
                .Where(x => x.Name.Equals(artist.Name))
                .FirstOrDefaultAsync();


        await _context.Artist.AddAsync(artist);
        await _context.SaveChangesAsync();
        return artist;
    }

    public async Task<Artist> FindArtistById(TId id)
    {
        return await _context.Artist.FindAsync(id);

    }

    public async Task<List<Artist>> FindArtistByName(string name)
    {
        return await _context.Artist.Where(x => x.Name.ToLower().Contains(name)).ToListAsync();
    }
    public async Task<Artist> UpdateArtist(Artist artist)
    {
        await _context.SaveChangesAsync();
        return artist;
    }

    public async Task DeleteArtist(TEntity artist)
    {
        _dbSet.Remove(artist);
        await _context.SaveChangesAsync();
    }

}
