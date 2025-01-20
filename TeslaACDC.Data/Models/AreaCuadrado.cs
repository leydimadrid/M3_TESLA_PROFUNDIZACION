using System;

namespace TeslaACDC.Data.Models;

public class AreaCuadrado
{
    public int Lado { get; set; }

    public float CalcularArea()
    {
        return Lado * Lado;
    }
}
