using System;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Data.IRepository;

public interface IArtistRepository<TId, TEntity>
where TId : struct
where TEntity : BaseEntity<TId>
{
    Task<List<Artist>> GetAllArtist();
    Task<Artist> AddArtist(Artist artist);
    Task<Artist> FindArtistById(TId id);
    Task<List<Artist>> FindArtistByName(string name);
    Task<Artist> UpdateArtist(Artist artist);
    Task DeleteArtist(TEntity entity);
}
