using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ToDoApp.Models
{
    public partial class ShobDBContext : DbContext
    {
        public ShobDBContext()
        {
        }

        public ShobDBContext(DbContextOptions<ShobDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Todotbl> Todotbl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ShobDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todotbl>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.ToTable("todotbl");

                entity.Property(e => e.TaskId).ValueGeneratedNever();

                entity.Property(e => e.TaskDisc).HasMaxLength(100);

                entity.Property(e => e.TaskName).HasMaxLength(50);
            });
        }
    }
}
