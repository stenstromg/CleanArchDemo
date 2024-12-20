﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.PresentationAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDemoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACCOUNTS",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nickname = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    account_type = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    updated_date = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNTS", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PEOPLE",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    updated_date = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEOPLE", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TRANSACTIONS",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    amount = table.Column<decimal>(type: "decimal(24,4)", nullable: false),
                    timestamp = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    reference_no = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    transaction_type = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    updated_date = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSACTIONS", x => x.id);
                    table.ForeignKey(
                        name: "FK_TRANSACTIONS_ACCOUNTS_AccountID",
                        column: x => x.AccountID,
                        principalTable: "ACCOUNTS",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CONTACTS",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(type: "bigint", nullable: true),
                    UserProfileID = table.Column<long>(type: "bigint", nullable: true),
                    label = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    primary_email_id = table.Column<long>(type: "bigint", nullable: true),
                    primary_phone_id = table.Column<long>(type: "bigint", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    updated_date = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTACTS", x => x.id);
                    table.ForeignKey(
                        name: "FK_CONTACTS_PEOPLE_PersonID",
                        column: x => x.PersonID,
                        principalTable: "PEOPLE",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "EMAILS",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email_domain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email_username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    updated_date = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMAILS", x => x.id);
                    table.ForeignKey(
                        name: "FK_EMAILS_CONTACTS_contact_id",
                        column: x => x.contact_id,
                        principalTable: "CONTACTS",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "PHONE_NUMBERS",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    area_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    line_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    label = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    contact_id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    updated_date = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHONE_NUMBERS", x => x.id);
                    table.ForeignKey(
                        name: "FK_PHONE_NUMBERS_CONTACTS_contact_id",
                        column: x => x.contact_id,
                        principalTable: "CONTACTS",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "USER_LOGIN",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailID = table.Column<long>(type: "bigint", nullable: true),
                    failed_login_count = table.Column<int>(type: "int", nullable: false),
                    enabled = table.Column<bool>(type: "bit", nullable: false),
                    locked = table.Column<bool>(type: "bit", nullable: false),
                    password = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    last_login_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    password_must_be_changed = table.Column<bool>(type: "bit", nullable: false),
                    username = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PersonID = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    updated_date = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_LOGIN", x => x.id);
                    table.ForeignKey(
                        name: "FK_USER_LOGIN_EMAILS_EmailID",
                        column: x => x.EmailID,
                        principalTable: "EMAILS",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_USER_LOGIN_PEOPLE_PersonID",
                        column: x => x.PersonID,
                        principalTable: "PEOPLE",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONTACTS_PersonID",
                table: "CONTACTS",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_CONTACTS_UserProfileID",
                table: "CONTACTS",
                column: "UserProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_EMAILS_contact_id",
                table: "EMAILS",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_PHONE_NUMBERS_contact_id",
                table: "PHONE_NUMBERS",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTIONS_AccountID",
                table: "TRANSACTIONS",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_LOGIN_EmailID",
                table: "USER_LOGIN",
                column: "EmailID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_LOGIN_PersonID",
                table: "USER_LOGIN",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_CONTACTS_USER_LOGIN_UserProfileID",
                table: "CONTACTS",
                column: "UserProfileID",
                principalTable: "USER_LOGIN",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CONTACTS_PEOPLE_PersonID",
                table: "CONTACTS");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_LOGIN_PEOPLE_PersonID",
                table: "USER_LOGIN");

            migrationBuilder.DropForeignKey(
                name: "FK_CONTACTS_USER_LOGIN_UserProfileID",
                table: "CONTACTS");

            migrationBuilder.DropTable(
                name: "PHONE_NUMBERS");

            migrationBuilder.DropTable(
                name: "TRANSACTIONS");

            migrationBuilder.DropTable(
                name: "ACCOUNTS");

            migrationBuilder.DropTable(
                name: "PEOPLE");

            migrationBuilder.DropTable(
                name: "USER_LOGIN");

            migrationBuilder.DropTable(
                name: "EMAILS");

            migrationBuilder.DropTable(
                name: "CONTACTS");
        }
    }
}
