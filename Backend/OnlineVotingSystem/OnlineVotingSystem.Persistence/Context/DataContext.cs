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

    public DbSet<User> Users => Set<User>();
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<Vote> Votes => Set<Vote>();
    public DbSet<Election> Elections => Set<Election>();
    public DbSet<Ballot> Ballots => Set<Ballot>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<PartyAffiliation> PartyAffiliations => Set<PartyAffiliation>();
    public DbSet<PersonalDocument> PersonalDocuments => Set<PersonalDocument>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Position
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

        // Government IDs
        Guid Document_ID1 = Guid.NewGuid();
        Guid Document_ID2 = Guid.NewGuid();
        Guid Document_ID3 = Guid.NewGuid();
        Guid Document_ID4 = Guid.NewGuid();
        Guid Document_ID5 = Guid.NewGuid();
        Guid Document_ID6 = Guid.NewGuid();
        Guid Document_ID7 = Guid.NewGuid();
        Guid Document_ID8 = Guid.NewGuid();
        Guid Document_ID9 = Guid.NewGuid();
        Guid Document_ID10 = Guid.NewGuid();
        Guid Document_ID11 = Guid.NewGuid();
        Guid Document_ID12 = Guid.NewGuid();
        Guid Document_ID13 = Guid.NewGuid();
        Guid Document_ID14 = Guid.NewGuid();
        Guid Document_ID15 = Guid.NewGuid();

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
                Gender = "Male",
                PersonalDocumentId = Document_ID4,
                Nationality = "Filipino",
                Occupation = "Admin",
                Religion = "Roman Catholic",
                ZipCode = "1234",
                PIDNumber = "",
                PImage = ""
            }
        );

        modelBuilder.Entity<PersonalDocument>().HasData(
           new PersonalDocument
           {
               Id = Document_ID1,
               Document = "UMID",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID2,
               Document = "Driver’s License",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID3,
               Document = "Professional Regulation Commission ID",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID4,
               Document = "Passport",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID5,
               Document = "Senior Citizen ID",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID6,
               Document = "SSS ID",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID7,
               Document = "Philippine Identification",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID8,
               Document = "NBI Clearance",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID9,
               Document = "BIR",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID10,
               Document = "Pag-ibig ID",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID11,
               Document = "Barangay ID",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID12,
               Document = "Philippine Postal ID",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID13,
               Document = "School ID",
               DateCreated = DateTime.Now
           },
           new PersonalDocument
           {
               Id = Document_ID14,
               Document = "Other valid government-issued IDs",
               DateCreated = DateTime.Now
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
