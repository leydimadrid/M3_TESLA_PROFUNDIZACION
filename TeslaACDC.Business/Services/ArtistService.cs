namespace TeslaACDC.Business.Services;

using System.Threading.Tasks;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data;
using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Models;
using TeslaACDC.Data.Repository;

public class ArtistService : IArtistService
{
    private readonly TeslaContext _context;
    private IArtistRepository<int, Artist> _artistRepository;

    public ArtistService(TeslaContext context)
    {
        _context = context;
        _artistRepository = new ArtistRepository<int, Artist>(_context);
    }
    public async Task<Artist> AddArtist(Artist artist)
    {
        await _artistRepository.AddAsync(artist);
        return artist;
    }

    public async Task<Artist> FindById(int id)
    {
        var artist = await _artistRepository.FindAsync(id);
        return artist;
    }
}
