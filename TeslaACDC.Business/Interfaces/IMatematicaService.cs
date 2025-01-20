using System;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Business.Interfaces;

public interface IMatematicaService
{
    Task<string> Sum(SumRequest sumRequest);
    Task<string> AreaCuadrado(AreaCuadrado areaCuadrado);
    Task<string> AreaTriangulo(AreaTriangulo areaTriangulo);
    Task<string> AreaCuadradoCuatroLados(AreaCuadradoCuatroLados areaCuadradoCuatroLados);
}
