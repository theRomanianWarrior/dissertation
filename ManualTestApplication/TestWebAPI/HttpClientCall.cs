using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AttractionsCsvGenerator;

public static class HttpClientCall
{
    public static async Task<T> GetRequestAsync<T>(string uri)
    {
        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
        
               return JsonSerializer.Deserialize<T>(responseBody)!;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return default!;
    }
    
    public static async Task<TOut> PostRequestAsync<TIn, TOut>(string uri, TIn content)
    {
        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var diud = JsonSerializer.Serialize(content);
            var serialized = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(uri, serialized);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
    
            return JsonSerializer.Deserialize<TOut>(responseBody)!;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return default!;
    }
}