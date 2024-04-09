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
                    SaveResponse = table.Column<bool>(type: "bit", nullable: false),
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
                    { new Guid("0181cb2f-be73-4e96-b62b-6fe24bbe9533"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3494), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "School ID", false },
                    { new Guid("1aa78370-7589-444e-a91f-a95ea7c9753d"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3469), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UMID", false },
                    { new Guid("22d8b2ac-ac00-414e-ae0d-bca80fef658c"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3486), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SSS ID", false },
                    { new Guid("3929abaf-7dca-42c6-bf1b-9fdf95d8bb61"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3483), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Professional Regulation Commission ID", false },
                    { new Guid("478f59b6-3b00-4d1e-b7fb-fdec4a0d2703"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3484), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Passport", false },
                    { new Guid("55de046d-9ea9-4e32-9c09-aa88de195966"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3490), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BIR", false },
                    { new Guid("568998cb-5d6a-4730-b8d0-62b0830779c4"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3481), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Driver’s License", false },
                    { new Guid("782b6182-644d-4e43-b8a1-d1075182e44f"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3491), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pag-ibig ID", false },
                    { new Guid("88e23e0d-5908-4e1a-bed8-b3c14146721b"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3488), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NBI Clearance", false },
                    { new Guid("bdf33f58-a182-4fcc-a542-fb61e51f53f4"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3492), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barangay ID", false },
                    { new Guid("c6cba42d-66a6-4071-9688-2e053d449159"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3485), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Senior Citizen ID", false },
                    { new Guid("d33a2337-9d8d-41f9-a949-adcd3b6866ed"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3493), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philippine Postal ID", false },
                    { new Guid("d749bbc2-383f-445d-b3f2-dbb887dfef51"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3487), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philippine Identification", false },
                    { new Guid("e679f451-3a7b-4ee4-8a3f-5b484f6e2073"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3495), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other valid government-issued IDs", false }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("01e1c6c1-5667-4862-8583-59b7ba9c0c06"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3588), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Mayor" },
                    { new Guid("19b609fd-d7a2-4b7d-8173-cef7a6e639ad"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3589), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Councilor" },
                    { new Guid("19b70aaa-a6da-4b7d-9ddd-5104495a1228"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3580), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice President" },
                    { new Guid("2c3e4b4a-9fd8-4315-a61e-c16879e49492"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3587), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Governor" },
                    { new Guid("46c930a9-982c-4ac5-882d-09a527e85304"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3590), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Captain" },
                    { new Guid("805715f8-a00a-4941-8859-e5cd767d2da6"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3578), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "President" },
                    { new Guid("8b87514f-6024-43ab-b221-7d7600e5b544"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3583), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Member of the House of Representatives" },
                    { new Guid("8eba2901-14c1-4f4c-bad7-8a8ed7c4afee"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3585), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Mayor" },
                    { new Guid("9a666418-f634-445c-999b-99b85aa9c837"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3581), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Senator" },
                    { new Guid("a2a8e9d8-90ee-47d7-aab1-d46d9dffe852"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3595), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Kagawad" },
                    { new Guid("a305fea2-f680-42b6-889b-5205b40f0801"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3593), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Chairperson" },
                    { new Guid("cc6ca734-af96-49e8-a02e-b2a09fba210d"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3584), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Governor" },
                    { new Guid("fd5cf185-8acb-4b73-8938-222cf6d49a63"), new DateTime(2024, 4, 9, 16, 30, 53, 798, DateTimeKind.Local).AddTicks(3592), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Kagawad" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DateCreated", "DateOfBirth", "DateUpdated", "Email", "FirstName", "Gender", "HasAgreedToTerms", "IsActive", "IsValidate", "LastName", "Nationality", "Occupation", "PIDNumber", "PImage", "Password", "PasswordResetToken", "PersonalDocumentId", "PhoneNumber", "Religion", "ResetTokenExpires", "Role", "SuffixName", "Token", "VerificationStatus", "VoterId", "VoterImages", "ZipCode", "isRemember", "isVoted" },
                values: new object[] { new Guid("ae4883bf-5f3b-4a08-82e5-4f24a9081b2a"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 9, 8, 30, 53, 798, DateTimeKind.Utc).AddTicks(3296), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "garadojosephmartin98@gmail.com", "Joseph Martin", "", false, true, true, "Garado", "Filipino", "", "", "", "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", null, new Guid("478f59b6-3b00-4d1e-b7fb-fdec4a0d2703"), "09199431060", "", null, 2, null, "", 0, 2073, "PathImages\\VoterImages\\admin picture.png", "", null, null });

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
