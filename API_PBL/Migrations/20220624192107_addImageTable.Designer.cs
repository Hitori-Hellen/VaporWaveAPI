﻿// <auto-generated />
using System;
using API_PBL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_PBL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220624192107_addImageTable")]
    partial class addImageTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("passwordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("passwordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AgeRating")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Developer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("GameRating")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Spec")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isLiked")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("gameId")
                        .HasColumnType("int");

                    b.Property<string>("imageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("gameId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.Library", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("gameName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("Library");
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.Receipt", b =>
                {
                    b.Property<int>("receiptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("receiptId"), 1L, 1);

                    b.Property<int>("gameId")
                        .HasColumnType("int");

                    b.Property<double>("gamePrice")
                        .HasColumnType("float");

                    b.Property<DateTime>("purchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("receiptId");

                    b.HasIndex("gameId");

                    b.HasIndex("userId");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("tagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.User", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("dateOfBirth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("userWallet")
                        .HasColumnType("float");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.WishList", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("gameName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("WishLists");
                });

            modelBuilder.Entity("GameTag", b =>
                {
                    b.Property<int>("GamesId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("GamesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("GameTag");
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.Account", b =>
                {
                    b.HasOne("API_PBL.Models.DatabaseModels.User", "User")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.Image", b =>
                {
                    b.HasOne("API_PBL.Models.DatabaseModels.Game", "Game")
                        .WithMany("Images")
                        .HasForeignKey("gameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.Library", b =>
                {
                    b.HasOne("API_PBL.Models.DatabaseModels.User", null)
                        .WithOne("Library")
                        .HasForeignKey("API_PBL.Models.DatabaseModels.Library", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.Receipt", b =>
                {
                    b.HasOne("API_PBL.Models.DatabaseModels.Game", "Game")
                        .WithMany()
                        .HasForeignKey("gameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_PBL.Models.DatabaseModels.User", "User")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.WishList", b =>
                {
                    b.HasOne("API_PBL.Models.DatabaseModels.User", null)
                        .WithOne("WishList")
                        .HasForeignKey("API_PBL.Models.DatabaseModels.WishList", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameTag", b =>
                {
                    b.HasOne("API_PBL.Models.DatabaseModels.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_PBL.Models.DatabaseModels.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.Game", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("API_PBL.Models.DatabaseModels.User", b =>
                {
                    b.Navigation("Library")
                        .IsRequired();

                    b.Navigation("WishList")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
