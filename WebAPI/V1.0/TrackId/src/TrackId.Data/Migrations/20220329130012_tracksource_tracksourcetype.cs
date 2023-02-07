using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackId.Data.Migrations
{
    public partial class tracksource_tracksourcetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreamSlug = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    IsBrokenLink = table.Column<bool>(type: "bit", nullable: false),
                    TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackSourceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackSources_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackSourceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmbeddedUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackSourceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackSourceTypes_TrackSources_Id",
                        column: x => x.Id,
                        principalTable: "TrackSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("69532b21-3b21-4a16-9a13-efe69f109cca"),
                column: "CreateDateTime",
                value: new DateTime(2022, 3, 29, 13, 0, 11, 279, DateTimeKind.Utc).AddTicks(6595));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b3e78bd1-5fa8-4dd9-ac80-19063dbc82e1"),
                column: "ConcurrencyStamp",
                value: "1eca591a-2b58-4e38-8a63-928383077091");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bba64c5f-a693-4f88-96a3-7207018bc14e"),
                column: "ConcurrencyStamp",
                value: "02a8ffc2-37cf-48b4-9251-5c24977a2760");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f8dbb603-a0f4-4b38-a1d6-ccb370d58586"),
                column: "ConcurrencyStamp",
                value: "eda8cabb-387d-4fad-90b4-c159f0c9d806");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8a24bfa9-26fe-4bd1-9d84-ab2a83d6f2fb"),
                columns: new[] { "ConcurrencyStamp", "CreateDateTime", "PasswordHash" },
                values: new object[] { "899b4df6-bff5-47f1-bff9-30a101d652ce", new DateTime(2022, 3, 29, 13, 0, 11, 252, DateTimeKind.Utc).AddTicks(542), "AQAAAAEAACcQAAAAEP4+W1cvPKydT8PsRkRDSrXTnVEBU5eDh1LDxbOikprw8fuh+Ci+Ex8/xgKSCu4GfQ==" });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("1af4af53-05a4-4934-b8b1-758d9750f8d9"),
                column: "CreateDateTime",
                value: new DateTime(2022, 3, 29, 13, 0, 11, 280, DateTimeKind.Utc).AddTicks(2704));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2443687e-b3e6-49aa-87ec-4ad9e7eae6bd"),
                column: "CreateDateTime",
                value: new DateTime(2022, 3, 29, 13, 0, 11, 280, DateTimeKind.Utc).AddTicks(2385));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("fac53697-439f-48fd-9050-832a981adf2c"),
                column: "CreateDateTime",
                value: new DateTime(2022, 3, 29, 13, 0, 11, 280, DateTimeKind.Utc).AddTicks(2702));

            migrationBuilder.CreateIndex(
                name: "IX_TrackSources_TrackId",
                table: "TrackSources",
                column: "TrackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackSourceTypes");

            migrationBuilder.DropTable(
                name: "TrackSources");

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

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("1af4af53-05a4-4934-b8b1-758d9750f8d9"),
                column: "CreateDateTime",
                value: new DateTime(2022, 3, 21, 10, 36, 36, 858, DateTimeKind.Utc).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2443687e-b3e6-49aa-87ec-4ad9e7eae6bd"),
                column: "CreateDateTime",
                value: new DateTime(2022, 3, 21, 10, 36, 36, 858, DateTimeKind.Utc).AddTicks(7766));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("fac53697-439f-48fd-9050-832a981adf2c"),
                column: "CreateDateTime",
                value: new DateTime(2022, 3, 21, 10, 36, 36, 858, DateTimeKind.Utc).AddTicks(8055));
        }
    }
}
