namespace DataAccess.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
