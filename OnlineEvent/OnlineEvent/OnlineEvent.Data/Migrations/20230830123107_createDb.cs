using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineEvent.Data.Migrations
{
    /// <inheritdoc />
    public partial class createDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionWebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationDeadLine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quota = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreTickets = table.Column<bool>(type: "bit", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUserEvents",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EventUserType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserEvents", x => new { x.EventId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AppUserEvents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WebSiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketInfos_Events_Id",
                        column: x => x.Id,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "Name", "NormalizedName" },
                values: new object[] { 1, null, new DateTime(2023, 8, 30, 15, 31, 7, 768, DateTimeKind.Local).AddTicks(5562), "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "InstitutionName", "InstitutionWebSite", "IsActive", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { 1, 0, "e2da00da-eb34-488b-b959-5480e9f39687", new DateTime(2023, 8, 30, 15, 31, 7, 768, DateTimeKind.Local).AddTicks(5698), "admin@example.com", true, null, null, true, false, null, "admin", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEASxyJeimlGnc3bbUjTEfk8xpE6GcQImDVw8JelW7mPE1Nl8nNwZvFL4YTUKfFzANw==", null, false, null, "admin", false, "admin@example.com", "Admin" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "IsActive" },
                values: new object[,]
                {
                    { 1, "Müzikli Festival", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5691), true },
                    { 2, "Sahne", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5704), true },
                    { 3, "Spor", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5705), true }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CityName", "CreatedDate", "IsActive" },
                values: new object[,]
                {
                    { 1, "Adana", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5855), false },
                    { 2, "Adıyaman", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5858), false },
                    { 3, "Afyonkarahisar", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5860), false },
                    { 4, "Ağrı", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5861), false },
                    { 5, "Amasya", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5862), false },
                    { 6, "Ankara", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5863), false },
                    { 7, "Antalya", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5864), false },
                    { 8, "Artvin", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5867), false },
                    { 9, "Aydın", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5868), false },
                    { 10, "Balıkesir", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5869), false },
                    { 11, "Bilecik", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5870), false },
                    { 12, "Bingöl", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5872), false },
                    { 13, "Bitlis", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5873), false },
                    { 14, "Bolu", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5874), false },
                    { 15, "Burdur", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5875), false },
                    { 16, "Bursa", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5876), false },
                    { 17, "Çanakkale", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5877), false },
                    { 18, "Çankırı", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5878), false },
                    { 19, "Çorum", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5879), false },
                    { 20, "Denizli", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5881), false },
                    { 21, "Diyarbakır", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5882), false },
                    { 22, "Edirne", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5883), false },
                    { 23, "Elazığ", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5884), false },
                    { 24, "Erzincan", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5886), false },
                    { 25, "Erzurum", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5887), false },
                    { 26, "Eskişehir", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5888), false },
                    { 27, "Gaziantep", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5889), false },
                    { 28, "Giresun", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5890), false },
                    { 29, "Gümüşhane", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5891), false },
                    { 30, "Hakkari", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5892), false },
                    { 31, "Hatay", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5893), false },
                    { 32, "Isparta", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5894), false },
                    { 33, "Mersin", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5895), false },
                    { 34, "İstanbul", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5896), false },
                    { 35, "İzmir", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5897), false },
                    { 36, "Kars", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5899), false },
                    { 37, "Kastamonu", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5900), false },
                    { 38, "Kayseri", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5901), false },
                    { 39, "Kırklareli", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5902), false },
                    { 40, "Kırşehir", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5903), false },
                    { 41, "Kocaeli", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5904), false },
                    { 42, "Konya", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5906), false },
                    { 43, "Kütahya", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5907), false },
                    { 44, "Malatya", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5908), false },
                    { 45, "Manisa", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5909), false },
                    { 46, "Kahramanmaraş", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5910), false },
                    { 47, "Mardin", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5911), false },
                    { 48, "Muğla", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5912), false },
                    { 49, "Muş", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5913), false },
                    { 50, "Nevşehir", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5914), false },
                    { 51, "Niğde", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5915), false },
                    { 52, "Ordu", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5916), false },
                    { 53, "Rize", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5917), false },
                    { 54, "Sakarya", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5919), false },
                    { 55, "Samsun", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5920), false },
                    { 56, "Siirt", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5921), false },
                    { 57, "Sinop", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5922), false },
                    { 58, "Sivas", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5923), false },
                    { 59, "Tekirdağ", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5924), false },
                    { 60, "Tokat", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5925), false },
                    { 61, "Trabzon", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5926), false },
                    { 62, "Tunceli", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5927), false },
                    { 63, "Şanlıurfa", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5928), false },
                    { 64, "Uşak", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5970), false },
                    { 65, "Van", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5972), false },
                    { 66, "Yozgat", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5973), false },
                    { 67, "Zonguldak", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5973), false },
                    { 68, "Aksaray", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5974), false },
                    { 69, "Bayburt", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5975), false },
                    { 70, "Karaman", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5976), false },
                    { 71, "Kırıkkale", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5977), false },
                    { 72, "Batman", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5978), false },
                    { 73, "Şırnak", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5979), false },
                    { 74, "Bartın", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5980), false },
                    { 75, "Ardahan", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5981), false },
                    { 76, "Iğdır", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5982), false },
                    { 77, "Yalova", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5983), false },
                    { 78, "Karabük", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5984), false },
                    { 79, "Kilis", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5985), false },
                    { 80, "Osmaniye", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5986), false },
                    { 81, "Düzce", new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(5987), false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "ApplicationDeadLine", "AreTickets", "CategoryId", "CityId", "CreatedDate", "CreatorUserId", "Description", "EventDate", "ImageUrl", "IsActive", "Quota", "Title" },
                values: new object[] { 1, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 34, new DateTime(2023, 8, 30, 15, 31, 7, 765, DateTimeKind.Local).AddTicks(6137), 1, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, 50, "İlk etkinliğim" });

            migrationBuilder.InsertData(
                table: "AppUserEvents",
                columns: new[] { "EventId", "UserId", "EventUserType" },
                values: new object[] { 1, 1, "Creator" });

            migrationBuilder.InsertData(
                table: "TicketInfos",
                columns: new[] { "Id", "TicketPrice", "WebSiteUrl" },
                values: new object[] { 1, 50m, "www.google.com" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserEvents_UserId",
                table: "AppUserEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CityId",
                table: "Events",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatorUserId",
                table: "Events",
                column: "CreatorUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserEvents");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TicketInfos");

            migrationBuilder.DropTable(
                name: "UserRefreshToken");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
