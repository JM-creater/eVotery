using Newtonsoft.Json;
using OnlineVotingSystem.Domain.Enum;

namespace OnlineVotingSystem.Domain.Responses;

public class ApiResponse<T>
{
    public int ResponseCode { get; set; }
    public T? Result { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? ErrorMessage { get; set; }
    public UserRole? UserRole { get; set; }
    public string? Token { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Guid? UserId { get; set; }
}
