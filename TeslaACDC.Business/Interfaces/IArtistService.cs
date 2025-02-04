using System;

namespace TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;


public interface IArtistService
{
    public Task<List<Artist>> GetAllArtist();
    public Task<Artist> FindArtistById(int id);
    public Task<Artist> AddArtist(Artist artist);
    public Task<Artist> UpdateArtist(int id, Artist artist);
    public Task DeleteArtist(int id);
}
