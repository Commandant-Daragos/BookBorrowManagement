using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookBorrowManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BookBorrowManagement.Data.BookBorrowManagementContext _context;

        public SelectList? BookIDs { get; set; }

        ////[BindProperty(SupportsGet = true)]
        ////public int? SelectedBookId { get; set; }

        public IndexModel(ILogger<IndexModel> logger, BookBorrowManagement.Data.BookBorrowManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
           BookIDs = new SelectList(_context.Book, "Id", "Title");
        }

        public IActionResult OnGetDelete(int id)
        {
            return RedirectToPage("/Books/Delete", new { id });
        }
    }
}
