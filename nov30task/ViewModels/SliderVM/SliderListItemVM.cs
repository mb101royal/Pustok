using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.SliderVM
{
    public class SliderListItemVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public string ButtonText { get; set; }

        public bool IsLeft { get; set; }

    }
}
