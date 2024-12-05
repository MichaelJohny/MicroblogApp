namespace Application.Common.Interfaces;

public interface ICacheService
{
    T GetFromCache<T>(string key) where T : class;
    
    IEnumerable<string> GetFromCache(string key) ;
    void SetCache<T>(string key, T value, int time = 0) where T : class;
}