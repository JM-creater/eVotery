using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineVotingSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ElectionName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartyAffiliations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartyName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LogoImage = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyAffiliations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    VoterImages = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    IsValidate = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    VerificationStatus = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ballots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BallotName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ElectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ballots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ballots_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    PartyAffiliationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BallotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_Ballots_BallotId",
                        column: x => x.BallotId,
                        principalTable: "Ballots",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidates_PartyAffiliations_PartyAffiliationId",
                        column: x => x.PartyAffiliationId,
                        principalTable: "PartyAffiliations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidates_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VotedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votes_Users_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("04bfd31b-e8d1-491f-944b-030b670f6e21"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9127), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Member of the House of Representatives" },
                    { new Guid("177f4a1d-1c20-48db-8481-29674d121a81"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9112), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "President" },
                    { new Guid("47ce5a83-142f-4104-8ce9-5aa5ffcec500"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9128), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Governor" },
                    { new Guid("4855a70b-ea0e-4633-aaeb-abe4de14fdbc"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9134), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Captain" },
                    { new Guid("76bf870f-3ae1-41a5-ba72-2a4ad01f7855"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9135), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Kagawad" },
                    { new Guid("7b8b403d-c159-4258-8b45-45e7156197e8"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9137), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Chairperson" },
                    { new Guid("98bf2e56-587d-4477-878f-bc518826ad29"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9125), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Senator" },
                    { new Guid("b4dc0f9a-3dbb-47f5-8881-b794941e1e7e"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9131), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Governor" },
                    { new Guid("c7fbd963-194e-4ec6-8ae7-9f773e548176"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9133), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Councilor" },
                    { new Guid("d9bc4f84-3846-4f5c-a3ba-ace3781e7218"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9138), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Kagawad" },
                    { new Guid("e14370c3-baf8-4cef-a7ab-76f3475cf8f1"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9132), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Mayor" },
                    { new Guid("e8e2bef9-3ae1-4be6-a5f9-68f53b77991b"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9129), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Mayor" },
                    { new Guid("fba4a22b-56ce-433b-9973-9e0b278fd829"), new DateTime(2024, 3, 3, 20, 2, 22, 51, DateTimeKind.Local).AddTicks(9124), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice President" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DateCreated", "DateOfBirth", "DateUpdated", "Email", "FirstName", "Gender", "IsActive", "IsValidate", "LastName", "Password", "PasswordResetToken", "PhoneNumber", "ResetTokenExpires", "Role", "VerificationStatus", "VoterId", "VoterImages" },
                values: new object[] { new Guid("360213dd-97ac-4cb2-812b-f36fa3d0e6a3"), "123 Main Street", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 3, 12, 2, 22, 51, DateTimeKind.Utc).AddTicks(8986), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "garadojosephmartin98@gmail.com", "Joseph Martin", 2, true, true, "Garado", "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", null, "09199431060", null, 2, 0, 4496, "PathImages\\VoterImages\\admin picture.png" });

            migrationBuilder.CreateIndex(
                name: "IX_Ballots_ElectionId",
                table: "Ballots",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_BallotId",
                table: "Candidates",
                column: "BallotId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PartyAffiliationId",
                table: "Candidates",
                column: "PartyAffiliationId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PositionId",
                table: "Candidates",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VoterId",
                table: "Votes",
                column: "VoterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Ballots");

            migrationBuilder.DropTable(
                name: "PartyAffiliations");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Elections");
        }
    }
}
