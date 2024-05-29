using System.ComponentModel.DataAnnotations;

namespace BookBorrowManagement.Models
{
    public class User
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Name can contain only letters, starting with capital letter!")]
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Name can contain max 10 letters, minimum allowed is 3!")]
        public string Name { get; set; }

        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Middle Name can contain only letters, starting with capital letter!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Middle Name can contain max 10 characters, minimum allowed is 3!")]
        public string? MidName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Surname can contain only letters, starting with capital letter!")]
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Surname can contain max 20 characters, minimum allowed is 5!")]
        public string Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<User_Book_Management>? UserBookManagements { get; set; }
        public virtual ICollection<User_Book_Management_History>? UserBookManagementsHistory { get; set; }

    }
}
