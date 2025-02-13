using System;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;
using TeslaACDC.Data;
using System.Net;

namespace TeslaACDC.Business.Services;

public class SongService : ISongService
{
    private readonly IUnitOfWork _unitOfWork;

    public SongService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<BaseMessage<Song>> GetAllSongs()
    {
        var lista = await _unitOfWork.SongRepository.GetAllAsync();
        return lista.Any()
            ? BuildMessage(lista.ToList(), "", HttpStatusCode.OK, lista.Count())
            : BuildMessage(lista.ToList(), "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Song>> AddSong(Song song)
    {
        var error = Validate.ValidateNameSong(song);
        if (error.Any())
        {
            return BuildMessage(null, string.Join("\n", error), HttpStatusCode.BadRequest, 0);
        }

        _unitOfWork.SongRepository.AddAsync(song);
        await _unitOfWork.SaveAsync();
        return song != null
            ? BuildMessage(new List<Song> { song }, "Canción agregada exitosamente.", HttpStatusCode.OK, 1)
            : BuildMessage(new List<Song>(), "", HttpStatusCode.InternalServerError, 0);
    }


    public async Task<BaseMessage<Song>> FindSongById(int id)
    {
        var song = await _unitOfWork.SongRepository.FindAsync(id);
        return song == null
            ? BuildMessage(new List<Song> { song }, "", HttpStatusCode.NotFound, 0)
            : BuildMessage(new List<Song> { song }, "", HttpStatusCode.OK, 1);
    }

    public async Task<BaseMessage<Song>> FindSongByName(string name)
    {
        var song = await _unitOfWork.SongRepository.GetAllAsync(x => x.Name.ToLower().Contains(name.ToLower()));
        return song.Any()
            ? BuildMessage(song.ToList(), "", HttpStatusCode.OK, song.Count())
            : BuildMessage(song.ToList(), "", HttpStatusCode.NotFound, 0);
    }


    public async Task<BaseMessage<Song>> UpdateSong(int id, Song song)
    {
        var songEntity = await _unitOfWork.SongRepository.FindAsync(id);
        if (songEntity == null)
        {
            return BuildMessage(new List<Song>(), "Canción no encontrada", HttpStatusCode.NotFound, 0);
        }

        songEntity.Name = song.Name;
        songEntity.DurationSeg = song.DurationSeg;

        _unitOfWork.SongRepository.Update(songEntity);
        await _unitOfWork.SaveAsync();
        return BuildMessage(new List<Song> { song }, "", HttpStatusCode.OK, 1);
    }

    public async Task<BaseMessage<Song>> DeleteSong(int id)
    {
        var song = await _unitOfWork.SongRepository.FindAsync(id);
        if (song == null)
        {
            return BuildMessage(new List<Song>(), "Canción no encontrada", HttpStatusCode.NotFound, 0);
        }
        _unitOfWork.SongRepository.Delete(song);
        await _unitOfWork.SaveAsync();
        return BuildMessage(new List<Song> { song }, "", HttpStatusCode.OK, 1);

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
