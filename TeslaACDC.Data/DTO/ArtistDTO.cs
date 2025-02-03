using System;

namespace TeslaACDC.Data.DTO;

public class ArtistDTO
{
    public string Name { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public bool IsOnTour { get; set; }
}
