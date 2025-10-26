using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AllTheBeans.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeanOfTheDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BeanId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeanOfTheDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColourName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsBOTD = table.Column<bool>(type: "bit", nullable: false),
                    CostInGBP = table.Column<float>(type: "real", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    ColourId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beans_Colours_ColourId",
                        column: x => x.ColourId,
                        principalTable: "Colours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beans_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Colours",
                columns: new[] { "Id", "ColourName" },
                values: new object[,]
                {
                    { 1, "dark roast" },
                    { 2, "golden" },
                    { 3, "green" },
                    { 4, "light roast" },
                    { 5, "medium roast" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryName" },
                values: new object[,]
                {
                    { 1, "Peru" },
                    { 2, "Vietnam" },
                    { 3, "Colombia" },
                    { 4, "Brazil" },
                    { 5, "Honduras" }
                });

            migrationBuilder.InsertData(
                table: "Beans",
                columns: new[] { "Id", "ColourId", "CostInGBP", "CountryId", "Description", "ImageUrl", "IsBOTD", "Name" },
                values: new object[,]
                {
                    { "66a374590abf949489fb28f7", 2, 17.59f, 3, "Commodo veniam voluptate elit reprehenderit incididunt. Ut laboris dolor sint cupidatat ut adipisicing. Nostrud magna labore voluptate commodo in sunt proident sunt deserunt dolor ullamco officia tempor dolor. Laboris exercitation est mollit eiusmod nostrud. Sit qui ullamco minim cillum officia irure cillum tempor eu. Et cupidatat proident amet dolore non minim.\r\n", "https://images.unsplash.com/photo-1442550528053-c431ecb55509", false, "XLEEN" },
                    { "66a374591a995a2b48761408", 2, 18.57f, 2, "Dolor fugiat duis dolore ut occaecat. Excepteur nostrud velit aute dolore sint labore do eu amet. Anim adipisicing quis ut excepteur tempor magna reprehenderit non ut excepteur minim. Anim dolore eiusmod nisi nulla aliquip aliqua occaecat.\r\n", "https://images.unsplash.com/photo-1641399756770-9b0b104e67c1", false, "ISONUS" },
                    { "66a374592169e1bfcca2fb1c", 5, 16.44f, 3, "Incididunt exercitation mollit duis consectetur consequat duis culpa tempor. Fugiat nisi fugiat dolore irure in. Fugiat nulla amet dolore labore laboris sint laborum pariatur commodo amet. Ut velit sit proident fugiat cillum cupidatat ea.\r\n", "https://images.unsplash.com/photo-1694763768576-0c7c3af6a4d8", false, "FUTURIS" },
                    { "66a374593a88b14d9fff0e2e", 3, 25.49f, 2, "Deserunt consequat ea incididunt aliquip. Occaecat excepteur minim occaecat aute amet adipisicing. Tempor id veniam ipsum et tempor pariatur anim elit laboris commodo mollit. Ipsum incididunt Lorem veniam id fugiat incididunt consequat est et. Id deserunt eiusmod esse duis cupidatat Lorem. Ullamco Lorem ullamco cupidatat nostrud amet id minim ut voluptate adipisicing ipsum. Fugiat reprehenderit laborum proident eiusmod esse sint adipisicing fugiat ex.\r\n", "https://images.unsplash.com/photo-1549420751-ea3f7ab42006", false, "LOCAZONE" },
                    { "66a374593ae6cb5148781b9b", 3, 33.87f, 3, "Cillum nostrud mollit non ad dolore ad dolore veniam. Adipisicing anim commodo fugiat aute commodo occaecat officia id officia ullamco. Dolore irure magna aliqua fugiat incididunt ullamco ea. Aliqua eu pariatur cupidatat ut.\r\n", "https://images.unsplash.com/photo-1522809269485-981d0c303355", false, "ZILLAN" },
                    { "66a3745945fcae53593c42e7", 3, 26.53f, 2, "Labore veniam amet ipsum eu dolor. Aliquip Lorem et eiusmod exercitation. Amet ex eu deserunt labore est ex consectetur ut fugiat. Duis veniam voluptate elit consequat tempor nostrud enim mollit occaecat.\r\n", "https://images.unsplash.com/photo-1512568400610-62da28bc8a13", false, "EARWAX" },
                    { "66a37459591e872ce11c3b41", 4, 36.56f, 2, "Reprehenderit est laboris tempor quis exercitation laboris. Aute nulla aliqua consectetur nostrud ullamco cupidatat do cillum amet reprehenderit mollit non voluptate. Deserunt consectetur reprehenderit nostrud enim proident ea. Quis quis voluptate ex dolore non reprehenderit minim veniam nisi aute do incididunt voluptate. Duis aliquip commodo cupidatat anim ut ullamco eiusmod culpa velit incididunt.\r\n", "https://images.unsplash.com/photo-1692299108834-038511803008", false, "EVENTEX" },
                    { "66a374596122a40616cb8599", 1, 39.26f, 1, "Ipsum cupidatat nisi do elit veniam Lorem magna. Ullamco qui exercitation fugiat pariatur sunt dolore Lorem magna magna pariatur minim. Officia amet incididunt ad proident. Dolore est irure ex fugiat. Voluptate sunt qui ut irure commodo excepteur enim labore incididunt quis duis. Velit anim amet tempor ut labore sint deserunt.\r\n", "https://images.unsplash.com/photo-1672306319681-7b6d7ef349cf", false, "TURNABOUT" },
                    { "66a37459771606d916a226ff", 1, 17.69f, 4, "Et deserunt nisi in anim cillum sint voluptate proident. Est occaecat id cupidatat cupidatat ex veniam irure veniam pariatur excepteur duis labore occaecat amet. Culpa adipisicing nisi esse consequat adipisicing anim. Fugiat tempor enim ullamco sint anim qui enim. Voluptate duis proident reprehenderit et duis nisi. In consectetur nisi eu cupidatat voluptate ullamco nulla esse cupidatat dolore sit. Cupidatat laboris adipisicing ullamco mollit culpa cupidatat ex laborum consectetur consectetur.\r\n", "https://images.unsplash.com/photo-1598198192305-46b0805890d3", true, "RONBERT" },
                    { "66a374599018ca32d01fee66", 3, 22.92f, 4, "Mollit deserunt tempor qui consectetur excepteur non. Laborum voluptate voluptate laborum non magna et. Ea velit ipsum labore occaecat ea do cupidatat duis adipisicing. Ut eiusmod dolor anim et ea ea. Aliquip mollit aliqua nisi velit consequat nisi. Laborum velit anim non incididunt non qui commodo. Ea voluptate dolore pariatur eu enim.\r\n", "https://images.unsplash.com/photo-1692296115158-38194aafa7df", false, "NITRACYR" },
                    { "66a3745997fa4069ce1b418f", 3, 29.42f, 4, "Esse ad eiusmod eiusmod nisi cillum magna quis non voluptate nulla est labore in sunt. Magna aliqua pariatur commodo deserunt. Pariatur pariatur pariatur id excepteur ex elit veniam.\r\n", "https://images.unsplash.com/photo-1544486864-3087e2e20d91", false, "XEREX" },
                    { "66a37459b7933d86991ce243", 4, 10.27f, 2, "Qui deserunt adipisicing nulla ad enim commodo reprehenderit id veniam consequat ut do ea officia. Incididunt ex esse cupidatat consequat. Sit incididunt ex magna sint reprehenderit id minim non.\r\n", "https://images.unsplash.com/photo-1508690207469-5c5aebedf76d", false, "ZYTRAC" },
                    { "66a37459caf60416d0571db4", 1, 19.07f, 5, "Velit quis veniam velit et sint. Irure excepteur officia ipsum sint. Est ipsum pariatur exercitation voluptate commodo. Ex irure commodo exercitation labore nulla qui dolore ad quis.\r\n", "https://images.unsplash.com/photo-1673208127664-23a2f3b27921", false, "ZANITY" },
                    { "66a37459cc0f1fb1d1a24cf0", 3, 32.77f, 1, "Pariatur qui Lorem sunt labore Lorem nulla nulla ea excepteur Lorem cillum amet. Amet ea officia incididunt culpa non. Do reprehenderit qui eiusmod dolore est deserunt labore do et dolore eiusmod quis elit.\r\n", "https://images.unsplash.com/photo-1692299108333-471157a30882", false, "KLUGGER" },
                    { "66a37459cca42ce9e15676a3", 5, 37.91f, 3, "Veniam laborum consequat minim laborum mollit id ea Lorem in. Labore aliqua dolore quis sunt aliquip commodo aute excepteur. Voluptate tempor consequat pariatur do esse consectetur sunt ut mollit magna enim.\r\n", "https://images.unsplash.com/photo-1522120378538-41fb9564bc75", false, "PARAGONIA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beans_ColourId",
                table: "Beans",
                column: "ColourId");

            migrationBuilder.CreateIndex(
                name: "IX_Beans_CountryId",
                table: "Beans",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Colours_ColourName",
                table: "Colours",
                column: "ColourName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryName",
                table: "Countries",
                column: "CountryName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeanOfTheDay");

            migrationBuilder.DropTable(
                name: "Beans");

            migrationBuilder.DropTable(
                name: "Colours");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
