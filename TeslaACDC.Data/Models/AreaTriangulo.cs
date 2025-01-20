using System;

namespace TeslaACDC.Data.Models;

public class AreaTriangulo
{
    public float Base { get; set; }
    public float Altura { get; set; }


    public float CalcularArea()
    {
        return Base * Altura / 2;
    }
}
