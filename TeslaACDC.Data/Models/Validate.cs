
namespace TeslaACDC.Data.Models;

public static class Validate
{
    public static List<string> ValidateNameAlbum(Album album)
    {
        var message = new List<string>();

        if (string.IsNullOrEmpty(album.Name))
        {
            message.Add("El nombre es requerido");
        }

        return message;
    }


    public static List<string> ValidateNameArtist(Artist artist)
    {
        var message = new List<string>();

        if (string.IsNullOrEmpty(artist.Name))
        {
            message.Add("El nombre es requerido");
        }

        return message;
    }

    public static List<string> ValidateUniqueArtistName(Artist artist, List<Artist> existingArtists)
    {
        var message = new List<string>();

        if (existingArtists.Any(a => a.Name.Equals(artist.Name, StringComparison.OrdinalIgnoreCase)))
        {
            message.Add("El artista ya existe");
        }

        return message;
    }



    public static List<string> ValidateNameSong(Song song)
    {
        var message = new List<string>();

        if (string.IsNullOrEmpty(song.Name))
        {
            message.Add("El nombre es requerido");
        }

        return message;
    }

    public static List<string> ValidateByRange(int year1, int year2)
    {
        var message = new List<string>();

        int currentYear = DateTime.Now.Year;
        if (year1 < 1901 || year2 > currentYear)
        {
            message.Add($"El año debe estar entre 1901 y {currentYear}.");
        }

        return message;
    }
}
