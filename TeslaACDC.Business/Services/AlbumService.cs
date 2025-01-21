using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.DTO;

namespace TeslaACDC.Business.Services;

public class AlbumService : IAlbumService
{

    public async Task<List<Album>> GetAlbums()
    {
        Album album1 = new()
        {
            Nombre = "Back in Black",
            Genero = "Hard Rock",
            Anio = 1980
        };

        Album album2 = new()
        {
            Nombre = "The Dark Side of the Moon",
            Genero = "Rock Progresivo",
            Anio = 1973
        };

        Album album3 = new()
        {
            Nombre = "Mañana será bonito",
            Genero = "Urbano",
            Anio = 2022
        };

        return new List<Album> { album1, album2, album3 };
    }

    public async Task<List<Album>> AddAlbums(Album album)
    {
        return new List<Album> { album };
    }
}
