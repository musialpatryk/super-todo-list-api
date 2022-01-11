﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoListApiSqlite.Data;

#nullable disable

namespace TodoListApiSqlite.Migrations
{
    [DbContext(typeof(TodoListApiContext))]
    partial class TodoListApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("TodoListApiSqlite.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TodoListApiSqlite.Models.GroupUser", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupUsers");
                });

            modelBuilder.Entity("TodoListApiSqlite.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Priority")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("TodoListApiSqlite.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("InvitationLink")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TodoListApiSqlite.Models.GroupUser", b =>
                {
                    b.HasOne("TodoListApiSqlite.Models.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TodoListApiSqlite.Models.User", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TodoListApiSqlite.Models.Note", b =>
                {
                    b.HasOne("TodoListApiSqlite.Models.Group", "Group")
                        .WithMany("Notes")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("TodoListApiSqlite.Models.Group", b =>
                {
                    b.Navigation("Notes");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("TodoListApiSqlite.Models.User", b =>
                {
                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
