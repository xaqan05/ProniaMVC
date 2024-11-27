using System.ComponentModel.DataAnnotations;

namespace ProniaMVC.Models
{
    public class Slider : BaseEntity
    {
        [MaxLength(32)]
        public string Title { get; set; } = null!;

        [MaxLength(64)]
        public string Subtitle_1 { get; set; } = null!;
        
        [MaxLength(64)]
        public string Subtitle_2 { get; set; } = null!;

        public string? Link { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
