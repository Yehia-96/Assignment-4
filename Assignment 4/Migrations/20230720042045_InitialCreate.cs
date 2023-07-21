using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_4.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    participantId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(name: "Full Name", type: "varchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.participantId);
                });

            migrationBuilder.CreateTable(
                name: "Kitty",
                columns: table => new
                {
                    kittyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventName = table.Column<string>(name: "Event Name", type: "varchar(25)", nullable: false),
                    Currency = table.Column<int>(type: "INTEGER", nullable: false),
                    participantId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitty", x => x.kittyId);
                    table.ForeignKey(
                        name: "FK_Kitty_Participant_participantId",
                        column: x => x.participantId,
                        principalTable: "Participant",
                        principalColumn: "participantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    expenseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    kittyId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpensesNames = table.Column<string>(name: "Expenses Names", type: "varchar(25)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.expenseId);
                    table.ForeignKey(
                        name: "FK_Expenses_Kitty_kittyId",
                        column: x => x.kittyId,
                        principalTable: "Kitty",
                        principalColumn: "kittyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubParticipant",
                columns: table => new
                {
                    subParticipantId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    expenseId = table.Column<int>(type: "INTEGER", nullable: false),
                    kittyId = table.Column<int>(type: "INTEGER", nullable: false),
                    MembersNames = table.Column<string>(name: "Members Names", type: "varchar(25)", nullable: false),
                    OwedAmount = table.Column<decimal>(name: "Owed Amount", type: "decimal(6, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubParticipant", x => x.subParticipantId);
                    table.ForeignKey(
                        name: "FK_SubParticipant_Expenses_expenseId",
                        column: x => x.expenseId,
                        principalTable: "Expenses",
                        principalColumn: "expenseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubParticipant_Kitty_kittyId",
                        column: x => x.kittyId,
                        principalTable: "Kitty",
                        principalColumn: "kittyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_kittyId",
                table: "Expenses",
                column: "kittyId");

            migrationBuilder.CreateIndex(
                name: "IX_Kitty_participantId",
                table: "Kitty",
                column: "participantId");

            migrationBuilder.CreateIndex(
                name: "IX_SubParticipant_expenseId",
                table: "SubParticipant",
                column: "expenseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubParticipant_kittyId",
                table: "SubParticipant",
                column: "kittyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubParticipant");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Kitty");

            migrationBuilder.DropTable(
                name: "Participant");
        }
    }
}
