using System;
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
    public async Task AddAsync(TEntity artista)
    {
        await _dbSet.AddAsync(artista);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity> FindAsync(TId id)
    {
        return await _dbSet.FindAsync(id);
    }
}
