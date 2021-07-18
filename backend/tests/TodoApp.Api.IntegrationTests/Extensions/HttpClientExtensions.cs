using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace TodoApp.Api.IntegrationTests.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ConvertAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<T>(responseString);

            return responseObject;
        }
    }
}
