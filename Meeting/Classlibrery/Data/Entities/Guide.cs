namespace DataAccess.Data.Entities
{
    public class Guide
    {
       
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? EditDate { get; set; }

        public int CreatorId { get; set; }
        public User? Creator { get; set; }

        public int HobbyId { get; set; }
        public Hobby? Hobby { get; set; }



    }
}
