﻿// <auto-generated />
using System;
using Dream.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(DreamContext))]
    partial class DreamContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data.Models.GameDeveloper", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("DeveloperId")
                        .HasColumnType("int");

                    b.HasKey("GameId", "DeveloperId");

                    b.HasIndex("DeveloperId");

                    b.ToTable("GamesDevelopers");
                });

            modelBuilder.Entity("Dream.Data.Models.Developer", b =>
                {
                    b.Property<int>("DeveloperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeveloperId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DeveloperId")
                        .HasName("PK_Developer");

                    b.HasIndex(new[] { "Email" }, "UK_Email_Developer")
                        .IsUnique();

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("Dream.Data.Models.Download", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("Downloads");
                });

            modelBuilder.Entity("Dream.Data.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<double>("RequiredMemory")
                        .HasPrecision(12, 2)
                        .HasColumnType("float(12)");

                    b.HasKey("GameId")
                        .HasName("PK_Game");

                    b.HasIndex("GenreId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Dream.Data.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<int?>("AgeRequirements")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("GenreId")
                        .HasName("PK_Genre");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Dream.Data.Models.Like", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Dream.Data.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<decimal?>("Balance")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId")
                        .HasName("PK_User");

                    b.HasIndex(new[] { "Email" }, "UK_Email_User")
                        .IsUnique();

                    b.HasIndex(new[] { "Username" }, "UK_Username_User")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.Models.GameDeveloper", b =>
                {
                    b.HasOne("Dream.Data.Models.Developer", "Developer")
                        .WithMany("GameDevelopers")
                        .HasForeignKey("DeveloperId")
                        .IsRequired()
                        .HasConstraintName("FK_GamesDevelopers_Developers");

                    b.HasOne("Dream.Data.Models.Game", "Game")
                        .WithMany("GameDevelopers")
                        .HasForeignKey("GameId")
                        .IsRequired()
                        .HasConstraintName("FK_GamesDevelopers_Games");

                    b.Navigation("Developer");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Dream.Data.Models.Download", b =>
                {
                    b.HasOne("Dream.Data.Models.Game", "Game")
                        .WithMany("Downloads")
                        .HasForeignKey("GameId")
                        .IsRequired()
                        .HasConstraintName("FK_Downloads_Games");

                    b.HasOne("Dream.Data.Models.User", "User")
                        .WithMany("Downloads")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Downloads_Users");

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dream.Data.Models.Game", b =>
                {
                    b.HasOne("Dream.Data.Models.Genre", "Genre")
                        .WithMany("Games")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Dream.Data.Models.Like", b =>
                {
                    b.HasOne("Dream.Data.Models.Game", "Game")
                        .WithMany("Likes")
                        .HasForeignKey("GameId")
                        .IsRequired()
                        .HasConstraintName("FK_Likes_Games");

                    b.HasOne("Dream.Data.Models.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Likes_Users");

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dream.Data.Models.Developer", b =>
                {
                    b.Navigation("GameDevelopers");
                });

            modelBuilder.Entity("Dream.Data.Models.Game", b =>
                {
                    b.Navigation("Downloads");

                    b.Navigation("GameDevelopers");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("Dream.Data.Models.Genre", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("Dream.Data.Models.User", b =>
                {
                    b.Navigation("Downloads");

                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
