using System.Net;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Business.Services;

public class AlbumService : IAlbumService
{
    private readonly IUnitOfWork _unitOfWork;

    public AlbumService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<BaseMessage<Album>> GetAllAlbums()
    {
        var lista = await _unitOfWork.AlbumRepository.GetAllAsync();
        return lista.Any()
            ? BuildMessage(lista.ToList(), "", HttpStatusCode.OK, lista.Count())
            : BuildMessage(lista.ToList(), "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> AddAlbum(Album album)
    {
        var error = Validate.ValidateNameAlbum(album);
        if (error.Any())
        {
            return BuildMessage(null, string.Join("\n", error), HttpStatusCode.BadRequest, 0);
        }

        await _unitOfWork.AlbumRepository.AddAsync(album);
        await _unitOfWork.SaveAsync();
        return album != null
            ? BuildMessage(new List<Album> { album }, "Álbum agregado exitosamente.", HttpStatusCode.OK, 1)
            : BuildMessage(new List<Album>(), "", HttpStatusCode.InternalServerError, 0);
    }

    public async Task<BaseMessage<Album>> FindAlbumById(int id)
    {
        var album = await _unitOfWork.AlbumRepository.FindAsync(id);
        return album == null
            ? BuildMessage(new List<Album>(), "", HttpStatusCode.NotFound, 0)
            : BuildMessage(new List<Album> { album }, "", HttpStatusCode.OK, 1);
    }

    public async Task<BaseMessage<Album>> FindAlbumByName(string name)
    {
        var lista = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Name.ToLower().Contains(name.ToLower()));
        return lista.Any()
            ? BuildMessage(lista.ToList(), "", HttpStatusCode.OK, lista.Count())
            : BuildMessage(lista.ToList(), "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindAlbumByRange(int year1, int year2)
    {

        var error = Validate.ValidateByRange(year1, year2);
        if (error.Any())
        {
            return BuildMessage(null, string.Join("\n", error), HttpStatusCode.BadRequest, 0);
        }

        var album = await _unitOfWork.AlbumRepository.GetAllAsync(album => album.Year >= year1 && album.Year <= year2);
        return BuildMessage(album.ToList(), "", HttpStatusCode.OK, album.Count());
    }

    public async Task<BaseMessage<Album>> UpdateAlbum(int id, Album album)
    {

        var albumEntity = await _unitOfWork.AlbumRepository.FindAsync(id);
        if (albumEntity == null)
        {
            return BuildMessage(new List<Album>(), "Álbum no encontrado", HttpStatusCode.NotFound, 0);
        }

        albumEntity.Name = album.Name;
        albumEntity.Year = album.Year;
        albumEntity.ArtistId = album.ArtistId;
        albumEntity.Artist = album.Artist;
        albumEntity.Genre = album.Genre;

        _unitOfWork.AlbumRepository.Update(albumEntity);
        await _unitOfWork.SaveAsync();
        return BuildMessage(new List<Album> { album }, "", HttpStatusCode.OK, 1);

    }

    public async Task<BaseMessage<Album>> DeleteAlbum(int id)
    {
        var album = await _unitOfWork.AlbumRepository.FindAsync(id);
        if (album == null)
        {
            return BuildMessage(new List<Album>(), "Álbum no encontrado", HttpStatusCode.NotFound, 0);
        }

        _unitOfWork.AlbumRepository.Delete(album);
        await _unitOfWork.SaveAsync();
        return BuildMessage(new List<Album> { album }, "", HttpStatusCode.OK, 1);
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
