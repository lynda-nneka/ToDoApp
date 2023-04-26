using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Context
{
    public class ToDoAppDbContext : DbContext
    {
        public ToDoAppDbContext(DbContextOptions<ToDoAppDbContext>options): base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Todo> ToDoList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasMany(u => u.Todos)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
               .Property(u => u.FirstName)
               .HasMaxLength(50)
               .IsRequired();

            modelBuilder.Entity<User>()
              .Property(u => u.LastName)
              .HasMaxLength(50)
              .IsRequired();

            modelBuilder.Entity<Todo>()
               .Property(t => t.Title)
               .HasMaxLength(50)
               .IsRequired();

            modelBuilder.Entity<Todo>()
                .Property(t => t.Description)
                .HasMaxLength(100);

            base.OnModelCreating(modelBuilder);
        }
    }
}

