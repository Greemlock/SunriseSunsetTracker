using Newtonsoft.Json;

namespace SunriseSunsetTracker.Api.Utils.Extensions;

public static class HttpHelper
{
    public static async Task<T> DeserializeResponseAsync<T>(this HttpResponseMessage response)
    {
        if (response == null)
            throw new ArgumentNullException(nameof(response), "Response cannot be null!");

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}!");

        if (response.Content is null)
            throw new HttpRequestException("Response content is null!");

        var content = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(content))
            throw new HttpRequestException("Response content is empty!");

        try
        {
            var result = JsonConvert.DeserializeObject<T>(content);
            
            if (result is null)
                throw new HttpRequestException("Error deserializing response content!");

            return result;
        }
        catch (Exception ex)
        {
            throw new HttpRequestException("Error deserializing response content!", ex);
        }
    }

}