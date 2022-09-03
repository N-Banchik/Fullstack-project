namespace DataAccess.DTOs.ViewDtos
{
    public class GuideViewDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string? CreatorUserName { get; set; }
        public int HobbyId { get; set; }
    }
}
