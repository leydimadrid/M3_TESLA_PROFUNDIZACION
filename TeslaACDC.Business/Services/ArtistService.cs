namespace TeslaACDC.Business.Services;

using System.Net;
using System.Threading.Tasks;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data;
using TeslaACDC.Data.Models;

public class ArtistService : IArtistService
{
    private readonly IUnitOfWork _unitOfWork;

    public ArtistService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseMessage<Artist>> GetAllArtist()
    {
        var lista = await _unitOfWork.ArtistRepository.GetAllAsync();
        return lista.Any()
            ? BuildMessage(lista.ToList(), "", HttpStatusCode.OK, lista.Count())
            : BuildMessage(lista.ToList(), "", HttpStatusCode.NotFound, 0);
    }


    public async Task<BaseMessage<Artist>> AddArtist(Artist artist)
    {

        var error = Validate.ValidateNameArtist(artist);

        var existingArtists = await _unitOfWork.ArtistRepository.GetAllAsync();

        var nameUnique = Validate.ValidateUniqueArtistName(artist, existingArtists.ToList());

        if (error.Any() || nameUnique.Any())
        {
            return BuildMessage(null, string.Join("\n", error.Concat(nameUnique)), HttpStatusCode.BadRequest, 0);
        }

        await _unitOfWork.ArtistRepository.AddAsync(artist);
        await _unitOfWork.SaveAsync();
        return BuildMessage(new List<Artist> { artist }, "Artista agregado exitosamente.", HttpStatusCode.OK, 1);
    }

    public async Task<BaseMessage<Artist>> FindArtistById(int id)
    {
        var artist = await _unitOfWork.ArtistRepository.FindAsync(id);
        return artist == null
            ? BuildMessage(new List<Artist> { artist }, "", HttpStatusCode.NotFound, 0)
            : BuildMessage(new List<Artist> { artist }, "", HttpStatusCode.OK, 1);

    }

    public async Task<BaseMessage<Artist>> FindArtistByName(string name)
    {
        var artist = await _unitOfWork.ArtistRepository.GetAllAsync(x => x.Name.ToLower().Contains(name.ToLower()));
        return artist.Any()
            ? BuildMessage(artist.ToList(), "", HttpStatusCode.OK, artist.Count())
            : BuildMessage(artist.ToList(), "", HttpStatusCode.NotFound, 0);
    }


    public async Task<BaseMessage<Artist>> UpdateArtist(int id, Artist artist)
    {
        var artistEntity = await _unitOfWork.ArtistRepository.FindAsync(id);
        if (artistEntity == null)
        {
            return BuildMessage(new List<Artist>(), "Artista no encontrado", HttpStatusCode.NotFound, 0);
        }


        artistEntity.Name = artist.Name;
        artistEntity.Label = artist.Label;
        artistEntity.IsOnTour = artist.IsOnTour;

        _unitOfWork.ArtistRepository.Update(artistEntity);
        await _unitOfWork.SaveAsync();
        return BuildMessage(new List<Artist> { artist }, "", HttpStatusCode.OK, 1);
    }

    public async Task<BaseMessage<Artist>> DeleteArtist(int id)
    {
        var artist = await _unitOfWork.ArtistRepository.FindAsync(id);
        if (artist == null)
        {
            return BuildMessage(new List<Artist>(), "Artista no encontrado", HttpStatusCode.NotFound, 0);
        }

        _unitOfWork.ArtistRepository.Delete(artist);
        await _unitOfWork.SaveAsync();
        return BuildMessage(new List<Artist> { artist }, "", HttpStatusCode.OK, 1);

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
