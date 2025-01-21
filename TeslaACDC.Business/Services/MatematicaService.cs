using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.DTO;

namespace TeslaACDC.Business.Services;

public class MatematicaService : IMatematicaService
{
    public async Task<double> AreaCuadrado(AreaCuadrado areaCuadrado)
    {
        double resultado = areaCuadrado.Lado * areaCuadrado.Lado;
        return resultado;
    }

    public async Task<double> AreaCuadradoCuatroLados(AreaCuadradoCuatroLados lado)
    {
        if (lado.Lado1 == lado.Lado2 && lado.Lado2
            == lado.Lado3 && lado.Lado3 == lado.Lado4)
        {
            double resultado = lado.Lado1 * lado.Lado1;
            return resultado;
        }
        else
        {
            throw new InvalidOperationException("Todos los lados de un cuadrado deben ser iguales.");
        }
    }

    public async Task<double> AreaTriangulo(AreaTriangulo areaTriangulo)
    {
        double resultado = areaTriangulo.Base * areaTriangulo.Altura / 2;
        return resultado;
    }

    public async Task<double> Sum(Sum sum)
    {
        double resultado = sum.Value1 + sum.Value2;
        return resultado;
    }
}
