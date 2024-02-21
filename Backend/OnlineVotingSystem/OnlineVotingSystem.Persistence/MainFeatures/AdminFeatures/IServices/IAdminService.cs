using OnlineVotingSystem.Domain.Entity;

namespace OnlineVotingSystem.Persistence.MainFeatures.AdminFeatures.IServices;

public interface IAdminService
{
    Task<List<User>> GetUsersForAdmin(int voterId);
}
