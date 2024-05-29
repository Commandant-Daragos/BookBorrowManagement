using BookBorrowManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookBorrowManagement.Models
{
    public class Book
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s]*$", ErrorMessage = "Book Title can contain only letters, numbers and white spaces, starting with capital letter!")]
        [Required]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "Title can contain max 60 characters, minimum allowed is 5!")]
        public string Title { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Book Genre can contain only letters and white spaces, starting with capital letter!")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Genre can contain max 20 characters, minimum allowed is 5!")]
        public string? Genre { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s]*$", ErrorMessage = "Book Description can contain only letters, numbers and white spaces, starting with capital letter!")]
        [StringLength(120, MinimumLength = 20, ErrorMessage = "Title can contain max 120 characters, minimum allowed is 20!")]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public Status? Status { get; set; }

        public virtual ICollection<User_Book_Management>? UserBookManagements { get; set; }
        public virtual ICollection<User_Book_Management_History>? UserBookManagementsHistory { get; set; }
    }
}
