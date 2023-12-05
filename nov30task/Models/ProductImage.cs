namespace nov30task.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int ProductId { get; set; }
        public bool IsActive { get; set; }
        public Product Product { get; set; }
    }
}
