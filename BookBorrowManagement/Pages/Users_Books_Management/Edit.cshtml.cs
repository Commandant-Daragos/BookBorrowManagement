using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookBorrowManagement.Data;
using BookBorrowManagement.Models;

namespace BookBorrowManagement.Pages.Users_Books_Management
{
    public class EditModel : PageModel
    {
        private readonly BookBorrowManagement.Data.BookBorrowManagementContext _context;

        public EditModel(BookBorrowManagement.Data.BookBorrowManagementContext context)
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

            var user_book_management =  await _context.User_Book_Management.FirstOrDefaultAsync(m => m.Id == id);
            if (user_book_management == null)
            {
                return NotFound();
            }
            User_Book_Management = user_book_management;
           ViewData["BookID"] = new SelectList(_context.Book, "Id", "Id");
           ViewData["UserID"] = new SelectList(_context.User, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(User_Book_Management).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_Book_ManagementExists(User_Book_Management.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool User_Book_ManagementExists(int id)
        {
            return _context.User_Book_Management.Any(e => e.Id == id);
        }
    }
}
