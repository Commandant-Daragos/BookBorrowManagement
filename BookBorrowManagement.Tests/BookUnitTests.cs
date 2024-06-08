using BookBorrowManagement.Data;
using BookBorrowManagement.Models;
using BookBorrowManagement.Pages.Books;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

public class BookUnitTests
{
    //need further analysis for unit tests and database usage
    [Fact]
    public async Task OnPostAsync_ValidModel_AddsBookToDatabase()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Book>>();
        var mockContext = new Mock<BookBorrowManagementContext>(new DbContextOptions<BookBorrowManagementContext>());
        mockContext.Setup(m => m.Book).Returns(mockSet.Object);

        var createModel = new CreateModel(mockContext.Object)
        {
            Book = new Book
            {
                Title = "Test Title",
                Genre = "TestGenre",
                Description = "",
                ReleaseDate = new DateTime(1998, 5, 5)
            }
        };

        // Act
        var result = await createModel.OnPostAsync();

        // Assert
        mockSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Once());
        mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());

        var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("./Index", redirectToPageResult.PageName);
    }

    [Fact]
    public async Task OnPostAsync_InvalidModel_ReturnsPage()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Book>>();
        var mockContext = new Mock<BookBorrowManagementContext>(new DbContextOptions<BookBorrowManagementContext>());
        mockContext.Setup(m => m.Book).Returns(mockSet.Object);

        var createModel = new CreateModel(mockContext.Object);
        createModel.ModelState.AddModelError("Title", "Required");

        // Act
        var result = await createModel.OnPostAsync();

        // Assert
        var pageResult = Assert.IsType<PageResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_DeletesBookFromDatabase()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Book>>();
        var mockContext = new Mock<BookBorrowManagementContext>(new DbContextOptions<BookBorrowManagementContext>());
        mockContext.Setup(m => m.Book).Returns(mockSet.Object);

        var deleteModel = new DeleteModel(mockContext.Object)
        {
            Book = new Book { Id = 1, Title = "Test Title" }
        };

        mockSet.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(deleteModel.Book);
        mockSet.Setup(m => m.Remove(It.IsAny<Book>())).Callback<Book>(b => mockSet.Object.Remove(b));

        // Act
        var result = await deleteModel.OnPostAsync(1);

        // Assert
        mockSet.Verify(m => m.Remove(It.IsAny<Book>()), Times.Once());
        mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());

        var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("./Index", redirectToPageResult.PageName);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsBookDetail()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Book>>();
        var mockContext = new Mock<BookBorrowManagementContext>(new DbContextOptions<BookBorrowManagementContext>());
        mockContext.Setup(m => m.Book).Returns(mockSet.Object);

        var book = new Book { Id = 1, Title = "Test Title" };
        //mockSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>())).ReturnsAsync(book); CS0854 An expression tree may not contain a call or invocation that uses optional arguments
        //mockSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(book);
        mockSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>(), default)).ReturnsAsync(book);

        var detailModel = new DetailsModel(mockContext.Object);

        // Act
        var result = await detailModel.OnGetAsync(1);

        // Assert
        var pageResult = Assert.IsType<PageResult>(result);
        Assert.Equal("Test Title", detailModel.Book.Title);
    }

    [Fact]
    public async Task OnPostAsync_ValidModel_EditsBookInDatabase()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Book>>();
        var mockContext = new Mock<BookBorrowManagementContext>(new DbContextOptions<BookBorrowManagementContext>());
        mockContext.Setup(m => m.Book).Returns(mockSet.Object);

        var editModel = new EditModel(mockContext.Object)
        {
            Book = new Book
            {
                Id = 1,
                Title = "Updated Title",
                Genre = "UpdatedGenre",
                Description = "Updated Description",
                ReleaseDate = new DateTime(2000, 1, 1)
            }
        };

        // Act
        var result = await editModel.OnPostAsync();

        // Assert
        mockSet.Verify(m => m.Attach(It.IsAny<Book>()), Times.Once());
        mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());

        var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("./Index", redirectToPageResult.PageName);
    }

    [Fact]
    public async Task OnGetAsync_ReturnsListOfBooks()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Book>>();
        var mockContext = new Mock<BookBorrowManagementContext>(new DbContextOptions<BookBorrowManagementContext>());
        mockContext.Setup(m => m.Book).Returns(mockSet.Object);

        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Test Title 1" },
            new Book { Id = 2, Title = "Test Title 2" }
        }.AsQueryable();

        mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
        mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
        mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
        mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

        var indexModel = new IndexModel(mockContext.Object);

        // Act
        await indexModel.OnGetAsync();

        // Assert
        Assert.Equal(2, indexModel.Book.Count);
        Assert.Equal("Test Title 1", indexModel.Book[0].Title);
        Assert.Equal("Test Title 2", indexModel.Book[1].Title);
    }
}
