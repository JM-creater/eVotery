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
    [Migration("20240604064244_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            Id = new Guid("0a854ad7-d39f-49b4-b0e5-85c99f2921f3"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5843),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "UMID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("db46a2ab-8603-45e1-90de-b20a60a53003"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5873),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Driver’s License",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("7d8dac7f-4fa4-46a5-b96d-75f075814771"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5881),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Professional Regulation Commission ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("1f4934ec-cbc6-4225-b755-a1e989ebdb31"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5883),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Passport",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("c4149202-ffa4-45a8-b224-2b37e32ac163"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5885),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Senior Citizen ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("ab9cf9ba-7079-4a3b-98ea-0c35e58a696b"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5887),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "SSS ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("3993c640-eb40-468f-b8e2-c131beebf5ed"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5889),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Philippine Identification",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("64a586d9-7c1e-4c18-8195-3b52fa29851f"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5891),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "NBI Clearance",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("8e3effb8-d298-43ca-9a1d-697e3b6306c2"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5893),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "BIR",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("887496cb-7094-4fdd-ba2d-9ccbf668f812"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5895),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Pag-ibig ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("1b15f574-8c3e-43ad-94d1-3455ffd467f2"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5897),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Barangay ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("f0b8efd4-2eb9-4e97-9e69-8a686ec885d8"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5899),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "Philippine Postal ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("6640221c-f089-4da2-8a44-838b16652eec"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5901),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Document = "School ID",
                            IsActive = false
                        },
                        new
                        {
                            Id = new Guid("aabf48ae-b7d5-4118-b6ef-de9b4a76abda"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5902),
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
                            Id = new Guid("39daf74d-8806-4b18-bfca-4216fecfd1ab"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5970),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "President"
                        },
                        new
                        {
                            Id = new Guid("6f6cb8a2-68e7-47db-963a-f9ff50eead45"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5973),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice President"
                        },
                        new
                        {
                            Id = new Guid("4a2f90e1-d49d-4436-84ae-bcd4df54f86c"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5975),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Senator"
                        },
                        new
                        {
                            Id = new Guid("6a6cf5ea-da8e-498f-90e5-9dc55840d3b3"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5978),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Member of the House of Representatives"
                        },
                        new
                        {
                            Id = new Guid("7d226be2-184b-4654-ab2c-20175b7b6133"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5980),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Governor"
                        },
                        new
                        {
                            Id = new Guid("c9414369-c957-476c-bdbd-c2171cfa09dc"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5982),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Mayor"
                        },
                        new
                        {
                            Id = new Guid("f547f9ba-252a-4882-aed2-4ff7b91e8bae"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5984),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice Governor"
                        },
                        new
                        {
                            Id = new Guid("b4d76ef8-7aa5-4fff-88a6-ae566289ccab"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5986),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Vice Mayor"
                        },
                        new
                        {
                            Id = new Guid("82d853d8-a0f2-420b-a251-53a0452b93c6"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5988),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Councilor"
                        },
                        new
                        {
                            Id = new Guid("a0b6e1ef-a845-4deb-a15e-d0c41e9da7ac"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5996),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Barangay Captain"
                        },
                        new
                        {
                            Id = new Guid("97100166-8274-41df-bf85-666c470ba262"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(5998),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Barangay Kagawad"
                        },
                        new
                        {
                            Id = new Guid("4d40ad0b-611c-42c5-be37-96dc2beb55d5"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(6000),
                            DateUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            Name = "Sangguniang Kabataan Chairperson"
                        },
                        new
                        {
                            Id = new Guid("2b5fac9c-9959-4e5c-9d8a-6c587b336dd8"),
                            DateCreated = new DateTime(2024, 6, 4, 14, 42, 41, 191, DateTimeKind.Local).AddTicks(6002),
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
                            Id = new Guid("2b992939-4881-4398-9689-0dd5448f96ac"),
                            Address = "",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfBirth = new DateTime(2024, 6, 4, 6, 42, 41, 191, DateTimeKind.Utc).AddTicks(5493),
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
                            PersonalDocumentId = new Guid("1f4934ec-cbc6-4225-b755-a1e989ebdb31"),
                            PhoneNumber = "09199431060",
                            Religion = "",
                            Role = 2,
                            Token = "",
                            VerificationStatus = 0,
                            VoterId = 5364,
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