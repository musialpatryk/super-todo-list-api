using System;
using System.Collections.Generic;
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
    }
}
