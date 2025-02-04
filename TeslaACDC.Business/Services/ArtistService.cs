namespace TeslaACDC.Business.Services;

using System.Net;
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
    public async Task<List<Artist>> GetAllArtist()
    {
        return await _artistRepository.ToListAsync();
    }

    public async Task<Artist> AddArtist(Artist artist)
    {
        var artistEntity = new Artist
        {
            Name = artist.Name,
            Label = artist.Label,
            IsOnTour = artist.IsOnTour,
        };

        var addedArtist = await _artistRepository.AddAsync(artistEntity);
        return addedArtist;
    }

    public async Task<Artist> FindArtistById(int id)
    {
        var artist = await _artistRepository.FindAsync(id);
        if (artist == null)
        {
            throw new KeyNotFoundException($"No se encontró el artista con ID {id}");
        }
        return artist;
    }

    public async Task<Artist> UpdateArtist(int id, Artist artist)
    {
        var artistEntity = await _artistRepository.FindAsync(id);
        if (artistEntity == null)
        {
            throw new KeyNotFoundException("El artista no fue encontrado.");
        }

        artistEntity.Name = artist.Name;
        artistEntity.Label = artist.Label;
        artistEntity.IsOnTour = artist.IsOnTour;


        var updatedArtist = await _artistRepository.UpdateAsync(artistEntity);
        return updatedArtist;
    }

    public async Task DeleteArtist(int id)
    {
        var artist = await _artistRepository.FindAsync(id);
        if (artist == null)
        {
            throw new KeyNotFoundException("El artist no fue encontrado.");
        }
        await _artistRepository.DeleteAsync(artist);
    }

    private BaseMessage<Artist> BuildMessage(List<Artist> responseElements, string message = "", HttpStatusCode
    statusCode = HttpStatusCode.OK, int totalElements = 0)
    {
        return new BaseMessage<Artist>()
        {
            Message = message,
            StatusCode = statusCode,
            TotalElements = totalElements,
            ResponseElements = responseElements
        };
    }
}
