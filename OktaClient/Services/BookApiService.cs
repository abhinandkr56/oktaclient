using System.Text.Json;
namespace OktaClient.Services;

public class BookApiService
{
    private readonly HttpService _httpService;

    public BookApiService(HttpService httpService)
    {
        _httpService = httpService;
    }
    public async Task<List<string>> GetBooks()
    {
        var httpClient = await _httpService.GetClient();
        var response = await httpClient.GetAsync("http://localhost:5295/book/get");
        var content = await response.Content.ReadAsStringAsync();
        var books = JsonSerializer.Deserialize<List<string>>(content);

        return books;
    }
    
}