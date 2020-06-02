﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NG.DBManager.Infrastructure.Contracts.Contexts;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    [DbContext(typeof(NgContext))]
    [Migration("20200602144547_PhoneNumberOptional")]
    partial class PhoneNumberOptional
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Audio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("NodeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NodeId");

                    b.ToTable("Audio");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Commerce", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Commerce");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Coupon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CommerceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("GenerationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ValidationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CommerceId");

                    b.HasIndex("UserId");

                    b.ToTable("Coupon");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("NodeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NodeId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Latitude", "Longitude")
                        .IsUnique();

                    b.ToTable("Location");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Node", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoordinatesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid>("TourId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("TourId");

                    b.ToTable("Node");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Restaurant", b =>
                {
                    b.Property<Guid>("CommerceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CommerceId");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Review", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TourId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("UserId", "TourId");

                    b.HasIndex("TourId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Tour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsFeatured")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPremium")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Tour");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.TourTag", b =>
                {
                    b.Property<Guid>("TourId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TourId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("TourTag");
                });

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(254)");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ImageId");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.ToTable("User");
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

            modelBuilder.Entity("NG.DBManager.Infrastructure.Contracts.Models.Coupon", b =>
                {
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Commerce", "Commerce")
                        .WithMany()
                        .HasForeignKey("CommerceId")
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
                    b.HasOne("NG.DBManager.Infrastructure.Contracts.Models.Location", "Location")
                        .WithMany("Nodes")
                        .HasForeignKey("LocationId");

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
