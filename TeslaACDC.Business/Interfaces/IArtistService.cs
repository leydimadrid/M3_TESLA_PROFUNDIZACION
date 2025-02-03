using System;

namespace TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;


public interface IArtistService
{
    public Task<Artist> FindById(int id);
    public Task<Artist> AddArtist(Artist artist);
}
