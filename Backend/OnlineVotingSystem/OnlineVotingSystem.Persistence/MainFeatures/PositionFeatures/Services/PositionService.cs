using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.Services;

public class PositionService : IPositionService
{
    private readonly DataContext context;
    public PositionService(DataContext _context)
    {
        context = _context;
    }

    public async Task<List<Position>> GetAll()
        => await context.Positions.ToListAsync();

    public async Task<Position> GetById(Guid id)
        => await context.Positions
                        .Where(p => p.Id == id)
                        .FirstOrDefaultAsync();
}
