using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace billing_backend.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "UserMaster",
                newName: "Password");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "UserMaster",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserMaster",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "UserMaster",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserMaster");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserMaster");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserMaster");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserMaster",
                newName: "PasswordHash");
        }
    }
}
