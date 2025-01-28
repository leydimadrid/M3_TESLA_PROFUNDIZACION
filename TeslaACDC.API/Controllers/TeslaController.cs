namespace TeslaACDC.Controllers;

using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Business.Interfaces;


[ApiController]
[Route("api/tesla")]
public class TeslaController : ControllerBase
{
    private readonly IAlbumService _albumService;

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
}