namespace TeslaACDC.Controllers;

using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.DTO;


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
    public async Task<IActionResult> ToListAsync()
    {
        var albums = await _albumService.ToListAsync();
        return Ok(albums);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> FindAsync(int id)
    {
        var album = await _albumService.FindAsync(id);
        return Ok(album);
    }

    [HttpPost]
    [Route("CreateAlbum")]
    public async Task<IActionResult> AddAsync(AlbumDTO album)
    {
        var newAlbum = await _albumService.AddAsync(album);
        return Ok(newAlbum);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAlbum(int id, AlbumDTO album)
    {
        try
        {
            var updatedAlbum = await _albumService.UpdateAsync(id, album);
            return Ok(updatedAlbum);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
        var result = await _albumService.DeleteAsync(id);
        return Ok(result);
    }



}