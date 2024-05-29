using BookBorrowManagement.Models;
using BookBorrowManagement.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookBorrowManagement.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BookBorrowManagementContext _context;

        public IndexModel(BookBorrowManagementContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Book = await _context.Book.ToListAsync();
        }
    }
}
