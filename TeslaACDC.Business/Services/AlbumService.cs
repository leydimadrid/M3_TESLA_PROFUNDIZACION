using System.Net;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Business.Services;

public class AlbumService : IAlbumService
{
    private IAlbumRepository<int, Album> _albumRepository;

    public AlbumService(IAlbumRepository<int, Album> albumRepository)
    {
        _albumRepository = albumRepository;
    }


    public async Task<BaseMessage<Album>> GetAllAlbums()
    {
        var lista = await _albumRepository.GetAllAlbums();
        return lista.Any()
            ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count)
            : BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> AddAlbum(Album album)
    {
        var error = Validate.ValidateNameAlbum(album);
        if (error.Any())
        {
            return BuildMessage(null, string.Join("\n", error), HttpStatusCode.BadRequest, 0);
        }

        var addAlbum = await _albumRepository.AddAlbum(album);
        return addAlbum != null
            ? BuildMessage(new List<Album> { album }, "Álbum agregado exitosamente.", HttpStatusCode.OK, 1)
            : BuildMessage(new List<Album>(), "", HttpStatusCode.InternalServerError, 0);
    }

    public async Task<BaseMessage<Album>> FindAlbumById(int id)
    {
        var album = await _albumRepository.FindAlbumById(id);
        return album == null
            ? BuildMessage(new List<Album>(), "", HttpStatusCode.NotFound, 0)
            : BuildMessage(new List<Album> { album }, "", HttpStatusCode.OK, 1);
    }

    public async Task<BaseMessage<Album>> FindAlbumByName(string name)
    {
        var album = await _albumRepository.FindAlbumByName(name);
        return album.Any()
            ? BuildMessage(album, "", HttpStatusCode.OK, album.Count())
            : BuildMessage(album, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindAlbumByRange(int year1, int year2)
    {

        var error = Validate.ValidateByRange(year1, year2);
        if (error.Any())
        {
            return BuildMessage(null, string.Join("\n", error), HttpStatusCode.BadRequest, 0);
        }

        var album = await _albumRepository.FindAlbumByRange(year1, year2);
        return BuildMessage(album, "", HttpStatusCode.OK, album.Count());
    }

    public async Task<BaseMessage<Album>> UpdateAlbum(int id, Album album)
    {

        var albumEntity = await _albumRepository.FindAlbumById(id);
        albumEntity.Name = album.Name;
        albumEntity.Year = album.Year;
        albumEntity.ArtistId = album.ArtistId;
        albumEntity.Artist = album.Artist;
        albumEntity.Genre = album.Genre;
        return albumEntity == null
            ? BuildMessage(new List<Album>(), "Álbum no encontrado", HttpStatusCode.NotFound, 0)
            : await _albumRepository.UpdateAlbum(albumEntity)
                .ContinueWith(_ => BuildMessage(new List<Album> { album }, "", HttpStatusCode.OK, 1));

    }

    public async Task<BaseMessage<Album>> DeleteAlbum(int id)
    {
        var album = await _albumRepository.FindAlbumById(id);
        return album == null
            ? BuildMessage(new List<Album>(), "Álbum no encontrado", HttpStatusCode.InternalServerError, 0)
            : await _albumRepository.DeleteAlbum(album)
                .ContinueWith(_ => BuildMessage(new List<Album> { album }, "", HttpStatusCode.OK, 1));
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
