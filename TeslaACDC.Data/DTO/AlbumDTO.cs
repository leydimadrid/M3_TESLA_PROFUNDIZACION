using System;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Data.DTO;

public class AlbumDTO
{
    public string Name { get; set; } = String.Empty;
    public int Year { get; set; }
    public int ArtistId { get; set; }
    public Gender Gender { get; set; }
}
