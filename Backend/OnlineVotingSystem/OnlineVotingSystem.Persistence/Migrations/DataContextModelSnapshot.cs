﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineVotingSystem.Persistence.Context;

#nullable disable

namespace OnlineVotingSystem.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.Ballot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BallotName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

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

                    b.Property<bool>("SaveResponse")
                        .HasColumnType("bit");

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

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<bool>("CurrentUserVoted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<Guid>("PartyAffiliationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PositionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BallotId");

                    b.HasIndex("PartyAffiliationId");

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

                    b.Property<string>("ElectionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Elections");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.PartyAffiliation", b =>
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

                    b.Property<string>("LogoImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("PartyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("PartyAffiliations");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.PersonalDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("PersonalDocuments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("722f7ac9-58b0-4a90-9287-b70e1881a02b"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2240),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "UMID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("9dbfc951-9b74-49d2-ad05-bb72bbfb09c6"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2264),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Driver’s License",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("7adcd5dc-34f9-4531-a0cf-173dd863fb6e"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2266),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Professional Regulation Commission ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("3f933143-64ca-452d-bf23-7688b47424f7"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2267),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Passport",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("b2dc71e3-7818-4362-a8a1-ac6d969f6624"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2269),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Senior Citizen ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("e4163ede-020c-4b4d-9726-325344b2fc73"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2271),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "SSS ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("739f130f-616b-40a4-8b2a-df1ffa8e2903"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2277),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Philippine Identification",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("be8d4ded-6dc2-4f08-bdb7-b48503de56fd"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2279),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "NBI Clearance",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("cef30315-e9ba-43ad-aab7-0e82dcb3de18"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2280),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "BIR",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("00a0f118-13e6-4e54-8447-712b84b1dec0"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2282),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Pag-ibig ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("a2e745cb-55f4-47f4-a45e-3db00cec0e8d"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2283),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Barangay ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("9d741c67-a125-4428-9f5e-5c30dc27829d"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2285),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Philippine Postal ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("89ce3ca7-77a9-41fa-ac30-63eb6f3a3ebe"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2287),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "School ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("72c64d01-9205-42b5-a185-19b1d9e2ed45"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2289),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Other valid government-issued IDs",
                            IsActive = false
                        });
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
                            Id = new Guid("b6f24071-d147-410d-a7e7-dcd188a11704"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2380),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "President"
                        },
                        new
                        {
                            Id = new Guid("1826aab2-0354-4e5d-9b68-929683296e71"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2384),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice President"
                        },
                        new
                        {
                            Id = new Guid("d105a221-ebb7-46f9-839b-368ba3543a14"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2387),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Senator"
                        },
                        new
                        {
                            Id = new Guid("a2543a5a-fcd4-4ed5-b846-1d2414d18308"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2390),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Member of the House of Representatives"
                        },
                        new
                        {
                            Id = new Guid("9c8bbbb1-bd5f-47d4-b9b4-6db115518b0b"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2392),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Governor"
                        },
                        new
                        {
                            Id = new Guid("fd46c5c2-e834-48cd-8330-ac8467668a9d"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2395),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Mayor"
                        },
                        new
                        {
                            Id = new Guid("0b551495-c855-4754-a33b-30e813e85816"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2480),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice Governor"
                        },
                        new
                        {
                            Id = new Guid("59440f01-f468-49a8-9212-2da3e3aa24ea"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2482),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice Mayor"
                        },
                        new
                        {
                            Id = new Guid("c7e3c8ca-a7a9-4889-a8e2-d6814905f60e"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2484),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Councilor"
                        },
                        new
                        {
                            Id = new Guid("e3be9557-ca71-4390-868f-ae38beb526d3"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2486),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Barangay Captain"
                        },
                        new
                        {
                            Id = new Guid("669d6873-c17b-4819-8e4c-ca79649cf6cb"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2487),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Barangay Kagawad"
                        },
                        new
                        {
                            Id = new Guid("edb00492-af63-4425-8c53-75a1ef570a95"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2489),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Sangguniang Kabataan Chairperson"
                        },
                        new
                        {
                            Id = new Guid("a3a60dba-b057-45af-9581-f0bce1674a74"),
                            DateCreated = new DateTime(2024, 4, 17, 15, 28, 26, 294, DateTimeKind.Local).AddTicks(2493),
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
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("HasAgreedToTerms")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsValidate")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Occupation")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PIDNumber")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PImage")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PersonalDocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Religion")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SuffixName")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VerificationStatus")
                        .HasColumnType("int");

                    b.Property<int>("VoterId")
                        .HasColumnType("int");

                    b.Property<string>("VoterImages")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(4)");

                    b.Property<bool?>("isRemember")
                        .HasColumnType("bit");

                    b.Property<bool?>("isVoted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("PersonalDocumentId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dd840c07-5239-4966-b6b6-59d6fecfb167"),
                            Address = "",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfBirth = new DateTime(2024, 4, 17, 7, 28, 26, 294, DateTimeKind.Utc).AddTicks(1791),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "garadojosephmartin98@gmail.com",
                            FirstName = "Joseph Martin",
                            Gender = "",
                            HasAgreedToTerms = false,
                            IsActive = true,
                            IsValidate = true,
                            LastName = "Garado",
                            Nationality = "Filipino",
                            Occupation = "",
                            PIDNumber = "",
                            PImage = "",
                            Password = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=",
                            PersonalDocumentId = new Guid("3f933143-64ca-452d-bf23-7688b47424f7"),
                            PhoneNumber = "09199431060",
                            Religion = "",
                            Role = 2,
                            Token = "",
                            VerificationStatus = 0,
                            VoterId = 3765,
                            VoterImages = "PathImages\\VoterImages\\admin picture.png",
                            ZipCode = ""
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

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("VotedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("UserId");

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

                    b.HasOne("OnlineVotingSystem.Domain.Entity.PartyAffiliation", "PartyAffiliation")
                        .WithMany("Candidates")
                        .HasForeignKey("PartyAffiliationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OnlineVotingSystem.Domain.Entity.Position", "Position")
                        .WithMany("Candidates")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ballot");

                    b.Navigation("PartyAffiliation");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.User", b =>
                {
                    b.HasOne("OnlineVotingSystem.Domain.Entity.PersonalDocument", "PersonalDocument")
                        .WithMany()
                        .HasForeignKey("PersonalDocumentId");

                    b.Navigation("PersonalDocument");
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
                        .HasForeignKey("UserId")
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

            modelBuilder.Entity("OnlineVotingSystem.Domain.Entity.PartyAffiliation", b =>
                {
                    b.Navigation("Candidates");
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
