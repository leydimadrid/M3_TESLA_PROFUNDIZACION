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
    public async Task<BaseMessage<Artist>> GetAllArtist()
    {
        var lista = await _artistRepository.GetAllArtist();
        return lista.Any()
            ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count)
            : BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }


    public async Task<BaseMessage<Artist>> AddArtist(Artist artist)
    {

        var error = Validate.ValidateNameArtist(artist);
        if (error.Any())
        {
            return BuildMessage(null, string.Join("\n", error), HttpStatusCode.BadRequest, 0);
        }

        await _artistRepository.AddArtist(artist);
        return BuildMessage(new List<Artist> { artist }, "Artista agregado exitosamente.", HttpStatusCode.OK, 1);
    }

    public async Task<BaseMessage<Artist>> FindArtistById(int id)
    {
        var artist = await _artistRepository.FindArtistById(id);
        return artist == null
            ? BuildMessage(new List<Artist> { artist }, "", HttpStatusCode.NotFound, 0)
            : BuildMessage(new List<Artist> { artist }, "", HttpStatusCode.OK, 1);

    }

    public async Task<BaseMessage<Artist>> FindArtistByName(string name)
    {
        var artist = await _artistRepository.FindArtistByName(name);
        return artist.Any()
            ? BuildMessage(artist, "", HttpStatusCode.OK, artist.Count())
            : BuildMessage(artist, "", HttpStatusCode.NotFound, 0);
    }


    public async Task<BaseMessage<Artist>> UpdateArtist(int id, Artist artist)
    {
        var artistEntity = await _artistRepository.FindArtistById(id);

        artistEntity.Name = artist.Name;
        artistEntity.Label = artist.Label;
        artistEntity.IsOnTour = artist.IsOnTour;

        return artistEntity == null
            ? BuildMessage(new List<Artist>(), "Artista no encontrado", HttpStatusCode.NotFound, 0)
            : await _artistRepository.UpdateArtist(artistEntity)
                .ContinueWith(_ => BuildMessage(new List<Artist> { artist }, "", HttpStatusCode.OK, 1));
    }

    public async Task<BaseMessage<Artist>> DeleteArtist(int id)
    {
        var artist = await _artistRepository.FindArtistById(id);
        return artist == null
            ? BuildMessage(new List<Artist>(), "Artista no encontrado", HttpStatusCode.InternalServerError, 0)
            : await _artistRepository.DeleteArtist(artist)
                .ContinueWith(_ => BuildMessage(new List<Artist> { artist }, "", HttpStatusCode.OK, 1));

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
