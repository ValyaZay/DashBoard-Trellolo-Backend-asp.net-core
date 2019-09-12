﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrelloProject.DAL.EF;

namespace TrelloProject.DAL.Migrations
{
    [DbContext(typeof(TrelloDbContext))]
    [Migration("20190827110754_UserBoardIdRemoved")]
    partial class UserBoardIdRemoved
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrelloProject.DAL.Entities.BackgroundColor", b =>
                {
                    b.Property<int>("BackgroundColorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ColorHex");

                    b.HasKey("BackgroundColorId");

                    b.ToTable("BackgroundColors");

                    b.HasData(
                        new
                        {
                            BackgroundColorId = 1,
                            ColorHex = "#C0C0C0"
                        },
                        new
                        {
                            BackgroundColorId = 2,
                            ColorHex = "#ffff00"
                        },
                        new
                        {
                            BackgroundColorId = 3,
                            ColorHex = "#FFA500"
                        },
                        new
                        {
                            BackgroundColorId = 4,
                            ColorHex = "#0000FF"
                        },
                        new
                        {
                            BackgroundColorId = 5,
                            ColorHex = "#008000"
                        });
                });

            modelBuilder.Entity("TrelloProject.DAL.Entities.Board", b =>
                {
                    b.Property<int>("BoardId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentBackgroundColorId");

                    b.Property<string>("Title");

                    b.HasKey("BoardId");

                    b.HasIndex("CurrentBackgroundColorId");

                    b.ToTable("Boards");

                    b.HasData(
                        new
                        {
                            BoardId = 1,
                            CurrentBackgroundColorId = 1,
                            Title = "ManagerBoard"
                        });
                });

            modelBuilder.Entity("TrelloProject.DAL.Entities.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssigneeId");

                    b.Property<int>("CardListId");

                    b.Property<int>("CreatedById");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<bool>("Hidden");

                    b.Property<string>("Title");

                    b.HasKey("CardId");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("CardListId");

                    b.HasIndex("CreatedById");

                    b.ToTable("Cards");

                    b.HasData(
                        new
                        {
                            CardId = 1,
                            AssigneeId = 2,
                            CardListId = 1,
                            CreatedById = 1,
                            CreatedDate = new DateTime(2019, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Just create a new task",
                            Hidden = false,
                            Title = "Create a task"
                        },
                        new
                        {
                            CardId = 2,
                            AssigneeId = 3,
                            CardListId = 1,
                            CreatedById = 1,
                            CreatedDate = new DateTime(2019, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Implement INewInterface now",
                            Hidden = false,
                            Title = "Implement an Interface"
                        });
                });

            modelBuilder.Entity("TrelloProject.DAL.Entities.CardComment", b =>
                {
                    b.Property<int>("CardCommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CardId");

                    b.Property<int>("CreatedById");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("RefersToId");

                    b.Property<string>("Text");

                    b.HasKey("CardCommentId");

                    b.HasIndex("CardId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("RefersToId");

                    b.ToTable("CardComments");

                    b.HasData(
                        new
                        {
                            CardCommentId = 1,
                            CardId = 1,
                            CreatedById = 2,
                            CreatedDate = new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Text = "Good comment"
                        },
                        new
                        {
                            CardCommentId = 2,
                            CardId = 1,
                            CreatedById = 2,
                            CreatedDate = new DateTime(2019, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RefersToId = 1,
                            Text = "Bad comment"
                        });
                });

            modelBuilder.Entity("TrelloProject.DAL.Entities.CardList", b =>
                {
                    b.Property<int>("CardListId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BoardId");

                    b.Property<int>("Order");

                    b.Property<string>("Title");

                    b.HasKey("CardListId");

                    b.HasIndex("BoardId");

                    b.ToTable("CardLists");

                    b.HasData(
                        new
                        {
                            CardListId = 1,
                            BoardId = 1,
                            Order = 0,
                            Title = "ToDo"
                        },
                        new
                        {
                            CardListId = 2,
                            BoardId = 1,
                            Order = 1,
                            Title = "Done"
                        },
                        new
                        {
                            CardListId = 3,
                            BoardId = 1,
                            Order = 2,
                            Title = "DoNOTDo"
                        });
                });

            modelBuilder.Entity("TrelloProject.DAL.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "valya@valya.net",
                            FirstName = "Valya",
                            LastName = "Zay"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "vova@vova.com",
                            FirstName = "Vova",
                            LastName = "Petrov"
                        },
                        new
                        {
                            UserId = 3,
                            Email = "gora@gora.net",
                            FirstName = "Gora",
                            LastName = "Sidorov"
                        });
                });

            modelBuilder.Entity("TrelloProject.DAL.Entities.UserBoard", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("BoardId");

                    b.HasKey("UserId", "BoardId");

                    b.HasIndex("BoardId");

                    b.ToTable("UserBoards");

                    b.HasData(
                        new
                        {
                            UserId = 2,
                            BoardId = 1
                        });
                });

            modelBuilder.Entity("TrelloProject.DAL.Entities.Board", b =>
                {
                    b.HasOne("TrelloProject.DAL.Entities.BackgroundColor", "BackgroundColor")
                        .WithMany("Boards")
                        .HasForeignKey("CurrentBackgroundColorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrelloProject.DAL.Entities.Card", b =>
                {
                    b.HasOne("TrelloProject.DAL.Entities.User", "Assignee")
                        .WithMany("CardsAssigned")
                        .HasForeignKey("AssigneeId");

                    b.HasOne("TrelloProject.DAL.Entities.CardList", "CardList")
                        .WithMany("Cards")
                        .HasForeignKey("CardListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TrelloProject.DAL.Entities.User", "CreatedBy")
                        .WithMany("CardsCreated")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrelloProject.DAL.Entities.CardComment", b =>
                {
                    b.HasOne("TrelloProject.DAL.Entities.Card", "Card")
                        .WithMany("CardComments")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TrelloProject.DAL.Entities.User", "CreatedBy")
                        .WithMany("CardComments")
                        .HasForeignKey("CreatedById");

                    b.HasOne("TrelloProject.DAL.Entities.CardComment", "RefersTo")
                        .WithMany()
                        .HasForeignKey("RefersToId");
                });

            modelBuilder.Entity("TrelloProject.DAL.Entities.CardList", b =>
                {
                    b.HasOne("TrelloProject.DAL.Entities.Board", "Board")
                        .WithMany("CardLists")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrelloProject.DAL.Entities.UserBoard", b =>
                {
                    b.HasOne("TrelloProject.DAL.Entities.Board", "Board")
                        .WithMany("UserBoards")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TrelloProject.DAL.Entities.User", "User")
                        .WithMany("UserBoards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
