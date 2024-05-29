using BookBorrowManagement.Models;
using BookBorrowManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookBorrowManagement.Pages.Users_Books_Management
{
    public class CreateModel : PageModel
    {
        private readonly BookBorrowManagementContext _context;

        [BindProperty]
        public User_Book_Management? User_Book_Management { get; set; }
        public SelectList? Users { get; set; }
        public SelectList? Books { get; set; }

        public CreateModel(BookBorrowManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (User_Book_Management == null)
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
            book.Status = Enums.Status.Borrowed;
            //var user = await _context.User.FindAsync(User_Book_Management.UserId);

            if (!ModelState.IsValid)
            {
                Books = new SelectList(_context.Book, "Id", "Title");
                Users = new SelectList(_context.User, "Id", "Name");
                return Page();
            }

            // Add the User_Book_Management entity to the context
            _context.User_Book_Management.Add(User_Book_Management);


            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
