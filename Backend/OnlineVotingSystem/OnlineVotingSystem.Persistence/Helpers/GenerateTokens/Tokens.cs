using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineVotingSystem.Domain.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineVotingSystem.Persistence.Helpers.GenerateTokens;

public class Tokens
{
    private readonly IConfiguration configuration;
    public Tokens(IConfiguration _configuration)
    {
        configuration = _configuration;
    }
    public static int GenerateVoterID()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }

    public static string GenerateToken(User user, IConfiguration configuration)
    {
        var secret = configuration["JWT:Secret"];

        if (string.IsNullOrEmpty(secret))
        {
            throw new InvalidOperationException("JWT Secret is not configured properly.");
        }

        var key = Encoding.ASCII.GetBytes(secret);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddHours(24).ToString())
            }),
            Expires = DateTime.Now.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
