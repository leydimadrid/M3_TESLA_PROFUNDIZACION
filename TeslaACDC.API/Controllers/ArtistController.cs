using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.DTO;
namespace TeslaACDC.Controllers;

[Controller]
[Route("api/[controller]")]
public class ArtistController : ControllerBase
{

    private readonly IArtistService _artistService;

    public ArtistController(IArtistService artistService)
    {
        _artistService = artistService;
    }

    [HttpGet]
    [Route("GetAllArtist")]
    public async Task<IActionResult> ToListAsync()
    {
        var artist = await _artistService.ToListAsync();
        return Ok(artist);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> FindAsync(int id)
    {
        var artist = await _artistService.FindAsync(id);
        return Ok(artist);
    }

    [HttpPost]
    [Route("CreateArtist")]
    public async Task<IActionResult> AddAsync([FromBody] ArtistDTO artist)
    {
        var newArtist = await _artistService.AddAsync(artist);
        return Ok(newArtist);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateArtist(int id, [FromBody] ArtistDTO artist)
    {
        try
        {
            var updatedArtist = await _artistService.UpdateAsync(id, artist);
            return Ok(updatedArtist);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteArtist(int id)
    {
        var result = await _artistService.DeleteAsync(id);
        return Ok(result);
    }
}