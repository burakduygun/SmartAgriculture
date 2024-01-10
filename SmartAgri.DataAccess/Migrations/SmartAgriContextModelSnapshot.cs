﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SmartAgri.DataAccess.Concrete.EntityFramework;

#nullable disable

namespace SmartAgri.DataAccess.Migrations
{
    [DbContext(typeof(SmartAgriContext))]
    partial class SmartAgriContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Advert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Adverts");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.AdvertStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AdvertStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Active"
                        },
                        new
                        {
                            Id = 2,
                            Name = "notProccess"
                        },
                        new
                        {
                            Id = 3,
                            Name = "complated"
                        });
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 9,
                            ImagePath = "wheat.png",
                            Name = "Buğday"
                        });
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Reply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TopicId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "content");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BuyAdvertId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("SellAdvertId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BuyAdvertId");

                    b.HasIndex("SellAdvertId")
                        .IsUnique();

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CoinAccountId")
                        .HasColumnType("uuid");

                    b.Property<string>("CoinAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExternalCoinAddress")
                        .HasColumnType("text");

                    b.Property<decimal>("LockedBalance")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CoinAccountId = new Guid("00000000-0000-0000-0000-000000000000"),
                            CoinAddress = "",
                            Email = "admin@admin.com",
                            LockedBalance = 0m,
                            Name = "admin",
                            Password = "admin123",
                            RoleId = 1,
                            Surname = "admin"
                        },
                        new
                        {
                            Id = 2,
                            CoinAccountId = new Guid("1fbef5d8-950a-47df-b03d-755b9cb90aae"),
                            CoinAddress = "RRZ6bNDpVSywsMZvNmwtD1AYFtnupWaEH4",
                            Email = "hms45267@gmail.com",
                            LockedBalance = 0m,
                            Name = "Habil Mevlut",
                            Password = "admin123",
                            RoleId = 2,
                            Surname = "Sayar"
                        });
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.AdvertBuy", b =>
                {
                    b.HasBaseType("SmartAgri.Entities.Concrete.Advert");

                    b.ToTable("AdvertBuys");

                    b.HasData(
                        new
                        {
                            Id = 6,
                            CreatedAt = new DateTime(2024, 1, 10, 17, 30, 51, 307, DateTimeKind.Local).AddTicks(5493),
                            ProductId = 9,
                            Quantity = 300,
                            StatusId = 1,
                            UnitPrice = 220m,
                            UserId = 2
                        },
                        new
                        {
                            Id = 7,
                            CreatedAt = new DateTime(2024, 1, 10, 17, 30, 51, 307, DateTimeKind.Local).AddTicks(5493),
                            ProductId = 9,
                            Quantity = 200,
                            StatusId = 1,
                            UnitPrice = 210m,
                            UserId = 2
                        },
                        new
                        {
                            Id = 8,
                            CreatedAt = new DateTime(2024, 1, 10, 17, 30, 51, 307, DateTimeKind.Local).AddTicks(5494),
                            ProductId = 9,
                            Quantity = 180,
                            StatusId = 1,
                            UnitPrice = 200m,
                            UserId = 2
                        },
                        new
                        {
                            Id = 9,
                            CreatedAt = new DateTime(2024, 1, 10, 17, 30, 51, 307, DateTimeKind.Local).AddTicks(5495),
                            ProductId = 9,
                            Quantity = 150,
                            StatusId = 1,
                            UnitPrice = 190m,
                            UserId = 2
                        },
                        new
                        {
                            Id = 10,
                            CreatedAt = new DateTime(2024, 1, 10, 17, 30, 51, 307, DateTimeKind.Local).AddTicks(5496),
                            ProductId = 9,
                            Quantity = 200,
                            StatusId = 1,
                            UnitPrice = 180m,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.AdvertSell", b =>
                {
                    b.HasBaseType("SmartAgri.Entities.Concrete.Advert");

                    b.ToTable("AdvertSells");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 1, 10, 17, 30, 51, 307, DateTimeKind.Local).AddTicks(5474),
                            ProductId = 9,
                            Quantity = 100,
                            StatusId = 1,
                            UnitPrice = 295m,
                            UserId = 2
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 1, 10, 17, 30, 51, 307, DateTimeKind.Local).AddTicks(5481),
                            ProductId = 9,
                            Quantity = 200,
                            StatusId = 1,
                            UnitPrice = 285m,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 1, 10, 17, 30, 51, 307, DateTimeKind.Local).AddTicks(5482),
                            ProductId = 9,
                            Quantity = 250,
                            StatusId = 1,
                            UnitPrice = 280m,
                            UserId = 2
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2024, 1, 10, 17, 30, 51, 307, DateTimeKind.Local).AddTicks(5482),
                            ProductId = 9,
                            Quantity = 125,
                            StatusId = 1,
                            UnitPrice = 240m,
                            UserId = 2
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2024, 1, 10, 17, 30, 51, 307, DateTimeKind.Local).AddTicks(5483),
                            ProductId = 9,
                            Quantity = 175,
                            StatusId = 1,
                            UnitPrice = 230m,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Advert", b =>
                {
                    b.HasOne("SmartAgri.Entities.Concrete.Product", "Product")
                        .WithMany("Adverts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartAgri.Entities.Concrete.AdvertStatus", "Status")
                        .WithMany("Adverts")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartAgri.Entities.Concrete.User", "User")
                        .WithMany("Adverts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Reply", b =>
                {
                    b.HasOne("SmartAgri.Entities.Concrete.Topic", "Topic")
                        .WithMany("Replies")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartAgri.Entities.Concrete.User", "User")
                        .WithMany("Replies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Topic", b =>
                {
                    b.HasOne("SmartAgri.Entities.Concrete.User", "User")
                        .WithMany("Topics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Transaction", b =>
                {
                    b.HasOne("SmartAgri.Entities.Concrete.AdvertBuy", "BuyAdvert")
                        .WithMany("Transactions")
                        .HasForeignKey("BuyAdvertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartAgri.Entities.Concrete.AdvertSell", "SellAdvert")
                        .WithOne("Transaction")
                        .HasForeignKey("SmartAgri.Entities.Concrete.Transaction", "SellAdvertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuyAdvert");

                    b.Navigation("SellAdvert");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.User", b =>
                {
                    b.HasOne("SmartAgri.Entities.Concrete.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.AdvertBuy", b =>
                {
                    b.HasOne("SmartAgri.Entities.Concrete.Advert", null)
                        .WithOne()
                        .HasForeignKey("SmartAgri.Entities.Concrete.AdvertBuy", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.AdvertSell", b =>
                {
                    b.HasOne("SmartAgri.Entities.Concrete.Advert", null)
                        .WithOne()
                        .HasForeignKey("SmartAgri.Entities.Concrete.AdvertSell", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.AdvertStatus", b =>
                {
                    b.Navigation("Adverts");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Product", b =>
                {
                    b.Navigation("Adverts");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.Topic", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.User", b =>
                {
                    b.Navigation("Adverts");

                    b.Navigation("Replies");

                    b.Navigation("Topics");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.AdvertBuy", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("SmartAgri.Entities.Concrete.AdvertSell", b =>
                {
                    b.Navigation("Transaction");
                });
#pragma warning restore 612, 618
        }
    }
}
