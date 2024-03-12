using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MwTech.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountingPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Possition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rfid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminRights = table.Column<bool>(type: "bit", nullable: false),
                    SuperAdminRights = table.Column<bool>(type: "bit", nullable: false),
                    RegisterDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IfsProductRecipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Contract = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AlternativeNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RevisionNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LineItemNo = table.Column<int>(type: "int", nullable: false),
                    LineSequence = table.Column<int>(type: "int", nullable: false),
                    RevisionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AlternativeDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ComponentPart = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    QtyPerAssembly = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    PartsByWeight = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    ShrinkageFactor = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    ComponentScrap = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    ConsumptionItemDb = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrintUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EffPhaseInDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffPhaseOutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlternativeState = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PartStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IfsProductRecipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IfsProductStructures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Contract = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AlternativeNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RevisionNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LineItemNo = table.Column<int>(type: "int", nullable: false),
                    LineSequence = table.Column<int>(type: "int", nullable: false),
                    RevisionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AlternativeDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ComponentPart = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    QtyPerAssembly = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    ShrinkageFactor = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    ComponentScrap = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    ConsumptionItemDb = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrintUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EffPhaseInDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffPhaseOutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlternativeState = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PartStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IfsProductStructures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IfsRoutes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Contract = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    AlternativeNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RevisionNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    OperationNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    OperationDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AlternativeDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WorkCenterNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LaborClassNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MachRunFactor = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    LaborRunFactor = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    CrewSize = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    RunTimeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    SetupLaborClassNo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    MachSetupTime = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    LaborSetupTime = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    SetupCrewSize = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    MoveTime = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    Overlap = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    OperationId = table.Column<int>(type: "int", nullable: true),
                    ToolId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToolQuantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IfsRoutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IfsWorkCentersMaterialsRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkCenterNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtyRequired = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtyDelivered = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReqState = table.Column<int>(type: "int", nullable: false),
                    SourceLocation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IfsWorkCentersMaterialsRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdinalNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoutingTools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToolNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutingTools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScadaReport",
                columns: table => new
                {
                    SEQ_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OP_ID = table.Column<long>(type: "bigint", nullable: false),
                    TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TYPE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PART_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REPORTED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QTY_REPORTED = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: false),
                    QTY_ISSUED = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: false),
                    TIME_CONSUMED = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LOT_BATCH_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HANDLING_UNIT_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WORK_CENTER_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RESOURCE_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TIME_START = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TIME_STOP = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScadaReport", x => x.SEQ_ID);
                });

            migrationBuilder.CreateTable(
                name: "Temps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idx01 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Idx02 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<bool>(type: "bit", nullable: false),
                    Time = table.Column<bool>(type: "bit", nullable: false),
                    Cost = table.Column<bool>(type: "bit", nullable: false),
                    Boolean = table.Column<bool>(type: "bit", nullable: false),
                    PeriodInSeconds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSettingsPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    AppSettingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettingsPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSettingsPositions_AppSettings_AppSettingId",
                        column: x => x.AppSettingId,
                        principalTable: "AppSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "Tyres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TyreNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RimDiameterInInches = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadIndex = table.Column<int>(type: "int", nullable: false),
                    PlyRating = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tyres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tyres_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tyres_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountingPeriodId = table.Column<int>(type: "int", nullable: false),
                    FromCurrencyId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false),
                    EstimatedRate = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyRates_AccountingPeriods_AccountingPeriodId",
                        column: x => x.AccountingPeriodId,
                        principalTable: "AccountingPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrencyRates_Currencies_FromCurrencyId",
                        column: x => x.FromCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecipeCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ScrapNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipes_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipes_RecipeCategories_RecipeCategoryId",
                        column: x => x.RecipeCategoryId,
                        principalTable: "RecipeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TyreVersion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TyreId = table.Column<int>(type: "int", nullable: false),
                    IsAccepted01 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted01ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted01Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAccepted02 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted02ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted02Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TyreVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TyreVersion_AspNetUsers_Accepted01ByUserId",
                        column: x => x.Accepted01ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TyreVersion_AspNetUsers_Accepted02ByUserId",
                        column: x => x.Accepted02ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TyreVersion_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TyreVersion_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TyreVersion_Tyres_TyreId",
                        column: x => x.TyreId,
                        principalTable: "Tyres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    AlternativeNo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultVersion = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAccepted01 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted01ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted01Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAccepted02 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted02ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted02Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeVersions_AspNetUsers_Accepted01ByUserId",
                        column: x => x.Accepted01ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeVersions_AspNetUsers_Accepted02ByUserId",
                        column: x => x.Accepted02ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeVersions_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeVersions_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeVersions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Boms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdinalNumber = table.Column<int>(type: "int", nullable: false),
                    SetId = table.Column<int>(type: "int", nullable: false),
                    SetVersionId = table.Column<int>(type: "int", nullable: false),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    PartQty = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnProductionOrder = table.Column<bool>(type: "bit", nullable: false),
                    DoNotIncludeInTkw = table.Column<bool>(type: "bit", nullable: false),
                    DoNotIncludeInWeight = table.Column<bool>(type: "bit", nullable: false),
                    DoNotExportToIfs = table.Column<bool>(type: "bit", nullable: false),
                    Excess = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    Layer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineCategoryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machines_MachineCategories_MachineCategoryId",
                        column: x => x.MachineCategoryId,
                        principalTable: "MachineCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ManufactoringRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteVersionId = table.Column<int>(type: "int", nullable: false),
                    OrdinalNumber = table.Column<int>(type: "int", nullable: false),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    WorkCenterId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    ResourceQty = table.Column<decimal>(type: "decimal(12,3)", precision: 12, scale: 3, nullable: false, defaultValue: 1m),
                    OperationLabourConsumption = table.Column<decimal>(type: "decimal(12,6)", precision: 12, scale: 6, nullable: false, defaultValue: 0m),
                    OperationMachineConsumption = table.Column<decimal>(type: "decimal(12,6)", precision: 12, scale: 6, nullable: false, defaultValue: 0m),
                    ChangeOverResourceId = table.Column<int>(type: "int", nullable: true),
                    ChangeOverNumberOfEmployee = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 1m),
                    ChangeOverLabourConsumption = table.Column<decimal>(type: "decimal(12,6)", precision: 12, scale: 6, nullable: false, defaultValue: 0m),
                    ChangeOverMachineConsumption = table.Column<decimal>(type: "decimal(12,6)", precision: 12, scale: 6, nullable: false, defaultValue: 0m),
                    Overlap = table.Column<decimal>(type: "decimal(12,6)", precision: 12, scale: 6, nullable: false, defaultValue: 0m),
                    MoveTime = table.Column<decimal>(type: "decimal(12,6)", precision: 12, scale: 6, nullable: false, defaultValue: 0m),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true),
                    RoutingToolId = table.Column<int>(type: "int", nullable: true),
                    ToolQuantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufactoringRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufactoringRoutes_RoutingTools_RoutingToolId",
                        column: x => x.RoutingToolId,
                        principalTable: "RoutingTools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Shift = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasurementHeaders_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeasurementHeaders_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementHeaderId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasurementPositions_MeasurementHeaders_MeasurementHeaderId",
                        column: x => x.MeasurementHeaderId,
                        principalTable: "MeasurementHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    UnitId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    No = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdinalNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteSource = table.Column<int>(type: "int", nullable: false),
                    CategoryNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TkwCountExcess = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    NoCalculateTkw = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertiesProductCategoriesMapId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OldProductNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechCardNumber = table.Column<int>(type: "int", nullable: true),
                    Idx01 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idx02 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReturnedFromProd = table.Column<bool>(type: "bit", nullable: false),
                    NoCalculateTkw = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsTest = table.Column<bool>(type: "bit", nullable: false),
                    Client = table.Column<bool>(type: "bit", nullable: false),
                    MwbaseMatid = table.Column<int>(type: "int", nullable: false),
                    MwbaseWyrobId = table.Column<int>(type: "int", nullable: false),
                    ContentsOfRubber = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false, defaultValue: 0m),
                    Density = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false, defaultValue: 0m),
                    ScalesId = table.Column<int>(type: "int", nullable: false),
                    DecimalPlaces = table.Column<int>(type: "int", nullable: true),
                    WeightTolerance = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    Aps01 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aps02 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ean13Code = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Cost = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    Markup = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    EstimatedCost = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    EstimatedMarkup = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    Contract = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    LaborClassNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LabourClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resources_Resources_LabourClassId",
                        column: x => x.LabourClassId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resources_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SettingCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingCategoryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrdinalNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineCategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingCategories_MachineCategories_MachineCategoryId",
                        column: x => x.MachineCategoryId,
                        principalTable: "MachineCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SettingCategories_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountingPeriodId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    LabourCost = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    MaterialCost = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    MarkupCost = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    ProductLabourCost = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    EstimatedCost = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    EstimatedLabourCost = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    EstimatedMaterialCost = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    EstimatedMarkupCost = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    EstimatedProductLabourCost = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    IsCalculated = table.Column<bool>(type: "bit", nullable: false),
                    IsImported = table.Column<bool>(type: "bit", nullable: false),
                    CalculatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImportedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCosts_AccountingPeriods_AccountingPeriodId",
                        column: x => x.AccountingPeriodId,
                        principalTable: "AccountingPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCosts_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCosts_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCosts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCosts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPropertyVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    AlternativeNo = table.Column<int>(type: "int", nullable: false),
                    DefaultVersion = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAccepted01 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted01ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted01Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAccepted02 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted02ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted02Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPropertyVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPropertyVersions_AspNetUsers_Accepted01ByUserId",
                        column: x => x.Accepted01ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPropertyVersions_AspNetUsers_Accepted02ByUserId",
                        column: x => x.Accepted02ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPropertyVersions_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPropertyVersions_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPropertyVersions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    AlternativeNo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultVersion = table.Column<bool>(type: "bit", nullable: false),
                    ComarchDefaultVersion = table.Column<bool>(type: "bit", nullable: false),
                    IfsDefaultVersion = table.Column<bool>(type: "bit", nullable: false),
                    ToIfs = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductQty = table.Column<decimal>(type: "decimal(12,6)", precision: 12, scale: 6, nullable: false),
                    ProductWeight = table.Column<decimal>(type: "decimal(12,6)", precision: 12, scale: 6, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAccepted01 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted01ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted01Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAccepted02 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted02ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted02Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MwbaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVersions_AspNetUsers_Accepted01ByUserId",
                        column: x => x.Accepted01ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductVersions_AspNetUsers_Accepted02ByUserId",
                        column: x => x.Accepted02ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductVersions_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductVersions_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductVersions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RouteVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    AlternativeNo = table.Column<int>(type: "int", nullable: false),
                    DefaultVersion = table.Column<bool>(type: "bit", nullable: false),
                    ComarchDefaultVersion = table.Column<bool>(type: "bit", nullable: false),
                    IfsDefaultVersion = table.Column<bool>(type: "bit", nullable: false),
                    ToIfs = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductQty = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAccepted01 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted01ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted01Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAccepted02 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted02ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted02Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteVersions_AspNetUsers_Accepted01ByUserId",
                        column: x => x.Accepted01ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteVersions_AspNetUsers_Accepted02ByUserId",
                        column: x => x.Accepted02ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteVersions_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteVersions_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteVersions_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteVersions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSettingVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlternativeNo = table.Column<int>(type: "int", nullable: false),
                    ProductSettingVersionNumber = table.Column<int>(type: "int", nullable: false),
                    Rev = table.Column<int>(type: "int", nullable: false),
                    DefaultVersion = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MwBaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MachineCategoryId = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    WorkCenterId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IsAccepted01 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted01ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted01Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAccepted02 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted02ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted02Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAccepted03 = table.Column<bool>(type: "bit", nullable: false),
                    Accepted03ByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Accepted03Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastCsvFileDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MwbaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSettingVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSettingVersions_AspNetUsers_Accepted01ByUserId",
                        column: x => x.Accepted01ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSettingVersions_AspNetUsers_Accepted02ByUserId",
                        column: x => x.Accepted02ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSettingVersions_AspNetUsers_Accepted03ByUserId",
                        column: x => x.Accepted03ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSettingVersions_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSettingVersions_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductSettingVersions_MachineCategories_MachineCategoryId",
                        column: x => x.MachineCategoryId,
                        principalTable: "MachineCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSettingVersions_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSettingVersions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSettingVersions_Resources_WorkCenterId",
                        column: x => x.WorkCenterId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeVersionId = table.Column<int>(type: "int", nullable: false),
                    StageNo = table.Column<int>(type: "int", nullable: false),
                    StageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductNumber = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MixerVolume = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    PrevStageQty = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    DivideQtyBy = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    MultiplyQtyBy = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    WorkCenterId = table.Column<int>(type: "int", nullable: true),
                    LabourClassId = table.Column<int>(type: "int", nullable: true),
                    CrewSize = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    LabourRunFactor = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    StageTimeInSeconds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeStages_RecipeVersions_RecipeVersionId",
                        column: x => x.RecipeVersionId,
                        principalTable: "RecipeVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeStages_Resources_LabourClassId",
                        column: x => x.LabourClassId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeStages_Resources_WorkCenterId",
                        column: x => x.WorkCenterId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrdinalNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettingCategoryId = table.Column<int>(type: "int", nullable: false),
                    MachineCategoryId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    UnitId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    Value = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsNumeric = table.Column<bool>(type: "bit", nullable: false),
                    AlwaysOnPrintout = table.Column<bool>(type: "bit", nullable: false),
                    HideOnPrintout = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_MachineCategories_MachineCategoryId",
                        column: x => x.MachineCategoryId,
                        principalTable: "MachineCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Settings_SettingCategories_SettingCategoryId",
                        column: x => x.SettingCategoryId,
                        principalTable: "SettingCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Settings_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeManuals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeVersionId = table.Column<int>(type: "int", nullable: false),
                    RecipeStageId = table.Column<int>(type: "int", nullable: false),
                    PositionNo = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    TextValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeManuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeManuals_RecipeStages_RecipeStageId",
                        column: x => x.RecipeStageId,
                        principalTable: "RecipeStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeManuals_RecipeVersions_RecipeVersionId",
                        column: x => x.RecipeVersionId,
                        principalTable: "RecipeVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipePositionsPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageNumber = table.Column<int>(type: "int", nullable: false),
                    ProductNumber = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RecipeStageId = table.Column<int>(type: "int", nullable: false),
                    BagIsIncluded = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkCenterId = table.Column<int>(type: "int", nullable: true),
                    LabourClassId = table.Column<int>(type: "int", nullable: true),
                    CrewSize = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TimeInSeconds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipePositionsPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipePositionsPackages_RecipeStages_RecipeStageId",
                        column: x => x.RecipeStageId,
                        principalTable: "RecipeStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipePositionsPackages_Resources_LabourClassId",
                        column: x => x.LabourClassId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipePositionsPackages_Resources_WorkCenterId",
                        column: x => x.WorkCenterId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSettingVersionPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSettingVersionId = table.Column<int>(type: "int", nullable: false),
                    SettingId = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    Value = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MwbaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSettingVersionPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSettingVersionPositions_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSettingVersionPositions_ProductSettingVersions_ProductSettingVersionId",
                        column: x => x.ProductSettingVersionId,
                        principalTable: "ProductSettingVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSettingVersionPositions_Settings_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipePositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeStageId = table.Column<int>(type: "int", nullable: false),
                    RecipePositionPackageId = table.Column<int>(type: "int", nullable: true),
                    PositionNo = table.Column<int>(type: "int", nullable: false),
                    PacketNo = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductQty = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReturnFromProcessing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipePositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipePositions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipePositions_RecipePositionsPackages_RecipePositionPackageId",
                        column: x => x.RecipePositionPackageId,
                        principalTable: "RecipePositionsPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipePositions_RecipeStages_RecipeStageId",
                        column: x => x.RecipeStageId,
                        principalTable: "RecipeStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    ProductPropertiesVersionId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Value = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductProperties_ProductPropertyVersions_ProductPropertiesVersionId",
                        column: x => x.ProductPropertiesVersionId,
                        principalTable: "ProductPropertyVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVersionProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    ProductVersionId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(16,6)", precision: 16, scale: 6, nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(12,5)", precision: 12, scale: 5, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVersionProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVersionProperties_ProductVersions_ProductVersionId",
                        column: x => x.ProductVersionId,
                        principalTable: "ProductVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdinalNo = table.Column<int>(type: "int", nullable: false),
                    PropertyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScadaPropertyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    IsGeneralProperty = table.Column<bool>(type: "bit", nullable: false),
                    IsVersionProperty = table.Column<bool>(type: "bit", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    DecimalPlaces = table.Column<int>(type: "int", nullable: true),
                    HideOnReport = table.Column<bool>(type: "bit", nullable: false),
                    PropertiesProductCategoriesMapId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertiesProductCategoriesMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertiesProductCategoriesMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertiesProductCategoriesMaps_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertiesProductCategoriesMaps_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "Id", "Description", "Order" },
                values: new object[,]
                {
                    { 1, "E-mail", 2 },
                    { 2, "Ogólne", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ADE32A3F-6149-475A-8155-CFE5D69ACA42", "B50B7D83-F6E6-4DE3-8346-7D0E8501EEB5", "Ksiegowy", "KSIEGOWY" },
                    { "C854A873-3D75-4973-AD7E-A83C95726133", "C778085D-D407-4936-8A19-350C6817AA5D", "Administrator", "ADMINISTRATOR" },
                    { "EC23C152-A1C5-4D9A-B8D4-FAE62D5F059D", "A1277894-E24D-490E-A3BA-83F9CF5F838D", "Technolog", "TECHNOLOG" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AdminRights", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Possition", "ReferenceNumber", "RefreshToken", "RefreshTokenExpiryTime", "RegisterDateTime", "Rfid", "SecurityStamp", "SuperAdminRights", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2d1c0b97-5e74-49a1-9252-58788873dde5", 0, false, "3958de97-2995-4fa4-af37-eab993b16fb2", "m.wieczorek1972@gmail.com", true, "Mariusz", false, "Wieczorek", true, null, "M.WIECZOREK1972@GMAIL.COM", "M.WIECZOREK1972@GMAIL.COM", "AQAAAAIAAYagAAAAEHA2HWq0uJVCUIzYmHM+yamfiVIrNSnuinbmP43J+KJQJPFhjNVR1O46ElzpL2++wQ==", "11111111", false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 12, 10, 18, 13, 281, DateTimeKind.Utc).AddTicks(4483), null, "ZZNB2SYLM3POSL4P4JSV2MLP7N6KVTV4", false, false, "m.wieczorek1972@gmail.com" });

            migrationBuilder.InsertData(
                table: "AppSettingsPositions",
                columns: new[] { "Id", "AppSettingId", "Description", "Key", "Order", "Type", "Value" },
                values: new object[,]
                {
                    { 1, 1, "Host", "HostSmtp", 1, 0, "smtp.gmail.com" },
                    { 2, 1, "Port", "Port", 2, 2, "587" },
                    { 3, 1, "Adres e-mail nadawcy", "SenderEmail", 3, 0, "mariusz.wieczorek.testy@gmail.com" },
                    { 4, 1, "Hasło", "SenderEmailPassword", 4, 4, "rmhfvaurzyxnuztn" },
                    { 5, 1, "Nazwa nadawcy", "SenderName", 5, 0, "Mariusz Wieczorek" },
                    { 6, 1, "Login nadawcy", "SenderLogin", 6, 0, "" },
                    { 7, 2, "Czy wyświetlać banner na stronie głównej?", "BannerVisible", 1, 1, "True" },
                    { 8, 2, "Folor footera strona głównej", "FooterColor", 2, 5, "#dc3545" },
                    { 9, 2, "Główny adres e-mail administratora", "AdminEmail", 3, 0, "mariusz.wieczorek@kabat.pl" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSettingsPositions_AppSettingId",
                table: "AppSettingsPositions",
                column: "AppSettingId");

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
                name: "IX_Boms_PartId",
                table: "Boms",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Boms_SetId",
                table: "Boms",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Boms_SetVersionId",
                table: "Boms",
                column: "SetVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_AccountingPeriodId",
                table: "CurrencyRates",
                column: "AccountingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_FromCurrencyId",
                table: "CurrencyRates",
                column: "FromCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineCategories_ProductCategoryId",
                table: "MachineCategories",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MachineCategoryId",
                table: "Machines",
                column: "MachineCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufactoringRoutes_ChangeOverResourceId",
                table: "ManufactoringRoutes",
                column: "ChangeOverResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufactoringRoutes_OperationId",
                table: "ManufactoringRoutes",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufactoringRoutes_ProductCategoryId",
                table: "ManufactoringRoutes",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufactoringRoutes_ResourceId",
                table: "ManufactoringRoutes",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufactoringRoutes_RouteVersionId",
                table: "ManufactoringRoutes",
                column: "RouteVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufactoringRoutes_RoutingToolId",
                table: "ManufactoringRoutes",
                column: "RoutingToolId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufactoringRoutes_WorkCenterId",
                table: "ManufactoringRoutes",
                column: "WorkCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementHeaders_CreatedByUserId",
                table: "MeasurementHeaders",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementHeaders_ModifiedByUserId",
                table: "MeasurementHeaders",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementHeaders_ProductId",
                table: "MeasurementHeaders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementPositions_MeasurementHeaderId",
                table: "MeasurementPositions",
                column: "MeasurementHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_OperationNumber",
                table: "Operations",
                column: "OperationNumber",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ProductCategoryId",
                table: "Operations",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_UnitId",
                table: "Operations",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_PropertiesProductCategoriesMapId",
                table: "ProductCategories",
                column: "PropertiesProductCategoriesMapId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCosts_AccountingPeriodId",
                table: "ProductCosts",
                column: "AccountingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCosts_CreatedByUserId",
                table: "ProductCosts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCosts_CurrencyId",
                table: "ProductCosts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCosts_ModifiedByUserId",
                table: "ProductCosts",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCosts_ProductId",
                table: "ProductCosts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProperties_ProductPropertiesVersionId",
                table: "ProductProperties",
                column: "ProductPropertiesVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProperties_PropertyId",
                table: "ProductProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyVersions_Accepted01ByUserId",
                table: "ProductPropertyVersions",
                column: "Accepted01ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyVersions_Accepted02ByUserId",
                table: "ProductPropertyVersions",
                column: "Accepted02ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyVersions_CreatedByUserId",
                table: "ProductPropertyVersions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyVersions_ModifiedByUserId",
                table: "ProductPropertyVersions",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyVersions_ProductId",
                table: "ProductPropertyVersions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedByUserId",
                table: "Products",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModifiedByUserId",
                table: "Products",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductNumber",
                table: "Products",
                column: "ProductNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSettingVersionPositions_ModifiedByUserId",
                table: "ProductSettingVersionPositions",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSettingVersionPositions_ProductSettingVersionId",
                table: "ProductSettingVersionPositions",
                column: "ProductSettingVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSettingVersionPositions_SettingId",
                table: "ProductSettingVersionPositions",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSettingVersions_Accepted01ByUserId",
                table: "ProductSettingVersions",
                column: "Accepted01ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSettingVersions_Accepted02ByUserId",
                table: "ProductSettingVersions",
                column: "Accepted02ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSettingVersions_Accepted03ByUserId",
                table: "ProductSettingVersions",
                column: "Accepted03ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSettingVersions_CreatedByUserId",
                table: "ProductSettingVersions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSettingVersions_MachineCategoryId",
                table: "ProductSettingVersions",
                column: "MachineCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSettingVersions_MachineId",
                table: "ProductSettingVersions",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSettingVersions_ModifiedByUserId",
                table: "ProductSettingVersions",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSettingVersions_ProductId",
                table: "ProductSettingVersions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSettingVersions_WorkCenterId",
                table: "ProductSettingVersions",
                column: "WorkCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersionProperties_ProductVersionId",
                table: "ProductVersionProperties",
                column: "ProductVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersionProperties_PropertyId",
                table: "ProductVersionProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersions_Accepted01ByUserId",
                table: "ProductVersions",
                column: "Accepted01ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersions_Accepted02ByUserId",
                table: "ProductVersions",
                column: "Accepted02ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersions_CreatedByUserId",
                table: "ProductVersions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersions_ModifiedByUserId",
                table: "ProductVersions",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersions_ProductId",
                table: "ProductVersions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ProductCategoryId",
                table: "Properties",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertiesProductCategoriesMapId",
                table: "Properties",
                column: "PropertiesProductCategoriesMapId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UnitId",
                table: "Properties",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesProductCategoriesMaps_ProductCategoryId",
                table: "PropertiesProductCategoriesMaps",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesProductCategoriesMaps_PropertyId",
                table: "PropertiesProductCategoriesMaps",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeManuals_RecipeStageId",
                table: "RecipeManuals",
                column: "RecipeStageId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeManuals_RecipeVersionId",
                table: "RecipeManuals",
                column: "RecipeVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipePositions_ProductId",
                table: "RecipePositions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipePositions_RecipePositionPackageId",
                table: "RecipePositions",
                column: "RecipePositionPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipePositions_RecipeStageId",
                table: "RecipePositions",
                column: "RecipeStageId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipePositionsPackages_LabourClassId",
                table: "RecipePositionsPackages",
                column: "LabourClassId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipePositionsPackages_RecipeStageId",
                table: "RecipePositionsPackages",
                column: "RecipeStageId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipePositionsPackages_WorkCenterId",
                table: "RecipePositionsPackages",
                column: "WorkCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CreatedByUserId",
                table: "Recipes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ModifiedByUserId",
                table: "Recipes",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RecipeCategoryId",
                table: "Recipes",
                column: "RecipeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RecipeNumber",
                table: "Recipes",
                column: "RecipeNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStages_LabourClassId",
                table: "RecipeStages",
                column: "LabourClassId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStages_RecipeVersionId",
                table: "RecipeStages",
                column: "RecipeVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStages_WorkCenterId",
                table: "RecipeStages",
                column: "WorkCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeVersions_Accepted01ByUserId",
                table: "RecipeVersions",
                column: "Accepted01ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeVersions_Accepted02ByUserId",
                table: "RecipeVersions",
                column: "Accepted02ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeVersions_CreatedByUserId",
                table: "RecipeVersions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeVersions_ModifiedByUserId",
                table: "RecipeVersions",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeVersions_RecipeId",
                table: "RecipeVersions",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_LabourClassId",
                table: "Resources",
                column: "LabourClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ProductCategoryId",
                table: "Resources",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceNumber",
                table: "Resources",
                column: "ResourceNumber",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_UnitId",
                table: "Resources",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteVersions_Accepted01ByUserId",
                table: "RouteVersions",
                column: "Accepted01ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteVersions_Accepted02ByUserId",
                table: "RouteVersions",
                column: "Accepted02ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteVersions_AlternativeNo",
                table: "RouteVersions",
                column: "AlternativeNo")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_RouteVersions_CreatedByUserId",
                table: "RouteVersions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteVersions_ModifiedByUserId",
                table: "RouteVersions",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteVersions_ProductCategoryId",
                table: "RouteVersions",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteVersions_ProductId",
                table: "RouteVersions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteVersions_VersionNumber",
                table: "RouteVersions",
                column: "VersionNumber")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_SettingCategories_MachineCategoryId",
                table: "SettingCategories",
                column: "MachineCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingCategories_ProductCategoryId",
                table: "SettingCategories",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_MachineCategoryId",
                table: "Settings",
                column: "MachineCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_SettingCategoryId",
                table: "Settings",
                column: "SettingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_UnitId",
                table: "Settings",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Temps_Idx01_Idx02",
                table: "Temps",
                columns: new[] { "Idx01", "Idx02" },
                unique: true,
                filter: "[Idx01] IS NOT NULL AND [Idx02] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tyres_CreatedByUserId",
                table: "Tyres",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tyres_ModifiedByUserId",
                table: "Tyres",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tyres_TyreNumber",
                table: "Tyres",
                column: "TyreNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TyreVersion_Accepted01ByUserId",
                table: "TyreVersion",
                column: "Accepted01ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TyreVersion_Accepted02ByUserId",
                table: "TyreVersion",
                column: "Accepted02ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TyreVersion_CreatedByUserId",
                table: "TyreVersion",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TyreVersion_ModifiedByUserId",
                table: "TyreVersion",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TyreVersion_TyreId",
                table: "TyreVersion",
                column: "TyreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boms_ProductVersions_SetVersionId",
                table: "Boms",
                column: "SetVersionId",
                principalTable: "ProductVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boms_Products_PartId",
                table: "Boms",
                column: "PartId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Boms_Products_SetId",
                table: "Boms",
                column: "SetId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineCategories_ProductCategories_ProductCategoryId",
                table: "MachineCategories",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ManufactoringRoutes_Operations_OperationId",
                table: "ManufactoringRoutes",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ManufactoringRoutes_ProductCategories_ProductCategoryId",
                table: "ManufactoringRoutes",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ManufactoringRoutes_Resources_ChangeOverResourceId",
                table: "ManufactoringRoutes",
                column: "ChangeOverResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ManufactoringRoutes_Resources_ResourceId",
                table: "ManufactoringRoutes",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ManufactoringRoutes_Resources_WorkCenterId",
                table: "ManufactoringRoutes",
                column: "WorkCenterId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ManufactoringRoutes_RouteVersions_RouteVersionId",
                table: "ManufactoringRoutes",
                column: "RouteVersionId",
                principalTable: "RouteVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementHeaders_Products_ProductId",
                table: "MeasurementHeaders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_ProductCategories_ProductCategoryId",
                table: "Operations",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_PropertiesProductCategoriesMaps_PropertiesProductCategoriesMapId",
                table: "ProductCategories",
                column: "PropertiesProductCategoriesMapId",
                principalTable: "PropertiesProductCategoriesMaps",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperties_Properties_PropertyId",
                table: "ProductProperties",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVersionProperties_Properties_PropertyId",
                table: "ProductVersionProperties",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertiesProductCategoriesMaps_PropertiesProductCategoriesMapId",
                table: "Properties",
                column: "PropertiesProductCategoriesMapId",
                principalTable: "PropertiesProductCategoriesMaps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_ProductCategories_ProductCategoryId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertiesProductCategoriesMaps_ProductCategories_ProductCategoryId",
                table: "PropertiesProductCategoriesMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Units_UnitId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertiesProductCategoriesMaps_PropertiesProductCategoriesMapId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "AppSettingsPositions");

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
                name: "Boms");

            migrationBuilder.DropTable(
                name: "CurrencyRates");

            migrationBuilder.DropTable(
                name: "IfsProductRecipes");

            migrationBuilder.DropTable(
                name: "IfsProductStructures");

            migrationBuilder.DropTable(
                name: "IfsRoutes");

            migrationBuilder.DropTable(
                name: "IfsWorkCentersMaterialsRequests");

            migrationBuilder.DropTable(
                name: "ManufactoringRoutes");

            migrationBuilder.DropTable(
                name: "MeasurementPositions");

            migrationBuilder.DropTable(
                name: "ProductCosts");

            migrationBuilder.DropTable(
                name: "ProductProperties");

            migrationBuilder.DropTable(
                name: "ProductSettingVersionPositions");

            migrationBuilder.DropTable(
                name: "ProductVersionProperties");

            migrationBuilder.DropTable(
                name: "RecipeManuals");

            migrationBuilder.DropTable(
                name: "RecipePositions");

            migrationBuilder.DropTable(
                name: "ScadaReport");

            migrationBuilder.DropTable(
                name: "Temps");

            migrationBuilder.DropTable(
                name: "TyreVersion");

            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "RouteVersions");

            migrationBuilder.DropTable(
                name: "RoutingTools");

            migrationBuilder.DropTable(
                name: "MeasurementHeaders");

            migrationBuilder.DropTable(
                name: "AccountingPeriods");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "ProductPropertyVersions");

            migrationBuilder.DropTable(
                name: "ProductSettingVersions");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "ProductVersions");

            migrationBuilder.DropTable(
                name: "RecipePositionsPackages");

            migrationBuilder.DropTable(
                name: "Tyres");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "SettingCategories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RecipeStages");

            migrationBuilder.DropTable(
                name: "MachineCategories");

            migrationBuilder.DropTable(
                name: "RecipeVersions");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RecipeCategories");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "PropertiesProductCategoriesMaps");

            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
