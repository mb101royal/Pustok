using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.SlidersVM
{
    public class SliderListItemVM
    {
         public int Id { get; set; }

        [Required, MinLength(3), MaxLength(32)]
        public string? Title { get; set; }

        [MinLength(3), MaxLength(64)]
        public string? Text { get; set; }

        [Required, MaxLength(256)]
        public string? ImageUrl { get; set; }

        [Required, MinLength(3), MaxLength(16)]
        public string? ButtonText { get; set; }

        [Required]
        public bool Position { get; set; }

    }
}
