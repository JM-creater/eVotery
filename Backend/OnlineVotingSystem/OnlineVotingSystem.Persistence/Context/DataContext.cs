﻿using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Persistence.Helpers.GenerateTokens;
using OnlineVotingSystem.Persistence.Helpers.Security;

namespace OnlineVotingSystem.Persistence.Context;

public partial class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<Election> Elections { get; set; }
    public DbSet<Ballot> Ballots { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<PartyAffiliation> PartyAffiliations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Guid Position_ID1 = Guid.NewGuid();
        Guid Position_ID2 = Guid.NewGuid();
        Guid Position_ID3 = Guid.NewGuid();
        Guid Position_ID4 = Guid.NewGuid();
        Guid Position_ID5 = Guid.NewGuid();
        Guid Position_ID6 = Guid.NewGuid();
        Guid Position_ID7 = Guid.NewGuid();
        Guid Position_ID8 = Guid.NewGuid();
        Guid Position_ID9 = Guid.NewGuid();
        Guid Position_ID10 = Guid.NewGuid();
        Guid Position_ID11 = Guid.NewGuid();
        Guid Position_ID12 = Guid.NewGuid();
        Guid Position_ID13 = Guid.NewGuid();

        modelBuilder.Entity<User>().HasData(
            new User
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
                Gender = "Male"
            }
        );

        modelBuilder.Entity<Position>().HasData(
            new Position
            {
                Id = Position_ID1,
                Name = "President",
                IsActive = true,
                DateCreated = DateTime.Now
            },
            new Position
            {
                Id = Position_ID2,
                Name = "Vice President",
                IsActive = true,
                DateCreated = DateTime.Now
            },
            new Position
            {
                Id = Position_ID3,
                Name = "Senator",
                IsActive = true,
                DateCreated = DateTime.Now
            },
            new Position
            {
                Id = Position_ID4,
                Name = "Member of the House of Representatives",
                IsActive = true,
                DateCreated = DateTime.Now
            },
            new Position
            {
                Id = Position_ID5,
                Name = "Governor",
                IsActive = true,
                DateCreated = DateTime.Now
            },
            new Position
            {
                Id = Position_ID6,
                Name = "Mayor",
                IsActive = true,
                DateCreated = DateTime.Now
            },
            new Position
            {
                Id = Position_ID7,
                Name = "Vice Governor",
                IsActive = true,
                DateCreated = DateTime.Now
            },
            new Position
            {
                Id = Position_ID8,
                Name = "Vice Mayor",
                IsActive = true,
                DateCreated = DateTime.Now
            },
            new Position
            {
                Id = Position_ID9,
                Name = "Councilor",
                IsActive = true,
                DateCreated = DateTime.Now
            },
            new Position
            {
                Id = Position_ID10,
                Name = "Barangay Captain",
                IsActive = true,
                DateCreated = DateTime.Now
            },
            new Position
            {
                Id = Position_ID11,
                Name = "Barangay Kagawad",
                IsActive = true,
                DateCreated = DateTime.Now
            },
            new Position
            {
                Id = Position_ID12,
                Name = "Sangguniang Kabataan Chairperson",
                IsActive = true,
                DateCreated = DateTime.Now
            },
            new Position
            {
                Id = Position_ID13,
                Name = "Sangguniang Kabataan Kagawad",
                IsActive = true,
                DateCreated = DateTime.Now
            }
        );

        modelBuilder.Entity<Vote>()
            .HasOne(v => v.Voter)
            .WithMany(vt => vt.Votes)
            .HasForeignKey(v => v.VoterId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Vote>()
            .HasOne(v => v.Candidate)
            .WithMany(c => c.Votes)
            .HasForeignKey(v => v.CandidateId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Ballot>()
            .HasOne(b => b.Election)
            .WithMany(e => e.Ballots)
            .HasForeignKey(b => b.ElectionId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Candidate>()
            .HasOne(c => c.Ballot)
            .WithMany(b => b.Candidates)
            .HasForeignKey(c => c.BallotId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Candidate>()
            .HasOne(c => c.Position)
            .WithMany(p => p.Candidates)
            .HasForeignKey(c => c.PositionId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Candidate>()
            .HasOne(c => c.PartyAffiliation)
            .WithMany(pa => pa.Candidates)
            .HasForeignKey(c => c.PartyAffiliationId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
