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
    [Migration("20240217131158_InitialCreate")]
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
                            Id = new Guid("9925cc11-a753-4c31-b792-7599586ad4a1"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2555),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "President"
                        },
                        new
                        {
                            Id = new Guid("78cc0564-ff78-4a28-9587-5f7ad2e2a713"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2567),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice President"
                        },
                        new
                        {
                            Id = new Guid("55311263-4c66-4f82-801a-4ade4699e51a"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2569),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Senator"
                        },
                        new
                        {
                            Id = new Guid("5f9e3b50-46dd-4f08-afa2-51b6911ac2f5"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2570),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Member of the House of Representatives"
                        },
                        new
                        {
                            Id = new Guid("7b8c7ac2-7d11-4151-9dcd-d00b864ddc8a"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2572),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Governor"
                        },
                        new
                        {
                            Id = new Guid("583b12f9-b763-4509-a1ae-32c1d315996e"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2573),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Mayor"
                        },
                        new
                        {
                            Id = new Guid("e40d1b96-c3e0-465d-8d37-0575b27debe4"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2574),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice Governor"
                        },
                        new
                        {
                            Id = new Guid("8c289ffa-16c5-4366-92fc-a831ce833a8e"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2576),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice Mayor"
                        },
                        new
                        {
                            Id = new Guid("c9ec2926-375f-48ac-9666-60b855c0a5f7"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2577),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Councilor"
                        },
                        new
                        {
                            Id = new Guid("04b402d2-a581-47b5-a136-560e68134d38"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2578),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Barangay Captain"
                        },
                        new
                        {
                            Id = new Guid("249c37ad-0c36-4092-9991-b7dcfca1da6f"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2579),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Barangay Kagawad"
                        },
                        new
                        {
                            Id = new Guid("dfcec5df-d2ef-4105-9fa6-cbed1b85cd60"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2581),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Sangguniang Kabataan Chairperson"
                        },
                        new
                        {
                            Id = new Guid("9909b105-2272-4ff0-b007-0127c9837552"),
                            DateCreated = new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2582),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Sangguniang Kabataan Kagawad"
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

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Voter", b =>
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

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)");

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

                    b.ToTable("Voters");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ea11545e-c4fd-4174-a0a5-5898f3264477"),
                            Address = "123 Main Street",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfBirth = new DateTime(2024, 2, 17, 13, 11, 58, 138, DateTimeKind.Utc).AddTicks(2402),
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
                            VoterId = 3265,
                            VoterImages = "PathImages\\VoterImages\\admin picture.png"
                        });
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

                    b.HasOne("OnlineVotingSystem.Domain.Entity.Voter", "Voter")
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

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Voter", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
