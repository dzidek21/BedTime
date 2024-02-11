

namespace BedTime.Models
{
    public class LikedStory
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public bool IsLiked { get; set; }
    }
}
