using RadioFETestApp.Model;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace RadioFETestApp.Data
{
    public class ApiConnector
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConnectionStrings _connectionStrings;

        public ApiConnector(IHttpClientFactory httpClientFactory, ConnectionStrings connectionStrings)
        {
            _httpClientFactory = httpClientFactory;
            _connectionStrings = connectionStrings;
        }

        private async Task<string> PostDataToApi(string url, InputObject input)
        {
            using var client = _httpClientFactory.CreateClient("TestClient");
            client.BaseAddress = new Uri(_connectionStrings.RadioFEApi);

            var json = new JObject();
            json.Add("input", input.Input);


            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(json)));
            var data = new StringContent(encoded, Encoding.UTF8, mediaType: "application/custom");

            var response = await client.PostAsync(url, data).ConfigureAwait(false);

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var returned = JsonConvert.DeserializeObject<JObject>(responseString);

            return returned.Value<string>("message");
        }

        public async Task<string> Get(int id)
        {
            try
            {
                using var client = _httpClientFactory.CreateClient("TestClient");
                client.BaseAddress = new Uri(_connectionStrings.RadioFEApi);

                var response = await client.GetAsync($"api/v1/diff/{id}").ConfigureAwait(false);

                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var returned = JsonConvert.DeserializeObject<JObject>(responseString);

                return returned.Value<string>("message");

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> SetLeftSide(InputObject input)
        {
            try
            {
                return await PostDataToApi($"api/v1/diff/{input.Id}/left", input).ConfigureAwait(false);

            } catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> SetRightSide(InputObject input)
        {
            try
            {
                return await PostDataToApi($"api/v1/diff/{input.Id}/right", input).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
