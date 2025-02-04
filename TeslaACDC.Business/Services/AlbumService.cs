using System.Net;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data;
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


    public async Task<List<Album>> GetAllAlbums()
    {
        return await _albumRepository.ToListAsync();
    }

    public async Task<Album> AddAlbum(Album album)
    {
        var albumEntity = new Album
        {
            Name = album.Name,
            Year = album.Year,
            ArtistId = album.ArtistId,
            Artist = album.Artist,
            Genre = album.Genre
        };
        var addedAlbum = await _albumRepository.AddAsync(albumEntity);
        return addedAlbum;
    }

    public async Task<Album> FindAlbumById(int id)
    {
        var album = await _albumRepository.FindAsync(id);
        if (album == null)
        {
            throw new KeyNotFoundException($"No se encontró el álbum con ID {id}");
        }

        return album;
    }

    public async Task<Album> UpdateAlbum(int id, Album album)
    {
        var albumEntity = await _albumRepository.FindAsync(id);
        if (albumEntity == null)
        {
            throw new KeyNotFoundException("El álbum no fue encontrado.");
        }

        albumEntity.Name = album.Name;
        albumEntity.Year = album.Year;
        albumEntity.ArtistId = album.ArtistId;
        albumEntity.Artist = album.Artist;
        albumEntity.Genre = album.Genre;

        var updatedAlbum = await _albumRepository.UpdateAsync(albumEntity);
        return updatedAlbum;
    }

    public async Task DeleteAlbum(int id)
    {
        var album = await _albumRepository.FindAsync(id);
        if (album == null)
        {
            throw new KeyNotFoundException("El álbum no fue encontrado.");
        }
        await _albumRepository.DeleteAsync(album);
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
