using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class SeedData
    {
        string lorm = "Mauris pellentesque imperdiet risus, id porta risus vehicula ac. Integer viverra est felis, vitae lacinia libero porttitor sed. Integer eget venenatis felis, at finibus lacus. Cras ac magna ac est imperdiet convallis sit amet at eros. Nunc porta quam eget lacus ullamcorper iaculis. Duis gravida gravida purus a laoreet. Praesent ut neque pharetra, viverra urna id, condimentum massa. Suspendisse potenti. Pellentesque cursus varius congue. Vestibulum ipsum eros, mollis ut elit sed, pulvinar luctus orci. Nulla sed lectus et nibh ultricies scelerisque ut id dolor. Donec egestas ligula vitae ex sagittis, a vestibulum augue lobortis. Donec elementum urna neque, vel condimentum ante pellentesque ac. Duis mattis, orci ac tempus dignissim, lorem est consequat nunc, quis pulvinar ante dui in diam. Nam fermentum lobortis nunc, eu pulvinar orci iaculis et.";
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        public SeedData(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Seed()
        {
            await _context.Database.MigrateAsync();
            await _roleManager.CreateAsync(new IdentityRole<int>("Member"));
            await _roleManager.CreateAsync(new IdentityRole<int>("Admin"));
            List<User> users = new List<User>();
            string password = "Password123!";
            for (int i = 0; i < 7; i++)
            {
                await _userManager.CreateAsync(new User() { UserName = $"User{i}", Email = $"TestEmail{i}@gmail.com", FirstName = $"john{i}", LastName = "doe", DateOfBirth = DateTime.Now.AddMonths(i * -2).AddYears(i * -1), City="Tel Aviv",Country="Israel" },password);
            }
            List<Guide> guides = new List<Guide>() { new Guide() { Title="Guide1",Content=lorm,CreatorId=2}, new Guide() { Title = "Guide2", Content = lorm, CreatorId = 1 } };
            List<Event> events = new List<Event>() { new Event() { EventTitle = "event1", EventDescription = "this is an event! how amazing.", EventRules = "c'mon. we already talked about rules.", EventLocation = "Independence square,Tel Aviv,Israel", EventDate = DateTime.Now.AddDays(15), EventCreatorId = 5, Posts = new List<Post>() { new Post() { CreatorId = 1, Content = "Post!" }, new Post() { CreatorId = 3, Content = "Post!" } } }, new Event() { EventTitle = "event2", EventDescription = "this is an event! how amazing.", EventRules = "c'mon. we already talked about rules.", EventLocation = "tranquility base,Moon,Space", EventDate = DateTime.Now.AddDays(42), EventCreatorId = 2, Posts = new List<Post>() { new Post() { CreatorId = 1, Content = "Post!" }, new Post() { CreatorId = 3, Content = "Post!" } } } };
            List<Hobby> hobbies = new List<Hobby>() { new Hobby() {HobbyName="Hobby1",Description ="This is the best Hobby Ever! So many Fun things happening here!",KeyFeatures ="fun,sports,more fun",Rules="You have to have fun!" }, new Hobby() {HobbyName="Fight Club",Description = "Its fight club, you don't know?",Rules= "1. you don't talk about fight club.\n 2.you don't talk about fight club\n 3.you don't talk about fight club",KeyFeatures="Fight,club",Guides=guides,Events=events } };
            _context.Categories.Add(new Category() {CategoryName="Category1",Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus maximus iaculis lacus, vitae elementum odio dictum finibus. Quisque lacinia mauris vitae mauris commodo, eget pharetra tellus posuere. Sed ullamcorper nulla a velit volutpat accumsan. Nunc neque elit,",Hobbies= hobbies });
            _context.Categories.Add(new Category() { CategoryName = "Category2", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus maximus iaculis lacus, vitae elementum odio dictum finibus. Quisque lacinia mauris vitae mauris commodo, eget pharetra tellus posuere. Sed ullamcorper nulla a velit volutpat accumsan. Nunc neque elit,", Hobbies = hobbies });




            await _context.Categories.AddAsync(new Category { CategoryName = "Category test", Description = "test test" });
            await _context.SaveChangesAsync();
        }
      


}
}
