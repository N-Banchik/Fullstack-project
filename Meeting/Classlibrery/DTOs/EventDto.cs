using DataAccess.Data.Entities;

namespace DataAccess.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string? EventTitle { get; set; }
        public string? EventDescription { get; set; }
        public string? EventRules { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventCreated { get; set; }
        public string? EventLocation { get; set; }
        public bool Canceled { get; set; }
        public bool passed { get; set; }
        public string? MainPhotoUrl { get; set; }
        public MemberDto? Creator { get; set; }
        public ICollection<Photo<Event>>? Photos { get; set; }
        public ICollection<EventMemberDto>?Users { get; set; }
        public ICollection<PostDto>? Posts { get; set; }
    }
}
