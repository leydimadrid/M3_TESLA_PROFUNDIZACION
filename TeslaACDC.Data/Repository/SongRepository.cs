using System;
using Microsoft.EntityFrameworkCore;


namespace TeslaACDC.Data.Repository;
using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Models;

public class SongRepository<TId, TEntity> : ISongRepository<TId, TEntity>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    private readonly TeslaContext _context;
    internal DbSet<TEntity> _dbSet;

    public SongRepository(TeslaContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<List<Song>> GetAllSongs()
    {
        return await _context.Songs.ToListAsync();
    }

    public async Task<TEntity> AddSong(TEntity song)
    {
        await _dbSet.AddAsync(song);
        await _context.SaveChangesAsync();
        return song;
    }

    public async Task<Song> FindSongById(TId id)
    {
        return await _context.Songs.FindAsync(id);
    }

    public async Task<List<Song>> FindSongByName(string name)
    {
        return await _context.Songs.Where(x => x.Name.ToLower().Contains(name)).ToListAsync();
    }

    public async Task<Song> UpdateSong(Song song)
    {
        await _context.SaveChangesAsync();
        return song;
    }

    public async Task DeleteSong(TEntity song)
    {
        _dbSet.Remove(song);
        await _context.SaveChangesAsync();
    }

}
