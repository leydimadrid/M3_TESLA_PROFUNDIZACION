using System;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Repository;
using TeslaACDC.Data.Models;
using TeslaACDC.Data;
using System.Net;

namespace TeslaACDC.Business.Services;

public class SongService : ISongService
{
    private ISongRepository<int, Song> _songRepository;

    public SongService(ISongRepository<int, Song> songRepository)
    {
        _songRepository = songRepository;
    }

    public async Task<BaseMessage<Song>> GetAllSongs()
    {
        var lista = await _songRepository.GetAllSongs();
        return lista.Any()
            ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count)
            : BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Song>> AddSong(Song song)
    {
        var error = Validate.ValidateNameSong(song);
        if (error.Any())
        {
            return BuildMessage(null, string.Join("\n", error), HttpStatusCode.BadRequest, 0);
        }

        var addSong = await _songRepository.AddSong(song);
        return addSong != null
            ? BuildMessage(new List<Song> { song }, "Canción agregada exitosamente.", HttpStatusCode.OK, 1)
            : BuildMessage(new List<Song>(), "", HttpStatusCode.InternalServerError, 0);
    }


    public async Task<BaseMessage<Song>> FindSongById(int id)
    {
        var song = await _songRepository.FindSongById(id);
        return song == null
            ? BuildMessage(new List<Song> { song }, "", HttpStatusCode.NotFound, 0)
            : BuildMessage(new List<Song> { song }, "", HttpStatusCode.OK, 1);
    }

    public async Task<BaseMessage<Song>> FindSongByName(string name)
    {
        var song = await _songRepository.FindSongByName(name);
        return song.Any()
            ? BuildMessage(song, "", HttpStatusCode.OK, song.Count())
            : BuildMessage(song, "", HttpStatusCode.NotFound, 0);
    }


    public async Task<BaseMessage<Song>> UpdateSong(int id, Song song)
    {
        var songEntity = await _songRepository.FindSongById(id);

        songEntity.Name = song.Name;
        songEntity.DurationSeg = song.DurationSeg;

        return songEntity == null
            ? BuildMessage(new List<Song>(), "Artista no encontrado", HttpStatusCode.NotFound, 0)
            : await _songRepository.UpdateSong(songEntity)
                .ContinueWith(_ => BuildMessage(new List<Song> { song }, "", HttpStatusCode.OK, 1));
    }

    public async Task<BaseMessage<Song>> DeleteSong(int id)
    {
        var song = await _songRepository.FindSongById(id);
        return song == null
            ? BuildMessage(new List<Song>(), "Esta canción no existe", HttpStatusCode.InternalServerError, 0)
            : await _songRepository.DeleteSong(song)
                .ContinueWith(_ => BuildMessage(new List<Song> { song }, "", HttpStatusCode.OK, 1));

    }

    private BaseMessage<Song> BuildMessage(List<Song> responseElements, string message = "", HttpStatusCode
    statusCode = HttpStatusCode.OK, int totalElements = 0)
    {
        return new BaseMessage<Song>()
        {
            Message = message,
            StatusCode = statusCode,
            TotalElements = totalElements,
            ResponseElements = responseElements
        };
    }
}
