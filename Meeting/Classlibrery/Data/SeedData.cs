using DataAccess.Data.Entities;
using DataAccess.Data.Entities.Bridge_Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
            if (_context.Users.Any())
            {
                return;
            }
            await _roleManager.CreateAsync(new IdentityRole<int>("Member"));
            await _roleManager.CreateAsync(new IdentityRole<int>("Admin"));
            List<string> Fnames = new() { "Liam", "Noah", "Olivia", "Amelia", "John", "Emma" };
            List<string> Lnames = new() { "Smith", "Johnson", "Williams", "Lopez", "Martinez", "Lee" };
            List<User> users = new List<User>();
            string password = "Password123!";

            for (int i = 0; i < Fnames.Count; i++)
            {
                User u = new User() { UserName = $"{Fnames[i]}_{Lnames[i]}", Email = $"TestEmail{i}@gmail.com", FirstName = $"{Fnames[i]}", LastName = $"{Lnames[i]}", DateOfBirth = DateTime.Now.AddMonths(i * -2).AddYears(i * -1), City = "Tel Aviv", Country = "Israel" };
                users.Add(u);
                await _userManager.CreateAsync(u,password);
            }
            //List<Guide> guides = new List<Guide>() { new Guide() { Title="Guide1",Content=lorm,Creator = users[2] }, new Guide() { Title = "Guide2", Content = lorm, Creator = users[1] } };
            //List<Event> events = new List<Event>() { new Event() { EventTitle = "event1", EventDescription = "this is an event! how amazing.", EventRules = "c'mon. we already talked about rules.", EventLocation = "Independence square,Tel Aviv,Israel", EventDate = DateTime.Now.AddDays(15), EventCreatorId = 5, Posts = new List<Post>() { new Post() { CreatorId = 1, Content = "Post!" }, new Post() { CreatorId = 3, Content = "Post!" } } }, new Event() { EventTitle = "event2", EventDescription = "this is an event! how amazing.", EventRules = "c'mon. we already talked about rules.", EventLocation = "tranquility base,Moon,Space", EventDate = DateTime.Now.AddDays(42), EventCreatorId = 2, Posts = new List<Post>() { new Post() { CreatorId = 1, Content = "Post!" }, new Post() { CreatorId = 3, Content = "Post!" } } } };
            //List<Hobby> hobbies = new List<Hobby>() { new Hobby() {HobbyName="Hobby1",Description ="This is the best Hobby Ever! So many Fun things happening here!",KeyFeatures ="fun,sports,more fun",Rules="You have to have fun!" }, new Hobby() {HobbyName="Fight Club",Description = "Its fight club, you don't know?",Rules= "1. you don't talk about fight club.\n 2.you don't talk about fight club\n 3.you don't talk about fight club",KeyFeatures="Fight,club",Guides=guides,Events=events } };
            //_context.Categories.Add(new Category() {CategoryName="Category1",Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus maximus iaculis lacus, vitae elementum odio dictum finibus. Quisque lacinia mauris vitae mauris commodo, eget pharetra tellus posuere. Sed ullamcorper nulla a velit volutpat accumsan. Nunc neque elit,",Hobbies= hobbies });
            //_context.Categories.Add(new Category() { CategoryName = "Category2", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus maximus iaculis lacus, vitae elementum odio dictum finibus. Quisque lacinia mauris vitae mauris commodo, eget pharetra tellus posuere. Sed ullamcorper nulla a velit volutpat accumsan. Nunc neque elit,", Hobbies = hobbies });

            Guide g1 = new Guide() { Creator = users[1], Title = "My First Guide", Content = lorm };
            Guide g2 = new Guide() { Creator = users[3], Title = "This is a Guide about doing a guide.", Content = lorm };

            Event e1 = new Event() { Creator = users[2], EventTitle = "Lets all Meet!", EventDate = DateTime.Now.AddDays(2), EventDescription = "We will meet and Have FUN!", EventRules = "Be nice or leave!", EventLocation = "Independence square,Tel Aviv,Israel", Posts = new List<Post>() { new Post() { CreatorId = 1, Content = "This is gonna be fun!" }, new Post() { CreatorId = 3, Content = "first!!" } }, Users = new List<UserEvent>() { new UserEvent() { User = users[2],Arriving= true }, new UserEvent() { User = users[1], Arriving = true }, new UserEvent() { User = users[5], Arriving = true } } };
            Event e2 = new Event() { Creator = users[5], EventTitle = "Super secret meeting in space!", EventDate = DateTime.Now.AddDays(2), EventDescription = "Its a Secret!", EventRules = "Be nice or leave!", EventLocation = "tranquility base,Moon,Space", Posts = new List<Post>() { new Post() { CreatorId = 1, Content = "This is gonna be fun!" }, new Post() { CreatorId = 3, Content = "first!!" } }, Users = new List<UserEvent>() { new UserEvent() { User = users[4], Arriving = true }, new UserEvent() { User = users[1], Arriving = true }, new UserEvent() { User = users[5], Arriving = true } } };

            Hobby h1 = new Hobby() { HobbyName = "Astronomy", Description = "For people who love SPACE!", Rules = "Only Space Talking", KeyFeatures = "Science,Space,Moon", Guides = new List<Guide>() { g2}, Events = new List<Event>() {  e2 }, Users = new List<UserHobby>() { new UserHobby() { User = users[1],Following=true }, new UserHobby() { User = users[2], Following = true }, new UserHobby() { User = users[3], Following = true } } };
            Hobby h2 = new Hobby() { HobbyName = "Fight Club", Description = "Welcome to Fight Club", Rules = "The first rule of Fight Club is: you do not talk about Fight Club. \nThe second rule of Fight Club is: you DO NOT talk about Fight Club! \nThird rule of Fight Club: if someone yells “stop!”, goes limp, or taps out, the fight is over.", KeyFeatures = "Sport,Fight,Movie", Guides = new List<Guide>() {  g1 }, Events = new List<Event>() { e1 }, Users = new List<UserHobby>() { new UserHobby() { User = users[3], Following = true }, new UserHobby() { User = users[4], Following = true }, new UserHobby() { User = users[5], Following = true } } };

            Category c1 = new Category() { CategoryName = "Sports", Description = "All sorts of Sports!", Hobbies = new List<Hobby>() { h2 } };
            Category c2 = new Category() { CategoryName = "Science", Description = "All sorts of smart stuff!", Hobbies = new List<Hobby>() { h1 } };
            await _context.Categories!.AddRangeAsync(new List<Category>() {c1,c2});
            await _context.SaveChangesAsync();
        }



    }
}
