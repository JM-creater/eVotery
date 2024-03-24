using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;

namespace OnlineVotingSystem.Persistence.MainFeatures.AdminFeatures.IServices;

public interface IAdminService
{
    Task<IEnumerable<GetAdminAccount>> GetAdminAccountInfo();
}
