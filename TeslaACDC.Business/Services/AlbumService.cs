using System.Net;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Business.Services;

public class AlbumService : IAlbumService
{
    private List<Album> _listaAlbum = new();

    public AlbumService()
    {
        _listaAlbum.Add(new()
        {
            Name = "Un Verano Sin Ti",
            Gender = Gender.Reggaeton,
            Year = 2022,
            Id = 1,
            Artist = new() { Id = 0, Name = "Bad Bunny", Label = "Rimas Entertainment", IsOnTour = true }
        });
        _listaAlbum.Add(new()
        {
            Name = "El Cantinero",
            Gender = Gender.Vallenato,
            Year = 2009,
            Id = 2,
            Artist = new() { Id = 1, Name = "Jorge Celedón", Label = "Sony Music", IsOnTour = false }
        });
        _listaAlbum.Add(new()
        {
            Name = "Deja Vu",
            Gender = Gender.Champeta,
            Year = 2013,
            Id = 3,
            Artist = new() { Id = 2, Name = "Kevin Flórez", Label = "Independiente", IsOnTour = true }
        });
        _listaAlbum.Add(new()
        {
            Name = "En Letra de Otro",
            Gender = Gender.Reggaeton,
            Year = 2018,
            Id = 4,
            Artist = new() { Id = 3, Name = "Farruko", Label = "Sony Music Latin", IsOnTour = false }
        });
        _listaAlbum.Add(new()
        {
            Name = "Tierra de Cantores",
            Gender = Gender.Vallenato,
            Year = 2001,
            Id = 5,
            Artist = new() { Id = 4, Name = "Iván Villazón", Label = "Codiscos", IsOnTour = true }
        });
        _listaAlbum.Add(new()
        {
            Name = "La Suegra",
            Gender = Gender.Champeta,
            Year = 2015,
            Id = 6,
            Artist = new() { Id = 5, Name = "Mr. Black", Label = "Independiente", IsOnTour = true }
        });
        _listaAlbum.Add(new()
        {
            Name = "Los Reyes del Movimiento",
            Gender = Gender.Reggaeton,
            Year = 2014,
            Id = 7,
            Artist = new() { Id = 6, Name = "Plan B", Label = "Pina Records", IsOnTour = false }
        });
        _listaAlbum.Add(new()
        {
            Name = "El Binomio de Oro de América",
            Gender = Gender.Vallenato,
            Year = 1982,
            Id = 8,
            Artist = new() { Id = 7, Name = "Binomio de Oro", Label = "Codiscos", IsOnTour = true }
        });
        _listaAlbum.Add(new()
        {
            Name = "Champeta Feeling",
            Gender = Gender.Champeta,
            Year = 2020,
            Id = 9,
            Artist = new() { Id = 8, Name = "Twister El Rey", Label = "Independiente", IsOnTour = true }
        });
        _listaAlbum.Add(new()
        {
            Name = "1 de 1",
            Gender = Gender.Reggaeton,
            Year = 2020,
            Id = 10,
            Artist = new() { Id = 9, Name = "Daddy Yankee", Label = "El Cartel Records", IsOnTour = false }
        });

        Album album = new Album();
    }

    public async Task<BaseMessage<Album>> GetAlbums()
    {
        return BuildMessage(_listaAlbum, "", HttpStatusCode.OK, _listaAlbum.Count);
    }

    public async Task<BaseMessage<Album>> FindById(int id)
    {
        var lista = _listaAlbum.Where(album => album.Id == id).ToList();
        return lista.Any() ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count) :
        BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByName(string name)
    {
        var lista = _listaAlbum.Where(album => album.Name.ToLower().Contains(name.ToLower())).ToList();
        return lista.Any() ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count) :
        BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByNameArtist(string artist)
    {
        var lista = _listaAlbum.Where(album => album.Artist.Name.ToLower().Contains(artist.ToLower())).ToList();
        return lista.Any() ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count) :
        BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByRangeYear(int year1, int year2)
    {
        var lista = _listaAlbum.Where(album => album.Year >= year1 && album.Year <= year2).ToList();
        return lista.Any() ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count) :
        BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByYear(int year)
    {
        var lista = _listaAlbum.Where(album => album.Year == year).ToList();
        return lista.Any() ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count) :
        BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByGender(int gender)
    {

        var lista = _listaAlbum.Where(album => album.Gender == (Gender)gender).ToList();
        return lista.Any() ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count) :
            BuildMessage(lista, "Género no encontrado", HttpStatusCode.NotFound, 0);
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
