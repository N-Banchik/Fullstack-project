namespace DataAccess.DTOs
{
    public class GuideDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
        public string? CreatorUserName { get; set; }
        
    }
}
