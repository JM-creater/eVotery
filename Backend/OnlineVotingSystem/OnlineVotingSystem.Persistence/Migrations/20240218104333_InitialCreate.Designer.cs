﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineVotingSystem.Persistence.Context;

#nullable disable

namespace OnlineVotingSystem.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240218104333_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Ballot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BallotId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ElectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BallotId");

                    b.HasIndex("ElectionId");

                    b.ToTable("Ballots");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BallotId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<Guid>("PositionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BallotId");

                    b.HasIndex("PositionId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Election", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Elections");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Positions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5b25eed3-7da1-42ab-a0b6-4f9ab77df936"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7660),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "President"
                        },
                        new
                        {
                            Id = new Guid("64939090-8a63-4681-b1f5-f1462d7b4b92"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7670),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice President"
                        },
                        new
                        {
                            Id = new Guid("2c7e614d-80a6-423c-9eab-dd6ca84b1979"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7671),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Senator"
                        },
                        new
                        {
                            Id = new Guid("89ecb9f7-ea68-46db-8765-4903b98c7ca3"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7672),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Member of the House of Representatives"
                        },
                        new
                        {
                            Id = new Guid("944524cb-e51f-4777-96f5-37ce27d6b879"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7674),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Governor"
                        },
                        new
                        {
                            Id = new Guid("aa64c7ac-a520-491c-bdce-31c2a3e972a7"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7675),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Mayor"
                        },
                        new
                        {
                            Id = new Guid("985f457c-fed2-44b6-82c3-b5bc43505469"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7676),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice Governor"
                        },
                        new
                        {
                            Id = new Guid("d36ed6e8-be2d-4c20-8396-edf5e3fa9009"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7677),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice Mayor"
                        },
                        new
                        {
                            Id = new Guid("8deb12e2-0275-4891-80be-07bb7c7a4f4d"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7678),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Councilor"
                        },
                        new
                        {
                            Id = new Guid("d73f87f4-562c-4a7b-b305-9bfe37b7fac7"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7679),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Barangay Captain"
                        },
                        new
                        {
                            Id = new Guid("3972b0e7-175c-4a90-a900-385f54ebe6b2"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7680),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Barangay Kagawad"
                        },
                        new
                        {
                            Id = new Guid("784d1fc9-cbe0-445e-9982-68de07440627"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7681),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Sangguniang Kabataan Chairperson"
                        },
                        new
                        {
                            Id = new Guid("dcd3480d-1eff-424a-9b4a-d5f52c077635"),
                            DateCreated = new DateTime(2024, 2, 18, 18, 43, 32, 690, DateTimeKind.Local).AddTicks(7682),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Sangguniang Kabataan Kagawad"
                        });
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsValidate")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("VerificationStatus")
                        .HasColumnType("int");

                    b.Property<int>("VoterId")
                        .HasColumnType("int");

                    b.Property<string>("VoterImages")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9985b7c2-803e-4beb-be93-a18816bde423"),
                            Address = "123 Main Street",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfBirth = new DateTime(2024, 2, 18, 10, 43, 32, 690, DateTimeKind.Utc).AddTicks(7482),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "garadojosephmartin98@gmail.com",
                            FirstName = "Joseph Martin",
                            Gender = 2,
                            IsActive = true,
                            IsValidate = true,
                            LastName = "Garado",
                            Password = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=",
                            PhoneNumber = "09199431060",
                            Role = 2,
                            VerificationStatus = 0,
                            VoterId = 3280,
                            VoterImages = "PathImages\\VoterImages\\admin picture.png"
                        });
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VotedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("VoterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("VoterId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Ballot", b =>
                {
                    b.HasOne("OnlineVotingSystem.Domain.Entity.Ballot", null)
                        .WithMany("Ballots")
                        .HasForeignKey("BallotId");

                    b.HasOne("OnlineVotingSystem.Domain.Entity.Election", "Election")
                        .WithMany("Ballots")
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Election");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Candidate", b =>
                {
                    b.HasOne("OnlineVotingSystem.Domain.Entity.Ballot", "Ballot")
                        .WithMany("Candidates")
                        .HasForeignKey("BallotId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OnlineVotingSystem.Domain.Entity.Position", "Position")
                        .WithMany("Candidates")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ballot");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Vote", b =>
                {
                    b.HasOne("OnlineVotingSystem.Domain.Entity.Candidate", "Candidate")
                        .WithMany("Votes")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OnlineVotingSystem.Domain.Entity.User", "Voter")
                        .WithMany("Votes")
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Voter");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Ballot", b =>
                {
                    b.Navigation("Ballots");

                    b.Navigation("Candidates");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Candidate", b =>
                {
                    b.Navigation("Votes");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Election", b =>
                {
                    b.Navigation("Ballots");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Position", b =>
                {
                    b.Navigation("Candidates");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.User", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}