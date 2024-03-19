using OnlineVotingSystem.Domain.Enum;

namespace OnlineVotingSystem.Domain.Responses;

public class ApiResponse<T>
{
    public int ResponseCode { get; set; }
    public T Result { get; set; }
    public string ErrorMessage { get; set; }
    public UserRole UserRole { get; set; }
}
