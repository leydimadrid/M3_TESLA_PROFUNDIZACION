using System.ComponentModel.DataAnnotations.Schema;

namespace TeslaACDC.Data.Models;

public class Album : BaseEntity<int>
{
    public string Name { get; set; } = String.Empty;
    public int Year { get; set; }
    public Genre Genre { get; set; }
    [ForeignKey("Artist")]
    public int ArtistId { get; set; }
    public virtual Artist? Artist { get; set; }
}

public enum Genre
{
    Reggaeton,
    Vallenato,
    Champeta,
    Unknown
}

