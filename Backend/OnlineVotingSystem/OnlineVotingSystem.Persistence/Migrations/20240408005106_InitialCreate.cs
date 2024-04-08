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
                table: "PersonalDocuments",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "Document", "IsActive" },
                values: new object[,]
                {
                    { new Guid("0a973224-5c1a-4ee5-8bbe-b2e078f9603e"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(458), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other valid government-issued IDs", false },
                    { new Guid("0cf6c86f-f47f-48f8-97a2-a706b1291549"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(455), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barangay ID", false },
                    { new Guid("273f7bcb-a1bc-4427-a533-46981711ee20"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(454), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pag-ibig ID", false },
                    { new Guid("2b729bef-fc7e-4842-b5ef-487d923f24a6"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(431), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UMID", false },
                    { new Guid("52bfdbe5-a807-418c-aa0c-10c9b81d96c8"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(451), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philippine Identification", false },
                    { new Guid("5a28487c-ff30-409f-83ef-635af777b05b"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(457), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "School ID", false },
                    { new Guid("5a316d48-8096-41f2-9e9f-16325be4b778"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(453), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BIR", false },
                    { new Guid("79fd61a5-2b3e-46f8-b4bf-9b030b67a2ce"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(449), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SSS ID", false },
                    { new Guid("90775612-0a98-453f-9066-d8b22888f77f"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(446), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Professional Regulation Commission ID", false },
                    { new Guid("952cffc9-c81f-4e65-ac49-442272009301"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(456), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philippine Postal ID", false },
                    { new Guid("c13916d8-58f7-4394-9569-30a79c094d28"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(445), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Driver’s License", false },
                    { new Guid("c2192901-b8e5-4e5e-9238-cc97e6746ebc"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(452), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NBI Clearance", false },
                    { new Guid("dd3ddd9f-7666-4dbf-b113-2777957c5ac9"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(448), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Senior Citizen ID", false },
                    { new Guid("fe53ea89-a0c2-41a4-a4a0-937f6be4def8"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(447), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Passport", false }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("029c64d2-eef3-4dde-8a69-c9938ae2eeda"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(520), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice President" },
                    { new Guid("3666ad1e-7ed8-46fe-91ad-c57d65e2de06"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(522), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Member of the House of Representatives" },
                    { new Guid("3cd425d4-54a8-4fef-af50-2e05b39e4d0f"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(524), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Mayor" },
                    { new Guid("40f9894f-bad5-452b-83f8-b6e894d82150"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(533), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Kagawad" },
                    { new Guid("4ebcda83-8957-4225-be76-509032562a5e"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(521), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Senator" },
                    { new Guid("6058c946-d1e2-45dd-9e32-0a5ff2a08af1"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(525), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Governor" },
                    { new Guid("660dd954-0f4d-43a5-95f5-91ad265b3956"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(530), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Chairperson" },
                    { new Guid("8124b41b-e099-4be9-8beb-be9c679e87be"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(526), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Mayor" },
                    { new Guid("8b482745-0806-47bb-abdf-dbbfe0334219"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(518), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "President" },
                    { new Guid("ac35b7dd-1725-472c-9f7c-d28264d62b7a"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(523), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Governor" },
                    { new Guid("b200ca8e-feaa-4488-84b5-8011dffc5f7f"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(527), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Councilor" },
                    { new Guid("e63e726e-8b74-45bb-824d-b87974840421"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(528), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Captain" },
                    { new Guid("fea97ae6-1e2e-47ce-8684-c6a705d9b001"), new DateTime(2024, 4, 8, 8, 51, 6, 151, DateTimeKind.Local).AddTicks(529), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Kagawad" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DateCreated", "DateOfBirth", "DateUpdated", "Email", "FirstName", "Gender", "HasAgreedToTerms", "IsActive", "IsValidate", "LastName", "Nationality", "Occupation", "PIDNumber", "PImage", "Password", "PasswordResetToken", "PersonalDocumentId", "PhoneNumber", "Religion", "ResetTokenExpires", "Role", "SuffixName", "Token", "VerificationStatus", "VoterId", "VoterImages", "ZipCode", "isRemember", "isVoted" },
                values: new object[] { new Guid("957d8c6a-0f2e-4cad-99a1-8db4ecbb566a"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 8, 0, 51, 6, 151, DateTimeKind.Utc).AddTicks(274), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "garadojosephmartin98@gmail.com", "Joseph Martin", "", false, true, true, "Garado", "Filipino", "", "", "", "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", null, new Guid("fe53ea89-a0c2-41a4-a4a0-937f6be4def8"), "09199431060", "", null, 2, null, "", 0, 9830, "PathImages\\VoterImages\\admin picture.png", "", null, null });

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
                name: "PersonalDocuments");

            migrationBuilder.DropTable(
                name: "Elections");
        }
    }
}
