using BookBorrowManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookBorrowManagement.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public Status? Status { get; set; }

        public virtual ICollection<User_Book_Management>? UserBookManagements { get; set; }
    }
}
