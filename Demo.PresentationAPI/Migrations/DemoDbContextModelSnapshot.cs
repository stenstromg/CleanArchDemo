﻿// <auto-generated />
using System;
using Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Demo.PresentationAPI.Migrations
{
    [DbContext(typeof(DemoDbContext))]
    partial class DemoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Demo.Domain.Models.Account", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("account_number");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("created_date");

                    b.Property<string>("Nickname")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasColumnName("nickname");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnName("account_type");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("updated_date");

                    b.HasKey("ID");

                    b.ToTable("ACCOUNTS");
                });

            modelBuilder.Entity("Demo.Domain.Models.Contact", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("created_date");

                    b.Property<string>("Label")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasColumnName("label");

                    b.Property<long?>("PersonID")
                        .HasColumnType("bigint");

                    b.Property<long?>("PrimaryEmailID")
                        .HasColumnType("bigint");

                    b.Property<long?>("PrimaryPhoneID")
                        .HasColumnType("bigint");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("updated_date");

                    b.Property<long?>("UserProfileID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.HasIndex("PrimaryEmailID");

                    b.HasIndex("PrimaryPhoneID");

                    b.HasIndex("UserProfileID");

                    b.ToTable("CONTACTS");
                });

            modelBuilder.Entity("Demo.Domain.Models.Email", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<long>("ContactID")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("created_date");

                    b.Property<string>("Domain")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailUsername")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("label");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("updated_date");

                    b.HasKey("ID");

                    b.HasIndex("ContactID");

                    b.ToTable("EMAILS");
                });

            modelBuilder.Entity("Demo.Domain.Models.Person", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2")
                        .HasColumnName("dob");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("first_name");

                    b.Property<string>("Gender")
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasColumnName("last_name");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("updated_date");

                    b.HasKey("ID");

                    b.ToTable("PEOPLE");
                });

            modelBuilder.Entity("Demo.Domain.Models.PhoneNumber", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("AreaCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("area_code");

                    b.Property<long>("ContactID")
                        .HasColumnType("bigint");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("country_code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("created_date");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("extension");

                    b.Property<string>("Label")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasColumnName("label");

                    b.Property<string>("LineNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line_number");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("prefix");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("updated_date");

                    b.HasKey("ID");

                    b.HasIndex("ContactID");

                    b.ToTable("PHONE_NUMBERS");
                });

            modelBuilder.Entity("Demo.Domain.Models.Transaction", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<long?>("AccountID")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(24,4)")
                        .HasColumnName("amount");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("Date")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("timestamp");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("description");

                    b.Property<string>("ReferenceNumber")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("reference_no");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int")
                        .HasColumnName("transaction_type");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("updated_date");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("TRANSACTIONS");
                });

            modelBuilder.Entity("Demo.Domain.Models.UserLogin", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("created_date");

                    b.Property<long>("EmailID")
                        .HasColumnType("bigint");

                    b.Property<int>("FailedLoginCount")
                        .HasColumnType("int")
                        .HasColumnName("failed_login_count");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("enabled");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit")
                        .HasColumnName("locked");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_login_date");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("password");

                    b.Property<bool>("PasswordMustBeChanged")
                        .HasColumnType("bit")
                        .HasColumnName("password_must_be_changed");

                    b.Property<long?>("PersonID")
                        .HasColumnType("bigint");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("updated_date");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("username");

                    b.HasKey("ID");

                    b.HasIndex("EmailID");

                    b.HasIndex("PersonID");

                    b.ToTable("USER_LOGIN");
                });

            modelBuilder.Entity("Demo.Domain.Models.Contact", b =>
                {
                    b.HasOne("Demo.Domain.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID");

                    b.HasOne("Demo.Domain.Models.Email", "PrimaryEmail")
                        .WithMany()
                        .HasForeignKey("PrimaryEmailID");

                    b.HasOne("Demo.Domain.Models.PhoneNumber", "PrimaryPhone")
                        .WithMany()
                        .HasForeignKey("PrimaryPhoneID");

                    b.HasOne("Demo.Domain.Models.UserLogin", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileID");

                    b.Navigation("Person");

                    b.Navigation("PrimaryEmail");

                    b.Navigation("PrimaryPhone");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Demo.Domain.Models.Email", b =>
                {
                    b.HasOne("Demo.Domain.Models.Contact", "Contact")
                        .WithMany("Emails")
                        .HasForeignKey("ContactID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Demo.Domain.Models.PhoneNumber", b =>
                {
                    b.HasOne("Demo.Domain.Models.Contact", "Contact")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("ContactID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Demo.Domain.Models.Transaction", b =>
                {
                    b.HasOne("Demo.Domain.Models.Account", null)
                        .WithMany("Transactions")
                        .HasForeignKey("AccountID");
                });

            modelBuilder.Entity("Demo.Domain.Models.UserLogin", b =>
                {
                    b.HasOne("Demo.Domain.Models.Email", "Email")
                        .WithMany()
                        .HasForeignKey("EmailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Demo.Domain.Models.Person", "Person")
                        .WithMany("UserLogins")
                        .HasForeignKey("PersonID");

                    b.Navigation("Email");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Demo.Domain.Models.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Demo.Domain.Models.Contact", b =>
                {
                    b.Navigation("Emails");

                    b.Navigation("PhoneNumbers");
                });

            modelBuilder.Entity("Demo.Domain.Models.Person", b =>
                {
                    b.Navigation("UserLogins");
                });
#pragma warning restore 612, 618
        }
    }
}
