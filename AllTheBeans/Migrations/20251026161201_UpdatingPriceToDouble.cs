using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllTheBeans.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingPriceToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CostInGBP",
                table: "Beans",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.UpdateData(
                table: "BeanOfTheDay",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 10, 26, 16, 12, 0, 587, DateTimeKind.Utc).AddTicks(4270));

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374590abf949489fb28f7",
                column: "CostInGBP",
                value: 17.590000152587891);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374591a995a2b48761408",
                column: "CostInGBP",
                value: 18.569999694824219);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374592169e1bfcca2fb1c",
                column: "CostInGBP",
                value: 16.440000534057617);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374593a88b14d9fff0e2e",
                column: "CostInGBP",
                value: 25.489999771118164);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374593ae6cb5148781b9b",
                column: "CostInGBP",
                value: 33.869998931884766);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a3745945fcae53593c42e7",
                column: "CostInGBP",
                value: 26.530000686645508);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a37459591e872ce11c3b41",
                column: "CostInGBP",
                value: 36.560001373291016);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374596122a40616cb8599",
                column: "CostInGBP",
                value: 39.259998321533203);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a37459771606d916a226ff",
                column: "CostInGBP",
                value: 17.690000534057617);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374599018ca32d01fee66",
                column: "CostInGBP",
                value: 22.920000076293945);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a3745997fa4069ce1b418f",
                column: "CostInGBP",
                value: 29.420000076293945);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a37459b7933d86991ce243",
                column: "CostInGBP",
                value: 10.270000457763672);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a37459caf60416d0571db4",
                column: "CostInGBP",
                value: 19.069999694824219);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a37459cc0f1fb1d1a24cf0",
                column: "CostInGBP",
                value: 32.770000457763672);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a37459cca42ce9e15676a3",
                column: "CostInGBP",
                value: 37.909999847412109);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "CostInGBP",
                table: "Beans",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "BeanOfTheDay",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 10, 26, 15, 19, 1, 812, DateTimeKind.Utc).AddTicks(2620));

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374590abf949489fb28f7",
                column: "CostInGBP",
                value: 17.59f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374591a995a2b48761408",
                column: "CostInGBP",
                value: 18.57f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374592169e1bfcca2fb1c",
                column: "CostInGBP",
                value: 16.44f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374593a88b14d9fff0e2e",
                column: "CostInGBP",
                value: 25.49f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374593ae6cb5148781b9b",
                column: "CostInGBP",
                value: 33.87f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a3745945fcae53593c42e7",
                column: "CostInGBP",
                value: 26.53f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a37459591e872ce11c3b41",
                column: "CostInGBP",
                value: 36.56f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374596122a40616cb8599",
                column: "CostInGBP",
                value: 39.26f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a37459771606d916a226ff",
                column: "CostInGBP",
                value: 17.69f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a374599018ca32d01fee66",
                column: "CostInGBP",
                value: 22.92f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a3745997fa4069ce1b418f",
                column: "CostInGBP",
                value: 29.42f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a37459b7933d86991ce243",
                column: "CostInGBP",
                value: 10.27f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a37459caf60416d0571db4",
                column: "CostInGBP",
                value: 19.07f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a37459cc0f1fb1d1a24cf0",
                column: "CostInGBP",
                value: 32.77f);

            migrationBuilder.UpdateData(
                table: "Beans",
                keyColumn: "Id",
                keyValue: "66a37459cca42ce9e15676a3",
                column: "CostInGBP",
                value: 37.91f);
        }
    }
}
