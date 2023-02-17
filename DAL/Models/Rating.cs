using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Rating : BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Range(1, 5)]
        public decimal Score { get; set; }
    }
}
