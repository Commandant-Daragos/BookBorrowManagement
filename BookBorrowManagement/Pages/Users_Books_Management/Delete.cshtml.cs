using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookBorrowManagement.Data;
using BookBorrowManagement.Models;

namespace BookBorrowManagement.Pages.Users_Books_Management
{
    public class DeleteModel : PageModel
    {
        private readonly BookBorrowManagement.Data.BookBorrowManagementContext _context;

        public DeleteModel(BookBorrowManagement.Data.BookBorrowManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User_Book_Management User_Book_Management { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user_book_management = await _context.User_Book_Management
                                                        .Include(ubm => ubm.Book)
                                                        .Include(ubm => ubm.User)
                                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (user_book_management == null)
            {
                return NotFound();
            }
            else
            {
                User_Book_Management = user_book_management;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user_book_management = await _context.User_Book_Management.FindAsync(id);
            if (user_book_management != null)
            {
                User_Book_Management = user_book_management;
                _context.User_Book_Management.Remove(User_Book_Management);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
