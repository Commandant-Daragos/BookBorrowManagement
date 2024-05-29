using BookBorrowManagement.Models;
using BookBorrowManagement.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookBorrowManagement.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly BookBorrowManagementContext _context;

        public IndexModel(BookBorrowManagementContext context)
        {
            _context = context;
        }

        public IList<User> User { get; set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
