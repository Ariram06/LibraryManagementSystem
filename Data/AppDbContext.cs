using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.BookId);

                entity.Property(b => b.Title)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(b => b.Author)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(b => b.AvailableCopies)
                      .IsRequired();

                entity.Property(b => b.TotalCopies)
                      .IsRequired();
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.MemberId);

                entity.Property(m => m.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(m => m.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(m => m.PhoneNumber)
                      .IsRequired()
                      .HasMaxLength(15);

                entity.HasIndex(m => m.Email)
                      .IsUnique();
            });

            modelBuilder.Entity<BorrowRecord>(entity =>
            {
                entity.HasKey(br => br.BorrowRecordId);

                entity.HasOne(br => br.Book)
                      .WithMany(b => b.BorrowRecords)
                      .HasForeignKey(br => br.BookId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(br => br.Member)
                      .WithMany(m => m.BorrowRecords)
                      .HasForeignKey(br => br.MemberId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Fine>(entity =>
            {
                entity.HasKey(f => f.FineId);

                entity.Property(f => f.PaymentStatus)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasOne<BorrowRecord>() 
                      .WithOne()
                      .HasForeignKey<Fine>(f => f.BorrowRecordId)
                      .OnDelete(DeleteBehavior.Cascade); 
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                
                entity.HasIndex(u => u.Email)
                      .IsUnique();

                entity.Property(u => u.PasswordHash)
                      .IsRequired();
                entity.Property(u => u.Role)
                      .IsRequired()
                      .HasMaxLength(20)
                      .HasDefaultValue("Member");
            });




        }
    }
}
