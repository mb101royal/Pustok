using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nov30task.Models
{
	public class Book
	{
		public int Id { get; set; }
        [MaxLength(64)]
		public string Name { get; set; }
        [MaxLength(128)]
        public string? About { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal SellPrice { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal CostPrice { get; set; }
		[Range(0, 100)]
        public float Discount { get; set; }
        public ushort Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<BookImage> BookImages { get; set; }
    }
}
