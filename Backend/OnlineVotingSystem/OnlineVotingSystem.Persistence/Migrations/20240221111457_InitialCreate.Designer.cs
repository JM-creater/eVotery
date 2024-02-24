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
    [Migration("20240221111457_InitialCreate")]
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

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

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
                            Id = new Guid("4dfce662-bfe5-480d-a4bb-20da8f44cec3"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6785),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "President"
                        },
                        new
                        {
                            Id = new Guid("7feb587e-fc90-4c69-9aa5-842068c11adb"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6796),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice President"
                        },
                        new
                        {
                            Id = new Guid("8471f071-ea7e-4d0d-b124-7b14094c636a"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6798),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Senator"
                        },
                        new
                        {
                            Id = new Guid("1679307d-5105-48df-946e-a67917fd79a8"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6799),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Member of the House of Representatives"
                        },
                        new
                        {
                            Id = new Guid("496e087c-46a1-4bea-80fd-68428fd94b98"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6801),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Governor"
                        },
                        new
                        {
                            Id = new Guid("a8c5d77e-9df2-4601-bdfc-e5bec5da6545"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6802),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Mayor"
                        },
                        new
                        {
                            Id = new Guid("56b4afe7-7ee3-4429-9ac0-3ade6d7b122a"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6803),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice Governor"
                        },
                        new
                        {
                            Id = new Guid("dfc8e0b4-c4b0-4f7b-9f16-064946eb3041"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6804),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice Mayor"
                        },
                        new
                        {
                            Id = new Guid("b0495072-6f17-4f3a-a1b0-a212508c843b"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6806),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Councilor"
                        },
                        new
                        {
                            Id = new Guid("0a654b6b-9451-4f17-a909-c6f2a8046d13"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6807),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Barangay Captain"
                        },
                        new
                        {
                            Id = new Guid("6893bb8a-4368-4e1c-bcc8-79434988f3bd"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6809),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Barangay Kagawad"
                        },
                        new
                        {
                            Id = new Guid("45211817-b31a-4684-bf33-9f8aea7e8095"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6810),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Sangguniang Kabataan Chairperson"
                        },
                        new
                        {
                            Id = new Guid("f1fbf616-c123-4626-b40f-cf29d2da2863"),
                            DateCreated = new DateTime(2024, 2, 21, 19, 14, 57, 231, DateTimeKind.Local).AddTicks(6811),
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
                            Id = new Guid("8c4cce96-e514-42a0-818a-8445922ab21d"),
                            Address = "123 Main Street",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfBirth = new DateTime(2024, 2, 21, 11, 14, 57, 231, DateTimeKind.Utc).AddTicks(6650),
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
                            VoterId = 4735,
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