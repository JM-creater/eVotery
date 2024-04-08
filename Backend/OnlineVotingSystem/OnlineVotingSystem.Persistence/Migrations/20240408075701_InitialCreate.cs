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
                name: "PersonalDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDocuments", x => x.Id);
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    PersonalDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    SuffixName = table.Column<string>(type: "nvarchar(4)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(4)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    VoterImages = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    HasAgreedToTerms = table.Column<bool>(type: "bit", nullable: false),
                    PIDNumber = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    PImage = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    isRemember = table.Column<bool>(type: "bit", nullable: true),
                    isVoted = table.Column<bool>(type: "bit", nullable: true),
                    IsValidate = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    VerificationStatus = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_PersonalDocuments_PersonalDocumentId",
                        column: x => x.PersonalDocumentId,
                        principalTable: "PersonalDocuments",
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_Votes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "PersonalDocuments",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "Document", "IsActive" },
                values: new object[,]
                {
                    { new Guid("18d5b4bc-52e4-4e82-9e71-0f849cd0e658"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(159), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other valid government-issued IDs", false },
                    { new Guid("256a1096-c7ea-4e1f-89b7-c2b801878cdd"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(147), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philippine Identification", false },
                    { new Guid("31043501-7a61-49c4-8561-ce53ee1fe84d"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(144), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SSS ID", false },
                    { new Guid("4713318f-48d0-4660-a67d-b85110349b26"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(156), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philippine Postal ID", false },
                    { new Guid("608ce4bd-48cd-4b58-8746-c9c61f824170"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(137), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Driver’s License", false },
                    { new Guid("6996cc32-c180-4b44-bd7f-930897d0de0a"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(140), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Passport", false },
                    { new Guid("6f914a61-e819-44df-b5fa-ecc45936f151"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(158), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "School ID", false },
                    { new Guid("9692f1db-b076-4370-af5e-5d8116610154"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(139), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Professional Regulation Commission ID", false },
                    { new Guid("9d8f3287-146c-4e69-bbd7-1b5d8f8a42ad"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(149), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NBI Clearance", false },
                    { new Guid("a3dd3687-5837-44cd-89ee-c61778b43239"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(115), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UMID", false },
                    { new Guid("c3e886c8-6985-4844-80f5-11b45745ee73"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(142), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Senior Citizen ID", false },
                    { new Guid("d5cb295c-5405-4bad-8a1d-13b22c40d0f7"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(154), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barangay ID", false },
                    { new Guid("dea6d610-c682-4852-8cc7-3ecb5f6f0de4"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(151), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BIR", false },
                    { new Guid("eef7013e-5774-4709-9e53-d80bfe37e527"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(153), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pag-ibig ID", false }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("007476ba-0913-430d-9fde-f894ea9b40f6"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(257), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Mayor" },
                    { new Guid("0b7e4b30-fae2-4dd4-8b4b-8c808f81b4cb"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(266), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Captain" },
                    { new Guid("0db51c81-4886-4e5e-8957-e803d08a2065"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(252), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Member of the House of Representatives" },
                    { new Guid("134e96a3-03c8-477d-afd9-35b47185476c"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(274), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Kagawad" },
                    { new Guid("1f9b158c-fcc4-4db9-8437-78556b98f011"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(249), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Senator" },
                    { new Guid("4efa8ded-2a5f-4e08-8a8f-8270ceb73272"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(259), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Governor" },
                    { new Guid("5433b00f-b79d-47ac-91dc-101696d48eec"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(270), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Chairperson" },
                    { new Guid("6ef1a1c4-fba6-4177-8d1c-c6809f02eca5"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(254), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Governor" },
                    { new Guid("769a86b0-5b9a-40f2-b54b-0082d34277ec"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(261), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Mayor" },
                    { new Guid("82c34f3f-6558-490e-9490-ba2de24e1cfa"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(265), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Councilor" },
                    { new Guid("89e17e34-e52e-41cd-ba9d-4d3f63428ade"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(246), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice President" },
                    { new Guid("ae3c81ef-9e33-4550-a49d-6fbc519ca9a1"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(242), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "President" },
                    { new Guid("c636c3fb-2383-4c42-8489-9287fb732c56"), new DateTime(2024, 4, 8, 15, 57, 0, 126, DateTimeKind.Local).AddTicks(268), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Kagawad" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DateCreated", "DateOfBirth", "DateUpdated", "Email", "FirstName", "Gender", "HasAgreedToTerms", "IsActive", "IsValidate", "LastName", "Nationality", "Occupation", "PIDNumber", "PImage", "Password", "PasswordResetToken", "PersonalDocumentId", "PhoneNumber", "Religion", "ResetTokenExpires", "Role", "SuffixName", "Token", "VerificationStatus", "VoterId", "VoterImages", "ZipCode", "isRemember", "isVoted" },
                values: new object[] { new Guid("8ccdd719-b17e-4794-8542-ebafb06d38f2"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 8, 7, 57, 0, 125, DateTimeKind.Utc).AddTicks(9679), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "garadojosephmartin98@gmail.com", "Joseph Martin", "", false, true, true, "Garado", "Filipino", "", "", "", "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", null, new Guid("6996cc32-c180-4b44-bd7f-930897d0de0a"), "09199431060", "", null, 2, null, "", 0, 9393, "PathImages\\VoterImages\\admin picture.png", "", null, null });

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
                name: "IX_Users_PersonalDocumentId",
                table: "Users",
                column: "PersonalDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");
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
                name: "PersonalDocuments");

            migrationBuilder.DropTable(
                name: "Elections");
        }
    }
}
