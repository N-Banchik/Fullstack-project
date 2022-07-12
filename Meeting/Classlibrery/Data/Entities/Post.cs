namespace DataAccess.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int CreatorId { get; set; }
        public User? Creator { get; set; }
        public string? CreatorPhotoUrl { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public DateTime? EditTime { get; set; }
        public bool Deleted { get; set; }
    }
}
