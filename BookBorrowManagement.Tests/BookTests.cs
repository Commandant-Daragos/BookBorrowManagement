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

        /// <summary>
        /// Test to check if API for creating working correctly.
        /// Book should be created and data stored in database.
        /// Test if book status is automatically added in OnPostAsync when creating book.
        /// </summary
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

        /// <summary>
        /// Test to check if API for deleting working correctly.
        /// Book data should be deleted from database.
        /// </summary>
        [Fact]
        public async Task OnPostAsync_DeletesBookFromDatabaseTest()
        {
            // Arrange
            await SeedDatabaseAsync();
            var bookToDelete = await _dbContext.Book.FirstAsync();

            var pageModel = new DeleteModel(_dbContext);

            // Act
            await pageModel.OnPostAsync(bookToDelete.Id);

            // Assert
            var books = await _dbContext.Book.ToListAsync();
            Assert.Empty(books);
        }

        /// <summary>
        /// Test to check if API for detail working correctly.
        /// Book data should be obtained from database.
        /// </summary>
        [Fact]
        public async Task OnGetAsync_DetailFromBookInDatabaseTest()
        {
            // Arrange
            await SeedDatabaseAsync();
            var bookToDelete = await _dbContext.Book.FirstAsync();

            var pageModel = new DetailsModel(_dbContext);

            // Act
            await pageModel.OnGetAsync(bookToDelete.Id);

            // Assert
            var books = await _dbContext.Book.ToListAsync();
            Assert.Single(books);
            Assert.Equal("Test Title", books[0].Title);
            Assert.Equal("TestGenre", books[0].Genre);
            Assert.Equal("", books[0].Description);
            Assert.Equal(new DateTime(1998, 5, 5), books[0].ReleaseDate);
            Assert.Equal(Status.New, books[0].Status);
        }

        /// <summary>
        /// Test to check if API for edit existing book working correctly.
        /// Book data should be obtained from database and after edit,
        /// different data should be present.
        /// </summary>
        [Fact]
        public async Task OnPostAsync_EditsBookInDatabaseTest()
        {
            // Arrange
            await SeedDatabaseAsync();
            var bookToEdit = await _dbContext.Book.FirstAsync();

            // Assert 1
            Assert.Equal("Test Title", bookToEdit.Title);
            Assert.Equal("TestGenre", bookToEdit.Genre);
            Assert.Equal("", bookToEdit.Description);
            Assert.Equal(new DateTime(1998, 5, 5), bookToEdit.ReleaseDate);
            Assert.Equal(Status.New, bookToEdit.Status);

            // Create pageModel and assign the existing book
            var pageModel = new EditModel(_dbContext)
            {
                Book = bookToEdit
            };

            // Update properties
            pageModel.Book.Title = "Updated Title";
            pageModel.Book.Genre = "UpdatedGenre";
            pageModel.Book.Description = "Updated Description";
            pageModel.Book.ReleaseDate = new DateTime(2000, 1, 1);
            pageModel.Book.Status = Status.Borrowed;


            // Act
            await pageModel.OnPostAsync();

            // Assert 2
            var editedBook = await _dbContext.Book.FindAsync(bookToEdit.Id);
            Assert.Equal("Updated Title", editedBook.Title);
            Assert.Equal("UpdatedGenre", editedBook.Genre);
            Assert.Equal("Updated Description", editedBook.Description);
            Assert.Equal(new DateTime(2000, 1, 1), editedBook.ReleaseDate);
            Assert.Equal(Status.Borrowed, editedBook.Status);
        }

        /// <summary>
        /// Test to check if API to get list of books working correctly.
        /// Books data should be obtained from database.
        /// </summary>
        [Fact]
        public async Task OnGetAsync_BookListFromDatabaseTest()
        {
            // Arrange
            await SeedDatabaseWithThreeBooksAsync();
            var pageModel = new IndexModel(_dbContext);

            // Act
            await pageModel.OnGetAsync();

            // Assert
            var books = pageModel.Book;
            Assert.NotNull(books);
            Assert.Equal(3, books.Count);
        }

        /// <summary>
        /// Support async Task method to create and store book data in database.
        /// Method used in tests for delete, detail and edit.
        /// </summary>
        private async Task SeedDatabaseAsync()
        {
            var book = new Book
            {
                Title = "Test Title",
                Genre = "TestGenre",
                Description = "",
                ReleaseDate = new DateTime(1998, 5, 5),
                Status = Status.New
            };

            _dbContext.Book.Add(book);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Support async Task method to create and store books data in database.
        /// Method used in test to get list of boks from database.
        /// </summary>
        private async Task SeedDatabaseWithThreeBooksAsync()
        {
            var books = new List<Book>
            {
                new Book
                {
                    Title = "Test Title",
                    Genre = "TestGenre",
                    Description = "",
                    ReleaseDate = new DateTime(1998, 5, 5),
                    Status = Status.New
                },

                new Book
                {
                    Title = "Test Title 1",
                    Genre = "TestGenre1",
                    Description = "",
                    ReleaseDate = new DateTime(1998, 5, 5),
                    Status = Status.Borrowed
                },
                new Book
                {
                    Title = "Test Title 2",
                    Genre = "TestGenre2",
                    Description = "",
                    ReleaseDate = new DateTime(1998, 5, 5),
                    Status = Status.Returned
                }
            };

            _dbContext.Book.AddRange(books);
            await _dbContext.SaveChangesAsync();
        }
    }
}