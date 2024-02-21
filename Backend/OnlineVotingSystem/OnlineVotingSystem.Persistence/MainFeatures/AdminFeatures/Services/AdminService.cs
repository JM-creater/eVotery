using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.AdminFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.AdminFeatures.Services;

public class AdminService : IAdminService
{
    private readonly DataContext context;
    public AdminService(DataContext _context)
    {
        context = _context;
    }

    public async Task<List<User>> GetUsersForAdmin(int voterId)
        => await context.Users
                        .Where(u => u.VoterId == voterId &&
                                    u.Role == Domain.Enum.UserRole.Admin)
                        .ToListAsync();
}
