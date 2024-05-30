

using BookBorrowManagement.Data;
using BookBorrowManagement.Pages.Books;
using Moq;

namespace BookBorrowManagement.Tests
{
    public class BookTests : IDisposable
    {
        private Mock<CreateModel> _createModel;
        private Mock<BookBorrowManagementContext> _context;

        public BookTests()
        {
            _context = new Mock<BookBorrowManagementContext>();
        }

        public void Dispose()
        {
            _createModel = null;
        }

        [Fact]
        public void CreateNewBookTest()
        {
            //Arrange
            _createModel = new Mock<CreateModel>();

            //Act

            //Assert
        }


    }
}