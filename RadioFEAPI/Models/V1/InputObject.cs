using Newtonsoft.Json;

namespace RadioFEAPI.Models.V1
{
    public class InputObject
    {
        [JsonProperty("input")]
        public string Input { get; init; }

    }
}
