using BookBorrowManagement.Data;
using BookBorrowManagement.Enums;
using BookBorrowManagement.Models;
using BookBorrowManagement.Pages.Books;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookBorrowManagement.Tests
{
    public class BookTests : IDisposable
    {
        private readonly BookBorrowManagementContext _dbContext;

        public BookTests()
        {
            var options = new DbContextOptionsBuilder<BookBorrowManagementContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new BookBorrowManagementContext(options);
            _dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Fact]
        public async Task OnPostAsync_CreatesNewBookInDatabaseTest()
        {
            //Arrange
            var pageModel = new CreateModel(_dbContext)
            {
                Book = new Book
                {
                    Title = "Test Title",
                    Genre = "TestGenre",
                    Description = "",
                    ReleaseDate = new DateTime(1998, 5, 5)
                }
            };


            //Act
            await pageModel.OnPostAsync();

            //Assert
            var books = await _dbContext.Book.ToListAsync();
            Assert.Single(books);
            Assert.Equal("Test Title", books[0].Title);
            Assert.Equal("TestGenre", books[0].Genre);
            Assert.Equal("", books[0].Description);
            Assert.Equal(new DateTime(1998, 5, 5), books[0].ReleaseDate);
            Assert.Equal(Status.New, books[0].Status);
        }
    }
}