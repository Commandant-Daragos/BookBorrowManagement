using System.ComponentModel.DataAnnotations;

namespace BookBorrowManagement.Models
{
    public class User_Book_Management
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }

        [DataType(DataType.Date)]
        public DateTime BorrowDate { get; set; }
        public Status Status { get; set; }

        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}
