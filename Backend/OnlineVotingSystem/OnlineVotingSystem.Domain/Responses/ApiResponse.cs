using Newtonsoft.Json;
using OnlineVotingSystem.Domain.Enum;

namespace OnlineVotingSystem.Domain.Responses;

public class ApiResponse<T>
{
    public int ResponseCode { get; set; }
    public T Result { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string ErrorMessage { get; set; }
    public UserRole? UserRole { get; set; }
}
