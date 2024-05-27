using BookBorrowManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace BookBorrowManagement.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookBorrowManagementContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BookBorrowManagementContext>>()))
            {
                if (context == null || context.Book == null || context.User == null)
                {
                    throw new ArgumentNullException("BookBorrowManagementContext");
                }

                // Look for any movies.
                if (context.Book.Any() || context.User.Any())
                {
                    return;   // DB has been seeded
                }

                context.Book.AddRange(
                    new Book
                    {
                        Title = "How to build a catapult.",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "History novel",
                        Description = "Description of catapults in 19th. century",
                        Status = Status.New
                    },

                    new Book
                    {
                        Title = "How to build a flying machine.",
                        ReleaseDate = DateTime.Parse("1500-5-10"),
                        Genre = "History novel",
                        Description = "Description of flying machines in 15th. century",
                        Status = Status.New
                    },

                    new Book
                    {
                        Title = "Tutorial how to be good doggo.",
                        ReleaseDate = DateTime.Parse("1999-1-23"),
                        Genre = "Life support",
                        Description = "Book for dogs on how to properly behave.",
                        Status = Status.New
                    },

                    new Book
                    {
                        Title = "How to build a catapult(Again)",
                        ReleaseDate = DateTime.Parse("1325-12-12"),
                        Genre = "History novel",
                        Description = "Description of catapults in 15th. century(Again-because content did not change)",
                        Status = Status.New
                    }
                );

                context.User.AddRange(
                    new User
                    {
                        Name = "Friedrich",
                        Surname = "Romanes",
                        Email = "friedrich.romanes@catapults4ever.com",
                        CreatedDate = DateTime.Parse("1500-5-10")
                    },

                    new User
                    {
                        Name = "Samuel",
                        MidName = "Lobotomy",
                        Surname = "Lackson",
                        Email = "sammy88@somemail.com",
                        CreatedDate = DateTime.Parse("2020-6-22")
                    },

                    new User
                    {
                        Name = "Alibaba",
                        MidName = "The",
                        Surname = "Djin",
                        Email = "alibabathedjin@3wishes.com",
                        CreatedDate = DateTime.Parse("1653-1-1")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
