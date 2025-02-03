using System;

namespace TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.DTO;


public interface IArtistService
{
    public Task<List<ArtistDTO>> ToListAsync();
    public Task<ArtistDTO> FindAsync(int id);
    public Task<ArtistDTO> AddAsync(ArtistDTO artist);
    public Task<ArtistDTO> UpdateAsync(int id, ArtistDTO artist);
    public Task<bool> DeleteAsync(int id);
}
