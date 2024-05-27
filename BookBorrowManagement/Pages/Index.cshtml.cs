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

        //[BindProperty(SupportsGet = true)]
        //public int? SelectedBookId { get; set; }

        public IndexModel(ILogger<IndexModel> logger, BookBorrowManagement.Data.BookBorrowManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            IQueryable<int> bookIds = from b in _context.Book
                                      select b.Id;

            BookIDs = new SelectList(await bookIds.ToListAsync());
        }

        public IActionResult OnGetDelete(int id)
        {
            //if (SelectedBookId is not int)
            //{
            //    ModelState.AddModelError(string.Empty, "You must select a book to delete.");
            //    return Page();
            //}


            return RedirectToPage("/Books/Delete", new { id });
        }
    }
}
