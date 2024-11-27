namespace ProniaMVC.ViewModels
{
    public class SliderItemVM
    {
        public string Title { get; set; } = null!;

        public string Subtitle_1 { get; set; } = null!;
        public string Subtitle_2 { get; set; } = null!;

        public string? Link { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
