using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA.JustBlog.Core.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentTime",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 20, 21, 55, 28, 649, DateTimeKind.Local).AddTicks(3812),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 17, 23, 23, 22, 393, DateTimeKind.Local).AddTicks(1249));

            migrationBuilder.InsertData(
                table: "PostTagsMap",
                columns: new[] { "PostId", "TagId" },
                values: new object[,]
                {
                    { 4, 3 },
                    { 5, 1 },
                    { 5, 2 },
                    { 6, 2 },
                    { 6, 3 },
                    { 7, 2 },
                    { 8, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "PostedOn",
                value: new DateTime(2023, 4, 20, 21, 55, 28, 649, DateTimeKind.Local).AddTicks(6095));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5,
                column: "PostedOn",
                value: new DateTime(2023, 4, 20, 21, 55, 28, 649, DateTimeKind.Local).AddTicks(6099));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 6,
                column: "PostedOn",
                value: new DateTime(2023, 4, 20, 21, 55, 28, 649, DateTimeKind.Local).AddTicks(6102));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 7,
                column: "PostedOn",
                value: new DateTime(2023, 4, 20, 21, 55, 28, 649, DateTimeKind.Local).AddTicks(6103));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 8,
                column: "PostedOn",
                value: new DateTime(2023, 4, 20, 21, 55, 28, 649, DateTimeKind.Local).AddTicks(6104));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PostTagsMap",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "PostTagsMap",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "PostTagsMap",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "PostTagsMap",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "PostTagsMap",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "PostTagsMap",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "PostTagsMap",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentTime",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 17, 23, 23, 22, 393, DateTimeKind.Local).AddTicks(1249),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 20, 21, 55, 28, 649, DateTimeKind.Local).AddTicks(3812));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "PostedOn",
                value: new DateTime(2023, 4, 17, 23, 23, 22, 393, DateTimeKind.Local).AddTicks(3161));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5,
                column: "PostedOn",
                value: new DateTime(2023, 4, 17, 23, 23, 22, 393, DateTimeKind.Local).AddTicks(3163));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 6,
                column: "PostedOn",
                value: new DateTime(2023, 4, 17, 23, 23, 22, 393, DateTimeKind.Local).AddTicks(3165));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 7,
                column: "PostedOn",
                value: new DateTime(2023, 4, 17, 23, 23, 22, 393, DateTimeKind.Local).AddTicks(3166));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 8,
                column: "PostedOn",
                value: new DateTime(2023, 4, 17, 23, 23, 22, 393, DateTimeKind.Local).AddTicks(3167));
        }
    }
}
