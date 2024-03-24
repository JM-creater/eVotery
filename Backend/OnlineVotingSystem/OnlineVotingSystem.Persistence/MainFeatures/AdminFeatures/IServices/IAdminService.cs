using OnlineVotingSystem.Domain.Dtos;

namespace OnlineVotingSystem.Persistence.MainFeatures.AdminFeatures.IServices;

public interface IAdminService
{
    Task<IEnumerable<GetAdminAccount>> GetAdminAccountInfo();
}
