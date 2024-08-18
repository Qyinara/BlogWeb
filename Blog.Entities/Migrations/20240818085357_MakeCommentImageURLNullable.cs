using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Entities.Migrations
{
    /// <inheritdoc />
    public partial class MakeCommentImageURLNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 55, DateTimeKind.Utc).AddTicks(981),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 16, 19, 33, 54, 44, DateTimeKind.Utc).AddTicks(8536));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Roles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(8964),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 16, 19, 33, 54, 44, DateTimeKind.Utc).AddTicks(6218));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(2772),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 16, 19, 33, 54, 43, DateTimeKind.Utc).AddTicks(9135));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Categories",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(317),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 16, 19, 33, 54, 43, DateTimeKind.Utc).AddTicks(6348));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 11, 53, 57, 54, DateTimeKind.Local).AddTicks(1084));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 11, 53, 57, 54, DateTimeKind.Local).AddTicks(1095));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 11, 53, 57, 54, DateTimeKind.Local).AddTicks(9643));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 11, 53, 57, 54, DateTimeKind.Local).AddTicks(9647));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 11, 53, 57, 55, DateTimeKind.Local).AddTicks(2189));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 16, 19, 33, 54, 44, DateTimeKind.Utc).AddTicks(8536),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 55, DateTimeKind.Utc).AddTicks(981));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Roles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 16, 19, 33, 54, 44, DateTimeKind.Utc).AddTicks(6218),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(8964));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 16, 19, 33, 54, 43, DateTimeKind.Utc).AddTicks(9135),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(2772));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Categories",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 16, 19, 33, 54, 43, DateTimeKind.Utc).AddTicks(6348),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(317));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 16, 22, 33, 54, 43, DateTimeKind.Local).AddTicks(7246));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 8, 16, 22, 33, 54, 43, DateTimeKind.Local).AddTicks(7259));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 16, 22, 33, 54, 44, DateTimeKind.Local).AddTicks(6999));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 8, 16, 22, 33, 54, 44, DateTimeKind.Local).AddTicks(7004));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 16, 22, 33, 54, 44, DateTimeKind.Local).AddTicks(9899));
        }
    }
}
