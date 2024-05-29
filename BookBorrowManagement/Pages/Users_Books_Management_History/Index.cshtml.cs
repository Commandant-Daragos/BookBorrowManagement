using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookBorrowManagement.Data;
using BookBorrowManagement.Models;

namespace BookBorrowManagement.Pages.Users_Books_Management_History
{
    public class IndexModel : PageModel
    {
        private readonly BookBorrowManagement.Data.BookBorrowManagementContext _context;

        public IndexModel(BookBorrowManagement.Data.BookBorrowManagementContext context)
        {
            _context = context;
        }

        public IList<User_Book_Management_History> User_Book_Management_History { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User_Book_Management_History = await _context.User_Book_Management_History
                .Include(u => u.Book)
                .Include(u => u.User).ToListAsync();
        }
    }
}
