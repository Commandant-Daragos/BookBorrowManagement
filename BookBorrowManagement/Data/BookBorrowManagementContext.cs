using BookBorrowManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBorrowManagement.Data
{
    public class BookBorrowManagementContext : DbContext
    {
        public BookBorrowManagementContext(DbContextOptions<BookBorrowManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
        public DbSet<User_Book_Management> User_Book_Management { get; set; } = default!;
        public DbSet<User_Book_Management_History> User_Book_Management_History { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the one-to-many relationship between User and User_Book_Management
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserBookManagements)
                .WithOne(ubm => ubm.User)
                .HasForeignKey(ubm => ubm.UserId);

            // Configuring the one-to-many relationship between Book and User_Book_Management
            modelBuilder.Entity<Book>()
                .HasMany(b => b.UserBookManagements)
                .WithOne(ubm => ubm.Book)
                .HasForeignKey(ubm => ubm.BookId);

            // Configuring the one-to-many relationship between User and User_Book_Management_History
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserBookManagementsHistory)
                .WithOne(ubmh => ubmh.User)
                .HasForeignKey(ubmh => ubmh.UserId);

            // Configuring the one-to-many relationship between Book and User_Book_Management_History
            modelBuilder.Entity<Book>()
                .HasMany(b => b.UserBookManagementsHistory)
                .WithOne(ubmh => ubmh.Book)
                .HasForeignKey(ubmh => ubmh.BookId);
        }
    }
}
