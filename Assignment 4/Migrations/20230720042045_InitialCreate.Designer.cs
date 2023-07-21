﻿// <auto-generated />
using Assignment_4;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment_4.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230720042045_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("Assignment_4.Expenses", b =>
                {
                    b.Property<int>("expenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(6, 2)")
                        .HasColumnName("Amount");

                    b.Property<int>("kittyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("purpose")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Description");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasColumnName("Expenses Names");

                    b.HasKey("expenseId");

                    b.HasIndex("kittyId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Assignment_4.Kitty", b =>
                {
                    b.Property<int>("kittyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("eventName")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasColumnName("Event Name");

                    b.Property<int>("money")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Currency");

                    b.Property<int>("participantId")
                        .HasColumnType("INTEGER");

                    b.HasKey("kittyId");

                    b.HasIndex("participantId");

                    b.ToTable("Kitty");
                });

            modelBuilder.Entity("Assignment_4.Participant", b =>
                {
                    b.Property<int>("participantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasColumnName("Full Name");

                    b.HasKey("participantId");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("Assignment_4.SubParticipant", b =>
                {
                    b.Property<int>("subParticipantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("expenseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("kittyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasColumnName("Members Names");

                    b.Property<decimal>("owe")
                        .HasColumnType("decimal(6, 2)")
                        .HasColumnName("Owed Amount");

                    b.HasKey("subParticipantId");

                    b.HasIndex("expenseId");

                    b.HasIndex("kittyId");

                    b.ToTable("SubParticipant");
                });

            modelBuilder.Entity("Assignment_4.Expenses", b =>
                {
                    b.HasOne("Assignment_4.Kitty", "kitty")
                        .WithMany("expenses")
                        .HasForeignKey("kittyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("kitty");
                });

            modelBuilder.Entity("Assignment_4.Kitty", b =>
                {
                    b.HasOne("Assignment_4.Participant", "participant")
                        .WithMany("kitties")
                        .HasForeignKey("participantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("participant");
                });

            modelBuilder.Entity("Assignment_4.SubParticipant", b =>
                {
                    b.HasOne("Assignment_4.Expenses", "Expense")
                        .WithMany("subParticipantsE")
                        .HasForeignKey("expenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment_4.Kitty", "Kitty")
                        .WithMany("subParticipantsK")
                        .HasForeignKey("kittyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");

                    b.Navigation("Kitty");
                });

            modelBuilder.Entity("Assignment_4.Expenses", b =>
                {
                    b.Navigation("subParticipantsE");
                });

            modelBuilder.Entity("Assignment_4.Kitty", b =>
                {
                    b.Navigation("expenses");

                    b.Navigation("subParticipantsK");
                });

            modelBuilder.Entity("Assignment_4.Participant", b =>
                {
                    b.Navigation("kitties");
                });
#pragma warning restore 612, 618
        }
    }
}