using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Entity;
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
                VoterId = 20163482,
                FirstName = "Joseph Martin",
                LastName = "Garado",
                Password = PasswordHasher.EncryptPassword("123456"),
                Email = "garadojosephmartin98@gmail.com",
                PhoneNumber = "09199431060",
                VoterImages = "PathImages\\VoterImages\\admin picture.png",
                IsActive = true,
                IsValidate = true,
                Role = Domain.Enum.UserRole.Admin,
                Address = "123 Main Street"
            }
        );
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
