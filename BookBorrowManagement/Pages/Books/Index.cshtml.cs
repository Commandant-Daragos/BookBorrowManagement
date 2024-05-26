using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookBorrowManagement.Data;
using BookBorrowManagement.Models;

namespace BookBorrowManagement.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BookBorrowManagement.Data.BookBorrowManagementContext _context;

        public IndexModel(BookBorrowManagement.Data.BookBorrowManagementContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Book = await _context.Book.ToListAsync();
        }
    }
}
