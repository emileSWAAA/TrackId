using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackId.Data.Migrations
{
    public partial class Genre002seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("69532b21-3b21-4a16-9a13-efe69f109cca"),
                column: "CreateDateTime",
                value: new DateTime(2022, 3, 21, 10, 36, 36, 858, DateTimeKind.Utc).AddTicks(2270));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b3e78bd1-5fa8-4dd9-ac80-19063dbc82e1"),
                column: "ConcurrencyStamp",
                value: "2b48b0b8-b64a-4ef6-be81-6628841f819f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bba64c5f-a693-4f88-96a3-7207018bc14e"),
                column: "ConcurrencyStamp",
                value: "aff79ed3-b8a2-4bdc-9b62-f69bcf432164");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f8dbb603-a0f4-4b38-a1d6-ccb370d58586"),
                column: "ConcurrencyStamp",
                value: "59aa1bef-63c2-464e-a271-b6f7dbabc7fe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8a24bfa9-26fe-4bd1-9d84-ab2a83d6f2fb"),
                columns: new[] { "ConcurrencyStamp", "CreateDateTime", "PasswordHash" },
                values: new object[] { "0611ee3e-e828-4584-97e9-cc8686998ef5", new DateTime(2022, 3, 21, 10, 36, 36, 841, DateTimeKind.Utc).AddTicks(5962), "AQAAAAEAACcQAAAAEJP3AuZc2VpHVOe63AaBEt2ATy66DoVNw773mTW29v4N+cFx5RbGEKgm7kfMdD/u3w==" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreateDateTime", "DeleteDateTime", "Description", "IsDeleted", "Name", "ParentGenreId" },
                values: new object[] { new Guid("1af4af53-05a4-4934-b8b1-758d9750f8d9"), new DateTime(2022, 3, 21, 10, 36, 36, 858, DateTimeKind.Utc).AddTicks(8057), null, "Electronic dance music", false, "EDM", null });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreateDateTime", "DeleteDateTime", "Description", "IsDeleted", "Name", "ParentGenreId" },
                values: new object[] { new Guid("2443687e-b3e6-49aa-87ec-4ad9e7eae6bd"), new DateTime(2022, 3, 21, 10, 36, 36, 858, DateTimeKind.Utc).AddTicks(7766), null, "Hardstyle", false, "Hardstyle", new Guid("1af4af53-05a4-4934-b8b1-758d9750f8d9") });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreateDateTime", "DeleteDateTime", "Description", "IsDeleted", "Name", "ParentGenreId" },
                values: new object[] { new Guid("fac53697-439f-48fd-9050-832a981adf2c"), new DateTime(2022, 3, 21, 10, 36, 36, 858, DateTimeKind.Utc).AddTicks(8055), null, "Hardstyle", false, "Raw hardstyle", new Guid("2443687e-b3e6-49aa-87ec-4ad9e7eae6bd") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("fac53697-439f-48fd-9050-832a981adf2c"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2443687e-b3e6-49aa-87ec-4ad9e7eae6bd"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("1af4af53-05a4-4934-b8b1-758d9750f8d9"));

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
        }
    }
}
