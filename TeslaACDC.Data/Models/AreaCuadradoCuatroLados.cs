namespace TeslaACDC.Data.Models;

public class AreaCuadradoCuatroLados
{
    public float Lado1 { get; set; }
    public float Lado2 { get; set; }
    public float Lado3 { get; set; }
    public float Lado4 { get; set; }


    public float CalcularArea()
    {
        if (Lado1 == Lado2 && Lado2 == Lado3 && Lado3 == Lado4)
        {
            return Lado1 * Lado1;
        }
        else { throw new InvalidOperationException("Todos los lados de un cuadrado deben ser iguales."); }
    }
}

