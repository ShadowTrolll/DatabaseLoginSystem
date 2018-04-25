using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatabaseTrybutitsEntityFramework.Models
{
    public partial class Data1Context : DbContext
    {
        public virtual DbSet<UserData> UserData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=HONZA-PC;Database=Data1;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>(entity =>
            {
                entity.HasKey(e => e.UserID);

                entity.Property(e => e.UserID)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserPass)
                    .IsRequired()
                    .HasMaxLength(300);
            });
        }
    }
}
