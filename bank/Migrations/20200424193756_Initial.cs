using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bank.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullname = table.Column<string>(nullable: true),
                    passport = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: true),
                    adress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DepositType",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typeName = table.Column<string>(nullable: true),
                    minMoney = table.Column<int>(nullable: false),
                    maxMoney = table.Column<int>(nullable: false),
                    period = table.Column<int>(nullable: false),
                    capitalization = table.Column<int>(nullable: false),
                    percent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeLogin = table.Column<string>(nullable: true),
                    EmployeePassword = table.Column<string>(nullable: true),
                    EmployeeFullName = table.Column<string>(nullable: true),
                    EmployeePhoto = table.Column<string>(nullable: true),
                    isAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    initialMoney = table.Column<int>(nullable: false),
                    dateOfOpening = table.Column<string>(nullable: true),
                    plannedFinalAmountOfMoney = table.Column<int>(nullable: false),
                    typeOfDeposit = table.Column<int>(nullable: false),
                    DepositTypeid = table.Column<int>(nullable: true),
                    contractNumber = table.Column<int>(nullable: false),
                    clientID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.id);
                    table.ForeignKey(
                        name: "FK_Deposit_DepositType_DepositTypeid",
                        column: x => x.DepositTypeid,
                        principalTable: "DepositType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposit_Client_clientID",
                        column: x => x.clientID,
                        principalTable: "Client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateOfSigning = table.Column<DateTime>(nullable: false),
                    clientID = table.Column<int>(nullable: false),
                    employeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.id);
                    table.ForeignKey(
                        name: "FK_Contract_Client_clientID",
                        column: x => x.clientID,
                        principalTable: "Client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Employee_employeeID",
                        column: x => x.employeeID,
                        principalTable: "Employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_clientID",
                table: "Contract",
                column: "clientID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_employeeID",
                table: "Contract",
                column: "employeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_DepositTypeid",
                table: "Deposit",
                column: "DepositTypeid");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_clientID",
                table: "Deposit",
                column: "clientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "DepositType");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
