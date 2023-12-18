namespace nov30task.Models
{
    public class BookImage
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        public int BookId { get; set; }
        public bool IsActive { get; set; }
        public Book? Book { get; set; }
    }
}
