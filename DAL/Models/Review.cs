namespace DAL.Models
{
    public class Review : BaseEntity
    {
        public string Message { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string Reviewer { get; set; }
    }
}
