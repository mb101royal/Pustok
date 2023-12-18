using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using nov30task.Models;

namespace nov30task.ViewModels.BooksVM
{
    public class BookListItemVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? About { get; set; }
        public string? Description { get; set; }
        public decimal SellPrice { get; set; }
        public decimal CostPrice { get; set; }
        public float Discount { get; set; }
        public ushort Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
