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

            modelBuilder.Entity("Dream.Data.Models.Developer", b =>
                {
                    b.Property<int>("DeveloperId")
                        .HasColumnType("int")
                        .HasColumnName("developer_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("last_name");

                    b.HasKey("DeveloperId")
                        .HasName("PK_Developer");

                    b.HasIndex(new[] { "Email" }, "UK_Email_Developer")
                        .IsUnique();

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("Dream.Data.Models.Download", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("GameId")
                        .HasColumnType("int")
                        .HasColumnName("game_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.HasKey("UserId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("Downloads");
                });

            modelBuilder.Entity("Dream.Data.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int")
                        .HasColumnName("game_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("genre_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(12, 2)")
                        .HasColumnName("price");

                    b.Property<double>("RequiredMemory")
                        .HasColumnType("float")
                        .HasColumnName("required_memory");

                    b.HasKey("GameId");

                    b.HasIndex("GenreId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Dream.Data.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("genre_id");

                    b.Property<int?>("AgeRequirements")
                        .HasColumnType("int")
                        .HasColumnName("age_requirements");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Dream.Data.Models.Like", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("GameId")
                        .HasColumnType("int")
                        .HasColumnName("game_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

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
                        .HasColumnType("int")
                        .HasColumnName("age");

                    b.Property<decimal?>("Balance")
                        .HasColumnType("decimal(12, 2)")
                        .HasColumnName("balance");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("username");

                    b.HasKey("UserId")
                        .HasName("PK_User");

                    b.HasIndex(new[] { "Email" }, "UK_Email_User")
                        .IsUnique();

                    b.HasIndex(new[] { "Username" }, "UK_Username_User")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GamesDeveloper", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int")
                        .HasColumnName("game_id");

                    b.Property<int>("DeveloperId")
                        .HasColumnType("int")
                        .HasColumnName("developer_id");

                    b.HasKey("GameId", "DeveloperId");

                    b.HasIndex("DeveloperId");

                    b.ToTable("Games_Developers", (string)null);
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
                        .IsRequired()
                        .HasConstraintName("FK_Games_Genres");

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

            modelBuilder.Entity("GamesDeveloper", b =>
                {
                    b.HasOne("Dream.Data.Models.Developer", null)
                        .WithMany()
                        .HasForeignKey("DeveloperId")
                        .IsRequired()
                        .HasConstraintName("FK_Games_Developers_Developers");

                    b.HasOne("Dream.Data.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GameId")
                        .IsRequired()
                        .HasConstraintName("FK_Games_Developers_Games");
                });

            modelBuilder.Entity("Dream.Data.Models.Game", b =>
                {
                    b.Navigation("Downloads");

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
