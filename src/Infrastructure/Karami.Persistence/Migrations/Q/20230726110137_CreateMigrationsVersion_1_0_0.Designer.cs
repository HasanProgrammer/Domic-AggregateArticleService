﻿// <auto-generated />
using System;
using Karami.Persistence.Contexts.Q;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Karami.Persistence.Migrations.Q
{
    [DbContext(typeof(SQLContext))]
    [Migration("20230726110137_CreateMigrationsVersion_1_0_0")]
    partial class CreateMigrationsVersion_1_0_0
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Karami.Domain.Article.Entities.ArticleQuery", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt_EnglishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedAt_PersianDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt_EnglishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedAt_PersianDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Articles", (string)null);
                });

            modelBuilder.Entity("Karami.Domain.ArticleComment.Entities.ArticleCommentQuery", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArticleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt_EnglishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedAt_PersianDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt_EnglishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedAt_PersianDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("OwnerId");

                    b.ToTable("ArticleComments", (string)null);
                });

            modelBuilder.Entity("Karami.Domain.ArticleCommentAnswer.Entities.ArticleCommentAnswerQuery", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt_EnglishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedAt_PersianDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt_EnglishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedAt_PersianDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("OwnerId");

                    b.ToTable("ArticleCommentAnswers", (string)null);
                });

            modelBuilder.Entity("Karami.Domain.Category.Entities.CategoryQuery", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Karami.Domain.File.Entities.FileQuery", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArticleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt_EnglishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedAt_PersianDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt_EnglishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedAt_PersianDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Files", (string)null);
                });

            modelBuilder.Entity("Karami.Domain.User.Entities.UserQuery", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Karami.Domain.Article.Entities.ArticleQuery", b =>
                {
                    b.HasOne("Karami.Domain.Category.Entities.CategoryQuery", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Karami.Domain.User.Entities.UserQuery", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Karami.Domain.ArticleComment.Entities.ArticleCommentQuery", b =>
                {
                    b.HasOne("Karami.Domain.Article.Entities.ArticleQuery", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId");

                    b.HasOne("Karami.Domain.User.Entities.UserQuery", "User")
                        .WithMany("Comments")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Karami.Domain.ArticleCommentAnswer.Entities.ArticleCommentAnswerQuery", b =>
                {
                    b.HasOne("Karami.Domain.ArticleComment.Entities.ArticleCommentQuery", "Comment")
                        .WithMany("Answers")
                        .HasForeignKey("CommentId");

                    b.HasOne("Karami.Domain.User.Entities.UserQuery", "User")
                        .WithMany("Answers")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Karami.Domain.File.Entities.FileQuery", b =>
                {
                    b.HasOne("Karami.Domain.Article.Entities.ArticleQuery", "Article")
                        .WithMany("Files")
                        .HasForeignKey("ArticleId");

                    b.Navigation("Article");
                });

            modelBuilder.Entity("Karami.Domain.Article.Entities.ArticleQuery", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Files");
                });

            modelBuilder.Entity("Karami.Domain.ArticleComment.Entities.ArticleCommentQuery", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Karami.Domain.Category.Entities.CategoryQuery", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Karami.Domain.User.Entities.UserQuery", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Articles");

                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
