namespace TeslaACDC.Controllers;

using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;


[ApiController]
[Route("api/[controller]")]
public class AlbumController : ControllerBase
{
    private readonly IAlbumService _albumService;

    public AlbumController(IAlbumService albumService)
    {
        _albumService = albumService;
    }


    [HttpGet]
    [Route("GetAllAlbum")]
    public async Task<IActionResult> GetAllAlbums()
    {
        var albums = await _albumService.GetAllAlbums();
        return Ok(albums);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> FindAlbumById(int id)
    {
        var album = await _albumService.FindAlbumById(id);
        return Ok(album);
    }

    [HttpPost]
    [Route("CreateAlbum")]
    public async Task<IActionResult> AddAlbum(Album album)
    {
        var newAlbum = await _albumService.AddAlbum(album);
        return Ok(newAlbum);
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateAlbum(int id, Album album)
    {
        var updatedAlbum = await _albumService.UpdateAlbum(id, album);
        return Ok(updatedAlbum);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
        await _albumService.DeleteAlbum(id);
        return NoContent();
    }



}