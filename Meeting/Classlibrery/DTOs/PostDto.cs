namespace DataAccess.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int CreatorId { get; set; }
        public string? CreatorUserName { get; set; }
        public string? CreatorPhotoUrl { get; set; }
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime? EditTime { get; set; }
    }
}
