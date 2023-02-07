using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackId.Data.Migrations
{
    public partial class Genre001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GenreId",
                table: "Tracks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ParentGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_Genres_ParentGenreId",
                        column: x => x.ParentGenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("69532b21-3b21-4a16-9a13-efe69f109cca"),
                column: "CreateDateTime",
                value: new DateTime(2022, 3, 21, 10, 31, 25, 27, DateTimeKind.Utc).AddTicks(6881));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b3e78bd1-5fa8-4dd9-ac80-19063dbc82e1"),
                column: "ConcurrencyStamp",
                value: "a950110d-14b9-48bd-a44c-2fc35ed297f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bba64c5f-a693-4f88-96a3-7207018bc14e"),
                column: "ConcurrencyStamp",
                value: "2222cad4-1758-4a0f-8ccf-4234544645fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f8dbb603-a0f4-4b38-a1d6-ccb370d58586"),
                column: "ConcurrencyStamp",
                value: "88975938-3cd3-4940-8895-5f95ff5ca4dd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8a24bfa9-26fe-4bd1-9d84-ab2a83d6f2fb"),
                columns: new[] { "ConcurrencyStamp", "CreateDateTime", "PasswordHash" },
                values: new object[] { "92718187-f81e-4ca8-b97b-b7da4437efc1", new DateTime(2022, 3, 21, 10, 31, 25, 10, DateTimeKind.Utc).AddTicks(2579), "AQAAAAEAACcQAAAAELhDMi4sIlnBJ+oMFx5ThF4nCBTJAh3nVfi8bW8i6RlXi3k+1T7W4k7NELyu+e0c2g==" });

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_GenreId",
                table: "Tracks",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ParentGenreId",
                table: "Genres",
                column: "ParentGenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Genres_GenreId",
                table: "Tracks",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Genres_GenreId",
                table: "Tracks");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_GenreId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Tracks");

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("69532b21-3b21-4a16-9a13-efe69f109cca"),
                column: "CreateDateTime",
                value: new DateTime(2021, 10, 17, 18, 47, 17, 158, DateTimeKind.Utc).AddTicks(8533));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b3e78bd1-5fa8-4dd9-ac80-19063dbc82e1"),
                column: "ConcurrencyStamp",
                value: "f24959ab-8a77-46cb-89fc-1018ea07610b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bba64c5f-a693-4f88-96a3-7207018bc14e"),
                column: "ConcurrencyStamp",
                value: "ab92ee8c-c351-4d53-a357-e6e79c699b54");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f8dbb603-a0f4-4b38-a1d6-ccb370d58586"),
                column: "ConcurrencyStamp",
                value: "846c1053-1081-4acd-a197-829655054848");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8a24bfa9-26fe-4bd1-9d84-ab2a83d6f2fb"),
                columns: new[] { "ConcurrencyStamp", "CreateDateTime", "PasswordHash" },
                values: new object[] { "d1c8c309-d062-4525-bf1c-439176a3930a", new DateTime(2021, 10, 17, 18, 47, 17, 153, DateTimeKind.Utc).AddTicks(3368), "AQAAAAEAACcQAAAAEMwb4nz0pl4OWhyOVrcSABAOC6mB037v5W2CqxHNAePYWzXQXcrosE40/u61WK8DhA==" });
        }
    }
}
