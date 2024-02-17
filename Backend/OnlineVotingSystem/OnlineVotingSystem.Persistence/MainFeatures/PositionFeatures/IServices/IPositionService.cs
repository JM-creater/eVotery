using OnlineVotingSystem.Domain.Entity;

namespace OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.IServices;

public interface IPositionService
{
    Task<List<Position>> GetAll();
    Task<Position> GetById(Guid id);
}
