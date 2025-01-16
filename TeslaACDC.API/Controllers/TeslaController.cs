namespace TeslaACDC.Controllers;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Models;

[ApiController] // @Controller
[Route("api/tesla")]
public class TeslaController : ControllerBase
{


    // 1. Método que devuelve un array de álbumes
    [HttpGet]
    [Route("GetAlbum")]
    public async Task<IActionResult> GetAlbums()
    {
        Album album1 = new()
        {
            Nombre = "Back in Black",
            Genero = "Hard Rock",
            Anio = 1980
        };

        Album album2 = new()
        {
            Nombre = "The Dark Side of the Moon",
            Genero = "Rock Progresivo",
            Anio = 1973
        };

        Album album3 = new()
        {
            Nombre = "Mañana será bonito",
            Genero = "Urbano",
            Anio = 2022
        };

        Album[] albums = { album1, album2, album3 };
        return Ok(albums);
    }

    // 2. Método para recibir un álbum en una solicitud POST
    [HttpPost]
    [Route("PostAlbum")]
    public async Task<IActionResult> ArrayAlbums(Album album)
    {

        return Ok("🔹Nombre: " + album.Nombre + "\n🔹Año: " + album.Anio + "\n🔹Género: " + album.Genero);
    }

    //3. Método que suma dos valores y devuelve el resultado

    [HttpPost]
    [Route("Sum")]
    public async Task<IActionResult> Sum(SumRequest sumRequest)
    {
        int result = sumRequest.Value1 + sumRequest.Value2;
        return Ok($"➕ La suma de {sumRequest.Value1} y {sumRequest.Value2} es: {result}");
    }


    //4. Método que calcula el área de un cuadrado
    [HttpPost]
    [Route("CalcularArea")]
    public async Task<IActionResult> CalcularAreaCuadrado(AreaCuadrado areaCuadrado)
    {

        double area = areaCuadrado.Lado * areaCuadrado.Lado;
        return Ok($"🟥 El área del cuadrado es: {area}");
    }


}