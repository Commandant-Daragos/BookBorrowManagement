using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookBorrowManagement.Data;
using BookBorrowManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookBorrowManagement.Pages.Users_Books_Management
{
    public class DetailsModel : PageModel
    {
        private readonly BookBorrowManagement.Data.BookBorrowManagementContext _context;

        public User DetailUser { get; set; }
        public Book Book { get; set; }

        public DetailsModel(BookBorrowManagement.Data.BookBorrowManagementContext context)
        {
            _context = context;
        }

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

                //var book = await _context.Book.FindAsync(User_Book_Management.BookId);
                //var user = await _context.User.FindAsync(User_Book_Management.UserId);

                //DetailUser = user;
                //Book = book;
            }
            return Page();
        }
    }
}
