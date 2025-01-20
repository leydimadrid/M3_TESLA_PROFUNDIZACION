using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Business.Services;

public class MatematicaService : IMatematicaService
{
    public async Task<string> AreaCuadrado(AreaCuadrado areaCuadrado)
    {
        double resultado = areaCuadrado.CalcularArea();
        return await Task.FromResult($"🟥 El área del cuadrado es: {resultado}");
    }

    public async Task<string> AreaCuadradoCuatroLados(AreaCuadradoCuatroLados areaCuadradoCuatroLados)
    {
        double resultado = areaCuadradoCuatroLados.CalcularArea();
        return await Task.FromResult($"🟥 El área del cuadrado es: {resultado}");
    }

    public async Task<string> AreaTriangulo(AreaTriangulo areaTriangulo)
    {
        double resultado = areaTriangulo.CalcularArea();
        return await Task.FromResult($"🔺El área del triángulo es: {resultado}");
    }

    public async Task<string> Sum(SumRequest sumRequest)
    {
        double resultado = sumRequest.Sum();
        return await Task.FromResult($"➕ La suma de {sumRequest.Value1} y {sumRequest.Value2} es: {resultado}");
    }
}
