using System.ComponentModel.DataAnnotations;

namespace ProniaMVC.ViewModels
{
    public class SliderUpdateVM
    {
        [MaxLength(32, ErrorMessage = "Title must be less than 32"), Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = null!;

        [MaxLength(64, ErrorMessage = "Title must be less than 32"), Required(ErrorMessage = "Subtitle is required")]
        public string Subtitle_1 { get; set; } = null!;

        [MaxLength(64, ErrorMessage = "Title must be less than 32"), Required(ErrorMessage = "Subtitle is required")]
        public string Subtitle_2 { get; set; } = null!;

        public string? Link { get; set; }

        public IFormFile? File { get; set; }
    }
}
