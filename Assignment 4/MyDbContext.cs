using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    public class MyDbContext : DbContext
    {

        public DbSet<Kitty> Kitty { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<SubParticipant> SubParticipant { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = @"C:\Users\Yehia\source\repos\Assignment 4\Assignment 4\KittyDb.db";
            optionsBuilder.UseSqlite($"Data Source={dbPath};");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kitty>(entity =>
            {
                entity.Property(e => e.eventName)
                .HasColumnName("Event Name")
                .HasColumnType("varchar(25)")
                .IsRequired();

                entity.Property(e => e.money)
                .HasColumnName("Currency");
                

                entity.HasKey(e => e.kittyId);

                entity.HasOne(e => e.participant)
                .WithMany(e => e.kitties)
                .HasForeignKey(e => e.participantId);

               
            });

            modelBuilder.Entity<Expenses>(entity =>
            {
                

                entity.HasOne(k => k.kitty)
                .WithMany(e => e.expenses)
                .HasForeignKey(k => k.kittyId);

                entity.Property(e => e.amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(6, 2)")
                .IsRequired();

                entity.Property(e => e.purpose)
                .HasColumnName("Description")
                .HasColumnType("text");

                entity.Property(e => e.type)
                .HasColumnName("Expenses Names")
                .HasColumnType("varchar(25)");

                entity.HasKey(e => e.expenseId);

            });
            modelBuilder.Entity<Participant>(entity =>
            {

                entity.Property(e => e.name)
                .HasColumnType("varchar(25)")
                .HasColumnName("Full Name")
                .IsRequired();

                entity.HasKey(e => e.participantId);

                
            });
            modelBuilder.Entity<SubParticipant>(entity =>
            {
                entity.HasKey(e => e.subParticipantId);
                entity.Property(e => e.subParticipantId).ValueGeneratedOnAdd();
                entity.Property(e => e.name)
                      .HasColumnType("varchar(25)")
                      .HasColumnName("Members Names");

                entity.Property(e => e.owe)
                      .HasColumnType("decimal(6, 2)")
                      .HasColumnName("Owed Amount");

                entity.HasOne(e => e.Kitty)
                .WithMany(e => e.subParticipantsK)
                .HasForeignKey(e => e.kittyId);

                entity.HasOne(e => e.Expense)
                .WithMany(e => e.subParticipantsE)
                .HasForeignKey(e => e.expenseId);


            });
        }
    }
}
