using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.BooksVM
{
    public class BookUpdateVM
    {
<<<<<<< HEAD
        public string? Name { get; set; }
=======
        [MaxLength(64)]
        public string Name { get; set; }
>>>>>>> 1c03025e5f05c7c77e8ab41bf5db0c598bebb33f
        [MaxLength(128)]
        public string? About { get; set; }

        public string? Description { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal SellPrice { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal CostPrice { get; set; }
        [Range(0, 100)]
        public float Discount { get; set; }
<<<<<<< HEAD

        /*public IFormFile? ImageFile { get; set; }*/

=======
>>>>>>> 1c03025e5f05c7c77e8ab41bf5db0c598bebb33f
        public ushort Quantity { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
