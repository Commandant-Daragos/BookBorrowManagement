using System.ComponentModel.DataAnnotations;

namespace BookBorrowManagement.Models
{
    public class User_Book_Management_History
    {
        //primary key
        public int Id { get; set; }

        //foreign keys
        public int UserId { get; set; }
        public int BookId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Borrow Date")]
        public DateTime BorrowDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        public virtual User? User { get; set; }
        public virtual Book? Book { get; set; }
    }
}
