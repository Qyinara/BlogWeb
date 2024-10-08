﻿// <auto-generated />
using System;
using Blog.Entities.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Blog.Entities.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240818085357_MakeCommentImageURLNullable")]
    partial class MakeCommentImageURLNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Blog.Entities.Models.Concrete.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ActivityDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ActivityType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(317));

                    b.HasKey("Id");

                    b.HasIndex("CategoryName")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Yazılım",
                            CreateDate = new DateTime(2024, 8, 18, 11, 53, 57, 54, DateTimeKind.Local).AddTicks(1084)
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Donanım",
                            CreateDate = new DateTime(2024, 8, 18, 11, 53, 57, 54, DateTimeKind.Local).AddTicks(1095)
                        });
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("CommentImageURL")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.CommentLike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("UserId");

                    b.ToTable("CommentLikes");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(2772));

                    b.Property<string>("PostImageURL")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.PostLike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostLikes");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 8, 18, 8, 53, 57, 54, DateTimeKind.Utc).AddTicks(8964));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("RoleName")
                        .IsUnique();

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(2024, 8, 18, 11, 53, 57, 54, DateTimeKind.Local).AddTicks(9643),
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            CreateDate = new DateTime(2024, 8, 18, 11, 53, 57, 54, DateTimeKind.Local).AddTicks(9647),
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 8, 18, 8, 53, 57, 55, DateTimeKind.Utc).AddTicks(981));

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProfilePhotoUrl")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Mail")
                        .IsUnique();

                    b.HasIndex("Password")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(2024, 8, 18, 11, 53, 57, 55, DateTimeKind.Local).AddTicks(2189),
                            LastName = "Aydemir",
                            Mail = "erolaydemir27@gmail.com",
                            Name = "Erol",
                            Password = "admin",
                            ProfilePhotoUrl = " ",
                            Role = "Admin",
                            RoleId = 1,
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.Activity", b =>
                {
                    b.HasOne("Blog.Entities.Models.Concrete.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.Comment", b =>
                {
                    b.HasOne("Blog.Entities.Models.Concrete.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blog.Entities.Models.Concrete.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.CommentLike", b =>
                {
                    b.HasOne("Blog.Entities.Models.Concrete.Comment", "Comment")
                        .WithMany("CommentLikes")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blog.Entities.Models.Concrete.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.Post", b =>
                {
                    b.HasOne("Blog.Entities.Models.Concrete.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blog.Entities.Models.Concrete.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.PostLike", b =>
                {
                    b.HasOne("Blog.Entities.Models.Concrete.Post", "Post")
                        .WithMany("PostLikes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blog.Entities.Models.Concrete.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.User", b =>
                {
                    b.HasOne("Blog.Entities.Models.Concrete.Role", "Rolee")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rolee");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.Category", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.Comment", b =>
                {
                    b.Navigation("CommentLikes");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PostLikes");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Blog.Entities.Models.Concrete.User", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
