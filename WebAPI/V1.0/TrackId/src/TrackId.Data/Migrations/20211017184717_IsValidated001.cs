using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackId.Data.Migrations
{
    public partial class IsValidated001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValidated",
                table: "Artists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "CreateDateTime", "DeleteDateTime", "IsDeleted", "IsValidated", "Name" },
                values: new object[] { new Guid("69532b21-3b21-4a16-9a13-efe69f109cca"), new DateTime(2021, 10, 17, 18, 47, 17, 158, DateTimeKind.Utc).AddTicks(8533), null, false, false, "TBA" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("69532b21-3b21-4a16-9a13-efe69f109cca"));

            migrationBuilder.DropColumn(
                name: "IsValidated",
                table: "Artists");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b3e78bd1-5fa8-4dd9-ac80-19063dbc82e1"),
                column: "ConcurrencyStamp",
                value: "7a5e4e92-cc7e-4bb0-93c5-7a4fc08ddc09");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bba64c5f-a693-4f88-96a3-7207018bc14e"),
                column: "ConcurrencyStamp",
                value: "a2c1097a-ed12-4eab-ac33-e5ce9d4d4439");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f8dbb603-a0f4-4b38-a1d6-ccb370d58586"),
                column: "ConcurrencyStamp",
                value: "e911fdc5-872f-48e3-9684-e167c788479c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8a24bfa9-26fe-4bd1-9d84-ab2a83d6f2fb"),
                columns: new[] { "ConcurrencyStamp", "CreateDateTime", "PasswordHash" },
                values: new object[] { "9e522823-93a0-4885-8401-212a8dd8812f", new DateTime(2021, 10, 16, 21, 34, 31, 899, DateTimeKind.Utc).AddTicks(7757), "AQAAAAEAACcQAAAAEKzW+K0dEKetiVxGYYWHKUpQHarco2YXe8JQC19Fd4j+m06PZckLjcNMyBSHREXKvQ==" });
        }
    }
}
