using Newtonsoft.Json;

namespace OnlineVotingSystem.Domain.Responses;

public class SaveResponse
{
    public int ResponseCode { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? ErrorMessage { get; set; }
    public string? Token { get; set; }
}
