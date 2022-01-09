using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TodoListApiSqlite.Models;

namespace TodoListApiSqlite.Data
{
    public partial class TodoListApiContext : DbContext
    {
        public TodoListApiContext()
        {
        }

        public TodoListApiContext(DbContextOptions<TodoListApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        public virtual DbSet<GroupUser> GroupUsers { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupUser>()
                .HasKey(gu => new {gu.GroupId, gu.UserId});

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.Group)
                .WithMany(gu => gu.Users)
                .HasForeignKey(gu => gu.GroupId);

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.User)
                .WithMany(u => u.Groups)
                .HasForeignKey(u => u.UserId);
        }
    }
}
