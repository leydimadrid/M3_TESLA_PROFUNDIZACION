

using TeslaACDC.Data.DTO;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Business.Interfaces;

public interface IAlbumService
{

    public Task<List<AlbumDTO>> ToListAsync();
    public Task<AlbumDTO> FindAsync(int id);
    public Task<AlbumDTO> AddAsync(AlbumDTO album);
    public Task<AlbumDTO> UpdateAsync(int id, AlbumDTO albumDto);
    public Task<bool> DeleteAsync(int id);

}
