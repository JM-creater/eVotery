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
                    { new Guid("06808f55-30d9-4e30-9e06-27ab571e4788"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8895), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Driver’s License", false },
                    { new Guid("06c9edaa-b38f-408d-ad73-0ad83d816502"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8904), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pag-ibig ID", false },
                    { new Guid("0d801249-119a-48ba-be17-a4325b29a7d7"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8907), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "School ID", false },
                    { new Guid("18ca1ecd-c375-46ad-b436-251cc437f33e"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8903), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BIR", false },
                    { new Guid("41bea30f-8084-4cd1-a852-83445ae2cba4"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8908), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other valid government-issued IDs", false },
                    { new Guid("5e1a2250-e5dc-4c37-9754-d9b6ff64d247"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8901), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philippine Identification", false },
                    { new Guid("60c657ce-00d9-43ca-8211-00a769cfa129"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8906), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philippine Postal ID", false },
                    { new Guid("6b06e749-7be0-40ee-a2c7-773a6f1a6795"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8900), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SSS ID", false },
                    { new Guid("a1227d59-ecc9-4767-97f3-b75ac9315bb2"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8897), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Passport", false },
                    { new Guid("a2b2a3d9-e796-4f7f-b327-f288459444ec"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8902), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NBI Clearance", false },
                    { new Guid("a7c48452-759a-4dcd-b6c8-9b584eeb4559"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8896), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Professional Regulation Commission ID", false },
                    { new Guid("d25af56a-cf5e-49ad-9776-a428e4dcf93c"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8882), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UMID", false },
                    { new Guid("d7144e7d-b19a-4c60-8e2c-3fdf80f533f7"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8899), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Senior Citizen ID", false },
                    { new Guid("e5562e48-e741-4ad1-92d8-4e094f0bd26f"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8905), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barangay ID", false }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("09728259-6264-4ef6-b929-fa6c21f0413d"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8955), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Councilor" },
                    { new Guid("201870df-84b0-42f3-95bc-681b6ed7f806"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8957), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Kagawad" },
                    { new Guid("4dfb5db2-c0f3-4d2c-8d32-0085c07eb8d0"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8954), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Mayor" },
                    { new Guid("52e665bf-e782-4d2b-827b-7e92c19eebcb"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8944), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "President" },
                    { new Guid("7f4c893a-fcb8-4f0a-8aa3-237b791d8976"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8958), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Chairperson" },
                    { new Guid("945d6723-df87-4217-b34e-8daada6e64b0"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8947), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Senator" },
                    { new Guid("9c149e45-10d7-4345-a41e-8dcf09f58ad7"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8950), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Governor" },
                    { new Guid("bc411c32-b3a0-41c2-a237-54ebe369cf53"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8961), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Kagawad" },
                    { new Guid("c7c56569-a2a9-409f-9dbf-331f92a6fadc"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8951), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Mayor" },
                    { new Guid("d3832018-74fa-428f-a857-112c222a5f81"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8952), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Governor" },
                    { new Guid("e06c8401-1410-44f4-a5e8-8f873a383d9d"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8946), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice President" },
                    { new Guid("eb9bb557-ce0a-403d-8f43-1fefd52fc7be"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8956), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Captain" },
                    { new Guid("f9b5ea7f-209f-42f7-94a3-bfc5d1972087"), new DateTime(2024, 4, 16, 7, 34, 21, 359, DateTimeKind.Local).AddTicks(8949), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Member of the House of Representatives" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DateCreated", "DateOfBirth", "DateUpdated", "Email", "FirstName", "Gender", "HasAgreedToTerms", "IsActive", "IsValidate", "LastName", "Nationality", "Occupation", "PIDNumber", "PImage", "Password", "PasswordResetToken", "PersonalDocumentId", "PhoneNumber", "Religion", "ResetTokenExpires", "Role", "SuffixName", "Token", "VerificationStatus", "VoterId", "VoterImages", "ZipCode", "isRemember", "isVoted" },
                values: new object[] { new Guid("3baa6014-aa9b-4363-899c-f94ec39a28ed"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 15, 23, 34, 21, 359, DateTimeKind.Utc).AddTicks(8708), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "garadojosephmartin98@gmail.com", "Joseph Martin", "", false, true, true, "Garado", "Filipino", "", "", "", "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", null, new Guid("a1227d59-ecc9-4767-97f3-b75ac9315bb2"), "09199431060", "", null, 2, null, "", 0, 9466, "PathImages\\VoterImages\\admin picture.png", "", null, null });

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
