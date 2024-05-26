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
    public class DetailsModel : PageModel
    {
        private readonly BookBorrowManagement.Data.BookBorrowManagementContext _context;

        public DetailsModel(BookBorrowManagement.Data.BookBorrowManagementContext context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }
            return Page();
        }
    }
}
