﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    [DbContext(typeof(NgContext))]
    [Migration("20200727090128_TourSeed")]
    partial class TourSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Audio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("NodeId")
                        .HasColumnType("uuid");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NodeId");

                    b.ToTable("Audio");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Commerce", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Commerce");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a4506bf8-9cca-4413-b0d4-4247c61b1231"),
                            LocationId = new Guid("0013a98e-32f6-494d-b055-c9fb4dafc3e8"),
                            Name = "Test Commerce",
                            UserId = new Guid("73b7b257-41f7-4b22-9a10-93fb91238fd9")
                        });
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.CommerceDeal", b =>
                {
                    b.Property<Guid>("CommerceId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DealId")
                        .HasColumnType("uuid");

                    b.HasKey("CommerceId", "DealId");

                    b.HasIndex("DealId");

                    b.ToTable("CommerceDeal");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Coupon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("GenerationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("NodeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ValidationDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("NodeId");

                    b.HasIndex("UserId");

                    b.ToTable("Coupon");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Deal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Deal");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("NodeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("NodeId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Latitude", "Longitude")
                        .IsUnique();

                    b.ToTable("Location");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0013a98e-32f6-494d-b055-c9fb4dafc3e8"),
                            Latitude = 33.842185m,
                            Longitude = -40.707753m,
                            Name = "Test Location"
                        });
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Node", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DealId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<Guid>("TourId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DealId");

                    b.HasIndex("LocationId");

                    b.HasIndex("TourId");

                    b.ToTable("Node");

                    b.HasData(
                        new
                        {
                            Id = new Guid("080942cb-aad4-4e94-9385-f53e6c72113c"),
                            LocationId = new Guid("0013a98e-32f6-494d-b055-c9fb4dafc3e8"),
                            Name = "Test Node",
                            Order = 1,
                            TourId = new Guid("e6cfc804-a418-4683-81be-ca9c753b698a")
                        });
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Restaurant", b =>
                {
                    b.Property<Guid>("CommerceId")
                        .HasColumnType("uuid");

                    b.HasKey("CommerceId");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Review", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TourId")
                        .HasColumnType("uuid");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "TourId");

                    b.HasIndex("TourId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Tour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("GeoJson")
                        .HasColumnType("text");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsFeatured")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPremium")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Tour");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e6cfc804-a418-4683-81be-ca9c753b698a"),
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Duration = 0,
                            IsFeatured = false,
                            IsPremium = false,
                            Name = "Test Tour"
                        });
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.TourTag", b =>
                {
                    b.Property<Guid>("TourId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uuid");

                    b.HasKey("TourId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("TourTag");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ImageId");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b0f2451e-5820-4eca-a797-46a01693a3b2"),
                            Birthdate = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "basic@test.org",
                            Name = "Basic",
                            Password = "10000.+2PnZrnAWQRgqlMx+l8kyA==.ALiUC3pHYJJ7cr8Xqnn1y16XROosvjHNTDmf+Em+pMM=",
                            PhoneNumber = "+222222222",
                            Role = 2,
                            Surname = "QA User"
                        },
                        new
                        {
                            Id = new Guid("0ac2c4c5-ebff-445e-85d4-1db76d65ce0a"),
                            Birthdate = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@test.org",
                            Name = "Admin",
                            Password = "10000.r1m2AhgohtRKaAYihSdiFQ==.9jOF0O4zo3WoBYq+H1f3XTPG9An8LZfEJd1uwB66N0s=",
                            PhoneNumber = "+000000000",
                            Role = 0,
                            Surname = "QA User"
                        },
                        new
                        {
                            Id = new Guid("440edb6b-342e-4d5f-a233-62aef964cbfa"),
                            Birthdate = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "commerce@test.org",
                            Name = "Commerce",
                            Password = "10000.NcEE328o58z2KLy1cIiKMA==.5+Mwrqw7XVP2dE+RtcMorXI/Ri6daF4nCRZB4+xJUAY=",
                            PhoneNumber = "+111111111",
                            Role = 1,
                            Surname = "QA User"
                        },
                        new
                        {
                            Id = new Guid("73b7b257-41f7-4b22-9a10-93fb91238fd9"),
                            Birthdate = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "fullcommerce@test.org",
                            Name = "FullCommerce",
                            Password = "10000./LphyV3IUSMjgcllhGg/HA==.ZeBKs4MVq3+BKEQw9ejzr/HbAwI7/KOGr10FqkuGSmE=",
                            PhoneNumber = "+0111111111",
                            Role = 1,
                            Surname = "QA User"
                        });
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Audio", b =>
                {
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Node", null)
                        .WithMany("Audios")
                        .HasForeignKey("NodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Commerce", b =>
                {
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.User", "User")
                        .WithOne("Commerce")
                        .HasForeignKey("NG.DBManager.Infrastructure.Contracts.Models.Commerce", "UserId");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.CommerceDeal", b =>
                {
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Commerce", "Commerce")
                        .WithMany("CommerceDeals")
                        .HasForeignKey("CommerceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Deal", "Deal")
                        .WithMany()
                        .HasForeignKey("DealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Coupon", b =>
                {
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Node", "Node")
                        .WithMany()
                        .HasForeignKey("NodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Image", b =>
                {
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Node", null)
                        .WithMany("Images")
                        .HasForeignKey("NodeId");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Node", b =>
                {
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Deal", "Deal")
                        .WithMany()
                        .HasForeignKey("DealId");

                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Location", "Location")
                        .WithMany("Nodes")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Tour", null)
                        .WithMany("Nodes")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Restaurant", b =>
                {
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Commerce", "Commerce")
                        .WithMany()
                        .HasForeignKey("CommerceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Review", b =>
                {
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Tour", "Tour")
                        .WithMany()
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Tour", b =>
                {
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.TourTag", b =>
                {
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Tour", "Tour")
                        .WithMany("TourTags")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.User", b =>
                {
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");
                });
#pragma warning restore 612, 618
        }
    }
}