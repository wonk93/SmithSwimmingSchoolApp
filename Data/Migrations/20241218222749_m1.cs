using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmithSwimmingSchoolApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CoachSex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coaches_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groupings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Places = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groupings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Swimmers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SwimmerSex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Swimmers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Swimmers_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachId = table.Column<int>(type: "int", nullable: false),
                    LevelCourse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    SwimmerId = table.Column<int>(type: "int", nullable: false),
                    GroupingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Groupings_GroupingId",
                        column: x => x.GroupingId,
                        principalTable: "Groupings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Swimmers_SwimmerId",
                        column: x => x.SwimmerId,
                        principalTable: "Swimmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Administrator", "ADMINISTRATOR" },
                    { "2", null, "Coach", "COACH" },
                    { "3", null, "Swimmer", "SWIMMER" },
                    { "4", null, "Visitor", "VISITOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "100", 0, "10c8f5ca-8973-4c7c-8c5c-30a4d0f78b74", "admin@3s.com", true, "Administrator", false, null, "ADMIN@3S.COM", "ADMIN@3S.COM", "AQAAAAIAAYagAAAAEGwEz0QdXB+ALjuSLziFQrvvk2Dzf7JdLD3RGubdsmqmHKQXT7KAhBXjU78MhnZB8A==", null, false, "2d64aeb1-bcaf-455b-a665-cbfa1819e3c6", false, "admin@3s.com" },
                    { "101", 0, "0a1e205b-0c6b-4bd8-aea4-9be5f5d196e2", "johnsmith@3s.com", true, "John Smith", false, null, "JOHNSMITH@3S.COM", "JOHNSMITH@3S.COM", "AQAAAAIAAYagAAAAEKAo6FUDbLCesfrEfVmexdbRkff4e8mkkXo86hKbgifRZiWjIhE9dFMsZOxa78/bVg==", null, false, "c1dcf93d-5182-44f4-9073-663159d60a47", false, "johnsmith@3s.com" },
                    { "102", 0, "95afee3b-56c7-4eff-a341-554a49866983", "aliceaohnson@3s.com", true, "Alice Johnson", false, null, "ALICEJOHNSON@3S.COM", "ALICEJOHNSON@3S.COM", "AQAAAAIAAYagAAAAEEL2cmklYPOTU/UoTADoqZSgsVvC3j+AfwfKyfgxFy+YygH/5CfgIQiHYQlJ8QZmHA==", null, false, "b1c47b8b-0ade-4f45-a40d-9174f46b4d1b", false, "aliceaohnson@3s.com" },
                    { "103", 0, "ea4a3f62-ccc4-426e-ad2f-6c5251019178", "bobbrown@3s.com", true, "Bob Brown", false, null, "BOBBROWN@3S.COM", "BOBBROWN@3S.COM", "AQAAAAIAAYagAAAAEPe8A64wX5UwCmNqCR7UpNK7iSBpuqEg8cEt9Cdyb0B635WYMM6l6z2faM8hbko5Wg==", null, false, "94a72b7f-8d26-4fb4-9ecb-77e20831a215", false, "bobbrown@3s.com" },
                    { "104", 0, "6d143273-439e-4c8c-aa33-11ef4dc112e7", "charlieblack@3s.com", true, "Charlie Black", false, null, "CHARLIEBLACK@3S.COM", "CHARLIEBLACK@3S.COM", "AQAAAAIAAYagAAAAEOAa+vaeJuG8aWPjYlYjmwiQi8TSLzsrCw+X8gB2HK5vqIjfnXjh6GUBw0XYgftpdg==", null, false, "4302afac-3423-428d-aee8-4347f3c88a25", false, "charlieblack@3s.com" },
                    { "105", 0, "610bea20-86ee-4d88-be18-1bc969662b06", "dianawhite@3s.com", true, "Diana White", false, null, "DIANAWHITE@3S.COM", "DIANAWHITE@3S.COM", "AQAAAAIAAYagAAAAEMaIua5E9GfuiDQLGZKOM42jdRsoJRmbrNThwoDtgKu8ozJL37L/EiDLCQmBxEKr/g==", null, false, "eafb9ce4-debf-42b2-b28e-c054691ab7d8", false, "dianawhite@3s.com" },
                    { "106", 0, "4e1f2747-c805-4f54-ad6a-dd98181bc73d", "edwardgreen@3s.com", true, "Edward Green", false, null, "EDWARDGREEN@3S.COM", "EDWARDGREEN@3S.COM", "AQAAAAIAAYagAAAAEBKI7xVR/d1xW95gSm8fGqGXoX2KTZQd7tz015+G7Zoj2h1H4DHvZOTNAmMz4NBF/Q==", null, false, "a380127b-b913-4d37-a6a1-bd729af5493e", false, "edwardgreen@3s.com" },
                    { "107", 0, "132f6d59-f3dd-49b7-9e0e-6e83a130b639", "janedoe@3s.com", true, "Jane Doe", false, null, "JANEDOE@3S.COM", "JANEDOE@3S.COM", "AQAAAAIAAYagAAAAEM5Dn7B44BkI6IqPUFQwXuT4I9mzK6JPOGVe4samFoVTcrxxpo7wZ0FNSOjyVJeExQ==", null, false, "ae3b5d05-a7b1-4898-8d2a-b34f938d9f96", false, "janedoe@3s.com" },
                    { "108", 0, "c045da02-ab85-4b58-bec2-16c2f4d2138e", "michaeljordan@3s.com", true, "Michael Jordan", false, null, "MICHAELJORDAN@3S.COM", "MICHAELJORDAN@3S.COM", "AQAAAAIAAYagAAAAEND34BKUn2pu0fL8OZndy1kymknEgSoMImoxTX+L7GwEc9r2ADloltFBkJZUZ+Gxcg==", null, false, "f27c1594-e00f-4b4a-a067-0726b7b0540d", false, "michaeljordan@3s.com" },
                    { "109", 0, "4a93efee-846d-4f33-b03a-755d24cb00a8", "serenawilliams@3s.com", true, "Serena Williams", false, null, "SERENAWILLIAMS@3S.COM", "SERENAWILLIAMS@3S.COM", "AQAAAAIAAYagAAAAEOCv8or549gNfqDBEkDRgBGEKihw2NDX8GD2MCM/5IeVxASpEaW7JPhyyN9wvTfgaQ==", null, false, "5f9cb9ea-800e-49a1-b254-d47ce7826b50", false, "serenawilliams@3s.com" },
                    { "110", 0, "910caaf9-41c1-47fb-bb64-dd3b2ca0b895", "rogerfederer@3s.com", true, "Roger Federer", false, null, "ROGERFEDERER@3S.COM", "ROGERFEDERER@3S.COM", "AQAAAAIAAYagAAAAEAxfQ0pQBpdWyk5lVTSEFuItZDe9+YcrutL9Br/4ePY53SL3pF8h9isUJeZAmLqWSw==", null, false, "a50a1557-353b-4bbf-88b8-51c91de54fb8", false, "rogerfederer@3s.com" }
                });

            migrationBuilder.InsertData(
                table: "Groupings",
                columns: new[] { "Id", "EndDate", "Level", "Places", "StartDate", "StartTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beginner", 15, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0) },
                    { 2, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advanced", 10, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0) },
                    { 3, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intermediate", 12, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0) },
                    { 4, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Expert", 8, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0) },
                    { 5, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master", 6, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "100" },
                    { "2", "101" },
                    { "3", "102" },
                    { "3", "103" },
                    { "3", "104" },
                    { "3", "105" },
                    { "3", "106" },
                    { "2", "107" },
                    { "2", "108" },
                    { "2", "109" },
                    { "2", "110" }
                });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "CoachSex", "IdentityUserId", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 0, "101", "John Smith", "123-456-7890" },
                    { 2, 1, "107", "Jane Doe", "987-654-3210" },
                    { 3, 0, "108", "Michael Jordan", "333-444-5555" },
                    { 4, 1, "109", "Serena Williams", "777-888-9999" },
                    { 5, 1, "110", "Roger Federer", "555-666-7777" }
                });

            migrationBuilder.InsertData(
                table: "Swimmers",
                columns: new[] { "Id", "BirthDate", "IdentityUserId", "Name", "PhoneNumber", "SwimmerSex" },
                values: new object[,]
                {
                    { 1, new DateTime(2005, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "102", "Alice Johnson", "111-222-3333", 1 },
                    { 2, new DateTime(2008, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "103", "Bob Brown", "444-555-6666", 0 },
                    { 3, new DateTime(2010, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "104", "Charlie Black", "888-999-0000", 0 },
                    { 4, new DateTime(2006, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "105", "Diana White", "222-333-4444", 1 },
                    { 5, new DateTime(2007, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "106", "Edward Green", "555-777-8888", 0 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CoachId", "LevelCourse", "Title" },
                values: new object[,]
                {
                    { 1, 1, 0, "Beginner Swimming" },
                    { 2, 2, 1, "Advanced Swimming" },
                    { 3, 3, 1, "Intermediate Swimming" },
                    { 4, 4, 2, "Expert Swimming" },
                    { 5, 5, 3, "Master Swimming" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "CourseId", "GroupingId", "SwimmerId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 2, 2 },
                    { 3, 3, 3, 3 },
                    { 4, 4, 4, 4 },
                    { 5, 5, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "Content", "EnrollmentId" },
                values: new object[,]
                {
                    { 1, "Good progress", 1 },
                    { 2, "Excellent performance", 2 },
                    { 3, "Needs improvement", 3 },
                    { 4, "Outstanding swimmer", 4 },
                    { 5, "Beginner with potential", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_IdentityUserId",
                table: "Coaches",
                column: "IdentityUserId",
                unique: true,
                filter: "[IdentityUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CoachId",
                table: "Courses",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_GroupingId",
                table: "Enrollments",
                column: "GroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_SwimmerId",
                table: "Enrollments",
                column: "SwimmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_EnrollmentId",
                table: "Reports",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Swimmers_IdentityUserId",
                table: "Swimmers",
                column: "IdentityUserId",
                unique: true,
                filter: "[IdentityUserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Groupings");

            migrationBuilder.DropTable(
                name: "Swimmers");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "100" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "101" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "102" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "103" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "104" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "105" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "106" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "107" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "108" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "109" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "110" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "100");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "101");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "102");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "103");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "104");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "105");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "106");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "107");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "108");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "109");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "110");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
