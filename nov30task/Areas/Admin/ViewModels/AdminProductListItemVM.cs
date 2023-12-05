using nov30task.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nov30task.Areas.Admin.ViewModels
{
	public class AdminProductListItemVM
	{
        public int Id { get; set; }

		[MaxLength(64)]
        public string Name { get; set; }

		[Column(TypeName = "smallmoney")]
		public decimal SellPrice { get; set; }
		[Column(TypeName = "smallmoney")]
		public decimal CostPrice { get; set; }
		[Range(0, 100)]
		public float Discount { get; set; }

		public ushort Quantity { get; set; }
		public string ImageUrl { get; set; }
		public Category? Category { get; set; }
		public bool IsDeleted { get; set; }
	}
}
