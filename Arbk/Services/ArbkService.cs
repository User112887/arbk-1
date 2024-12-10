using Arbk.Models;
using Newtonsoft.Json;

namespace Arbk.Services;

internal class ArbkService
{
    public static async Task<int> GetNumberOfBusinesses(Dictionary<string, string> headers)
    {
        using HttpClient client = new HttpClient();

        foreach (var header in headers)
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
        }

        var response = await client.GetAsync("https://arbk.rks-gov.net/api/api/Services/StatLlojiBiznesve?Viti=0&KomunaId=0&status=0&Gjuha=sq");

        var responseBodyString = await response.Content.ReadAsStringAsync();

        var list = JsonConvert.DeserializeObject<List<Stats>>(responseBodyString);

        return list.Sum(c => c.NR);
    }

    public static async Task<Business> GetBusiness(int id, Dictionary<string, string> headers)
    {
        using HttpClient client = new HttpClient();

        foreach (var header in headers)
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
        }

        var url = $"https://arbk.rks-gov.net/api/api/Services/TeDhenatBiznesit?nRegjistriId={id}&Gjuha=sq";

        var newRequest = new HttpRequestMessage(HttpMethod.Get, url);

        var response = await client.SendAsync(newRequest);

        var responseBody = await response.Content.ReadAsStringAsync();

        var business = JsonConvert.DeserializeObject<List<Business>>(responseBody);

        return business.First();
    }
}
