using System.Net;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data;
using TeslaACDC.Data.DTO;
using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Models;
using TeslaACDC.Data.Repository;

namespace TeslaACDC.Business.Services;

public class AlbumService : IAlbumService
{
    private readonly TeslaContext _context;
    private IAlbumRepository<int, Album> _albumRepository;

    public AlbumService(TeslaContext context)
    {
        _context = context;
        _albumRepository = new AlbumRepository<int, Album>(_context);
    }


    public async Task<List<AlbumDTO>> ToListAsync()
    {
        var albums = await _albumRepository.ToListAsync();
        // Mapeo manual de Album a AlbumDTO
        var albumDTOs = albums.Select(a => new AlbumDTO
        {
            Name = a.Name,
            Year = a.Year,
            ArtistId = a.ArtistId,
            Gender = a.Gender
        }).ToList();
        return albumDTOs;
    }

    public async Task<AlbumDTO> AddAsync(AlbumDTO album)
    {
        var albumEntity = new Album
        {
            Name = album.Name,
            Year = album.Year,
            ArtistId = album.ArtistId,
            Gender = album.Gender
        };

        // Llama al repositorio para agregar el álbum
        var addedAlbum = await _albumRepository.AddAsync(albumEntity);

        // Mapea la entidad Album a AlbumDTO para devolverlo
        return new AlbumDTO
        {
            Name = addedAlbum.Name,
            Year = addedAlbum.Year,
            ArtistId = addedAlbum.ArtistId,
            Gender = addedAlbum.Gender
        };
    }

    public async Task<AlbumDTO> FindAsync(int id)
    {
        var album = await _albumRepository.FindAsync(id);
        if (album == null)
            throw new KeyNotFoundException($"No se encontró el álbum con ID {id}");

        return new AlbumDTO
        {
            Name = album.Name,
            Year = album.Year,
            ArtistId = album.ArtistId,
            Gender = album.Gender
        };
    }

    public async Task<AlbumDTO> UpdateAsync(int id, AlbumDTO album)
    {
        // Buscar el álbum en el repositorio
        var albumEntity = await _albumRepository.FindAsync(id);
        if (albumEntity == null)
            throw new KeyNotFoundException("El álbum no fue encontrado.");

        // Actualizar las propiedades del álbum con los datos del DTO
        albumEntity.Name = album.Name;
        albumEntity.Year = album.Year;
        albumEntity.ArtistId = album.ArtistId;
        albumEntity.Gender = album.Gender;

        // Llamar al repositorio para actualizar el álbum
        var updatedAlbum = await _albumRepository.UpdateAsync(albumEntity);

        // Mapear la entidad actualizada a un DTO
        return new AlbumDTO
        {
            Name = updatedAlbum.Name,
            Year = updatedAlbum.Year,
            ArtistId = updatedAlbum.ArtistId,
            Gender = updatedAlbum.Gender
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _albumRepository.DeleteAsync(id);
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
