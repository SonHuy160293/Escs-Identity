﻿// <auto-generated />
using System;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace Identity.Infrastructure.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    partial class IdentityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Domain.Entities.OutboxMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("QueueName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages");
                });

            modelBuilder.Entity("Identity.Domain.Model.EmailServiceConfig", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<long>("ServiceId")
                        .HasColumnType("NUMBER(19)");

                    b.Property<string>("SmtpEmail")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("SmtpPassword")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("SmtpPort")
                        .HasColumnType("NUMBER(10)");

                    b.Property<long>("UserId")
                        .HasColumnType("NUMBER(19)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("EmailServiceConfigs");
                });

            modelBuilder.Entity("Identity.Domain.Model.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Identity.Domain.Model.Service", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Identity.Domain.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int?>("AccessFailedCount")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool?>("LockoutEnabled")
                        .HasColumnType("NUMBER(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<long>("RoleId")
                        .HasColumnType("NUMBER(19)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Identity.Domain.Model.UserToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<long>("ServiceId")
                        .HasColumnType("NUMBER(19)");

                    b.Property<long>("UserId")
                        .HasColumnType("NUMBER(19)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Identity.Domain.Model.EmailServiceConfig", b =>
                {
                    b.HasOne("Identity.Domain.Model.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Identity.Domain.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Identity.Domain.Model.User", b =>
                {
                    b.HasOne("Identity.Domain.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Identity.Domain.Model.UserToken", b =>
                {
                    b.HasOne("Identity.Domain.Model.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Identity.Domain.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
