using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookBorrowManagement.Data;
using BookBorrowManagement.Models;

namespace BookBorrowManagement.Pages.Users_Books_Management
{
    public class CreateModel : PageModel
    {
        private readonly BookBorrowManagement.Data.BookBorrowManagementContext _context;

        [BindProperty]
        public User_Book_Management? User_Book_Management { get; set; }
        public SelectList? Users { get; set; }
        public SelectList? Books { get; set; }

        public CreateModel(BookBorrowManagement.Data.BookBorrowManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if(User_Book_Management == null)
            {
                User_Book_Management = new User_Book_Management();
            }


            User_Book_Management.BorrowDate = DateTime.Now;
            Books = new SelectList(_context.Book, "Id", "Title");
            Users = new SelectList(_context.User, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var book = await _context.Book.FindAsync(User_Book_Management.BookId);
            var user = await _context.User.FindAsync(User_Book_Management.UserId);

            if (!ModelState.IsValid)
            {
                Books = new SelectList(_context.Book, "Id", "Title");
                Users = new SelectList(_context.User, "Id", "Name");
                return Page();
            }

            // Add the User_Book_Management entity to the context
            _context.User_Book_Management.Add(User_Book_Management);

            // Set the navigation properties
            User_Book_Management.User = user;
            User_Book_Management.Book = book;

            book.UserBookManagements.Add(User_Book_Management);
            user.UserBookManagements.Add(User_Book_Management);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
