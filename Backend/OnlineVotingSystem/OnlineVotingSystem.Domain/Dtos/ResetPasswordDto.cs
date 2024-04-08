namespace OnlineVotingSystem.Domain.Dtos;

public record ResetPasswordDto
{
    public string Token { get; set; }
    public string NewPassword { get; set; }
}
