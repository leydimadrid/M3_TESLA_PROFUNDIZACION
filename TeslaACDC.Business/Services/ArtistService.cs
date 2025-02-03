namespace TeslaACDC.Business.Services;

using System.Net;
using System.Threading.Tasks;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data;
using TeslaACDC.Data.DTO;
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
    public async Task<List<ArtistDTO>> ToListAsync()
    {
        var albums = await _artistRepository.ToListAsync();
        // Mapeo manual de Album a AlbumDTO
        var artist = albums.Select(a => new ArtistDTO
        {
            Name = a.Name,
            Label = a.Label,
            IsOnTour = a.IsOnTour,
        }).ToList();
        return artist;
    }

    public async Task<ArtistDTO> AddAsync(ArtistDTO artist)
    {
        var artistEntity = new Artist
        {
            Name = artist.Name,
            Label = artist.Label,
            IsOnTour = artist.IsOnTour,
        };

        // Llama al repositorio para agregar el álbum
        var addedArtist = await _artistRepository.AddAsync(artistEntity);

        // Mapea la entidad Album a AlbumDTO para devolverlo
        return new ArtistDTO
        {
            Name = addedArtist.Name,
            Label = addedArtist.Label,
            IsOnTour = addedArtist.IsOnTour,
        };
    }

    public async Task<ArtistDTO> FindAsync(int id)
    {
        var artist = await _artistRepository.FindAsync(id);
        if (artist == null)
            throw new KeyNotFoundException($"No se encontró el artista con ID {id}");

        return new ArtistDTO
        {
            Name = artist.Name,
            Label = artist.Label,
            IsOnTour = artist.IsOnTour,
        };
    }

    public async Task<ArtistDTO> UpdateAsync(int id, ArtistDTO artist)
    {
        // Buscar el álbum en el repositorio
        var artistEntity = await _artistRepository.FindAsync(id);
        if (artistEntity == null)
            throw new KeyNotFoundException("El artista no fue encontrado.");

        // Actualizar las propiedades del álbum
        artistEntity.Name = artist.Name;
        artistEntity.Label = artist.Label;
        artistEntity.IsOnTour = artist.IsOnTour;

        // Llamar al repositorio para actualizar el álbum
        var updatedArtist = await _artistRepository.UpdateAsync(artistEntity);

        return new ArtistDTO
        {
            Name = updatedArtist.Name,
            Label = updatedArtist.Label,
            IsOnTour = updatedArtist.IsOnTour,
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _artistRepository.DeleteAsync(id);
    }

    private BaseMessage<Album> BuildMessage(List<Album> responseElements, string message = "", HttpStatusCode
    statusCode = HttpStatusCode.OK, int totalElements = 0)
    {
        return new BaseMessage<Album>()
        {
            Message = message,
            StatusCode = statusCode,
            TotalElements = totalElements,
            ResponseElements = responseElements
        };
    }
}
