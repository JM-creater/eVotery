using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Persistence.Helpers.GenerateTokens;
using OnlineVotingSystem.Persistence.Helpers.Security;

namespace OnlineVotingSystem.Persistence.Context;

public partial class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Voter> Voters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Voter>().HasData(
            new Voter
            {
                Id = Guid.NewGuid(),
                VoterId = Tokens.GenerateVoterID(),
                FirstName = "Joseph Martin",
                LastName = "Garado",
                Password = PasswordHasher.EncryptPassword("123456"),
                DateOfBirth = DateTime.UtcNow,
                Email = "garadojosephmartin98@gmail.com",
                PhoneNumber = "09199431060",
                VoterImages = "PathImages\\VoterImages\\admin picture.png",
                IsActive = true,
                IsValidate = true,
                Role = Domain.Enum.UserRole.Admin,
                Address = "123 Main Street",
                Gender = Domain.Enum.Gender.Male
            }
        );
    }
}
