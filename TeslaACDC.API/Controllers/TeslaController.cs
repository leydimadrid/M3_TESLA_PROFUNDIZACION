namespace TeslaACDC.Controllers;

using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Business.Services;
using TeslaACDC.Data.DTO;


[ApiController] // @Controller
[Route("api/tesla")]
public class TeslaController : ControllerBase
{
    private readonly IAlbumService _albumService;
    // private readonly IMatematicaService _matematicaService;

    public TeslaController(IAlbumService albumService)
    {
        _albumService = albumService;
    }

    [HttpGet]
    [Route("GetAlbum")]
    public async Task<IActionResult> GetAlbums()
    {
        var lista = await _albumService.GetAlbums();
        return Ok(lista);
    }

    [HttpGet]
    [Route("GetAlbumId")]
    public async Task<IActionResult> FindById(int id)
    {
        var lista = await _albumService.FindById(id);
        return StatusCode((int)lista.StatusCode, lista);
    }

    [HttpGet]
    [Route("GetAlbumByName")]
    public async Task<IActionResult> FindByName(string name)
    {
        var lista = await _albumService.FindByName(name);
        return StatusCode((int)lista.StatusCode, lista);
    }

    [HttpGet]
    [Route("GetAlbumByYear")]
    public async Task<IActionResult> FindByYear(int year)
    {
        var lista = await _albumService.FindByYear(year);
        return StatusCode((int)lista.StatusCode, lista);
    }

    [HttpGet]
    [Route("GetAlbumByRangeYear")]
    public async Task<IActionResult> FindByRangeYear(int year1, int year2)
    {
        var lista = await _albumService.FindByRangeYear(year1, year2);
        return StatusCode((int)lista.StatusCode, lista);
    }

    [HttpGet]
    [Route("GetAlbumByNameArtist")]
    public async Task<IActionResult> FindByNameArtist(string artist)
    {
        var lista = await _albumService.FindByNameArtist(artist);
        return StatusCode((int)lista.StatusCode, lista);
    }

    [HttpGet]
    [Route("GetAlbumByGender")]
    public async Task<IActionResult> FindByGender(int gender)
    {

        var lista = await _albumService.FindByGender(gender);
        return StatusCode((int)lista.StatusCode, lista);
    }



    // [HttpPost]
    // [Route("PostAlbum")]
    // public async Task<IActionResult> ArrayAlbums(Album album)
    // {
    //     var lista = await _albumService.AddAlbums(album);
    //     return Ok(lista);
    // }

    // [HttpPost]
    // [Route("Sum")]
    // public async Task<IActionResult> Sum(Sum sum)
    // {
    //     var resultado = await _matematicaService.Sum(sum);
    //     return Ok("➕ La suma de " + sum.Value1 + " y " + sum.Value2 + " es: " + resultado);
    // }

    // [HttpPost]
    // [Route("CalcularAreaCuadrado")]
    // public async Task<IActionResult> CalcularAreaCuadrado(AreaCuadrado areaCuadrado)
    // {
    //     var resultado = await _matematicaService.AreaCuadrado(areaCuadrado);
    //     return Ok("🟥 El área del cuadrado es: " + resultado);
    // }

    // [HttpPost]
    // [Route("AreaCuadradoCuatroLados")]
    // public async Task<IActionResult> AreaCuadradoCuatroLados(AreaCuadradoCuatroLados lado)
    // {
    //     var resultado = await _matematicaService.AreaCuadradoCuatroLados(lado);
    //     return Ok("🟥 El área del cuadrado es: " + resultado);
    // }

    // [HttpPost]
    // [Route("CalcularAreaTriangulo")]
    // public async Task<IActionResult> CalcularAreaTriangulo(AreaTriangulo areaTriangulo)
    // {
    //     try
    //     {
    //         var resultado = await _matematicaService.AreaTriangulo(areaTriangulo);
    //         return Ok("🔺El área del triángulo es: " + resultado);
    //     }
    //     catch (InvalidOperationException ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }

    // }

}