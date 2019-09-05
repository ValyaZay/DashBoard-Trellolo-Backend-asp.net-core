using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DAL.Entities;

namespace TrelloProject.DAL.EF
{
    internal class TrelloDbContext : DbContext
    {
        public TrelloDbContext(DbContextOptions<TrelloDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                new User() { UserId = 1, FirstName = "Valya", LastName = "Zay", Email = "valya@valya.net" },
                new User() { UserId = 2, FirstName = "Vova", LastName = "Petrov", Email = "vova@vova.com" },
                new User() { UserId = 3, FirstName = "Gora", LastName = "Sidorov", Email = "gora@gora.net" }
                );

            modelBuilder.Entity<Board>()
                .HasData(
                new Board() { BoardId = 1, Title = "ManagerBoard", CurrentBackgroundColorId = 1 }
                );

            modelBuilder.Entity<UserBoard>()
                .HasData(
                new UserBoard() { UserId = 2, BoardId = 1 }
                );

            modelBuilder.Entity<BackgroundColor>()
                .HasData(
                new BackgroundColor() { BackgroundColorId = 1, ColorHex = "#C0C0C0" }, //grey
                new BackgroundColor() { BackgroundColorId = 2, ColorHex = "#ffff00" }, //yellow
                new BackgroundColor() { BackgroundColorId = 3, ColorHex = "#FFA500" }, //orange
                new BackgroundColor() { BackgroundColorId = 4, ColorHex = "#0000FF" }, //blue
                new BackgroundColor() { BackgroundColorId = 5, ColorHex = "#008000" } //green
                );

            modelBuilder.Entity<CardList>()
                .HasData(
                new CardList() { CardListId = 1, Title = "ToDo", Order = 0, BoardId = 1 },
                new CardList() { CardListId = 2, Title = "Done", Order = 1, BoardId = 1 },
                new CardList() { CardListId = 3, Title = "DoNOTDo", Order = 2, BoardId = 1 }
                );

            modelBuilder.Entity<Card>()
                .HasData(
                new Card() { CardId = 1, Title = "Create a task", Description = "Just create a new task", CreatedDate = DateTime.Parse("2019-08-23"), CreatedById = 1, AssigneeId = 2, Hidden = false, CardListId = 1 },
                new Card() { CardId = 2, Title = "Implement an Interface", Description = "Implement INewInterface now", CreatedDate = DateTime.Parse("2019-08-15"), CreatedById = 1, AssigneeId = 3, Hidden = false, CardListId = 1 }

                );

            modelBuilder.Entity<CardComment>()
                .HasData(
                new CardComment() { CardCommentId = 1, Text = "Good comment", CreatedDate = DateTime.Parse("2019-02-15"), CreatedById = 2, RefersToId = null, CardId = 1 },
                new CardComment() { CardCommentId = 2, Text = "Bad comment", CreatedDate = DateTime.Parse("2019-04-18"), CreatedById = 2, RefersToId = 1, CardId = 1 }

                );

            modelBuilder.Entity<UserBoard>().HasKey(userboard => new { userboard.UserId, userboard.BoardId });

            modelBuilder.Entity<Board>()
                .HasOne<BackgroundColor>(board => board.BackgroundColor)
                .WithMany(color => color.Boards)
                .HasForeignKey(board => board.CurrentBackgroundColorId);

            modelBuilder.Entity<CardList>()
                .HasOne<Board>(cardlist => cardlist.Board)
                .WithMany(board => board.CardLists)
                .HasForeignKey(cardlist => cardlist.BoardId);


            modelBuilder.Entity<Card>()
                .HasOne<CardList>(card => card.CardList)
                .WithMany(cardlist => cardlist.Cards)
                .HasForeignKey(card => card.CardListId);

            modelBuilder.Entity<Card>()
                .HasOne<User>(card => card.CreatedBy)
                .WithMany(cardlist => cardlist.CardsCreated)
                .HasForeignKey(card => card.CreatedById);


            modelBuilder.Entity<Card>()
                 .HasOne<User>(card => card.Assignee)
                 .WithMany(cardlist => cardlist.CardsAssigned)
                 .HasForeignKey(card => card.AssigneeId);
                 


            modelBuilder.Entity<CardComment>()
                .HasOne<Card>(comment => comment.Card)
                .WithMany(card => card.CardComments)
                .HasForeignKey(comment => comment.CardId);

            modelBuilder.Entity<CardComment>()
                .HasOne(comment => comment.RefersTo)
                .WithMany()
                .HasForeignKey(comment => comment.RefersToId);


            modelBuilder.Entity<CardComment>()
                .HasOne<User>(comment => comment.CreatedBy)
                .WithMany(user => user.CardComments)
                .HasForeignKey(comment => comment.CreatedById)
                .OnDelete(0);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<UserBoard> UserBoards { get; set; }
        public DbSet<BackgroundColor> BackgroundColors { get; set; }
        public DbSet<CardList> CardLists { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardComment> CardComments { get; set; }

    }
}
