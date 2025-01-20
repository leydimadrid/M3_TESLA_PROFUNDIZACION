namespace TeslaACDC.Controllers;

using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Business.Services;
using TeslaACDC.Data.Models;


[ApiController] // @Controller
[Route("api/tesla")]
public class TeslaController : ControllerBase
{
    private readonly IAlbumService _albumService;
    private readonly IMatematicaService _matematicaService;

    public TeslaController(IAlbumService albumService, IMatematicaService matematicaService)
    {
        _albumService = albumService;
        _matematicaService = matematicaService;
    }


    [HttpGet]
    [Route("GetAlbum")]
    public async Task<IActionResult> GetAlbums()
    {
        var lista = await _albumService.GetAlbums();
        return Ok(lista);
    }

    [HttpPost]
    [Route("PostAlbum")]
    public async Task<IActionResult> ArrayAlbums(Album album)
    {
        var lista = await _albumService.AddAlbums(album);
        return Ok(lista);
    }

    [HttpPost]
    [Route("Sum")]
    public async Task<IActionResult> Sum(SumRequest sumRequest)
    {
        var resultado = await _matematicaService.Sum(sumRequest);
        return Ok(resultado);
    }

    [HttpPost]
    [Route("CalcularAreaCuadrado")]
    public async Task<IActionResult> CalcularAreaCuadrado(AreaCuadrado areaCuadrado)
    {
        var resultado = await _matematicaService.AreaCuadrado(areaCuadrado);
        return Ok(resultado);
    }

    [HttpPost]
    [Route("AreaCuadradoCuatroLados")]
    public async Task<IActionResult> AreaCuadradoCuatroLados(AreaCuadradoCuatroLados areaCuadradoCuatroLados)
    {
        var resultado = await _matematicaService.AreaCuadradoCuatroLados(areaCuadradoCuatroLados);
        return Ok(resultado);
    }

    [HttpPost]
    [Route("CalcularAreaTriangulo")]
    public async Task<IActionResult> CalcularAreaTriangulo(AreaTriangulo areaTriangulo)
    {
        try
        {
            var resultado = await _matematicaService.AreaTriangulo(areaTriangulo);
            return Ok(resultado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }

    }

}