using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.SlidersVM
{
    public class SliderUpdateVM
    {
        [Required, MinLength(3), MaxLength(32)]
        public string? Title { get; set; }

        [MinLength(3), MaxLength(64)]
        public string? Text { get; set; }

        [Required, MaxLength(256), DisplayName("Image url")]
        public string? ImageUrl { get; set; }

        [Required, MinLength(3), MaxLength(16), DisplayName("Button Text")]
        public string? ButtonText { get; set; }

        public byte Position { get; set; }
    }
}
