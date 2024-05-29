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

            User_Book_Management = await _context.User_Book_Management
                .Include(u => u.Book)
                .Include(u => u.User).ToListAsync();

            var user_book_management = User_Book_Management.FirstOrDefault(m => m.Id == id);

            User_Book_Management_History users_Books_Management_History = new User_Book_Management_History() 
            { BookId = user_book_management.BookId,
              UserId = user_book_management.UserId,
              BorrowDate = user_book_management.BorrowDate,
              ReturnDate = DateTime.Now,
            };

            user_book_management.Book.Status = Enums.Status.Returned;
            _context.User_Book_Management_History.Add(users_Books_Management_History);
            _context.User_Book_Management.Remove(user_book_management);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index"); //check solution
            //return Page();
        }
    }
}
