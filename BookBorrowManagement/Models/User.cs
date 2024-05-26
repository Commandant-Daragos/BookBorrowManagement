using System.ComponentModel.DataAnnotations;

namespace BookBorrowManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? MidName { get; set; }
        public string Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

    }
}
