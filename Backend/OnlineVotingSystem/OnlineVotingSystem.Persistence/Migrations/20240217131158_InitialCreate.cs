using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineVotingSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
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
                name: "Voters",
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ballots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ElectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BallotId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ballots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ballots_Ballots_BallotId",
                        column: x => x.BallotId,
                        principalTable: "Ballots",
                        principalColumn: "Id");
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
                    BallotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_Votes_Voters_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Voters",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("04b402d2-a581-47b5-a136-560e68134d38"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2578), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Captain" },
                    { new Guid("249c37ad-0c36-4092-9991-b7dcfca1da6f"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2579), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Kagawad" },
                    { new Guid("55311263-4c66-4f82-801a-4ade4699e51a"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2569), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Senator" },
                    { new Guid("583b12f9-b763-4509-a1ae-32c1d315996e"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2573), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Mayor" },
                    { new Guid("5f9e3b50-46dd-4f08-afa2-51b6911ac2f5"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2570), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Member of the House of Representatives" },
                    { new Guid("78cc0564-ff78-4a28-9587-5f7ad2e2a713"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2567), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice President" },
                    { new Guid("7b8c7ac2-7d11-4151-9dcd-d00b864ddc8a"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2572), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Governor" },
                    { new Guid("8c289ffa-16c5-4366-92fc-a831ce833a8e"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2576), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Mayor" },
                    { new Guid("9909b105-2272-4ff0-b007-0127c9837552"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2582), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Kagawad" },
                    { new Guid("9925cc11-a753-4c31-b792-7599586ad4a1"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2555), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "President" },
                    { new Guid("c9ec2926-375f-48ac-9666-60b855c0a5f7"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2577), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Councilor" },
                    { new Guid("dfcec5df-d2ef-4105-9fa6-cbed1b85cd60"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2581), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Chairperson" },
                    { new Guid("e40d1b96-c3e0-465d-8d37-0575b27debe4"), new DateTime(2024, 2, 17, 21, 11, 58, 138, DateTimeKind.Local).AddTicks(2574), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Governor" }
                });

            migrationBuilder.InsertData(
                table: "Voters",
                columns: new[] { "Id", "Address", "DateCreated", "DateOfBirth", "DateUpdated", "Email", "FirstName", "Gender", "IsActive", "IsValidate", "LastName", "Password", "PhoneNumber", "Role", "VerificationStatus", "VoterId", "VoterImages" },
                values: new object[] { new Guid("ea11545e-c4fd-4174-a0a5-5898f3264477"), "123 Main Street", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 17, 13, 11, 58, 138, DateTimeKind.Utc).AddTicks(2402), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "garadojosephmartin98@gmail.com", "Joseph Martin", 2, true, true, "Garado", "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", "09199431060", 2, 0, 3265, "PathImages\\VoterImages\\admin picture.png" });

            migrationBuilder.CreateIndex(
                name: "IX_Ballots_BallotId",
                table: "Ballots",
                column: "BallotId");

            migrationBuilder.CreateIndex(
                name: "IX_Ballots_ElectionId",
                table: "Ballots",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_BallotId",
                table: "Candidates",
                column: "BallotId");

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
                name: "Voters");

            migrationBuilder.DropTable(
                name: "Ballots");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Elections");
        }
    }
}
