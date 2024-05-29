using BookBorrowManagement.Models;
using BookBorrowManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookBorrowManagement.Pages.Users_Books_Management
{
    public class IndexModel : PageModel
    {
        private readonly BookBorrowManagementContext _context;

        public int ID { get; set; }

        public IndexModel(BookBorrowManagementContext context)
        {
            _context = context;
        }

        public IList<User_Book_Management> User_Book_Management { get; set; } = default!;

        public async Task OnGetAsync()
        {
            User_Book_Management = await _context.User_Book_Management
                .Include(u => u.Book)
                .Include(u => u.User).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //await _context.User_Book_Management.FindAsync(id);
            User_Book_Management = await _context.User_Book_Management
                .Include(u => u.Book)
                .Include(u => u.User).ToListAsync();

            var user_book_management = User_Book_Management.FirstOrDefault(m => m.Id == id);
            {
                user_book_management.Book.Status = Enums.Status.Returned;
                await _context.SaveChangesAsync();
            }

            return Page();
        }
    }
}
