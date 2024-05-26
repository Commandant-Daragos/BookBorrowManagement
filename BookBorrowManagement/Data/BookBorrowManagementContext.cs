﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookBorrowManagement.Models;

namespace BookBorrowManagement.Data
{
    public class BookBorrowManagementContext : DbContext
    {
        public BookBorrowManagementContext(DbContextOptions<BookBorrowManagementContext> options)
            : base(options)
        {
        }

        public DbSet<BookBorrowManagement.Models.Book> Book { get; set; } = default!;
        public DbSet<BookBorrowManagement.Models.User_Book_Management> User_Book_Management { get; set; } = default!;
        public DbSet<BookBorrowManagement.Models.User> User { get; set; } = default!;
    }
}