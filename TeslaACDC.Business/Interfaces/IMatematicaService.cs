using System;
using TeslaACDC.Data.DTO;

namespace TeslaACDC.Business.Interfaces;

public interface IMatematicaService
{
    Task<double> Sum(Sum sum);
    Task<double> AreaCuadrado(AreaCuadrado areaCuadrado);
    Task<double> AreaTriangulo(AreaTriangulo areaTriangulo);
    Task<double> AreaCuadradoCuatroLados(AreaCuadradoCuatroLados areaCuadradoCuatroLados);
}
