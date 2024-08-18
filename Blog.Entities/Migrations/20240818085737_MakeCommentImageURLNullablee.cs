using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Entities.Migrations
{
    /// <inheritdoc />
    public partial class MakeCommentImageURLNullablee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 18, 8, 57, 36, 726, DateTimeKind.Utc).AddTicks(5315),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 55, DateTimeKind.Utc).AddTicks(981));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Roles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 18, 8, 57, 36, 726, DateTimeKind.Utc).AddTicks(3328),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(8964));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 18, 8, 57, 36, 725, DateTimeKind.Utc).AddTicks(7239),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(2772));

            migrationBuilder.AlterColumn<string>(
                name: "CommentImageURL",
                table: "Comments",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Categories",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 18, 8, 57, 36, 725, DateTimeKind.Utc).AddTicks(4916),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(317));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 11, 57, 36, 725, DateTimeKind.Local).AddTicks(5668));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 11, 57, 36, 725, DateTimeKind.Local).AddTicks(5681));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 11, 57, 36, 726, DateTimeKind.Local).AddTicks(4009));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 11, 57, 36, 726, DateTimeKind.Local).AddTicks(4014));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 11, 57, 36, 726, DateTimeKind.Local).AddTicks(6545));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 55, DateTimeKind.Utc).AddTicks(981),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 18, 8, 57, 36, 726, DateTimeKind.Utc).AddTicks(5315));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Roles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(8964),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 18, 8, 57, 36, 726, DateTimeKind.Utc).AddTicks(3328));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(2772),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 18, 8, 57, 36, 725, DateTimeKind.Utc).AddTicks(7239));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentImageURL",
                keyValue: null,
                column: "CommentImageURL",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CommentImageURL",
                table: "Comments",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Categories",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(317),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 8, 18, 8, 57, 36, 725, DateTimeKind.Utc).AddTicks(4916));

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
    }
}
