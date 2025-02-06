using System;

namespace TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Models;

public interface ISongRepository<TId, TEntity>
where TId : struct
where TEntity : BaseEntity<TId>
{
    Task<List<Song>> GetAllSongs();
    Task<TEntity> AddSong(TEntity song);
    Task<Song> FindSongById(TId id);
    Task<List<Song>> FindSongByName(string name);
    Task<Song> UpdateSong(Song song);
    Task DeleteSong(TEntity song);
}
