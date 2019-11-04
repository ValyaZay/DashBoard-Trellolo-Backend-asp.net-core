using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TrelloProject.DAL.Entities;

namespace TrelloProject.DAL.EF
{
    internal class TrelloDbContext : IdentityDbContext<User>
    {
        public TrelloDbContext(DbContextOptions<TrelloDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Board>()
                .HasIndex(board => board.Title)
                .IsUnique();

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

        //public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<UserBoard> UserBoards { get; set; }
        public DbSet<BackgroundColor> BackgroundColors { get; set; }
        public DbSet<CardList> CardLists { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardComment> CardComments { get; set; }

    }
}
