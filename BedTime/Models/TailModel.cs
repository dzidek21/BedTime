namespace BedTime.Models
{
    public class TailModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Story { get; set; }
        public Uri? ImageUrl { get; set; }
        public bool Favorites { get; set; }
        public int LikeCount { get; set; }
    }
}
