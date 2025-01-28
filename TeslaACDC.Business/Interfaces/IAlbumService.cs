
using TeslaACDC.Data.Models;

namespace TeslaACDC.Business.Interfaces;

public interface IAlbumService
{
    Task<BaseMessage<Album>> GetAlbums();
    Task<BaseMessage<Album>> FindById(int id);
    Task<BaseMessage<Album>> FindByName(string name);
    Task<BaseMessage<Album>> FindByYear(int year);
    Task<BaseMessage<Album>> FindByRangeYear(int year1, int year2);
    Task<BaseMessage<Album>> FindByNameArtist(string artist);
    Task<BaseMessage<Album>> FindByGender(int gender);

}
