using System.Text;

namespace OnlineVotingSystem.Persistence.Helpers.GenerateTokens;

public class Tokens
{
    public static int GenerateVoterID()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
}
