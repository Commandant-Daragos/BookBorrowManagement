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
        public DateTime ReleaseDate { get; set; }

        public Status? Status { get; set; }
    }

    public enum Status
    {
        New,
        Borrowed,
        Returned
    }
}
