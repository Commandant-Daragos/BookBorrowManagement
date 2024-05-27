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
        public User_Book_Management User_Book_Management { get; set; } = default!;

        public CreateModel(BookBorrowManagement.Data.BookBorrowManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BookTitle"] = new SelectList(_context.Book.Select(b => b.Title).ToList());
        ViewData["UserName"] = new SelectList(_context.User.Select(u => u.Name +" "+ u.MidName +" "+ u.Surname).ToList());
            // Ensure User_Book_Management is instantiated
            if (User_Book_Management == null)
            {
                User_Book_Management = new User_Book_Management();
            }

            // Set the default value of BorrowDate to the current date
            User_Book_Management.BorrowDate = DateTime.Today;
            return Page();
        }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.User_Book_Management.Add(User_Book_Management);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
