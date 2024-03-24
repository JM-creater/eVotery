using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Dtos;
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

    public async Task<IEnumerable<GetAdminAccount>> GetAdminAccountInfo()
    {
        var admin = await context.Users
                                 .Where(u => u.Role == Domain.Enum.UserRole.Admin)
                                 .ToListAsync();

        var account = admin.Select(a => new GetAdminAccount
        {
            FirstName = a.FirstName,
            LastName = a.LastName,
            Email = a.Email,
            Password = a.Password,
            Address = a.Address,
            PhoneNumber = a.PhoneNumber,
            IsValidate = a.IsValidate,
            IsActive = a.IsActive,
            VerificationStatus = a.VerificationStatus,
            Role = a.Role
        });

        return account;
    }
        
}
