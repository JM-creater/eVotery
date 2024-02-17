using OnlineVotingSystem.Domain.Enum;

namespace OnlineVotingSystem.Domain.Responses;

public class ApiResponse
{
    public int ResponseCode { get; set; }
    public string Result { get; set; }
    public string ErrorMessage { get; set; }
    public UserRole UserRole { get; set; }
}
