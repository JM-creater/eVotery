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
                    { new Guid("049bf2a0-e9e9-490e-92a3-f66a2592ba05"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4813), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Professional Regulation Commission ID", false },
                    { new Guid("20189f85-fd5e-4fad-9ab0-ab52ca464727"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4809), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Driver’s License", false },
                    { new Guid("2b5f5582-0563-417c-a953-4965d13bb75e"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4788), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UMID", false },
                    { new Guid("3e4b7a23-31b6-4207-baf1-a170224d460e"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4830), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NBI Clearance", false },
                    { new Guid("5b6e49ea-13ad-4503-92e5-172305dce933"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4833), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BIR", false },
                    { new Guid("661ebbe3-550a-4a93-99ed-955d9ad14a96"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4823), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SSS ID", false },
                    { new Guid("7543aa90-e608-4e95-847b-b33b58cebf54"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4846), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "School ID", false },
                    { new Guid("7804a7e9-16b5-4a6f-928f-613ae98b19fd"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4816), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Passport", false },
                    { new Guid("87a183dd-c6eb-4b1b-be06-38266456db42"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4837), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pag-ibig ID", false },
                    { new Guid("9da51889-e13e-4b95-a7d7-452ecf36d608"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4849), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other valid government-issued IDs", false },
                    { new Guid("b8d87f28-1044-41c6-acae-aadcb38397f7"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4840), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barangay ID", false },
                    { new Guid("c4c9609e-ca10-468c-85a4-da04e7c6bf5d"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4843), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philippine Postal ID", false },
                    { new Guid("d2510798-1432-4d0e-95ea-c7ae79fd599c"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4826), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philippine Identification", false },
                    { new Guid("ee5a9e2a-dbb6-43cd-8b79-808235c456f9"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4820), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Senior Citizen ID", false }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("1fe592c6-a90b-4cf6-b48b-c0bd7ff24f6c"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4966), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Mayor" },
                    { new Guid("27a8cb54-4ebc-41f3-bd64-bb20c3da0709"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4984), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Kagawad" },
                    { new Guid("342b55d6-1394-48a2-b0fc-69d8609163f3"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4970), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Governor" },
                    { new Guid("4820941a-099e-434d-b1e4-0292cb7999d7"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4962), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Governor" },
                    { new Guid("4fc61997-b427-458c-b583-81df754f64dd"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4977), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Councilor" },
                    { new Guid("5a5c27c5-a35c-41a9-b019-93ff28ed691a"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4974), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice Mayor" },
                    { new Guid("7b87ad6c-5015-450f-a641-5e3faede834e"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4980), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Barangay Captain" },
                    { new Guid("a626a6af-62cb-4dc3-ac58-a3232f1bde04"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4959), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Member of the House of Representatives" },
                    { new Guid("bc8837dd-f2aa-4e14-9a5d-4469baaac305"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4946), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "President" },
                    { new Guid("c2593769-976c-4777-8ed0-a7e9e45bacba"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4955), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Senator" },
                    { new Guid("c3b9c7da-d072-43e4-a33a-31be0da4a5cb"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4951), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Vice President" },
                    { new Guid("d6f49eb2-23e5-4b52-b217-e8c7f674a1d0"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4987), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Chairperson" },
                    { new Guid("d992fba8-014e-4dcc-abb1-e6dde4901eff"), new DateTime(2024, 4, 7, 5, 27, 25, 688, DateTimeKind.Local).AddTicks(4993), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sangguniang Kabataan Kagawad" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DateCreated", "DateOfBirth", "DateUpdated", "Email", "FirstName", "Gender", "HasAgreedToTerms", "IsActive", "IsValidate", "LastName", "Nationality", "Occupation", "PIDNumber", "PImage", "Password", "PasswordResetToken", "PersonalDocumentId", "PhoneNumber", "Religion", "ResetTokenExpires", "Role", "SuffixName", "Token", "VerificationStatus", "VoterId", "VoterImages", "ZipCode", "isRemember" },
                values: new object[] { new Guid("46f13d19-c0b1-4516-9dbc-e6e75164806a"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 6, 21, 27, 25, 688, DateTimeKind.Utc).AddTicks(4356), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "garadojosephmartin98@gmail.com", "Joseph Martin", "", false, true, true, "Garado", "Filipino", "", "", "", "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", null, new Guid("7804a7e9-16b5-4a6f-928f-613ae98b19fd"), "09199431060", "", null, 2, null, "", 0, 2831, "PathImages\\VoterImages\\admin picture.png", "", null });

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
