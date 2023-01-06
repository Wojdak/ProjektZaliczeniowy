using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projektzaliczeniowy.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hostteam = table.Column<int>(name: "host_team", type: "int", nullable: false),
                    guestteam = table.Column<int>(name: "guest_team", type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tickets = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    score = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stadium = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    howManyPeople = table.Column<int>(type: "int", nullable: false),
                    seats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalPrice = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeam",
                columns: table => new
                {
                    MatchesId = table.Column<int>(type: "int", nullable: false),
                    TeamsListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeam", x => new { x.MatchesId, x.TeamsListId });
                    table.ForeignKey(
                        name: "FK_MatchTeam_Matches_MatchesId",
                        column: x => x.MatchesId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchTeam_Teams_TeamsListId",
                        column: x => x.TeamsListId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(name: "first_name", type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateofbirth = table.Column<DateTime>(name: "date_of_birth", type: "datetime2", nullable: false),
                    position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "Id", "date", "guest_team", "host_team", "price", "score", "tickets" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, 80, null, 107 },
                    { 2, new DateTime(2022, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 70, null, 100 },
                    { 3, new DateTime(2022, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4, 80, null, 260 },
                    { 4, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 75, null, 324 }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "city", "country", "name", "stadium" },
                values: new object[,]
                {
                    { 1, "Manchester", "England", "Manchester City", "Etihad Stadium" },
                    { 2, "London", "England", "Arsenal", "Emirates Stadium" },
                    { 3, "Liverpool", "England", "Liverpool FC", "Anfield Stadium" },
                    { 4, "London", "England", "Chelsea", "Stamford Bridge Stadium" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "date_of_birth", "first_name", "nationality", "position", "surname", "TeamId" },
                values: new object[,]
                {
                    { 1, new DateTime(1997, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gabriel", "Brazilian", "Attacker", "Jesus", 2 },
                    { 2, new DateTime(1998, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Martin", "Norwegian", "Midfielder", "odegaard", 2 },
                    { 3, new DateTime(1997, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kieran", "Scottish", "Defender", "Tierney", 2 },
                    { 4, new DateTime(2000, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Erling", "Norwegian", "Attacker", "Haaland", 1 },
                    { 5, new DateTime(1991, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kevin", "Belgian", "Midfielder", "De Bruyne", 1 },
                    { 6, new DateTime(1997, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ruben", "Portuguese", "Defender", "Dias", 1 },
                    { 7, new DateTime(1992, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mohamed", "Egyptian", "Attacker", "Salah", 3 },
                    { 8, new DateTime(2090, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jordan", "English", "Midfielder", "Henderson", 3 },
                    { 9, new DateTime(1991, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Virgil", "Dutch", "Defender", "van Dijk", 3 },
                    { 10, new DateTime(1999, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kai", "German", "Attacker", "Havertz", 4 },
                    { 11, new DateTime(1999, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mason", "English", "Midfielder", "Mount", 4 },
                    { 12, new DateTime(1984, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thiago", "Brazilian", "Defender", "Silva", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_TeamsListId",
                table: "MatchTeam",
                column: "TeamsListId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MatchId",
                table: "Tickets",
                column: "MatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchTeam");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
