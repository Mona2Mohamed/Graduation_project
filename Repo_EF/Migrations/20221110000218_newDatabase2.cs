﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class newDatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acknowledges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AckNum = table.Column<int>(type: "int", nullable: false),
                    AckDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acknowledges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParamTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Satellites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    Mass = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SatelliteType = table.Column<int>(type: "int", nullable: true),
                    OrbitType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satellites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    Delay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AckId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Repeat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcknowledgeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => new { x.Id, x.SequenceNumber });
                    table.ForeignKey(
                        name: "FK_Plans_Acknowledges_AcknowledgeId",
                        column: x => x.AcknowledgeId,
                        principalTable: "Acknowledges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subsystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubSystemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubSystemType = table.Column<int>(type: "int", nullable: true),
                    SatelliteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subsystems_Satellites_SatelliteId",
                        column: x => x.SatelliteId,
                        principalTable: "Satellites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SatelliteStation",
                columns: table => new
                {
                    SatellitesId = table.Column<int>(type: "int", nullable: false),
                    StationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatelliteStation", x => new { x.SatellitesId, x.StationsId });
                    table.ForeignKey(
                        name: "FK_SatelliteStation_Satellites_SatellitesId",
                        column: x => x.SatellitesId,
                        principalTable: "Satellites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SatelliteStation_Stations_StationsId",
                        column: x => x.StationsId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PlanSequenceNumber = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanResults", x => new { x.Id, x.PlanId, x.PlanSequenceNumber });
                    table.ForeignKey(
                        name: "FK_PlanResults_Plans_PlanId_PlanSequenceNumber",
                        columns: x => new { x.PlanId, x.PlanSequenceNumber },
                        principalTable: "Plans",
                        principalColumns: new[] { "Id", "SequenceNumber" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubSystemId = table.Column<int>(type: "int", nullable: false),
                    SensorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    PlanSequenceNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commands_Plans_PlanId_PlanSequenceNumber",
                        columns: x => new { x.PlanId, x.PlanSequenceNumber },
                        principalTable: "Plans",
                        principalColumns: new[] { "Id", "SequenceNumber" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commands_Subsystems_SubSystemId",
                        column: x => x.SubSystemId,
                        principalTable: "Subsystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ParamTypeId = table.Column<int>(type: "int", nullable: false),
                    CommandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandParams", x => new { x.Id, x.CommandId, x.ParamTypeId });
                    table.ForeignKey(
                        name: "FK_CommandParams_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandParams_ParamTypes_ParamTypeId",
                        column: x => x.ParamTypeId,
                        principalTable: "ParamTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParamValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ParamTypeId = table.Column<int>(type: "int", nullable: false),
                    Device = table.Column<int>(type: "int", nullable: false),
                    CommandParamId = table.Column<int>(type: "int", nullable: false),
                    CommandId = table.Column<int>(type: "int", nullable: false),
                    CommandParamId1 = table.Column<int>(type: "int", nullable: false),
                    CommandParamCommandId = table.Column<int>(type: "int", nullable: false),
                    CommandParamParamTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamValues", x => new { x.Id, x.Device, x.ParamTypeId });
                    table.ForeignKey(
                        name: "FK_ParamValues_CommandParams_CommandParamId1_CommandParamCommandId_CommandParamParamTypeId",
                        columns: x => new { x.CommandParamId1, x.CommandParamCommandId, x.CommandParamParamTypeId },
                        principalTable: "CommandParams",
                        principalColumns: new[] { "Id", "CommandId", "ParamTypeId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandParams_CommandId",
                table: "CommandParams",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandParams_ParamTypeId",
                table: "CommandParams",
                column: "ParamTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_PlanId_PlanSequenceNumber",
                table: "Commands",
                columns: new[] { "PlanId", "PlanSequenceNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_SubSystemId",
                table: "Commands",
                column: "SubSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_ParamValues_CommandParamId1_CommandParamCommandId_CommandParamParamTypeId",
                table: "ParamValues",
                columns: new[] { "CommandParamId1", "CommandParamCommandId", "CommandParamParamTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanResults_PlanId_PlanSequenceNumber",
                table: "PlanResults",
                columns: new[] { "PlanId", "PlanSequenceNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Plans_AcknowledgeId",
                table: "Plans",
                column: "AcknowledgeId");

            migrationBuilder.CreateIndex(
                name: "IX_SatelliteStation_StationsId",
                table: "SatelliteStation",
                column: "StationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Subsystems_SatelliteId",
                table: "Subsystems",
                column: "SatelliteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParamValues");

            migrationBuilder.DropTable(
                name: "PlanResults");

            migrationBuilder.DropTable(
                name: "SatelliteStation");

            migrationBuilder.DropTable(
                name: "CommandParams");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "ParamTypes");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Subsystems");

            migrationBuilder.DropTable(
                name: "Acknowledges");

            migrationBuilder.DropTable(
                name: "Satellites");
        }
    }
}
